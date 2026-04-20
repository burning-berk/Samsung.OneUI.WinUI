using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using CommunityToolkit.WinUI;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Windows.System.Power;
using Windows.UI;
using Windows.UI.ViewManagement;
using WinRT;
using WinRT.Samsung_OneUI_WinUIVtableClasses;

namespace Samsung.OneUI.WinUI.Tokens;

[ContentProperty(Name = "LayerContent")]
[WinRTRuntimeClassName("Microsoft.UI.Xaml.IUIElementOverrides")]
[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_BGBlurWinRTTypeDetails))]
public sealed class BlurLayer : UserControl, IComponentConnector
{
	public const string GRAYISH_LAYER = "GrayishLayer";

	public const string ULTRA_THIN_BASE_LAYER_1 = "UltraThin_BaseLayer1";

	public const string ULTRA_THIN_BASE_LAYER_2 = "UltraThin_BaseLayer2";

	public const string THIN_BASE_LAYER_1 = "Thin_BaseLayer1";

	public const string THIN_BASE_LAYER_2 = "Thin_BaseLayer2";

	public const string REGULAR_BASE_LAYER_1 = "Regular_BaseLayer1";

	public const string REGULAR_BASE_LAYER_2 = "Regular_BaseLayer2";

	public const string THICK_BASE_LAYER_1 = "Thick_BaseLayer1";

	public const string THICK_BASE_LAYER_2 = "Thick_BaseLayer2";

	public const string ULTRA_THICK_BASE_LAYER_1 = "UltraThick_BaseLayer1";

	public const string ULTRA_THICK_BASE_LAYER_2 = "UltraThick_BaseLayer2";

	private const string SUFFIX_BASE_LAYER1 = "_BaseLayer1";

	private const string SUFFIX_BASE_LAYER2 = "_BaseLayer2";

	private const string LIGHT_THEME = "Light";

	private const string DARK_THEME = "Dark";

	private readonly UISettings _uiSettings;

	private readonly AccessibilitySettings _accessibilitySettings;

	private bool? _currentLayerIsFallback;

	public static readonly Color LIGHT_BASE_LAYER_COLOR = Color.FromArgb(byte.MaxValue, 252, 252, byte.MaxValue);

	public static readonly Color DARK_BASE_LAYER_COLOR = Color.FromArgb(byte.MaxValue, 204, 204, 206);

	public static readonly Color LIGHT_ACRYLIC_TINT_COLOR = Color.FromArgb(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	public static readonly Color DARK_ACRYLIC_TINT_COLOR = Color.FromArgb(byte.MaxValue, 23, 23, 23);

	public static readonly Color DARK_GRAYISH_ACRYLIC_TINT_COLOR = Color.FromArgb(byte.MaxValue, 0, 0, 0);

	public static readonly DependencyProperty IsBlurProperty = DependencyProperty.Register("IsBlur", typeof(bool), typeof(BlurLayer), new PropertyMetadata(true, OnPropertyChanged));

	public static readonly DependencyProperty BlurLevelProperty = DependencyProperty.Register("BlurLevel", typeof(BlurLevel), typeof(BlurLayer), new PropertyMetadata(BlurLevel.Thin, OnPropertyChanged));

	public static readonly DependencyProperty VibrancyProperty = DependencyProperty.Register("Vibrancy", typeof(VibrancyLevel), typeof(BlurLayer), new PropertyMetadata(VibrancyLevel.High, OnPropertyChanged));

	public static readonly DependencyProperty LayerContentProperty = DependencyProperty.Register("LayerContent", typeof(UIElement), typeof(BlurLayer), new PropertyMetadata(null, OnPropertyChanged));

	public static readonly DependencyProperty FallbackBackgroundProperty = DependencyProperty.Register("FallbackBackground", typeof(Brush), typeof(BlurLayer), new PropertyMetadata(null, OnPropertyChanged));

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private Grid RootGrid;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private bool _contentLoaded;

	public bool IsBlur
	{
		get
		{
			return (bool)GetValue(IsBlurProperty);
		}
		set
		{
			SetValue(IsBlurProperty, value);
		}
	}

	public BlurLevel BlurLevel
	{
		get
		{
			return (BlurLevel)GetValue(BlurLevelProperty);
		}
		set
		{
			SetValue(BlurLevelProperty, value);
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

	private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is BlurLayer { IsLoaded: not false } blurLayer)
		{
			blurLayer.BuildLayers();
		}
	}

	public BlurLayer()
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
		if (!(_uiSettings == null))
		{
			_uiSettings.AdvancedEffectsEnabledChanged -= UiSettings_AdvancedEffectsEnabledChanged;
			_uiSettings.AdvancedEffectsEnabledChanged += UiSettings_AdvancedEffectsEnabledChanged;
			PowerManager.EnergySaverStatusChanged -= PowerManager_EnergySaverStatusChanged;
			PowerManager.EnergySaverStatusChanged += PowerManager_EnergySaverStatusChanged;
			BuildLayers();
		}
	}

