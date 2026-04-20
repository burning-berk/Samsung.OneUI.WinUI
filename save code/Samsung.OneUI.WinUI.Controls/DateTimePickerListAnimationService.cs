using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.WinUI;

namespace Samsung.OneUI.WinUI.Controls;

internal class DateTimePickerListAnimationService
{
	private const int DAY_MONTH_YEAR_COLUMN = 1;

	private const int HOUR_COLUMN = 2;

	private const int MINUTE_COLUMN = 3;

	private const int PERIOD_COLUMN = 4;

	private const double DPI_SCALE_125 = 1.25;

	private const double DPI_SCALE_175 = 1.75;

	private const int ENTRANCE_ANIMATION_TOTAL_DURATION_MS = 1500;

	private const int ENTRANCE_ANIMATION_WAIT_TIME_BETWEEN_SCROLLLIST_MS = 100;

	private const int ENTRANCE_ANIMATION_WAIT_TIME_AFTER_COMPLETED_MS = 200;

	private const int ENTRANCE_ANIMATION_VERTICAL_OFF_SET = 280;

	private const double ENTRANCE_ANIMATION_VERTICAL_OFF_SET_DPI_SCALE_125 = 280.0;

	private const double ENTRANCE_ANIMATION_VERTICAL_OFF_SET_DPI_SCALE_175 = 280.0;

	private const int PERIOD_ENTRANCE_ANIMATION_VERTICAL_OFF_SET = 56;

	private const double PERIOD_ENTRANCE_ANIMATION_VERTICAL_OFF_SET_DPI_SCALE_125 = 56.0;

	private const double PERIOD_ENTRANCE_ANIMATION_VERTICAL_OFF_SET_DPI_SCALE_175 = 56.0;

	private const int THRESHOLD_WAIT_TIME = 100;

	private bool _listDayMonthYearEntranceAnimationCompleted;

	private readonly DateTimePickerList _dateTimePickerList;

	private readonly TimePickerListAnimationService _timePickerAnimationService;

	public event EventHandler StoppedEntranceAnimationEvent;

	public DateTimePickerListAnimationService(DateTimePickerList dateTimePickerList)
	{
		_dateTimePickerList = dateTimePickerList;
		_timePickerAnimationService = new TimePickerListAnimationService(dateTimePickerList);
	}

	public async Task StartEntranceAnimationAsync()
	{
		int num = 1500;
		int waitTime = 100;
		int timeToStopAnimation = 200 + num;
		PickerEntranceAnimationStartParameter parameter = await GetEntranceAnimationStartParameterAsync(num, waitTime);
		PickerEntranceAnimationStopParameter entranceAnimationStopParameter = GetEntranceAnimationStopParameter(timeToStopAnimation, parameter);
		StartingEntranceAnimation(parameter);
		StoppingEntranceAnimation(entranceAnimationStopParameter);
	}

