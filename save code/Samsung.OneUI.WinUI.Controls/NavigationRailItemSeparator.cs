using System;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls;

[Obsolete("NavigationRailItemSeparator is deprecated, please use NavigationViewItemSeparator instead.")]
public class NavigationRailItemSeparator : Microsoft.UI.Xaml.Controls.NavigationViewItemSeparator
{
	public NavigationRailItemSeparator()
	{
		base.DefaultStyleKey = typeof(NavigationRailItemSeparator);
	}
}