	private void BlurLayer_Unloaded(object sender, RoutedEventArgs e)
	{
		base.ActualThemeChanged -= BlurLayer_ActualThemeChanged;
		PowerManager.EnergySaverStatusChanged -= PowerManager_EnergySaverStatusChanged;
		if (_uiSettings != null)
		{
			_uiSettings.AdvancedEffectsEnabledChanged -= UiSettings_AdvancedEffectsEnabledChanged;
		}
	}

	private async void UiSettings_AdvancedEffectsEnabledChanged(UISettings sender, object args)
	{
		await base.DispatcherQueue.EnqueueAsync(delegate
		{
			bool num = ShouldUseFallback();
			if (_currentLayerIsFallback != num)
			{
				BuildLayers();
			}
		});
	}

	private async void BlurLayer_ActualThemeChanged(FrameworkElement sender, object args)
	{
		await base.DispatcherQueue.EnqueueAsync(delegate
		{
			BuildLayers();
		});
	}

	private async void PowerManager_EnergySaverStatusChanged(object sender, object e)
	{
		await base.DispatcherQueue.EnqueueAsync(delegate
		{
			BuildLayers();
		});
	}

	private void InitResources()
	{
		ResourceDictionary resourceDictionary = (base.Resources = base.Resources ?? new ResourceDictionary());
		Color fallbackColor = ((FallbackBackground != null) ? ((SolidColorBrush)FallbackBackground).Color : GetDefaultFallbackColor());
		IDictionary<object, object> themeDictionaries = resourceDictionary.ThemeDictionaries;
		ResourceDictionary resourceDictionary3;
		if (themeDictionaries.ContainsKey("Light"))
		{
			resourceDictionary3 = (ResourceDictionary)themeDictionaries["Light"];
			resourceDictionary3.Clear();
		}
		else
		{
			resourceDictionary3 = new ResourceDictionary();
			themeDictionaries.Add("Light", resourceDictionary3);
		}
		ResourceDictionary resourceDictionary4;
		if (themeDictionaries.ContainsKey("Dark"))
		{
			resourceDictionary4 = (ResourceDictionary)themeDictionaries["Dark"];
			resourceDictionary4.Clear();
		}
		else
		{
			resourceDictionary4 = new ResourceDictionary();
			themeDictionaries.Add("Dark", resourceDictionary4);
		}
		CreateGrayishLayerResources(resourceDictionary3, resourceDictionary4, fallbackColor);
		CreateUltraThinBaseLayerResources(resourceDictionary3, resourceDictionary4, fallbackColor);
		CreateThinBaseLayerResources(resourceDictionary3, resourceDictionary4, fallbackColor);
		CreateRegularBaseLayerResources(resourceDictionary3, resourceDictionary4, fallbackColor);
		CreateThickBaseLayerResources(resourceDictionary3, resourceDictionary4, fallbackColor);
		CreateUltraThickBaseLayerResources(resourceDictionary3, resourceDictionary4, fallbackColor);
	}

