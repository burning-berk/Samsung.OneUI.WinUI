using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Markup;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.Foundation;
using WinRT;
using WinRT.Samsung_OneUI_WinUIVtableClasses;

namespace Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker;

[WinRTRuntimeClassName("Microsoft.UI.Xaml.IUIElementOverrides")]
[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_BGBlurWinRTTypeDetails))]
public sealed class ColorPickerTextBox : UserControl, IComponentConnector
{
	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private interface IColorPickerTextBox_Bindings
	{
		void Initialize();

		void Update();

		void StopTracking();

		void DisconnectUnloadedObject(int connectionId);
	}

	private interface IColorPickerTextBox_BindingsScopeConnector
	{
		WeakReference Parent { get; set; }

		bool ContainsElement(int connectionId);

		void RegisterForElementConnection(int connectionId, IComponentConnector connector);
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	private static class XamlBindingSetters
	{
		public static void Set_Microsoft_UI_Xaml_Automation_AutomationProperties_Name(DependencyObject obj, string value, string targetNullValue)
		{
			if (value == null && targetNullValue != null)
			{
				value = targetNullValue;
			}
			AutomationProperties.SetName(obj, value);
		}

		public static void Set_Microsoft_UI_Xaml_FrameworkElement_Style(FrameworkElement obj, Style value, string targetNullValue)
		{
			if (value == null && targetNullValue != null)
			{
				value = (Style)XamlBindingHelper.ConvertValue(typeof(Style), targetNullValue);
			}
			obj.Style = value;
		}

		public static void Set_Microsoft_UI_Xaml_Controls_TextBox_Text(TextBox obj, string value, string targetNullValue)
		{
			if (value == null && targetNullValue != null)
			{
				value = targetNullValue;
			}
			obj.Text = value ?? string.Empty;
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	[WinRTRuntimeClassName("Microsoft.UI.Xaml.Markup.IComponentConnector")]
	[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_Chips_Chips_obj1_BindingsWinRTTypeDetails))]
	private class ColorPickerTextBox_obj1_Bindings : IComponentConnector, IColorPickerTextBox_Bindings
	{
		[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
		[DebuggerNonUserCode]
		private class ColorPickerTextBox_obj1_BindingsTracking
		{
			private WeakReference<ColorPickerTextBox_obj1_Bindings> weakRefToBindingObj;

			private long tokenDPC_IsTextBoxLoaded;

			private long tokenDPC_StringResourceKey;

			private long tokenDPC_Text;

			public ColorPickerTextBox_obj1_BindingsTracking(ColorPickerTextBox_obj1_Bindings obj)
			{
				weakRefToBindingObj = new WeakReference<ColorPickerTextBox_obj1_Bindings>(obj);
			}

			public ColorPickerTextBox_obj1_Bindings TryGetBindingObject()
			{
				ColorPickerTextBox_obj1_Bindings target = null;
				if (weakRefToBindingObj != null)
				{
					weakRefToBindingObj.TryGetTarget(out target);
					if (target == null)
					{
						weakRefToBindingObj = null;
						ReleaseAllListeners();
					}
				}
				return target;
			}

			public void ReleaseAllListeners()
			{
				UpdateChildListeners_(null);
			}

			public void DependencyPropertyChanged_IsTextBoxLoaded(DependencyObject sender, DependencyProperty prop)
			{
				ColorPickerTextBox_obj1_Bindings colorPickerTextBox_obj1_Bindings = TryGetBindingObject();
				if (colorPickerTextBox_obj1_Bindings != null)
				{
					ColorPickerTextBox colorPickerTextBox = sender as ColorPickerTextBox;
					if (colorPickerTextBox != null)
					{
						colorPickerTextBox_obj1_Bindings.Update_IsTextBoxLoaded(colorPickerTextBox.IsTextBoxLoaded, 1073741824);
					}
					colorPickerTextBox_obj1_Bindings.CompleteUpdate(1073741824);
				}
			}

			public void DependencyPropertyChanged_StringResourceKey(DependencyObject sender, DependencyProperty prop)
			{
				ColorPickerTextBox_obj1_Bindings colorPickerTextBox_obj1_Bindings = TryGetBindingObject();
				if (colorPickerTextBox_obj1_Bindings != null)
				{
					ColorPickerTextBox colorPickerTextBox = sender as ColorPickerTextBox;
					if (colorPickerTextBox != null)
					{
						colorPickerTextBox_obj1_Bindings.Update_StringResourceKey(colorPickerTextBox.StringResourceKey, 1073741824);
					}
					colorPickerTextBox_obj1_Bindings.CompleteUpdate(1073741824);
				}
			}

			public void DependencyPropertyChanged_Text(DependencyObject sender, DependencyProperty prop)
			{
				ColorPickerTextBox_obj1_Bindings colorPickerTextBox_obj1_Bindings = TryGetBindingObject();
				if (colorPickerTextBox_obj1_Bindings != null)
				{
					ColorPickerTextBox colorPickerTextBox = sender as ColorPickerTextBox;
					if (colorPickerTextBox != null)
					{
						colorPickerTextBox_obj1_Bindings.Update_Text(colorPickerTextBox.Text, 1073741824);
					}
					colorPickerTextBox_obj1_Bindings.CompleteUpdate(1073741824);
				}
			}

			public void UpdateChildListeners_(ColorPickerTextBox obj)
			{
				ColorPickerTextBox_obj1_Bindings colorPickerTextBox_obj1_Bindings = TryGetBindingObject();
				if (colorPickerTextBox_obj1_Bindings != null)
				{
					if (colorPickerTextBox_obj1_Bindings.dataRoot != null)
					{
						colorPickerTextBox_obj1_Bindings.dataRoot.UnregisterPropertyChangedCallback(IsTextBoxLoadedProperty, tokenDPC_IsTextBoxLoaded);
						colorPickerTextBox_obj1_Bindings.dataRoot.UnregisterPropertyChangedCallback(StringResourceKeyProperty, tokenDPC_StringResourceKey);
						colorPickerTextBox_obj1_Bindings.dataRoot.UnregisterPropertyChangedCallback(TextProperty, tokenDPC_Text);
					}
					if (obj != null)
					{
						colorPickerTextBox_obj1_Bindings.dataRoot = obj;
						tokenDPC_IsTextBoxLoaded = obj.RegisterPropertyChangedCallback(IsTextBoxLoadedProperty, DependencyPropertyChanged_IsTextBoxLoaded);
						tokenDPC_StringResourceKey = obj.RegisterPropertyChangedCallback(StringResourceKeyProperty, DependencyPropertyChanged_StringResourceKey);
						tokenDPC_Text = obj.RegisterPropertyChangedCallback(TextProperty, DependencyPropertyChanged_Text);
					}
				}
			}

			public void RegisterTwoWayListener_2(TextBox sourceObject)
			{
				sourceObject.RegisterPropertyChangedCallback(TextBox.TextProperty, delegate
				{
					TryGetBindingObject()?.UpdateTwoWay_2_Text();
				});
			}
		}

		private ColorPickerTextBox dataRoot;

		private bool initialized;

		private const int NOT_PHASED = int.MinValue;

		private const int DATA_CHANGED = 1073741824;

		private TextBox obj2;

		private bool obj2LoadDeferredValue;

		private string obj2NameDeferredValue;

		private Style obj2StyleDeferredValue;

		private string obj2TextDeferredValue;

		private Queue<int> UnloadedElementsToUpdate = new Queue<int>();

		private ColorPickerTextBox_obj1_BindingsTracking bindingsTracking;

		public ColorPickerTextBox_obj1_Bindings()
		{
			bindingsTracking = new ColorPickerTextBox_obj1_BindingsTracking(this);
		}

		public void Connect(int connectionId, object target)
		{
			if (connectionId == 2)
			{
				obj2 = target.As<TextBox>();
				if (obj2 != null)
				{
					XamlBindingSetters.Set_Microsoft_UI_Xaml_Automation_AutomationProperties_Name(obj2, obj2NameDeferredValue, null);
				}
				if (obj2 != null)
				{
					XamlBindingSetters.Set_Microsoft_UI_Xaml_FrameworkElement_Style(obj2, obj2StyleDeferredValue, null);
				}
				if (obj2 != null)
				{
					XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBox_Text(obj2, obj2TextDeferredValue, null);
				}
				bindingsTracking.RegisterTwoWayListener_2(obj2);
			}
		}

		[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
		[DebuggerNonUserCode]
		public IComponentConnector GetBindingConnector(int connectionId, object target)
		{
			return null;
		}

		public void Initialize()
		{
			if (!initialized)
			{
				Update();
			}
		}

		public void Update()
		{
			Update_(dataRoot, int.MinValue);
			initialized = true;
		}

		public void StopTracking()
		{
			bindingsTracking.ReleaseAllListeners();
			initialized = false;
		}

		public void DisconnectUnloadedObject(int connectionId)
		{
			if (connectionId == 2)
			{
				if (obj2 != null)
				{
					obj2NameDeferredValue = AutomationProperties.GetName(obj2);
					obj2StyleDeferredValue = obj2.Style;
					obj2TextDeferredValue = obj2.Text;
					obj2 = null;
				}
				return;
			}
			throw new ArgumentException("Invalid connectionId.");
		}

		private void UpdateUnloadedElement(int connectionId)
		{
			if (connectionId == 2)
			{
				if (obj2LoadDeferredValue)
				{
					dataRoot.FindName("TextBox");
				}
				else
				{
					dataRoot.UnloadObject(obj2);
				}
				return;
			}
			throw new ArgumentException("Invalid connectionId.");
		}

		public bool SetDataRoot(object newDataRoot)
		{
			bindingsTracking.ReleaseAllListeners();
			if (newDataRoot != null)
			{
				dataRoot = newDataRoot.As<ColorPickerTextBox>();
				return true;
			}
			return false;
		}

		public void Activated(object obj, WindowActivatedEventArgs data)
		{
			Initialize();
		}

		public void Loading(FrameworkElement src, object data)
		{
			Initialize();
		}

		private void CompleteUpdate(int phase)
		{
			while (UnloadedElementsToUpdate.Count > 0)
			{
				UpdateUnloadedElement(UnloadedElementsToUpdate.Dequeue());
			}
		}

		private void Update_(ColorPickerTextBox obj, int phase)
		{
			bindingsTracking.UpdateChildListeners_(obj);
			if (obj != null)
			{
				if ((phase & -1073741823) != 0)
				{
					Update_IsTextBoxLoaded(obj.IsTextBoxLoaded, phase);
					Update_StringResourceKey(obj.StringResourceKey, phase);
				}
				if ((phase & -2147483647) != 0)
				{
					Update_TextBoxStyle(obj.TextBoxStyle, phase);
				}
				if ((phase & -1073741823) != 0)
				{
					Update_Text(obj.Text, phase);
				}
			}
			CompleteUpdate(phase);
		}

		private void Update_IsTextBoxLoaded(bool obj, int phase)
		{
			if ((phase & -1073741823) != 0)
			{
				obj2LoadDeferredValue = obj;
				if (obj)
				{
					dataRoot.FindName("TextBox");
				}
				else
				{
					dataRoot.UnloadObject(obj2);
				}
			}
		}

		private void Update_StringResourceKey(string obj, int phase)
		{
			if ((phase & -1073741823) != 0)
			{
				if (obj2 != null)
				{
					XamlBindingSetters.Set_Microsoft_UI_Xaml_Automation_AutomationProperties_Name(obj2, obj, null);
				}
				else
				{
					obj2NameDeferredValue = obj;
				}
			}
		}

		private void Update_TextBoxStyle(Style obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				if (obj2 != null)
				{
					XamlBindingSetters.Set_Microsoft_UI_Xaml_FrameworkElement_Style(obj2, obj, null);
				}
				else
				{
					obj2StyleDeferredValue = obj;
				}
			}
		}

		private void Update_Text(string obj, int phase)
		{
			if ((phase & -1073741823) != 0)
			{
				if (obj2 != null)
				{
					XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBox_Text(obj2, obj, null);
				}
				else
				{
					obj2TextDeferredValue = obj;
				}
			}
		}

		private void UpdateTwoWay_2_Text()
		{
			if (initialized && dataRoot != null)
			{
				dataRoot.Text = obj2.Text;
			}
		}
	}

