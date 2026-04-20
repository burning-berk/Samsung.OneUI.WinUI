using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.WinUI;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.UI;

namespace Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker;

[TemplatePart(Name = "PART_ColorPickerSwatchedGridView", Type = typeof(GridView))]
internal sealed class ColorPickerSwatched : Microsoft.UI.Xaml.Controls.ColorPicker
{
	private const string COLOR_PICKER_GRIDVIEW_NAME = "PART_ColorPickerSwatchedGridView";

	private const string COLOR_PICKER_SLIDER_ALPHA_NAME = "AlphaSlider";

	private const string COLOR_PICKER_TEXT_SLIDER_ALPHA_NAME = "AlphaTextBox";

	private const string COLOR_PICKER_SLIDER_ALPHA_CONTAINER_NAME = "AlphaSliderContainer";

	private const int Z_INDEX_POINTER_OVER = 101;

	private const int Z_INDEX_SELECTED = 102;

	private const int Z_INDEX_DEFAULT = 1;

	private const int DELAY_SELECTED_COLOR_FIRST_LOAD_IN_MILLISECONDS = 1200;

	private const string OPACITY_STRING_ID = "DREAM_IDLE_OPT_OPACITY/Text";

	private GridView _colorPickerSwatchedGridView;

	private TextBlock _colorPickerTextAlphaSlider;

	private ColorPickerSliderCustom _colorPickerAlphaSlider;

	private ObservableCollection<ColorInfo> _defaultColorList = new ObservableCollection<ColorInfo>();