	private void CreateGrayishLayerResources(ResourceDictionary light, ResourceDictionary dark, Color fallbackColor)
	{
		light["GrayishLayer"] = CreateAcrylicBrush(LIGHT_ACRYLIC_TINT_COLOR, 0.04, 0, fallbackColor);
		dark["GrayishLayer"] = CreateAcrylicBrush(DARK_GRAYISH_ACRYLIC_TINT_COLOR, 0.04, 0, fallbackColor);
	}

	private void CreateUltraThickBaseLayerResources(ResourceDictionary light, ResourceDictionary dark, Color fallbackColor)
	{
		light["UltraThick_BaseLayer1"] = CreateSolidColorBrush(LIGHT_BASE_LAYER_COLOR, 0.02);
		light["UltraThick_BaseLayer2"] = CreateAcrylicBrush(LIGHT_ACRYLIC_TINT_COLOR, 0.8, 0, fallbackColor);
		dark["UltraThick_BaseLayer1"] = CreateSolidColorBrush(DARK_BASE_LAYER_COLOR, 0.02);
		dark["UltraThick_BaseLayer2"] = CreateAcrylicBrush(DARK_ACRYLIC_TINT_COLOR, 0.8, 0, fallbackColor);
	}

	private void CreateThickBaseLayerResources(ResourceDictionary light, ResourceDictionary dark, Color fallbackColor)
	{
		light["Thick_BaseLayer1"] = CreateSolidColorBrush(LIGHT_BASE_LAYER_COLOR, 0.03);
		light["Thick_BaseLayer2"] = CreateAcrylicBrush(LIGHT_ACRYLIC_TINT_COLOR, 0.7, 0, fallbackColor);
		dark["Thick_BaseLayer1"] = CreateSolidColorBrush(DARK_BASE_LAYER_COLOR, 0.03);
		dark["Thick_BaseLayer2"] = CreateAcrylicBrush(DARK_ACRYLIC_TINT_COLOR, 0.7, 0, fallbackColor);
	}

	private void CreateRegularBaseLayerResources(ResourceDictionary light, ResourceDictionary dark, Color fallbackColor)
	{
		light["Regular_BaseLayer1"] = CreateSolidColorBrush(LIGHT_BASE_LAYER_COLOR, 0.04);
		light["Regular_BaseLayer2"] = CreateAcrylicBrush(LIGHT_ACRYLIC_TINT_COLOR, 0.6, 0, fallbackColor);
		dark["Regular_BaseLayer1"] = CreateSolidColorBrush(DARK_BASE_LAYER_COLOR, 0.04);
		dark["Regular_BaseLayer2"] = CreateAcrylicBrush(DARK_ACRYLIC_TINT_COLOR, 0.6, 0, fallbackColor);
	}

	private void CreateThinBaseLayerResources(ResourceDictionary light, ResourceDictionary dark, Color fallbackColor)
	{
		light["Thin_BaseLayer1"] = CreateSolidColorBrush(LIGHT_BASE_LAYER_COLOR, 0.05);
		light["Thin_BaseLayer2"] = CreateAcrylicBrush(LIGHT_ACRYLIC_TINT_COLOR, 0.5, 0, fallbackColor);
		dark["Thin_BaseLayer1"] = CreateSolidColorBrush(DARK_BASE_LAYER_COLOR, 0.05);
		dark["Thin_BaseLayer2"] = CreateAcrylicBrush(DARK_ACRYLIC_TINT_COLOR, 0.5, 0, fallbackColor);
	}

