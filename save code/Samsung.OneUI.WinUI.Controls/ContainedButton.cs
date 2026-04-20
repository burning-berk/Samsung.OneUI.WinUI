using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls;

public class ContainedButton : ContainedButtonBase
{
	private const string PART_TEXTBLOCK = "PART_Text";

	private const string PROGRESS_CIRCLE_INDETERMINATE = "PART_OneUIProgressCircleIndeterminate";

	private const string LARGE_FONT_SIZE_TOKEN = "OneUISizeLG";

	private const string SMALL_FONT_SIZE_TOKEN = "OneUISizeMS";

	private const string EXTRA_SMALL_FONT_SIZE_TOKEN = "OneUISizeSM";

	private const string XLARGE_CORNER_RADIUS_TOKEN = "OneUICornerRadiusXLarge";

	private const string LARGE_CORNER_RADIUS_TOKEN = "OneUICornerRadiusLarge";

	private const string SEMILARGE_CORNER_RADIUS_TOKEN = "OneUICornerRadiusSemiLarge";

	private const double MAX_VALUE_MINWIDTH_SIZE_LG = 240.0;

	private const double MIN_VALUE_MINWIDTH_SIZE_LG = 120.0;

	public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(ContainedButtonType), typeof(ContainedButton), new PropertyMetadata(ContainedButtonType.Primary, OnTypeChanged));

	public static readonly DependencyProperty SizeProperty = DependencyProperty.Register("Size", typeof(ContainedButtonSize), typeof(ContainedButton), new PropertyMetadata(ContainedButtonSize.Medium, OnSizeChanged));

	private List<ContainedButtonModel> _containedButtonADO;

	private TextBlock _buttonText;

	private ProgressCircleIndeterminate _progressCircleIndeterminate;

	private readonly long _tokenOnMinWidthPropertyChanged;

	private double _initMinWidthValue;

	public ContainedButtonType Type
	{
		get
		{
			return (ContainedButtonType)GetValue(TypeProperty);
		}
		set
		{
			SetValue(TypeProperty, value);
		}
	}

	public ContainedButtonSize Size
	{
		get
		{
			return (ContainedButtonSize)GetValue(SizeProperty);
		}
		set
		{
			SetValue(SizeProperty, value);
		}
	}

	private static void OnTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ContainedButton containedButton)
		{
			containedButton.Style = new ContainedButtonStyleSelector(containedButton.Type).SelectStyle();
		}
	}

	private static void OnSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ContainedButton containedButton)
		{
			containedButton.UpdateContainedButtonProperties();
		}
	}

	public ContainedButton()
	{
		base.Style = new ContainedButtonStyleSelector(Type).SelectStyle();
		CreateContainedButtonBySize();
		base.Unloaded += ContainedButton_Unloaded;
		_tokenOnMinWidthPropertyChanged = RegisterPropertyChangedCallback(FrameworkElement.MinWidthProperty, OnMinWidthChanged);
	}

	protected override void OnApplyTemplate()
	{
		_buttonText = GetTemplateChild("PART_Text") as TextBlock;
		_progressCircleIndeterminate = GetTemplateChild("PART_OneUIProgressCircleIndeterminate") as ProgressCircleIndeterminate;
		_initMinWidthValue = base.MinWidth;
		UpdateContainedButtonProperties();
		if (Size == ContainedButtonSize.Large)
		{
			base.MinWidth = _initMinWidthValue;
		}
		base.OnApplyTemplate();
	}

	private void ContainedButton_Unloaded(object sender, RoutedEventArgs e)
	{
		UnregisterPropertyChangedCallback(FrameworkElement.MinWidthProperty, _tokenOnMinWidthPropertyChanged);
	}

	private void CreateContainedButtonBySize()
	{
		_containedButtonADO = new List<ContainedButtonModel>();
		ContainedButtonModel item = new ContainedButtonModel
		{
			MinHeight = 44.0,
			MinWidth = 240.0,
			FontSize = (double)Application.Current.Resources["OneUISizeLG"],
			Margin = new Thickness(35.0, 12.0, 35.0, 12.0),
			CornerRadius = (CornerRadius)Application.Current.Resources["OneUICornerRadiusXLarge"],
			ProgressCircleSize = ProgressCircleSize.Small,
			Size = ContainedButtonSize.Large
		};
		_containedButtonADO.Add(item);
		ContainedButtonModel item2 = new ContainedButtonModel
		{
			MinHeight = 32.0,
			MinWidth = 73.0,
			FontSize = (double)Application.Current.Resources["OneUISizeMS"],
			Margin = new Thickness(16.0, 8.0, 16.0, 8.0),
			CornerRadius = (CornerRadius)Application.Current.Resources["OneUICornerRadiusLarge"],
			ProgressCircleSize = ProgressCircleSize.Small,
			Size = ContainedButtonSize.Medium
		};
		_containedButtonADO.Add(item2);
		ContainedButtonModel item3 = new ContainedButtonModel
		{
			MinHeight = 28.0,
			MinWidth = 66.0,
			FontSize = (double)Application.Current.Resources["OneUISizeSM"],
			Margin = new Thickness(14.0, 6.0, 14.0, 6.0),
			CornerRadius = (CornerRadius)Application.Current.Resources["OneUICornerRadiusSemiLarge"],
			ProgressCircleSize = ProgressCircleSize.SmallTitle,
			Size = ContainedButtonSize.Small
		};
		_containedButtonADO.Add(item3);
	}

	private void UpdateContainedButtonProperties()
	{
		if (!(_buttonText == null) && !(_progressCircleIndeterminate == null) && _containedButtonADO != null)
		{
			ContainedButtonModel containedButtonModel = _containedButtonADO.FirstOrDefault((ContainedButtonModel x) => x.Size == Size);
			if (containedButtonModel != null)
			{
				base.MinHeight = containedButtonModel.MinHeight;
				base.MinWidth = containedButtonModel.MinWidth;
				base.FontSize = containedButtonModel.FontSize;
				base.CornerRadius = containedButtonModel.CornerRadius;
				_buttonText.Margin = containedButtonModel.Margin;
				_progressCircleIndeterminate.Size = containedButtonModel.ProgressCircleSize;
			}
		}
	}

	private void OnMinWidthChanged(DependencyObject sender, DependencyProperty dp)
	{
		if (sender is ContainedButton { Size: ContainedButtonSize.Large } containedButton)
		{
			base.MinWidth = ClampValue(containedButton.MinWidth, 120.0, 240.0);
		}
	}

	private static double ClampValue(double value, double min, double max)
	{
		return Math.Max(Math.Min(value, max), min);
	}
}
