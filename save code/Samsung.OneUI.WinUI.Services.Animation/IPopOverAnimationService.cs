using System;
using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Services.Animation;

internal interface IPopOverAnimationService : IAnimationService
{
	void OpenAnimation(FrameworkElement menuFlyoutPresenter, double centerX = 0.0, double centerY = 0.0);

	void CloseAnimation(FrameworkElement menuFlyoutPresenter, Action OnCompleted);
}
