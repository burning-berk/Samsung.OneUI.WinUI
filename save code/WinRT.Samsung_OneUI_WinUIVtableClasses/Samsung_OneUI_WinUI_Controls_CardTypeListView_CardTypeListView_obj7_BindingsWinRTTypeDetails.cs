using System.Runtime.InteropServices;
using ABI.Microsoft.UI.Xaml;
using ABI.Microsoft.UI.Xaml.Markup;

namespace WinRT.Samsung_OneUI_WinUIVtableClasses;

internal sealed class Samsung_OneUI_WinUI_Controls_CardTypeListView_CardTypeListView_obj7_BindingsWinRTTypeDetails : IWinRTExposedTypeDetails
{
	public ComWrappers.ComInterfaceEntry[] GetExposedInterfaces()
	{
		return new ComWrappers.ComInterfaceEntry[3]
		{
			new ComWrappers.ComInterfaceEntry
			{
				IID = IDataTemplateExtensionMethods.IID,
				Vtable = IDataTemplateExtensionMethods.AbiToProjectionVftablePtr
			},
			new ComWrappers.ComInterfaceEntry
			{
				IID = IDataTemplateComponentMethods.IID,
				Vtable = IDataTemplateComponentMethods.AbiToProjectionVftablePtr
			},
			new ComWrappers.ComInterfaceEntry
			{
				IID = IComponentConnectorMethods.IID,
				Vtable = IComponentConnectorMethods.AbiToProjectionVftablePtr
			}
		};
	}
}
