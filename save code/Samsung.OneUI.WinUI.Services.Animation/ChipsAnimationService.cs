using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Animation;
using Samsung.OneUI.WinUI.Services.Animation.Timelines;
using Windows.Foundation;

namespace Samsung.OneUI.WinUI.Services.Animation;

internal class ChipsAnimationService : IChipsAnimationService, IAnimationService
{
	private readonly IAnimationMaker _animationMaker;

	public ChipsAnimationService()
	{
		_animationMaker = new AnimationMaker();
	}

	public void AddAnimation(FrameworkElement itemPresenter, FrameworkElement contentPresenter, Action OnCompleted = null)
	{
		if (!(itemPresenter == null) && !(contentPresenter == null))
		{
			Storyboard storyboard = _animationMaker.Create();
			TimelineBase timelineBase = new TimelineBase
			{
				From = 0,
				To = 1,
				BeginTime = TimeSpan.FromMilliseconds(0.0),
				EndTime = TimeSpan.FromMilliseconds(150.0),
				KeySpline = new KeySpline
				{
					ControlPoint1 = new Point(0f, 0f),
					ControlPoint2 = new Point(1f, 1f)
				},
				AnimationTimeline = new DoubleAnimationTimeline()
			};
			_animationMaker.AddAnimation(itemPresenter, PropertiesEnum.Opacity, timelineBase);
			TimelineBase timelineBase2 = new TimelineBase
			{
				From = 0,
				To = 1,
				BeginTime = TimeSpan.FromMilliseconds(100.0),
				EndTime = TimeSpan.FromMilliseconds(300.0),
				KeySpline = new KeySpline
				{
					ControlPoint1 = new Point(0f, 0f),
					ControlPoint2 = new Point(1f, 1f)
				},
				AnimationTimeline = new DoubleAnimationTimeline()
			};
			_animationMaker.AddAnimation(contentPresenter, PropertiesEnum.Opacity, timelineBase2);
			TimelineBase timelineBase3 = new TimelineBase
			{
				From = 0,
				To = itemPresenter.DesiredSize.Width,
				BeginTime = TimeSpan.FromMilliseconds(0.0),
				EndTime = TimeSpan.FromMilliseconds(450.0),
				KeySpline = new KeySpline
				{
					ControlPoint1 = new Point(0.22, 0.25),
					ControlPoint2 = new Point(0f, 1f)
				},
				AnimationTimeline = new DoubleAnimationTimeline()
			};
			_animationMaker.AddAnimation(itemPresenter, PropertiesEnum.Width, timelineBase3);
			storyboard.FillBehavior = FillBehavior.Stop;
			_animationMaker.Execute(OnCompleted);
		}
	}

	public void RemoveAnimation(FrameworkElement itemPresenter, FrameworkElement contentPresenter, Action OnCompleted = null)
	{
		if (!(itemPresenter == null))
		{
			_animationMaker.Create();
			TimelineBase timelineBase = new TimelineBase
			{
				From = 1,
				To = 0,
				BeginTime = TimeSpan.FromMilliseconds(0.0),
				EndTime = TimeSpan.FromMilliseconds(150.0),
				KeySpline = new KeySpline
				{
					ControlPoint1 = new Point(0f, 0f),
					ControlPoint2 = new Point(1f, 1f)
				},
				AnimationTimeline = new DoubleAnimationTimeline()
			};
			_animationMaker.AddAnimation(itemPresenter, PropertiesEnum.Opacity, timelineBase);
			TimelineBase timelineBase2 = new TimelineBase
			{
				From = 1,
				To = 0,
				BeginTime = TimeSpan.FromMilliseconds(0.0),
				EndTime = TimeSpan.FromMilliseconds(100.0),
				KeySpline = new KeySpline
				{
					ControlPoint1 = new Point(0f, 0f),
					ControlPoint2 = new Point(1f, 1f)
				},
				AnimationTimeline = new DoubleAnimationTimeline()
			};
			_animationMaker.AddAnimation(contentPresenter, PropertiesEnum.Opacity, timelineBase2);
			TimelineBase timelineBase3 = new TimelineBase
			{
				From = contentPresenter.ActualWidth,
				To = 0,
				BeginTime = TimeSpan.FromMilliseconds(0.0),
				EndTime = TimeSpan.FromMilliseconds(450.0),
				KeySpline = new KeySpline
				{
					ControlPoint1 = new Point(0.22, 0.25),
					ControlPoint2 = new Point(0f, 1f)
				},
				AnimationTimeline = new DoubleAnimationTimeline()
			};
			_animationMaker.AddAnimation(itemPresenter, PropertiesEnum.Width, timelineBase3);
			TimelineBase timelineBase4 = new TimelineBase
			{
				From = itemPresenter.Margin,
				To = new Thickness(itemPresenter.Margin.Left, itemPresenter.Margin.Top, 0.0, itemPresenter.Margin.Bottom),
				BeginTime = TimeSpan.FromMilliseconds(0.0),
				EndTime = TimeSpan.FromMilliseconds(200.0),
				KeySpline = new KeySpline
				{
					ControlPoint1 = new Point(0.22, 0.25),
					ControlPoint2 = new Point(0f, 1f)
				},
				AnimationTimeline = new ObjectAnimationTimeline()
			};
			_animationMaker.AddAnimation(itemPresenter, PropertiesEnum.Margin, timelineBase4);
			_animationMaker.Execute(OnCompleted);
		}
	}
}
