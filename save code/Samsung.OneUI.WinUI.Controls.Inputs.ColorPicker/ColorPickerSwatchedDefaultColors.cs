using System.Collections.Generic;
using System.Linq;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker;

internal class ColorPickerSwatchedDefaultColors
{
	private readonly List<ColorInfo> _defaultColorsInfo = new List<ColorInfo>();

	private static ColorPickerSwatchedDefaultColors _instance;

	private ColorPickerSwatchedDefaultColors()
	{
		InitializeDefaultColors();
	}

	public static ColorPickerSwatchedDefaultColors GetInstance()
	{
		if (_instance == null)
		{
			_instance = new ColorPickerSwatchedDefaultColors();
		}
		return _instance;
	}

	public List<ColorInfo> GetList()
	{
		return _defaultColorsInfo;
	}

	public ColorInfo TryGetColorInfoFromDefaultList(string hexadecimal, string description)
	{
		return _defaultColorsInfo.FirstOrDefault((ColorInfo defaultColor) => defaultColor.ColorBrush.Color.ToString().Equals(hexadecimal)) ?? new ColorInfo(description, hexadecimal);
	}

	private void InitializeDefaultColors()
	{
		_defaultColorsInfo.AddRange(CreateGrayScaleColors());
		_defaultColorsInfo.AddRange(CreateRedColors());
		_defaultColorsInfo.AddRange(CreateOrangeColors());
		_defaultColorsInfo.AddRange(CreateYellowColors());
		_defaultColorsInfo.AddRange(CreateGreenColors());
		_defaultColorsInfo.AddRange(CreateSpringGreenColors());
		_defaultColorsInfo.AddRange(CreateCyanColors());
		_defaultColorsInfo.AddRange(CreateAzureColors());
		_defaultColorsInfo.AddRange(CreateBlueColors());
		_defaultColorsInfo.AddRange(CreateVioletColors());
		_defaultColorsInfo.AddRange(CreateMagentaColors());
	}

