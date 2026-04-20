using Samsung.OneUI.WinUI.Controls;

namespace Samsung.OneUI.WinUI.Services.Animation;

internal interface IDatePickerAnimationService : IAnimationService
{
	void CreateTransitionAnimation(TransitionType transitionType);
}
