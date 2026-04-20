using System;
using Microsoft.UI.Xaml.Media.Animation;
using Samsung.OneUI.WinUI.Services.Animation.Timelines;
using Windows.Foundation;

namespace Samsung.OneUI.WinUI.Services.Animation;

internal class TimelineBase
{
	public object ZeroTimeValue { get; set; }

	public object From { get; set; } = 0;

	public object To { get; set; } = 0;

	public TimeSpan BeginTime { get; set; } = TimeSpan.Zero;

	public TimeSpan EndTime { get; set; } = TimeSpan.Zero;

	public KeySpline KeySpline { get; set; } = new KeySpline
	{
		ControlPoint1 = new Point(0f, 0f),
		ControlPoint2 = new Point(1f, 1f)
	};

	public IAnimationTimeline AnimationTimeline { get; set; }
}
