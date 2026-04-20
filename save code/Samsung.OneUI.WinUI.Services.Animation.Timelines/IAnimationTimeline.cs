using Microsoft.UI.Xaml.Media.Animation;

namespace Samsung.OneUI.WinUI.Services.Animation.Timelines;

internal interface IAnimationTimeline
{
	Timeline CreateAnimation(TimelineBase timelineBase);
}
