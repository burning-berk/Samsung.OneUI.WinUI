using System.Threading.Tasks;

namespace Samsung.OneUI.WinUI.Controls;

internal interface ITimePickerListAnimationService : IPickerListAnimationService
{
	Task<bool> IsPeriodVisible();
}
