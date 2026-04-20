using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Animation;

namespace Samsung.OneUI.WinUI.Services.Animation;

internal interface IAnimationMaker
{
	Storyboard Create();

	void Execute(Action callback = null);

	void AddAnimation(DependencyObject contentPresenter, string propTarget, TimelineBase timelineBase);

	void AddAnimation(DependencyObject contentPresenter, PropertiesEnum propTarget, TimelineBase timelineBase);
}
