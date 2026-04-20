using System;
using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Services.Animation;

internal interface IMultiPaneAnimationService : IAnimationService
{
	void ExecuteAnimation(FrameworkElement panel, double from, double to, Action OnCompleted);
}