	public static readonly DependencyProperty IsTextBoxLoadedProperty = DependencyProperty.Register("IsTextBoxLoaded", typeof(bool), typeof(ColorPickerTextBox), new PropertyMetadata(true));

	public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ColorPickerTextBox), new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty TextBoxStyleProperty = DependencyProperty.Register("TextBoxStyle", typeof(Style), typeof(ColorPickerTextBox), new PropertyMetadata(null));

	public static readonly DependencyProperty StringResourceKeyProperty = DependencyProperty.Register("StringResourceKey", typeof(string), typeof(ColorPickerTextBox), new PropertyMetadata(string.Empty));

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private TextBox TextBox;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private bool _contentLoaded;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private IColorPickerTextBox_Bindings Bindings;

	public bool IsTextBoxLoaded
	{
		get
		{
			return (bool)GetValue(IsTextBoxLoadedProperty);
		}
		set
		{
			SetValue(IsTextBoxLoadedProperty, value);
		}
	}

	public string Text
	{
		get
		{
			return (string)GetValue(TextProperty);
		}
		set
		{
			SetValue(TextProperty, value);
		}
	}

	public Style TextBoxStyle
	{
		get
		{
			return (Style)GetValue(TextBoxStyleProperty);
		}
		set
		{
			SetValue(TextBoxStyleProperty, value);
		}
	}

