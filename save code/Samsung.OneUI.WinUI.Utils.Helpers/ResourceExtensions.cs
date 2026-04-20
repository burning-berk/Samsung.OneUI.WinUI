using Microsoft.UI.Xaml;
using Microsoft.Windows.ApplicationModel.Resources;

namespace Samsung.OneUI.WinUI.Utils.Helpers;

internal static class ResourceExtensions
{
	private const string Resource = "Samsung.OneUI.WinUI/Resources/";

	private static readonly ResourceManager resourceManager = new ResourceManager();

	public static Style GetStyle(this string resourceKey)
	{
		return (Style)Application.Current.Resources[resourceKey];
	}

	public static object GetKey(this string resourceKey)
	{
		return Application.Current.Resources[resourceKey];
	}

	public static DataTemplate GetDataTemplate(this string resourceKey)
	{
		return (DataTemplate)Application.Current.Resources[resourceKey];
	}

	public static string GetLocalized(this string resourceKey)
	{
		return resourceManager.MainResourceMap.GetValue("Samsung.OneUI.WinUI/Resources/" + resourceKey).ValueAsString;
	}
}
