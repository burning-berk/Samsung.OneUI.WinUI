using System;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Samsung.OneUI.WinUI.Services.Animation;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.UI;
using Windows.UI.ViewManagement;
using WinRT;
using WinRT.Samsung_OneUI_WinUIVtableClasses;

namespace Samsung.OneUI.WinUI.Controls;

[WinRTRuntimeClassName("Microsoft.UI.Xaml.IUIElementOverrides")]
[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_DatePickerSpinnerListWinRTTypeDetails))]
public sealed class PageIndicator : Control
{
	private const int MAX_PIPS = 8;

	private const string PLAY_ICON = "PART_PlayIcon";

	private const string PAUSE_ICON = "PART_PauseIcon";

	private const string PREVIOUS_BUTTON = "PART_PreviousPageButton";

	private const string NEXT_BUTTON = "PART_NextPageButton";

	private const string BUTTON_CONTAINER = "PART_IndicatorContainer";

	private const string PLAY_PAUSE_BUTTON = "PART_PlayPauseButton";

	private const string SELECTED_STYLE = "OneUIPageIndicatorSelectedDotButtonStyle";

	private const string UN_SELECTED_STYLE = "OneUIPageIndicatorUnSelectedDotButtonStyle";

	private const string OVER_STYLE = "OneUIPageIndicatorOverDotButtonStyle";

	private const string ROOT_GRID = "RootGrid";

	private const string OFFSET_X = "Offset.X";

	private const string COLOR = "Color";

	private const string OPACITY = "Opacity";

	private const string SCALE = "Scale";

	private const int DEFAUT_MAX_VISIBLE_PIPS = 5;

	private const int DEFAUT_AUTO_PLAY_INTERVAL = 1000;

	private const int MIN_AUTO_PLAY_INTERVAL = 10;

	private const int MIN_NUMBER_OF_PAGE = 2;

	private const int ELLISPSE_UNSELECTED_PIP_SIZE = 6;

	private const string POINTER_OVER_FOCUSED_STATE = "PointerOverFocused";

	private readonly Style _unselectedStyle = Application.Current.Resources["OneUIPageIndicatorUnSelectedDotButtonStyle"] as Style;

	private readonly Style _selectedStyle = Application.Current.Resources["OneUIPageIndicatorSelectedDotButtonStyle"] as Style;

	private readonly Style _overStyle = Application.Current.Resources["OneUIPageIndicatorOverDotButtonStyle"] as Style;

	private int _pipSelectedIndex;

	private int _selectedPage;

	private int _numberOfPips;

	private bool _isFirstLoaded;

	private readonly DispatcherTimer _autoNextPageTimer = new DispatcherTimer();

	private StackPanel _indicatorContainer;

	private Button _animationEffectButton;

	private Button _prevArrowButton;

	private Button _nextArrowButton;

	private Button _playPauseButton;

	private Grid _rootGrid;

	private FrameworkElement _playIcon;

	private FrameworkElement _pauseIcon;

	private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

	private ScalarKeyFrameAnimation _slideAnimation;

	private Compositor _compositor;

	private Visual _reduceOpacityPip;

	private Visual _reduceSizePip;

	private Visual _increaseAndColorChangePip;

	private Visual _increaseOpacityPip;

	private Visual _pipPanelVisual;

	private Ellipse _ellipseReduceSizePip;

	private Ellipse _ellipseIncreaseAndColorChangePip;

	private CompositionScopedBatch _runningBatch;

	private double _buttonWidth = 14.0;

	private Color _unselectedPipColor;

	private Color _selectedPipColor;

	private readonly UISettings _uiSettings = new UISettings();

	private readonly IPageIndicatorAnimationService _pageIndicatorAnimationService = new PageIndicatorAnimationService();

	public static readonly DependencyProperty AutoPlayIntervalProperty = DependencyProperty.Register("AutoPlayInterval", typeof(int), typeof(PageIndicator), new PropertyMetadata(1000, OnAutoPlayIntervalChanged));

	public static readonly DependencyProperty NumberOfPagesProperty = DependencyProperty.Register("NumberOfPages", typeof(int), typeof(PageIndicator), new PropertyMetadata(0, OnNumberOfPagesChanged));

	public static readonly DependencyProperty SelectedPageIndexProperty = DependencyProperty.Register("SelectedPageIndex", typeof(int), typeof(PageIndicator), new PropertyMetadata(0, OnSelectedPageIndexChanged));

