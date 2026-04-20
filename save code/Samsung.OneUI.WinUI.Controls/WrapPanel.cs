using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;
using WinRT;
using WinRT.Samsung_OneUI_WinUIVtableClasses;

namespace Samsung.OneUI.WinUI.Controls;

[WinRTRuntimeClassName("Microsoft.UI.Xaml.IUIElementOverrides")]
[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_WrapPanelWinRTTypeDetails))]
internal class WrapPanel : Panel
{
	private class RectMeasure
	{
		private Rect _region;

		public UIElement Target { get; }

		public Rect Region => _region;

		public double Height => Region.Height;

		public RectMeasure(Rect region, UIElement target)
		{
			_region = region;
			Target = target;
		}

		public void Arrange()
		{
			Target.Arrange(Region);
		}

		public void RepositionY(double y)
		{
			_region.Y = y;
		}
	}

	private class Row
	{
		private readonly List<RectMeasure> _items;

		private double _height = -1.0;

		public IReadOnlyList<RectMeasure> Items => _items;

		public double Height => _height;

		public Row()
		{
			_items = new List<RectMeasure>();
		}

		public void Add(RectMeasure item)
		{
			if (_height < item.Height)
			{
				_height = item.Height;
			}
			_items.Add(item);
		}

		public void RepositionY(double y)
		{
			_items.ForEach(delegate(RectMeasure item)
			{
				item.RepositionY(y);
			});
		}
	}

	private readonly List<Row> _rows = new List<Row>();

	public static readonly DependencyProperty HorizontalSpacingProperty = DependencyProperty.Register("HorizontalSpacing", typeof(double), typeof(WrapPanel), new PropertyMetadata(0.0, ContentPropertyChanged));

	public static readonly DependencyProperty VerticalSpacingProperty = DependencyProperty.Register("VerticalSpacing", typeof(double), typeof(WrapPanel), new PropertyMetadata(0.0, ContentPropertyChanged));

	public double HorizontalSpacing
	{
		get
		{
			return (double)GetValue(HorizontalSpacingProperty);
		}
		set
		{
			SetValue(HorizontalSpacingProperty, value);
		}
	}

	public double VerticalSpacing
	{
		get
		{
			return (double)GetValue(VerticalSpacingProperty);
		}
		set
		{
			SetValue(VerticalSpacingProperty, value);
		}
	}

	private static void ContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is WrapPanel wrapPanel)
		{
			wrapPanel.InvalidateMeasure();
			wrapPanel.InvalidateArrange();
		}
	}

	protected override Size MeasureOverride(Size availableSize)
	{
		_rows.Clear();
		if (!base.Children.Any())
		{
			availableSize.Width = 0.0;
			availableSize.Height = 0.0;
			return availableSize;
		}
		Row row = new Row();
		_rows.Add(row);
		double num = 0.0;
		double y = 0.0;
		foreach (UIElement child in base.Children)
		{
			child.Measure(availableSize);
			Size desiredSize = child.DesiredSize;
			if (desiredSize.Width + num + HorizontalSpacing > availableSize.Width)
			{
				row = new Row();
				_rows.Add(row);
				num = 0.0;
			}
			RectMeasure item = new RectMeasure(new Rect(num, y, desiredSize.Width, desiredSize.Height), child);
			row.Add(item);
			num += desiredSize.Width + HorizontalSpacing;
		}
		if (double.IsInfinity(availableSize.Width))
		{
			availableSize.Width = num;
		}
		double num2 = 0.0;
		double num3 = 0.0;
		for (int i = 0; i < _rows.Count; i++)
		{
			Row row2 = _rows[i];
			num2 += row2.Height;
			if (num3 > 0.0)
			{
				row2.RepositionY(num3);
			}
			num3 += row2.Height + VerticalSpacing;
		}
		availableSize.Height = num2 + (double)(_rows.Count - 1) * VerticalSpacing;
		return availableSize;
	}

	protected override Size ArrangeOverride(Size finalSize)
	{
		_rows.ForEach(delegate(Row row)
		{
			foreach (RectMeasure item in row.Items)
			{
				item.Arrange();
			}
		});
		return finalSize;
	}
}
