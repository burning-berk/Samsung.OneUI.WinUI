using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Controls;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Utils.Extensions;

internal static class TreeViewItemExtension
{
	private const string EXPANDABLE_LIST_ITEM_HEADER_BORDER = "ExpandableListItemHeaderBorder";

	internal static TreeViewList GetTreeView(this TreeViewItem treeViewItem)
	{
		TreeViewList visualParent = UIExtensionsInternal.GetVisualParent<TreeViewList>(treeViewItem);
		if (visualParent == null)
		{
			return null;
		}
		return visualParent;
	}

	internal static void SetFocus(this TreeViewItem sender)
	{
		TreeViewList treeView = sender.GetTreeView();
		if (treeView == null)
		{
			return;
		}
		int num = treeView.Items.Count();
		if (num > 0)
		{
			treeView.UpdateLayout();
			bool flag = IsLastItem(sender, treeView);
			int num2 = treeView.IndexFromContainer(sender);
			ContentControl contentControl = UIExtensionsInternal.FindChildByName<ContentControl>("ExpandableListItemHeaderBorder", sender);
			CornerRadius cornerRadius = new CornerRadius(0.0);
			if (num == 1)
			{
				cornerRadius = new CornerRadius(16.0);
			}
			else if (num2 == 0)
			{
				cornerRadius = new CornerRadius(16.0, 16.0, 0.0, 0.0);
			}
			else if (flag && !sender.IsExpanded)
			{
				cornerRadius = new CornerRadius(0.0, 0.0, 16.0, 16.0);
			}
			if (contentControl != null)
			{
				contentControl.CornerRadius = cornerRadius;
			}
			VisualStateManager.GoToState(sender, "Focused", useTransitions: true);
		}
	}

	private static bool IsLastItem(TreeViewItem sender, TreeViewList treeView)
	{
		object item = treeView.Items[treeView.Items.Count - 1];
		TreeViewItem treeViewItem = treeView.ContainerFromItem(item) as TreeViewItem;
		ExpandableListItemHeader expandableListItemHeader = FindLastExpandableListItemHeader(treeView);
		if (sender == treeViewItem || expandableListItemHeader == sender)
		{
			return true;
		}
		return false;
	}

	private static ExpandableListItemHeader FindLastExpandableListItemHeader(TreeViewList treeView)
	{
		ExpandableListItemHeader result = null;
		foreach (object item in treeView.Items)
		{
			if (treeView.ContainerFromItem(item) is ExpandableListItemHeader expandableListItemHeader)
			{
				result = expandableListItemHeader;
			}
		}
		return result;
	}
}
