using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;
using Samsung.OneUI.WinUI.Controls.Selectors;

namespace Samsung.OneUI.WinUI.Controls;

public class Divider : Control
{
	private const string ROOT_PANEL = "RootPanel";

	private const string TEXT_DIVIDER = "TextDivider";

	private const string STRAIGHT_LINE_NAME = "StraightLine";

	private const string DASH_LINE_NAME = "DashLine";

	private const double DASH_LINE_DEFAULT_LENGTH = 1080.0;

	private Panel _rootPanel;

	private Shape _lineShape;

	private TextBlock _textBlockDivider;

	public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(DividerType), typeof(Divider), new PropertyMetadata(DividerType.Line, OnTypePropertyChanged));

	public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(Divider), new PropertyMetadata(Orientation.Horizontal, OnOrientationPropertyChanged));

	public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register("HeaderText", typeof(string), typeof(Divider), new PropertyMetadata(string.Empty, OnHeaderTextPropertyChanged));

	public DividerType Type
	{
		get
		{
			return (DividerType)GetValue(TypeProperty);
		}
		set
		{
			SetValue(TypeProperty, value);
		}
	}

	public Orientation Orientation
	{
		get
		{
			return (Orientation)GetValue(OrientationProperty);
		}
		set
		{
			SetValue(OrientationProperty, value);
		}
	}

	[Obsolete("This property is deprecated. This property will be removed soon.", false)]
	public string HeaderText
	{
		get
		{
			return (string)GetValue(HeaderTextProperty);
		}
		set
		{
			SetValue(HeaderTextProperty, value);
		}
	}

	public Divider()
	{
		base.DefaultStyleKey = typeof(Divider);
		UpdateStyle();
		base.Loaded += Divider_Loaded;
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_rootPanel = GetTemplateChild("RootPanel") as Panel;
		_textBlockDivider = GetTemplateChild("TextDivider") as TextBlock;
		_lineShape = Type switch
		{
			DividerType.Line => GetTemplateChild("StraightLine") as Shape, 
			DividerType.Dash => GetTemplateChild("DashLine") as Shape, 
			_ => (GetTemplateChild("StraightLine") as Shape) ?? (GetTemplateChild("DashLine") as Shape), 
		};
		HeaderTextChangeVisibility(HeaderText);
		UpdateVisualState(useTransitions: false);
		UpdateLineEndpoint();
	}

	private void UpdateStyle()
	{
		base.Style = new DividerStyleSelector(Type).SelectStyle();
	}

	private void Divider_Loaded(object sender, RoutedEventArgs e)
	{
		UpdateLineEndpoint();
	}

	private static void OnTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is Divider divider)
		{
			if (divider.Orientation == Orientation.Vertical && (DividerType)e.NewValue == DividerType.Dash)
			{
				divider.SetValue(TypeProperty, DividerType.Line);
			}
			else
			{
				divider.UpdateStyle();
			}
		}
	}

	private static void OnOrientationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is Divider divider)
		{
			if ((Orientation)e.NewValue == Orientation.Vertical && divider.Type == DividerType.Dash)
			{
				divider.SetValue(TypeProperty, DividerType.Line);
			}
			divider.UpdateVisualState(useTransitions: true);
			divider.UpdateLineEndpoint();
		}
	}

	private void UpdateVisualState(bool useTransitions)
	{
		if (_rootPanel != null && Type == DividerType.Line)
		{
			VisualStateManager.GoToState(this, Orientation.ToString(), useTransitions);
		}
	}

	private void UpdateLineEndpoint()
	{
		if (!(_lineShape == null) && !(_rootPanel == null) && Orientation == Orientation.Horizontal && _lineShape is Line line)
		{
			line.X2 = ((_rootPanel.ActualWidth > 0.0) ? _rootPanel.ActualWidth : 1080.0);
		}
	}

	private static void OnHeaderTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is Divider divider)
		{
			divider.HeaderTextChangeVisibility(e.NewValue?.ToString());
		}
	}

	private void HeaderTextChangeVisibility(string headerText)
	{
		if (!(_textBlockDivider == null))
		{
			_textBlockDivider.Visibility = ((Orientation == Orientation.Vertical || string.IsNullOrEmpty(headerText)) ? Visibility.Collapsed : Visibility.Visible);
			if (_textBlockDivider.Visibility == Visibility.Visible)
			{
				_textBlockDivider.Text = headerText;
			}
		}
	}
}
