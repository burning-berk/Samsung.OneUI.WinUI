using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.Foundation;

namespace Samsung.OneUI.WinUI.Controls;

public class ProgressCircleDeterminate : ProgressCircle
{
	private const double RADIANS = Math.PI / 180.0;

	private const string PART_COLOR_GRID_NAME = "PART_ColorGrid";

	private const string PART_TEXT_NAME = "PART_text";

	private const string PART_CANVAS = "PART_canvas";

	private const string PART_OUTER_PATH = "PART_OuterPath";

	private const string PART_INNER_PATH = "PART_InnerPath";

	private const string PART_OUTER_PATH_FIGURE = "PART_OuterPathFigure";

	private const string PART_INNER_PATH_FIGURE = "PART_InnerPathFigure";

	private const string PART_OUTER_ARC_SEGMENT = "PART_OuterArcSegment";

	private const string PART_INNER_ARC_SEGMENT = "PART_InnerArcSegment";

	private const string PART_START_ELLIPSE = "PART_startEllipse";

	private const string PART_END_ELLIPSE = "PART_endEllipse";

	private const string RESOURCE_COLOR_ARC_OUTER = "OneUIProgressCircleDeterminateArcOuter";

	private const string RESOURCE_COLOR_ARC_INNER = "OneUIProgressCircleDeterminateArcInner";

	private const double CIRCLE_CENTER_TO_BORDER_CORRECTION_FACTOR = 0.98;

	private const string PART_TEXT_FONTSIZE_MS = "OneUISizeMS";

	private const string PART_TEXT_FONTSIZE_SM = "OneUISizeSM";

	private const string PART_TEXT_FONTSIZE_XS = "OneUISizeXS";

	private const int GRIDSIZE_XL = 45;

	private const int GRIDSIZE_LG = 30;

	private const int GRIDSIZE_MD = 24;

	private const int GRIDSIZE_SM = 12;

	private const int GRIDSIZE_ST = 8;

	private Canvas _canvas;

	private Path _outerPath;

	private Path _innerPath;

	private PathFigure _outerPathFigure;

	private PathFigure _innerPathFigure;

	private ArcSegment _outerArc;

	private ArcSegment _innerArc;

	private EllipseGeometry _startEllipse;

	private EllipseGeometry _endEllipse;

	private TextBlock _text;

	private readonly List<ProgressCircleDeterminateModel> _progressCircleDeterminateModels = new List<ProgressCircleDeterminateModel>
	{
		new ProgressCircleDeterminateModel
		{
			Type = ProgressCircleDeterminateType.Determinate1,
			Size = ProgressCircleSize.XLarge,
			Orientation = ProgressCircleIndeterminateOrientation.Vertical,
			RadiusSize = 30.0,
			Thickness = 10.0,
			Margin = new Thickness(7.0)
		},
		new ProgressCircleDeterminateModel
		{
			Type = ProgressCircleDeterminateType.Determinate1,
			Size = ProgressCircleSize.Large,
			Orientation = ProgressCircleIndeterminateOrientation.Vertical,
			RadiusSize = 21.0,
			Thickness = 8.0,
			Margin = new Thickness(5.0)
		},
		new ProgressCircleDeterminateModel
		{
			Type = ProgressCircleDeterminateType.Determinate1,
			Size = ProgressCircleSize.Medium,
			Orientation = ProgressCircleIndeterminateOrientation.Vertical,
			RadiusSize = 17.0,
			Thickness = 6.0,
			Margin = new Thickness(4.0)
		},
		new ProgressCircleDeterminateModel
		{
			Type = ProgressCircleDeterminateType.Determinate1,
			Size = ProgressCircleSize.Small,
			Orientation = ProgressCircleIndeterminateOrientation.Horizontal,
			RadiusSize = 8.5,
			Thickness = 3.0,
			Margin = new Thickness(2.0)
		},
		new ProgressCircleDeterminateModel
		{
			Type = ProgressCircleDeterminateType.Determinate1,
			Size = ProgressCircleSize.SmallTitle,
			Orientation = ProgressCircleIndeterminateOrientation.Horizontal,
			RadiusSize = 7.0,
			Thickness = 2.0,
			Margin = new Thickness(1.6)
		},
		new ProgressCircleDeterminateModel
		{
			Type = ProgressCircleDeterminateType.Determinate2,
			Size = ProgressCircleSize.SmallTitle,
			Orientation = ProgressCircleIndeterminateOrientation.Vertical,
			RadiusSize = 31.0,
			Thickness = 6.0,
			Margin = new Thickness(7.0)
		}
	};