	private void CreateUltraThinBaseLayerResources(ResourceDictionary light, ResourceDictionary dark, Color fallbackColor)
	{
		light["UltraThin_BaseLayer1"] = CreateSolidColorBrush(LIGHT_BASE_LAYER_COLOR, 0.06);
		light["UltraThin_BaseLayer2"] = CreateAcrylicBrush(LIGHT_ACRYLIC_TINT_COLOR, 0.4, 0, fallbackColor);
		dark["UltraThin_BaseLayer1"] = CreateSolidColorBrush(DARK_BASE_LAYER_COLOR, 0.06);
		dark["UltraThin_BaseLayer2"] = CreateAcrylicBrush(DARK_ACRYLIC_TINT_COLOR, 0.4, 0, fallbackColor);
	}

	private AcrylicBrush CreateAcrylicBrush(Color tintColor, double tintLuminosityOpacity, int tintOpacity, Color fallbackColor)
	{
		return new AcrylicBrush
		{
			TintColor = tintColor,
			TintLuminosityOpacity = tintLuminosityOpacity,
			TintOpacity = tintOpacity,
			FallbackColor = fallbackColor
		};
	}

	private SolidColorBrush CreateSolidColorBrush(Color color, double opacity)
	{
		return new SolidColorBrush
		{
			Color = color,
			Opacity = opacity
		};
	}

	private void BuildLayers()
	{
		bool flag = ShouldUseFallback();
		if (flag)
		{
			CreateFallbackBackground();
		}
		else
		{
			InitResources();
			CreateBlurLayer();
		}
		_currentLayerIsFallback = flag;
	}

	private void CreateBlurLayer()
	{
		RootGrid.Children.Clear();
		string text = BlurLevel.ToString();
		List<UIElement> list = new List<UIElement>();
		list.Add(CreateBorder(text + "_BaseLayer1"));
		list.Add(CreateBorder(text + "_BaseLayer2"));
		int num = Vibrancy switch
		{
			VibrancyLevel.High => 0, 
			VibrancyLevel.Medium => 1, 
			VibrancyLevel.Low => 2, 
			_ => 0, 
		};
		for (int i = 0; i < num; i++)
		{
			list.Add(CreateBorder("GrayishLayer"));
		}
		if (LayerContent != null)
		{
			list.Add(new ContentPresenter
			{
				Content = LayerContent
			});
		}
		UIElement uIElement = list[list.Count - 1];
		for (int num2 = list.Count - 2; num2 >= 0; num2--)
		{
			if (list[num2] is Border border)
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

	private Border CreateBorder(string resourceKey)
	{
		Brush brushForTheme = GetBrushForTheme(resourceKey);
		return new Border
		{
			Background = brushForTheme,
			CornerRadius = base.CornerRadius,
			HorizontalAlignment = HorizontalAlignment.Stretch,
			VerticalAlignment = VerticalAlignment.Stretch
		};
	}

	private Brush GetBrushForTheme(string resourceKey, string themeMode = null)
	{
		if (string.IsNullOrEmpty(resourceKey))
		{
			return null;
		}
		if (base.Resources.ThemeDictionaries.TryGetValue(themeMode ?? base.ActualTheme.ToString(), out var value) && value is ResourceDictionary resourceDictionary && resourceDictionary.TryGetValue(resourceKey, out var value2) && value2 is Brush result)
		{
			return result;
		}
		return null;
	}

	private bool ShouldUseFallback()
	{
		if (IsTransparencyEffectsEnabled() && !IsInHighContrastMode())
		{
			return !IsBlur;
		}
		return true;
	}

	private bool IsTransparencyEffectsEnabled()
	{
		return _uiSettings.AdvancedEffectsEnabled;
	}

	private bool IsInHighContrastMode()
	{
		if (_accessibilitySettings != null)
		{
			return _accessibilitySettings.HighContrast;
		}
		return false;
	}

	private Color GetDefaultFallbackColor()
	{
		if (base.ActualTheme != ElementTheme.Light)
		{
			return Colors.Black;
		}
		return Colors.White;
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("ms-appx:///Samsung.OneUI.WinUI/Tokens/Blur/BlurLayer.xaml");
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
