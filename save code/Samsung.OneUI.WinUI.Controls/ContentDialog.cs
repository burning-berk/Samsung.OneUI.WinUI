using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.Xaml.Interactivity;
using Samsung.OneUI.WinUI.Controls.Behaviors;
using Samsung.OneUI.WinUI.Controls.EventHandlers;
using Samsung.OneUI.WinUI.Services.Animation;
using Samsung.OneUI.WinUI.Utils.Extensions;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.Foundation;
using Windows.UI.ViewManagement;

namespace Samsung.OneUI.WinUI.Controls;

public class ContentDialog : Microsoft.UI.Xaml.Controls.ContentDialog
{
	private const double DIALOG_MAX_HEIGHT = 660.0;

	private const int FADE_OUT_OPACITY = 0;

	private const double TOOLTIP_VERTICAL_OFFSET = 10.0;

	private const string DEFAULT_BACKGROUND_DIALOG_THEME_RESOURCE_KEY = "OneUIContentDialogFullBackground";

	private const string RECTANGLE_SMOKE_BACKGROUND_ELEMENT_NAME = "SmokeLayerBackground";

	private const string TITLE_COLLAPSED_STATE = "DialogTitleCollapsed";

	private const string TITLE_VISIBLE_STATE = "DialogTitleVisible";

	private const string HIDDEN_DIALOG_STORYBOARD_NAME = "HiddenStoryboard";

	private const string SHOWING_DIALOG_STORYBOARD_NAME = "DialogShowingAnimationStoryboard";

	private readonly IDialogAnimationService _dialogAnimationService;

	private readonly AccessibilitySettings _accessibilitySettings;

	private Rectangle _smokedBackground;

	private bool _isHighContrastEnabled;

	private Storyboard _hiddenStoryboard;

	private Storyboard _dialogShowingAnimationStoryboard;

	private bool _closeAnimationCompleted;

	public static readonly DependencyProperty DialogMaxHeightProperty = DependencyProperty.Register("DialogMaxHeight", typeof(double), typeof(ContentDialog), new PropertyMetadata(660.0));

	public static readonly DependencyProperty DialogWidthProperty = DependencyProperty.Register("DialogWidth", typeof(double), typeof(ContentDialog), new PropertyMetadata(360.0));

	public static readonly DependencyProperty DialogTitleAlignmentProperty = DependencyProperty.Register("DialogTitleAlignment", typeof(HorizontalAlignment), typeof(ContentDialog), new PropertyMetadata(HorizontalAlignment.Left));

	public static readonly DependencyProperty IsCloseButtonEnabledProperty = DependencyProperty.Register("IsCloseButtonEnabled", typeof(bool), typeof(ContentDialog), new PropertyMetadata(true));

	public static readonly DependencyProperty CustomSmokedBackgroundResourceKeyProperty = DependencyProperty.Register("CustomSmokedBackgroundResourceKey", typeof(string), typeof(ContentDialog), new PropertyMetadata(null, OnCustomSmokedBackgroundResourceKeyPropertyChanged));

	public static readonly DependencyProperty BackgroundDialogProperty = DependencyProperty.Register("BackgroundDialog", typeof(SolidColorBrush), typeof(ContentDialog), new PropertyMetadata(null));

	public static readonly DependencyProperty CustomAppBarMarginProperty = DependencyProperty.Register("CustomAppBarMargin", typeof(Thickness), typeof(ContentDialog), new PropertyMetadata(new Thickness(0.0)));

	public static readonly DependencyProperty ContentMarginProperty = DependencyProperty.Register("ContentMargin", typeof(Thickness), typeof(ContentDialog), new PropertyMetadata(new Thickness(0.0)));

	public double DialogMaxHeight
	{
		get
		{
			return (double)GetValue(DialogMaxHeightProperty);
		}
		set
		{
			SetValue(DialogMaxHeightProperty, value);
		}
	}

	public double DialogWidth
	{
		get
		{
			return (double)GetValue(DialogWidthProperty);
		}
		set
		{
			SetValue(DialogWidthProperty, value);
		}
	}

