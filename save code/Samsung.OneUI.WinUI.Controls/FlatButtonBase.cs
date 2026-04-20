using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls;

public class FlatButtonBase : Button
{
	private const double DPI_SCALE_150 = 1.5;

	private const double DPI_SCALE_175 = 1.75;

	private const double SIDE_TEXT_MARGIN = 8.0;

	private const double BOTTOM_TEXT_MARGIN_150_DPI = 0.0;

	private const double BOTTOM_TEXT_MARGIN_175_DPI = 1.0;

	private const string TEXT_BLOCK_CONTENT = "PART_Text";

	private const string PROGRESS_ENABLED_VISUAL_STATE = "ProgressEnabled";

	private const string PROGRESS_DISABLED_VISUAL_STATE = "ProgressDisabled";

	private const string DISABLED_VISUAL_STATE = "Disabled";

	private TextBlock _textContent;

	public static readonly DependencyProperty TextTrimmingProperty = DependencyProperty.Register("TextTrimming", typeof(TextTrimming), typeof(FlatButtonBase), new PropertyMetadata(TextTrimming.None));

	public static readonly DependencyProperty MaxTextLinesProperty = DependencyProperty.Register("MaxTextLines", typeof(int), typeof(FlatButtonBase), new PropertyMetadata(0));

	public static readonly DependencyProperty IsProgressEnabledProperty = DependencyProperty.Register("IsProgressEnabled", typeof(bool), typeof(FlatButtonBase), new PropertyMetadata(false, OnIsProgressEnabledChanged));

	public TextTrimming TextTrimming
	{
		get
		{
			return (TextTrimming)GetValue(TextTrimmingProperty);
		}
		set
		{
			SetValue(TextTrimmingProperty, value);
		}
	}

	public int MaxTextLines
	{
		get
		{
			return (int)GetValue(MaxTextLinesProperty);
		}
		set
		{
			SetValue(MaxTextLinesProperty, value);
		}
	}

	public bool IsProgressEnabled
	{
		get
		{
			return (bool)GetValue(IsProgressEnabledProperty);
		}
		set
		{
			SetValue(IsProgressEnabledProperty, value);
		}
	}

	public FlatButtonBase()
	{
		base.IsEnabledChanged += FlatButton_IsEnabledChanged;
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_textContent = (TextBlock)GetTemplateChild("PART_Text");
		AdjustMarginToScreenScale();
	}

	private void AdjustMarginToScreenScale()
	{
		double? num = base.XamlRoot?.RasterizationScale;
		if (!(_textContent == null) && num.HasValue)
		{
			if (num.GetValueOrDefault() == 1.5)
			{
				_textContent.Margin = new Thickness(8.0, 0.0, 8.0, 0.0);
			}
			else if (num.GetValueOrDefault() == 1.75)
			{
				_textContent.Margin = new Thickness(8.0, 0.0, 8.0, 1.0);
			}
		}
	}

	private void FlatButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		ProgressVisualStateChange();
	}

	private static void OnIsProgressEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is FlatButtonBase flatButtonBase)
		{
			flatButtonBase.ProgressVisualStateChange();
		}
	}

	private void ProgressVisualStateChange(bool useTransition = false)
	{
		if (base.IsEnabled && IsProgressEnabled)
		{
			VisualStateManager.GoToState(this, "ProgressEnabled", useTransition);
			base.IsTabStop = false;
		}
		else
		{
			VisualStateManager.GoToState(this, "ProgressDisabled", useTransition);
			base.IsTabStop = true;
		}
		if (!base.IsEnabled)
		{
			VisualStateManager.GoToState(this, "Disabled", useTransition);
		}
	}
}
