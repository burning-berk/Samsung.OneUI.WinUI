using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Animation;
using Samsung.OneUI.WinUI.Services.Animation.Timelines;
using Windows.Foundation;

namespace Samsung.OneUI.WinUI.Services.Animation;

internal class MultiPaneAnimationService : IMultiPaneAnimationService, IAnimationService
{
	private readonly IAnimationMaker _animationMaker;

	public MultiPaneAnimationService()
	{
		_animationMaker = new AnimationMaker();
	}

	public void ExecuteAnimation(FrameworkElement panel, double from, double to, Action OnCompleted)
	{
		if (!(panel == null))
		{
			_animationMaker.Create();
			TimelineBase timelineBase = new TimelineBase
			{
				From = from,
				To = to,
				BeginTime = TimeSpan.FromMilliseconds(0.0),
				EndTime = TimeSpan.FromMilliseconds(400.0),
				KeySpline = new KeySpline
				{
					ControlPoint1 = new Point(0.22, 0.25),
					ControlPoint2 = new Point(0.0, 1.0)
				},
				AnimationTimeline = new DoubleAnimationTimeline()
			};
			_animationMaker.AddAnimation(panel, PropertiesEnum.OpenPaneLength, timelineBase);
			_animationMaker.Execute(OnCompleted);
		}
	}
}
