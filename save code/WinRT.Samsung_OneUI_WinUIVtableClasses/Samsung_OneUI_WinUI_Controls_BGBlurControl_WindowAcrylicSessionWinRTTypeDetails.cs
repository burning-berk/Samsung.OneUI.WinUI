using System.Runtime.InteropServices;
using ABI.System;

namespace WinRT.Samsung_OneUI_WinUIVtableClasses;

internal sealed class Samsung_OneUI_WinUI_Controls_BGBlurControl_WindowAcrylicSessionWinRTTypeDetails : IWinRTExposedTypeDetails
{
	public ComWrappers.ComInterfaceEntry[] GetExposedInterfaces()
	{
		return new ComWrappers.ComInterfaceEntry[1]
		{
			new ComWrappers.ComInterfaceEntry
			{
				IID = IDisposableMethods.IID,
				Vtable = IDisposableMethods.AbiToProjectionVftablePtr
			}
		};
	}
}
