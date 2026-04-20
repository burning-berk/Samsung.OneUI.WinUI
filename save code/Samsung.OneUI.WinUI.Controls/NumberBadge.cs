using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Controls.EventHandlers;

namespace Samsung.OneUI.WinUI.Controls;

public class NumberBadge : BadgeBase
{
	private const int MAX_NUM = 1000;

	private const string MAX_NOTIFICATION = "999+";

	private const string PART_BORDER_NUM = "TextBlockBadgeValue";

	private const int NUMBER_BADGE_ONE_DIGIT_WIDTH = 16;

	private const int NUMBER_BADGE_TWO_DIGITS_WIDTH = 26;

	private const int NUMBER_BADGE_MORE_DIGITS_WIDTH = 31;

	private TextBlock _borderNum;

	public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(NumberBadge), new PropertyMetadata(0, OnValuePropertyChanged));

	public int Value
	{
		get
		{
			return (int)GetValue(ValueProperty);
		}
		set
		{
			SetValue(ValueProperty, value);
		}
	}

	public event EventHandler<NumberBadgeValueChangedEventArgs> ValueChanged;

	protected override void OnApplyTemplate()
	{
		_borderNum = GetTemplateChild("TextBlockBadgeValue") as TextBlock;
		SetTextBlockWidth();
		base.SizeChanged += NumberBadge_SizeChanged;
		UpdateValue();
		base.OnApplyTemplate();
	}

	private void NumberBadge_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		SetTextBlockWidth();
	}

	private void SetTextBlockWidth()
	{
		if (_borderNum != null)
		{
			UpdateLayout();
			int length = _borderNum.Text.Length;
			TextBlock borderNum = _borderNum;
			borderNum.Width = length switch
			{
				1 => 16, 
				2 => 26, 
				_ => 31, 
			};
		}
	}

	private void UpdateValue()
	{
		if (_borderNum != null)
		{
			if (Value < 0)
			{
				Value = 0;
				_borderNum.Text = Value.ToString();
			}
			else if (Value >= 1000)
			{
				_borderNum.Text = "999+";
			}
			else
			{
				_borderNum.Text = Value.ToString();
			}
			SetValue(AutomationProperties.NameProperty, _borderNum.Text);
			SetTextBlockWidth();
		}
	}

	private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is NumberBadge numberBadge)
		{
			numberBadge.ValueChanged?.Invoke(numberBadge, new NumberBadgeValueChangedEventArgs((int)e.NewValue, (int)e.OldValue));
			numberBadge.UpdateValue();
		}
	}
}
