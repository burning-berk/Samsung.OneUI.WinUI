using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Windows.System.Power;
using Windows.UI.ViewManagement;
using WinRT;
using WinRT.Samsung_OneUI_WinUIVtableClasses;

namespace Samsung.OneUI.WinUI.Controls;

[ContentProperty(Name = "LayerContent")]
[WinRTRuntimeClassName("Microsoft.UI.Xaml.IUIElementOverrides")]
[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_BGBlurWinRTTypeDetails))]
public sealed class BGBlur : UserControl, IComponentConnector
{
	private const string BASE_LAYER = "BaseLayer";

	private const string GRAYISH_LAYER_THIN = "GrayishLayerThin";

	private const string GRAYISH_LAYER_REGULAR = "GrayishLayerRegular";

	private const string GRAYISH_LAYER_THICK = "GrayishLayerThick";

	private const string DARK_GRAYISH_BASE_LAYER = "DarkGrayishBaseLayer";

	private const string DARK_GRAYISH_GRAYISH_LAYER = "DarkGrayishGrayishLayer";

	private readonly UISettings _uiSettings;

	private readonly AccessibilitySettings _accessibilitySettings;

	public static readonly DependencyProperty LayerContentProperty = DependencyProperty.Register("LayerContent", typeof(UIElement), typeof(BGBlur), new PropertyMetadata(null, OnPropertyChanged));

	public static readonly DependencyProperty VibrancyProperty = DependencyProperty.Register("Vibrancy", typeof(VibrancyLevel), typeof(BGBlur), new PropertyMetadata(VibrancyLevel.Thin, OnPropertyChanged));

	public static readonly DependencyProperty FallbackBackgroundProperty = DependencyProperty.Register("FallbackBackground", typeof(Brush), typeof(BGBlur), new PropertyMetadata(null, OnPropertyChanged));

	public static readonly DependencyProperty IsDarkGrayishProperty = DependencyProperty.Register("IsDarkGrayish", typeof(bool), typeof(BGBlur), new PropertyMetadata(true, OnPropertyChanged));

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private Grid RootGrid;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private bool _contentLoaded;

	public UIElement LayerContent
	{
		get
		{
			return (UIElement)GetValue(LayerContentProperty);
		}
		set
		{
			SetValue(LayerContentProperty, value);
		}
	}

	public VibrancyLevel Vibrancy
	{
		get
		{
			return (VibrancyLevel)GetValue(VibrancyProperty);
		}
		set
		{
			SetValue(VibrancyProperty, value);
		}
	}

	public Brush FallbackBackground
	{
		get
		{
			return (Brush)GetValue(FallbackBackgroundProperty);
		}
		set
		{
			SetValue(FallbackBackgroundProperty, value);
		}
	}

	public bool IsDarkGrayish
	{
		get
		{
			return (bool)GetValue(IsDarkGrayishProperty);
		}
		set
		{
			SetValue(IsDarkGrayishProperty, value);
		}
	}

