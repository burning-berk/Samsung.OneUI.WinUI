using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls;

public class PeriodScrollList : ScrollList
{
	private const int AM_INDEX = 1;

	private const int PM_INDEX = 2;

	public static readonly DependencyProperty VerticalOffSetAnimationProperty = DependencyProperty.Register("VerticalOffSetAnimation", typeof(double), typeof(PeriodScrollList), new PropertyMetadata(0));

	private readonly PeriodScrollListAnimationService _periodScrollListAnimationService;

	public double VerticalOffSetAnimation
	{
		get
		{
			return (double)GetValue(VerticalOffSetAnimationProperty);
		}
		set
		{
			SetValue(VerticalOffSetAnimationProperty, value);
		}
	}

	public PeriodScrollList()
	{
		base.DefaultStyleKey = typeof(PeriodScrollList);
		base.InfiniteScroll = false;
		base.Unloaded += OnPeriodScrollListUnloaded;
		_periodScrollListAnimationService = new PeriodScrollListAnimationService(this);
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		if (_listView != null)
		{
			_listView.SelectionChanged += OnListViewSelectionChanged;
		}
	}

	internal override void ConfigureListViewEntranceAnimation()
	{
		if (IsAnimationEnabled())
		{
			_periodScrollListAnimationService.ConfigureListViewEntranceAnimation();
		}
	}

	internal override void StopScrollAnimation()
	{
		if (IsAnimationEnabled())
		{
			_periodScrollListAnimationService.StopScrollAnimation();
		}
	}

	internal override void StartScrollAnimation()
	{
		if (IsAnimationEnabled())
		{
			_periodScrollListAnimationService.StartScrollAnimation();
		}
	}

	internal override void EntranceAnimation(double verticalOffSet, TimeSpan duration, Action onCompleted = null)
	{
		if (IsAnimationEnabled())
		{
			_periodScrollListAnimationService.EntranceAnimation(verticalOffSet, duration, onCompleted);
		}
	}

	internal override void StopEntranceAnimation()
	{
		if (IsAnimationEnabled())
		{
			_periodScrollListAnimationService.StopEntranceAnimation();
		}
	}

	internal override void PrepareEntranceAnimation()
	{
		if (IsAnimationEnabled())
		{
			_periodScrollListAnimationService.PrepareEntranceAnimation();
		}
	}

	private void OnListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		ListView listView = sender as ListView;
		if (listView.SelectedIndex <= 1)
		{
			listView.SelectedIndex = 1;
		}
		else
		{
			listView.SelectedIndex = 2;
		}
	}

	private void OnPeriodScrollListUnloaded(object sender, RoutedEventArgs e)
	{
		if (_listView != null)
		{
			_listView.SelectionChanged -= OnListViewSelectionChanged;
		}
	}
}