	public static readonly DependencyProperty MaxVisiblePipsProperty = DependencyProperty.Register("MaxVisiblePips", typeof(int), typeof(PageIndicator), new PropertyMetadata(5, OnMaxVisiblePipsChanged));

	public static readonly DependencyProperty PreviousButtonVisibilityProperty = DependencyProperty.Register("PreviousButtonVisibility", typeof(Visibility), typeof(PageIndicator), new PropertyMetadata(Visibility.Collapsed, OnPreviousButtonVisibilityChanged));

	public static readonly DependencyProperty NextButtonVisibilityProperty = DependencyProperty.Register("NextButtonVisibility", typeof(Visibility), typeof(PageIndicator), new PropertyMetadata(Visibility.Collapsed, OnNextButtonVisibilityChanged));

	public static readonly DependencyProperty PlayPauseButtonVisibilityProperty = DependencyProperty.Register("PlayPauseButtonVisibility", typeof(Visibility), typeof(PageIndicator), new PropertyMetadata(Visibility.Collapsed, OnPlayPauseButtonVisibilityChanged));

	public static readonly DependencyProperty IsClickActionEnableProperty = DependencyProperty.Register("IsClickActionEnable", typeof(bool), typeof(PageIndicator), new PropertyMetadata(true, OnIsClickActionEnablePropertyChanged));

	public static readonly DependencyProperty IsLoopingProperty = DependencyProperty.Register("IsLooping", typeof(bool), typeof(PageIndicator), new PropertyMetadata(null));

	public int AutoPlayInterval
	{
		get
		{
			return (int)GetValue(AutoPlayIntervalProperty);
		}
		set
		{
			SetValue(AutoPlayIntervalProperty, Math.Max(value, 10));
		}
	}

	public int NumberOfPages
	{
		get
		{
			return (int)GetValue(NumberOfPagesProperty);
		}
		set
		{
			if (_rootGrid != null)
			{
				_rootGrid.Visibility = ((value < 2) ? Visibility.Collapsed : Visibility.Visible);
			}
			if (value >= 2)
			{
				SetValue(NumberOfPagesProperty, value);
			}
		}
	}

	public int SelectedPageIndex
	{
		get
		{
			return (int)GetValue(SelectedPageIndexProperty);
		}
		set
		{
			if (_isFirstLoaded)
			{
				value = ((!IsLooping || value != NumberOfPages || !(_autoNextPageTimer != null) || !_autoNextPageTimer.IsEnabled) ? MakeClampedValue(value, 0, NumberOfPages - 1) : 0);
			}
			SetValue(SelectedPageIndexProperty, value);
		}
	}

	public int MaxVisiblePips
	{
		get
		{
			return (int)GetValue(MaxVisiblePipsProperty);
		}
		set
		{
			SetValue(MaxVisiblePipsProperty, MakeClampedValue(value, 2, 8));
		}
	}

	public Visibility PreviousButtonVisibility
	{
		get
		{
			return (Visibility)GetValue(PreviousButtonVisibilityProperty);
		}
		set
		{
			SetValue(PreviousButtonVisibilityProperty, value);
		}
	}

	public Visibility NextButtonVisibility
	{
		get
		{
			return (Visibility)GetValue(NextButtonVisibilityProperty);
		}
		set
		{
			SetValue(NextButtonVisibilityProperty, value);
		}
	}

	public Visibility PlayPauseButtonVisibility
	{
		get
		{
			return (Visibility)GetValue(PlayPauseButtonVisibilityProperty);
		}
		set
		{
			SetValue(PlayPauseButtonVisibilityProperty, value);
		}
	}

	public bool IsClickActionEnable
	{
		get
		{
			return (bool)GetValue(IsClickActionEnableProperty);
		}
		set
		{
			SetValue(IsClickActionEnableProperty, value);
		}
	}

	public bool IsLooping
	{
		get
		{
			return (bool)GetValue(IsLoopingProperty);
		}
		set
		{
			SetValue(IsLoopingProperty, value);
		}
	}

	public event EventHandler<SelectedIndexChangedEventArgs> SelectedPageChanged;

