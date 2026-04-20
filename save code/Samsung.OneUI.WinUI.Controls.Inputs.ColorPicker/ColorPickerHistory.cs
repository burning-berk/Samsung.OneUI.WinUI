using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.System;
using Windows.UI;

namespace Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker;

internal sealed class ColorPickerHistory : Control
{
	private const string COLOR_GRID_VIEW_NAME = "PART_ColorGridView";

	private const string COLOR_GRID_VIEW_ITEM_VISUAL_STATE_NORMAL = "Normal";

	private const string COLOR_GRID_VIEW_ITEM_VISUAL_STATE_PRESSED = "Pressed";

	private const string ITEM_PANEL_COLOR_PICKER = "ItemPanelColorPicker";

	private const string NO_COLOR_STRING_ID = "DREAM_NO_COLOR_SET_TBOPT";

	private const int MAX_RECENT_COLORS = 6;

	private const string ITEM_SIZE_KEY = "OneUIItemTotalSize";

	private readonly SolidColorBrush TRANSPARENT_COLOR = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

	private ColorPickerHistoryGridViewCustom _colorsGridView;

	public static readonly DependencyProperty ItemColorBackgroundProperty = DependencyProperty.Register("ItemColorBackground", typeof(SolidColorBrush), typeof(ColorPickerHistory), new PropertyMetadata(null));

	public static readonly DependencyProperty RecentColorsProperty = DependencyProperty.Register("RecentColors", typeof(List<ColorInfo>), typeof(ColorPickerHistory), new PropertyMetadata(new ColorInfo[6]));

	public string SelectedColorDescription { get; private set; }

	public SolidColorBrush ItemColorBackground
	{
		get
		{
			return (SolidColorBrush)GetValue(ItemColorBackgroundProperty);
		}
		set
		{
			SetValue(ItemColorBackgroundProperty, value);
		}
	}

	public List<ColorInfo> RecentColors
	{
		get
		{
			return (List<ColorInfo>)GetValue(RecentColorsProperty);
		}
		set
		{
			SetValue(RecentColorsProperty, value);
		}
	}

	public event EventHandler<SolidColorBrush> ColorChangedEvent;

	public ColorPickerHistory()
	{
		base.DefaultStyleKey = typeof(ColorPickerHistory);
		base.Unloaded += ColorList_Unloaded;
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		UpdateProperties();
	}

	public void UpdateProperties()
	{
		InitializeColorGridView();
		if (!(_colorsGridView == null))
		{
			if (RecentColors == null)
			{
				List<ColorInfo> list = (RecentColors = new List<ColorInfo>(6));
			}
			RecentColors = GetConstructedRecentColors(RecentColors);
			RefreshColorGridItemsSource();
		}
	}

	public void InsertColorHistory(ColorInfo selectedColor)
	{
		if (RecentColors != null && _colorsGridView != null)
		{
			RecentColors = GetRecentColorsWithNewColor(selectedColor);
			RefreshColorGridItemsSource();
		}
	}

	private void ColorsGridView_ItemClick(object sender, ItemClickEventArgs e)
	{
		object clickedItem = e.ClickedItem;
		ColorInfo clickedColor = clickedItem as ColorInfo;
		if (clickedColor != null && !TRANSPARENT_COLOR.Color.ToString().Equals(clickedColor.ColorBrush.Color.ToString()))
		{
			ColorInfo selectedColor = RecentColors.Find((ColorInfo a) => a.ColorBrush?.Color.ToString() == clickedColor.ColorBrush.Color.ToString());
			SetSelectedColor(selectedColor);
		}
	}

	private void ColorList_Unloaded(object sender, RoutedEventArgs e)
	{
		DisposeColorGridView();
	}

	private void ColorsGridView_KeyDown(object sender, KeyRoutedEventArgs e)
	{
		HandleActionKeyForGridViewItemFocused(sender, e, "Pressed");
	}

	private void ColorsGridView_KeyUp(object sender, KeyRoutedEventArgs e)
	{
		HandleActionKeyForGridViewItemFocused(sender, e, "Normal");
	}

