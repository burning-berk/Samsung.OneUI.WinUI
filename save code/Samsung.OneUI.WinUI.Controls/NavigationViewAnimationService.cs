using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Samsung.OneUI.WinUI.Utils.Extensions;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.Foundation;

namespace Samsung.OneUI.WinUI.Controls;

internal class NavigationViewAnimationService
{
	private const double DEFAULT_OPACITY_VALUE = 1.0;

	private const double MINIMUM_OPACITY_VALUE = 0.15;

	private const double ANIMATIONS_DEFAULT_DURATION = 400.0;

	private const double X1_INTERPOLATION_POINT = 0.22;

	private const double Y1_INTERPOLATION_POINT = 0.25;

	private const double X2_INTERPOLATION_POINT = 0.0;

	private const double Y2_INTERPOLATION_POINT = 1.0;

	private const string AUTO_SUGGEST_AREA = "PaneAutoSuggestBoxPresenter";

	private const string TOGGLE_PANE_BUTTON = "TogglePaneButton";

	private const string PANE_SEARCH_BUTTON = "PaneAutoSuggestButton";

	private bool _isManipulationStarted;

	private double _defaultRangeBetweenCompactAndOpenPaneLengths;

	private readonly double _flingGestureThresholdToOpenOrCompactPane;

	private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

	private readonly NavigationView _navigationView;

	private NavigationViewItemPresenter _settingsItemPresenter;

	private ContentControl _autoSuggestArea;

	private bool _isToggleButtonClicked;

	public NavigationViewAnimationService(NavigationView navigationView)
	{
		_navigationView = navigationView;
		GetNavigationViewTemplate();
		InitSetup();
		_flingGestureThresholdToOpenOrCompactPane = CalculateFlingThreshold();
	}

	private void GetNavigationViewTemplate()
	{
		if (_navigationView.settingsNavPaneItem != null)
		{
			_settingsItemPresenter = UIExtensionsInternal.FindFirstChildOfType<NavigationViewItemPresenter>(_navigationView.settingsNavPaneItem);
		}
		_autoSuggestArea = UIExtensionsInternal.FindChildByName<ContentControl>("PaneAutoSuggestBoxPresenter", _navigationView);
	}

	private void InitSetup()
	{
		if (!(_navigationView.rootSplitView == null))
		{
			SetDefaultRangeBetweenCompactAndOpenPaneLengths();
			AssignEvents();
		}
	}

	private void SetDefaultRangeBetweenCompactAndOpenPaneLengths()
	{
		_defaultRangeBetweenCompactAndOpenPaneLengths = _navigationView.defaultOpenPaneLength - _navigationView.defaultCompactPaneLength;
	}

	private void AssignEvents()
	{
		AssignToggleButtonEvents();
		AssignSearchButtonEvents();
		AssignRootSplitViewEvents();
	}

	private void AssignSearchButtonEvents()
	{
		Button button = UIExtensionsInternal.FindChildByName<Button>("PaneAutoSuggestButton", _navigationView);
		if (button != null)
		{
			button.Click += SearchButton_Click;
		}
	}

