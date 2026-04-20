using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.Xaml.Interactivity;

namespace Samsung.OneUI.WinUI.Controls.Behaviors;

internal class ThumbDisabledScrollBarDimensionsBehavior : Behavior<Thumb>
{
	public static readonly DependencyProperty SmallRepeatButtonProperty = DependencyProperty.Register("SmallRepeatButton", typeof(RepeatButton), typeof(ThumbDisabledScrollBarDimensionsBehavior), new PropertyMetadata(null));

	public static readonly DependencyProperty RepeatButtonProperty = DependencyProperty.Register("LargeRepeatButton", typeof(RepeatButton), typeof(ThumbDisabledScrollBarDimensionsBehavior), new PropertyMetadata(null));

	public static readonly DependencyProperty CustomParentProperty = DependencyProperty.Register("ScrollBarReference", typeof(ScrollBar), typeof(ThumbDisabledScrollBarDimensionsBehavior), new PropertyMetadata(null));

	public RepeatButton SmallRepeatButton
	{
		get
		{
			return (RepeatButton)GetValue(SmallRepeatButtonProperty);
		}
		set
		{
			SetValue(SmallRepeatButtonProperty, value);
		}
	}

	public RepeatButton LargeRepeatButton
	{
		get
		{
			return (RepeatButton)GetValue(RepeatButtonProperty);
		}
		set
		{
			SetValue(RepeatButtonProperty, value);
		}
	}

	public ScrollBar ScrollBarReference
	{
		get
		{
			return (ScrollBar)GetValue(CustomParentProperty);
		}
		set
		{
			SetValue(CustomParentProperty, value);
		}
	}

	protected override void OnAttached()
	{
		base.OnAttached();
		UpdateThumbBarDimension();
	}

	private void UpdateThumbBarDimension()
	{
		ScrollBar scrollBarReference = ScrollBarReference;
		if ((object)scrollBarReference != null)
		{
			((scrollBarReference.Orientation == Orientation.Horizontal) ? new Action<ScrollBar>(UpdateHorizontalThumbBarDimension) : new Action<ScrollBar>(UpdateVerticalThumbBarDimension))(scrollBarReference);
		}
	}

	private void UpdateHorizontalThumbBarDimension(ScrollBar scrollBar)
	{
		if (!(base.AssociatedObject == null) && !(scrollBar == null) && !(SmallRepeatButton == null))
		{
			double num = scrollBar.ActualWidth - SmallRepeatButton.Width * 2.0;
			base.AssociatedObject.Width = GetPositiveThumbDimension(num / 2.0);
			if (LargeRepeatButton != null)
			{
				LargeRepeatButton.Width = base.AssociatedObject.Width - base.AssociatedObject.Width / 2.0;
			}
		}
	}

	private void UpdateVerticalThumbBarDimension(ScrollBar scrollBar)
	{
		if (!(base.AssociatedObject == null) && !(scrollBar == null) && !(SmallRepeatButton == null))
		{
			double num = scrollBar.ActualHeight - SmallRepeatButton.Height * 2.0;
			base.AssociatedObject.Height = GetPositiveThumbDimension(num / 2.0);
			if (LargeRepeatButton != null)
			{
				LargeRepeatButton.Height = base.AssociatedObject.Height - base.AssociatedObject.Height / 2.0;
			}
		}
	}

	private double GetPositiveThumbDimension(double value)
	{
		if (!(value < 0.0))
		{
			return value;
		}
		return double.NaN;
	}
}
