using System.Collections.Generic;

namespace Samsung.OneUI.WinUI.Controls;

internal class PickerEntranceAnimationStartParameter
{
	public double VerticalOffSet { get; set; }

	public double PeriodVerticalOffSet { get; set; }

	public bool IsPeriodVisible { get; set; }

	public Queue<int> ColumnsDuration { get; set; }
}
