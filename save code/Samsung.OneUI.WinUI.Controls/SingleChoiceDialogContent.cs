using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Markup;
using Windows.System;
using WinRT;
using WinRT.Samsung_OneUI_WinUIVtableClasses;

namespace Samsung.OneUI.WinUI.Controls;

[WinRTRuntimeClassName("Microsoft.UI.Xaml.IUIElementOverrides")]
[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_TimePickerListDialogContentWinRTTypeDetails))]
public sealed class SingleChoiceDialogContent : Page, IComponentConnector
{
	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private ListViewCustom ListView1;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private bool _contentLoaded;

	public event EventHandler<string> OptionSelected;

	public SingleChoiceDialogContent(List<string> options = null)
	{
		if (options == null)
		{
			throw new ArgumentNullException("options", "The options list cannot be null");
		}
		InitializeComponent();
		ListView1.ItemsSource = new ObservableCollection<string>(options);
		ListView1.ItemClick += ListView1_ItemClick;
		ListView1.PreviewKeyDown += ListView1_PreviewKeyDown;
	}

	private void ListView1_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
	{
		if (e.Key == VirtualKey.Enter)
		{
			e.Handled = true;
			InvokeSelectionEvent(ListView1.SelectedItem);
		}
	}

	private void ListView1_ItemClick(object sender, ItemClickEventArgs e)
	{
		InvokeSelectionEvent(e.ClickedItem);
	}

	private void InvokeSelectionEvent(object clickedItem)
	{
		if (clickedItem != null)
		{
			this.OptionSelected(this, clickedItem.ToString());
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("ms-appx:///Samsung.OneUI.WinUI/Controls/DialogsAndFlyouts/SingleChoice/SingleChoiceDialogContent.xaml");
			Application.LoadComponent(this, resourceLocator, ComponentResourceLocation.Nested);
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void Connect(int connectionId, object target)
	{
		if (connectionId == 12)
		{
			ListView1 = target.As<ListViewCustom>();
		}
		_contentLoaded = true;
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public IComponentConnector GetBindingConnector(int connectionId, object target)
	{
		return null;
	}
}
