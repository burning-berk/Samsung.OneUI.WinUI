using System;
using Microsoft.UI.Xaml;
using Samsung.OneUI.WinUI.Services.Animation.Timelines;

namespace Samsung.OneUI.WinUI.Services.Animation;

internal class DialogAnimationService : IDialogAnimationService, IAnimationService
{
	private readonly IAnimationMaker _animationMaker;

	public DialogAnimationService()
	{
		_animationMaker = new AnimationMaker();
	}

	public void RunBackgroundOpacity(FrameworkElement FrameworkElement, double finalOpacity)
	{
		if (!(FrameworkElement == null))
		{
			_animationMaker.Create();
			TimelineBase timelineBase = new TimelineBase
			{
				From = FrameworkElement.Opacity,
				To = finalOpacity,
				BeginTime = TimeSpan.FromMilliseconds(0.0),
				EndTime = TimeSpan.FromMilliseconds(150.0),
				AnimationTimeline = new DoubleAnimationTimeline()
			};
			_animationMaker.AddAnimation(FrameworkElement, PropertiesEnum.Opacity, timelineBase);
			_animationMaker.Execute();
		}
	}
}
