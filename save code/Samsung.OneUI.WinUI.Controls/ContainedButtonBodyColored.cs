using System;

namespace Samsung.OneUI.WinUI.Controls;

[Obsolete("This component is deprecated, please use property \"Type\" and \"Size\" from ContainedButton. This component will be removed soon.", false)]
public class ContainedButtonBodyColored : ContainedButtonBase
{
	public ContainedButtonBodyColored()
	{
		base.DefaultStyleKey = typeof(ContainedButtonBodyColored);
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
	}
}
