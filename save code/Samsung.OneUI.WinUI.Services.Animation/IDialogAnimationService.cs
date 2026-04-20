using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Services.Animation;

internal interface IDialogAnimationService : IAnimationService
{
	void RunBackgroundOpacity(FrameworkElement FrameworkElement, double finalOpacity);
}