	private IEnumerable<ColorInfo> CreateGrayScaleColors()
	{
		return new List<ColorInfo>
		{
			new ColorInfo("DREAM_IDLE_TBOPT_WHITE".GetLocalized(), 100, "#FFFFFF"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_GRAY".GetLocalized(), 80, "#CCCCCC"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_GRAY".GetLocalized(), 70, "#B3B3B3"),
			new ColorInfo("DREAM_IDLE_TBOPT_GRAY".GetLocalized(), 60, "#999999"),
			new ColorInfo("DREAM_IDLE_TBOPT_GRAY".GetLocalized(), 51, "#828282"),
			new ColorInfo("DREAM_IDLE_TBOPT_GRAY".GetLocalized(), 40, "#666666"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_GRAY".GetLocalized(), 30, "#4D4D4D"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_GRAY".GetLocalized(), 20, "#333333"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_GRAY".GetLocalized(), 10, "#1A1A1A"),
			new ColorInfo("DREAM_IDLE_TBOPT_BLACK".GetLocalized(), 0, "#000000")
		};
	}

	private IEnumerable<ColorInfo> CreateRedColors()
	{
		return new List<ColorInfo>
		{
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_RED".GetLocalized(), 83, "#FFA8A8"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_RED".GetLocalized(), 71, "#FF6B6B"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_RED".GetLocalized(), 62, "#FF3D3D"),
			new ColorInfo("DREAM_IDLE_TBOPT_RED/Text".GetLocalized(), 54, "#FF1414"),
			new ColorInfo("DREAM_IDLE_TBOPT_RED/Text".GetLocalized(), 0, "#FF0000"),
			new ColorInfo("DREAM_IDLE_TBOPT_RED/Text".GetLocalized(), 49, "#FA0000"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_RED".GetLocalized(), 43, "#DB0000"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_RED".GetLocalized(), 33, "#A80000"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_RED".GetLocalized(), 18, "#5C0000"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_RED".GetLocalized(), 10, "#330000")
		};
	}

	private IEnumerable<ColorInfo> CreateOrangeColors()
	{
		return new List<ColorInfo>
		{
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_ORANGE".GetLocalized(), 83, "#FFD4A8"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_ORANGE".GetLocalized(), 71, "#FFB56B"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_ORANGE".GetLocalized(), 61, "#FF9C38"),
			new ColorInfo("DREAM_IDLE_TBOPT_ORANGE".GetLocalized(), 53, "#FF880F"),
			new ColorInfo("DREAM_IDLE_TBOPT_ORANGE".GetLocalized(), 0, "#FF8000"),
			new ColorInfo("DREAM_IDLE_TBOPT_ORANGE".GetLocalized(), 49, "#FA7D00"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_ORANGE".GetLocalized(), 43, "#DB6E00"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_ORANGE".GetLocalized(), 33, "#A85400"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_ORANGE".GetLocalized(), 18, "#5C2E00"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_ORANGE".GetLocalized(), 10, "#331A00")
		};
	}

	private IEnumerable<ColorInfo> CreateYellowColors()
	{
		return new List<ColorInfo>
		{
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_YELLOW".GetLocalized(), 83, "#FFFFA8"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_YELLOW".GetLocalized(), 70, "#FFFF66"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_YELLOW".GetLocalized(), 61, "#FFFF38"),
			new ColorInfo("DREAM_IDLE_TBOPT_YELLOW".GetLocalized(), 0, "#FFFF00"),
			new ColorInfo("DREAM_IDLE_TBOPT_YELLOW".GetLocalized(), 0, "#FAFC00"),
			new ColorInfo("DREAM_IDLE_TBOPT_YELLOW".GetLocalized(), 49, "#FAFA00"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_YELLOW".GetLocalized(), 43, "#DBDB00"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_YELLOW".GetLocalized(), 32, "#A3A300"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_YELLOW".GetLocalized(), 18, "#5C5C00"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_YELLOW".GetLocalized(), 10, "#333300")
		};
	}

	private IEnumerable<ColorInfo> CreateGreenColors()
	{
		return new List<ColorInfo>
		{
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_GREEN".GetLocalized(), 83, "#A8FFA8"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_GREEN".GetLocalized(), 70, "#66FF66"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_GREEN".GetLocalized(), 61, "#38FF38"),
			new ColorInfo("DREAM_IDLE_TBOPT_GREEN/Text".GetLocalized(), 52, "#0AFF0A"),
			new ColorInfo("DREAM_IDLE_TBOPT_GREEN/Text".GetLocalized(), 0, "#00FF00"),
			new ColorInfo("DREAM_IDLE_TBOPT_GREEN/Text".GetLocalized(), 49, "#00FA00"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_GREEN".GetLocalized(), 43, "#00DB00"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_GREEN".GetLocalized(), 32, "#00A300"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_GREEN".GetLocalized(), 18, "#005C00"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_GREEN".GetLocalized(), 10, "#003300")
		};
	}

	private IEnumerable<ColorInfo> CreateSpringGreenColors()
	{
		return new List<ColorInfo>
		{
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_SPRING_GREEN".GetLocalized(), 83, "#A8FFCB"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_SPRING_GREEN".GetLocalized(), 70, "#66FFA3"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_SPRING_GREEN".GetLocalized(), 61, "#38FF88"),
			new ColorInfo("DREAM_IDLE_TBOPT_SPRING_GREEN".GetLocalized(), 53, "#0FFF6F"),
			new ColorInfo("DREAM_IDLE_TBOPT_SPRING_GREEN".GetLocalized(), 0, "#00FF66"),
			new ColorInfo("DREAM_IDLE_TBOPT_SPRING_GREEN".GetLocalized(), 48, "#00F562"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_SPRING_GREEN".GetLocalized(), 43, "#00DB58"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_SPRING_GREEN".GetLocalized(), 32, "#00A341"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_SPRING_GREEN".GetLocalized(), 18, "#005C25"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_SPRING_GREEN".GetLocalized(), 10, "#003314")
		};
	}

	private IEnumerable<ColorInfo> CreateCyanColors()
	{
		return new List<ColorInfo>
		{
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_CYAN".GetLocalized(), 83, "#A8FFFF"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_CYAN".GetLocalized(), 70, "#66FFFF"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_CYAN".GetLocalized(), 62, "#3DFFFF"),
			new ColorInfo("DREAM_IDLE_TBOPT_CYAN".GetLocalized(), 52, "#0AFFFF"),
			new ColorInfo("DREAM_IDLE_TBOPT_CYAN".GetLocalized(), 0, "#00FFFF"),
			new ColorInfo("DREAM_IDLE_TBOPT_CYAN".GetLocalized(), 48, "#00F5F5"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_CYAN".GetLocalized(), 43, "#00DBDB"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_CYAN".GetLocalized(), 32, "#00A3A3"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_CYAN".GetLocalized(), 18, "#005C5C"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_CYAN".GetLocalized(), 10, "#003333")
		};
	}

	private IEnumerable<ColorInfo> CreateAzureColors()
	{
		return new List<ColorInfo>
		{
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_AZURE".GetLocalized(), 83, "#A8D4FF"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_AZURE".GetLocalized(), 71, "#6BB5FF"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_AZURE".GetLocalized(), 61, "#389CFF"),
			new ColorInfo("DREAM_IDLE_TBOPT_AZURE".GetLocalized(), 54, "#148AFF"),
			new ColorInfo("DREAM_IDLE_TBOPT_AZURE".GetLocalized(), 0, "#0080FF"),
			new ColorInfo("DREAM_IDLE_TBOPT_AZURE".GetLocalized(), 49, "#007DFA"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_AZURE".GetLocalized(), 43, "#006EDB"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_AZURE".GetLocalized(), 33, "#0054A8"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_AZURE".GetLocalized(), 19, "#003161"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_AZURE".GetLocalized(), 10, "#001A33")
		};
	}

	private IEnumerable<ColorInfo> CreateBlueColors()
	{
		return new List<ColorInfo>
		{
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_BLUE".GetLocalized(), 83, "#A8A8FF"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_BLUE".GetLocalized(), 71, "#6B6BFF"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_BLUE".GetLocalized(), 61, "#3838FF"),
			new ColorInfo("DREAM_IDLE_TBOPT_BLUE/Text".GetLocalized(), 52, "#0A0AFF"),
			new ColorInfo("DREAM_IDLE_TBOPT_BLUE/Text".GetLocalized(), 0, "#0000FF"),
			new ColorInfo("DREAM_IDLE_TBOPT_BLUE/Text".GetLocalized(), 49, "#0000FA"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_BLUE".GetLocalized(), 43, "#0000DB"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_BLUE".GetLocalized(), 33, "#0000A8"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_BLUE".GetLocalized(), 19, "#000061"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_BLUE".GetLocalized(), 10, "#000033")
		};
	}

	private IEnumerable<ColorInfo> CreateVioletColors()
	{
		return new List<ColorInfo>
		{
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_VIOLET".GetLocalized(), 83, "#CBA8FF"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_VIOLET".GetLocalized(), 71, "#A66BFF"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_VIOLET".GetLocalized(), 61, "#8838FF"),
			new ColorInfo("DREAM_IDLE_TBOPT_VIOLET".GetLocalized(), 53, "#6F0FFF"),
			new ColorInfo("DREAM_IDLE_TBOPT_VIOLET".GetLocalized(), 0, "#6600FF"),
			new ColorInfo("DREAM_IDLE_TBOPT_VIOLET".GetLocalized(), 49, "#6400FA"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_VIOLET".GetLocalized(), 43, "#5800DB"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_VIOLET".GetLocalized(), 33, "#4300A8"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_VIOLET".GetLocalized(), 18, "#25005C"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_VIOLET".GetLocalized(), 10, "#140033")
		};
	}

	private IEnumerable<ColorInfo> CreateMagentaColors()
	{
		return new List<ColorInfo>
		{
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_MAGENTA".GetLocalized(), 83, "#FFA8FF"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_MAGENTA".GetLocalized(), 70, "#FF66FF"),
			new ColorInfo("DREAM_IDLE_TBOPT_LIGHT_MAGENTA".GetLocalized(), 61, "#FF38FF"),
			new ColorInfo("DREAM_IDLE_TBOPT_MAGENTA".GetLocalized(), 53, "#FF0FFF"),
			new ColorInfo("DREAM_IDLE_TBOPT_MAGENTA".GetLocalized(), 0, "#FF00FF"),
			new ColorInfo("DREAM_IDLE_TBOPT_MAGENTA".GetLocalized(), 49, "#FA00FA"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_MAGENTA".GetLocalized(), 43, "#DB00DB"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_MAGENTA".GetLocalized(), 33, "#A800A8"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_MAGENTA".GetLocalized(), 19, "#610061"),
			new ColorInfo("DREAM_IDLE_TBOPT_DARK_MAGENTA".GetLocalized(), 10, "#330033")
		};
	}
}
