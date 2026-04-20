using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Windows.System;

namespace Samsung.OneUI.WinUI.Controls;

public class FlipView : Microsoft.UI.Xaml.Controls.FlipView
{
	private Button _buttonUp;

	private Button _buttonDown;

	private Button _buttonLeft;

	private Button _buttonRight;

	private ScrollViewer _scrollingHostScrollViewer;

	public static readonly DependencyProperty FlipViewModeProperty = DependencyProperty.Register("IsClickable", typeof(bool), typeof(FlipView), new PropertyMetadata(true, IsClickableChanged));

	public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(FlipView), new PropertyMetadata(Orientation.Horizontal, OnOrientationPropertyChanged));

	public static readonly DependencyProperty PreviousButtonHorizontalStyleProperty = DependencyProperty.Register("PreviousButtonHorizontalStyle", typeof(Style), typeof(FlipView), new PropertyMetadata(null));

	public static readonly DependencyProperty NextButtonHorizontalStyleProperty = DependencyProperty.Register("NextButtonHorizontalStyle", typeof(Style), typeof(FlipView), new PropertyMetadata(null));

	public static readonly DependencyProperty PreviousButtonVerticalStyleProperty = DependencyProperty.Register("PreviousButtonVerticalStyle", typeof(Style), typeof(FlipView), new PropertyMetadata(null));

	public static readonly DependencyProperty NextButtonVerticalStyleProperty = DependencyProperty.Register("NextButtonVerticalStyle", typeof(Style), typeof(FlipView), new PropertyMetadata(null));

	public static readonly DependencyProperty IsBlurButtonProperty = DependencyProperty.Register("IsBlurButton", typeof(bool), typeof(FlipView), new PropertyMetadata(false));

	public bool IsClickable
	{
		get
		{
			return (bool)GetValue(FlipViewModeProperty);
		}
		set
		{
			SetValue(FlipViewModeProperty, value);
		}
	}

	public Orientation Orientation
	{
		get
		{
			return (Orientation)GetValue(OrientationProperty);
		}
		set
		{
			SetValue(OrientationProperty, value);
		}
	}

	public Style PreviousButtonHorizontalStyle
	{
		get
		{
			return (Style)GetValue(PreviousButtonHorizontalStyleProperty);
		}
		set
		{
			SetValue(PreviousButtonHorizontalStyleProperty, value);
		}
	}

	public Style NextButtonHorizontalStyle
	{
		get
		{
			return (Style)GetValue(NextButtonHorizontalStyleProperty);
		}
		set
		{
			SetValue(NextButtonHorizontalStyleProperty, value);
		}
	}

	public Style PreviousButtonVerticalStyle
	{
		get
		{
			return (Style)GetValue(PreviousButtonVerticalStyleProperty);
		}
		set
		{
			SetValue(PreviousButtonVerticalStyleProperty, value);
		}
	}

	public Style NextButtonVerticalStyle
	{
		get
		{
			return (Style)GetValue(NextButtonVerticalStyleProperty);
		}
		set
		{
			SetValue(NextButtonVerticalStyleProperty, value);
		}
	}

	public bool IsBlurButton
	{
		get
		{
			return (bool)GetValue(IsBlurButtonProperty);
		}
		set
		{
			SetValue(IsBlurButtonProperty, value);
		}
	}

	public FlipView()
	{
		base.DefaultStyleKey = typeof(Microsoft.UI.Xaml.Controls.FlipView);
	}

	private void MakeFlipViewButtonsVisible()
	{
		if (Orientation == Orientation.Horizontal && _buttonLeft != null && _buttonRight != null)
		{
			MakeHorizontalFlipViewButtonsVisible();
		}
		else if (Orientation == Orientation.Vertical && _buttonUp != null && _buttonDown != null)
		{
			MakeVerticalFlipViewButtonsVisible();
		}
	}

	private void MakeVerticalFlipViewButtonsVisible()
	{
		if (base.SelectedIndex != 0)
		{
			_buttonUp.Visibility = Visibility.Visible;
		}
		if (base.SelectedIndex != base.Items.Count - 1)
		{
			_buttonDown.Visibility = Visibility.Visible;
		}
	}

	private void MakeHorizontalFlipViewButtonsVisible()
	{
		if (base.SelectedIndex != 0)
		{
			_buttonLeft.Visibility = Visibility.Visible;
		}
		if (base.SelectedIndex != base.Items.Count - 1)
		{
			_buttonRight.Visibility = Visibility.Visible;
		}
	}

	protected override DependencyObject GetContainerForItemOverride()
	{
		return new FlipViewItem(Orientation);
	}

	private static void OnOrientationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is FlipView flipView)
		{
			flipView.UpdateStackPanelOrientationAsync();
		}
	}

	private async void UpdateStackPanelOrientationAsync()
	{
		await base.DispatcherQueue.EnqueueAsync(delegate
		{
			if (base.ItemsPanelRoot is VirtualizingStackPanel virtualizingStackPanel)
			{
				virtualizingStackPanel.Orientation = Orientation;
			}
		});
	}

	private static void IsClickableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is FlipView flipView)
		{
			flipView.UpdateButtonStates();
		}
	}

	protected override void OnApplyTemplate()
	{
		_buttonUp = (Button)GetTemplateChild("PreviousButtonVertical");
		_buttonDown = (Button)GetTemplateChild("NextButtonVertical");
		_buttonLeft = (Button)GetTemplateChild("PreviousButtonHorizontal");
		_buttonRight = (Button)GetTemplateChild("NextButtonHorizontal");
		_scrollingHostScrollViewer = (ScrollViewer)GetTemplateChild("ScrollingHost");
		AssignLoadEventToCaptureItemsPanel();
		UpdateButtonStates();
		base.OnApplyTemplate();
	}

	private void AssignLoadEventToCaptureItemsPanel()
	{
		base.Loaded += FlipView_Loaded;
	}

	private void FlipView_Loaded(object sender, RoutedEventArgs e)
	{
		UpdateStackPanelOrientationAsync();
		base.SelectionChanged += FlipView_SelectionChanged;
		if (base.XamlRoot != null)
		{
			base.XamlRoot.Content.KeyUp += Content_KeyUp;
		}
	}

	private void Content_KeyUp(object sender, KeyRoutedEventArgs args)
	{
		if (args.Key == VirtualKey.Tab)
		{
			MakeFlipViewButtonsVisible();
		}
	}

	private void FlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (Orientation == Orientation.Horizontal)
		{
			ChangingButtonVisibility(_buttonLeft, _buttonRight);
		}
		else
		{
			ChangingButtonVisibility(_buttonUp, _buttonDown);
		}
	}

	private void ChangingButtonVisibility(Button prevButton, Button nextButton)
	{
		prevButton.Visibility = ((base.SelectedIndex == 0) ? Visibility.Collapsed : Visibility.Visible);
		nextButton.Visibility = ((base.SelectedIndex == base.Items.Count - 1) ? Visibility.Collapsed : Visibility.Visible);
	}

	protected override void OnPointerWheelChanged(PointerRoutedEventArgs e)
	{
		if (IsClickable)
		{
			base.OnPointerWheelChanged(e);
		}
	}

	private void UpdateButtonStates()
	{
		if (_buttonUp != null)
		{
			Button[] obj = new Button[4] { _buttonUp, _buttonDown, _buttonLeft, _buttonRight };
			bool isClickable = IsClickable;
			Button[] array = obj;
			foreach (Button obj2 in array)
			{
				VisualStateManager.GoToState(obj2, isClickable ? "Normal" : "Disabled", useTransitions: false);
				obj2.IsEnabled = isClickable;
			}
			_scrollingHostScrollViewer.IsEnabled = isClickable;
		}
	}
}
