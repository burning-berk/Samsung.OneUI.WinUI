using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace Samsung.OneUI.WinUI.Controls;

internal class PeriodScrollListAnimationService : ScrollListAnimationService
{
	private const int AM_INDEX = 1;

	private const int PM_INDEX = 2;

	private readonly PeriodScrollList _scrollList;

	public PeriodScrollListAnimationService(PeriodScrollList scrollList)
		: base(scrollList)
	{
		_scrollList = scrollList;
	}

	internal override void ConfigureListViewEntranceAnimation()
	{
		if (_scrollList._listViewEntranceAnimation != null && _scrollList._listView != null)
		{
			ConfigureParametersDefaultEntranceAnimation();
			_scrollList._listViewEntranceAnimation.ItemsSource = CloneObservableCollection(_scrollList.TimeItemsSource);
			_scrollList._listViewEntranceAnimation.SelectedIndex = GetSelectedIndexBeforeEntranceAnimation();
			ConfigureInitialPositionListViewAnimation(_scrollList.VerticalOffSetAnimation);
		}
	}

	internal override void ConfigureFromAndToEntranceAnimation(double verticalOffSet, out double from, out double to)
	{
		from = 0.0;
		to = 0.0;
		if (GetSelectedIndexListViewAnimation() == 1)
		{
			from = 0.0 - verticalOffSet;
		}
		else
		{
			to = 0.0 - verticalOffSet;
		}
	}

	private int GetSelectedIndexListViewAnimation()
	{
		int result = -1;
		if (_scrollList._listViewEntranceAnimation != null && _scrollList._listViewEntranceAnimation.ItemsSource != null)
		{
			ObservableCollection<object> observableCollection = (ObservableCollection<object>)_scrollList._listViewEntranceAnimation.ItemsSource;
			if (observableCollection != null)
			{
				result = observableCollection.IndexOf(_scrollList.SelectedTime);
			}
		}
		return result;
	}

	private int GetSelectedIndexBeforeEntranceAnimation()
	{
		if (GetSelectedIndexListViewAnimation() == 1)
		{
			return 2;
		}
		return 1;
	}

	private void ConfigureInitialPositionListViewAnimation(double verticalOffSet)
	{
		if (_scrollList._listViewEntranceAnimation != null)
		{
			ListView listViewEntranceAnimation = _scrollList._listViewEntranceAnimation;
			TranslateTransform translateTransform = new TranslateTransform();
			if (GetSelectedIndexListViewAnimation() == 1)
			{
				translateTransform.Y = 0.0 - verticalOffSet;
			}
			listViewEntranceAnimation.RenderTransform = translateTransform;
		}
	}
}
