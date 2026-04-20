using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class ListFlyoutSeparator : MenuFlyoutSeparator
{
	private const string PART_STYLE_SEPARATOR = "OneUIListFlyoutSeparator";

	private const string LINE_NAME = "DottedLine";

	private Line dottedLine;

	public event EventHandler<RoutedEventArgs> LineLoaded;

	public ListFlyoutSeparator()
	{
		base.DefaultStyleKey = typeof(ListFlyoutSeparator);
		base.Style = "OneUIListFlyoutSeparator".GetStyle();
		base.Loaded += MenuListFlyoutSeparator_Loaded;
	}

	private void MenuListFlyoutSeparator_Loaded(object sender, RoutedEventArgs e)
	{
		if (dottedLine != null)
		{
			this.LineLoaded?.Invoke(sender, e);
		}
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		dottedLine = GetTemplateChild("DottedLine") as Line;
	}

	public void SetSeparatorEndPoint(double value)
	{
		if (dottedLine != null)
		{
			dottedLine.X2 = value;
		}
	}
}