	public PageIndicator()
	{
		base.DefaultStyleKey = typeof(PageIndicator);
		InitCompositor();
		if (_pageIndicatorAnimationService != null)
		{
			_pageIndicatorAnimationService.InitializeCompositor(_compositor);
		}
		base.Loaded += OnLoaded;
		base.Unloaded += OnUnLoaded;
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_indicatorContainer = GetTemplateChild("PART_IndicatorContainer") as StackPanel;
		_prevArrowButton = GetTemplateChild("PART_PreviousPageButton") as Button;
		_nextArrowButton = GetTemplateChild("PART_NextPageButton") as Button;
		_playPauseButton = GetTemplateChild("PART_PlayPauseButton") as Button;
		_rootGrid = GetTemplateChild("RootGrid") as Grid;
		_playPauseButton.ApplyTemplate();
		_playIcon = UIExtensionsInternal.FindChildByName<ContentControl>("PART_PlayIcon", _playPauseButton);
		_pauseIcon = UIExtensionsInternal.FindChildByName<ContentControl>("PART_PauseIcon", _playPauseButton);
		_pipPanelVisual = ElementCompositionPreview.GetElementVisual(_indicatorContainer);
		_animationEffectButton = CreatePip(_unselectedStyle);
		RegisterEvent();
	}

