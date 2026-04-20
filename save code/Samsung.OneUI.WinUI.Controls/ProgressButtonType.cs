using System;

namespace Samsung.OneUI.WinUI.Controls;

[Obsolete("This enum is deprecated, please use property \"IsProgressEnabled\" from Contained, ContainedColored, ContainedBody or ContainedBodyColored. This component will be removed soon.", false)]
public enum ProgressButtonType
{
	Flat,
	Contained,
	ContainedColored,
	ContainedBody,
	ContainedBodyColored
}
