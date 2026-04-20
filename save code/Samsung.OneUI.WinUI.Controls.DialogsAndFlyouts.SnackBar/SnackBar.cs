using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Markup;
using Windows.Foundation;
using WinRT;
using WinRT.Samsung_OneUI_WinUIVtableClasses;

namespace Samsung.OneUI.WinUI.Controls.DialogsAndFlyouts.SnackBar;

[WinRTRuntimeClassName("Microsoft.UI.Xaml.IUIElementOverrides")]
[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_DialogsAndFlyouts_SnackBar_SnackBarWinRTTypeDetails))]
public sealed class SnackBar : ContentControl, IComponentConnector
{
	private const int SNACKBAR_TOTAL_HORIZONTAL_PADDING = 36;

	private const int MAX_BUTTON_TEXT_LENGTH = 16;

	private const int MAX_TEXT_LENGTH = 115;

	private const string NARRATOR_ACTIVITY_ID = "SnackBar Notification";

	private const string TEXT_TRIMMED = "...";

	private const float UNTARGETED_TIP_WINDOW_EDGE_MARGIN = 24f;

	private const float MARGIN_TOP_DEFAULT = 120f;

	private const int LARGE_SCREEN_BREAKPOINT = 641;

	private const int MEDIUM_SCREEN_BREAKPOINT = 450;

	private const int LARGE_SCREEN_SNACKBAR_WIDTH = 540;

	private const int MEDIUM_SCREEN_SNACKBAR_WIDTH = 410;

	private const double SMALL_SCREEN_SNACKBAR_WIDTH = 0.84;

	private string _snackBarMessage;

	private double _offSetAppTitleBar;

	private double _verticalOffSet;

	private FrameworkElement _snackBarTarget;

	private readonly SnackBarAnimationService _snackBarAnimationService;

	private readonly Queue<SnackBarMessage> _queuedMessages;

	private readonly FrameworkElementAutomationPeer _automationPeer;

	private static readonly Rect _offsetTargeted = new Rect(6f, 6f, 6f, 6f);

	private static readonly Rect _offsetUntargeted = new Rect(0f, 0f, 0f, 0f);

	public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(SnackBar), new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty SnackBarDurationProperty = DependencyProperty.Register("SnackBarDuration", typeof(SnackBarDuration), typeof(SnackBar), new PropertyMetadata(SnackBarDuration.Short));

