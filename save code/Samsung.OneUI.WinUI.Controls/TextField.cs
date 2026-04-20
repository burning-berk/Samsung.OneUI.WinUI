using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Animation;
using Samsung.OneUI.WinUI.AttachedProperties;
using Samsung.OneUI.WinUI.Controls.Selectors;
using Samsung.OneUI.WinUI.Utils.Extensions;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.System;

namespace Samsung.OneUI.WinUI.Controls;

public class TextField : TextBox
{
	private const string PART_ERROR_MESSAGE_PRESENTER = "ErrorMessagePresenter";

	private const string PART_ICON = "PART_Icon";

	private const string PLACEHOLDER_PRESENTER = "PlaceholderTextContentPresenter";

	private const string REST_STATE = "Rest";

	private const string ACTIVATED_AND_TYPING_STATE = "ActivatedAndTyping";

	private const string REST_FILL_STATE = "RestFill";

	private const string ERROR_STATE = "Error";

	private const string DISABLED_STATE = "CustomDisabled";

	private const string ERROR_AND_DISABLED_STATE = "ErrorAndDisabled";

	private const string ENABLE_HEADER_STATE = "EnableHeader";

	private const string DISABLE_HEADER_STATE = "DisableHeader";

	private const string SVG_ICON_GRID = "SVGIconGrid";

	private const string HIDE_ERROR_MESSAGE_ANIMATION = "HideErrorMessageAnimation";

	private const string SHOW_ERROR_MESSAGE_ANIMATION = "ShowErrorMessageAnimation";

	private const string HEADER_CONTENT_PRESENTER = "HeaderContentPresenter";

	private const string CONTENT_SCROLL_VIEWER = "ContentElement";

	private const double DEFAULT_SCROLLVIEWER_MAX_HEIGHT = 77.0;

	private const string EDIT_STRING_ID = "DREAM_EDIT_OPT";

	private TextBlock _errorMessage;

	private ContentControl _icon;

	private ContentPresenter _headerContentPresenter;

	private KeyboardAccelerator _redoKeyboardShortcut;

	private Grid _svgIconGrid;

	private Microsoft.UI.Xaml.Controls.ScrollViewer _contentScrollViewer;

	private Storyboard _notErrorStoryboard;

	private Storyboard _showStoryboard;

	private TextBlock _placeholder;

	private long _tokenTextPropertyRegistered;

	private long _tokenPlaceholderTextPropertyRegistered;

	private long _tokenHeaderPropertyRegistered;

	private long _tokenDescriptionPropertyRegistered;

	internal string _textFieldNarratorText;

