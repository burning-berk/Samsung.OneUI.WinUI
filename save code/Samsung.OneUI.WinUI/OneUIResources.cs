using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Markup;

namespace Samsung.OneUI.WinUI;

public sealed class OneUIResources : ResourceDictionary, IComponentConnector
{
	private const string LIGHT_COLOR_RESOURCES = "ms-appx:///Samsung.OneUI.WinUI/Tokens/Colors/Light/Colors.xaml";

	private const string DARK_COLOR_RESOURCES = "ms-appx:///Samsung.OneUI.WinUI/Tokens/Colors/Dark/Colors.xaml";

	private const string GENERIC_RESOURCES = "ms-appx:///Samsung.OneUI.WinUI/Themes/Generic.xaml";

	private const string LIGHT_THEME = "Light";

	private const string DARK_THEME = "Dark";

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private bool _contentLoaded;

	public OneUIResources()
	{
		InitializeComponent();
		ImportResources();
	}

	private void ImportResources()
	{
		Application.Current.Resources.ThemeDictionaries.Add("Light", new ResourceDictionary
		{
			Source = new Uri("ms-appx:///Samsung.OneUI.WinUI/Tokens/Colors/Light/Colors.xaml")
		});
		Application.Current.Resources.ThemeDictionaries.Add("Dark", new ResourceDictionary
		{
			Source = new Uri("ms-appx:///Samsung.OneUI.WinUI/Tokens/Colors/Dark/Colors.xaml")
		});
		Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
		{
			Source = new Uri("ms-appx:///Samsung.OneUI.WinUI/Themes/Generic.xaml")
		});
		base.ThemeDictionaries.Add("Light", new ResourceDictionary
		{
			Source = new Uri("ms-appx:///Samsung.OneUI.WinUI/Tokens/Colors/Light/Colors.xaml")
		});
		base.ThemeDictionaries.Add("Dark", new ResourceDictionary
		{
			Source = new Uri("ms-appx:///Samsung.OneUI.WinUI/Tokens/Colors/Dark/Colors.xaml")
		});
		base.MergedDictionaries.Add(new ResourceDictionary
		{
			Source = new Uri("ms-appx:///Samsung.OneUI.WinUI/Themes/Generic.xaml")
		});
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("ms-appx:///Samsung.OneUI.WinUI/OneUIResources.xaml");
			Application.LoadComponent(this, resourceLocator, ComponentResourceLocation.Nested);
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void Connect(int connectionId, object target)
	{
		_contentLoaded = true;
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public IComponentConnector GetBindingConnector(int connectionId, object target)
	{
		return null;
	}
}
