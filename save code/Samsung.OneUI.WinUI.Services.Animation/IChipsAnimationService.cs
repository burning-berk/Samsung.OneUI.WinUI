using System;
using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Services.Animation;

internal interface IChipsAnimationService : IAnimationService
{
	void AddAnimation(FrameworkElement itemPresenter, FrameworkElement contentPresenter, Action OnCompleted = null);

	void RemoveAnimation(FrameworkElement itemPresenter, FrameworkElement contentPresenter, Action OnCompleted = null);
}
