using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Controls.EventHandlers;
using WinRT;
using WinRT.Samsung_OneUI_WinUIVtableClasses;

namespace Samsung.OneUI.WinUI.Controls;

[WinRTRuntimeClassName("Microsoft.UI.Xaml.IUIElementOverrides")]
[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_BGBlurWinRTTypeDetails))]
public sealed class CardTypeListView : UserControl, IComponentConnector
{
	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private interface ICardTypeListView_Bindings
	{
		void Initialize();

		void Update();

		void StopTracking();

		void DisconnectUnloadedObject(int connectionId);
	}

	private interface ICardTypeListView_BindingsScopeConnector
	{
		WeakReference Parent { get; set; }

		bool ContainsElement(int connectionId);

		void RegisterForElementConnection(int connectionId, IComponentConnector connector);
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	private static class XamlBindingSetters
	{
		public static void Set_Microsoft_UI_Xaml_Controls_ItemsControl_ItemsSource(ItemsControl obj, object value, string targetNullValue)
		{
			if (value == null && targetNullValue != null)
			{
				value = XamlBindingHelper.ConvertValue(typeof(object), targetNullValue);
			}
			obj.ItemsSource = value;
		}

		public static void Set_Samsung_OneUI_WinUI_Controls_CardType_Title(CardType obj, string value, string targetNullValue)
		{
			if (value == null && targetNullValue != null)
			{
				value = targetNullValue;
			}
			obj.Title = value ?? string.Empty;
		}

		public static void Set_Samsung_OneUI_WinUI_Controls_CardType_ButtonText(CardType obj, string value, string targetNullValue)
		{
			if (value == null && targetNullValue != null)
			{
				value = targetNullValue;
			}
			obj.ButtonText = value ?? string.Empty;
		}

		public static void Set_Samsung_OneUI_WinUI_Controls_CardType_Description(CardType obj, string value, string targetNullValue)
		{
			if (value == null && targetNullValue != null)
			{
				value = targetNullValue;
			}
			obj.Description = value ?? string.Empty;
		}

		public static void Set_Samsung_OneUI_WinUI_Controls_CardType_Image(CardType obj, ImageSource value, string targetNullValue)
		{
			if (value == null && targetNullValue != null)
			{
				value = (ImageSource)XamlBindingHelper.ConvertValue(typeof(ImageSource), targetNullValue);
			}
			obj.Image = value;
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	[WinRTRuntimeClassName("Microsoft.UI.Xaml.IDataTemplateExtension")]
	[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_CardTypeListView_CardTypeListView_obj7_BindingsWinRTTypeDetails))]
	private class CardTypeListView_obj7_Bindings : IDataTemplateExtension, IDataTemplateComponent, IComponentConnector, ICardTypeListView_Bindings
	{
		private CardTypeItem dataRoot;

		private bool initialized;

		private const int NOT_PHASED = int.MinValue;

		private const int DATA_CHANGED = 1073741824;

		private bool removedDataContextHandler;

		private WeakReference obj7;

		private CardType obj9;

		private EventHandler<RoutedEventArgs> obj9Click;

		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 7:
				obj7 = new WeakReference(target.As<UserControl>());
				break;
			case 9:
				obj9 = target.As<CardType>();
				obj9Click = delegate(object p0, RoutedEventArgs p1)
				{
					if (dataRoot != null)
					{
						dataRoot.ButtonRoutedEvent(p0, p1);
					}
				};
				target.As<CardType>().Click += obj9Click;
				break;
			}
		}