	public HorizontalAlignment DialogTitleAlignment
	{
		get
		{
			return (HorizontalAlignment)GetValue(DialogTitleAlignmentProperty);
		}
		set
		{
			SetValue(DialogTitleAlignmentProperty, value);
		}
	}

	public bool IsCloseButtonEnabled
	{
		get
		{
			return (bool)GetValue(IsCloseButtonEnabledProperty);
		}
		set
		{
			SetValue(IsCloseButtonEnabledProperty, value);
		}
	}

	public string CustomSmokedBackgroundResourceKey
	{
		get
		{
			return (string)GetValue(CustomSmokedBackgroundResourceKeyProperty);
		}
		set
		{
			SetValue(CustomSmokedBackgroundResourceKeyProperty, value);
		}
	}

	public SolidColorBrush BackgroundDialog
	{
		get
		{
			return (SolidColorBrush)GetValue(BackgroundDialogProperty);
		}
		set
		{
			SetValue(BackgroundDialogProperty, value);
		}
	}

	public Thickness CustomAppBarMargin
	{
		get
		{
			return (Thickness)GetValue(CustomAppBarMarginProperty);
		}
		set
		{
			SetValue(CustomAppBarMarginProperty, value);
		}
	}

	public Thickness ContentMargin
	{
		get
		{
			return (Thickness)GetValue(ContentMarginProperty);
		}
		set
		{
			SetValue(ContentMarginProperty, value);
		}
	}

	internal event TypedEventHandler<ContentDialog, OneUIContentDialogClosingEventArgs> AnimationClosing;

	internal event EventHandler AnimationClosed;

