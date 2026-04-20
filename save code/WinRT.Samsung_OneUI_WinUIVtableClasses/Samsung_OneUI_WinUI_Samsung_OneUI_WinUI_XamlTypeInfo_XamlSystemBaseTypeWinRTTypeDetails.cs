using System.Runtime.InteropServices;
using ABI.Microsoft.UI.Xaml.Markup;

namespace WinRT.Samsung_OneUI_WinUIVtableClasses;

internal sealed class Samsung_OneUI_WinUI_Samsung_OneUI_WinUI_XamlTypeInfo_XamlSystemBaseTypeWinRTTypeDetails : IWinRTExposedTypeDetails
{
	public ComWrappers.ComInterfaceEntry[] GetExposedInterfaces()
	{
		return new ComWrappers.ComInterfaceEntry[1]
		{
			new ComWrappers.ComInterfaceEntry
			{
				IID = IXamlTypeMethods.IID,
				Vtable = IXamlTypeMethods.AbiToProjectionVftablePtr
			}
		};
	}
}