	private void AssignToggleButtonEvents()
	{
		Button button = UIExtensionsInternal.FindChildByName<Button>("TogglePaneButton", _navigationView);
		if (button != null)
		{
			button.Click += ToggleButton_Click;
			button.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(ToggleButton_PointerPressed), handledEventsToo: true);
			button.AddHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(ToggleButton_PointerReleased), handledEventsToo: true);
		}
	}

	private void SetupNavigationViewAnimation(double paneLength, double fromOpacity, double toOpacity)
	{
		StartPathInterpolatorAnimation("OpenPaneLength", paneLength);
		ConfigureOpacityAnimation(fromOpacity, toOpacity);
	}

	private void AssignRootSplitViewEvents()
	{
		_navigationView.rootSplitView.PaneOpening += RootSplitView_PaneOpening;
		_navigationView.rootSplitView.Pane.ManipulationDelta += PaneContentGrid_ManipulationDelta;
		_navigationView.rootSplitView.Pane.ManipulationCompleted += PaneContentGrid_ManipulationCompleted;
	}

	private void StartToCompactPane(double deltaTranslationX)
	{
		deltaTranslationX = Math.Abs(deltaTranslationX);
		if (deltaTranslationX <= CalculateRemainigRangeAvailableToCompactPane())
		{
			_navigationView.OpenPaneLength -= deltaTranslationX;
			SetupOpacityValues(isAnimated: false);
		}
	}

	private void StartToOpenPane(double deltaTranslationX)
	{
		deltaTranslationX = Math.Abs(deltaTranslationX);
		if (IsPaneCompacted())
		{
			SetPaneToBeAbleToOpen();
		}
		if (deltaTranslationX <= CalculateRemainingRangeAvailableToOpenPane() && CanKeepOpening(deltaTranslationX))
		{
			_navigationView.OpenPaneLength += deltaTranslationX;
			SetupOpacityValues(isAnimated: false);
		}
	}

	private void SetPaneToBeAbleToOpen()
	{
		_navigationView.OpenPaneLength = _navigationView.defaultCompactPaneLength;
		_navigationView.IsPaneOpen = true;
	}

	private void SetupOpacityValues(bool isAnimated)
	{
		double num = CalculateOpacity();
		if (isAnimated)
		{
			ConfigureOpacityAnimation(num, 1.0);
			return;
		}
		SetItemPresenterOpacity(_navigationView.MenuItems.OfType<NavigationViewItem>(), num);
		SetItemPresenterOpacity(_navigationView.FooterMenuItems.OfType<NavigationViewItem>(), num);
		NavigationViewItemPresenter navigationViewItemPresenter = UIExtensionsInternal.FindFirstChildOfType<NavigationViewItemPresenter>(_navigationView.settingsNavPaneItem);
		if (navigationViewItemPresenter != null)
		{
			SetOpacity(navigationViewItemPresenter, num);
		}
		_autoSuggestArea.Opacity = num;
	}

	private void SetOpacity(NavigationViewItemPresenter itemPresenter, double opacity)
	{
		if (!(itemPresenter == null))
		{
			itemPresenter.SetOpacity(opacity);
		}
	}

	private ContentPresenter GetItemContentPresenter(NavigationViewItem navigationViewItem)
	{
		ContentPresenter result = null;
		NavigationViewItemPresenter navigationViewItemPresenter = GetNavigationViewItemPresenter(navigationViewItem);
		if (navigationViewItemPresenter != null)
		{
			result = navigationViewItemPresenter.GetContentPresenter();
		}
		return result;
	}

	private Brush GetLayoutRootBackground(NavigationViewItem navigationViewItem)
	{
		return (GetNavigationViewItemPresenter(navigationViewItem)?.GetLayoutRoot())?.Background;
	}

	private NavigationViewItemPresenter GetNavigationViewItemPresenter(NavigationViewItem navigationViewItem)
	{
		return UIExtensionsInternal.FindFirstChildOfType<NavigationViewItemPresenter>(navigationViewItem);
	}

	private SplineDoubleKeyFrame SetupSplineKeyFrameAnimation(double value)
	{
		SplineDoubleKeyFrame splineDoubleKeyFrame = new SplineDoubleKeyFrame();
		if (_navigationView.IsPaneOpen)
		{
			splineDoubleKeyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(CalculateTimeToClosePane()));
		}
		else
		{
			splineDoubleKeyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(CalculateTimeToOpenPane()));
		}
		splineDoubleKeyFrame.KeySpline = SetupPathInterpolator(0.22, 0.25, 0.0, 1.0);
		splineDoubleKeyFrame.Value = value;
		return splineDoubleKeyFrame;
	}

	private KeySpline SetupPathInterpolator(double x1, double y1, double x2, double y2)
	{
		return new KeySpline
		{
			ControlPoint1 = new Point(x1, y1),
			ControlPoint2 = new Point(x2, y2)
		};
	}

	private void StartOpacityAnimation(UIElement target, double fromOpacity, double toOpacity, double duration = 400.0)
	{
		if (!(target == null))
		{
			Storyboard storyboard = new Storyboard();
			DoubleAnimationUsingKeyFrames item = CreateDoubleAnimation(target, fromOpacity, toOpacity, duration);
			storyboard.Children.Add(item);
			storyboard.SafeBegin();
		}
	}

	private DoubleAnimationUsingKeyFrames CreateDoubleAnimation(UIElement target, double fromOpacity, double toOpacity, double duration)
	{
		DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames
		{
			EnableDependentAnimation = true
		};
		if (target is NavigationViewItem navigationViewItem)
		{
			Storyboard.SetTarget(doubleAnimationUsingKeyFrames, GetLayoutRootBackground(navigationViewItem));
		}
		else
		{
			Storyboard.SetTarget(doubleAnimationUsingKeyFrames, target);
		}
		Storyboard.SetTargetProperty(doubleAnimationUsingKeyFrames, "Opacity");
		SplineDoubleKeyFrame splineDoubleKeyFrame = new SplineDoubleKeyFrame();
		splineDoubleKeyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(duration));
		splineDoubleKeyFrame.Value = toOpacity;
		splineDoubleKeyFrame.KeySpline = SetupPathInterpolator(0.22, 0.25, 0.0, 1.0);
		doubleAnimationUsingKeyFrames.KeyFrames.Add(splineDoubleKeyFrame);
		return doubleAnimationUsingKeyFrames;
	}

	private bool IsSwipedToLeft(double swipeDistance)
	{
		return swipeDistance < 0.0;
	}

	private bool IsSwipedToRight(double swipeDistance)
	{
		return swipeDistance > 0.0;
	}

	private bool CanKeepOpening(double deltaTranslationX)
	{
		return _navigationView.OpenPaneLength + deltaTranslationX <= _navigationView.defaultOpenPaneLength;
	}

	private bool CanOpenPane()
	{
		return _navigationView.OpenPaneLength <= _navigationView.defaultOpenPaneLength;
	}

	private bool IsPaneCompacted()
	{
		return !_navigationView.IsPaneOpen;
	}

	private bool IsValidFlingGestureThresholdToOpenPane()
	{
		return _navigationView.OpenPaneLength > _flingGestureThresholdToOpenOrCompactPane;
	}

	private bool IsValidFlingGestureThresholdToCompactPane()
	{
		return _navigationView.OpenPaneLength < _flingGestureThresholdToOpenOrCompactPane;
	}

	private double CalculateFlingThreshold()
	{
		return (_navigationView.defaultOpenPaneLength - _navigationView.defaultCompactPaneLength) / 2.0 + _navigationView.defaultCompactPaneLength;
	}

	private double CalculateRemainigRangeAvailableToCompactPane()
	{
		return _navigationView.OpenPaneLength - _navigationView.defaultCompactPaneLength;
	}

	private double CalculateRemainingRangeAvailableToOpenPane()
	{
		return _navigationView.defaultOpenPaneLength - _navigationView.CompactPaneLength;
	}

	private double CalculateOpacity()
	{
		return (_navigationView.OpenPaneLength - _navigationView.defaultCompactPaneLength) / _defaultRangeBetweenCompactAndOpenPaneLengths;
	}

	private double CalculateTimeToOpenPane()
	{
		double result = 400.0 * (_navigationView.defaultOpenPaneLength - _navigationView.OpenPaneLength) / _defaultRangeBetweenCompactAndOpenPaneLengths;
		if ((_navigationView.defaultOpenPaneLength - _navigationView.OpenPaneLength) / _defaultRangeBetweenCompactAndOpenPaneLengths == 0.0)
		{
			return 400.0;
		}
		return result;
	}

	private double CalculateTimeToClosePane()
	{
		double result = 400.0 * (_navigationView.OpenPaneLength - _navigationView.defaultCompactPaneLength) / _defaultRangeBetweenCompactAndOpenPaneLengths;
		if ((_navigationView.OpenPaneLength - _navigationView.defaultCompactPaneLength) / _defaultRangeBetweenCompactAndOpenPaneLengths == 0.0)
		{
			return 400.0;
		}
		return result;
	}

	private void StartPathInterpolatorAnimation(string property, double value)
	{
		Storyboard storyboard = new Storyboard();
		DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames
		{
			EnableDependentAnimation = true
		};
		Storyboard.SetTarget(doubleAnimationUsingKeyFrames, _navigationView);
		Storyboard.SetTargetProperty(doubleAnimationUsingKeyFrames, property);
		SplineDoubleKeyFrame item = SetupSplineKeyFrameAnimation(value);
		doubleAnimationUsingKeyFrames.KeyFrames.Add(item);
		storyboard.Children.Add(doubleAnimationUsingKeyFrames);
		storyboard.SafeBegin();
	}

	public void ConfigureOpacityAnimation(double fromOpacity, double toOpacity)
	{
		double num = CalculateTimeToOpenPane();
		SetItemPresenterStartOpacityAnimation(_navigationView.MenuItems.OfType<NavigationViewItem>(), fromOpacity, toOpacity, num);
		SetItemPresenterStartOpacityAnimation(_navigationView.FooterMenuItems.OfType<NavigationViewItem>(), fromOpacity, toOpacity, num);
		StartOpacityAnimation(GetItemContentPresenter(_navigationView.settingsNavPaneItem), fromOpacity, toOpacity, num);
		StartOpacityAnimation(_autoSuggestArea, fromOpacity, toOpacity, num);
	}

	public void RunOpenPaneAnimation()
	{
		StartPathInterpolatorAnimation("OpenPaneLength", _navigationView.defaultOpenPaneLength);
		_navigationView.IsPaneOpen = true;
	}

	public void RunCompactPaneAnimation()
	{
		try
		{
			_semaphore.WaitAsync();
			StartPathInterpolatorAnimation("OpenPaneLength", _navigationView.defaultCompactPaneLength);
			_navigationView.IsPaneOpen = false;
		}
		finally
		{
			_semaphore.Release();
		}
	}

	private void ToggleButton_Click(object sender, RoutedEventArgs e)
	{
		if (_navigationView.IsPaneOpen)
		{
			SetupNavigationViewAnimation(_navigationView.defaultOpenPaneLength, 0.15, 1.0);
		}
		else
		{
			SetupNavigationViewAnimation(_navigationView.defaultCompactPaneLength, 1.0, 0.15);
		}
	}

	private void ToggleButton_PointerReleased(object sender, PointerRoutedEventArgs e)
	{
		_isToggleButtonClicked = false;
	}

	private void ToggleButton_PointerPressed(object sender, PointerRoutedEventArgs e)
	{
		_isToggleButtonClicked = true;
	}

	private void SearchButton_Click(object sender, RoutedEventArgs e)
	{
		RunOpenPaneAnimation();
		ConfigureOpacityAnimation(0.15, 1.0);
	}

	private void RootSplitView_PaneOpening(SplitView sender, object args)
	{
		try
		{
			_semaphore.WaitAsync();
			_navigationView.RemoveShadowFromSplitViewPane(sender);
		}
		finally
		{
			_semaphore.Release();
		}
	}

	private void PaneContentGrid_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
	{
		if (!_isToggleButtonClicked)
		{
			_isManipulationStarted = true;
			_navigationView.rootSplitView.IsHitTestVisible = false;
			double x = e.Delta.Translation.X;
			if (IsSwipedToLeft(x))
			{
				StartToCompactPane(x);
			}
			if (IsSwipedToRight(x) && CanOpenPane())
			{
				StartToOpenPane(x);
			}
		}
	}

	private void PaneContentGrid_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
	{
		if (!_isToggleButtonClicked && _isManipulationStarted)
		{
			if (IsValidFlingGestureThresholdToOpenPane())
			{
				RunOpenPaneAnimation();
				SetupOpacityValues(isAnimated: true);
			}
			else if (IsValidFlingGestureThresholdToCompactPane())
			{
				RunCompactPaneAnimation();
				_autoSuggestArea.Opacity = 1.0;
			}
			_isManipulationStarted = false;
			_navigationView.rootSplitView.IsHitTestVisible = true;
		}
	}

	private void SetItemPresenterStartOpacityAnimation(IEnumerable<NavigationViewItem> enumerable, double fromOpacity, double toOpacity, double timeLeftToOpenPane)
	{
		foreach (NavigationViewItem item in enumerable)
		{
			ContentPresenter itemContentPresenter = GetItemContentPresenter(item);
			if (itemContentPresenter != null)
			{
				StartOpacityAnimation(itemContentPresenter, fromOpacity, toOpacity, timeLeftToOpenPane);
			}
		}
	}

	private void SetItemPresenterOpacity(IEnumerable<NavigationViewItem> enumerable, double opacity)
	{
		foreach (NavigationViewItem item in enumerable)
		{
			NavigationViewItemPresenter navigationViewItemPresenter = UIExtensionsInternal.FindFirstChildOfType<NavigationViewItemPresenter>(item);
			if (navigationViewItemPresenter != null)
			{
				SetOpacity(navigationViewItemPresenter, opacity);
			}
		}
	}
}