	public static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(FrameworkElement), typeof(SnackBar), new PropertyMetadata(null));

	public static readonly DependencyProperty IsShowButtonProperty = DependencyProperty.Register("IsShowButton", typeof(bool), typeof(SnackBar), new PropertyMetadata(false));

	public static readonly DependencyProperty ButtonTextProperty = DependencyProperty.Register("ButtonText", typeof(string), typeof(SnackBar), new PropertyMetadata("Button"));

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private Popup SnackBarPopup;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private Border SnackBarBorder;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private StackPanel SnackBarContainer;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private Border InnerStroke;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private TextBlock SnackBarText;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private SnackBarButton SnackBarButton;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private bool _contentLoaded;

	public string Message
	{
		get
		{
			return (string)GetValue(MessageProperty);
		}
		set
		{
			SetValue(MessageProperty, value);
		}
	}

	public SnackBarDuration SnackBarDuration
	{
		get
		{
			return (SnackBarDuration)GetValue(SnackBarDurationProperty);
		}
		set
		{
			SetValue(SnackBarDurationProperty, value);
		}
	}

	public FrameworkElement Target
	{
		get
		{
			return (FrameworkElement)GetValue(TargetProperty);
		}
		set
		{
			SetValue(TargetProperty, value);
		}
	}

	public bool IsShowButton
	{
		get
		{
			return (bool)GetValue(IsShowButtonProperty);
		}
		set
		{
			SetValue(IsShowButtonProperty, value);
		}
	}

	public string ButtonText
	{
		get
		{
			return (string)GetValue(ButtonTextProperty);
		}
		set
		{
			SetValue(ButtonTextProperty, value);
		}
	}

	public event EventHandler<NullObject<FrameworkElement>> CompletedEvent;

	public event EventHandler<RoutedEventArgs> Click;

	public SnackBar()
	{
		InitializeComponent();
		_queuedMessages = new Queue<SnackBarMessage>();
		AddEvents();
		SubscribeButtonEvents();
		_snackBarMessage = null;
		_snackBarTarget = null;
		_automationPeer = new FrameworkElementAutomationPeer(this);
		_snackBarAnimationService = new SnackBarAnimationService(SnackBarBorder, SnackBarContainer, SnackBarText, SnackBarButton);
	}

	private void AnimationCompleted()
	{
		lock (_queuedMessages)
		{
			if (_queuedMessages.Any())
			{
				SnackBarMessage snackBarMessage = _queuedMessages.Dequeue();
				SnackBarBorder.Height = double.NaN;
				SnackBarBorder.Width = double.NaN;
				SnackBarContainer.Orientation = Orientation.Horizontal;
				SnackBarButton.Margin = new Thickness(-8.0, 0.0, 8.0, 0.0);
				SnackBarText.Margin = new Thickness(18.0, 12.0, 18.0, 12.0);
				DoShowMessage(snackBarMessage.Message, snackBarMessage.ButtonText, snackBarMessage.IsShowButton, snackBarMessage.Duration, snackBarMessage.Target);
			}
			else
			{
				SnackBarPopup.IsOpen = false;
				this.CompletedEvent?.Invoke(this, _snackBarTarget);
				_snackBarMessage = null;
				_snackBarTarget = null;
				UnsubscribeWindowEvent();
			}
		}
	}

	private void SnackBarBorder_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		if (_snackBarTarget == null)
		{
			PositionUntargetedPopup();
		}
		else
		{
			PositionTargetedPopup();
		}
	}

	private void SnackBarText_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		if (sender is TextBlock textBlock)
		{
			GetWidthByResolution();
			double num = base.MaxWidth - SnackBarButton.ActualWidth - 36.0;
			if (textBlock.Text.Length > 115)
			{
				SnackBarText.Text = SnackBarText.Text.Substring(0, 115) + "...";
			}
			if (textBlock.ActualWidth > num)
			{
				SnackBarContainer.Orientation = Orientation.Vertical;
				SnackBarBorder.Width = base.MaxWidth;
				SnackBarButton.Margin = new Thickness(-8.0, 8.0, 10.0, 12.0);
				SnackBarText.Margin = ((SnackBarButton.Visibility == Visibility.Visible) ? new Thickness(18.0, 12.0, 18.0, 0.0) : new Thickness(18.0, 12.0, 18.0, 12.0));
			}
		}
	}

	private void CurrentWindow_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		PositionUntargetedPopup();
	}

	public void AdjustSizeAndTrimming(double textWidth)
	{
		double maxWidth = (SnackBarBorder.MaxWidth = ((textWidth > 0.0) ? Math.Min(textWidth, GetWidthByResolution()) : GetWidthByResolution()));
		base.MaxWidth = maxWidth;
	}

	internal void Show(string message, string buttonText, bool isShowButton, SnackBarDuration duration, FrameworkElement target = null)
	{
		lock (_queuedMessages)
		{
			if (IsShowingSnackBar())
			{
				if (_snackBarMessage != null && !_snackBarMessage.Equals(message))
				{
					_queuedMessages.Enqueue(new SnackBarMessage
					{
						Message = message,
						ButtonText = buttonText,
						IsShowButton = isShowButton,
						Duration = duration,
						Target = target
					});
				}
			}
			else
			{
				DoShowMessage(message, buttonText, isShowButton, duration, target);
			}
		}
	}

	private void DoShowMessage(string message, string buttonText, bool isShowButton, SnackBarDuration duration, FrameworkElement target = null)
	{
		_snackBarMessage = message;
		_snackBarTarget = target;
		SnackBarText.Text = _snackBarMessage;
		SnackBarText.HorizontalTextAlignment = (isShowButton ? TextAlignment.Left : TextAlignment.Center);
		SnackBarButton.Content = ((buttonText.Length > 16) ? (buttonText.Substring(0, 16) + "...") : buttonText);
		SnackBarButton.Visibility = ((!isShowButton) ? Visibility.Collapsed : Visibility.Visible);
		SnackBarPopup.XamlRoot = base.XamlRoot;
		SnackBarPopup.RequestedTheme = GetCurrentTheme();
		SnackBarPopup.IsOpen = true;
		SubscribeWindowEvent();
		SnackBarPopup.UpdateLayout();
		CreateAnimation(target, duration);
		_automationPeer.RaiseNotificationEvent(AutomationNotificationKind.ActionAborted, AutomationNotificationProcessing.ImportantMostRecent, SnackBarText.Text ?? "", "SnackBar Notification");
	}

	public void UpdateOffSetAppTitleBar(double verticalAppTitleBarTopOffset = 0.0)
	{
		_offSetAppTitleBar = verticalAppTitleBarTopOffset;
	}

	public void UpdateVerticalOffSet(double verticalOffset = 0.0)
	{
		_verticalOffSet = verticalOffset;
	}

	private bool IsShowingSnackBar()
	{
		return SnackBarPopup.IsOpen;
	}

	private void AddEvents()
	{
		if (SnackBarBorder != null)
		{
			SnackBarBorder.SizeChanged += SnackBarBorder_SizeChanged;
		}
	}

	private void PositionTargetedPopup()
	{
		Rect rect = _snackBarTarget.TransformToVisual(null).TransformBounds(new Rect(0.0, 0.0, _snackBarTarget.ActualWidth, _snackBarTarget.ActualHeight));
		SnackBarPopup.HorizontalOffset = (rect.X * 2.0 + rect.Width - SnackBarBorder.ActualWidth) / 2.0;
	}

	private void PositionUntargetedPopup()
	{
		if (SnackBarPopup.XamlRoot != null)
		{
			Size size = SnackBarPopup.XamlRoot.Size;
			Rect rect = new Rect(0.0, 0.0, size.Width, size.Height);
			SnackBarPopup.HorizontalOffset = UntargetedTipCenterPlacementOffset(rect.X, rect.Width, SnackBarBorder.ActualWidth, _offsetUntargeted.Left, _offsetUntargeted.Right);
		}
	}

	private double UntargetedTipFarPlacementOffset(Border snackBarBorder, double offSetAppTitleBar, double verticalOffset)
	{
		if (base.XamlRoot == null)
		{
			return 0.0;
		}
		Size size = base.XamlRoot.Size;
		double num = offSetAppTitleBar + 120.0;
		double num2 = size.Height - (num + 24.0 + SnackBarBorder.BorderThickness.Bottom);
		num2 = ((num2 < 0.0) ? 0.0 : num2);
		if (num2 <= snackBarBorder.ActualHeight)
		{
			double num3 = 24.0 + SnackBarBorder.BorderThickness.Bottom;
			double num4 = size.Height - (num3 + num);
			SnackBarBorder.Height = ((num4 < 0.0) ? 0.0 : num4);
			return num;
		}
		if (!double.IsNaN(verticalOffset) && verticalOffset != 0.0)
		{
			verticalOffset = ((verticalOffset <= num) ? num : verticalOffset);
			return verticalOffset;
		}
		return size.Height - (snackBarBorder.ActualHeight + 24.0 + SnackBarBorder.BorderThickness.Bottom);
	}

	private static double UntargetedTipCenterPlacementOffset(double nearWindowCoordinateInCoreWindowSpace, double farWindowCoordinateInCoreWindowSpace, double tipSize, double nearOffset, double farOffset)
	{
		return (nearWindowCoordinateInCoreWindowSpace + farWindowCoordinateInCoreWindowSpace) / 2.0 - tipSize / 2.0 + nearOffset - farOffset;
	}

	private static double TargetedTipCenterPlacementOffset(Rect currentTargetBoundsInCoreWindowSpace)
	{
		return currentTargetBoundsInCoreWindowSpace.Y + currentTargetBoundsInCoreWindowSpace.Height + _offsetTargeted.Bottom;
	}

	private double GetWidthByResolution()
	{
		double num = base.XamlRoot?.Size.Width ?? 0.0;
		if (num >= 641.0)
		{
			return 540.0;
		}
		if (num >= 450.0)
		{
			return 410.0;
		}
		return num * 0.84;
	}

	private void SubscribeWindowEvent()
	{
		if (SnackBarPopup.XamlRoot?.Content is FrameworkElement frameworkElement)
		{
			frameworkElement.SizeChanged += CurrentWindow_SizeChanged;
		}
	}

	private void UnsubscribeWindowEvent()
	{
		if (SnackBarPopup.XamlRoot?.Content is FrameworkElement frameworkElement)
		{
			frameworkElement.SizeChanged -= CurrentWindow_SizeChanged;
		}
	}

	private void SubscribeButtonEvents()
	{
		SnackBarButton.Click += delegate(object s, RoutedEventArgs e)
		{
			this.Click?.Invoke(this, e);
		};
	}

	private ElementTheme GetCurrentTheme()
	{
		if (base.XamlRoot.Content is FrameworkElement frameworkElement)
		{
			return frameworkElement.RequestedTheme;
		}
		return ElementTheme.Default;
	}

	private void CreateAnimation(FrameworkElement target, SnackBarDuration duration)
	{
		double verticalOffsetY;
		if (target != null)
		{
			Rect rect = new Rect(0.0, 0.0, _snackBarTarget.ActualWidth, _snackBarTarget.ActualHeight);
			verticalOffsetY = TargetedTipCenterPlacementOffset(_snackBarTarget.TransformToVisual(null).TransformBounds(rect));
		}
		else
		{
			verticalOffsetY = UntargetedTipFarPlacementOffset(SnackBarBorder, _offSetAppTitleBar, _verticalOffSet);
		}
		_snackBarAnimationService.CreateAnimation(target, duration, verticalOffsetY, AnimationCompleted);
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("ms-appx:///Samsung.OneUI.WinUI/Controls/DialogsAndFlyouts/SnackBar/SnackBar.xaml");
			Application.LoadComponent(this, resourceLocator, ComponentResourceLocation.Nested);
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 2:
			SnackBarPopup = target.As<Popup>();
			break;
		case 3:
			SnackBarBorder = target.As<Border>();
			break;
		case 4:
			SnackBarContainer = target.As<StackPanel>();
			break;
		case 5:
			InnerStroke = target.As<Border>();
			break;
		case 6:
			SnackBarText = target.As<TextBlock>();
			SnackBarText.SizeChanged += SnackBarText_SizeChanged;
			break;
		case 7:
			SnackBarButton = target.As<SnackBarButton>();
			break;
		}
		_contentLoaded = true;
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public IComponentConnector GetBindingConnector(int connectionId, object target)
	{
		return null;
	}
}
