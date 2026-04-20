using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class ColorInfo
{
	public string Name { get; set; }

	public string Description { get; set; }

	public string HexValue { get; set; }

	public SolidColorBrush ColorBrush
	{
		get
		{
			if (HexValue == null)
			{
				return null;
			}
			return ColorsHelpers.ConvertColorHex(HexValue);
		}
	}

	public ColorInfo(string description, int bright, string hexValue)
	{
		Name = GetName(description, bright);
		HexValue = hexValue;
		Description = description;
	}

	public ColorInfo(string description, string hexValue)
	{
		Description = description;
		HexValue = hexValue;
	}

	private static string GetName(string description, int bright)
	{
		string localized = "DREAM_IDLE_TBOPT_BLACK".GetLocalized();
		if (bright == 0 && description != localized)
		{
			return description;
		}
		return $"{description}, {bright}";
	}
}
