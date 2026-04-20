using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Samsung.OneUI.WinUI.Services.Animation.Timelines;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.Foundation;

namespace Samsung.OneUI.WinUI.Services.Animation;

internal class FlyoutAnimationService : IFlyoutAnimationService, IAnimationService
{
	private readonly IAnimationMaker _animationMaker;

	public FlyoutAnimationService()
	{
		_animationMaker = new AnimationMaker();
	}

	public void OpenAnimation(FrameworkElement menuFlyoutPresenter, double centerX = 0.0, double centerY = 0.0)
	{
		if (!(menuFlyoutPresenter == null))
		{
			_animationMaker.Create();
			TimelineBase timelineBase = new TimelineBase
			{
				From = 0,
				To = 1,
				BeginTime = TimeSpan.FromMilliseconds(0.0),
				EndTime = TimeSpan.FromMilliseconds(100.0),
				AnimationTimeline = new DoubleAnimationTimeline()
			};
			_animationMaker.AddAnimation(menuFlyoutPresenter, PropertiesEnum.Opacity, timelineBase);
			TimelineBase timelineBase2 = new TimelineBase
			{
				ZeroTimeValue = 1,
				From = 0.8,
				To = 1,
				BeginTime = TimeSpan.FromMilliseconds(1.0),
				EndTime = TimeSpan.FromMilliseconds(350.0),
				KeySpline = new KeySpline
				{
					ControlPoint1 = new Point(0.22, 0.25),
					ControlPoint2 = new Point(0f, 1f)
				},
				AnimationTimeline = new DoubleAnimationTimeline()
			};
			CompositeTransform transform = menuFlyoutPresenter.GetTransform<CompositeTransform>();
			transform.CenterX = centerX;
			transform.CenterY = centerY;
			_animationMaker.AddAnimation(transform, PropertiesEnum.ScaleX, timelineBase2);
			_animationMaker.AddAnimation(transform, PropertiesEnum.ScaleY, timelineBase2);
			_animationMaker.Execute();
		}
	}

	public void CloseAnimation(FrameworkElement menuFlyoutPresenter, Action OnCompleted)
	{
		if (!(menuFlyoutPresenter == null))
		{
			new DoubleAnimationTimeline();
			_animationMaker.Create();
			TimelineBase timelineBase = new TimelineBase
			{
				From = 1,
				To = 0,
				BeginTime = TimeSpan.FromMilliseconds(0.0),
				EndTime = TimeSpan.FromMilliseconds(100.0),
				AnimationTimeline = new DoubleAnimationTimeline()
			};
			_animationMaker.AddAnimation(menuFlyoutPresenter, PropertiesEnum.Opacity, timelineBase);
			TimelineBase timelineBase2 = new TimelineBase
			{
				From = 1,
				To = 0.8,
				BeginTime = TimeSpan.FromMilliseconds(0.0),
				EndTime = TimeSpan.FromMilliseconds(150.0),
				KeySpline = new KeySpline
				{
					ControlPoint1 = new Point(0.22, 0.25),
					ControlPoint2 = new Point(0f, 1f)
				},
				AnimationTimeline = new DoubleAnimationTimeline()
			};
			CompositeTransform transform = menuFlyoutPresenter.GetTransform<CompositeTransform>();
			_animationMaker.AddAnimation(transform, PropertiesEnum.ScaleX, timelineBase2);
			_animationMaker.AddAnimation(transform, PropertiesEnum.ScaleY, timelineBase2);
			_animationMaker.Execute(OnCompleted);
		}
	}
}
