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
using Microsoft.UI.Xaml.Media.Animation;
using Windows.Foundation;
using WinRT;
using WinRT.Samsung_OneUI_WinUIVtableClasses;

namespace Samsung.OneUI.WinUI.Controls;

[Obsolete("Toast is deprecated, please use SnackBar instead.")]
[WinRTRuntimeClassName("Microsoft.UI.Xaml.IUIElementOverrides")]
[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_DialogsAndFlyouts_SnackBar_SnackBarWinRTTypeDetails))]
public sealed class Toast : ContentControl, IComponentConnector
{
	private const int MIN_HEIGHT = 20;

	private const int MAX_WIDTH = 500;

	private const int MAX_WIDTH_THRESHOLD = 487;

	private const string SHORT_TOAST_STORYBOARD_NAME = "ShortToastStoryboard";

	private const string LONG_TOAST_STORYBOARD_NAME = "LongToastStoryboard";

	private const string NARRATOR_ACTIVITY_ID = "Toast Notification";

	private const float UNTARGETED_TIP_WINDOW_EDGE_MARGIN = 24f;

	private const float MARGIN_TOP_DEFAULT = 120f;

	private Storyboard _shortToastStoryboard;

	private Storyboard _longToastStoryboard;

	private string _toastMessage;

	private double _offSetAppTitleBar;

	private double _verticalOffSet;

	private FrameworkElement _toastTarget;

	private readonly Queue<ToastMessage> _queuedMessages;

	private readonly FrameworkElementAutomationPeer _automationPeer;

	private static readonly Rect _offsetTargeted = new Rect(6f, 6f, 6f, 6f);

	private static readonly Rect _offsetUntargeted = new Rect(0f, 0f, 0f, 0f);

	private readonly IDictionary<Tuple<double, double>, double> _toastWidthByBreakpointResolutions = new Dictionary<Tuple<double, double>, double>
	{
		{
			new Tuple<double, double>(0.0, 640.0),
			410.0
		},
		{
			new Tuple<double, double>(641.0, 1392.0),
			540.0
		},
		{
			new Tuple<double, double>(1393.0, 2207.0),
			540.0
		},
		{
			new Tuple<double, double>(2208.0, 2592.0),
			540.0
		},
		{
			new Tuple<double, double>(2592.0, 2147483647.0),
			540.0
		}
	};

	public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(Toast), new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty ToastDurationProperty = DependencyProperty.Register("ToastDuration", typeof(ToastDuration), typeof(Toast), new PropertyMetadata(ToastDuration.Short));

