using System.Runtime.InteropServices;
using ABI.Microsoft.UI.Xaml.Data;

namespace WinRT.Samsung_OneUI_WinUIVtableClasses;

internal sealed class Samsung_OneUI_WinUI_Converters_DatePickerTextScaleSizeConverterWinRTTypeDetails : IWinRTExposedTypeDetails
{
	public ComWrappers.ComInterfaceEntry[] GetExposedInterfaces()
	{
		return new ComWrappers.ComInterfaceEntry[1]
		{
			new ComWrappers.ComInterfaceEntry
			{
				IID = IValueConverterMethods.IID,
				Vtable = IValueConverterMethods.AbiToProjectionVftablePtr
			}
		};
	}
}
