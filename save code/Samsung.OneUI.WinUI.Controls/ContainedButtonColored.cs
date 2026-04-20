using System;

namespace Samsung.OneUI.WinUI.Controls;

[Obsolete("This component is deprecated, please use property \"Type\" and \"Size\" from ContainedButton. This component will be removed soon.", false)]
public class ContainedButtonColored : ContainedButtonBase
{
	public ContainedButtonColored()
	{
		base.DefaultStyleKey = typeof(ContainedButtonColored);
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
	}
}
