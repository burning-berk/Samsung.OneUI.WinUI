using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Controls;

internal class TimePickerListAnimationService : ITimePickerListAnimationService, IPickerListAnimationService
{
	private const int HOUR_COLUMN = 1;

	private const int MINUTE_COLUMN = 2;

	private const int PERIOD_COLUMN = 3;

	private const int ENTRANCE_ANIMATION_TOTAL_DURATION_MS = 1500;

	private const int ENTRANCE_ANIMATION_WAIT_TIME_BETWEEN_SCROLLLIST_MS = 100;

	private const int ENTRANCE_ANIMATION_WAIT_TIME_AFTER_COMPLETED_MS = 200;

	private const int HOUR_MINUTE_ENTRANCE_ANIMATION_VERTICAL_OFF_SET = 280;

	private const int PERIOD_ENTRANCE_ANIMATION_VERTICAL_OFF_SET = 56;

	private const int ENTRANCE_ANIMATION_DIALOG_OPEN_DURATION = 350;

	private const int THRESHOLD_WAIT_TIME = 100;

	internal bool ListHoursEntranceAnimationCompleted;

	internal bool ListMinutesEntranceAnimationCompleted;

	internal bool ListPeriodEntranceAnimationCompleted;

	private readonly TimePickerList _timePickerList;

	public TimePickerListAnimationService(TimePickerList timePickerList)
	{
		_timePickerList = timePickerList;
	}

	public void StartEntranceAnimation()
	{
		new Thread((ThreadStart)async delegate
		{
			int num = 1500;
			int waitTime = 100;
			int timeToStopAnimation = 200 + num;
			PickerEntranceAnimationStartParameter startParameter = await GetEntranceAnimationStartParameter(num, waitTime);
			PickerEntranceAnimationStopParameter stopParameter = GetEntranceAnimationStopParameter(timeToStopAnimation, startParameter);
			await Task.Delay(TimeSpan.FromMilliseconds(350.0));
			StartingEntranceAnimation(startParameter);
			StoppingEntranceAnimation(stopParameter);
		}).Start();
	}

	public async Task<bool> IsPeriodVisible()
	{
		bool result = false;
		await _timePickerList.DispatcherQueue.EnqueueAsync(delegate
		{
			if (_timePickerList.ListPeriod != null)
			{
				result = Visibility.Visible.Equals(_timePickerList.ListPeriod.Visibility);
			}
		});
		return result;
	}

	private async Task<PickerEntranceAnimationStartParameter> GetEntranceAnimationStartParameter(int duration, int waitTime)
	{
		PickerEntranceAnimationStartParameter parameter = new PickerEntranceAnimationStartParameter
		{
			VerticalOffSet = 280.0,
			PeriodVerticalOffSet = GetPeriodVerticalOffSet()
		};
		PickerEntranceAnimationStartParameter pickerEntranceAnimationStartParameter = parameter;
		pickerEntranceAnimationStartParameter.IsPeriodVisible = await IsPeriodVisible();
		parameter.ColumnsDuration = new Queue<int>();
		parameter.ColumnsDuration.Enqueue(GetColumnDuration(1, duration, waitTime, parameter.IsPeriodVisible));
		parameter.ColumnsDuration.Enqueue(GetColumnDuration(2, duration, waitTime, parameter.IsPeriodVisible));
		parameter.ColumnsDuration.Enqueue(parameter.IsPeriodVisible ? GetColumnDuration(3, duration, waitTime, parameter.IsPeriodVisible) : 0);
		return parameter;
	}

	private PickerEntranceAnimationStopParameter GetEntranceAnimationStopParameter(int timeToStopAnimation, PickerEntranceAnimationStartParameter parameter)
	{
		return new PickerEntranceAnimationStopParameter
		{
			TimeToStopAnimation = timeToStopAnimation,
			IsPeriodVisible = parameter.IsPeriodVisible
		};
	}

