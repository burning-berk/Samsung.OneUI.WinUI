using System;
using Microsoft.UI.Xaml.Media.Animation;

namespace Samsung.OneUI.WinUI.Services.Animation.Timelines;

internal class DoubleAnimationTimeline : IAnimationTimeline
{
	public Timeline CreateAnimation(TimelineBase timelineBase)
	{
		try
		{
			DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
			doubleAnimationUsingKeyFrames.EnableDependentAnimation = true;
			if (timelineBase.ZeroTimeValue != null)
			{
				SplineDoubleKeyFrame splineDoubleKeyFrame = new SplineDoubleKeyFrame();
				splineDoubleKeyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero);
				splineDoubleKeyFrame.Value = Convert.ToDouble(timelineBase.ZeroTimeValue);
				doubleAnimationUsingKeyFrames.KeyFrames.Add(splineDoubleKeyFrame);
			}
			SplineDoubleKeyFrame splineDoubleKeyFrame2 = new SplineDoubleKeyFrame();
			splineDoubleKeyFrame2.KeyTime = KeyTime.FromTimeSpan(timelineBase.BeginTime);
			splineDoubleKeyFrame2.Value = Convert.ToDouble(timelineBase.From);
			SplineDoubleKeyFrame splineDoubleKeyFrame3 = new SplineDoubleKeyFrame();
			splineDoubleKeyFrame3.KeyTime = KeyTime.FromTimeSpan(timelineBase.EndTime);
			splineDoubleKeyFrame3.Value = Convert.ToDouble(timelineBase.To);
			splineDoubleKeyFrame3.KeySpline = new KeySpline
			{
				ControlPoint1 = timelineBase.KeySpline.ControlPoint1,
				ControlPoint2 = timelineBase.KeySpline.ControlPoint2
			};
			doubleAnimationUsingKeyFrames.KeyFrames.Add(splineDoubleKeyFrame2);
			doubleAnimationUsingKeyFrames.KeyFrames.Add(splineDoubleKeyFrame3);
			return doubleAnimationUsingKeyFrames;
		}
		catch
		{
			return new DoubleAnimationUsingKeyFrames();
		}
	}
}
