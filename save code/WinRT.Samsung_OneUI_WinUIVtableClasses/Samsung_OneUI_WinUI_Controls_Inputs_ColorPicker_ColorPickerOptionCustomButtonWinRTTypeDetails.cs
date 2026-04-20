using System.Runtime.InteropServices;
using ABI.Microsoft.UI.Composition;
using ABI.Microsoft.UI.Xaml;
using ABI.Microsoft.UI.Xaml.Controls;
using ABI.Microsoft.UI.Xaml.Controls.Primitives;

namespace WinRT.Samsung_OneUI_WinUIVtableClasses;

internal sealed class Samsung_OneUI_WinUI_Controls_Inputs_ColorPicker_ColorPickerOptionCustomButtonWinRTTypeDetails : IWinRTExposedTypeDetails
{
	public ComWrappers.ComInterfaceEntry[] GetExposedInterfaces()
	{
		return new ComWrappers.ComInterfaceEntry[8]
		{
			new ComWrappers.ComInterfaceEntry
			{
				IID = IUIElementOverridesMethods.IID,
				Vtable = IUIElementOverridesMethods.AbiToProjectionVftablePtr
			},
			new ComWrappers.ComInterfaceEntry
			{
				IID = IAnimationObjectMethods.IID,
				Vtable = IAnimationObjectMethods.AbiToProjectionVftablePtr
			},
			new ComWrappers.ComInterfaceEntry
			{
				IID = IVisualElementMethods.IID,
				Vtable = IVisualElementMethods.AbiToProjectionVftablePtr
			},
			new ComWrappers.ComInterfaceEntry
			{
				IID = IVisualElement2Methods.IID,
				Vtable = IVisualElement2Methods.AbiToProjectionVftablePtr
			},
			new ComWrappers.ComInterfaceEntry
			{
				IID = IFrameworkElementOverridesMethods.IID,
				Vtable = IFrameworkElementOverridesMethods.AbiToProjectionVftablePtr
			},
			new ComWrappers.ComInterfaceEntry
			{
				IID = IControlOverridesMethods.IID,
				Vtable = IControlOverridesMethods.AbiToProjectionVftablePtr
			},
			new ComWrappers.ComInterfaceEntry
			{
				IID = IContentControlOverridesMethods.IID,
				Vtable = IContentControlOverridesMethods.AbiToProjectionVftablePtr
			},
			new ComWrappers.ComInterfaceEntry
			{
				IID = IToggleButtonOverridesMethods.IID,
				Vtable = IToggleButtonOverridesMethods.AbiToProjectionVftablePtr
			}
		};
	}
}