	private int GetColumnDuration(int column, int totalDuration, int wait, bool isPeriodVisible)
	{
		if (isPeriodVisible)
		{
			return column switch
			{
				1 => totalDuration - wait - wait, 
				2 => totalDuration - wait, 
				_ => totalDuration, 
			};
		}
		return (column != 1) ? totalDuration : (totalDuration - wait);
	}

	internal double GetPeriodVerticalOffSet()
	{
		return 56.0;
	}

	internal void StartingEntranceAnimation(PickerEntranceAnimationStartParameter parameter)
	{
		AddEventsAnimation(parameter.IsPeriodVisible);
		int columnDuration = parameter.ColumnsDuration.Dequeue();
		AnimateColumn(_timePickerList.ListHours, columnDuration, parameter.VerticalOffSet);
		int columnDuration2 = parameter.ColumnsDuration.Dequeue();
		AnimateColumn(_timePickerList.ListMinutes, columnDuration2, parameter.VerticalOffSet);
		if (parameter.IsPeriodVisible)
		{
			int columnDuration3 = parameter.ColumnsDuration.Dequeue();
			AnimateColumn(_timePickerList.ListPeriod, columnDuration3, parameter.PeriodVerticalOffSet);
		}
	}

	private async void StoppingEntranceAnimation(PickerEntranceAnimationStopParameter parameter)
	{
		await Task.Delay(TimeSpan.FromMilliseconds(parameter.TimeToStopAnimation));
		await WaitForEntranceAnimationCompletionAsync(parameter);
		await _timePickerList.DispatcherQueue.EnqueueAsync(delegate
		{
			CreateStopEntranceAnimation();
		});
	}

	private async void AddEventsAnimation(bool isPeriodVisible)
	{
		await _timePickerList.DispatcherQueue.EnqueueAsync(delegate
		{
			if (_timePickerList.ListHours != null)
			{
				ListHoursEntranceAnimationCompleted = false;
				_timePickerList.ListHours.EntranceAnimationCompleted += ListHours_EntranceAnimationCompleted;
			}
			if (_timePickerList.ListMinutes != null)
			{
				ListMinutesEntranceAnimationCompleted = false;
				_timePickerList.ListMinutes.EntranceAnimationCompleted += ListMinutes_EntranceAnimationCompleted;
			}
			if (_timePickerList.ListPeriod != null && isPeriodVisible)
			{
				ListPeriodEntranceAnimationCompleted = false;
				_timePickerList.ListPeriod.EntranceAnimationCompleted += ListPeriod_EntranceAnimationCompleted;
			}
		});
	}

	private void ListHours_EntranceAnimationCompleted(object sender, EventArgs e)
	{
		ListHoursEntranceAnimationCompleted = true;
	}

	private void ListMinutes_EntranceAnimationCompleted(object sender, EventArgs e)
	{
		ListMinutesEntranceAnimationCompleted = true;
	}

	private void ListPeriod_EntranceAnimationCompleted(object sender, EventArgs e)
	{
		ListPeriodEntranceAnimationCompleted = true;
	}

	private async Task WaitForEntranceAnimationCompletionAsync(PickerEntranceAnimationStopParameter parameter)
	{
		while (!ListHoursEntranceAnimationCompleted || !ListMinutesEntranceAnimationCompleted || (parameter.IsPeriodVisible && !ListPeriodEntranceAnimationCompleted))
		{
			await Task.Delay(TimeSpan.FromMilliseconds(100.0));
		}
	}

	internal void AnimateColumn(ScrollList scrollList, int columnDuration, double verticalOffSet)
	{
		new Thread((ThreadStart)async delegate
		{
			await _timePickerList.DispatcherQueue.EnqueueAsync(delegate
			{
				scrollList?.EntranceAnimation(verticalOffSet, TimeSpan.FromMilliseconds(columnDuration));
			});
		}).Start();
	}

	internal void CreateStopEntranceAnimation()
	{
		_timePickerList.ListHours?.StopEntranceAnimation();
		_timePickerList.ListMinutes?.StopEntranceAnimation();
		_timePickerList.ListPeriod?.StopEntranceAnimation();
	}
}
