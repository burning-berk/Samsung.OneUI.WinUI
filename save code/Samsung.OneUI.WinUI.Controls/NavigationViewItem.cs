using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls;

public class NavigationViewItem : Microsoft.UI.Xaml.Controls.NavigationViewItem
{
	public static readonly DependencyProperty SvgIconStyleProperty = DependencyProperty.Register("SvgIconStyle", typeof(Style), typeof(NavigationViewItem), new PropertyMetadata(null));

	public static readonly DependencyProperty PngIconPathProperty = DependencyProperty.Register("PngIconPath", typeof(string), typeof(NavigationViewItem), new PropertyMetadata(null));

	public static readonly DependencyProperty NotificationBadgeProperty = DependencyProperty.Register("NotificationBadge", typeof(BadgeBase), typeof(NavigationViewItem), new PropertyMetadata(null));

	public Style SvgIconStyle
	{
		get
		{
			return (Style)GetValue(SvgIconStyleProperty);
		}
		set
		{
			SetValue(SvgIconStyleProperty, value);
		}
	}

	public string PngIconPath
	{
		get
		{
			return (string)GetValue(PngIconPathProperty);
		}
		set
		{
			SetValue(PngIconPathProperty, value);
		}
	}

	public BadgeBase NotificationBadge
	{
		get
		{
			return (BadgeBase)GetValue(NotificationBadgeProperty);
		}
		set
		{
			SetValue(NotificationBadgeProperty, value);
		}
	}

	public NavigationViewItem()
	{
		base.DefaultStyleKey = typeof(NavigationViewItem);
	}

	protected override AutomationPeer OnCreateAutomationPeer()
	{
		return new NavigationViewItemAutomationPeer(this);
	}
}
