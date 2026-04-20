using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Markup;
using WinRT;
using WinRT.Samsung_OneUI_WinUIVtableClasses;

namespace Samsung.OneUI.WinUI.Controls;

[WinRTRuntimeClassName("Microsoft.UI.Xaml.IUIElementOverrides")]
[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_TimePickerListDialogContentWinRTTypeDetails))]
public sealed class OneUIContentDialogContent : Page, IComponentConnector
{
	private const double SCALE_125 = 1.25;

	private ScrollViewer scrollViewer;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private Grid OneUIContentDialogGridContent;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private bool _contentLoaded;

	public ScrollViewer ScrollViewer
	{
		get
		{
			return scrollViewer;
		}
		set
		{
			if (scrollViewer != value)
			{
				scrollViewer = value;
				AddScrollViewerInGrid();
			}
		}
	}

	public OneUIContentDialogContent()
	{
		InitializeComponent();
		base.Loaded += OneUIContentDialogContent_Loaded;
		base.Unloaded += OneUIContentDialogContent_Unloaded;
	}

	private void OneUIContentDialogContent_Loaded(object sender, RoutedEventArgs e)
	{
		UpdateDPIScrollViewerMargin();
		if (base.XamlRoot != null)
		{
			base.XamlRoot.Changed += DpiChangedEvent;
		}
	}

	private void OneUIContentDialogContent_Unloaded(object sender, RoutedEventArgs e)
	{
		if (base.XamlRoot != null)
		{
			base.XamlRoot.Changed -= DpiChangedEvent;
		}
	}

	private void DpiChangedEvent(XamlRoot sender, XamlRootChangedEventArgs args)
	{
		UpdateDPIScrollViewerMargin();
	}

	private void UpdateDPIScrollViewerMargin()
	{
		if (ScrollViewer != null && base.XamlRoot != null)
		{
			if (base.XamlRoot.RasterizationScale >= 1.25)
			{
				ScrollViewer.Padding = new Thickness(0.0, -2.0, 0.0, 0.0);
			}
			else
			{
				ScrollViewer.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
			}
		}
	}

	private void AddScrollViewerInGrid()
	{
		if (OneUIContentDialogGridContent != null)
		{
			OneUIContentDialogGridContent.Children.Clear();
			OneUIContentDialogGridContent.Children.Add(ScrollViewer);
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("ms-appx:///Samsung.OneUI.WinUI/Controls/DialogsAndFlyouts/DIalog/OneUIContentDialogContent.xaml");
			Application.LoadComponent(this, resourceLocator, ComponentResourceLocation.Nested);
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void Connect(int connectionId, object target)
	{
		if (connectionId == 2)
		{
			OneUIContentDialogGridContent = target.As<Grid>();
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
