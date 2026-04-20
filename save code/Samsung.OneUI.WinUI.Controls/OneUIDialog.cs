using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Shapes;
using Samsung.OneUI.WinUI.AttachedProperties;
using Samsung.OneUI.WinUI.Controls.EventHandlers;
using Samsung.OneUI.WinUI.Utils.Extensions;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.Foundation;
using Windows.System;
using WinRT;
using WinRT.Samsung_OneUI_WinUIVtableClasses;

namespace Samsung.OneUI.WinUI.Controls;

[WinRTRuntimeClassName("Microsoft.UI.Xaml.IUIElementOverrides")]
[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_DatePickerSpinnerListWinRTTypeDetails))]
public class OneUIDialog : Control
{
	internal readonly ContentDialog dialog;

	protected Style defaultStyle;

	private ContentDialogResult _contentDialogResult;

	private bool _isAction3ButtonClicked;

	private double _initialWidth;

	private int _timerRepetition;

	private Timer _layoutChangeTimer;

	private FlatButton _action1Button;

	private FlatButton _action2Button;

	private FlatButton _action3Button;

	private Rectangle _divider1;

	private Rectangle _divider2;

	private StackPanel _buttonContainer;

	private Grid _commandSpace;

	private bool _isButtonHorizontal;

	private int _buttonVisibleCount;

	private int _checkedTextBlockCount;

	private double _widthForOneButtonVisible;

	private double _widthForTwoButtonVisible;

	private double _widthForThreeButtonVisible;

	private static Thickness DEFAULT_CONTENT_MARGIN = new Thickness(24.0, 0.0, 0.0, 0.0);

	private static readonly OneUIContentDialogAsyncLock dialogLock = new OneUIContentDialogAsyncLock();

	public static readonly DependencyProperty DialogStyleProperty = DependencyProperty.Register("Style", typeof(Style), typeof(OneUIDialog), new PropertyMetadata(null, OnStylePropertyChanged));

	public new static readonly DependencyProperty FlowDirectionProperty = DependencyProperty.RegisterAttached("FlowDirection", typeof(FlowDirection), typeof(OneUIDialog), new PropertyMetadata(null, OnFlowDirectionPropertyChanged));