	private static void OnCustomSmokedBackgroundResourceKeyPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is ContentDialog contentDialog)
		{
			contentDialog.UpdateSmokedBackground();
		}
	}

	public ContentDialog()
	{
		_dialogAnimationService = new DialogAnimationService();
		_accessibilitySettings = new AccessibilitySettings();
		_isHighContrastEnabled = _accessibilitySettings.HighContrast;
		base.Loaded += ContentDialog_Loaded;
		base.Unloaded += ContentDialog_Unloaded;
		base.Closing += ContentDialog_Closing;
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_smokedBackground = GetTemplateChild("SmokeLayerBackground") as Rectangle;
		_hiddenStoryboard = GetTemplateChild("HiddenStoryboard") as Storyboard;
		_dialogShowingAnimationStoryboard = GetTemplateChild("DialogShowingAnimationStoryboard") as Storyboard;
		UpdateSmokedBackground();
	}

	private void OnTitlePropertyChanged(DependencyObject sender, DependencyProperty dp)
	{
		UpdateTitleVisiblityState();
		TrimmingTitle();
	}

	private void ContentDialog_ActualThemeChanged(FrameworkElement sender, object args)
	{
		UpdateSmokedBackground();
	}

	private void ContentDialog_LayoutUpdated(object sender, object e)
	{
		if (!(_accessibilitySettings == null) && _accessibilitySettings.HighContrast != _isHighContrastEnabled)
		{
			_isHighContrastEnabled = _accessibilitySettings.HighContrast;
			UpdateSmokedBackground();
		}
	}

	private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
	{
		base.ActualThemeChanged += ContentDialog_ActualThemeChanged;
		base.LayoutUpdated += ContentDialog_LayoutUpdated;
		UpdateSmokedBackground();
		UpdateTitleVisiblityState();
		TrimmingTitle();
		RegisterPropertyChangedCallback(Microsoft.UI.Xaml.Controls.ContentDialog.TitleProperty, OnTitlePropertyChanged);
		ClearVisualStateTransitions();
		if (!(_dialogShowingAnimationStoryboard == null))
		{
			_dialogShowingAnimationStoryboard.SafeBegin();
		}
	}

	private void ContentDialog_Unloaded(object sender, RoutedEventArgs e)
	{
		base.ActualThemeChanged -= ContentDialog_ActualThemeChanged;
		base.LayoutUpdated -= ContentDialog_LayoutUpdated;
	}

	private void ContentDialog_Closing(Microsoft.UI.Xaml.Controls.ContentDialog sender, ContentDialogClosingEventArgs args)
	{
		if (_closeAnimationCompleted)
		{
			return;
		}
		OneUIContentDialogClosingEventArgs e = new OneUIContentDialogClosingEventArgs(args);
		this.AnimationClosing?.Invoke(this, e);
		args.Cancel = e.Cancel;
		if (e.Cancel)
		{
			_closeAnimationCompleted = false;
			return;
		}
		args.Cancel = true;
		if (!(_hiddenStoryboard == null))
		{
			_hiddenStoryboard.Completed -= HiddenStoryboard_Completed;
			_hiddenStoryboard.Completed += HiddenStoryboard_Completed;
			_hiddenStoryboard.SafeBegin();
			_dialogAnimationService.RunBackgroundOpacity(_smokedBackground, 0.0);
		}
	}

	private void HiddenStoryboard_Completed(object sender, object e)
	{
		_closeAnimationCompleted = true;
		Hide();
		this.AnimationClosed?.Invoke(this, null);
	}

	private void UpdateSmokedBackground()
	{
		if (_smokedBackground != null)
		{
			ApplyRectangleBackgroundColor(_smokedBackground);
			_dialogAnimationService.RunBackgroundOpacity(_smokedBackground, _smokedBackground.Fill.Opacity);
		}
	}

	private void ClearVisualStateTransitions()
	{
		foreach (Popup item in VisualTreeHelper.GetOpenPopupsForXamlRoot(base.XamlRoot))
		{
			if (item != null)
			{
				item.ChildTransitions = new TransitionCollection();
			}
		}
	}

	private void ApplyRectangleBackgroundColor(Rectangle rectangle)
	{
		if (!(rectangle == null))
		{
			Brush smokedBackgroundBrush = GetSmokedBackgroundBrush();
			rectangle.Fill = smokedBackgroundBrush;
			rectangle.Opacity = 0.0;
		}
	}

	private Brush GetSmokedBackgroundBrush()
	{
		if (!string.IsNullOrEmpty(CustomSmokedBackgroundResourceKey))
		{
			return GetBrushFromResource(CustomSmokedBackgroundResourceKey);
		}
		if (BackgroundDialog != null)
		{
			return new SolidColorBrush
			{
				Color = BackgroundDialog.Color,
				Opacity = BackgroundDialog.Opacity
			}.Color.SetOpacity(BackgroundDialog.Opacity);
		}
		return GetBrushFromResource("OneUIContentDialogFullBackground");
	}

	private Brush GetBrushFromResource(string resourceKey)
	{
		return ((resourceKey.GetKey() is SolidColorBrush solidColorBrush) ? solidColorBrush.Color.SetOpacity(solidColorBrush.Opacity) : null) ?? null;
	}

	private void UpdateTitleVisiblityState()
	{
		if ((base.Title is TextBlock textBlock && string.IsNullOrWhiteSpace(textBlock.Text)) || (base.Title is string value && string.IsNullOrWhiteSpace(value)))
		{
			VisualStateManager.GoToState(this, "DialogTitleCollapsed", useTransitions: false);
		}
		else
		{
			VisualStateManager.GoToState(this, "DialogTitleVisible", useTransitions: false);
		}
	}

	private void TrimmingTitle()
	{
		if (base.Title is string text)
		{
			TextBlock textBlock = new TextBlock
			{
				Text = text,
				MaxLines = 2,
				TextWrapping = TextWrapping.Wrap,
				TextTrimming = TextTrimming.CharacterEllipsis
			};
			AddTitleToolTip(textBlock);
			base.Title = textBlock;
		}
	}

	private void AddTitleToolTip(TextBlock textBlock)
	{
		ToolTip value = new ToolTip
		{
			IsTabStop = false,
			VerticalOffset = 10.0,
			Content = textBlock.Text
		};
		ToolTipService.SetToolTip(textBlock, value);
		Interaction.GetBehaviors(textBlock).Add(new TooltipForTrimmedTextBlockBehavior());
	}
}