	public string StringResourceKey
	{
		get
		{
			return (string)GetValue(StringResourceKeyProperty);
		}
		set
		{
			SetValue(StringResourceKeyProperty, value);
		}
	}

	public event RoutedEventHandler TextBoxLostFocus;

	public event TextChangedEventHandler TextChanged;

	public event TypedEventHandler<TextBox, TextBoxBeforeTextChangingEventArgs> BeforeTextChanging;

	public ColorPickerTextBox()
	{
		InitializeComponent();
	}

	private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		this.TextChanged?.Invoke(sender, e);
	}

	private void TextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
	{
		this.BeforeTextChanging?.Invoke(sender, args);
	}

	private void TextBox_LostFocus(object sender, RoutedEventArgs e)
	{
		this.TextBoxLostFocus?.Invoke(sender, e);
	}

	private void TextBox_Loaded(object sender, RoutedEventArgs e)
	{
		if (sender is TextBox element)
		{
			AutomationProperties.SetName(element, StringResourceKey.GetLocalized());
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("ms-appx:///Samsung.OneUI.WinUI/Controls/Inputs/ColorPicker/ColorPickerTextBox.xaml");
			Application.LoadComponent(this, resourceLocator, ComponentResourceLocation.Nested);
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	private void UnloadObject(DependencyObject unloadableObject)
	{
		if (unloadableObject != null)
		{
			if (unloadableObject == TextBox)
			{
				DisconnectUnloadedObject(2);
			}
			XamlMarkupHelper.UnloadObject(unloadableObject);
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void Connect(int connectionId, object target)
	{
		if (connectionId == 2)
		{
			TextBox = target.As<TextBox>();
			TextBox.BeforeTextChanging += TextBox_BeforeTextChanging;
			TextBox.Loaded += TextBox_Loaded;
			TextBox.LostFocus += TextBox_LostFocus;
			TextBox.TextChanged += TextBox_TextChanged;
		}
		_contentLoaded = true;
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	private void DisconnectUnloadedObject(int connectionId)
	{
		if (connectionId == 2)
		{
			Bindings.DisconnectUnloadedObject(2);
			TextBox = null;
			return;
		}
		throw new ArgumentException("Invalid connectionId.");
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public IComponentConnector GetBindingConnector(int connectionId, object target)
	{
		IComponentConnector result = null;
		if (connectionId == 1)
		{
			UserControl obj = (UserControl)target;
			ColorPickerTextBox_obj1_Bindings colorPickerTextBox_obj1_Bindings = new ColorPickerTextBox_obj1_Bindings();
			result = colorPickerTextBox_obj1_Bindings;
			colorPickerTextBox_obj1_Bindings.SetDataRoot(this);
			Bindings = colorPickerTextBox_obj1_Bindings;
			obj.Loading += colorPickerTextBox_obj1_Bindings.Loading;
		}
		return result;
	}
}