		[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
		[DebuggerNonUserCode]
		public IComponentConnector GetBindingConnector(int connectionId, object target)
		{
			return null;
		}

		public void DataContextChangedHandler(FrameworkElement sender, DataContextChangedEventArgs args)
		{
			if (SetDataRoot(args.NewValue))
			{
				Update();
			}
		}

		public bool ProcessBinding(uint phase)
		{
			throw new NotImplementedException();
		}

		public int ProcessBindings(ContainerContentChangingEventArgs args)
		{
			int nextPhase = -1;
			ProcessBindings(args.Item, args.ItemIndex, (int)args.Phase, out nextPhase);
			return nextPhase;
		}

		public void ResetTemplate()
		{
			Recycle();
		}

		public void ProcessBindings(object item, int itemIndex, int phase, out int nextPhase)
		{
			nextPhase = -1;
			if (phase == 0)
			{
				nextPhase = -1;
				SetDataRoot(item);
				if (!removedDataContextHandler)
				{
					removedDataContextHandler = true;
					UserControl userControl = obj7.Target as UserControl;
					if (userControl != null)
					{
						userControl.DataContextChanged -= DataContextChangedHandler;
					}
				}
				initialized = true;
			}
			Update_(item.As<CardTypeItem>(), 1 << phase);
		}

		public void Recycle()
		{
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
		}

		public void DisconnectUnloadedObject(int connectionId)
		{
			throw new ArgumentException("No unloadable elements to disconnect.");
		}

		public bool SetDataRoot(object newDataRoot)
		{
			if (newDataRoot != null)
			{
				dataRoot = newDataRoot.As<CardTypeItem>();
				return true;
			}
			return false;
		}

		private void Update_(CardTypeItem obj, int phase)
		{
			if (obj != null && (phase & -2147483647) != 0)
			{
				Update_Title(obj.Title, phase);
				Update_ButtonText(obj.ButtonText, phase);
				Update_Description(obj.Description, phase);
				Update_Image(obj.Image, phase);
			}
		}

		private void Update_Title(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Samsung_OneUI_WinUI_Controls_CardType_Title(obj9, obj, null);
			}
		}