	public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(TextFieldType), typeof(TextField), new PropertyMetadata(TextFieldType.Normal, OnTypeChanged));

	public static readonly DependencyProperty ErrorMessageProperty = DependencyProperty.Register("ErrorMessage", typeof(string), typeof(TextField), new PropertyMetadata(string.Empty, OnErrorMessagePropertyChanged));

	public static readonly DependencyProperty SvgIconProperty = DependencyProperty.Register("SvgIcon", typeof(Style), typeof(TextField), new PropertyMetadata(null, OnSvgIconPropertyChanged));

	public static readonly DependencyProperty ScrollViewerMaxHeightProperty = DependencyProperty.Register("ScrollViewerMaxHeight", typeof(double), typeof(TextField), new PropertyMetadata(77.0));

	public TextFieldType Type
	{
		get
		{
			return (TextFieldType)GetValue(TypeProperty);
		}
		set
		{
			SetValue(TypeProperty, value);
		}
	}

	public string ErrorMessage
	{
		get
		{
			return (string)GetValue(ErrorMessageProperty);
		}
		set
		{
			SetValue(ErrorMessageProperty, value);
		}
	}

	public Style SvgIcon
	{
		get
		{
			return (Style)GetValue(SvgIconProperty);
		}
		set
		{
			SetValue(SvgIconProperty, value);
		}
	}

	public double ScrollViewerMaxHeight
	{
		get
		{
			return (double)GetValue(ScrollViewerMaxHeightProperty);
		}
		set
		{
			SetValue(ScrollViewerMaxHeightProperty, value);
		}
	}

	private static void OnErrorMessagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((TextField)d).UpdateErrorMessage();
	}

	private static void OnSvgIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((TextField)d).UpdateSvgIcon();
	}

	public TextField()
	{
		base.Style = new TextFieldStyleSelector(Type).SelectStyle();
	}

	protected override void OnApplyTemplate()
	{
		UnregisterEvents();
		_errorMessage = GetTemplateChild("ErrorMessagePresenter") as TextBlock;
		_icon = GetTemplateChild("PART_Icon") as ContentControl;
		_svgIconGrid = GetTemplateChild("SVGIconGrid") as Grid;
		_headerContentPresenter = GetTemplateChild("HeaderContentPresenter") as ContentPresenter;
		_contentScrollViewer = GetTemplateChild("ContentElement") as Microsoft.UI.Xaml.Controls.ScrollViewer;
		_notErrorStoryboard = GetTemplateChild("HideErrorMessageAnimation") as Storyboard;
		_showStoryboard = GetTemplateChild("ShowErrorMessageAnimation") as Storyboard;
		_placeholder = GetTemplateChild("PlaceholderTextContentPresenter") as TextBlock;
		UpdateErrorMessage();
		UpdateSvgIcon();
		UpdateContentScrollViewer();
		RegisterEvents();
		UpdateHeaderVisibility();
		UpdateNarratorText();
		base.OnApplyTemplate();
	}

	protected override AutomationPeer OnCreateAutomationPeer()
	{
		return new TextFieldAutomationPeer(this);
	}

	private void UpdateNarratorText()
	{
		if (!string.IsNullOrEmpty(ErrorMessage))
		{
			_textFieldNarratorText = ErrorMessage;
		}
		else
		{
			_textFieldNarratorText = GetNarratorText();
		}
	}

	private string GetNarratorText()
	{
		"DREAM_EDIT_OPT".GetLocalized();
		string text = ((!string.IsNullOrEmpty(base.Text)) ? base.Text : (string.IsNullOrEmpty(base.PlaceholderText) ? string.Empty : base.PlaceholderText));
		if (!(base.Header is string text2) || string.IsNullOrEmpty(text2))
		{
			return $"{base.Header} {text} Edit";
		}
		return text2 + " Edit " + text;
	}

	private void RegisterPropertiesChangedTokens()
	{
		_tokenTextPropertyRegistered = RegisterPropertyChangedCallback(TextBox.TextProperty, OnPropertiesChanged);
		_tokenPlaceholderTextPropertyRegistered = RegisterPropertyChangedCallback(TextBox.PlaceholderTextProperty, OnPlaceholderTextPropertiesChanged);
		_tokenHeaderPropertyRegistered = RegisterPropertyChangedCallback(TextBox.HeaderProperty, OnHeaderPropertyChanged);
		_tokenDescriptionPropertyRegistered = RegisterPropertyChangedCallback(TextBox.DescriptionProperty, OnPropertiesChanged);
	}

	private void OnPlaceholderTextPropertiesChanged(DependencyObject sender, DependencyProperty dp)
	{
		UpdateNarratorText();
	}

	private void OnHeaderPropertyChanged(DependencyObject sender, DependencyProperty dp)
	{
		UpdateHeaderVisibility();
		UpdateNarratorText();
	}

	private void UpdateHeaderVisibility()
	{
		if (_headerContentPresenter != null && _headerContentPresenter.Content == null)
		{
			_headerContentPresenter.Visibility = Visibility.Collapsed;
			VisualStateManager.GoToState(this, "DisableHeader", useTransitions: true);
		}
		else
		{
			VisualStateManager.GoToState(this, "EnableHeader", useTransitions: true);
		}
	}

	private void UpdatePlaceholderVisibilityToFocused()
	{
		if (_headerContentPresenter != null && _headerContentPresenter.Content != null && _headerContentPresenter.Visibility == Visibility.Visible)
		{
			ShowPlaceHolderText(isShow: false);
		}
		else if (string.IsNullOrEmpty(base.Text))
		{
			ShowPlaceHolderText();
		}
		else
		{
			ShowPlaceHolderText(isShow: false);
		}
	}

	private void UnregisterPropertiesChangedTokens()
	{
		UnregisterPropertyChangedCallback(TextBox.TextProperty, _tokenTextPropertyRegistered);
		UnregisterPropertyChangedCallback(TextBox.PlaceholderTextProperty, _tokenPlaceholderTextPropertyRegistered);
		UnregisterPropertyChangedCallback(TextBox.HeaderProperty, _tokenHeaderPropertyRegistered);
		UnregisterPropertyChangedCallback(TextBox.DescriptionProperty, _tokenDescriptionPropertyRegistered);
	}

	private void OnPropertiesChanged(DependencyObject sender, DependencyProperty dp)
	{
		UpdateNarratorText();
	}

	private void RegisterExtraKeyboardShortcuts()
	{
		if (!(_redoKeyboardShortcut != null))
		{
			_redoKeyboardShortcut = new KeyboardAccelerator
			{
				Modifiers = (VirtualKeyModifiers.Control | VirtualKeyModifiers.Shift),
				Key = VirtualKey.Z
			};
			_redoKeyboardShortcut.Invoked += KeyboardRedoShortcutAccelerator_Invoked;
			base.KeyboardAccelerators.Add(_redoKeyboardShortcut);
		}
	}

	private void UpdateErrorMessage()
	{
		if (!(_errorMessage == null))
		{
			if (string.IsNullOrWhiteSpace(ErrorMessage))
			{
				VisualStateManager.GoToState(this, "ActivatedAndTyping", useTransitions: true);
			}
			else
			{
				_errorMessage.Text = ErrorMessage;
				VisualStateManager.GoToState(this, "Error", useTransitions: true);
			}
			ControlColorState();
			VerifyAnimationEnabled();
			UpdateNarratorText();
		}
	}

	private void VerifyAnimationEnabled()
	{
		_showStoryboard?.ValidateAnimationEnabled();
		_notErrorStoryboard?.ValidateAnimationEnabled();
	}

	private void UpdateSvgIcon()
	{
		if (_icon != null && _svgIconGrid != null)
		{
			_svgIconGrid.Visibility = ((!(SvgIcon != null)) ? Visibility.Collapsed : Visibility.Visible);
		}
		UpdateHeaderVisibility();
		UpdateNarratorText();
	}

	private bool IsErrorMessageEnabled()
	{
		return !string.IsNullOrWhiteSpace(ErrorMessage);
	}

	private void ControlColorState()
	{
		if (base.IsEnabled)
		{
			ChangeVisualStateToUnfocused();
		}
		else if (IsErrorMessageEnabled())
		{
			VisualStateManager.GoToState(this, "ErrorAndDisabled", useTransitions: true);
		}
		else
		{
			VisualStateManager.GoToState(this, "CustomDisabled", useTransitions: true);
		}
	}

	private void ChangeVisualStateToFocused()
	{
		if (IsErrorMessageEnabled())
		{
			VisualStateManager.GoToState(this, "Error", useTransitions: true);
		}
		else
		{
			VisualStateManager.GoToState(this, "ActivatedAndTyping", useTransitions: true);
		}
		UpdatePlaceholderVisibilityToFocused();
	}

	private void ChangeVisualStateToUnfocused()
	{
		if (IsErrorMessageEnabled())
		{
			VisualStateManager.GoToState(this, "Error", useTransitions: true);
		}
		else if (string.IsNullOrEmpty(base.Text))
		{
			VisualStateManager.GoToState(this, "Rest", useTransitions: true);
		}
		else
		{
			VisualStateManager.GoToState(this, "RestFill", useTransitions: true);
		}
		if (string.IsNullOrEmpty(base.Text))
		{
			ShowPlaceHolderText();
		}
		else
		{
			ShowPlaceHolderText(isShow: false);
		}
	}

	private void UpdateContentScrollViewer()
	{
		if (!(_contentScrollViewer == null))
		{
			if (_contentScrollViewer.ScrollableHeight > 0.0)
			{
				_contentScrollViewer.IsTabStop = true;
				Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.SetVerticalScrollBarSpacingFromContent(_contentScrollViewer, new GridLength(12.0));
			}
			else
			{
				_contentScrollViewer.IsTabStop = false;
				Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.SetVerticalScrollBarSpacingFromContent(_contentScrollViewer, GridLength.Auto);
			}
		}
	}

	private void UnregisterEvents()
	{
		base.GettingFocus -= TextField_GettingFocus;
		base.LostFocus -= TextField_LostFocus;
		base.TextChanged += TextField_TextChanged;
		base.KeyDown -= TextField_KeyDown;
		base.PreviewKeyDown -= TextField_PreviewKeyDown;
		base.IsEnabledChanged -= TextField_IsEnabledChanged;
		if (_notErrorStoryboard != null)
		{
			_notErrorStoryboard.Completed -= NotErrorStoryboard_Completed;
		}
		if (_redoKeyboardShortcut != null)
		{
			_redoKeyboardShortcut.Invoked -= KeyboardRedoShortcutAccelerator_Invoked;
			base.KeyboardAccelerators.Remove(_redoKeyboardShortcut);
			_redoKeyboardShortcut = null;
		}
		if (_contentScrollViewer != null)
		{
			_contentScrollViewer.SizeChanged -= ContentScrollViewer_SizeChanged;
		}
		UnregisterPropertiesChangedTokens();
	}

	private void RegisterEvents()
	{
		base.GettingFocus += TextField_GettingFocus;
		base.LostFocus += TextField_LostFocus;
		base.TextChanged += TextField_TextChanged;
		base.KeyDown += TextField_KeyDown;
		base.PreviewKeyDown += TextField_PreviewKeyDown;
		base.IsEnabledChanged += TextField_IsEnabledChanged;
		if (_notErrorStoryboard != null)
		{
			_notErrorStoryboard.Completed += NotErrorStoryboard_Completed;
		}
		RegisterExtraKeyboardShortcuts();
		if (_contentScrollViewer != null)
		{
			_contentScrollViewer.SizeChanged += ContentScrollViewer_SizeChanged;
		}
		RegisterPropertiesChangedTokens();
	}

	private void ContentScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		UpdateContentScrollViewer();
	}

	private void NotErrorStoryboard_Completed(object sender, object e)
	{
		_errorMessage.Text = ErrorMessage;
		UpdateNarratorText();
	}

	private void TextField_TextChanged(object sender, TextChangedEventArgs e)
	{
		ChangeVisualStateToFocused();
	}

	private void TextField_KeyDown(object sender, KeyRoutedEventArgs e)
	{
		if (e.Key == VirtualKey.Escape)
		{
			base.Text = string.Empty;
		}
		else
		{
			ChangeVisualStateToFocused();
		}
	}

	private void TextField_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
	{
		if (e.Key == VirtualKey.Back && string.IsNullOrEmpty(base.Text))
		{
			e.Handled = true;
		}
	}

	private void TextField_GettingFocus(UIElement sender, GettingFocusEventArgs args)
	{
		ChangeVisualStateToFocused();
	}

	private void TextField_LostFocus(object sender, RoutedEventArgs e)
	{
		ChangeVisualStateToUnfocused();
	}

	private void TextField_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		ControlColorState();
	}

	private static void OnTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is TextField textField)
		{
			textField.Style = new TextFieldStyleSelector(textField.Type).SelectStyle();
		}
	}

	private void KeyboardRedoShortcutAccelerator_Invoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
	{
		args.Handled = true;
		Redo();
	}

	private void ShowPlaceHolderText(bool isShow = true)
	{
		VisualStateManager.GoToState(this, isShow ? "PlaceHolderVisible" : "PlaceHolderCollapsed", useTransitions: false);
	}
}
