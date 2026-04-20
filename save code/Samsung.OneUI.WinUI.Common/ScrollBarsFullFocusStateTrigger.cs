using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Common;

internal class ScrollBarsFullFocusStateTrigger : StateTriggerBase
{
	public static readonly DependencyProperty ScrollModeProperty = DependencyProperty.Register("ScrollMode", typeof(ScrollMode), typeof(ScrollBarsFullFocusStateTrigger), new PropertyMetadata(null, OnScrollModePropertyChanged));

	public static readonly DependencyProperty FocusStateProperty = DependencyProperty.Register("FocusState", typeof(FocusState), typeof(ScrollBarsFullFocusStateTrigger), new PropertyMetadata(null, OnFocusStatePropertyChanged));

	public ScrollMode ScrollMode
	{
		get
		{
			return (ScrollMode)GetValue(ScrollModeProperty);
		}
		set
		{
			SetValue(ScrollModeProperty, value);
		}
	}

	public FocusState FocusState
	{
		get
		{
			return (FocusState)GetValue(FocusStateProperty);
		}
		set
		{
			SetValue(FocusStateProperty, value);
		}
	}

	private static void OnScrollModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ScrollBarsFullFocusStateTrigger scrollBarsFullFocusStateTrigger)
		{
			scrollBarsFullFocusStateTrigger.CheckFocusStateTrigger();
		}
	}

	private static void OnFocusStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ScrollBarsFullFocusStateTrigger scrollBarsFullFocusStateTrigger)
		{
			scrollBarsFullFocusStateTrigger.CheckFocusStateTrigger();
		}
	}

	private void CheckFocusStateTrigger()
	{
		if (FocusState == FocusState.Unfocused || ScrollMode == ScrollMode.Disabled)
		{
			SetActive(IsActive: false);
			return;
		}
		bool active = FocusState == FocusState.Keyboard || FocusState == FocusState.Programmatic;
		SetActive(active);
	}
}