	public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(ProgressCircleDeterminate), new PropertyMetadata(0.0, OnPercentValuePropertyChanged));

	public new static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(ProgressCircleDeterminate), new PropertyMetadata((SolidColorBrush)"OneUIProgressCircleDeterminateArcInner".GetKey(), OnForegroundPropertyChanged));

	public new static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(ProgressCircleDeterminate), new PropertyMetadata((SolidColorBrush)"OneUIProgressCircleDeterminateArcOuter".GetKey(), OnBackgroundPropertyChanged));

	public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(ProgressCircleDeterminateType), typeof(ProgressCircleDeterminate), new PropertyMetadata(ProgressCircleDeterminateType.Determinate1, OnTypePropertyChanged));

	public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register("Radius", typeof(double), typeof(ProgressCircleDeterminate), new PropertyMetadata(50.0, OnRadiusOrThicknessPropertyChanged));

	public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register("Thickness", typeof(double), typeof(ProgressCircleDeterminate), new PropertyMetadata(2.0, OnRadiusOrThicknessPropertyChanged));

	public double Value
	{
		get
		{
			return (double)GetValue(ValueProperty);
		}
		set
		{
			SetValue(ValueProperty, value);
		}
	}

	public new Brush Foreground
	{
		get
		{
			return (Brush)GetValue(ForegroundProperty);
		}
		set
		{
			SetValue(ForegroundProperty, value);
		}
	}

	public new Brush Background
	{
		get
		{
			return (Brush)GetValue(BackgroundProperty);
		}
		set
		{
			SetValue(BackgroundProperty, value);
		}
	}

	public ProgressCircleDeterminateType Type
	{
		get
		{
			return (ProgressCircleDeterminateType)GetValue(TypeProperty);
		}
		set
		{
			SetValue(TypeProperty, value);
		}
	}

	internal double Radius
	{
		get
		{
			return (double)GetValue(RadiusProperty);
		}
		set
		{
			SetValue(RadiusProperty, value);
		}
	}

	internal double Thickness
	{
		get
		{
			return (double)GetValue(ThicknessProperty);
		}
		set
		{
			SetValue(ThicknessProperty, value);
		}
	}

	public ProgressCircleDeterminate()
	{
		base.DefaultStyleKey = typeof(ProgressCircleDeterminate);
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		colorGrid = (Grid)GetTemplateChild("PART_ColorGrid");
		_text = (TextBlock)GetTemplateChild("PART_text");
		_canvas = (Canvas)GetTemplateChild("PART_canvas");
		_outerPath = GetTemplateChild("PART_OuterPath") as Path;
		_innerPath = GetTemplateChild("PART_InnerPath") as Path;
		_outerPathFigure = GetTemplateChild("PART_OuterPathFigure") as PathFigure;
		_innerPathFigure = GetTemplateChild("PART_InnerPathFigure") as PathFigure;
		_outerArc = GetTemplateChild("PART_OuterArcSegment") as ArcSegment;
		_innerArc = GetTemplateChild("PART_InnerArcSegment") as ArcSegment;
		_startEllipse = GetTemplateChild("PART_startEllipse") as EllipseGeometry;
		_endEllipse = GetTemplateChild("PART_endEllipse") as EllipseGeometry;
		SetControlSize();
		Draw();
		_innerPath.Stroke = Foreground;
		_outerPath.Stroke = Background;
	}

	protected override void OnSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		UpdateSize();
	}

	private static void OnRadiusOrThicknessPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		ConfigureProgress(d);
	}

	private static void OnPercentValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		ConfigureProgress(d);
	}

	private static void ConfigureProgress(DependencyObject d)
	{
		ProgressCircleDeterminate obj = d as ProgressCircleDeterminate;
		obj.SetControlSize();
		obj.Draw();
	}

	private static void OnForegroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ProgressCircleDeterminate progressCircleDeterminate && progressCircleDeterminate._innerPath != null)
		{
			progressCircleDeterminate._innerPath.Stroke = (Brush)e.NewValue;
		}
	}

	private static void OnBackgroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ProgressCircleDeterminate progressCircleDeterminate && progressCircleDeterminate._outerPath != null)
		{
			progressCircleDeterminate._outerPath.Stroke = (Brush)e.NewValue;
		}
	}

	private void Draw()
	{
		if (!(_canvas == null))
		{
			if (Value == 0.0)
			{
				_innerPath.Visibility = Visibility.Collapsed;
			}
			else
			{
				_innerPath.Visibility = Visibility.Visible;
			}
			GetCircleSegment(GetCenterPoint(), Radius, GetAngle());
		}
	}

	private void SetControlSize()
	{
		if (!(_canvas == null))
		{
			_canvas.Width = Radius * 2.0 + Thickness;
			_canvas.Height = Radius * 2.0 + Thickness;
		}
	}

	private Point GetCenterPoint()
	{
		return new Point(Radius + Thickness / 2.0, Radius + Thickness / 2.0);
	}

	private double GetAngle()
	{
		double num = Value * 0.98 / 100.0 * 360.0;
		if (num >= 360.0)
		{
			num = 359.999;
		}
		return num;
	}

	private void GetCircleSegment(Point centerPoint, double radius, double angle)
	{
		Point point = new Point(centerPoint.X, centerPoint.Y - radius);
		_innerPath.Width = Radius * 2.0 + Thickness;
		_innerPath.Height = Radius * 2.0 + Thickness;
		_innerPath.StrokeThickness = Thickness;
		_outerPath.Width = Radius * 2.0 + Thickness;
		_outerPath.Height = Radius * 2.0 + Thickness;
		_outerPath.StrokeThickness = Thickness;
		_innerPathFigure.StartPoint = point;
		_innerPathFigure.IsClosed = false;
		_outerPathFigure.StartPoint = point;
		_outerPathFigure.IsClosed = false;
		_innerArc.IsLargeArc = angle > 180.0;
		_innerArc.Point = ScaleUnitCirclePoint(centerPoint, angle, radius);
		_innerArc.Size = new Size(radius, radius);
		_innerArc.SweepDirection = SweepDirection.Clockwise;
		_outerArc.IsLargeArc = true;
		_outerArc.Point = ScaleUnitCirclePoint(centerPoint, 359.999, radius);
		_outerArc.Size = new Size(radius, radius);
		_outerArc.SweepDirection = SweepDirection.Clockwise;
		double x = _innerArc.Point.X + Math.Cos(Math.PI / 180.0 * angle);
		double y = _innerArc.Point.Y + Math.Sin(Math.PI / 180.0 * angle);
		_startEllipse.Center = point;
		_startEllipse.RadiusX = 0.25;
		_startEllipse.RadiusY = 0.25;
		_endEllipse.Center = new Point(x, y);
		_endEllipse.RadiusX = 0.25;
		_endEllipse.RadiusY = 0.25;
	}

	private static Point ScaleUnitCirclePoint(Point origin, double angle, double radius)
	{
		return new Point(origin.X + Math.Sin(Math.PI / 180.0 * angle) * radius, origin.Y - Math.Cos(Math.PI / 180.0 * angle) * radius);
	}

	private static void OnTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ProgressCircleDeterminate progressCircleDeterminate)
		{
			progressCircleDeterminate.UpdateSize();
		}
	}

	private void UpdateSize()
	{
		ProgressCircleDeterminateModel progressCircleDeterminateModel = ((Type != ProgressCircleDeterminateType.Determinate2) ? _progressCircleDeterminateModels.FirstOrDefault((ProgressCircleDeterminateModel x) => x.Type == Type && x.Size == base.Size) : _progressCircleDeterminateModels.FirstOrDefault((ProgressCircleDeterminateModel x) => x.Type == Type));
		if (progressCircleDeterminateModel != null)
		{
			Radius = progressCircleDeterminateModel.RadiusSize;
			Thickness = progressCircleDeterminateModel.Thickness;
			SetCanvasMargin(progressCircleDeterminateModel.Margin);
			SetTextAlignment(progressCircleDeterminateModel.Orientation);
			UpdateFontSizeMessage(progressCircleDeterminateModel);
		}
	}

	private void UpdateFontSizeMessage(ProgressCircleDeterminateModel progressDefinition)
	{
		if (progressDefinition.Type == ProgressCircleDeterminateType.Determinate2)
		{
			_text.FontSize = (double)"OneUISizeMS".GetKey();
		}
		else if (!(_text == null))
		{
			double radiusSize = progressDefinition.RadiusSize;
			double num = ((radiusSize == 45.0) ? ((double)"OneUISizeMS".GetKey()) : ((radiusSize == 30.0) ? ((double)"OneUISizeMS".GetKey()) : ((radiusSize == 24.0) ? ((double)"OneUISizeSM".GetKey()) : ((radiusSize == 12.0) ? ((double)"OneUISizeXS".GetKey()) : ((radiusSize != 8.0) ? ((double)"OneUISizeMS".GetKey()) : ((double)"OneUISizeXS".GetKey()))))));
			double fontSize = num;
			_text.FontSize = fontSize;
		}
	}

	private void SetCanvasMargin(Thickness margin)
	{
		if (_canvas != null)
		{
			_canvas.Margin = margin;
		}
	}

	private void SetTextAlignment(ProgressCircleIndeterminateOrientation orientation)
	{
		if (_progressCircleTextAlingmentDictionary.TryGetValue(orientation, out var value))
		{
			VisualStateManager.GoToState(this, value, useTransitions: true);
		}
	}
}
