using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Samsung.OneUI.WinUI.Services.Animation.Timelines;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.Foundation;

namespace Samsung.OneUI.WinUI.Services.Animation;

internal class ExpandableListAnimationService : IExpandableListAnimationService, IAnimationService
{
	private readonly IAnimationMaker _animationMaker;

	public ExpandableListAnimationService()
	{
		_animationMaker = new AnimationMaker();
	}

	public void OpenAnimation(FrameworkElement frameworkElement)
	{
		if (!(frameworkElement == null))
		{
			_animationMaker.Create();
			TimelineBase timelineBase = new TimelineBase
			{
				From = 0,
				To = 1,
				BeginTime = TimeSpan.FromMilliseconds(0.0),
				EndTime = TimeSpan.FromMilliseconds(300.0),
				AnimationTimeline = new DoubleAnimationTimeline()
			};
			_animationMaker.AddAnimation(frameworkElement, PropertiesEnum.Opacity, timelineBase);
			CompositeTransform renderTransform = new CompositeTransform
			{
				CenterX = 0.0,
				CenterY = 0.0,
				ScaleX = 1.0,
				ScaleY = 0.0
			};
			frameworkElement.RenderTransform = renderTransform;
			frameworkElement.RenderTransformOrigin = new Point(0f, 0f);
			TimelineBase timelineBase2 = new TimelineBase
			{
				From = 0,
				To = 1,
				BeginTime = TimeSpan.FromMilliseconds(0.0),
				EndTime = TimeSpan.FromMilliseconds(300.0),
				KeySpline = new KeySpline
				{
					ControlPoint1 = new Point(0.4, 0.6),
					ControlPoint2 = new Point(0f, 1f)
				},
				AnimationTimeline = new DoubleAnimationTimeline()
			};
			CompositeTransform transform = frameworkElement.GetTransform<CompositeTransform>();
			_animationMaker.AddAnimation(transform, PropertiesEnum.ScaleY, timelineBase2);
			_animationMaker.Execute();
		}
	}
}
