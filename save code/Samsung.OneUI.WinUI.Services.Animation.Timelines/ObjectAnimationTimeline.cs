using System;
using Microsoft.UI.Xaml.Media.Animation;

namespace Samsung.OneUI.WinUI.Services.Animation.Timelines;

internal class ObjectAnimationTimeline : IAnimationTimeline
{
	public Timeline CreateAnimation(TimelineBase timelineBase)
	{
		ObjectAnimationUsingKeyFrames obj = new ObjectAnimationUsingKeyFrames
		{
			EnableDependentAnimation = true
		};
		DiscreteObjectKeyFrame item = new DiscreteObjectKeyFrame
		{
			KeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero),
			Value = timelineBase.From
		};
		DiscreteObjectKeyFrame item2 = new DiscreteObjectKeyFrame
		{
			KeyTime = KeyTime.FromTimeSpan(timelineBase.EndTime),
			Value = timelineBase.To
		};
		obj.KeyFrames.Add(item);
		obj.KeyFrames.Add(item2);
		return obj;
	}
}
