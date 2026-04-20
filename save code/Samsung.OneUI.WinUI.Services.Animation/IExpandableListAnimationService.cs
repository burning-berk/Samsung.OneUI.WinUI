using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Services.Animation;

internal interface IExpandableListAnimationService : IAnimationService
{
	void OpenAnimation(FrameworkElement frameworkElement);
}
