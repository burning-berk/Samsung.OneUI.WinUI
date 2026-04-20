using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Animation;
using Samsung.OneUI.WinUI.Utils.Extensions;

namespace Samsung.OneUI.WinUI.Services.Animation;

internal class AnimationMaker : IAnimationMaker
{
	private Storyboard _storyboard;

	public Storyboard Create()
	{
		_storyboard = new Storyboard();
		return _storyboard;
	}

	public void AddAnimation(DependencyObject contentPresenter, string propTarget, TimelineBase timelineBase)
	{
		if (_storyboard == null)
		{
			_storyboard = new Storyboard();
		}
		Timeline timeline = timelineBase.AnimationTimeline.CreateAnimation(timelineBase);
		SetAnimation(contentPresenter, propTarget, timeline);
		_storyboard.Children.Add(timeline);
	}

	public void AddAnimation(DependencyObject contentPresenter, PropertiesEnum propTarget, TimelineBase timelineBase)
	{
		AddAnimation(contentPresenter, propTarget.ToString(), timelineBase);
	}

	public void Execute(Action callback = null)
	{
		if (!(_storyboard == null))
		{
			_storyboard.SafeBegin();
			_storyboard.Completed += delegate
			{
				callback?.Invoke();
			};
		}
	}

	private void SetAnimation(DependencyObject target, string propName, Timeline timelineBase)
	{
		Storyboard.SetTarget(timelineBase, target);
		Storyboard.SetTargetProperty(timelineBase, propName);
	}
}