	public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(object), typeof(OneUIDialog), new PropertyMetadata(null, OnTitlePropertyChanged));

	public static readonly DependencyProperty TitleTemplateProperty = DependencyProperty.Register("TitleTemplate", typeof(DataTemplate), typeof(OneUIDialog), new PropertyMetadata(null, OnTitleTemplatePropertyChanged));

	public static readonly DependencyProperty Action1ButtonTextProperty = DependencyProperty.Register("Action1ButtonText", typeof(string), typeof(OneUIDialog), new PropertyMetadata(string.Empty, OnAction1ButtonTextPropertyChanged));

	public static readonly DependencyProperty Action1ButtonStyleProperty = DependencyProperty.Register("Action1ButtonStyle", typeof(Style), typeof(OneUIDialog), new PropertyMetadata(null, OnAction1ButtonStylePropertyChanged));

	public static readonly DependencyProperty Action2ButtonTextProperty = DependencyProperty.Register("Action2ButtonText", typeof(string), typeof(OneUIDialog), new PropertyMetadata(string.Empty, OnAction2ButtonTextPropertyChanged));

	public static readonly DependencyProperty Action2ButtonStyleProperty = DependencyProperty.Register("Action2ButtonStyle", typeof(Style), typeof(OneUIDialog), new PropertyMetadata(null, OnAction2ButtonStylePropertyChanged));

	public static readonly DependencyProperty Action3ButtonTextProperty = DependencyProperty.Register("Action3ButtonText", typeof(string), typeof(OneUIDialog), new PropertyMetadata(string.Empty, OnAction3ButtonTextPropertyChanged));

	public static readonly DependencyProperty Action3ButtonStyleProperty = DependencyProperty.Register("Action3ButtonStyle", typeof(Style), typeof(OneUIDialog), new PropertyMetadata(null, OnAction3ButtonStylePropertyChanged));

	public static readonly DependencyProperty Action1ButtonTypeProperty = DependencyProperty.Register("Action1ButtonType", typeof(FlatButtonType), typeof(OneUIDialog), new PropertyMetadata(FlatButtonType.Secondary, OnAction1ButtonTypePropertyChanged));

	public static readonly DependencyProperty Action2ButtonTypeProperty = DependencyProperty.Register("Action2ButtonType", typeof(FlatButtonType), typeof(OneUIDialog), new PropertyMetadata(FlatButtonType.Secondary, OnAction2ButtonTypePropertyChanged));

	public static readonly DependencyProperty Action3ButtonTypeProperty = DependencyProperty.Register("Action3ButtonType", typeof(FlatButtonType), typeof(OneUIDialog), new PropertyMetadata(FlatButtonType.Secondary, OnAction3ButtonTypePropertyChanged));

	public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(OneUIDialog), new PropertyMetadata(null, OnContentPropertyChanged));

	public static readonly DependencyProperty DialogHandlerProperty = DependencyProperty.Register("DialogHandler", typeof(FrameworkElement), typeof(OneUIDialog), new PropertyMetadata(null));

	public static readonly DependencyProperty DefaultButtonProperty = DependencyProperty.Register("DefaultButton", typeof(OneUIContentDialogButton), typeof(OneUIDialog), new PropertyMetadata(OneUIContentDialogButton.Action1));

	public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("ButtonOrientation", typeof(Orientation), typeof(OneUIDialog), new PropertyMetadata(Orientation.Horizontal, OnButtonOrientationPropertyChanged));

	public static readonly DependencyProperty TitleAlignmentProperty = DependencyProperty.Register("TitleAlignment", typeof(HorizontalAlignment), typeof(OneUIDialog), new PropertyMetadata(HorizontalAlignment.Left, OnTitleAlignmentChanged));

	public static readonly DependencyProperty IsLightDismissEnabledProperty = DependencyProperty.Register("IsLightDismissEnabled", typeof(bool), typeof(OneUIDialog), new PropertyMetadata(false, OnIsLightDismissEnabledChanged));

	public static readonly DependencyProperty WidthSizeProperty = DependencyProperty.Register("WidthSize", typeof(DialogWidthSizeEnum), typeof(OneUIDialog), new PropertyMetadata(DialogWidthSizeEnum.Medium, OnWidthContentDialogPropertyChanged));

	public static readonly DependencyProperty PositionProperty = DependencyProperty.Register("Position", typeof(DialogPosition), typeof(OneUIDialog), new PropertyMetadata(DialogPosition.Default));

	public static readonly DependencyProperty CustomHorizontalPositionProperty = DependencyProperty.Register("CustomHorizontalPosition", typeof(float), typeof(OneUIDialog), new PropertyMetadata(0f, OnCustomHorizontalPositionChanged));

	public static readonly DependencyProperty CustomVerticalPositionProperty = DependencyProperty.Register("CustomVerticalPosition", typeof(float), typeof(OneUIDialog), new PropertyMetadata(0f, OnCustomVerticalPositionPropertyChanged));

	public static readonly DependencyProperty CustomAppBarMarginProperty = DependencyProperty.Register("CustomAppBarMargin", typeof(Thickness), typeof(OneUIDialog), new PropertyMetadata(new Thickness(0.0), OnCustomAppBarMarginPropertyChanged));

	public static readonly DependencyProperty CustomSmokeBackgroundProperty = DependencyProperty.Register("CustomSmokedBackgroundResourceKey", typeof(string), typeof(OneUIDialog), new PropertyMetadata(null, OnCustomSmokeBackgroundResourceKeyPropertyChanged));

	public static readonly DependencyProperty ContentMarginProperty = DependencyProperty.Register("ContentMargin", typeof(Thickness), typeof(OneUIDialog), new PropertyMetadata(DEFAULT_CONTENT_MARGIN, OnContentMarginPropertyChanged));

	public static readonly DependencyProperty IsOneUIPrimaryActionButtonEnabledProperty = DependencyProperty.Register("IsOneUIPrimaryActionButtonEnabled", typeof(bool), typeof(OneUIDialog), new PropertyMetadata(true, OnIsOneUIPrimaryActionButtonEnabledChanged));

	public static readonly DependencyProperty IsOneUISecondaryActionButtonEnabledProperty = DependencyProperty.Register("IsOneUISecondaryActionButtonEnabled", typeof(bool), typeof(OneUIDialog), new PropertyMetadata(true, OnIsOneUISecondaryActionButtonEnabledChanged));

	public static readonly DependencyProperty IsOneUICloseActionButtonEnabledProperty = DependencyProperty.Register("IsOneUICloseActionButtonEnabled", typeof(bool), typeof(OneUIDialog), new PropertyMetadata(true, OnIsOneUICloseActionButtonEnabledChanged));

	public static readonly DependencyProperty AllowMultiDialogProperty = DependencyProperty.Register("AllowMultiDialog", typeof(bool), typeof(OneUIDialog), new PropertyMetadata(false));

	public static readonly DependencyProperty AppTitleBarHeightOffsetProperty = DependencyProperty.Register("AppTitleBarHeightOffset", typeof(double), typeof(OneUIDialog), new PropertyMetadata(32.0));

	public Action Action1ButtonProgressCircleMethod { get; set; }

	public Action Action2ButtonProgressCircleMethod { get; set; }

	public Action Action3ButtonProgressCircleMethod { get; set; }

	public new Style Style
	{
		get
		{
			return (Style)GetValue(DialogStyleProperty);
		}
		set
		{
			SetValue(DialogStyleProperty, value);
		}
	}

	public new FlowDirection FlowDirection
	{
		get
		{
			return (FlowDirection)GetValue(FlowDirectionProperty);
		}
		set
		{
			SetValue(FlowDirectionProperty, value);
		}
	}

	public object Title
	{
		get
		{
			return GetValue(TitleProperty);
		}
		set
		{
			SetValue(TitleProperty, value);
		}
	}

	public DataTemplate TitleTemplate
	{
		get
		{
			return (DataTemplate)GetValue(TitleTemplateProperty);
		}
		set
		{
			SetValue(TitleTemplateProperty, value);
		}
	}

	public string Action1ButtonText
	{
		get
		{
			return (string)GetValue(Action1ButtonTextProperty);
		}
		set
		{
			SetValue(Action1ButtonTextProperty, value);
		}
	}

	[Obsolete("This property is deprecated, please use Action1ButtonType instead.")]
	public Style Action1ButtonStyle
	{
		get
		{
			return (Style)GetValue(Action1ButtonStyleProperty);
		}
		set
		{
			SetValue(Action1ButtonStyleProperty, value);
		}
	}

	public string Action2ButtonText
	{
		get
		{
			return (string)GetValue(Action2ButtonTextProperty);
		}
		set
		{
			SetValue(Action2ButtonTextProperty, value);
		}
	}

	[Obsolete("This property is deprecated, please use Action2ButtonType instead.")]
	public Style Action2ButtonStyle
	{
		get
		{
			return (Style)GetValue(Action2ButtonStyleProperty);
		}
		set
		{
			SetValue(Action2ButtonStyleProperty, value);
		}
	}

	public string Action3ButtonText
	{
		get
		{
			return (string)GetValue(Action3ButtonTextProperty);
		}
		set
		{
			SetValue(Action3ButtonTextProperty, value);
		}
	}

	[Obsolete("This property is deprecated, please use Action3ButtonType instead.")]
	public Style Action3ButtonStyle
	{
		get
		{
			return (Style)GetValue(Action3ButtonStyleProperty);
		}
		set
		{
			SetValue(Action3ButtonStyleProperty, value);
		}
	}

	public FlatButtonType Action1ButtonType
	{
		get
		{
			return (FlatButtonType)GetValue(Action1ButtonTypeProperty);
		}
		set
		{
			SetValue(Action1ButtonTypeProperty, value);
		}
	}

	public FlatButtonType Action2ButtonType
	{
		get
		{
			return (FlatButtonType)GetValue(Action2ButtonTypeProperty);
		}
		set
		{
			SetValue(Action2ButtonTypeProperty, value);
		}
	}

	public FlatButtonType Action3ButtonType
	{
		get
		{
			return (FlatButtonType)GetValue(Action3ButtonTypeProperty);
		}
		set
		{
			SetValue(Action3ButtonTypeProperty, value);
		}
	}

	public object Content
	{
		get
		{
			return GetValue(ContentProperty);
		}
		set
		{
			SetValue(ContentProperty, value);
		}
	}

	public FrameworkElement DialogHandler
	{
		get
		{
			return (FrameworkElement)GetValue(DialogHandlerProperty);
		}
		set
		{
			SetValue(DialogHandlerProperty, value);
		}
	}

	public OneUIContentDialogButton DefaultButton
	{
		get
		{
			return (OneUIContentDialogButton)GetValue(DefaultButtonProperty);
		}
		set
		{
			SetValue(DefaultButtonProperty, value);
		}
	}

	[Obsolete("ButtonOrientation property is deprecated. This property will be removed soon.", false)]
	public Orientation ButtonOrientation
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

	public HorizontalAlignment TitleAlignment
	{
		get
		{
			return (HorizontalAlignment)GetValue(TitleAlignmentProperty);
		}
		set
		{
			SetValue(TitleAlignmentProperty, value);
		}
	}

	public bool IsLightDismissEnabled
	{
		get
		{
			return (bool)GetValue(IsLightDismissEnabledProperty);
		}
		set
		{
			SetValue(IsLightDismissEnabledProperty, value);
		}
	}

	public DialogWidthSizeEnum WidthSize
	{
		get
		{
			return (DialogWidthSizeEnum)GetValue(WidthSizeProperty);
		}
		set
		{
			SetValue(WidthSizeProperty, value);
		}
	}

	public DialogPosition Position
	{
		get
		{
			return (DialogPosition)GetValue(PositionProperty);
		}
		set
		{
			SetValue(PositionProperty, value);
		}
	}

	public float CustomHorizontalPosition
	{
		get
		{
			return (float)GetValue(CustomHorizontalPositionProperty);
		}
		set
		{
			SetValue(CustomHorizontalPositionProperty, value);
		}
	}

	public float CustomVerticalPosition
	{
		get
		{
			return (float)GetValue(CustomVerticalPositionProperty);
		}
		set
		{
			SetValue(CustomVerticalPositionProperty, value);
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

	public string CustomSmokedBackgroundResourceKey
	{
		get
		{
			return (string)GetValue(CustomSmokeBackgroundProperty);
		}
		set
		{
			SetValue(CustomSmokeBackgroundProperty, value);
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

	public bool IsOneUIPrimaryActionButtonEnabled
	{
		get
		{
			return (bool)GetValue(IsOneUIPrimaryActionButtonEnabledProperty);
		}
		set
		{
			SetValue(IsOneUIPrimaryActionButtonEnabledProperty, value);
		}
	}

	public bool IsOneUISecondaryActionButtonEnabled
	{
		get
		{
			return (bool)GetValue(IsOneUISecondaryActionButtonEnabledProperty);
		}
		set
		{
			SetValue(IsOneUISecondaryActionButtonEnabledProperty, value);
		}
	}

	public bool IsOneUICloseActionButtonEnabled
	{
		get
		{
			return (bool)GetValue(IsOneUICloseActionButtonEnabledProperty);
		}
		set
		{
			SetValue(IsOneUICloseActionButtonEnabledProperty, value);
		}
	}

	public bool AllowMultiDialog
	{
		get
		{
			return (bool)GetValue(AllowMultiDialogProperty);
		}
		set
		{
			SetValue(AllowMultiDialogProperty, value);
		}
	}

	public double AppTitleBarHeightOffset
	{
		get
		{
			return (double)GetValue(AppTitleBarHeightOffsetProperty);
		}
		set
		{
			SetValue(AppTitleBarHeightOffsetProperty, value);
		}
	}

	public event TypedEventHandler<OneUIDialog, ContentDialogClosedEventArgs> Closed;

	public event TypedEventHandler<OneUIDialog, OneUIContentDialogClosingEventArgs> Closing;

	public event TypedEventHandler<OneUIDialog, ContentDialogOpenedEventArgs> Opened;

	public event TypedEventHandler<OneUIDialog, ContentDialogButtonClickEventArgs> Action1ButtonClick;

	public event TypedEventHandler<OneUIDialog, ContentDialogButtonClickEventArgs> Action2ButtonClick;

	public event TypedEventHandler<OneUIDialog, ContentDialogButtonClickEventArgs> Action3ButtonClick;

	public OneUIDialog()
	{
		base.DefaultStyleKey = typeof(OneUIDialog);
		dialog = new ContentDialog();
		ConfigureDialog();
	}

	private static void OnStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		OneUIDialog oneUIDialog = (OneUIDialog)d;
		oneUIDialog.defaultStyle = (Style)(e?.NewValue);
		oneUIDialog.dialog.Style = oneUIDialog.defaultStyle;
	}

	private static void OnFlowDirectionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((OneUIDialog)d).dialog.FlowDirection = (FlowDirection)(e?.NewValue);
	}

	private static void OnTitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((OneUIDialog)d).dialog.Title = e?.NewValue;
	}

	private static void OnTitleTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((OneUIDialog)d).dialog.TitleTemplate = (DataTemplate)(e?.NewValue);
	}

	private static void OnAction1ButtonTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((OneUIDialog)d).dialog.PrimaryButtonText = (string)e?.NewValue;
	}

	private static void OnAction1ButtonStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((OneUIDialog)d).dialog.PrimaryButtonStyle = (Style)(e?.NewValue);
	}

	private static void OnAction2ButtonTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((OneUIDialog)d).dialog.SecondaryButtonText = (string)e?.NewValue;
	}

	private static void OnAction2ButtonStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((OneUIDialog)d).dialog.SecondaryButtonStyle = (Style)(e?.NewValue);
	}

	private static void OnAction3ButtonTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((OneUIDialog)d).dialog.CloseButtonText = (string)e?.NewValue;
	}

	private static void OnAction3ButtonStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((OneUIDialog)d).dialog.CloseButtonStyle = (Style)(e?.NewValue);
	}

	private static void OnAction1ButtonTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		OneUIDialog oneUIDialog = (OneUIDialog)d;
		if (oneUIDialog.dialog != null)
		{
			FlatButton flatButton = UIExtensionsInternal.FindChildByName<FlatButton>("OneUIPrimaryActionButton", oneUIDialog.dialog);
			if (flatButton != null)
			{
				flatButton.Type = (FlatButtonType)(e?.NewValue);
			}
		}
	}

	private static void OnAction2ButtonTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		OneUIDialog oneUIDialog = (OneUIDialog)d;
		if (oneUIDialog.dialog != null)
		{
			FlatButton flatButton = UIExtensionsInternal.FindChildByName<FlatButton>("OneUISecondaryActionButton", oneUIDialog.dialog);
			if (flatButton != null)
			{
				flatButton.Type = (FlatButtonType)(e?.NewValue);
			}
		}
	}

	private static void OnAction3ButtonTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		OneUIDialog oneUIDialog = (OneUIDialog)d;
		if (oneUIDialog.dialog != null)
		{
			FlatButton flatButton = UIExtensionsInternal.FindChildByName<FlatButton>("OneUICloseActionButton", oneUIDialog.dialog);
			if (flatButton != null)
			{
				flatButton.Type = (FlatButtonType)(e?.NewValue);
			}
		}
	}

	private static void OnContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((OneUIDialog)d).dialog.Content = e?.NewValue;
	}

	private static void OnButtonOrientationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		_ = (OneUIDialog)d;
	}

	private static void OnTitleAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is OneUIDialog oneUIDialog)
		{
			oneUIDialog.dialog.DialogTitleAlignment = (HorizontalAlignment)e.NewValue;
		}
	}

	private static void OnIsLightDismissEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is OneUIDialog oneUIDialog)
		{
			if ((bool)e.NewValue)
			{
				oneUIDialog.dialog.Tapped += oneUIDialog.Dialog_Tapped;
			}
			else
			{
				oneUIDialog.dialog.Tapped -= oneUIDialog.Dialog_Tapped;
			}
		}
	}

	private static void OnWidthContentDialogPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		DialogWidthSizeEnum dialogWidthSizeEnum = (DialogWidthSizeEnum)e.NewValue;
		if (d is OneUIDialog oneUIDialog && (oneUIDialog.Width != (double)dialogWidthSizeEnum || double.IsNaN(oneUIDialog.Width)))
		{
			oneUIDialog.Width = (double)dialogWidthSizeEnum;
		}
	}

	private static void OnCustomHorizontalPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is OneUIDialog oneUIDialog)
		{
			oneUIDialog.SetDialogSpacePosition();
		}
	}

	private static void OnCustomVerticalPositionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is OneUIDialog oneUIDialog)
		{
			oneUIDialog.SetDialogSpacePosition();
		}
	}

	private static void OnCustomAppBarMarginPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((OneUIDialog)d).dialog.CustomAppBarMargin = (Thickness)(e?.NewValue);
	}

	private static void OnCustomSmokeBackgroundResourceKeyPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is OneUIDialog oneUIDialog)
		{
			oneUIDialog.UpdateCustomSmokedBackgroundResourceKey();
		}
	}

	private static void OnContentMarginPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is OneUIDialog oneUIDialog)
		{
			oneUIDialog.UpdateContentMargin();
		}
	}

	private static void OnIsOneUIPrimaryActionButtonEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is OneUIDialog oneUIDialog && e.NewValue is bool isPrimaryButtonEnabled)
		{
			oneUIDialog.dialog.IsPrimaryButtonEnabled = isPrimaryButtonEnabled;
		}
	}

	private static void OnIsOneUISecondaryActionButtonEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is OneUIDialog oneUIDialog && e.NewValue is bool isSecondaryButtonEnabled)
		{
			oneUIDialog.dialog.IsSecondaryButtonEnabled = isSecondaryButtonEnabled;
		}
	}

	private static void OnIsOneUICloseActionButtonEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is OneUIDialog oneUIDialog && e.NewValue is bool isCloseButtonEnabled)
		{
			oneUIDialog.dialog.IsCloseButtonEnabled = isCloseButtonEnabled;
		}
	}

	private void Content_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
	{
		if (e.Key == VirtualKey.Escape)
		{
			_contentDialogResult = ContentDialogResult.None;
			dialog.Hide();
		}
	}

	private void Dialog_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		CalculateButtonsWidth();
		CalculateButtonOrientation();
		UpdateButtonLayout();
	}

	private void Dialog_Loaded(object sender, RoutedEventArgs e)
	{
		_initialWidth = base.Width;
		LoadVisualElement();
		RemoveShadowBorder();
		ConfigureActionButtons();
		CalculateButtonsWidth();
		CalculateVisibleButton();
		UpdateButtonLayout();
		CalculateButtonOrientation();
		SetDialogSpacePosition();
		AddEvents();
		ResizeWhenDialogWidthIsGreaterThanWindow();
		SetUpKeyboardFocusToDefaultActionButton();
	}

	private async void XamlRoot_Changed(XamlRoot sender, XamlRootChangedEventArgs args)
	{
		await base.DispatcherQueue.EnqueueAsync(delegate
		{
			UpdateDialogWidthToUsefulSpace(sender);
			SetSizes();
			SetDialogSpacePosition();
		});
		if (_layoutChangeTimer != null)
		{
			_layoutChangeTimer.Dispose();
		}
		_layoutChangeTimer = new Timer(LayoutChangeTimerElapsed, null, 200, -1);
		_timerRepetition = 0;
	}

	private void PrimaryAndSecondaryButton_Click(object sender, RoutedEventArgs e)
	{
		if (!(sender is Button { Name: var name }))
		{
			return;
		}
		if (!(name == "OneUIPrimaryActionButton"))
		{
			if (name == "OneUISecondaryActionButton")
			{
				ExecuteProgressAction("SecondaryButtonShowingProgressCircle", Action2ButtonProgressCircleMethod, this.Action2ButtonClick);
				_contentDialogResult = ContentDialogResult.Secondary;
			}
		}
		else
		{
			ExecuteProgressAction("PrimaryButtonShowingProgressCircle", Action1ButtonProgressCircleMethod, this.Action1ButtonClick);
			_contentDialogResult = ContentDialogResult.Primary;
		}
	}

	private void CloseButton_Click(object sender, RoutedEventArgs e)
	{
		ExecuteProgressAction("CloseButtonShowingProgressCircle", Action3ButtonProgressCircleMethod, this.Action3ButtonClick);
		_isAction3ButtonClicked = true;
		_contentDialogResult = ContentDialogResult.None;
	}

	private void OneUIDialog_KeyDown(object sender, KeyRoutedEventArgs e)
	{
		if (DefaultButton != OneUIContentDialogButton.None && e.Key == VirtualKey.Enter)
		{
			PerformButtonClick();
			e.Handled = true;
		}
	}

	private void ActionButton_PreviewKeyDown(object sender, KeyRoutedEventArgs e, KeyEventHandler focusAction)
	{
		if (e.Key == VirtualKey.Enter)
		{
			if (sender is Button { FocusState: FocusState.Keyboard } button)
			{
				focusAction?.Invoke(button, e);
				VisualStateManager.GoToState(button, "Pressed", useTransitions: false);
			}
			else if (DefaultButton != OneUIContentDialogButton.None)
			{
				PerformButtonClick();
			}
			e.Handled = true;
		}
	}

	private void Dialog_Unloaded(object sender, RoutedEventArgs e)
	{
		RemoveEvents();
	}

	private void Dialog_Opened(Microsoft.UI.Xaml.Controls.ContentDialog sender, ContentDialogOpenedEventArgs args)
	{
		this.Opened?.Invoke(this, args);
	}

	private void Dialog_AnimationClosing(ContentDialog sender, OneUIContentDialogClosingEventArgs args)
	{
		this.Closing?.Invoke(this, args);
	}

	private void Dialog_Closed(Microsoft.UI.Xaml.Controls.ContentDialog sender, ContentDialogClosedEventArgs args)
	{
		this.Closed?.Invoke(this, args);
	}

	private void Dialog_PrimaryButtonClick(Microsoft.UI.Xaml.Controls.ContentDialog sender, ContentDialogButtonClickEventArgs args)
	{
		_contentDialogResult = ContentDialogResult.Primary;
		this.Action1ButtonClick?.Invoke(this, args);
	}

	private void Dialog_SecondaryButtonClick(Microsoft.UI.Xaml.Controls.ContentDialog sender, ContentDialogButtonClickEventArgs args)
	{
		_contentDialogResult = ContentDialogResult.Secondary;
		this.Action2ButtonClick?.Invoke(this, args);
	}

	private void Dialog_CloseButtonClick(Microsoft.UI.Xaml.Controls.ContentDialog sender, ContentDialogButtonClickEventArgs args)
	{
		_contentDialogResult = ContentDialogResult.None;
		this.Action3ButtonClick?.Invoke(this, args);
	}

	private void Dialog_Tapped(object sender, TappedRoutedEventArgs e)
	{
		if (((FrameworkElement)e.OriginalSource).Name == "Container")
		{
			Hide();
		}
	}

	private void OneUIDialog_ActualThemeChanged(FrameworkElement sender, object args)
	{
		dialog.RequestedTheme = sender.RequestedTheme;
		UpdateLayout();
		UpdateDividerVisibility();
	}

	private void ButtonContainer_LayoutUpdated(object sender, object e)
	{
		UpdateDividerVisibility();
	}

	private void Dialog_AnimationClosed(object sender, EventArgs e)
	{
		this.Closed?.Invoke(this, null);
	}

	private void ConfigureDialog()
	{
		dialog.RequestedTheme = base.RequestedTheme;
		defaultStyle = "OneUIContentDialogStyle".GetStyle();
		dialog.Style = defaultStyle;
		dialog.Loaded += Dialog_Loaded;
		dialog.Unloaded += Dialog_Unloaded;
		dialog.SizeChanged += Dialog_SizeChanged;
		base.ActualThemeChanged += OneUIDialog_ActualThemeChanged;
	}

	private async void LayoutChangeTimerElapsed(object state)
	{
		await base.DispatcherQueue.EnqueueAsync(async delegate
		{
			SetDialogSpacePosition();
			_timerRepetition++;
		});
		if (_timerRepetition >= 5)
		{
			_timerRepetition = 0;
			_layoutChangeTimer.Dispose();
			_layoutChangeTimer = null;
		}
	}

	private void UpdateDialogWidthToUsefulSpace(XamlRoot sender)
	{
		double oneUIContentDialogBorderPadding = GetOneUIContentDialogBorderPadding();
		double num = sender.Size.Width - oneUIContentDialogBorderPadding * 2.0;
		base.Width = ((num < _initialWidth) ? num : _initialWidth);
	}

	private void RemoveShadowBorder()
	{
		Border border = UIExtensionsInternal.FindChildByName<Border>("BackgroundElement", dialog);
		Border border2 = UIExtensionsInternal.FindChildByName<Border>("Container", dialog);
		if (border != null && border2 != null)
		{
			if (border.Translation.Z != 0f)
			{
				border.Translation = new Vector3(0f, 0f, -1000f);
			}
			border2.Translation = new Vector3(0f, 0f, -1000f);
		}
	}

	private void ExecuteProgressAction(string progressCircleXAMLStyleName, Action action, TypedEventHandler<OneUIDialog, ContentDialogButtonClickEventArgs> buttonClickEvent)
	{
		if (action != null)
		{
			VisualStateManager.GoToState(dialog, progressCircleXAMLStyleName, useTransitions: true);
			new Thread((ThreadStart)delegate
			{
				WorkThreadFunction(action);
			}).Start();
		}
		else
		{
			buttonClickEvent?.Invoke(this, null);
			dialog.Hide();
		}
	}

	protected void HideDialogBySpinnerButton()
	{
		ExecuteProgressAction("PrimaryButtonShowingProgressCircle", Action1ButtonProgressCircleMethod, this.Action1ButtonClick);
		_contentDialogResult = ContentDialogResult.Primary;
	}

	private async void WorkThreadFunction(Action method)
	{
		method?.Invoke();
		base.DispatcherQueue.TryEnqueue(delegate
		{
			HideDialog();
		});
	}

	private void HideDialog()
	{
		dialog.XamlRoot = base.XamlRoot;
		dialog.Hide();
	}

	private void AddEvents()
	{
		if (base.XamlRoot != null)
		{
			base.XamlRoot.Changed += XamlRoot_Changed;
		}
		dialog.AnimationClosing += Dialog_AnimationClosing;
		dialog.AnimationClosed += Dialog_AnimationClosed;
		dialog.Closed += Dialog_Closed;
		dialog.Opened += Dialog_Opened;
		dialog.PrimaryButtonClick += Dialog_PrimaryButtonClick;
		dialog.SecondaryButtonClick += Dialog_SecondaryButtonClick;
		dialog.CloseButtonClick += Dialog_CloseButtonClick;
		dialog.KeyDown += OneUIDialog_KeyDown;
		if (_buttonContainer != null)
		{
			_buttonContainer.LayoutUpdated += ButtonContainer_LayoutUpdated;
		}
	}

	private void RemoveEvents()
	{
		dialog.AnimationClosing -= Dialog_AnimationClosing;
		dialog.AnimationClosed -= Dialog_AnimationClosed;
		dialog.Closed -= Dialog_Closed;
		dialog.Opened -= Dialog_Opened;
		dialog.PrimaryButtonClick -= Dialog_PrimaryButtonClick;
		dialog.SecondaryButtonClick -= Dialog_SecondaryButtonClick;
		dialog.CloseButtonClick -= Dialog_CloseButtonClick;
		dialog.Tapped -= Dialog_Tapped;
		dialog.KeyDown -= OneUIDialog_KeyDown;
		if (_buttonContainer != null)
		{
			_buttonContainer.LayoutUpdated -= ButtonContainer_LayoutUpdated;
		}
		if (base.XamlRoot != null)
		{
			base.XamlRoot.Changed -= XamlRoot_Changed;
		}
	}

	private void PerformButtonClick()
	{
		PerformButtonClick(DefaultButton);
	}

	private static bool IsButtonEnableAndVisible(Button button)
	{
		if (button != null && button.IsEnabled)
		{
			return button.Visibility == Visibility.Visible;
		}
		return false;
	}

	private void ResizeWhenDialogWidthIsGreaterThanWindow()
	{
		UpdateDialogWidthToInitialUsefulSpace();
	}

	private void UpdateDialogWidthToInitialUsefulSpace()
	{
		if (!(base.XamlRoot == null))
		{
			double oneUIContentDialogBorderPadding = GetOneUIContentDialogBorderPadding();
			double num = base.XamlRoot.Size.Width - oneUIContentDialogBorderPadding * 2.0;
			if (num < base.Width)
			{
				base.Width = num;
				SetSizes();
			}
		}
	}

	private double GetOneUIContentDialogBorderPadding()
	{
		double? num = "OneUIContentDialogBorderPadding".GetKey() as double?;
		if (!num.HasValue)
		{
			return 0.0;
		}
		return num.Value;
	}

	private void SetDialogSpacePosition()
	{
		UpdateLayout();
		Border border = UIExtensionsInternal.FindChildByName<Border>("BackgroundElement", dialog);
		Grid grid = UIExtensionsInternal.FindChildByName<Grid>("DialogSpace", dialog);
		if (!(border == null) && !(grid == null))
		{
			ResetBorderAlignment(border);
			SetPosition(border, grid);
			border.ApplyAlignmentBounds(grid, dialog);
		}
	}

	private void ResetBorderAlignment(Border border)
	{
		if (!(dialog.XamlRoot == null))
		{
			border.HorizontalAlignment = HorizontalAlignment.Left;
			border.VerticalAlignment = VerticalAlignment.Bottom;
		}
	}

	private void SetPosition(Border dialogSpaceBorder, Grid dialogSpace)
	{
		if (DialogHandler == null || dialog.XamlRoot == null)
		{
			SetDialogDefaultCenterPosition(dialogSpaceBorder);
			return;
		}
		Size size = dialog.XamlRoot.Size;
		double num = size.GetAlignmentHorizontal(DialogHandler, dialog);
		double num2 = size.GetAlignmentVertical(DialogHandler, dialog);
		switch (Position)
		{
		case DialogPosition.ButtonTop:
			num = num + DialogHandler.ActualWidth / 2.0 - (dialogSpace.ActualWidth + 12.0) / 2.0;
			num2 = num2 + DialogHandler.ActualHeight + 1.0;
			break;
		case DialogPosition.ButtonBottom:
			num = num + DialogHandler.ActualWidth / 2.0 - (dialogSpace.ActualWidth + 12.0) / 2.0;
			num2 = num2 - dialogSpace.ActualHeight - 22.0;
			break;
		case DialogPosition.ButtonLeft:
			num2 = num2 + DialogHandler.ActualHeight / 2.0 - (dialogSpace.ActualHeight + 12.0) / 2.0;
			num = num - dialogSpace.ActualWidth - 21.0;
			break;
		case DialogPosition.ButtonRight:
			num2 = num2 + DialogHandler.ActualHeight / 2.0 - (dialogSpace.ActualHeight + 12.0) / 2.0;
			num = num + DialogHandler.ActualWidth + 1.0;
			break;
		case DialogPosition.Custom:
			num = CustomHorizontalPosition - 10f;
			num2 = CustomVerticalPosition - 10f;
			break;
		default:
			SetDialogDefaultCenterPosition(dialogSpaceBorder);
			break;
		}
		if (Position == DialogPosition.WindowRightTop)
		{
			dialogSpaceBorder.VerticalAlignment = VerticalAlignment.Top;
			dialogSpaceBorder.HorizontalAlignment = HorizontalAlignment.Right;
			dialogSpaceBorder.Margin = new Thickness(0.0, AppTitleBarHeightOffset + 12.0 - 10.0, 2.0, 0.0);
		}
		else if (Position != DialogPosition.Default)
		{
			dialogSpaceBorder.Margin = new Thickness(num, 0.0, 0.0, num2);
		}
	}

	private void SetDialogDefaultCenterPosition(Border dialogSpaceBorder)
	{
		if (!(dialogSpaceBorder == null))
		{
			dialogSpaceBorder.HorizontalAlignment = HorizontalAlignment.Center;
			dialogSpaceBorder.VerticalAlignment = VerticalAlignment.Center;
			dialogSpaceBorder.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
		}
	}

	public void PerformButtonClick(OneUIContentDialogButton buttonToClick)
	{
		Button button = null;
		switch (buttonToClick)
		{
		case OneUIContentDialogButton.Action1:
			button = UIExtensionsInternal.FindChildByName<Button>("OneUIPrimaryActionButton", dialog);
			break;
		case OneUIContentDialogButton.Action2:
			button = UIExtensionsInternal.FindChildByName<Button>("OneUISecondaryActionButton", dialog);
			break;
		case OneUIContentDialogButton.Action3:
			button = UIExtensionsInternal.FindChildByName<Button>("OneUICloseActionButton", dialog);
			break;
		}
		if (IsButtonEnableAndVisible(button) && buttonToClick != OneUIContentDialogButton.Action3)
		{
			PrimaryAndSecondaryButton_Click(button, null);
		}
		else
		{
			CloseButton_Click(button, null);
		}
	}

	public async void EnableDialogButtonAsync(DialogButton dialogButton, bool enable)
	{
		if (dialog == null)
		{
			return;
		}
		await base.DispatcherQueue.EnqueueAsync(delegate
		{
			switch (dialogButton)
			{
			case DialogButton.Action1:
				dialog.IsPrimaryButtonEnabled = enable;
				break;
			case DialogButton.Action2:
				dialog.IsSecondaryButtonEnabled = enable;
				break;
			case DialogButton.Action3:
				dialog.IsCloseButtonEnabled = enable;
				break;
			}
		});
	}

	public async Task<OneUIContentDialogResult> ShowAsync(CancellationTokenSource cts = null)
	{
		dialog.XamlRoot = base.XamlRoot;
		if (dialog.XamlRoot != null)
		{
			dialog.XamlRoot.Content.PreviewKeyDown += Content_PreviewKeyDown;
		}
		OneUIContentDialogResult oneUIContentDialogResult = GetOneUIContentDialogResult(await ShowDialogAsync(cts));
		if (dialog.XamlRoot != null)
		{
			dialog.XamlRoot.Content.PreviewKeyDown -= Content_PreviewKeyDown;
		}
		return oneUIContentDialogResult;
	}

	public async Task<OneUIContentDialogResult> ShowAsync(ContentDialogPlacement placement, CancellationTokenSource cts = null)
	{
		dialog.XamlRoot = base.XamlRoot;
		if (dialog.XamlRoot != null)
		{
			dialog.XamlRoot.Content.PreviewKeyDown += Content_PreviewKeyDown;
		}
		OneUIContentDialogResult oneUIContentDialogResult = GetOneUIContentDialogResult(await ShowDialogAsync(placement, cts));
		if (dialog.XamlRoot != null)
		{
			dialog.XamlRoot.Content.PreviewKeyDown -= Content_PreviewKeyDown;
		}
		return oneUIContentDialogResult;
	}

	public void Hide()
	{
		dialog.Hide();
	}

	public void SetSizes()
	{
		dialog.DialogMaxHeight = GetMaxHeight();
		if (!double.IsNaN(base.Width))
		{
			dialog.DialogWidth = base.Width;
			bool flag = TextScaleHelper.IsTextScaleChanged();
			if (flag)
			{
				object content = Content;
				bool flag2 = ((content is DateTimePickerDialogContent || content is DatePickerDialogContent) ? true : false);
				flag = flag2;
			}
			if (flag)
			{
				_initialWidth = base.Width;
				SetDialogSpacePosition();
				CalculateButtonsWidth();
				UpdateButtonLayout();
			}
		}
	}

	private double GetMaxHeight()
	{
		if (!double.IsInfinity(base.MaxHeight) && !double.IsNaN(base.MaxHeight) && !(base.MaxHeight > 660.0))
		{
			return base.MaxHeight;
		}
		return 660.0;
	}

	private void SetUpKeyboardFocusToDefaultActionButton()
	{
		if (DefaultButton == OneUIContentDialogButton.None)
		{
			return;
		}
		Button button = UIExtensionsInternal.FindChildByName<Button>("OneUIPrimaryActionButton", dialog);
		Button button2 = UIExtensionsInternal.FindChildByName<Button>("OneUISecondaryActionButton", dialog);
		Button button3 = UIExtensionsInternal.FindChildByName<Button>("OneUICloseActionButton", dialog);
		if (!(button == null) && !(button2 == null) && !(button3 == null) && button.FocusState == FocusState.Keyboard)
		{
			if (DefaultButton == OneUIContentDialogButton.Action1)
			{
				button.Focus(FocusState.Keyboard);
			}
			else if (DefaultButton == OneUIContentDialogButton.Action2)
			{
				button2.Focus(FocusState.Keyboard);
			}
			else
			{
				button3.Focus(FocusState.Keyboard);
			}
		}
	}

	private async Task<ContentDialogResult> ShowDialogAsync(CancellationTokenSource cts = null)
	{
		if (cts != null && cts.IsCancellationRequested)
		{
			return ContentDialogResult.None;
		}
		if (AllowMultiDialog)
		{
			await dialog.ShowAsync();
		}
		else
		{
			using (await dialogLock.LockAsync(cts))
			{
				await dialog.ShowAsync();
			}
		}
		return _contentDialogResult;
	}

	private async Task<ContentDialogResult> ShowDialogAsync(ContentDialogPlacement placement, CancellationTokenSource cts = null)
	{
		if (cts != null && cts.IsCancellationRequested)
		{
			return ContentDialogResult.None;
		}
		if (AllowMultiDialog)
		{
			await dialog.ShowAsync(placement);
		}
		else
		{
			using (await dialogLock.LockAsync(cts))
			{
				await dialog.ShowAsync(placement);
			}
		}
		return _contentDialogResult;
	}

	private void LoadVisualElement()
	{
		if (!(dialog == null))
		{
			_action1Button = UIExtensionsInternal.FindChildByName<FlatButton>("OneUIPrimaryActionButton", dialog);
			_action2Button = UIExtensionsInternal.FindChildByName<FlatButton>("OneUISecondaryActionButton", dialog);
			_action3Button = UIExtensionsInternal.FindChildByName<FlatButton>("OneUICloseActionButton", dialog);
			_divider1 = UIExtensionsInternal.FindChildByName<Rectangle>("Divider1Button", dialog);
			_divider2 = UIExtensionsInternal.FindChildByName<Rectangle>("Divider2Button", dialog);
			_buttonContainer = UIExtensionsInternal.FindChildByName<StackPanel>("ButtonContainer", dialog);
			_commandSpace = UIExtensionsInternal.FindChildByName<Grid>("CommandSpace", dialog);
		}
	}

	private void ConfigureActionButtons()
	{
		SetActionButtonsType();
		SetActionButtonsEvent();
		ConfigureAllTextBlockActionButtons();
	}

	private void SetActionButtonsType()
	{
		if (!(_action1Button == null) && !(_action2Button == null) && !(_action3Button == null))
		{
			_action1Button.Type = Action1ButtonType;
			_action2Button.Type = Action2ButtonType;
			_action3Button.Type = Action3ButtonType;
		}
	}

	private void SetActionButtonsEvent()
	{
		if (!(_action1Button == null) && !(_action2Button == null) && !(_action3Button == null))
		{
			_action1Button.Click += PrimaryAndSecondaryButton_Click;
			_action1Button.PreviewKeyDown += delegate(object sender, KeyRoutedEventArgs e)
			{
				ActionButton_PreviewKeyDown(sender, e, PrimaryAndSecondaryButton_Click);
			};
			_action2Button.Click += PrimaryAndSecondaryButton_Click;
			_action2Button.PreviewKeyDown += delegate(object sender, KeyRoutedEventArgs e)
			{
				ActionButton_PreviewKeyDown(sender, e, PrimaryAndSecondaryButton_Click);
			};
			_action3Button.Click += CloseButton_Click;
			_action3Button.PreviewKeyDown += delegate(object sender, KeyRoutedEventArgs e)
			{
				ActionButton_PreviewKeyDown(sender, e, CloseButton_Click);
			};
		}
	}

	private void ConfigureAllTextBlockActionButtons()
	{
		ConfigureTextBlockActionButtons(_action1Button);
		ConfigureTextBlockActionButtons(_action2Button);
		ConfigureTextBlockActionButtons(_action3Button);
	}

	private void ConfigureTextBlockActionButtons(FlatButton button)
	{
		if (!(button == null))
		{
			button.MaxTextLines = 1;
			button.TextTrimming = TextTrimming.CharacterEllipsis;
		}
	}

	private void CalculateButtonsWidth()
	{
		if (!(_commandSpace == null) && !(_divider1 == null) && !(_divider2 == null))
		{
			_widthForOneButtonVisible = _commandSpace.ActualWidth;
			_widthForTwoButtonVisible = (_commandSpace.ActualWidth - _divider1.ActualWidth - 10.0) / 2.0;
			_widthForThreeButtonVisible = (_commandSpace.ActualWidth - _divider1.ActualWidth - _divider2.ActualWidth - 20.0) / 3.0;
		}
	}

	private void CalculateVisibleButton()
	{
		_buttonVisibleCount = 0;
		_isButtonHorizontal = true;
		CountVisibleButton(_action1Button);
		CountVisibleButton(_action2Button);
		CountVisibleButton(_action3Button);
	}

	private void CountVisibleButton(FlatButton button)
	{
		if (!(button == null) && !(_commandSpace == null))
		{
			if (_commandSpace.Visibility == Visibility.Collapsed)
			{
				_buttonVisibleCount = 0;
			}
			else
			{
				_buttonVisibleCount += ((button.Visibility == Visibility.Visible) ? 1 : 0);
			}
		}
	}

	private void CalculateButtonOrientation()
	{
		_checkedTextBlockCount = 0;
		_isButtonHorizontal = true;
		CheckTextBlock(_action1Button);
		CheckTextBlock(_action2Button);
		CheckTextBlock(_action3Button);
	}

	private void CheckTextBlock(FlatButton button)
	{
		if (button == null)
		{
			return;
		}
		base.DispatcherQueue.TryEnqueue(delegate
		{
			_checkedTextBlockCount++;
			TextBlock textBlock = UIExtensionsInternal.FindChildByName<TextBlock>("PART_Text", button);
			CheckTrimmedTextBlock(textBlock);
			if (_checkedTextBlockCount == 3)
			{
				UpdateButtonLayout();
				SetDialogSpacePosition();
				_checkedTextBlockCount = 0;
			}
		});
	}

	private void CheckTrimmedTextBlock(TextBlock textBlock)
	{
		if (textBlock != null && textBlock.IsTextTrimmed)
		{
			_isButtonHorizontal = false;
		}
	}

	private void UpdateButtonLayout()
	{
		if (!(dialog == null))
		{
			UpdateButtonOrientation();
			UpdateButtonWidth();
			UpdateButtonMaxLineText();
			UpdateButtonMargin();
			UpdateDividerVisibility();
		}
	}

	private void UpdateDividerVisibility()
	{
		if (!(_divider1 == null) && !(_divider2 == null) && !(_action1Button == null))
		{
			if (_buttonVisibleCount == 2)
			{
				_divider1.Visibility = ((!_isButtonHorizontal || _action1Button.Visibility != Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible);
				_divider2.Visibility = ((!_isButtonHorizontal || _action1Button.Visibility != Visibility.Collapsed) ? Visibility.Collapsed : Visibility.Visible);
			}
			else if (_buttonVisibleCount == 3)
			{
				_divider1.Visibility = ((!_isButtonHorizontal) ? Visibility.Collapsed : Visibility.Visible);
				_divider2.Visibility = ((!_isButtonHorizontal) ? Visibility.Collapsed : Visibility.Visible);
			}
		}
	}

	private void UpdateButtonMargin()
	{
		if (!(_action1Button == null) && !(_action2Button == null))
		{
			_action1Button.Margin = ((_isButtonHorizontal || _buttonVisibleCount == 1) ? new Thickness(0.0) : new Thickness(0.0, 0.0, 0.0, 8.0));
			switch (_buttonVisibleCount)
			{
			case 1:
				_action2Button.Margin = new Thickness(0.0);
				break;
			case 2:
				_action2Button.Margin = ((_isButtonHorizontal || _action1Button.Visibility == Visibility.Visible) ? new Thickness(0.0) : new Thickness(0.0, 0.0, 0.0, 8.0));
				break;
			case 3:
				_action2Button.Margin = (_isButtonHorizontal ? new Thickness(0.0) : new Thickness(0.0, 0.0, 0.0, 8.0));
				break;
			}
		}
	}

	private void UpdateButtonMaxLineText()
	{
		if (!SafetyNullCheckActionButtons())
		{
			int maxTextLines = (_isButtonHorizontal ? 1 : 2);
			_action1Button.MaxTextLines = maxTextLines;
			_action2Button.MaxTextLines = maxTextLines;
			_action3Button.MaxTextLines = maxTextLines;
		}
	}

	private void UpdateButtonWidth()
	{
		if (!SafetyNullCheckActionButtons())
		{
			double width = 0.0;
			switch (_buttonVisibleCount)
			{
			case 1:
				width = _widthForOneButtonVisible;
				break;
			case 2:
				width = (_isButtonHorizontal ? _widthForTwoButtonVisible : _widthForOneButtonVisible);
				break;
			default:
				width = (_isButtonHorizontal ? _widthForThreeButtonVisible : _widthForOneButtonVisible);
				break;
			case 0:
				break;
			}
			_action1Button.Width = width;
			_action2Button.Width = width;
			_action3Button.Width = width;
		}
	}

	private bool SafetyNullCheckActionButtons()
	{
		if (!(_action1Button == null) && !(_action2Button == null))
		{
			return _action3Button == null;
		}
		return true;
	}

	private void UpdateButtonOrientation()
	{
		if (!(_buttonContainer == null))
		{
			_buttonContainer.Orientation = (_isButtonHorizontal ? Orientation.Horizontal : Orientation.Vertical);
		}
	}

	private OneUIContentDialogResult GetOneUIContentDialogResult(ContentDialogResult result)
	{
		OneUIContentDialogResult result2 = OneUIContentDialogResult.Action3;
		switch (result)
		{
		case ContentDialogResult.Primary:
			result2 = OneUIContentDialogResult.Action1;
			break;
		case ContentDialogResult.Secondary:
			result2 = OneUIContentDialogResult.Action2;
			break;
		case ContentDialogResult.None:
			if (!_isAction3ButtonClicked)
			{
				result2 = OneUIContentDialogResult.None;
			}
			break;
		}
		_isAction3ButtonClicked = false;
		return result2;
	}

	private void UpdateCustomSmokedBackgroundResourceKey()
	{
		if (!(dialog == null))
		{
			dialog.CustomSmokedBackgroundResourceKey = CustomSmokedBackgroundResourceKey;
		}
	}

	private void UpdateContentMargin()
	{
		if (!(dialog == null))
		{
			dialog.ContentMargin = ContentMargin;
			if (ContentMargin != DEFAULT_CONTENT_MARGIN)
			{
				Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.SetVerticalScrollBarSpacingFromContent(dialog, GridLength.Auto);
			}
		}
	}
}
