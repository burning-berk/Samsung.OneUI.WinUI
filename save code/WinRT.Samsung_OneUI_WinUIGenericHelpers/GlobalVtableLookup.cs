using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System;
using ABI.System.Collections;

namespace WinRT.Samsung_OneUI_WinUIGenericHelpers;

internal static class GlobalVtableLookup
{
	[ModuleInitializer]
	internal static void InitializeGlobalVtableLookup()
	{
		ComWrappersSupport.RegisterTypeComInterfaceEntriesLookup(LookupVtableEntries);
		ComWrappersSupport.RegisterTypeRuntimeClassNameLookup(LookupRuntimeClassName);
	}

	private static ComWrappers.ComInterfaceEntry[] LookupVtableEntries(System.Type type)
	{
		switch (type.ToString())
		{
		case "System.Threading.Tasks.Task`1[System.Object]":
			return new ComWrappers.ComInterfaceEntry[1]
			{
				new ComWrappers.ComInterfaceEntry
				{
					IID = IDisposableMethods.IID,
					Vtable = IDisposableMethods.AbiToProjectionVftablePtr
				}
			};
		case "System.Collections.Specialized.SingleItemReadOnlyList":
		case "System.Collections.Specialized.ReadOnlyList":
			return new ComWrappers.ComInterfaceEntry[2]
			{
				new ComWrappers.ComInterfaceEntry
				{
					IID = IListMethods.IID,
					Vtable = IListMethods.AbiToProjectionVftablePtr
				},
				new ComWrappers.ComInterfaceEntry
				{
					IID = IEnumerableMethods.IID,
					Vtable = IEnumerableMethods.AbiToProjectionVftablePtr
				}
			};
		default:
			return null;
		}
	}

	private static string LookupRuntimeClassName(System.Type type)
	{
		switch (type.ToString())
		{
		case "System.Threading.Tasks.Task`1[System.Object]":
			return "Windows.Foundation.IClosable";
		case "System.Collections.Specialized.SingleItemReadOnlyList":
		case "System.Collections.Specialized.ReadOnlyList":
			return "Microsoft.UI.Xaml.Interop.IBindableVector";
		default:
			return null;
		}
	}
}
