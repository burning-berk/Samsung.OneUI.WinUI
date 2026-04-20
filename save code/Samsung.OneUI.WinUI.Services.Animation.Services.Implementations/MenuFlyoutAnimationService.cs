using System;
using Microsoft.UI.Xaml;
using Samsung.OneUI.WinUI.Services.Animation.Timelines;

namespace Samsung.OneUI.WinUI.Services.Animation.Services.Implementations;

internal class MenuFlyoutAnimationService : IFlyoutAnimationService, IAnimationService
{
	private readonly IAnimationMaker _animationMaker;

	public MenuFlyoutAnimationService()
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
				EndTime = TimeSpan.FromMilliseconds(200.0),
				AnimationTimeline = new DoubleAnimationTimeline()
			};
			_animationMaker.AddAnimation(menuFlyoutPresenter, PropertiesEnum.Opacity, timelineBase);
			_animationMaker.Execute();
		}
	}

	public void CloseAnimation(FrameworkElement menuFlyoutPresenter, Action OnCompleted)
	{
	}
}