		private void Update_ButtonText(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Samsung_OneUI_WinUI_Controls_CardType_ButtonText(obj9, obj, null);
			}
		}

		private void Update_Description(string obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Samsung_OneUI_WinUI_Controls_CardType_Description(obj9, obj, null);
			}
		}

		private void Update_Image(ImageSource obj, int phase)
		{
			if ((phase & -2147483647) != 0)
			{
				XamlBindingSetters.Set_Samsung_OneUI_WinUI_Controls_CardType_Image(obj9, obj, null);
			}
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	[WinRTRuntimeClassName("Microsoft.UI.Xaml.Markup.IComponentConnector")]
	[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_Chips_Chips_obj1_BindingsWinRTTypeDetails))]
	private class CardTypeListView_obj1_Bindings : IComponentConnector, ICardTypeListView_Bindings
	{
		[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
		[DebuggerNonUserCode]
		private class CardTypeListView_obj1_BindingsTracking
		{
			private WeakReference<CardTypeListView_obj1_Bindings> weakRefToBindingObj;

			public CardTypeListView_obj1_BindingsTracking(CardTypeListView_obj1_Bindings obj)
			{
				weakRefToBindingObj = new WeakReference<CardTypeListView_obj1_Bindings>(obj);
			}

			public CardTypeListView_obj1_Bindings TryGetBindingObject()
			{
				CardTypeListView_obj1_Bindings target = null;
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
			}
		}

		private CardTypeListView dataRoot;

		private bool initialized;

		private const int NOT_PHASED = int.MinValue;

		private const int DATA_CHANGED = 1073741824;

		private ListView obj10;

		private CardTypeListView_obj1_BindingsTracking bindingsTracking;

		public CardTypeListView_obj1_Bindings()
		{
			bindingsTracking = new CardTypeListView_obj1_BindingsTracking(this);
		}

		public void Connect(int connectionId, object target)
		{
			if (connectionId == 10)
			{
				obj10 = target.As<ListView>();
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
			throw new ArgumentException("No unloadable elements to disconnect.");
		}

		public bool SetDataRoot(object newDataRoot)
		{
			bindingsTracking.ReleaseAllListeners();
			if (newDataRoot != null)
			{
				dataRoot = newDataRoot.As<CardTypeListView>();
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

		private void Update_(CardTypeListView obj, int phase)
		{
			if (obj != null && (phase & -1073741823) != 0)
			{
				Update_ItemsSource(obj.ItemsSource, phase);
			}
		}

		private void Update_ItemsSource(List<CardTypeItem> obj, int phase)
		{
			if ((phase & -1073741823) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ItemsControl_ItemsSource(obj10, obj, null);
			}
		}
	}

	public static readonly DependencyProperty CardItemListProperty = DependencyProperty.Register("ItemsSource", typeof(List<CardTypeItem>), typeof(CardTypeListView), new PropertyMetadata(null));

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private DataTemplate OneUICardTypeItemCustomTemplate;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private bool _contentLoaded;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private ICardTypeListView_Bindings Bindings;

	public List<CardTypeItem> ItemsSource
	{
		get
		{
			return (List<CardTypeItem>)GetValue(CardItemListProperty);
		}
		set
		{
			SetValue(CardItemListProperty, value);
		}
	}

	public event EventHandler<CardTypeEventArgs> CardRequested;

	public CardTypeListView()
	{
		InitializeComponent();
		base.Loaded += CardTypeList_Loaded;
	}

	private void CardTypeList_Loaded(object sender, RoutedEventArgs e)
	{
		AddEventsCardItem();
	}

	private void AddEventsCardItem()
	{
		if (ItemsSource == null)
		{
			return;
		}
		foreach (CardTypeItem item in ItemsSource)
		{
			item.Click_Event = (EventHandler)Delegate.Remove(item.Click_Event, new EventHandler(ItemActionClicked));
			item.Click_Event = (EventHandler)Delegate.Combine(item.Click_Event, new EventHandler(ItemActionClicked));
		}
	}

	private void ItemActionClicked(object sender, EventArgs e)
	{
		if (sender is CardTypeItem cardTypeItem && ItemsSource != null)
		{
			this.CardRequested?.Invoke(this, new CardTypeEventArgs(cardTypeItem, ItemsSource.IndexOf(cardTypeItem)));
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("ms-appx:///Samsung.OneUI.WinUI/Controls/DataView/CardType/Views/CardTypeListView.xaml");
			Application.LoadComponent(this, resourceLocator, ComponentResourceLocation.Nested);
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void Connect(int connectionId, object target)
	{
		if (connectionId == 2)
		{
			OneUICardTypeItemCustomTemplate = target.As<DataTemplate>();
		}
		_contentLoaded = true;
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public IComponentConnector GetBindingConnector(int connectionId, object target)
	{
		IComponentConnector result = null;
		switch (connectionId)
		{
		case 1:
		{
			UserControl obj = (UserControl)target;
			CardTypeListView_obj1_Bindings cardTypeListView_obj1_Bindings = new CardTypeListView_obj1_Bindings();
			result = cardTypeListView_obj1_Bindings;
			cardTypeListView_obj1_Bindings.SetDataRoot(this);
			Bindings = cardTypeListView_obj1_Bindings;
			obj.Loading += cardTypeListView_obj1_Bindings.Loading;
			break;
		}
		case 7:
		{
			UserControl userControl = (UserControl)target;
			CardTypeListView_obj7_Bindings cardTypeListView_obj7_Bindings = new CardTypeListView_obj7_Bindings();
			result = cardTypeListView_obj7_Bindings;
			cardTypeListView_obj7_Bindings.SetDataRoot(userControl.DataContext);
			userControl.DataContextChanged += cardTypeListView_obj7_Bindings.DataContextChangedHandler;
			DataTemplate.SetExtensionInstance(userControl, cardTypeListView_obj7_Bindings);
			XamlBindingHelper.SetDataTemplateComponent(userControl, cardTypeListView_obj7_Bindings);
			break;
		}
		}
		return result;
	}
}
