using System;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls;

[Obsolete("NavigationRailItemHeader is deprecated, please use NavigationViewItemHeader instead.")]
public class NavigationRailItemHeader : Microsoft.UI.Xaml.Controls.NavigationViewItemHeader
{
	public NavigationRailItemHeader()
	{
		base.DefaultStyleKey = typeof(NavigationRailItemHeader);
	}
}