	private void HandleActionKeyForGridViewItemFocused(object sender, KeyRoutedEventArgs e, string visualState)
	{
		if ((e.Key == VirtualKey.Enter || e.Key == VirtualKey.Space) && sender is FrameworkElement frameworkElement && FocusManager.GetFocusedElement(frameworkElement.XamlRoot) is GridViewItem control)
		{
			VisualStateManager.GoToState(control, visualState, useTransitions: true);
		}
	}

	private void OnColorChanged(SolidColorBrush brush)
	{
		this.ColorChangedEvent?.Invoke(this, brush);
	}

	private void ColorsGridView_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		if (!(sender is ColorPickerHistoryGridViewCustom))
		{
			return;
		}
		base.DispatcherQueue.TryEnqueue(delegate
		{
			StackPanel stackPanel = UIExtensionsInternal.FindChildByName<StackPanel>("ItemPanelColorPicker", _colorsGridView);
			if ((object)stackPanel != null)
			{
				double num = (double)Application.Current.Resources["OneUIItemTotalSize"];
				int count = _colorsGridView.Items.Count;
				double num2 = (double)count * num;
				stackPanel.Spacing = (_colorsGridView.ActualWidth - num2) / (double)(count - 1);
			}
		});
	}

	private void RefreshColorGridItemsSource()
	{
		_colorsGridView.ItemsSource = RecentColors;
	}

	private void SetSelectedColor(ColorInfo selectedColor)
	{
		SolidColorBrush solidColorBrush = ((selectedColor == null) ? ItemColorBackground : selectedColor.ColorBrush);
		if (!TRANSPARENT_COLOR.Equals(solidColorBrush.Color))
		{
			SelectedColorDescription = selectedColor?.Description ?? string.Empty;
			OnColorChanged(solidColorBrush);
		}
	}

	private List<ColorInfo> GetConstructedRecentColors(List<ColorInfo> recentColors)
	{
		int num = 6 - recentColors.Count;
		if (num < 0)
		{
			int num2 = recentColors.Count + num;
			int count = recentColors.Count - num2;
			recentColors.RemoveRange(num2, count);
		}
		else
		{
			for (int i = 0; i < num; i++)
			{
				recentColors.Add(new ColorInfo("DREAM_NO_COLOR_SET_TBOPT".GetLocalized(), TRANSPARENT_COLOR.Color.ToString()));
			}
		}
		return recentColors;
	}

	private List<ColorInfo> GetRecentColorsWithNewColor(ColorInfo selectedColor)
	{
		List<ColorInfo> list = new List<ColorInfo>(RecentColors);
		if (CanInsertToColorList(selectedColor))
		{
			list.Insert(0, selectedColor);
		}
		return GetConstructedRecentColors(list);
	}

	private bool CanInsertToColorList(ColorInfo selectedColor)
	{
		if (RecentColors.All((ColorInfo colorInfo) => colorInfo == null))
		{
			return true;
		}
		Color color = RecentColors.First().ColorBrush.Color;
		Color? obj = selectedColor?.ColorBrush?.Color;
		return !(color == obj);
	}

	private void InitializeColorGridView()
	{
		if (_colorsGridView == null)
		{
			_colorsGridView = (ColorPickerHistoryGridViewCustom)GetTemplateChild("PART_ColorGridView");
		}
		DisposeColorGridView();
		if (_colorsGridView != null)
		{
			_colorsGridView.PreviewKeyDown += ColorsGridView_KeyDown;
			_colorsGridView.KeyUp += ColorsGridView_KeyUp;
			_colorsGridView.ItemClick += ColorsGridView_ItemClick;
			_colorsGridView.SizeChanged += ColorsGridView_SizeChanged;
		}
	}

	private void DisposeColorGridView()
	{
		if (_colorsGridView != null)
		{
			_colorsGridView.PreviewKeyDown -= ColorsGridView_KeyDown;
			_colorsGridView.KeyUp -= ColorsGridView_KeyUp;
			_colorsGridView.ItemClick -= ColorsGridView_ItemClick;
			_colorsGridView.SizeChanged -= ColorsGridView_SizeChanged;
		}
	}
}
