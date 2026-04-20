using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Markup;
using Samsung.OneUI.WinUI.Controls.EventHandlers;
using Samsung.OneUI.WinUI.Services.Animation;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.System;
using WinRT;
using WinRT.Samsung_OneUI_WinUIVtableClasses;

namespace Samsung.OneUI.WinUI.Controls;

[WinRTRuntimeClassName("Microsoft.UI.Xaml.IUIElementOverrides")]
[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_BGBlurWinRTTypeDetails))]
public sealed class Chips : UserControl, IComponentConnector
{
	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private interface IChips_Bindings
	{
		void Initialize();

		void Update();

		void StopTracking();

		void DisconnectUnloadedObject(int connectionId);
	}

	private interface IChips_BindingsScopeConnector
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

		public static void Set_Microsoft_UI_Xaml_Controls_ListViewBase_SelectionMode(ListViewBase obj, ListViewSelectionMode value)
		{
			obj.SelectionMode = value;
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	[WinRTRuntimeClassName("Microsoft.UI.Xaml.Markup.IComponentConnector")]
	[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_Chips_Chips_obj1_BindingsWinRTTypeDetails))]
	private class Chips_obj1_Bindings : IComponentConnector, IChips_Bindings
	{
		[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
		[DebuggerNonUserCode]
		private class Chips_obj1_BindingsTracking
		{
			private WeakReference<Chips_obj1_Bindings> weakRefToBindingObj;

			private long tokenDPC_SelectionState;

			private ObservableCollection<ChipsItem> cache_Items;

			public Chips_obj1_BindingsTracking(Chips_obj1_Bindings obj)
			{
				weakRefToBindingObj = new WeakReference<Chips_obj1_Bindings>(obj);
			}

			public Chips_obj1_Bindings TryGetBindingObject()
			{
				Chips_obj1_Bindings target = null;
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
				UpdateChildListeners_Items(null);
			}

			public void DependencyPropertyChanged_SelectionState(DependencyObject sender, DependencyProperty prop)
			{
				Chips_obj1_Bindings chips_obj1_Bindings = TryGetBindingObject();
				if (chips_obj1_Bindings != null)
				{
					Chips chips = sender as Chips;
					if (chips != null)
					{
						chips_obj1_Bindings.Update_SelectionState(chips.SelectionState, 1073741824);
					}
				}
			}

			public void UpdateChildListeners_(Chips obj)
			{
				Chips_obj1_Bindings chips_obj1_Bindings = TryGetBindingObject();
				if (chips_obj1_Bindings != null)
				{
					if (chips_obj1_Bindings.dataRoot != null)
					{
						chips_obj1_Bindings.dataRoot.UnregisterPropertyChangedCallback(SelectionStateProperty, tokenDPC_SelectionState);
					}
					if (obj != null)
					{
						chips_obj1_Bindings.dataRoot = obj;
						tokenDPC_SelectionState = obj.RegisterPropertyChangedCallback(SelectionStateProperty, DependencyPropertyChanged_SelectionState);
					}
				}
			}

			public void PropertyChanged_Items(object sender, PropertyChangedEventArgs e)
			{
				if (TryGetBindingObject() != null)
				{
					string.IsNullOrEmpty(e.PropertyName);
				}
			}

			public void CollectionChanged_Items(object sender, NotifyCollectionChangedEventArgs e)
			{
				TryGetBindingObject();
			}

			public void UpdateChildListeners_Items(ObservableCollection<ChipsItem> obj)
			{
				if (obj != cache_Items)
				{
					if (cache_Items != null)
					{
						((INotifyPropertyChanged)cache_Items).PropertyChanged -= PropertyChanged_Items;
						((INotifyCollectionChanged)cache_Items).CollectionChanged -= CollectionChanged_Items;
						cache_Items = null;
					}
					if (obj != null)
					{
						cache_Items = obj;
						((INotifyPropertyChanged)obj).PropertyChanged += PropertyChanged_Items;
						((INotifyCollectionChanged)obj).CollectionChanged += CollectionChanged_Items;
					}
				}
			}
		}

		private Chips dataRoot;

		private bool initialized;

		private const int NOT_PHASED = int.MinValue;

		private const int DATA_CHANGED = 1073741824;

		private GridView obj2;

		private Chips_obj1_BindingsTracking bindingsTracking;

		public Chips_obj1_Bindings()
		{
			bindingsTracking = new Chips_obj1_BindingsTracking(this);
		}

		public void Connect(int connectionId, object target)
		{
			if (connectionId == 2)
			{
				obj2 = target.As<GridView>();
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
				dataRoot = newDataRoot.As<Chips>();
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

		private void Update_(Chips obj, int phase)
		{
			bindingsTracking.UpdateChildListeners_(obj);
			if (obj != null && (phase & -1073741823) != 0)
			{
				Update_Items(obj.Items, phase);
				Update_SelectionState(obj.SelectionState, phase);
			}
		}

		private void Update_Items(ObservableCollection<ChipsItem> obj, int phase)
		{
			bindingsTracking.UpdateChildListeners_Items(obj);
			if ((phase & -1073741823) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ItemsControl_ItemsSource(obj2, obj, null);
			}
		}

		private void Update_SelectionState(ListViewSelectionMode obj, int phase)
		{
			if ((phase & -1073741823) != 0)
			{
				XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ListViewBase_SelectionMode(obj2, obj);
			}
		}
	}

	private const string CANCEL_BUTTON_STYLE = "OneUIChipsItemStyleCancelButton";

	private const string MINUS_BUTTON_STYLE = "OneUIChipsItemStyleMinusButton";

	private const string NO_BUTTON_STYLE = "OneUIChipsItemStyleButton";

	private const string CHIP_SELECTOR = "OneUIChipsItemStyleSelector";

	public static readonly DependencyProperty ChipItemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<ChipsItem>), typeof(Chips), new PropertyMetadata(null));

	public static readonly DependencyProperty SelectionStateProperty = DependencyProperty.Register("SelectionState", typeof(ListViewSelectionMode), typeof(Chips), new PropertyMetadata(ListViewSelectionMode.Multiple));

	public static readonly DependencyProperty AllLabelsProperty = DependencyProperty.Register("AllLabels", typeof(ChipsItemGroupTemplate), typeof(Chips), new PropertyMetadata(ChipsItemGroupTemplate.None, OnLabelChanged));

	private readonly IChipsAnimationService _chipsAnimationService;

	private readonly Thickness ChipItemMargin = new Thickness(0.0, 0.0, 0.0, 0.0);

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private GridView ChipsGridView;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private bool _contentLoaded;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private IChips_Bindings Bindings;

	public ObservableCollection<ChipsItem> Items
	{
		get
		{
			return (ObservableCollection<ChipsItem>)GetValue(ChipItemsProperty);
		}
		set
		{
			if (Items != value)
			{
				SetValue(ChipItemsProperty, value);
				if (Items != null)
				{
					Items.CollectionChanged -= ChipItemsChanged;
					Items.CollectionChanged += ChipItemsChanged;
				}
			}
		}
	}

	public ListViewSelectionMode SelectionState
	{
		get
		{
			return (ListViewSelectionMode)GetValue(SelectionStateProperty);
		}
		set
		{
			SetValue(SelectionStateProperty, value);
		}
	}

	public ChipsItemGroupTemplate AllLabels
	{
		get
		{
			return (ChipsItemGroupTemplate)GetValue(AllLabelsProperty);
		}
		set
		{
			SetValue(AllLabelsProperty, value);
		}
	}

	public event EventHandler<ChipsEventArgs> ChipsItemRequested;

	private static void OnLabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is Chips chips)
		{
			chips.UpdateSelectionMode(chips.AllLabels);
		}
	}

	public Chips()
	{
		InitializeComponent();
		base.DefaultStyleKey = typeof(Chips);
		base.Loaded += ChipsLoaded;
		if (ChipsGridView != null)
		{
			ChipsGridView.PreviewKeyDown += GridViewPreviewKeyDown;
		}
		_chipsAnimationService = new ChipsAnimationService();
	}

	public List<object> GetSelectedItems()
	{
		if (ChipsGridView == null)
		{
			return new List<object>();
		}
		return ChipsGridView.SelectedItems.ToList();
	}

	private void UpdateSelectionMode(ChipsItemGroupTemplate ChipLabel)
	{
		Style itemContainerStyle = null;
		StyleSelector itemContainerStyleSelector = null;
		ChipsItemTemplate allItemsLabel = ChipsItemTemplate.Default;
		switch (ChipLabel)
		{
		case ChipsItemGroupTemplate.Default:
			itemContainerStyle = "OneUIChipsItemStyleButton".GetStyle();
			allItemsLabel = ChipsItemTemplate.Default;
			break;
		case ChipsItemGroupTemplate.Cancel:
			itemContainerStyle = "OneUIChipsItemStyleCancelButton".GetStyle();
			allItemsLabel = ChipsItemTemplate.Cancel;
			break;
		case ChipsItemGroupTemplate.Minus:
			itemContainerStyle = "OneUIChipsItemStyleMinusButton".GetStyle();
			allItemsLabel = ChipsItemTemplate.Minus;
			break;
		case ChipsItemGroupTemplate.Tag:
			itemContainerStyle = "OneUIChipsItemStyleButton".GetStyle();
			allItemsLabel = ChipsItemTemplate.Tag;
			break;
		case ChipsItemGroupTemplate.Custom:
			itemContainerStyle = "OneUIChipsItemStyleButton".GetStyle();
			allItemsLabel = ChipsItemTemplate.Custom;
			break;
		default:
			itemContainerStyleSelector = (StyleSelector)"OneUIChipsItemStyleSelector".GetKey();
			break;
		}
		if (!ChipsItemGroupTemplate.None.Equals(ChipLabel))
		{
			SetAllItemsLabel(allItemsLabel);
		}
		ChipsGridView.ItemContainerStyle = itemContainerStyle;
		ChipsGridView.ItemContainerStyleSelector = itemContainerStyleSelector;
		AddEventsChipItem();
	}

	private void ChipsLoaded(object sender, RoutedEventArgs e)
	{
		AddEventsChipItem();
		UpdateSelectionMode(AllLabels);
		UpdateChipsItemMargin();
	}

	private void ChipItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		if (e.Action == NotifyCollectionChangedAction.Add)
		{
			AddEventsChipItem();
			AddAnimation(e.NewItems);
		}
		UpdateChipsItemMargin();
	}

	private void AddAnimation(IList items)
	{
		foreach (object item in items)
		{
			ChipsItem chipsItem = item as ChipsItem;
			if ((object)chipsItem != null)
			{
				chipsItem.Loaded += delegate
				{
					_chipsAnimationService.AddAnimation(chipsItem, chipsItem.GetItemContent());
				};
			}
		}
	}

	private void AddEventsChipItem()
	{
		if (Items == null)
		{
			return;
		}
		foreach (ChipsItem item in Items)
		{
			item.ActionRequest -= ItemActionClicked;
			item.ActionRequest += ItemActionClicked;
		}
	}

	private void ItemActionClicked(object sender, EventArgs e)
	{
		RemoveItem(sender);
	}

	private void RemoveItem(object sender)
	{
		ChipsItem chipsItem = sender as ChipsItem;
		if ((object)chipsItem == null || Items == null)
		{
			return;
		}
		chipsItem.ActionRequest -= ItemActionClicked;
		this.ChipsItemRequested?.Invoke(this, new ChipsEventArgs(ChipsItemAction.Remove, chipsItem, Items.IndexOf(chipsItem)));
		Grid itemContent = chipsItem.GetItemContent();
		if ((object)itemContent != null)
		{
			_chipsAnimationService.RemoveAnimation(chipsItem, itemContent, delegate
			{
				Items?.Remove(chipsItem);
			});
		}
	}

	private void GridViewPreviewKeyDown(object sender, KeyRoutedEventArgs e)
	{
		if (!(sender is GridView gridView) || !IsLeftOrRightKeyDown(e) || !(base.XamlRoot != null))
		{
			return;
		}
		e.Handled = true;
		IEnumerable<ChipsItem> enumerable = gridView.ItemsSource as IEnumerable<ChipsItem>;
		ChipsItem actualGridItem = FocusManager.GetFocusedElement(base.XamlRoot) as ChipsItem;
		int num = enumerable?.ToList().FindIndex((ChipsItem c) => c == actualGridItem) ?? (-1);
		if (num >= 0)
		{
			int num2 = -1;
			if (e.Key == VirtualKey.Left && num > 0)
			{
				num2 = num - 1;
			}
			else if (e.Key == VirtualKey.Right && num < enumerable.Count() - 1)
			{
				num2 = num + 1;
			}
			if (num2 >= 0 && num2 < enumerable.Count())
			{
				FocusManager.TryFocusAsync(enumerable.ElementAt(num2), FocusState.Keyboard);
			}
		}
	}

	private static bool IsLeftOrRightKeyDown(KeyRoutedEventArgs e)
	{
		if (e.Key != VirtualKey.Left)
		{
			return e.Key == VirtualKey.Right;
		}
		return true;
	}

	private void SetAllItemsLabel(ChipsItemTemplate label)
	{
		if (Items == null)
		{
			return;
		}
		foreach (ChipsItem item in Items)
		{
			item.Label = label;
		}
	}

	private void UpdateChipsItemMargin()
	{
		if (Items != null)
		{
			int num = Items.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				Items.ElementAt(i).Margin = ChipItemMargin;
			}
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("ms-appx:///Samsung.OneUI.WinUI/Controls/DataView/Chips/Chips.xaml");
			Application.LoadComponent(this, resourceLocator, ComponentResourceLocation.Nested);
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void Connect(int connectionId, object target)
	{
		if (connectionId == 2)
		{
			ChipsGridView = target.As<GridView>();
		}
		_contentLoaded = true;
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public IComponentConnector GetBindingConnector(int connectionId, object target)
	{
		IComponentConnector result = null;
		if (connectionId == 1)
		{
			UserControl obj = (UserControl)target;
			Chips_obj1_Bindings chips_obj1_Bindings = new Chips_obj1_Bindings();
			result = chips_obj1_Bindings;
			chips_obj1_Bindings.SetDataRoot(this);
			Bindings = chips_obj1_Bindings;
			obj.Loading += chips_obj1_Bindings.Loading;
		}
		return result;
	}
}
