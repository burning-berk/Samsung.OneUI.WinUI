using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls;

public abstract class BadgeBase : Control
{
	private const string SELECTED_STATE = "Selected";

	private const string NORMAL_STATE = "Normal";

	public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(BadgeBase), new PropertyMetadata(false, OnIsSelectedChanged));

	public bool IsSelected
	{
		get
		{
			return (bool)GetValue(IsSelectedProperty);
		}
		set
		{
			SetValue(IsSelectedProperty, value);
		}
	}

	private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is BadgeBase badgeBase)
		{
			badgeBase.UpdateVisualState();
		}
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		UpdateVisualState();
	}

	private void UpdateVisualState()
	{
		VisualStateManager.GoToState(this, IsSelected ? "Selected" : "Normal", useTransitions: false);
	}
}