	private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is BGBlur { IsLoaded: not false } bGBlur)
		{
			bGBlur.BuildLayers();
		}
	}

	public BGBlur()
	{
		InitializeComponent();
		base.Loaded += BlurLayer_Loaded;
		base.Unloaded += BlurLayer_Unloaded;
		_uiSettings = new UISettings();
		_accessibilitySettings = new AccessibilitySettings();
	}

	private void BlurLayer_Loaded(object sender, RoutedEventArgs e)
	{
		base.ActualThemeChanged -= BlurLayer_ActualThemeChanged;
		base.ActualThemeChanged += BlurLayer_ActualThemeChanged;
		PowerManager.EnergySaverStatusChanged -= PowerManager_EnergySaverStatusChanged;
		PowerManager.EnergySaverStatusChanged += PowerManager_EnergySaverStatusChanged;
		_uiSettings.AdvancedEffectsEnabledChanged -= UiSettings_AdvancedEffectsEnabledChanged;
		_uiSettings.AdvancedEffectsEnabledChanged += UiSettings_AdvancedEffectsEnabledChanged;
		BuildLayers();
	}

	private void BlurLayer_Unloaded(object sender, RoutedEventArgs e)
	{
		base.ActualThemeChanged -= BlurLayer_ActualThemeChanged;
		_uiSettings.AdvancedEffectsEnabledChanged -= UiSettings_AdvancedEffectsEnabledChanged;
		PowerManager.EnergySaverStatusChanged -= PowerManager_EnergySaverStatusChanged;
	}

	private void UiSettings_AdvancedEffectsEnabledChanged(UISettings sender, object args)
	{
		BuildLayers();
	}

	private void PowerManager_EnergySaverStatusChanged(object sender, object e)
	{
		BuildLayers();
	}

	private void BlurLayer_ActualThemeChanged(FrameworkElement sender, object args)
	{
		BuildLayers();
	}

	private bool ShouldUseFallback()
	{
		if (IsTransparencyEffectsEnabled() && !IsInHighContrastMode())
		{
			return IsEnergySaver();
		}
		return true;
	}

	private bool IsTransparencyEffectsEnabled()
	{
		return _uiSettings.AdvancedEffectsEnabled;
	}

	private bool IsEnergySaver()
	{
		return PowerManager.EnergySaverStatus == EnergySaverStatus.On;
	}

	private bool IsInHighContrastMode()
	{
		if (_accessibilitySettings != null)
		{
			return _accessibilitySettings.HighContrast;
		}
		return false;
	}

	private void BuildLayers()
	{
		base.DispatcherQueue.TryEnqueue(delegate
		{
			if (ShouldUseFallback())
			{
				CreateFallbackBackground();
			}
			else
			{
				CreateBlurLayer();
			}
		});
	}

	private Border CreateBorder(string resourceKey)
	{
		return new Border
		{
			Background = GetBrushForTheme(resourceKey),
			CornerRadius = base.CornerRadius,
			HorizontalAlignment = HorizontalAlignment.Stretch,
			VerticalAlignment = VerticalAlignment.Stretch
		};
	}

	private Brush GetBrushForTheme(string resourceKey)
	{
		if (base.Resources.ThemeDictionaries.TryGetValue(base.ActualTheme.ToString(), out var value) && value is ResourceDictionary resourceDictionary && resourceDictionary.TryGetValue(resourceKey, out var value2) && value2 is Brush result)
		{
			return result;
		}
		return null;
	}

	private void CreateBlurLayer()
	{
		RootGrid.Children.Clear();
		if (!IsDarkGrayish)
		{
			CreateStandardBlurLayers();
		}
		else
		{
			CreateDarkGrayishBlurLayers();
		}
	}

	private void CreateStandardBlurLayers()
	{
		List<UIElement> list = new List<UIElement> { CreateBorder("BaseLayer") };
		VibrancyLevel vibrancy = Vibrancy;
		string text = default(string);
		switch (vibrancy)
		{
		case VibrancyLevel.Thin:
			text = "GrayishLayerThin";
			break;
		case VibrancyLevel.Regular:
			text = "GrayishLayerRegular";
			break;
		case VibrancyLevel.Thick:
			text = "GrayishLayerThick";
			break;
		default:
			global::_003CPrivateImplementationDetails_003E.ThrowSwitchExpressionException(vibrancy);
			break;
		}
		string resourceKey = text;
		list.Add(CreateBorder(resourceKey));
		if (LayerContent != null)
		{
			list.Add(new ContentPresenter
			{
				Content = LayerContent
			});
		}
		BuildLayerHierarchy(list);
	}

	private void CreateDarkGrayishBlurLayers()
	{
		List<UIElement> list = new List<UIElement>
		{
			CreateBorder("DarkGrayishBaseLayer"),
			CreateBorder("DarkGrayishGrayishLayer")
		};
		if (LayerContent != null)
		{
			list.Add(new ContentPresenter
			{
				Content = LayerContent
			});
		}
		BuildLayerHierarchy(list);
	}

	private void BuildLayerHierarchy(List<UIElement> layers)
	{
		UIElement uIElement = layers[layers.Count - 1];
		for (int num = layers.Count - 2; num >= 0; num--)
		{
			if (layers[num] is Border border)
			{
				border.Child = uIElement;
				uIElement = border;
			}
		}
		RootGrid.Children.Add(uIElement);
	}

	private void CreateFallbackBackground()
	{
		RootGrid.Children.Clear();
		Border border = new Border
		{
			Background = (FallbackBackground ?? new SolidColorBrush(Colors.Transparent)),
			CornerRadius = base.CornerRadius,
			HorizontalAlignment = HorizontalAlignment.Stretch,
			VerticalAlignment = VerticalAlignment.Stretch,
			IsHitTestVisible = true
		};
		if (LayerContent != null)
		{
			border.Child = new ContentPresenter
			{
				Content = LayerContent
			};
		}
		RootGrid.Children.Add(border);
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("ms-appx:///Samsung.OneUI.WinUI/Controls/BGBlurControl/BGBlur.xaml");
			Application.LoadComponent(this, resourceLocator, ComponentResourceLocation.Nested);
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void Connect(int connectionId, object target)
	{
		if (connectionId == 2)
		{
			RootGrid = target.As<Grid>();
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
