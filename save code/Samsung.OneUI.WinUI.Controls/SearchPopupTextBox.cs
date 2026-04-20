using System.Numerics;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.UI.ViewManagement;

namespace Samsung.OneUI.WinUI.Controls;

public class SearchPopupTextBox : TextBox
{
	private const string QUERY_BUTTON_NAME = "QueryButton";

	private const string DELETE_BUTTON_NAME = "DeleteButton";

	private const string ELLIPSIS_CONTENT_ELEMENT = "EllipsisContentElement";

	private const string ROOT_BORDER = "RootBorder";

	private const string ROOT_CONTAINER = "RootContainer";

	private const string SEMANTIC_LAYER = "SemanticLayer";

	private const string HC_COLOR2 = "OneUIHighContrastColor2";

	private const string HC_COLOR6 = "OneUIHighContrastColor6";

	private const string STATE_GROUP_COMMON_STATES = "CommonStates";

	private const string TEXTBOX_NORMAL_STATE = "Normal";

	private const string TEXTBOX_HOVER_STATE = "PointerOver";

	private const string TEXTBOX_FOCUSED_STATE = "Focused";

	private const string CLEAR_SEARCH_FIELD_OPT = "DREAM_CLEAR_SEARCH_FIELD_OPT";

	private const string BUTTON_CONTROL_TYPE = "SS_BUTTON_TTS/Text";

	private const string SELECTED_STRING_ID = "SS_SELECTED";

	private const string FIRST_GLOW_LAYER = "FirstGlowLayer";

	private const string GLOW_LAYER = "GlowLayer";

	private const string GLOW_LAYER_BLUR = "GlowLayerBlur";

	private const float FIRST_GLOW_LAYER_BLUR_AMOUNT = 2.56f;

	private const float BLUR_GROW_SIZE = 6f;

	private const string SECOND_GLOW_LAYER = "SecondGlowLayer";

	private const string INTELLIGENCE_EFFECT_GRID = "IntelligenceEffectGrid";

	private const string GAUSSIAN_BLUR_EFFECT_NAME = "Blur";

	private const string GAUSSIAN_BLUR_EFFECT_SOURCE_NAME = "Source";

	private Button _queryButton;

	private Button _deleteButton;

	private TextBlock _textElement;

	private Border _rootBorder;

	private Border _rootContainer;

	private Grid _semanticLayer;

	private readonly AccessibilitySettings _accessibilitySettings;

	private VisualStateGroup _commonStatesGroup;

	private readonly string _clearSearchFieldResource = "DREAM_CLEAR_SEARCH_FIELD_OPT".GetLocalized();

	private readonly string _buttonResource = "SS_BUTTON_TTS/Text".GetLocalized();

	private readonly string _selectedResource = "SS_SELECTED".GetLocalized();

	private Grid _firstGlowLayer;

	private Grid _glowLayer;

	private Grid _glowLayerBlur;

	private Grid _secondGlowLayer;

	private Grid _intelligenceEffectGrid;

	private Compositor _compositor;

	private CompositionVisualSurface _visualSurface;

	private SpriteVisual _blurVisual;

	private bool _isRightTapped;

	public SearchPopupTextBox()
	{
		_accessibilitySettings = new AccessibilitySettings();
		base.GotFocus += SearchPopupTextBox_GotFocus;
		base.TextChanged += SearchPopupTextBox_TextChanged;
		base.Loaded += SearchPopupTextBox_Loaded;
		base.Unloaded += SearchPopupTextBox_Unloaded;
		base.LostFocus += SearchPopupTextBox_LostFocus;
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_queryButton = GetTemplateChild("QueryButton") as Button;
		_deleteButton = GetTemplateChild("DeleteButton") as Button;
		_textElement = GetTemplateChild("EllipsisContentElement") as TextBlock;
		_rootBorder = GetTemplateChild("RootBorder") as Border;
		_rootContainer = GetTemplateChild("RootContainer") as Border;
		_semanticLayer = GetTemplateChild("SemanticLayer") as Grid;
		_commonStatesGroup = GetTemplateChild("CommonStates") as VisualStateGroup;
		_firstGlowLayer = GetTemplateChild("FirstGlowLayer") as Grid;
		_glowLayer = GetTemplateChild("GlowLayer") as Grid;
		_glowLayerBlur = GetTemplateChild("GlowLayerBlur") as Grid;
		_secondGlowLayer = GetTemplateChild("SecondGlowLayer") as Grid;
		_intelligenceEffectGrid = GetTemplateChild("IntelligenceEffectGrid") as Grid;
		UpdateButtonVisibility();
		UnregisterEvents();
		RegisterEvents();
		UpdateDeleteButtonTooltip();
		UpdateDeleteButtonAccessibilityName();
	}

