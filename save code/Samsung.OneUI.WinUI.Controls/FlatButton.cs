using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Controls.Selectors;

namespace Samsung.OneUI.WinUI.Controls;

public class FlatButton : FlatButtonBase
{
	private const string TEXT_BLOCK_CONTENT = "PART_Text";

	private const string PROGRESS_CIRCLE_INDETERMINATE = "PART_OneUIProgressCircleIndeterminate";

	private const string LARGE_CORNER_RADIUS_TOKEN = "OneUICornerRadiusLarge";

	private const string MEDIUM_UP_CORNER_RADIUS_TOKEN = "OneUICornerRadiusMediumUp";

	private const string LARGE_FONT_SIZE_TOKEN = "OneUISizeLG";

	private const string SMALL_FONT_SIZE_TOKEN = "OneUISizeMS";

	private List<FlatButtonModel> _flatButtonADO;

	private TextBlock _textContent;

	private ProgressCircleIndeterminate _progressCircleIndeterminate;

	public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(FlatButtonType), typeof(FlatButton), new PropertyMetadata(FlatButtonType.Primary, OnTypeChanged));

	public static readonly DependencyProperty SizeProperty = DependencyProperty.Register("Size", typeof(FlatButtonSize), typeof(FlatButton), new PropertyMetadata(FlatButtonSize.Medium, OnSizeChanged));

	public FlatButtonType Type
	{
		get
		{
			return (FlatButtonType)GetValue(TypeProperty);
		}
		set
		{
			SetValue(TypeProperty, value);
		}
	}

	public FlatButtonSize Size
	{
		get
		{
			return (FlatButtonSize)GetValue(SizeProperty);
		}
		set
		{
			SetValue(SizeProperty, value);
		}
	}

	public FlatButton()
	{
		base.Style = new FlatButtonStyleSelector(Type).SelectStyle();
		CreateFlatButtonBySize();
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_textContent = (TextBlock)GetTemplateChild("PART_Text");
		_progressCircleIndeterminate = (ProgressCircleIndeterminate)GetTemplateChild("PART_OneUIProgressCircleIndeterminate");
		UpdateFlatButtonProperties();
	}

	private static void OnTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is FlatButton flatButton)
		{
			flatButton.Style = new FlatButtonStyleSelector(flatButton.Type).SelectStyle();
		}
	}

	private static void OnSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is FlatButton flatButton)
		{
			flatButton.UpdateFlatButtonProperties();
		}
	}

	private void CreateFlatButtonBySize()
	{
		_flatButtonADO = new List<FlatButtonModel>();
		FlatButtonModel item = new FlatButtonModel
		{
			MinHeight = 32.0,
			FontSize = (double)Application.Current.Resources["OneUISizeLG"],
			Margin = new Thickness(12.0, 6.0, 12.0, 6.0),
			CornerRadius = (CornerRadius)Application.Current.Resources["OneUICornerRadiusLarge"],
			ProgressCircleSize = ProgressCircleSize.Small,
			Size = FlatButtonSize.Medium
		};
		_flatButtonADO.Add(item);
		FlatButtonModel item2 = new FlatButtonModel
		{
			MinHeight = 26.0,
			FontSize = (double)Application.Current.Resources["OneUISizeMS"],
			Margin = new Thickness(10.0, 4.0, 10.0, 4.0),
			CornerRadius = (CornerRadius)Application.Current.Resources["OneUICornerRadiusMediumUp"],
			ProgressCircleSize = ProgressCircleSize.SmallTitle,
			Size = FlatButtonSize.Small
		};
		_flatButtonADO.Add(item2);
	}

	private void UpdateFlatButtonProperties()
	{
		if (!(_textContent == null) && !(_progressCircleIndeterminate == null) && _flatButtonADO != null)
		{
			FlatButtonModel flatButtonModel = _flatButtonADO.FirstOrDefault((FlatButtonModel x) => x.Size == Size);
			if (flatButtonModel != null)
			{
				base.MinHeight = flatButtonModel.MinHeight;
				base.FontSize = flatButtonModel.FontSize;
				base.CornerRadius = flatButtonModel.CornerRadius;
				_textContent.Margin = flatButtonModel.Margin;
				_progressCircleIndeterminate.Size = flatButtonModel.ProgressCircleSize;
			}
		}
	}
}
