using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.UI.ViewManagement;

namespace Samsung.OneUI.WinUI.Controls;

internal class DatePickerSpinnerListAnimationService : IPickerListAnimationService
{
	private const int ENTRANCE_ANIMATION_TOTAL_DURATION_MS = 1500;

	private const int ENTRANCE_ANIMATION_WAIT_TIME_BETWEEN_SCROLLLIST_MS = 100;

	private const int ENTRANCE_ANIMATION_WAIT_TIME_AFTER_COMPLETED_MS = 200;

	private const int ENTRANCE_ANIMATION_VERTICAL_OFF_SET = 280;

	private const int ENTRANCE_ANIMATION_DIALOG_OPEN_DURATION = 350;

	private const int THRESHOLD_WAIT_TIME = 100;

	private const string GRID_SPINNER_NAME = "GridSpinner";

	private readonly IEnumerable<ScrollList> _scrollLists;

	private List<Tuple<int, bool>> _verifier;

	private readonly DependencyObject _parent;

	public DatePickerSpinnerListAnimationService(DependencyObject parent)
	{
		_parent = parent;
		_scrollLists = UIExtensionsInternal.FindChildByName<Grid>("GridSpinner", _parent)?.Children?.OfType<ScrollList>() ?? new Collection<ScrollList>();
	}

	public async void StartEntranceAnimation()
	{
		if (IsAnimationEnabled())
		{
			await Task.Delay(TimeSpan.FromMilliseconds(350.0));
			List<Tuple<int, ScrollList>> scrollListIndex = GetScrollListIndex();
			StartAllEnterAnimation(scrollListIndex);
			await Task.Delay(TimeSpan.FromMilliseconds(1700.0));
			await WaitForEntranceAnimationCompletionAsync();
			StopAllAnimations();
		}
	}

	private static bool IsAnimationEnabled()
	{
		return new UISettings().AnimationsEnabled;
	}

	public void PrepareEntranceAnimation()
	{
		foreach (ScrollList scrollList in _scrollLists)
		{
			scrollList.PrepareEntranceAnimation();
		}
	}

	private void StartAllEnterAnimation(List<Tuple<int, ScrollList>> columnIndexScroll)
	{
		int num = 1500;
		int num2 = 100;
		_verifier = new List<Tuple<int, bool>>();
		foreach (Tuple<int, ScrollList> scrollList in columnIndexScroll)
		{
			_verifier.Add(new Tuple<int, bool>(scrollList.Item1, item2: true));
			TimeSpan duration = TimeSpan.FromMilliseconds(num + num2);
			scrollList.Item2.EntranceAnimation(280.0, duration, delegate
			{
				int num3 = _verifier.FindIndex((Tuple<int, bool> t) => t.Item1 == scrollList.Item1);
				if (num3 != -1)
				{
					_verifier[num3] = new Tuple<int, bool>(scrollList.Item1, item2: false);
				}
			});
			num += num2;
		}
	}

	private List<Tuple<int, ScrollList>> GetScrollListIndex()
	{
		List<Tuple<int, ScrollList>> list = new List<Tuple<int, ScrollList>>();
		foreach (ScrollList scrollList in _scrollLists)
		{
			int column = Grid.GetColumn(scrollList);
			list.Add(new Tuple<int, ScrollList>(column, scrollList));
		}
		return list.OrderBy((Tuple<int, ScrollList> t) => t.Item1).ToList();
	}

	private async Task WaitForEntranceAnimationCompletionAsync()
	{
		bool existeBoolTrue = _verifier.Any((Tuple<int, bool> t) => t.Item2);
		while (existeBoolTrue)
		{
			existeBoolTrue = _verifier.Any((Tuple<int, bool> t) => t.Item2);
			await Task.Delay(TimeSpan.FromMilliseconds(100.0));
		}
	}

	private void StopAllAnimations()
	{
		foreach (ScrollList scrollList in _scrollLists)
		{
			scrollList.StopEntranceAnimation();
		}
	}
}