	private static void OnAutoPlayIntervalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PageIndicator pageIndicator && pageIndicator._autoNextPageTimer != null)
		{
			pageIndicator._autoNextPageTimer.Interval = TimeSpan.FromMilliseconds((int)e.NewValue);
		}
	}

	private static void OnNumberOfPagesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		int num = (int)e.NewValue;
		if (d is PageIndicator { _isFirstLoaded: not false } pageIndicator)
		{
			if (num != Math.Max(num, 2))
			{
				pageIndicator.SetValue(NumberOfPagesProperty, Math.Max(num, 2));
				return;
			}
			pageIndicator.InitPips();
			pageIndicator.SelectedPageIndex = 0;
			pageIndicator.ApplyPipStyle(0, pageIndicator._selectedStyle);
			pageIndicator.UpdateNextVisibility();
			pageIndicator.UpdatePreviousVisibility();
		}
	}

	private static async void OnSelectedPageIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PageIndicator { _isFirstLoaded: not false } control)
		{
			int newValue = (int)e.NewValue;
			if (newValue != MakeClampedValue(newValue, 0, control.NumberOfPages - 1))
			{
				control.SetValue(SelectedPageIndexProperty, MakeClampedValue(newValue, 0, control.NumberOfPages - 1));
			}
			bool runTimerAgain = false;
			if (control._autoNextPageTimer.IsEnabled)
			{
				control._autoNextPageTimer.Stop();
				runTimerAgain = true;
			}
			await control.CalculateSelectedPipIndexBySelectedPage();
			if (runTimerAgain && (newValue != control.NumberOfPages - 1 || control.IsLooping))
			{
				control._autoNextPageTimer.Start();
			}
			if (newValue == control.NumberOfPages - 1 && !control.IsLooping)
			{
				control.ChangePlayButtonState(visibility: true);
			}
			control.UpdateNextVisibility();
			control.UpdatePreviousVisibility();
			control.SelectedPageChanged?.Invoke(control, new SelectedIndexChangedEventArgs(newValue, (int)e.OldValue));
		}
	}

	private static void OnMaxVisiblePipsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		int num = (int)e.NewValue;
		if (d is PageIndicator { _isFirstLoaded: not false } pageIndicator)
		{
			if (num != MakeClampedValue(num, 2, 8))
			{
				pageIndicator.SetValue(MaxVisiblePipsProperty, MakeClampedValue(num, 2, 8));
				return;
			}
			pageIndicator.SelectedPageIndex = 0;
			pageIndicator.InitPips();
			pageIndicator.ApplyPipStyle(0, pageIndicator._selectedStyle);
		}
	}

	private static void OnPreviousButtonVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PageIndicator pageIndicator && !(pageIndicator._prevArrowButton == null))
		{
			pageIndicator._prevArrowButton.Visibility = pageIndicator.PreviousButtonVisibility;
			pageIndicator.UpdatePreviousVisibility();
		}
	}

	private static void OnNextButtonVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PageIndicator pageIndicator && !(pageIndicator._nextArrowButton == null))
		{
			pageIndicator._nextArrowButton.Visibility = pageIndicator.NextButtonVisibility;
			pageIndicator.UpdateNextVisibility();
		}
	}

	private static void OnPlayPauseButtonVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PageIndicator pageIndicator && !(pageIndicator._playPauseButton == null))
		{
			pageIndicator._playPauseButton.Visibility = pageIndicator.PlayPauseButtonVisibility;
			pageIndicator.UpdatedAutoPlayOnPlayPauseButtonVisibility();
		}
	}

	private static void OnIsClickActionEnablePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PageIndicator { _isFirstLoaded: not false } pageIndicator && !(pageIndicator._indicatorContainer == null))
		{
			bool flag = (bool)e.NewValue;
			pageIndicator._indicatorContainer.IsHitTestVisible = flag;
			pageIndicator.PlayPauseButtonVisibility = ((!flag || pageIndicator.PlayPauseButtonVisibility != Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible);
			pageIndicator.NextButtonVisibility = ((!flag || pageIndicator.NextButtonVisibility != Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible);
			pageIndicator.PreviousButtonVisibility = ((!flag || pageIndicator.PreviousButtonVisibility != Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible);
		}
	}

	private void RegisterEvent()
	{
		if (!SafetyNullCheckNavigationButtons() && !(_playPauseButton == null) && !(_animationEffectButton == null))
		{
			_animationEffectButton.Click += OnPipButtonClicked;
			_prevArrowButton.Click += OnPreviousButtonClick;
			_nextArrowButton.Click += OnNextButtonClick;
			_playPauseButton.Click += PlayButton_Click;
			_playPauseButton.PointerEntered += PlayPauseButton_PointerEntered;
			_playPauseButton.GotFocus += PlayPauseButton_GotFocus;
		}
	}

	private void UnRegisterEvent()
	{
		if (SafetyNullCheckNavigationButtons() || _playPauseButton == null || _indicatorContainer == null || _autoNextPageTimer == null || _animationEffectButton == null)
		{
			return;
		}
		_animationEffectButton.Click -= OnPipButtonClicked;
		_prevArrowButton.Click -= OnPreviousButtonClick;
		_nextArrowButton.Click -= OnNextButtonClick;
		_playPauseButton.Click -= PlayButton_Click;
		_autoNextPageTimer.Tick -= AutoNextPageTimer_Tick;
		_playPauseButton.PointerEntered -= PlayPauseButton_PointerEntered;
		_playPauseButton.GotFocus -= PlayPauseButton_GotFocus;
		foreach (Button item in _indicatorContainer.Children.OfType<Button>())
		{
			item.Click -= OnPipButtonClicked;
		}
	}

	private void PageIndicator_ActualThemeChanged(FrameworkElement sender, object args)
	{
		UpdateUIWhenChangedTheme();
	}

	private void OnLoaded(object sender, RoutedEventArgs e)
	{
		if (_autoNextPageTimer != null)
		{
			_autoNextPageTimer.Tick += AutoNextPageTimer_Tick;
		}
		UpdateUIWhenChangedTheme();
		ApplySelectedPageInFirstLoad();
		base.ActualThemeChanged += PageIndicator_ActualThemeChanged;
		_uiSettings.ColorValuesChanged += UISettings_ColorValuesChanged;
		_isFirstLoaded = true;
	}

	private void UISettings_ColorValuesChanged(UISettings sender, object args)
	{
		UpdateUIWhenChangedTheme();
	}

	private void OnUnLoaded(object sender, RoutedEventArgs e)
	{
		UnRegisterEvent();
	}

	private void AutoNextPageTimer_Tick(object sender, object e)
	{
		SelectedPageIndex++;
		if (_autoNextPageTimer.IsEnabled && SelectedPageIndex == NumberOfPages - 1 && !IsLooping)
		{
			_autoNextPageTimer.Stop();
			ChangePlayButtonState(visibility: true);
		}
	}

	private void OnNextButtonClick(object sender, RoutedEventArgs e)
	{
		SelectedPageIndex++;
	}

	private void OnPreviousButtonClick(object sender, RoutedEventArgs e)
	{
		SelectedPageIndex--;
	}

	private async void OnPipButtonClicked(object sender, RoutedEventArgs e)
	{
		FrameworkElement pipButton = sender as FrameworkElement;
		if (!(pipButton == null) && !(_autoNextPageTimer == null) && IsClickActionEnable)
		{
			await StopAnimation();
			int num = _indicatorContainer.Children.IndexOf(pipButton);
			SelectedPageIndex += num - _pipSelectedIndex;
		}
	}

	private void PlayButton_Click(object sender, RoutedEventArgs e)
	{
		if (SelectedPageIndex != NumberOfPages - 1 || IsLooping)
		{
			if (_autoNextPageTimer.IsEnabled)
			{
				_autoNextPageTimer.Stop();
				ChangePlayButtonState(visibility: true);
			}
			else
			{
				_autoNextPageTimer.Start();
				ChangePlayButtonState(visibility: false);
			}
		}
	}

	private void PlayPauseButton_GotFocus(object sender, RoutedEventArgs e)
	{
		if (IsPlayPauseButtonHovered() && IsPlayPauseButtonKeyboardFocused())
		{
			VisualStateManager.GoToState(_playPauseButton, "PointerOverFocused", useTransitions: false);
		}
	}

	private void PlayPauseButton_PointerEntered(object sender, PointerRoutedEventArgs e)
	{
		if (IsPlayPauseButtonKeyboardFocused())
		{
			VisualStateManager.GoToState(_playPauseButton, "PointerOverFocused", useTransitions: false);
		}
	}

	private void InitCompositor()
	{
		_compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
	}

	private void UpdateUIWhenChangedTheme()
	{
		base.DispatcherQueue.TryEnqueue(delegate
		{
			ApplyStyleForAllPip();
		});
	}

	private void UpdatedAutoPlayOnPlayPauseButtonVisibility()
	{
		if (PlayPauseButtonVisibility == Visibility.Visible)
		{
			_autoNextPageTimer.Start();
			ChangePlayButtonState(visibility: false);
		}
		else if (_autoNextPageTimer.IsEnabled)
		{
			_autoNextPageTimer.Stop();
			ChangePlayButtonState(visibility: true);
		}
	}

	private Color GetColorFromView(int index)
	{
		if (_pageIndicatorAnimationService != null)
		{
			Ellipse ellipse = PageIndicatorAnimationService.FindEllipseInPip(_indicatorContainer.Children[index] as Button);
			if (ellipse != null)
			{
				return ((SolidColorBrush)ellipse.Fill).Color;
			}
		}
		return _unselectedPipColor;
	}

	private async void ApplySelectedPageInFirstLoad()
	{
		NumberOfPages = Math.Max(0, NumberOfPages);
		SelectedPageIndex = MakeClampedValue(SelectedPageIndex, 0, NumberOfPages - 1);
		MaxVisiblePips = Math.Clamp(MaxVisiblePips, 2, 8);
		_autoNextPageTimer.Interval = TimeSpan.FromMilliseconds(AutoPlayInterval);
		_playPauseButton.Visibility = PlayPauseButtonVisibility;
		UpdatedAutoPlayOnPlayPauseButtonVisibility();
		_indicatorContainer.IsHitTestVisible = IsClickActionEnable;
		PlayPauseButtonVisibility = ((!IsClickActionEnable || PlayPauseButtonVisibility != Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible);
		NextButtonVisibility = ((!IsClickActionEnable || NextButtonVisibility != Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible);
		PreviousButtonVisibility = ((!IsClickActionEnable || PreviousButtonVisibility != Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible);
		if (_rootGrid != null && NumberOfPages < 2)
		{
			_rootGrid.Visibility = Visibility.Collapsed;
		}
		UpdatePreviousVisibility();
		UpdateNextVisibility();
		InitPips(isInit: true);
		await CalculateSelectedPipIndexBySelectedPage();
	}

	private static int MakeClampedValue(int value, int min, int max)
	{
		return Math.Max(Math.Min(value, max), min);
	}

	private int CalculateNumberOfPip()
	{
		return Math.Min(Math.Min(NumberOfPages, MaxVisiblePips), 8);
	}

	private void InitPips(bool isInit = false)
	{
		if (!(_indicatorContainer == null))
		{
			int count = _indicatorContainer.Children.Count;
			_numberOfPips = CalculateNumberOfPip();
			if (count > _numberOfPips)
			{
				RemovePips(count);
			}
			else
			{
				AddMorePips(isInit, count);
			}
			Style style = ((_numberOfPips == 8 && SelectedPageIndex >= _numberOfPips - 1) ? _overStyle : _unselectedStyle);
			Style style2 = ((_numberOfPips == 8 && SelectedPageIndex < NumberOfPages - _numberOfPips + 1) ? _overStyle : _unselectedStyle);
			ApplyPipStyle(0, style);
			ApplyPipStyle(_indicatorContainer.Children.Count - 1, style2);
			SelectedPageIndex = ((_numberOfPips != count && !isInit) ? (-1) : SelectedPageIndex);
		}
	}

	private void UpdateElementVisual(ScrollDirection scrollDirection)
	{
		_reduceOpacityPip = ElementCompositionPreview.GetElementVisual(_indicatorContainer.Children[(scrollDirection != ScrollDirection.Right) ? _numberOfPips : 0]);
		_ellipseReduceSizePip = PageIndicatorAnimationService.FindEllipseInPip(_indicatorContainer.Children[(scrollDirection == ScrollDirection.Right) ? 1 : (_numberOfPips - 1)]);
		_reduceSizePip = ElementCompositionPreview.GetElementVisual(_ellipseReduceSizePip);
		Grid reference = VisualTreeHelper.GetChild(_indicatorContainer.Children[(scrollDirection != ScrollDirection.Right) ? 1 : (_numberOfPips - 1)], 0) as Grid;
		_ellipseIncreaseAndColorChangePip = VisualTreeHelper.GetChild(reference, 0) as Ellipse;
		_increaseAndColorChangePip = ElementCompositionPreview.GetElementVisual(_ellipseIncreaseAndColorChangePip);
		_increaseOpacityPip = ElementCompositionPreview.GetElementVisual(_indicatorContainer.Children[(scrollDirection == ScrollDirection.Right) ? _numberOfPips : 0]);
	}

	private void RemovePips(int currentNumberOfPips)
	{
		int num = currentNumberOfPips - _numberOfPips;
		int num2 = currentNumberOfPips - 1;
		while (num2 >= 0 && num > 0)
		{
			if (_indicatorContainer.Children[num2] is Button { FocusState: FocusState.Unfocused } button)
			{
				_indicatorContainer.Children.Remove(button);
				num--;
			}
			num2--;
		}
	}

	private void AddMorePips(bool isInit, int currentNumberOfPips)
	{
		for (int i = currentNumberOfPips; i < _numberOfPips; i++)
		{
			Button pip = CreatePip(_unselectedStyle);
			pip.Click += OnPipButtonClicked;
			_indicatorContainer.Children.Add(pip);
			ApplyPipStyle(i, _unselectedStyle);
			if (isInit && i == currentNumberOfPips)
			{
				base.DispatcherQueue.TryEnqueue(delegate
				{
					_indicatorContainer.UpdateLayout();
					_buttonWidth = pip.ActualWidth;
				});
			}
		}
	}

	private Button CreatePip(Style style)
	{
		return new Button
		{
			Style = style
		};
	}

	private void UpdateNextVisibility()
	{
		if (!(_nextArrowButton == null))
		{
			_nextArrowButton.Visibility = ((SelectedPageIndex == NumberOfPages - 1 || NextButtonVisibility == Visibility.Collapsed) ? Visibility.Collapsed : Visibility.Visible);
		}
	}

	private void UpdatePreviousVisibility()
	{
		if (!(_prevArrowButton == null))
		{
			_prevArrowButton.Visibility = ((SelectedPageIndex <= 0 || PreviousButtonVisibility == Visibility.Collapsed) ? Visibility.Collapsed : Visibility.Visible);
		}
	}

	public static Task AwaitBatchCompletedAsync(CompositionScopedBatch batch)
	{
		TaskCompletionSource<object> batchCompletedTcs = new TaskCompletionSource<object>();
		batch.Completed += OnCompleted;
		return batchCompletedTcs.Task;
		void OnCompleted(object sender, CompositionBatchCompletedEventArgs e)
		{
			batch.Completed -= OnCompleted;
			batchCompletedTcs.TrySetResult(null);
		}
	}

	private async Task StopAnimation()
	{
		if (_runningBatch != null)
		{
			StopVisualAnimation();
			await AwaitBatchCompletedAsync(_runningBatch);
		}
	}

	private void StopVisualAnimation()
	{
		_pipPanelVisual.StopAnimation("Offset.X");
		_increaseOpacityPip.StopAnimation("Opacity");
		_reduceOpacityPip.StopAnimation("Opacity");
		if (_numberOfPips == 8)
		{
			_reduceSizePip.StopAnimation("Scale");
			_increaseAndColorChangePip.StopAnimation("Scale");
		}
	}

	private async Task CalculateSelectedPipIndexBySelectedPage()
	{
		await _semaphoreSlim.WaitAsync();
		await StopAnimation();
		int pipSelectedIndex = _pipSelectedIndex;
		Style style = ((_pipSelectedIndex == 7) ? _overStyle : _unselectedStyle);
		ApplyPipStyle(_pipSelectedIndex, style);
		_pipSelectedIndex = ((SelectedPageIndex == -1) ? (-1) : _pipSelectedIndex);
		_selectedPage = ((SelectedPageIndex == -1) ? (-1) : _selectedPage);
		if (SelectedPageIndex <= -1 || SelectedPageIndex > NumberOfPages - 1 || NumberOfPages == 0 || MaxVisiblePips == 0)
		{
			_semaphoreSlim.Release();
			return;
		}
		int num = SelectedPageIndex - Math.Max(_selectedPage, -1);
		_pipSelectedIndex = MakeClampedValue(_pipSelectedIndex + num, 0, MaxVisiblePips - 1);
		_indicatorContainer.UpdateLayout();
		if (_pageIndicatorAnimationService != null)
		{
			_pageIndicatorAnimationService.RunColorChangedAnimation(pipSelectedIndex, _indicatorContainer, _selectedPipColor, _unselectedPipColor);
		}
		if (_pipSelectedIndex < 1 && SelectedPageIndex != 0)
		{
			_pipSelectedIndex++;
			AnimateToSlideIndex(ScrollDirection.Left);
			ApplyPipStyle(_pipSelectedIndex, _selectedStyle);
		}
		else if (_pipSelectedIndex > _numberOfPips - 2 && SelectedPageIndex != NumberOfPages - 1)
		{
			_pipSelectedIndex--;
			AnimateToSlideIndex(ScrollDirection.Right);
			ApplyPipStyle(_pipSelectedIndex + 1, _selectedStyle);
		}
		else if (_pageIndicatorAnimationService != null)
		{
			_pageIndicatorAnimationService.RunColorChangedAnimation(_pipSelectedIndex, _indicatorContainer, _unselectedPipColor, _selectedPipColor);
			ApplyPipStyle(_pipSelectedIndex, _selectedStyle);
			ApplyPipStyle(pipSelectedIndex, _unselectedStyle);
		}
		_selectedPage = SelectedPageIndex;
		_semaphoreSlim.Release();
	}

	private void ApplyPipStyle(int pipIndex, Style style)
	{
		if (!(_indicatorContainer == null) && pipIndex >= 0 && pipIndex <= MaxVisiblePips && _indicatorContainer.Children[pipIndex] is Button button)
		{
			button.Style = style;
		}
	}

	private void ChangePlayButtonState(bool visibility)
	{
		if (!(_playIcon == null) && !(_pauseIcon == null))
		{
			_playIcon.Visibility = ((!visibility) ? Visibility.Collapsed : Visibility.Visible);
			_pauseIcon.Visibility = (visibility ? Visibility.Collapsed : Visibility.Visible);
		}
	}

	private bool IsPlayPauseButtonHovered()
	{
		return _playPauseButton.IsPointerOver;
	}

	private bool IsPlayPauseButtonKeyboardFocused()
	{
		return _playPauseButton.FocusState == FocusState.Keyboard;
	}

	private void AnimateToSlideIndex(ScrollDirection scrollDirection)
	{
		if (!(_animationEffectButton == null) && !(_indicatorContainer == null))
		{
			_animationEffectButton.Margin = ((scrollDirection == ScrollDirection.Right) ? new Thickness(0.0, 0.0, 0.0 - _buttonWidth, 0.0) : new Thickness(0.0 - _buttonWidth, 0.0, 0.0, 0.0));
			int num = ((scrollDirection == ScrollDirection.Right) ? _indicatorContainer.Children.Count : 0);
			Style style = ((MaxVisiblePips == 8 && SelectedPageIndex != 1 && SelectedPageIndex != NumberOfPages - 2) ? _overStyle : _unselectedStyle);
			_indicatorContainer.Children.Insert(num, _animationEffectButton);
			ApplyPipStyle(num, style);
			_indicatorContainer.UpdateLayout();
			int removePipPosition = ((scrollDirection != ScrollDirection.Right) ? _numberOfPips : 0);
			double newOffset = (0.0 - _buttonWidth) * (double)scrollDirection;
			UpdateElementVisual(scrollDirection);
			ScrollAnimation(newOffset, removePipPosition);
		}
	}

	private void ApplyStyleForAllPip()
	{
		for (int i = 0; i < _indicatorContainer.Children.Count; i++)
		{
			ApplyPipStyle(i, _unselectedStyle);
		}
		if (_numberOfPips == 8)
		{
			ApplyPipStyle(0, (SelectedPageIndex != 1) ? _overStyle : _unselectedStyle);
			ApplyPipStyle(_numberOfPips - 1, (SelectedPageIndex != NumberOfPages - 2) ? _overStyle : _unselectedStyle);
		}
		ApplyPipStyle(_pipSelectedIndex, _selectedStyle);
	}

	private void ScrollAnimation(double newOffset, int removePipPosition)
	{
		if (_pipPanelVisual == null || _pageIndicatorAnimationService == null)
		{
			return;
		}
		Vector3 currPipPanelVisualOffset = _pipPanelVisual.Offset;
		_slideAnimation = _pageIndicatorAnimationService.CreateOffsetAnimation(_pipPanelVisual, newOffset);
		ScalarKeyFrameAnimation scalarKeyFrameAnimation = _pageIndicatorAnimationService.CreateOpacityPipAnimation(0.0);
		Vector3KeyFrameAnimation vector3KeyFrameAnimation = _pageIndicatorAnimationService.CreateScalePipAnimation(_reduceSizePip, _ellipseReduceSizePip, 2f / 3f);
		CompositionColorBrush compositionColorBrush = _compositor.CreateColorBrush(_unselectedPipColor);
		ColorKeyFrameAnimation colorKeyFrameAnimation = _pageIndicatorAnimationService.CreateColorPipAnimation(compositionColorBrush, _selectedPipColor, _ellipseIncreaseAndColorChangePip);
		Vector3KeyFrameAnimation vector3KeyFrameAnimation2 = _pageIndicatorAnimationService.CreateScalePipAnimation(_increaseAndColorChangePip, _ellipseIncreaseAndColorChangePip, (_ellipseIncreaseAndColorChangePip.ActualWidth == 6.0) ? 1f : 1.5f);
		ScalarKeyFrameAnimation scalarKeyFrameAnimation2 = _pageIndicatorAnimationService.CreateOpacityPipAnimation(1.0);
		CompositionScopedBatch compositionScopedBatch = _compositor.CreateScopedBatch(CompositionBatchTypes.Animation);
		if (_numberOfPips == 8 && vector3KeyFrameAnimation != null && vector3KeyFrameAnimation2 != null)
		{
			_reduceSizePip.StartAnimation("Scale", vector3KeyFrameAnimation);
			_increaseAndColorChangePip.StartAnimation("Scale", vector3KeyFrameAnimation2);
		}
		if (SafetyNullCheckOpacityPips() || compositionColorBrush == null || colorKeyFrameAnimation == null || _slideAnimation == null || scalarKeyFrameAnimation == null || scalarKeyFrameAnimation2 == null)
		{
			return;
		}
		_pipPanelVisual.StartAnimation("Offset.X", _slideAnimation);
		_reduceOpacityPip.StartAnimation("Opacity", scalarKeyFrameAnimation);
		_increaseOpacityPip.StartAnimation("Opacity", scalarKeyFrameAnimation2);
		compositionColorBrush.StartAnimation("Color", colorKeyFrameAnimation);
		compositionScopedBatch.End();
		_runningBatch = compositionScopedBatch;
		compositionScopedBatch.Completed += delegate
		{
			_increaseOpacityPip.Opacity = 1f;
			_reduceOpacityPip.Opacity = 0f;
			if (_indicatorContainer.Children.Count > 0)
			{
				_animationEffectButton.Margin = new Thickness(0.0);
				_animationEffectButton = CreatePip(_unselectedStyle);
				_animationEffectButton.Click += OnPipButtonClicked;
				_animationEffectButton.Opacity = 1.0;
				_indicatorContainer.Children.RemoveAt(removePipPosition);
				ApplyStyleForAllPip();
			}
			_runningBatch = null;
			_pipPanelVisual.Offset = currPipPanelVisualOffset;
		};
	}

	private bool SafetyNullCheckNavigationButtons()
	{
		if (!(_prevArrowButton == null))
		{
			return _nextArrowButton == null;
		}
		return true;
	}

	private bool SafetyNullCheckOpacityPips()
	{
		if (!(_reduceOpacityPip == null))
		{
			return _increaseOpacityPip == null;
		}
		return true;
	}
}
