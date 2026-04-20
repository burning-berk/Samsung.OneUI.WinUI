using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;

namespace Samsung.OneUI.WinUI.Controls;

public class SubHeader : Control
{
	private const string PART_GRID_LINE = "PART_GridLine";

	private const string PART_LINE = "PART_Line";

	private const int HEADER_TEXT_MAX_LENGTH = 24;

	private Grid _gridLine;

	private Line _line;

	public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register("HeaderText", typeof(string), typeof(SubHeader), new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty IsShowDividerProperty = DependencyProperty.Register("IsShowDivider", typeof(bool), typeof(SubHeader), new PropertyMetadata(true, OnIsShowDividerPropertyChanged));

	public string HeaderText
	{
		get
		{
			return (string)GetValue(HeaderTextProperty);
		}
		set
		{
			if (!string.IsNullOrEmpty(value) && value.Length > 24)
			{
				SetValue(HeaderTextProperty, value.Substring(0, 24));
			}
			else
			{
				SetValue(HeaderTextProperty, value);
			}
		}
	}

	public bool IsShowDivider
	{
		get
		{
			return (bool)GetValue(IsShowDividerProperty);
		}
		set
		{
			SetValue(IsShowDividerProperty, value);
		}
	}

	public SubHeader()
	{
		base.DefaultStyleKey = typeof(SubHeader);
		base.Loaded += SubHeader_Loaded;
		base.Unloaded += SubHeader_Unloaded;
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_gridLine = GetTemplateChild("PART_GridLine") as Grid;
		_line = GetTemplateChild("PART_Line") as Line;
		UpdateDividerVisibility(IsShowDivider);
	}

	private static void OnIsShowDividerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is SubHeader subHeader && e.NewValue is bool isShowDivider)
		{
			subHeader.UpdateDividerVisibility(isShowDivider);
		}
	}

	private void UpdateDividerVisibility(bool isShowDivider)
	{
		if (_gridLine != null)
		{
			_gridLine.Visibility = ((!isShowDivider) ? Visibility.Collapsed : Visibility.Visible);
			UpdateLineWidth();
		}
	}

	private void SubHeader_Loaded(object sender, RoutedEventArgs e)
	{
		AddEvents();
	}

	private void SubHeader_Unloaded(object sender, RoutedEventArgs e)
	{
		RemoveEvents();
	}

	private void AddEvents()
	{
		if (base.XamlRoot != null)
		{
			base.XamlRoot.Changed -= OnXamlRootChanged;
			base.XamlRoot.Changed += OnXamlRootChanged;
		}
	}

	private void RemoveEvents()
	{
		if (base.XamlRoot != null)
		{
			base.XamlRoot.Changed -= OnXamlRootChanged;
		}
	}

	private void OnXamlRootChanged(XamlRoot sender, XamlRootChangedEventArgs args)
	{
		base.DispatcherQueue.TryEnqueue(delegate
		{
			UpdateLineWidth();
		});
	}

	private void UpdateLineWidth()
	{
		if (IsShowDivider && !(_line == null) && base.XamlRoot != null && base.XamlRoot.Size.Width > 0.0)
		{
			_line.X2 = base.XamlRoot.Size.Width;
		}
	}
}