	private void SearchPopupTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (!(_textElement == null) && !(_accessibilitySettings == null))
		{
			if (string.IsNullOrEmpty(_textElement.Text) && _accessibilitySettings.HighContrast)
			{
				SetBorderRestState();
			}
			SetBorderRestState();
			SetContainerSematicSearchState();
			UpdateButtonVisibility();
			ShowSecondGlowLayer();
			ShowIntelligenceEffectLayer();
		}
	}

	private void SearchPopupTextBox_Unloaded(object sender, RoutedEventArgs e)
	{
		UnregisterEvents();
		ClearReferences();
	}

	private void QueryButton_Click(object sender, RoutedEventArgs e)
	{
		Focus(FocusState.Programmatic);
	}

	private async void QueryButton_GettingFocusAsync(object sender, RoutedEventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(base.Text))
		{
			TurnOnlyDeleteButtonVisible();
			await FocusManager.TryFocusAsync(_deleteButton, FocusState.Keyboard);
		}
	}

	private void DeleteButton_GettingFocus(UIElement sender, GettingFocusEventArgs args)
	{
		TurnOnlyDeleteButtonVisible();
	}

	private void DeleteButton_LostFocus(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(base.Text))
		{
			TurnOnlyQueryButtonVisible();
		}
	}

	private void CommonStatesGroup_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
	{
		UpdateButtonVisibility();
	}

	private void SearchPopupTextBox_Loaded(object sender, RoutedEventArgs e)
	{
		ApplyGaussianBlurEffect();
		base.SizeChanged += SearchPopupTextBox_SizeChanged;
	}

	private void SearchPopupTextBox_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		UpdateGlowLayerSize(_firstGlowLayer, e.NewSize.Width, e.NewSize.Height);
		base.DispatcherQueue.TryEnqueue(delegate
		{
			UpdateBlurSize();
		});
	}

	private void SearchPopupTextBox_GotFocus(object sender, RoutedEventArgs e)
	{
		ShowSecondGlowLayer();
		ShowIntelligenceEffectLayer();
	}

	private void SearchPopupTextBox_RightTapped(object sender, RightTappedRoutedEventArgs e)
	{
		_isRightTapped = true;
	}

	private void SearchPopupTextBox_LostFocus(object sender, RoutedEventArgs e)
	{
		if (_isRightTapped && !IsSearched())
		{
			_isRightTapped = false;
			return;
		}
		SetBorderRestState();
		SetContainerSematicSearchState();
		UpdateButtonVisibility();
	}

	private void RegisterEvents()
	{
		if (_commonStatesGroup != null)
		{
			_commonStatesGroup.CurrentStateChanged += CommonStatesGroup_CurrentStateChanged;
		}
		if (_queryButton != null)
		{
			_queryButton.GotFocus += QueryButton_GettingFocusAsync;
			_queryButton.Click += QueryButton_Click;
		}
		if (_deleteButton != null)
		{
			_deleteButton.GettingFocus += DeleteButton_GettingFocus;
			_deleteButton.LostFocus += DeleteButton_LostFocus;
			_deleteButton.PointerEntered += DeleteButton_PointerEntered;
			_deleteButton.PointerExited += DeleteButton_PointerExited;
			_deleteButton.Click += DeleteButton_Click;
		}
		AddHandler(UIElement.RightTappedEvent, new RightTappedEventHandler(SearchPopupTextBox_RightTapped), handledEventsToo: true);
	}

	private void DeleteButton_Click(object sender, RoutedEventArgs e)
	{
		base.DispatcherQueue.TryEnqueue(delegate
		{
			Focus(FocusState.Programmatic);
			base.Text = string.Empty;
		});
	}

	private void DeleteButton_PointerExited(object sender, PointerRoutedEventArgs e)
	{
		VisualStateManager.GoToState(this, (base.FocusState == FocusState.Unfocused) ? "PointerOver" : "Focused", useTransitions: false);
	}

	private void DeleteButton_PointerEntered(object sender, PointerRoutedEventArgs e)
	{
		VisualStateManager.GoToState(this, (base.FocusState == FocusState.Unfocused) ? "Normal" : "Focused", useTransitions: false);
	}

	private void UnregisterEvents()
	{
		if (_commonStatesGroup != null)
		{
			_commonStatesGroup.CurrentStateChanged -= CommonStatesGroup_CurrentStateChanged;
		}
		if (_queryButton != null)
		{
			_queryButton.GotFocus -= QueryButton_GettingFocusAsync;
			_queryButton.Click -= QueryButton_Click;
		}
		if (_deleteButton != null)
		{
			_deleteButton.GettingFocus -= DeleteButton_GettingFocus;
			_deleteButton.LostFocus -= DeleteButton_LostFocus;
			_deleteButton.PointerEntered -= DeleteButton_PointerEntered;
			_deleteButton.PointerExited -= DeleteButton_PointerExited;
			_deleteButton.Click -= DeleteButton_Click;
		}
		RemoveHandler(UIElement.RightTappedEvent, new RightTappedEventHandler(SearchPopupTextBox_RightTapped));
	}

	private void SetBorderRestState()
	{
		if (_rootBorder != null && _textElement != null && _semanticLayer == null && _accessibilitySettings.HighContrast)
		{
			_rootBorder.BorderBrush = (string.IsNullOrEmpty(_textElement.Text) ? ("OneUIHighContrastColor2".GetKey() as SolidColorBrush) : ("OneUIHighContrastColor6".GetKey() as SolidColorBrush));
		}
	}

	private void UpdateButtonVisibility()
	{
		if (!string.IsNullOrWhiteSpace(base.Text))
		{
			TurnOnlyDeleteButtonVisible();
		}
		else if (base.FocusState == FocusState.Unfocused)
		{
			TurnOnlyQueryButtonVisible();
		}
		else
		{
			TurnOffQueryAndDeleteButton();
		}
	}

	private void TurnOnlyDeleteButtonVisible()
	{
		_deleteButton.Visibility = Visibility.Visible;
		_queryButton.Visibility = Visibility.Collapsed;
	}

	private void TurnOnlyQueryButtonVisible()
	{
		_queryButton.Visibility = Visibility.Visible;
		_deleteButton.Visibility = Visibility.Collapsed;
	}

	private void TurnOffQueryAndDeleteButton()
	{
		_queryButton.Visibility = Visibility.Collapsed;
		_deleteButton.Visibility = Visibility.Collapsed;
	}

	private bool IsSearched()
	{
		if (!string.IsNullOrEmpty(_textElement.Text))
		{
			return base.IsEnabled;
		}
		return false;
	}

	private void SetContainerSematicSearchState()
	{
		if (!(_rootContainer == null) && !(_textElement == null) && !(_semanticLayer == null))
		{
			_rootContainer.Visibility = ((IsSearched() || base.FocusState != FocusState.Unfocused) ? Visibility.Collapsed : Visibility.Visible);
			_semanticLayer.Visibility = ((!IsSearched() && base.FocusState == FocusState.Unfocused) ? Visibility.Collapsed : Visibility.Visible);
			_glowLayer.Opacity = ((IsSearched() || base.FocusState != FocusState.Unfocused) ? 1 : 0);
		}
	}

	private void UpdateDeleteButtonAccessibilityName()
	{
		if (!(_deleteButton == null) && !string.IsNullOrEmpty(_clearSearchFieldResource) && !string.IsNullOrEmpty(_buttonResource) && !string.IsNullOrEmpty(_selectedResource))
		{
			AutomationProperties.SetName(_deleteButton, _clearSearchFieldResource + " " + _buttonResource);
			AutomationProperties.SetLocalizedControlType(_deleteButton, _selectedResource ?? "");
		}
	}

	private void UpdateDeleteButtonTooltip()
	{
		if (!(_deleteButton == null))
		{
			ToolTip value = new ToolTip
			{
				Content = _clearSearchFieldResource
			};
			ToolTipService.SetToolTip(_deleteButton, value);
		}
	}

	private void ApplyGaussianBlurEffect()
	{
		if (!(_glowLayer == null) && !(_glowLayerBlur == null) && !(_firstGlowLayer == null))
		{
			_compositor = ElementCompositionPreview.GetElementVisual(_glowLayer).Compositor;
			_visualSurface = _compositor.CreateVisualSurface();
			_visualSurface.SourceVisual = ElementCompositionPreview.GetElementVisual(_firstGlowLayer);
			CompositionSurfaceBrush compositionSurfaceBrush = _compositor.CreateSurfaceBrush(_visualSurface);
			compositionSurfaceBrush.Stretch = CompositionStretch.None;
			GaussianBlurEffect graphicsEffect = new GaussianBlurEffect
			{
				Name = "Blur",
				Source = new CompositionEffectSourceParameter("Source"),
				BlurAmount = 2.56f,
				BorderMode = EffectBorderMode.Soft
			};
			CompositionEffectBrush compositionEffectBrush = _compositor.CreateEffectFactory(graphicsEffect).CreateBrush();
			compositionEffectBrush.SetSourceParameter("Source", compositionSurfaceBrush);
			_blurVisual = _compositor.CreateSpriteVisual();
			_blurVisual.Brush = compositionEffectBrush;
			ElementCompositionPreview.SetElementChildVisual(_glowLayerBlur, _blurVisual);
			UpdateBlurSize();
		}
	}

	private void UpdateBlurSize()
	{
		if (_glowLayerBlur == null || _firstGlowLayer == null || _visualSurface == null || _blurVisual == null)
		{
			return;
		}
		float num = (double.IsNaN(_firstGlowLayer.Width) ? ((float)(_firstGlowLayer.ActualWidth - (0.0 - _glowLayer.Margin.Left) * 2.0)) : ((float)_firstGlowLayer.ActualWidth));
		float num2 = (double.IsNaN(_firstGlowLayer.Height) ? ((float)(_firstGlowLayer.ActualHeight - (0.0 - _glowLayer.Margin.Top) * 2.0)) : ((float)_firstGlowLayer.ActualHeight));
		if (!(num <= 0f) && !(num2 <= 0f))
		{
			if (double.IsNaN(_firstGlowLayer.Width))
			{
				_firstGlowLayer.Width = num;
			}
			if (double.IsNaN(_firstGlowLayer.Height))
			{
				_firstGlowLayer.Height = num2;
			}
			CalculateGaussianBlurSize(num, num2);
		}
	}

	private void UpdateGlowLayerSize(FrameworkElement element, double width, double height)
	{
		if (!(element == null))
		{
			element.Width = width;
			element.Height = height;
		}
	}

	private void CalculateGaussianBlurSize(float targetWidth, float targetHeight)
	{
		if (!(_visualSurface == null) && !(_blurVisual == null))
		{
			float num = 15.36f;
			float num2 = targetWidth + num;
			float num3 = targetHeight + num;
			_visualSurface.SourceSize = new Vector2(targetWidth, targetHeight);
			_blurVisual.Size = new Vector2(num2, num3);
			UpdateGlowLayerSize(_glowLayerBlur, num2, num3);
		}
	}

	private void ShowSecondGlowLayer()
	{
		if (!(_secondGlowLayer == null) && !(_textElement == null))
		{
			bool flag = GetCurrentTheme() == ElementTheme.Light || IsSearched();
			_secondGlowLayer.Visibility = ((!flag) ? Visibility.Collapsed : Visibility.Visible);
		}
	}

	private void ShowIntelligenceEffectLayer()
	{
		if (!(_intelligenceEffectGrid == null) && !(_textElement == null))
		{
			bool flag = GetCurrentTheme() == ElementTheme.Dark && !IsSearched();
			_intelligenceEffectGrid.Visibility = ((!flag) ? Visibility.Collapsed : Visibility.Visible);
		}
	}

	private ElementTheme GetCurrentTheme()
	{
		if (base.XamlRoot.Content is FrameworkElement { RequestedTheme: not ElementTheme.Default } frameworkElement)
		{
			return frameworkElement.RequestedTheme;
		}
		return base.ActualTheme;
	}

	private void ClearReferences()
	{
		_blurVisual?.Dispose();
		_visualSurface?.Dispose();
		_firstGlowLayer = null;
		_glowLayer = null;
		_glowLayerBlur = null;
		_secondGlowLayer = null;
		_intelligenceEffectGrid = null;
		_compositor = null;
	}
}