	private async Task<PickerEntranceAnimationStartParameter> GetEntranceAnimationStartParameterAsync(int duration, int waitTime)
	{
		PickerEntranceAnimationStartParameter parameter = new PickerEntranceAnimationStartParameter();
		PickerEntranceAnimationStartParameter pickerEntranceAnimationStartParameter = parameter;
		pickerEntranceAnimationStartParameter.VerticalOffSet = await GetHourMinuteVerticalOffSetAsync();
		await _dateTimePickerList.DispatcherQueue.EnqueueAsync(delegate
		{
			parameter.PeriodVerticalOffSet = GetPeriodVerticalOffSet();
		});
		pickerEntranceAnimationStartParameter = parameter;
		pickerEntranceAnimationStartParameter.IsPeriodVisible = await _timePickerAnimationService.IsPeriodVisible();
		parameter.ColumnsDuration = new Queue<int>();
		parameter.ColumnsDuration.Enqueue(GetColumnDuration(1, duration, waitTime, parameter.IsPeriodVisible));
		parameter.ColumnsDuration.Enqueue(GetColumnDuration(2, duration, waitTime, parameter.IsPeriodVisible));
		parameter.ColumnsDuration.Enqueue(GetColumnDuration(3, duration, waitTime, parameter.IsPeriodVisible));
		parameter.ColumnsDuration.Enqueue(parameter.IsPeriodVisible ? GetColumnDuration(4, duration, waitTime, parameter.IsPeriodVisible) : 0);
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

	private async Task<double> GetHourMinuteVerticalOffSetAsync()
	{
		double result = 280.0;
		await _dateTimePickerList.DispatcherQueue.EnqueueAsync(delegate
		{
			if (_dateTimePickerList.XamlRoot != null)
			{
				double rasterizationScale = _dateTimePickerList.XamlRoot.RasterizationScale;
				if (rasterizationScale == 1.25)
				{
					result = 280.0;
				}
				else if (rasterizationScale == 1.75)
				{
					result = 280.0;
				}
			}
		});
		return result;
	}

	internal double GetPeriodVerticalOffSet()
	{
		double result = 56.0;
		if (_dateTimePickerList.XamlRoot != null)
		{
			double rasterizationScale = _dateTimePickerList.XamlRoot.RasterizationScale;
			if (rasterizationScale == 1.25)
			{
				result = 56.0;
			}
			else if (rasterizationScale == 1.75)
			{
				result = 56.0;
			}
		}
		return result;
	}

	private int GetColumnDuration(int column, int totalDuration, int wait, bool isPeriodVisible)
	{
		if (isPeriodVisible)
		{
			return column switch
			{
				1 => totalDuration - wait - wait - wait, 
				2 => totalDuration - wait - wait, 
				3 => totalDuration - wait, 
				_ => totalDuration, 
			};
		}
		return column switch
		{
			1 => totalDuration - wait - wait, 
			2 => totalDuration - wait, 
			_ => totalDuration, 
		};
	}

	private void StartingEntranceAnimation(PickerEntranceAnimationStartParameter parameter)
	{
		AddEventsAnimation();
		int columnDuration = parameter.ColumnsDuration.Dequeue();
		_timePickerAnimationService.AnimateColumn(_dateTimePickerList.ListDayMonthYear, columnDuration, parameter.VerticalOffSet);
		_timePickerAnimationService.StartingEntranceAnimation(parameter);
	}

	private async void StoppingEntranceAnimation(PickerEntranceAnimationStopParameter parameter)
	{
		await Task.Delay(TimeSpan.FromMilliseconds(parameter.TimeToStopAnimation));
		await WaitForEntranceAnimationCompletionAsync(parameter);
		await _dateTimePickerList.DispatcherQueue.EnqueueAsync(delegate
		{
			_dateTimePickerList.ListDayMonthYear?.StopEntranceAnimation();
			_timePickerAnimationService.CreateStopEntranceAnimation();
			this.StoppedEntranceAnimationEvent?.Invoke(this, EventArgs.Empty);
		});
	}

	private async void AddEventsAnimation()
	{
		await _dateTimePickerList.DispatcherQueue.EnqueueAsync(delegate
		{
			if (_dateTimePickerList.ListDayMonthYear != null)
			{
				_listDayMonthYearEntranceAnimationCompleted = false;
				_dateTimePickerList.ListDayMonthYear.EntranceAnimationCompleted += ListDayMonthYear_EntranceAnimationCompleted;
			}
		});
	}

	private void ListDayMonthYear_EntranceAnimationCompleted(object sender, EventArgs e)
	{
		_listDayMonthYearEntranceAnimationCompleted = true;
	}

	private async Task WaitForEntranceAnimationCompletionAsync(PickerEntranceAnimationStopParameter parameter)
	{
		while (IsThereAnyAnimationToComplete(parameter))
		{
			await Task.Delay(TimeSpan.FromMilliseconds(100.0));
		}
	}

	private bool IsThereAnyAnimationToComplete(PickerEntranceAnimationStopParameter parameter)
	{
		if (!IsDateSectionAnimationIncomplete() && !IsTimeSectionAnimationIncomplete())
		{
			return IsPeriodSectionAnimationIncomplete(parameter);
		}
		return true;
	}

	private bool IsDateSectionAnimationIncomplete()
	{
		return !_listDayMonthYearEntranceAnimationCompleted;
	}

	private bool IsTimeSectionAnimationIncomplete()
	{
		if (_timePickerAnimationService.ListHoursEntranceAnimationCompleted)
		{
			return !_timePickerAnimationService.ListMinutesEntranceAnimationCompleted;
		}
		return true;
	}

	private bool IsPeriodSectionAnimationIncomplete(PickerEntranceAnimationStopParameter parameter)
	{
		if (parameter.IsPeriodVisible)
		{
			return !_timePickerAnimationService.ListPeriodEntranceAnimationCompleted;
		}
		return false;
	}
}