	public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Brush), typeof(ColorPickerSwatched), new PropertyMetadata(new SolidColorBrush(Colors.Black), OnSelectedColorPropertyChanged));

	public double? AlphaSliderValue { get; set; }

	public bool IsColorPickerAlphaSliderEditable { get; set; }

	public new bool IsAlphaSliderVisible { get; set; }

	public string SelectedColorDescription { get; set; }

	public SolidColorBrush SelectedColor
	{
		get
		{
			return (SolidColorBrush)GetValue(SelectedColorProperty);
		}
		set
		{
			SetValue(SelectedColorProperty, value);
		}
	}

	public event EventHandler<SolidColorBrush> ColorChangedEvent;

	private static void OnSelectedColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ColorPickerSwatched colorPickerSwatched)
		{
			colorPickerSwatched.UpdateColor();
		}
	}

	public ColorPickerSwatched()
	{
		base.DefaultStyleKey = typeof(ColorPickerSwatched);
		base.Unloaded += ColorPickerSwatched_Unloaded;
	}

	public void UpdateProperties()
	{
		DisposeColorPickerSwatched();
		LoadDefaultColorList();
		_colorPickerSwatchedGridView = (ColorPickerSwatchedGridViewCustom)GetTemplateChild("PART_ColorPickerSwatchedGridView");
		if (_colorPickerSwatchedGridView != null)
		{
			_colorPickerSwatchedGridView.ItemsSource = _defaultColorList;
			_colorPickerSwatchedGridView.PointerMoved += ColorPickerSwatchedGridView_PointerMoved;
			_colorPickerSwatchedGridView.Loaded += ColorPickerSwatchedGridView_Loaded;
			_colorPickerSwatchedGridView.SelectionChanged += ColorPickerSwatchedGridView_SelectionChanged;
			AdjustSelectedColorBasedOnListValues();
			SetSelectedIndexGridViewItemFirstLoad();
			HighlightSelectedItem();
			SetFirstItemNarratorMessage();
		}
		_colorPickerTextAlphaSlider = (TextBlock)GetTemplateChild("AlphaTextBox");
		Grid grid = (Grid)GetTemplateChild("AlphaSliderContainer");
		if (grid != null)
		{
			grid.Visibility = ((!IsAlphaSliderVisible) ? Visibility.Collapsed : Visibility.Visible);
		}
		base.ColorChanged += ColorPickerSwatched_ColorChanged;
		_colorPickerAlphaSlider = (ColorPickerSliderCustom)GetTemplateChild("AlphaSlider");
		if (_colorPickerAlphaSlider != null)
		{
			AutomationProperties.SetName(_colorPickerAlphaSlider, ResourceExtensions.GetLocalized("DREAM_IDLE_OPT_OPACITY/Text"));
			if (AlphaSliderValue.HasValue)
			{
				_colorPickerAlphaSlider.Value = AlphaSliderValue.Value;
			}
			UpdateTextAlphaSlider();
			_colorPickerAlphaSlider.ValueChanged += Slider_Alpha_ValueChanged;
			_colorPickerAlphaSlider.IsEnabled = IsColorPickerAlphaSliderEditable;
		}
	}

	public void UpdateSwatchedSelection()
	{
		if (_colorPickerSwatchedGridView != null)
		{
			int item = GetSelectedColor().Item1;
			_colorPickerSwatchedGridView.SelectedIndex = item;
		}
	}

	public (int, ColorInfo) GetSelectedColor()
	{
		int num = 0;
		foreach (ColorInfo item in _colorPickerSwatchedGridView.Items)
		{
			Color color = SelectedColor.Color;
			Color color2 = item.ColorBrush.Color;
			if (color.R == color2.R && color.G == color2.G && color.B == color2.B)
			{
				return (num, item);
			}
			num++;
		}
		return (-1, null);
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		UpdateProperties();
	}

	private void ColorPickerSwatched_Unloaded(object sender, RoutedEventArgs e)
	{
		DisposeColorPickerSwatched();
	}

	private void ColorPickerSwatchedGridView_Loaded(object sender, RoutedEventArgs e)
	{
		HighlightSelectedItem();
	}

	private void ColorPickerSwatchedGridView_PointerMoved(object sender, PointerRoutedEventArgs e)
	{
		GridViewItem currentSelectedGridItem = GetCurrentSelectedGridItem();
		if (e?.OriginalSource is Grid pointerOverItem)
		{
			SetZindexCanvas(currentSelectedGridItem, pointerOverItem);
		}
	}

	private void ColorPickerSwatchedGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		ColorInfo colorInfo = (ColorInfo)_colorPickerSwatchedGridView.SelectedItem;
		if (colorInfo == null || colorInfo.ColorBrush == SelectedColor || _colorPickerSwatchedGridView.SelectedIndex == -1)
		{
			return;
		}
		base.Color = ReapplyOpacityInSelectedColor(colorInfo.ColorBrush.Color);
		GridViewItem currentSelectedGridItem = GetCurrentSelectedGridItem();
		foreach (object item in _colorPickerSwatchedGridView.Items)
		{
			if (_colorPickerSwatchedGridView.ContainerFromItem(item) is GridViewItem element)
			{
				Canvas.SetZIndex(element, 1);
			}
		}
		if (currentSelectedGridItem != null)
		{
			Canvas.SetZIndex(currentSelectedGridItem, 102);
			AutomationProperties.SetName(currentSelectedGridItem, colorInfo.Name);
		}
	}

	private void ColorPickerSwatched_ColorChanged(Microsoft.UI.Xaml.Controls.ColorPicker sender, ColorChangedEventArgs args)
	{
		if (base.Visibility != Visibility.Collapsed)
		{
			UpdateSelectedColor(sender.Color);
			OnColorChanged(SelectedColor);
		}
	}

	private void Slider_Alpha_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
	{
		if (!(e == null) && !(_colorPickerAlphaSlider == null))
		{
			UpdateTextAlphaSlider();
		}
	}

	private void OnColorChanged(SolidColorBrush brush)
	{
		this.ColorChangedEvent?.Invoke(this, brush);
	}

	private void UpdateColor()
	{
		base.Color = SelectedColor.Color;
	}

	private void LoadDefaultColorList()
	{
		if (!_defaultColorList.Any())
		{
			_defaultColorList = new ObservableCollection<ColorInfo>(ColorPickerSwatchedDefaultColors.GetInstance().GetList());
		}
	}

	private void HighlightSelectedItem()
	{
		if (_colorPickerSwatchedGridView == null)
		{
			return;
		}
		GridViewItem currentSelectedGridItem = GetCurrentSelectedGridItem();
		if (currentSelectedGridItem != null)
		{
			Canvas.SetZIndex(currentSelectedGridItem, 102);
			if (currentSelectedGridItem.Content is ColorInfo colorInfo)
			{
				AutomationProperties.SetName(currentSelectedGridItem, colorInfo.Name);
			}
		}
	}

	private void SetZindexCanvas(GridViewItem selectedItem, Grid pointerOverItem)
	{
		foreach (object item in _colorPickerSwatchedGridView.Items)
		{
			GridViewItem gridViewItem = _colorPickerSwatchedGridView.ContainerFromItem(item) as GridViewItem;
			if (IsItemPointerOver(pointerOverItem, gridViewItem))
			{
				Canvas.SetZIndex(gridViewItem, 101);
			}
			else if (IsItemSelected(selectedItem, gridViewItem))
			{
				Canvas.SetZIndex(gridViewItem, 102);
			}
			else
			{
				Canvas.SetZIndex(gridViewItem, 1);
			}
		}
	}

	private bool IsItemPointerOver(Grid pointerOverItem, GridViewItem itemColor)
	{
		if (pointerOverItem == null || itemColor == null)
		{
			return false;
		}
		if (pointerOverItem.Background is SolidColorBrush solidColorBrush && itemColor.Content is ColorInfo colorInfo)
		{
			return colorInfo.ColorBrush.Color == solidColorBrush.Color;
		}
		return false;
	}

	private bool IsItemSelected(GridViewItem itemSelected, GridViewItem itemColor)
	{
		if (itemSelected == null || itemColor == null)
		{
			return false;
		}
		if (itemSelected.Content is ColorInfo colorInfo && itemColor.Content is ColorInfo colorInfo2)
		{
			return colorInfo2.ColorBrush.Color == colorInfo.ColorBrush.Color;
		}
		return false;
	}

	private void UpdateSelectedColor(Color selectedColor)
	{
		SelectedColor = new SolidColorBrush(selectedColor);
		SelectedColorDescription = GetSelectedColorDescription();
	}

	private Color ReapplyOpacityInSelectedColor(Color selectedColor)
	{
		if (_colorPickerAlphaSlider == null)
		{
			return selectedColor;
		}
		return Color.FromArgb((byte)Math.Round(255.0 * _colorPickerAlphaSlider.Value / 100.0, MidpointRounding.AwayFromZero), selectedColor.R, selectedColor.G, selectedColor.B);
	}

	private void UpdateTextAlphaSlider()
	{
		if (_colorPickerTextAlphaSlider != null)
		{
			_colorPickerTextAlphaSlider.Text = $"{Math.Round(_colorPickerAlphaSlider.Value)}";
		}
	}

	private GridViewItem GetCurrentSelectedGridItem()
	{
		return LoadAndReturnGridItemContainer(_colorPickerSwatchedGridView?.SelectedItem);
	}

	private GridViewItem GetCurrentFirstGridItem()
	{
		return LoadAndReturnGridItemContainer(_colorPickerSwatchedGridView?.Items?.FirstOrDefault());
	}

	private GridViewItem LoadAndReturnGridItemContainer(object item)
	{
		if ((object)_colorPickerSwatchedGridView == null || item == null)
		{
			return null;
		}
		_colorPickerSwatchedGridView.ScrollIntoView(item, ScrollIntoViewAlignment.Default);
		return _colorPickerSwatchedGridView.ContainerFromItem(item) as GridViewItem;
	}

	private string GetSelectedColorDescription()
	{
		return GetSelectedColor().Item2?.Description ?? SelectedColorDescription;
	}

	private void SetSelectedIndexGridViewItemFirstLoad()
	{
		ExecuteDelayedAction(1200.0, delegate
		{
			if (_colorPickerSwatchedGridView != null)
			{
				int item = GetSelectedColor().Item1;
				_colorPickerSwatchedGridView.SelectedIndex = item;
			}
		});
	}

	private void ExecuteDelayedAction(double milliseconds, Action action)
	{
		DispatcherTimer dispatcherTimer = new DispatcherTimer();
		dispatcherTimer.Interval = TimeSpan.FromMilliseconds(milliseconds);
		dispatcherTimer.Tick += async delegate(object? sender, object args)
		{
			await base.DispatcherQueue.EnqueueAsync(delegate
			{
				action();
				if (sender is DispatcherTimer dispatcherTimer2)
				{
					dispatcherTimer2.Stop();
				}
			});
		};
		dispatcherTimer.Start();
	}

	private void DisposeColorPickerSwatched()
	{
		if (_colorPickerSwatchedGridView != null)
		{
			_colorPickerSwatchedGridView.PointerMoved -= ColorPickerSwatchedGridView_PointerMoved;
			_colorPickerSwatchedGridView.SelectionChanged -= ColorPickerSwatchedGridView_SelectionChanged;
			_colorPickerSwatchedGridView.Loaded -= ColorPickerSwatchedGridView_Loaded;
			_colorPickerSwatchedGridView.ItemsSource = null;
			_colorPickerSwatchedGridView = null;
		}
		if (_colorPickerAlphaSlider != null)
		{
			_colorPickerAlphaSlider.ValueChanged -= Slider_Alpha_ValueChanged;
			_colorPickerAlphaSlider = null;
		}
		_colorPickerTextAlphaSlider = null;
		base.ColorChanged -= ColorPickerSwatched_ColorChanged;
	}

	private void AdjustSelectedColorBasedOnListValues()
	{
		if (!(_colorPickerSwatchedGridView == null) && _colorPickerSwatchedGridView.ItemsSource == _defaultColorList)
		{
			ColorInfo colorInfo = _defaultColorList.FirstOrDefault((ColorInfo e) => e.ColorBrush.Color == SelectedColor.Color);
			if (colorInfo != null && colorInfo.ColorBrush != null)
			{
				SelectedColor = colorInfo.ColorBrush;
				_colorPickerSwatchedGridView.SelectedIndex = _defaultColorList.IndexOf(colorInfo);
			}
		}
	}

	private void SetFirstItemNarratorMessage()
	{
		GridViewItem currentFirstGridItem = GetCurrentFirstGridItem();
		if ((object)currentFirstGridItem != null && _colorPickerSwatchedGridView?.ItemsSource is Collection<ColorInfo> source)
		{
			AutomationProperties.SetName(currentFirstGridItem, source.FirstOrDefault()?.Name);
		}
	}
}