	public static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(FrameworkElement), typeof(Toast), new PropertyMetadata(null));

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private Popup ToastPopup;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private Border ToastBorder;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private TextBlock ToastText;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private Border InnerStroke;

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

	public ToastDuration ToastDuration
	{
		get
		{
			return (ToastDuration)GetValue(ToastDurationProperty);
		}
		set
		{
			SetValue(ToastDurationProperty, value);
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

	public event EventHandler<NullObject<FrameworkElement>> CompletedEvent;

	public Toast()
	{
		InitializeComponent();
		_queuedMessages = new Queue<ToastMessage>();
		AddEvents();
		_toastMessage = null;
		_toastTarget = null;
		_automationPeer = new FrameworkElementAutomationPeer(this);
		base.ActualThemeChanged += Toast_ActualThemeChanged;
	}

	private void Toast_ActualThemeChanged(FrameworkElement sender, object args)
	{
		ToastPopup.RequestedTheme = ToastPopup.RequestedTheme;
	}

	private void AnimationCompleted(object sender, object e)
	{
		lock (_queuedMessages)
		{
			if (_queuedMessages.Any())
			{
				ToastMessage toastMessage = _queuedMessages.Dequeue();
				DoShowMessage(toastMessage.Message, toastMessage.Duration, toastMessage.Target);
				return;
			}
			ToastPopup.IsOpen = false;
			this.CompletedEvent?.Invoke(this, _toastTarget);
			_toastMessage = null;
			_toastTarget = null;
			UnsubscribeWindowEvent();
		}
	}

	private void ToastBorder_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		if (_toastTarget == null)
		{
			PositionUntargetedPopup();
		}
		else
		{
			PositionTargetedPopup();
		}
	}

	private void ToastText_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		if (sender is TextBlock { ActualWidth: >487.0 } textBlock)
		{
			textBlock.Width = 500.0;
		}
	}

	private void CurrentWindow_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		PositionUntargetedPopup();
	}

	public void AdjustSizeAndTrimming(double textWidth)
	{
		double num = GetWidthByResolution();
		if (textWidth > 0.0)
		{
			num = ((textWidth < num) ? textWidth : num);
		}
		base.MaxWidth = num;
		ToastText.MaxWidth = num;
	}

	public void Show()
	{
		Show(Message, ToastDuration, Target);
	}

	internal void Show(string message, ToastDuration duration, FrameworkElement target = null)
	{
		lock (_queuedMessages)
		{
			if (IsShowingToast())
			{
				if (_toastMessage != null && !_toastMessage.Equals(message))
				{
					_queuedMessages.Enqueue(new ToastMessage
					{
						Message = message,
						Duration = duration,
						Target = target
					});
				}
			}
			else
			{
				DoShowMessage(message, duration, target);
			}
		}
	}

	private void DoShowMessage(string message, ToastDuration duration, FrameworkElement target = null)
	{
		_toastMessage = message;
		_toastTarget = target;
		ToastText.Text = _toastMessage;
		ToastPopup.XamlRoot = base.XamlRoot;
		ToastPopup.IsOpen = true;
		SubscribeWindowEvent();
		if (duration == ToastDuration.Short)
		{
			_shortToastStoryboard.Begin();
		}
		else
		{
			_longToastStoryboard.Begin();
		}
		_automationPeer.RaiseNotificationEvent(AutomationNotificationKind.ActionAborted, AutomationNotificationProcessing.ImportantMostRecent, ToastText.Text ?? "", "Toast Notification");
	}

	public void UpdateOffSetAppTitleBar(double verticalAppTitleBarTopOffset = 0.0)
	{
		_offSetAppTitleBar = verticalAppTitleBarTopOffset;
	}

	public void UpdateVerticalOffSet(double verticalOffset = 0.0)
	{
		_verticalOffSet = verticalOffset;
	}

	private bool IsShowingToast()
	{
		return ToastPopup.IsOpen;
	}

	private void AddEvents()
	{
		StoryboardLoad();
		if (ToastBorder != null)
		{
			ToastBorder.SizeChanged += ToastBorder_SizeChanged;
		}
	}

	private void StoryboardLoad()
	{
		if (ToastBorder != null)
		{
			if (ToastBorder.Resources.ContainsKey("ShortToastStoryboard"))
			{
				_shortToastStoryboard = (Storyboard)ToastBorder.Resources["ShortToastStoryboard"];
				_shortToastStoryboard.Completed += AnimationCompleted;
			}
			if (ToastBorder.Resources.ContainsKey("LongToastStoryboard"))
			{
				_longToastStoryboard = (Storyboard)ToastBorder.Resources["LongToastStoryboard"];
				_longToastStoryboard.Completed += AnimationCompleted;
			}
		}
	}

	private void PositionTargetedPopup()
	{
		Rect rect = _toastTarget.TransformToVisual(null).TransformBounds(new Rect(0.0, 0.0, _toastTarget.ActualWidth, _toastTarget.ActualHeight));
		ToastPopup.VerticalOffset = rect.Y + rect.Height + _offsetTargeted.Bottom;
		ToastPopup.HorizontalOffset = (rect.X * 2.0 + rect.Width - ToastBorder.ActualWidth) / 2.0;
	}

	private void PositionUntargetedPopup()
	{
		if (ToastPopup.XamlRoot != null)
		{
			Size size = ToastPopup.XamlRoot.Size;
			Rect rect = new Rect(0.0, 0.0, size.Width, size.Height);
			ToastPopup.VerticalOffset = UntargetedTipFarPlacementOffset(ToastBorder, _offSetAppTitleBar, _verticalOffSet);
			ToastPopup.HorizontalOffset = UntargetedTipCenterPlacementOffset(rect.X, rect.Width, ToastBorder.ActualWidth, _offsetUntargeted.Left, _offsetUntargeted.Right);
		}
	}

	private double UntargetedTipFarPlacementOffset(Border toastBorder, double offSetAppTitleBar, double verticalOffset)
	{
		if (base.XamlRoot == null)
		{
			return 0.0;
		}
		Size size = base.XamlRoot.Size;
		double num = offSetAppTitleBar + 120.0;
		double num2 = size.Height - (num + 24.0 + ToastBorder.BorderThickness.Bottom);
		num2 = ((num2 < 0.0) ? 0.0 : num2);
		if (num2 <= toastBorder.ActualHeight)
		{
			double num3 = 24.0 + ToastBorder.BorderThickness.Bottom;
			double num4 = size.Height - (num3 + num);
			ToastBorder.Height = ((num4 < 0.0) ? 0.0 : num4);
			return num;
		}
		if (!double.IsNaN(verticalOffset) && verticalOffset != 0.0)
		{
			verticalOffset = ((verticalOffset <= num) ? num : verticalOffset);
			return verticalOffset;
		}
		return size.Height - (toastBorder.ActualHeight + 24.0 + ToastBorder.BorderThickness.Bottom);
	}

	private static double UntargetedTipCenterPlacementOffset(double nearWindowCoordinateInCoreWindowSpace, double farWindowCoordinateInCoreWindowSpace, double tipSize, double nearOffset, double farOffset)
	{
		return (nearWindowCoordinateInCoreWindowSpace + farWindowCoordinateInCoreWindowSpace) / 2.0 - tipSize / 2.0 + nearOffset - farOffset;
	}

	private double GetWidthByResolution()
	{
		double widthResolution = base.XamlRoot?.Size.Width ?? 0.0;
		Tuple<double, double> key = _toastWidthByBreakpointResolutions.Keys.FirstOrDefault((Tuple<double, double> tuple) => widthResolution >= tuple.Item1 && widthResolution <= tuple.Item2);
		_toastWidthByBreakpointResolutions.TryGetValue(key, out var value);
		return value;
	}

	private void SubscribeWindowEvent()
	{
		if (ToastPopup.XamlRoot?.Content is FrameworkElement frameworkElement)
		{
			frameworkElement.SizeChanged += CurrentWindow_SizeChanged;
		}
	}

	private void UnsubscribeWindowEvent()
	{
		if (ToastPopup.XamlRoot?.Content is FrameworkElement frameworkElement)
		{
			frameworkElement.SizeChanged -= CurrentWindow_SizeChanged;
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("ms-appx:///Samsung.OneUI.WinUI/Controls/Deprecated/ToastControl/Toast.xaml");
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
			ToastPopup = target.As<Popup>();
			break;
		case 3:
			ToastBorder = target.As<Border>();
			break;
		case 4:
			ToastText = target.As<TextBlock>();
			ToastText.SizeChanged += ToastText_SizeChanged;
			break;
		case 5:
			InnerStroke = target.As<Border>();
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
