using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.WinUI;
using CommunityToolkit.WinUI.Media;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.AnimatedVisuals;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.XamlTypeInfo;
using Microsoft.Xaml.Interactions.Core;
using Microsoft.Xaml.Interactivity;
using Samsung.OneUI.WinUI.AttachedProperties;
using Samsung.OneUI.WinUI.AttachedProperties.Enums;
using Samsung.OneUI.WinUI.Common;
using Samsung.OneUI.WinUI.Controls;
using Samsung.OneUI.WinUI.Controls.Behaviors;
using Samsung.OneUI.WinUI.Controls.Brushes;
using Samsung.OneUI.WinUI.Controls.Converters;
using Samsung.OneUI.WinUI.Controls.DialogsAndFlyouts.SnackBar;
using Samsung.OneUI.WinUI.Controls.Inputs.CheckBox;
using Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker;
using Samsung.OneUI.WinUI.Controls.Inputs.Slider.LevelBar;
using Samsung.OneUI.WinUI.Controls.Inputs.ToggleSwitch;
using Samsung.OneUI.WinUI.Controls.Selectors;
using Samsung.OneUI.WinUI.Converters;
using Samsung.OneUI.WinUI.Tokens;
using Samsung.OneUI.WinUI.Utils.Enums;
using Samsung.OneUI.WinUI.Utils.Extensions;
using Windows.UI;
using Windows.UI.Text;

namespace Samsung.OneUI.WinUI.Samsung_OneUI_WinUI_XamlTypeInfo;

[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
[DebuggerNonUserCode]
internal class XamlTypeInfoProvider
{
	private Dictionary<string, IXamlType> _xamlTypeCacheByName = new Dictionary<string, IXamlType>();

	private Dictionary<Type, IXamlType> _xamlTypeCacheByType = new Dictionary<Type, IXamlType>();

	private Dictionary<string, IXamlMember> _xamlMembers = new Dictionary<string, IXamlMember>();

	private string[] _typeNameTable;

	private Type[] _typeTable;

	private List<IXamlMetadataProvider> _otherProviders;

	private List<IXamlMetadataProvider> OtherProviders
	{
		get
		{
			if (_otherProviders == null)
			{
				List<IXamlMetadataProvider> list = new List<IXamlMetadataProvider>();
				IXamlMetadataProvider item = new XamlControlsXamlMetaDataProvider();
				list.Add(item);
				_otherProviders = list;
			}
			return _otherProviders;
		}
	}

	public IXamlType GetXamlTypeByType(Type type)
	{
		IXamlType value;
		lock (_xamlTypeCacheByType)
		{
			if (_xamlTypeCacheByType.TryGetValue(type, out value))
			{
				return value;
			}
			int num = LookupTypeIndexByType(type);
			if (num != -1)
			{
				value = CreateXamlType(num);
			}
			XamlUserType xamlUserType = value as XamlUserType;
			if (value == null || (xamlUserType != null && xamlUserType.IsReturnTypeStub && !xamlUserType.IsLocalType))
			{
				IXamlType xamlType = CheckOtherMetadataProvidersForType(type);
				if (xamlType != null && (xamlType.IsConstructible || value == null))
				{
					value = xamlType;
				}
			}
			if (value != null)
			{
				_xamlTypeCacheByName.Add(value.FullName, value);
				_xamlTypeCacheByType.Add(value.UnderlyingType, value);
			}
		}
		return value;
	}

	public IXamlType GetXamlTypeByName(string typeName)
	{
		if (string.IsNullOrEmpty(typeName))
		{
			return null;
		}
		IXamlType value;
		lock (_xamlTypeCacheByType)
		{
			if (_xamlTypeCacheByName.TryGetValue(typeName, out value))
			{
				return value;
			}
			int num = LookupTypeIndexByName(typeName);
			if (num != -1)
			{
				value = CreateXamlType(num);
			}
			XamlUserType xamlUserType = value as XamlUserType;
			if (value == null || (xamlUserType != null && xamlUserType.IsReturnTypeStub && !xamlUserType.IsLocalType))
			{
				IXamlType xamlType = CheckOtherMetadataProvidersForName(typeName);
				if (xamlType != null && (xamlType.IsConstructible || value == null))
				{
					value = xamlType;
				}
			}
			if (value != null)
			{
				_xamlTypeCacheByName.Add(value.FullName, value);
				_xamlTypeCacheByType.Add(value.UnderlyingType, value);
			}
		}
		return value;
	}

	public IXamlMember GetMemberByLongName(string longMemberName)
	{
		if (string.IsNullOrEmpty(longMemberName))
		{
			return null;
		}
		IXamlMember value;
		lock (_xamlMembers)
		{
			if (_xamlMembers.TryGetValue(longMemberName, out value))
			{
				return value;
			}
			value = CreateXamlMember(longMemberName);
			if (value != null)
			{
				_xamlMembers.Add(longMemberName, value);
			}
		}
		return value;
	}

	private void InitTypeTables()
	{
		_typeNameTable = new string[391];
		_typeNameTable[0] = "Samsung.OneUI.WinUI.Controls.BGBlur";
		_typeNameTable[1] = "Microsoft.UI.Xaml.Controls.UserControl";
		_typeNameTable[2] = "Microsoft.UI.Xaml.UIElement";
		_typeNameTable[3] = "Samsung.OneUI.WinUI.Controls.VibrancyLevel";
		_typeNameTable[4] = "System.Enum";
		_typeNameTable[5] = "System.ValueType";
		_typeNameTable[6] = "Object";
		_typeNameTable[7] = "Microsoft.UI.Xaml.Media.Brush";
		_typeNameTable[8] = "Boolean";
		_typeNameTable[9] = "Microsoft.UI.Xaml.Controls.ListView";
		_typeNameTable[10] = "Samsung.OneUI.WinUI.Controls.CardType";
		_typeNameTable[11] = "Microsoft.UI.Xaml.Controls.Control";
		_typeNameTable[12] = "String";
		_typeNameTable[13] = "Microsoft.UI.Xaml.Media.ImageSource";
		_typeNameTable[14] = "Microsoft.UI.Xaml.Style";
		_typeNameTable[15] = "Samsung.OneUI.WinUI.Controls.CardTypeListView";
		_typeNameTable[16] = "System.Collections.Generic.List`1<Samsung.OneUI.WinUI.Controls.CardTypeItem>";
		_typeNameTable[17] = "Samsung.OneUI.WinUI.Controls.CardTypeItem";
		_typeNameTable[18] = "System.EventHandler";
		_typeNameTable[19] = "System.MulticastDelegate";
		_typeNameTable[20] = "System.Delegate";
		_typeNameTable[21] = "Samsung.OneUI.WinUI.Controls.WrapPanel";
		_typeNameTable[22] = "Microsoft.UI.Xaml.Controls.Panel";
		_typeNameTable[23] = "Double";
		_typeNameTable[24] = "Samsung.OneUI.WinUI.Controls.Chips";
		_typeNameTable[25] = "System.Collections.ObjectModel.ObservableCollection`1<Samsung.OneUI.WinUI.Controls.ChipsItem>";
		_typeNameTable[26] = "System.Collections.ObjectModel.Collection`1<Samsung.OneUI.WinUI.Controls.ChipsItem>";
		_typeNameTable[27] = "Samsung.OneUI.WinUI.Controls.ChipsItem";
		_typeNameTable[28] = "Microsoft.UI.Xaml.Controls.GridViewItem";
		_typeNameTable[29] = "Microsoft.UI.Xaml.Controls.ContentControl";
		_typeNameTable[30] = "Samsung.OneUI.WinUI.Controls.ChipsItemTemplate";
		_typeNameTable[31] = "Samsung.OneUI.WinUI.Controls.ChipsItemType";
		_typeNameTable[32] = "Microsoft.UI.Xaml.Controls.ListViewSelectionMode";
		_typeNameTable[33] = "Samsung.OneUI.WinUI.Controls.ChipsItemGroupTemplate";
		_typeNameTable[34] = "Samsung.OneUI.WinUI.Controls.Toast";
		_typeNameTable[35] = "Samsung.OneUI.WinUI.Controls.ToastDuration";
		_typeNameTable[36] = "Microsoft.UI.Xaml.FrameworkElement";
		_typeNameTable[37] = "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl";
		_typeNameTable[38] = "System.Nullable`1<Double>";
		_typeNameTable[39] = "Microsoft.UI.Xaml.Media.SolidColorBrush";
		_typeNameTable[40] = "Microsoft.UI.Xaml.Visibility";
		_typeNameTable[41] = "Microsoft.UI.Xaml.ElementTheme";
		_typeNameTable[42] = "System.Collections.Generic.List`1<Samsung.OneUI.WinUI.Controls.ColorInfo>";
		_typeNameTable[43] = "Samsung.OneUI.WinUI.Controls.ColorInfo";
		_typeNameTable[44] = "Samsung.OneUI.WinUI.Controls.FlatButton";
		_typeNameTable[45] = "Samsung.OneUI.WinUI.Controls.FlatButtonBase";
		_typeNameTable[46] = "Microsoft.UI.Xaml.Controls.Button";
		_typeNameTable[47] = "Samsung.OneUI.WinUI.Controls.FlatButtonSize";
		_typeNameTable[48] = "Samsung.OneUI.WinUI.Controls.FlatButtonType";
		_typeNameTable[49] = "Microsoft.UI.Xaml.TextTrimming";
		_typeNameTable[50] = "Int32";
		_typeNameTable[51] = "Samsung.OneUI.WinUI.Controls.ColorPickerDialog";
		_typeNameTable[52] = "System.Collections.Generic.List`1<String>";
		_typeNameTable[53] = "Samsung.OneUI.WinUI.Controls.DatePicker";
		_typeNameTable[54] = "Microsoft.UI.Xaml.Controls.CalendarView";
		_typeNameTable[55] = "System.DateTime";
		_typeNameTable[56] = "Samsung.OneUI.WinUI.Controls.DatePickerDialogContent";
		_typeNameTable[57] = "Microsoft.UI.Xaml.Controls.Page";
		_typeNameTable[58] = "Samsung.OneUI.WinUI.Controls.DateTimePickerList";
		_typeNameTable[59] = "Samsung.OneUI.WinUI.Controls.TimePickerList";
		_typeNameTable[60] = "System.Nullable`1<System.DateTime>";
		_typeNameTable[61] = "Samsung.OneUI.WinUI.Controls.TimeType";
		_typeNameTable[62] = "Samsung.OneUI.WinUI.Controls.TimePeriod";
		_typeNameTable[63] = "TimeSpan";
		_typeNameTable[64] = "Samsung.OneUI.WinUI.Controls.DateTimePickerDialogContent";
		_typeNameTable[65] = "Samsung.OneUI.WinUI.Controls.OneUIContentDialogContent";
		_typeNameTable[66] = "Microsoft.UI.Xaml.Controls.ScrollViewer";
		_typeNameTable[67] = "Microsoft.UI.Xaml.Controls.ListViewItem";
		_typeNameTable[68] = "Samsung.OneUI.WinUI.Controls.ListViewCustom";
		_typeNameTable[69] = "Microsoft.UI.Xaml.Controls.ItemsControl";
		_typeNameTable[70] = "Samsung.OneUI.WinUI.AttachedProperties.Responsiveness";
		_typeNameTable[71] = "Samsung.OneUI.WinUI.AttachedProperties.Enums.FlexibleSpacingType";
		_typeNameTable[72] = "System.Nullable`1<Boolean>";
		_typeNameTable[73] = "Samsung.OneUI.WinUI.Controls.SingleChoiceDialogContent";
		_typeNameTable[74] = "Samsung.OneUI.WinUI.Converters.BoolToVisibilityConverter";
		_typeNameTable[75] = "Samsung.OneUI.WinUI.Controls.SnackBarButton";
		_typeNameTable[76] = "Samsung.OneUI.WinUI.Controls.SnackBarButtonType";
		_typeNameTable[77] = "Samsung.OneUI.WinUI.Controls.DialogsAndFlyouts.SnackBar.SnackBar";
		_typeNameTable[78] = "Samsung.OneUI.WinUI.Controls.DialogsAndFlyouts.SnackBar.SnackBarDuration";
		_typeNameTable[79] = "Samsung.OneUI.WinUI.Controls.TimePickerKeyboard";
		_typeNameTable[80] = "Samsung.OneUI.WinUI.Controls.TimePickerKeyboardDialogContent";
		_typeNameTable[81] = "Samsung.OneUI.WinUI.Controls.TimePickerListDialogContent";
		_typeNameTable[82] = "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerOption";
		_typeNameTable[83] = "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerDescriptor";
		_typeNameTable[84] = "Samsung.OneUI.WinUI.Controls.SubHeader";
		_typeNameTable[85] = "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerHistory";
		_typeNameTable[86] = "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSwatched";
		_typeNameTable[87] = "Microsoft.UI.Xaml.Controls.ColorPicker";
		_typeNameTable[88] = "Windows.UI.Color";
		_typeNameTable[89] = "Microsoft.UI.Xaml.Controls.ColorSpectrumComponents";
		_typeNameTable[90] = "Microsoft.UI.Xaml.Controls.ColorSpectrumShape";
		_typeNameTable[91] = "Microsoft.UI.Xaml.Controls.Orientation";
		_typeNameTable[92] = "System.Nullable`1<Windows.UI.Color>";
		_typeNameTable[93] = "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSpectrum";
		_typeNameTable[94] = "Microsoft.UI.Xaml.CornerRadius";
		_typeNameTable[95] = "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.SolidBrushColorToHexadecimalConverter";
		_typeNameTable[96] = "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerTextBox";
		_typeNameTable[97] = "Samsung.OneUI.WinUI.Controls.Brushes.CheckeredBrush";
		_typeNameTable[98] = "Microsoft.UI.Xaml.Media.XamlCompositionBrushBase";
		_typeNameTable[99] = "Microsoft.UI.Xaml.Controls.TextBox";
		_typeNameTable[100] = "Samsung.OneUI.WinUI.Controls.Selectors.ColorListItemSelector";
		_typeNameTable[101] = "Microsoft.UI.Xaml.Controls.StyleSelector";
		_typeNameTable[102] = "Samsung.OneUI.WinUI.Converters.StringToSolidColorBrushConverter";
		_typeNameTable[103] = "Samsung.OneUI.WinUI.AttachedProperties.CornerRadiusAutoHalfCorner";
		_typeNameTable[104] = "Microsoft.UI.Xaml.DependencyObject";
		_typeNameTable[105] = "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerHistoryGridViewCustom";
		_typeNameTable[106] = "Microsoft.UI.Xaml.Controls.GridView";
		_typeNameTable[107] = "Microsoft.UI.Xaml.Thickness";
		_typeNameTable[108] = "Samsung.OneUI.WinUI.Utils.Extensions.OverlayColorsToSolidColorBrushExtension";
		_typeNameTable[109] = "Microsoft.UI.Xaml.Markup.MarkupExtension";
		_typeNameTable[110] = "System.Collections.Generic.IList`1<Microsoft.UI.Xaml.Media.SolidColorBrush>";
		_typeNameTable[111] = "Windows.UI.Text.FontWeight";
		_typeNameTable[112] = "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerOptionCustomButton";
		_typeNameTable[113] = "Microsoft.UI.Xaml.Controls.Primitives.ToggleButton";
		_typeNameTable[114] = "Samsung.OneUI.WinUI.Converters.CornerRadiusToDoubleConverter";
		_typeNameTable[115] = "Samsung.OneUI.WinUI.Converters.ICornerRadiusRoundingStrategyConvertion";
		_typeNameTable[116] = "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.OneUIColorSpectrum";
		_typeNameTable[117] = "Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum";
		_typeNameTable[118] = "System.Numerics.Vector4";
		_typeNameTable[119] = "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSliderCustom";
		_typeNameTable[120] = "Microsoft.UI.Xaml.Controls.Primitives.ColorPickerSlider";
		_typeNameTable[121] = "Microsoft.UI.Xaml.Controls.Slider";
		_typeNameTable[122] = "Microsoft.UI.Xaml.Controls.ColorPickerHsvChannel";
		_typeNameTable[123] = "Microsoft.UI.Xaml.Controls.Primitives.Thumb";
		_typeNameTable[124] = "CommunityToolkit.WinUI.Effects";
		_typeNameTable[125] = "CommunityToolkit.WinUI.AttachedShadowBase";
		_typeNameTable[126] = "CommunityToolkit.WinUI.Media.AttachedCardShadow";
		_typeNameTable[127] = "CommunityToolkit.WinUI.Media.InnerContentClipMode";
		_typeNameTable[128] = "Samsung.OneUI.WinUI.Converters.CornerRadiusCornersConverter";
		_typeNameTable[129] = "Samsung.OneUI.WinUI.Controls.Selectors.ColorPickerGridViewItemRadiusSelector";
		_typeNameTable[130] = "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSwatchedGridViewCustom";
		_typeNameTable[131] = "Samsung.OneUI.WinUI.OneUIResources";
		_typeNameTable[132] = "Microsoft.UI.Xaml.ResourceDictionary";
		_typeNameTable[133] = "Samsung.OneUI.WinUI.Controls.NumberBadge";
		_typeNameTable[134] = "Samsung.OneUI.WinUI.Controls.BadgeBase";
		_typeNameTable[135] = "Samsung.OneUI.WinUI.Controls.TextBadge";
		_typeNameTable[136] = "Samsung.OneUI.WinUI.Controls.AlertBadge";
		_typeNameTable[137] = "Samsung.OneUI.WinUI.Controls.DotBadge";
		_typeNameTable[138] = "Samsung.OneUI.WinUI.Controls.AddButton";
		_typeNameTable[139] = "Samsung.OneUI.WinUI.Controls.DeleteButton";
		_typeNameTable[140] = "Samsung.OneUI.WinUI.Controls.ContainedButtonBodyColored";
		_typeNameTable[141] = "Samsung.OneUI.WinUI.Controls.ContainedButtonBase";
		_typeNameTable[142] = "Microsoft.Xaml.Interactivity.Interaction";
		_typeNameTable[143] = "Microsoft.Xaml.Interactivity.BehaviorCollection";
		_typeNameTable[144] = "Microsoft.UI.Xaml.DependencyObjectCollection";
		_typeNameTable[145] = "Samsung.OneUI.WinUI.Controls.ProgressCircleIndeterminate";
		_typeNameTable[146] = "Samsung.OneUI.WinUI.Controls.ProgressCircle";
		_typeNameTable[147] = "Samsung.OneUI.WinUI.Controls.ProgressCircleSize";
		_typeNameTable[148] = "Samsung.OneUI.WinUI.Controls.Behaviors.TooltipForTrimmedButtonBehavior";
		_typeNameTable[149] = "Microsoft.Xaml.Interactivity.Behavior`1<Microsoft.UI.Xaml.FrameworkElement>";
		_typeNameTable[150] = "Microsoft.Xaml.Interactivity.Behavior";
		_typeNameTable[151] = "Samsung.OneUI.WinUI.Controls.ContainedButtonBody";
		_typeNameTable[152] = "Samsung.OneUI.WinUI.Controls.ContainedButtonColored";
		_typeNameTable[153] = "Samsung.OneUI.WinUI.Controls.ContainedButton";
		_typeNameTable[154] = "Samsung.OneUI.WinUI.Controls.ContainedButtonType";
		_typeNameTable[155] = "Samsung.OneUI.WinUI.Controls.ContainedButtonSize";
		_typeNameTable[156] = "Samsung.OneUI.WinUI.Controls.ContentButton";
		_typeNameTable[157] = "Samsung.OneUI.WinUI.Controls.ButtonShapeEnum";
		_typeNameTable[158] = "Samsung.OneUI.WinUI.Controls.ContentToggleButton";
		_typeNameTable[159] = "Samsung.OneUI.WinUI.Controls.EditButton";
		_typeNameTable[160] = "Samsung.OneUI.WinUI.Controls.EditButtonType";
		_typeNameTable[161] = "Samsung.OneUI.WinUI.Controls.FlatUnderlineButton";
		_typeNameTable[162] = "Samsung.OneUI.WinUI.Controls.FloatingActionButton";
		_typeNameTable[163] = "Samsung.OneUI.WinUI.AttachedProperties.ElevationCorner";
		_typeNameTable[164] = "Samsung.OneUI.WinUI.Tokens.BlurLayer";
		_typeNameTable[165] = "Samsung.OneUI.WinUI.Tokens.BlurLevel";
		_typeNameTable[166] = "Samsung.OneUI.WinUI.Tokens.VibrancyLevel";
		_typeNameTable[167] = "Samsung.OneUI.WinUI.Controls.GoToTopButton";
		_typeNameTable[168] = "Samsung.OneUI.WinUI.Controls.HyperlinkButton";
		_typeNameTable[169] = "Microsoft.UI.Xaml.Controls.HyperlinkButton";
		_typeNameTable[170] = "Samsung.OneUI.WinUI.Controls.ProgressButton";
		_typeNameTable[171] = "Samsung.OneUI.WinUI.Controls.ProgressButtonType";
		_typeNameTable[172] = "Samsung.OneUI.WinUI.Converters.StringToVisibilityConverter";
		_typeNameTable[173] = "Samsung.OneUI.WinUI.Converters.ImageToVisibilityConverter";
		_typeNameTable[174] = "Samsung.OneUI.WinUI.Controls.ToolTip";
		_typeNameTable[175] = "Microsoft.UI.Xaml.Controls.ToolTip";
		_typeNameTable[176] = "Samsung.OneUI.WinUI.Controls.Behaviors.TooltipForTrimmedTextBlockBehavior";
		_typeNameTable[177] = "Microsoft.UI.Xaml.Media.Animation.KeyTime";
		_typeNameTable[178] = "Samsung.OneUI.WinUI.Converters.NullToVisibilityConverter";
		_typeNameTable[179] = "Samsung.OneUI.WinUI.Controls.CheckBox";
		_typeNameTable[180] = "Microsoft.UI.Xaml.Controls.CheckBox";
		_typeNameTable[181] = "Microsoft.UI.Xaml.Controls.IconElement";
		_typeNameTable[182] = "Samsung.OneUI.WinUI.Controls.Inputs.CheckBox.CheckBoxType";
		_typeNameTable[183] = "Samsung.OneUI.WinUI.Converters.ChipsItemIconVisibilityConverter";
		_typeNameTable[184] = "Samsung.OneUI.WinUI.Converters.ChipsItemImageIconVisibilityConverter";
		_typeNameTable[185] = "Samsung.OneUI.WinUI.Converters.ChipsItemIconStyleConverter";
		_typeNameTable[186] = "Samsung.OneUI.WinUI.Controls.Selectors.ChipsItemStyleSelector";
		_typeNameTable[187] = "Samsung.OneUI.WinUI.Controls.Behaviors.CornerRadiusBorderCompensationBehavior";
		_typeNameTable[188] = "Microsoft.UI.Xaml.Controls.ImageIcon";
		_typeNameTable[189] = "Byte";
		_typeNameTable[190] = "Samsung.OneUI.WinUI.Controls.CommandBarButton";
		_typeNameTable[191] = "Microsoft.UI.Xaml.Controls.AppBarButton";
		_typeNameTable[192] = "Samsung.OneUI.WinUI.Converters.IntToEnumConverter";
		_typeNameTable[193] = "Samsung.OneUI.WinUI.Controls.CommandBar";
		_typeNameTable[194] = "Microsoft.UI.Xaml.Controls.CommandBar";
		_typeNameTable[195] = "System.Collections.ObjectModel.ObservableCollection`1<Microsoft.UI.Xaml.Controls.MenuFlyoutItemBase>";
		_typeNameTable[196] = "System.Collections.ObjectModel.Collection`1<Microsoft.UI.Xaml.Controls.MenuFlyoutItemBase>";
		_typeNameTable[197] = "Microsoft.UI.Xaml.Controls.MenuFlyoutItemBase";
		_typeNameTable[198] = "System.Windows.Input.ICommand";
		_typeNameTable[199] = "Samsung.OneUI.WinUI.Controls.CommandBarBackButtonType";
		_typeNameTable[200] = "System.Nullable`1<Microsoft.UI.Xaml.Controls.Primitives.FlyoutPlacementMode>";
		_typeNameTable[201] = "Microsoft.UI.Xaml.Controls.Primitives.FlyoutPlacementMode";
		_typeNameTable[202] = "Samsung.OneUI.WinUI.Controls.Behaviors.FlexibleSpacingBehavior";
		_typeNameTable[203] = "Samsung.OneUI.WinUI.Controls.IconButton";
		_typeNameTable[204] = "Samsung.OneUI.WinUI.Controls.ListFlyout";
		_typeNameTable[205] = "Microsoft.UI.Xaml.Controls.MenuFlyout";
		_typeNameTable[206] = "Samsung.OneUI.WinUI.AttachedProperties.Tooltip";
		_typeNameTable[207] = "Samsung.OneUI.WinUI.Controls.CommandBarToggleButton";
		_typeNameTable[208] = "Microsoft.UI.Xaml.Controls.AppBarToggleButton";
		_typeNameTable[209] = "Samsung.OneUI.WinUI.Converters.MaxNumberCornerRadiusRoundingStrategy";
		_typeNameTable[210] = "Samsung.OneUI.WinUI.Controls.ContextMenuItem";
		_typeNameTable[211] = "Samsung.OneUI.WinUI.Controls.ListFlyoutItem";
		_typeNameTable[212] = "Microsoft.UI.Xaml.Controls.MenuFlyoutItem";
		_typeNameTable[213] = "Samsung.OneUI.WinUI.Controls.ICommandBarItemOverflowable";
		_typeNameTable[214] = "Samsung.OneUI.WinUI.Controls.ContextMenuToggle";
		_typeNameTable[215] = "Samsung.OneUI.WinUI.Controls.ListFlyoutToggle";
		_typeNameTable[216] = "Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem";
		_typeNameTable[217] = "Samsung.OneUI.WinUI.Controls.ContextMenuSeparator";
		_typeNameTable[218] = "Samsung.OneUI.WinUI.Controls.ListFlyoutSeparator";
		_typeNameTable[219] = "Microsoft.UI.Xaml.Controls.MenuFlyoutSeparator";
		_typeNameTable[220] = "Samsung.OneUI.WinUI.Converters.ColorWithTransparencyConverter";
		_typeNameTable[221] = "Samsung.OneUI.WinUI.Controls.DatePickerSpinnerList";
		_typeNameTable[222] = "Samsung.OneUI.WinUI.Controls.DatePickerSpinnerListItem";
		_typeNameTable[223] = "Samsung.OneUI.WinUI.Controls.ScrollList";
		_typeNameTable[224] = "System.Collections.ObjectModel.ObservableCollection`1<Object>";
		_typeNameTable[225] = "System.Collections.ObjectModel.Collection`1<Object>";
		_typeNameTable[226] = "Samsung.OneUI.WinUI.Utils.Enums.TypeDate";
		_typeNameTable[227] = "Samsung.OneUI.WinUI.Converters.StringToUpperConverter";
		_typeNameTable[228] = "Samsung.OneUI.WinUI.Converters.DatePickerTextScaleSizeConverter";
		_typeNameTable[229] = "Microsoft.UI.Xaml.Controls.CalendarViewDayItem";
		_typeNameTable[230] = "Samsung.OneUI.WinUI.Controls.Selectors.PeriodStyleSelector";
		_typeNameTable[231] = "Samsung.OneUI.WinUI.Controls.PeriodScrollList";
		_typeNameTable[232] = "Samsung.OneUI.WinUI.Common.DpiChangedTo175StateTrigger";
		_typeNameTable[233] = "Samsung.OneUI.WinUI.Common.DpiChangedStateTriggerBase";
		_typeNameTable[234] = "Microsoft.UI.Xaml.StateTriggerBase";
		_typeNameTable[235] = "Samsung.OneUI.WinUI.Common.OSVersionType";
		_typeNameTable[236] = "Samsung.OneUI.WinUI.Common.DpiChangedTo125StateTrigger";
		_typeNameTable[237] = "Samsung.OneUI.WinUI.Controls.ContentDialog";
		_typeNameTable[238] = "Microsoft.UI.Xaml.Controls.ContentDialog";
		_typeNameTable[239] = "Microsoft.UI.Xaml.HorizontalAlignment";
		_typeNameTable[240] = "Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer";
		_typeNameTable[241] = "Microsoft.UI.Xaml.GridLength";
		_typeNameTable[242] = "Samsung.OneUI.WinUI.Controls.ShowVerticalScrollableIndicatorBehavior";
		_typeNameTable[243] = "Microsoft.Xaml.Interactivity.Behavior`1<Microsoft.UI.Xaml.DependencyObject>";
		_typeNameTable[244] = "Samsung.OneUI.WinUI.Controls.Divider";
		_typeNameTable[245] = "Samsung.OneUI.WinUI.Controls.DividerType";
		_typeNameTable[246] = "Samsung.OneUI.WinUI.Controls.DropdownList";
		_typeNameTable[247] = "System.Collections.IList";
		_typeNameTable[248] = "Samsung.OneUI.WinUI.DropdownListType";
		_typeNameTable[249] = "Samsung.OneUI.WinUI.Controls.DropdownListViewCustom";
		_typeNameTable[250] = "Samsung.OneUI.WinUI.Controls.DropdownCustomControl";
		_typeNameTable[251] = "Samsung.OneUI.WinUI.Controls.Converters.OpacityToVisibilityConverter";
		_typeNameTable[252] = "Samsung.OneUI.WinUI.Controls.ExpandableList";
		_typeNameTable[253] = "Microsoft.UI.Xaml.Controls.TreeView";
		_typeNameTable[254] = "Microsoft.UI.Xaml.Controls.TreeViewSelectionMode";
		_typeNameTable[255] = "Microsoft.UI.Xaml.Media.Animation.TransitionCollection";
		_typeNameTable[256] = "Microsoft.UI.Xaml.DataTemplate";
		_typeNameTable[257] = "Microsoft.UI.Xaml.Controls.DataTemplateSelector";
		_typeNameTable[258] = "System.Collections.Generic.IList`1<Microsoft.UI.Xaml.Controls.TreeViewNode>";
		_typeNameTable[259] = "Microsoft.UI.Xaml.Controls.TreeViewNode";
		_typeNameTable[260] = "System.Collections.Generic.IList`1<Object>";
		_typeNameTable[261] = "Samsung.OneUI.WinUI.Controls.ExpandableListItemHeader";
		_typeNameTable[262] = "Microsoft.UI.Xaml.Controls.TreeViewItem";
		_typeNameTable[263] = "Microsoft.UI.Xaml.Controls.TreeViewItemTemplateSettings";
		_typeNameTable[264] = "Samsung.OneUI.WinUI.Controls.ExpandButton";
		_typeNameTable[265] = "Microsoft.UI.Xaml.Controls.TreeViewList";
		_typeNameTable[266] = "Samsung.OneUI.WinUI.Controls.FlipViewButton";
		_typeNameTable[267] = "Samsung.OneUI.WinUI.Controls.FlipView";
		_typeNameTable[268] = "Microsoft.UI.Xaml.Controls.FlipView";
		_typeNameTable[269] = "Samsung.OneUI.WinUI.Controls.FlipViewItem";
		_typeNameTable[270] = "Microsoft.UI.Xaml.Controls.FlipViewItem";
		_typeNameTable[271] = "Samsung.OneUI.WinUI.Controls.IconToggleButton";
		_typeNameTable[272] = "Samsung.OneUI.WinUI.Controls.Inputs.Slider.LevelBar.LevelSlider";
		_typeNameTable[273] = "Samsung.OneUI.WinUI.Controls.Inputs.Slider.LevelBar.SliderMarkerControl";
		_typeNameTable[274] = "Samsung.OneUI.WinUI.Controls.LevelBar";
		_typeNameTable[275] = "Microsoft.UI.Xaml.Data.IValueConverter";
		_typeNameTable[276] = "Microsoft.UI.Xaml.Controls.ItemsRepeater";
		_typeNameTable[277] = "Microsoft.UI.Xaml.Controls.Layout";
		_typeNameTable[278] = "Microsoft.UI.Xaml.Controls.ItemCollectionTransitionProvider";
		_typeNameTable[279] = "Microsoft.UI.Xaml.Controls.ItemsSourceView";
		_typeNameTable[280] = "Microsoft.UI.Xaml.Controls.StackLayout";
		_typeNameTable[281] = "Microsoft.UI.Xaml.Controls.VirtualizingLayout";
		_typeNameTable[282] = "Microsoft.UI.Xaml.Controls.IndexBasedLayoutOrientation";
		_typeNameTable[283] = "Microsoft.UI.Xaml.Controls.UniformGridLayout";
		_typeNameTable[284] = "Microsoft.UI.Xaml.Controls.UniformGridLayoutItemsJustification";
		_typeNameTable[285] = "Microsoft.UI.Xaml.Controls.UniformGridLayoutItemsStretch";
		_typeNameTable[286] = "Microsoft.UI.Xaml.Controls.MenuFlyoutSubItem";
		_typeNameTable[287] = "Microsoft.UI.Xaml.Controls.MenuFlyoutPresenter";
		_typeNameTable[288] = "Samsung.OneUI.WinUI.Controls.MultiPane";
		_typeNameTable[289] = "Microsoft.UI.Xaml.Controls.SplitView";
		_typeNameTable[290] = "Samsung.OneUI.WinUI.Controls.SplitBar";
		_typeNameTable[291] = "Samsung.OneUI.WinUI.Controls.SplitBar.GridResizeDirection";
		_typeNameTable[292] = "Samsung.OneUI.WinUI.Controls.SplitBar.GridResizeBehavior";
		_typeNameTable[293] = "Samsung.OneUI.WinUI.Controls.SplitBar.GripperCursorType";
		_typeNameTable[294] = "Samsung.OneUI.WinUI.Controls.SplitBar.SplitterCursorBehavior";
		_typeNameTable[295] = "Samsung.OneUI.WinUI.Controls.NavigationRail";
		_typeNameTable[296] = "Microsoft.UI.Xaml.Controls.NavigationView";
		_typeNameTable[297] = "Microsoft.UI.Xaml.Controls.AutoSuggestBox";
		_typeNameTable[298] = "Microsoft.UI.Xaml.Controls.NavigationViewDisplayMode";
		_typeNameTable[299] = "Microsoft.UI.Xaml.Controls.NavigationViewBackButtonVisible";
		_typeNameTable[300] = "Microsoft.UI.Xaml.Controls.NavigationViewOverflowLabelMode";
		_typeNameTable[301] = "Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode";
		_typeNameTable[302] = "Microsoft.UI.Xaml.Controls.NavigationViewSelectionFollowsFocus";
		_typeNameTable[303] = "Microsoft.UI.Xaml.Controls.NavigationViewShoulderNavigationEnabled";
		_typeNameTable[304] = "Microsoft.UI.Xaml.Controls.NavigationViewTemplateSettings";
		_typeNameTable[305] = "Samsung.OneUI.WinUI.Controls.NavigationRailItem";
		_typeNameTable[306] = "Microsoft.UI.Xaml.Controls.NavigationViewItem";
		_typeNameTable[307] = "Microsoft.UI.Xaml.Controls.NavigationViewItemBase";
		_typeNameTable[308] = "Microsoft.UI.Xaml.Controls.InfoBadge";
		_typeNameTable[309] = "Samsung.OneUI.WinUI.Controls.NavigationRailItemHeader";
		_typeNameTable[310] = "Microsoft.UI.Xaml.Controls.NavigationViewItemHeader";
		_typeNameTable[311] = "Samsung.OneUI.WinUI.Controls.NavigationRailItemSeparator";
		_typeNameTable[312] = "Microsoft.UI.Xaml.Controls.NavigationViewItemSeparator";
		_typeNameTable[313] = "Samsung.OneUI.WinUI.Controls.NavigationRailItemPresenter";
		_typeNameTable[314] = "Microsoft.UI.Xaml.Controls.Primitives.NavigationViewItemPresenter";
		_typeNameTable[315] = "Microsoft.UI.Xaml.Controls.Primitives.NavigationViewItemPresenterTemplateSettings";
		_typeNameTable[316] = "Microsoft.UI.Xaml.Controls.AnimatedIcon";
		_typeNameTable[317] = "Microsoft.UI.Xaml.Controls.IAnimatedVisualSource2";
		_typeNameTable[318] = "Microsoft.UI.Xaml.Controls.IconSource";
		_typeNameTable[319] = "Microsoft.UI.Xaml.Controls.AnimatedVisuals.AnimatedChevronUpDownSmallVisualSource";
		_typeNameTable[320] = "System.Collections.Generic.IReadOnlyDictionary`2<String, Double>";
		_typeNameTable[321] = "Microsoft.UI.Xaml.Controls.FlyoutPresenter";
		_typeNameTable[322] = "Microsoft.UI.Xaml.Controls.ItemsRepeaterScrollHost";
		_typeNameTable[323] = "Samsung.OneUI.WinUI.Controls.NavigationView";
		_typeNameTable[324] = "Samsung.OneUI.WinUI.Controls.NavigationViewItem";
		_typeNameTable[325] = "Samsung.OneUI.WinUI.Controls.NavigationViewItemHeader";
		_typeNameTable[326] = "Samsung.OneUI.WinUI.Controls.NavigationViewItemSeparator";
		_typeNameTable[327] = "Samsung.OneUI.WinUI.Controls.NavigationViewItemPresenter";
		_typeNameTable[328] = "Samsung.OneUI.WinUI.Controls.PageIndicator";
		_typeNameTable[329] = "Samsung.OneUI.WinUI.Controls.PopOverPresenter";
		_typeNameTable[330] = "Samsung.OneUI.WinUI.Controls.ProgressBar";
		_typeNameTable[331] = "Microsoft.UI.Xaml.Controls.ProgressBar";
		_typeNameTable[332] = "Microsoft.UI.Xaml.Controls.Primitives.RangeBase";
		_typeNameTable[333] = "Microsoft.UI.Xaml.Controls.ProgressBarTemplateSettings";
		_typeNameTable[334] = "Samsung.OneUI.WinUI.Controls.ProgressCircleDeterminate";
		_typeNameTable[335] = "Samsung.OneUI.WinUI.Controls.ProgressCircleDeterminateType";
		_typeNameTable[336] = "Samsung.OneUI.WinUI.Controls.RadioButtons";
		_typeNameTable[337] = "Microsoft.UI.Xaml.Controls.RadioButtons";
		_typeNameTable[338] = "Samsung.OneUI.WinUI.Controls.RadioButton";
		_typeNameTable[339] = "Microsoft.UI.Xaml.Controls.RadioButton";
		_typeNameTable[340] = "Microsoft.UI.Xaml.GridUnitType";
		_typeNameTable[341] = "Samsung.OneUI.WinUI.Converters.ScrollModeToBoolConverter";
		_typeNameTable[342] = "Samsung.OneUI.WinUI.Converters.DoubleToThicknessTopAndBottomConverter";
		_typeNameTable[343] = "Microsoft.UI.Xaml.Controls.Primitives.ScrollBar";
		_typeNameTable[344] = "Microsoft.UI.Xaml.Controls.Primitives.RepeatButton";
		_typeNameTable[345] = "Samsung.OneUI.WinUI.Controls.Behaviors.ThumbDisabledScrollBarDimensionsBehavior";
		_typeNameTable[346] = "Microsoft.Xaml.Interactivity.Behavior`1<Microsoft.UI.Xaml.Controls.Primitives.Thumb>";
		_typeNameTable[347] = "Microsoft.Xaml.Interactions.Core.DataTriggerBehavior";
		_typeNameTable[348] = "Microsoft.Xaml.Interactivity.Trigger";
		_typeNameTable[349] = "Microsoft.Xaml.Interactivity.ActionCollection";
		_typeNameTable[350] = "Microsoft.Xaml.Interactions.Core.ComparisonConditionType";
		_typeNameTable[351] = "Microsoft.Xaml.Interactions.Core.GoToStateAction";
		_typeNameTable[352] = "Samsung.OneUI.WinUI.Common.ThumbCompositeTransformScaleStateTrigger";
		_typeNameTable[353] = "Samsung.OneUI.WinUI.Controls.SearchPopupListFooterButton";
		_typeNameTable[354] = "Samsung.OneUI.WinUI.Controls.SearchPopupList";
		_typeNameTable[355] = "System.Collections.ObjectModel.ObservableCollection`1<Samsung.OneUI.WinUI.Controls.SearchPopupListItem>";
		_typeNameTable[356] = "System.Collections.ObjectModel.Collection`1<Samsung.OneUI.WinUI.Controls.SearchPopupListItem>";
		_typeNameTable[357] = "Samsung.OneUI.WinUI.Controls.SearchPopupListItem";
		_typeNameTable[358] = "Samsung.OneUI.WinUI.Controls.FilterTextBlock";
		_typeNameTable[359] = "Samsung.OneUI.WinUI.Controls.SearchPopup";
		_typeNameTable[360] = "Samsung.OneUI.WinUI.Controls.SearchPopupRemoveButton";
		_typeNameTable[361] = "Samsung.OneUI.WinUI.Controls.SearchPopupTextBox";
		_typeNameTable[362] = "Samsung.OneUI.WinUI.AttachedProperties.BackdropBlurExtension";
		_typeNameTable[363] = "Samsung.OneUI.WinUI.Controls.Slider";
		_typeNameTable[364] = "Samsung.OneUI.WinUI.Controls.SliderBase";
		_typeNameTable[365] = "Samsung.OneUI.WinUI.Controls.ShockValueType";
		_typeNameTable[366] = "Samsung.OneUI.WinUI.Controls.SliderType";
		_typeNameTable[367] = "Samsung.OneUI.WinUI.Controls.BufferSlider";
		_typeNameTable[368] = "Samsung.OneUI.WinUI.Controls.CenterSlider";
		_typeNameTable[369] = "Samsung.OneUI.WinUI.Controls.SubAppBar";
		_typeNameTable[370] = "Samsung.OneUI.WinUI.Controls.TabView";
		_typeNameTable[371] = "Microsoft.UI.Xaml.Controls.Pivot";
		_typeNameTable[372] = "Samsung.OneUI.WinUI.Controls.TabViewType";
		_typeNameTable[373] = "Samsung.OneUI.WinUI.Controls.TabItem";
		_typeNameTable[374] = "Microsoft.UI.Xaml.Controls.PivotItem";
		_typeNameTable[375] = "Microsoft.UI.Xaml.Controls.Primitives.PivotHeaderItem";
		_typeNameTable[376] = "Samsung.OneUI.WinUI.Converters.ThicknessSideConverter";
		_typeNameTable[377] = "Samsung.OneUI.WinUI.Controls.TextField";
		_typeNameTable[378] = "Samsung.OneUI.WinUI.Controls.TextFieldType";
		_typeNameTable[379] = "Samsung.OneUI.WinUI.Controls.ThumbnailRadious";
		_typeNameTable[380] = "Samsung.OneUI.WinUI.Controls.ThumbnailRadiousVisualizationMode";
		_typeNameTable[381] = "Samsung.OneUI.WinUI.Controls.ThumbnailRadiousGridView";
		_typeNameTable[382] = "Samsung.OneUI.WinUI.Controls.Titlebar";
		_typeNameTable[383] = "Samsung.OneUI.WinUI.Controls.ToggleSwitch";
		_typeNameTable[384] = "Samsung.OneUI.WinUI.Controls.Inputs.ToggleSwitch.ToggleSwitchType";
		_typeNameTable[385] = "Samsung.OneUI.WinUI.Controls.ToggleSwitchGroup";
		_typeNameTable[386] = "Microsoft.UI.Xaml.Controls.ToggleSwitch";
		_typeNameTable[387] = "Samsung.OneUI.WinUI.Common.DpiChangedTo150StateTrigger";
		_typeNameTable[388] = "Samsung.OneUI.WinUI.Common.DpiChangedTo100StateTrigger";
		_typeNameTable[389] = "Samsung.OneUI.WinUI.Controls.ToggleSwitchLoadedBehavior";
		_typeNameTable[390] = "Microsoft.Xaml.Interactivity.Behavior`1<Microsoft.UI.Xaml.Controls.ToggleSwitch>";
		_typeTable = new Type[391];
		_typeTable[0] = typeof(BGBlur);
		_typeTable[1] = typeof(UserControl);
		_typeTable[2] = typeof(UIElement);
		_typeTable[3] = typeof(Samsung.OneUI.WinUI.Controls.VibrancyLevel);
		_typeTable[4] = typeof(Enum);
		_typeTable[5] = typeof(ValueType);
		_typeTable[6] = typeof(object);
		_typeTable[7] = typeof(Brush);
		_typeTable[8] = typeof(bool);
		_typeTable[9] = typeof(ListView);
		_typeTable[10] = typeof(CardType);
		_typeTable[11] = typeof(Control);
		_typeTable[12] = typeof(string);
		_typeTable[13] = typeof(ImageSource);
		_typeTable[14] = typeof(Style);
		_typeTable[15] = typeof(CardTypeListView);
		_typeTable[16] = typeof(List<CardTypeItem>);
		_typeTable[17] = typeof(CardTypeItem);
		_typeTable[18] = typeof(EventHandler);
		_typeTable[19] = typeof(MulticastDelegate);
		_typeTable[20] = typeof(Delegate);
		_typeTable[21] = typeof(WrapPanel);
		_typeTable[22] = typeof(Panel);
		_typeTable[23] = typeof(double);
		_typeTable[24] = typeof(Chips);
		_typeTable[25] = typeof(ObservableCollection<ChipsItem>);
		_typeTable[26] = typeof(Collection<ChipsItem>);
		_typeTable[27] = typeof(ChipsItem);
		_typeTable[28] = typeof(GridViewItem);
		_typeTable[29] = typeof(ContentControl);
		_typeTable[30] = typeof(ChipsItemTemplate);
		_typeTable[31] = typeof(ChipsItemType);
		_typeTable[32] = typeof(ListViewSelectionMode);
		_typeTable[33] = typeof(ChipsItemGroupTemplate);
		_typeTable[34] = typeof(Toast);
		_typeTable[35] = typeof(ToastDuration);
		_typeTable[36] = typeof(FrameworkElement);
		_typeTable[37] = typeof(ColorPickerControl);
		_typeTable[38] = typeof(double?);
		_typeTable[39] = typeof(SolidColorBrush);
		_typeTable[40] = typeof(Visibility);
		_typeTable[41] = typeof(ElementTheme);
		_typeTable[42] = typeof(List<ColorInfo>);
		_typeTable[43] = typeof(ColorInfo);
		_typeTable[44] = typeof(FlatButton);
		_typeTable[45] = typeof(FlatButtonBase);
		_typeTable[46] = typeof(Button);
		_typeTable[47] = typeof(FlatButtonSize);
		_typeTable[48] = typeof(FlatButtonType);
		_typeTable[49] = typeof(TextTrimming);
		_typeTable[50] = typeof(int);
		_typeTable[51] = typeof(ColorPickerDialog);
		_typeTable[52] = typeof(List<string>);
		_typeTable[53] = typeof(Samsung.OneUI.WinUI.Controls.DatePicker);
		_typeTable[54] = typeof(CalendarView);
		_typeTable[55] = typeof(DateTime);
		_typeTable[56] = typeof(DatePickerDialogContent);
		_typeTable[57] = typeof(Page);
		_typeTable[58] = typeof(DateTimePickerList);
		_typeTable[59] = typeof(TimePickerList);
		_typeTable[60] = typeof(DateTime?);
		_typeTable[61] = typeof(TimeType);
		_typeTable[62] = typeof(TimePeriod);
		_typeTable[63] = typeof(TimeSpan);
		_typeTable[64] = typeof(DateTimePickerDialogContent);
		_typeTable[65] = typeof(OneUIContentDialogContent);
		_typeTable[66] = typeof(Microsoft.UI.Xaml.Controls.ScrollViewer);
		_typeTable[67] = typeof(ListViewItem);
		_typeTable[68] = typeof(ListViewCustom);
		_typeTable[69] = typeof(ItemsControl);
		_typeTable[70] = typeof(Responsiveness);
		_typeTable[71] = typeof(FlexibleSpacingType);
		_typeTable[72] = typeof(bool?);
		_typeTable[73] = typeof(SingleChoiceDialogContent);
		_typeTable[74] = typeof(BoolToVisibilityConverter);
		_typeTable[75] = typeof(SnackBarButton);
		_typeTable[76] = typeof(SnackBarButtonType);
		_typeTable[77] = typeof(SnackBar);
		_typeTable[78] = typeof(SnackBarDuration);
		_typeTable[79] = typeof(TimePickerKeyboard);
		_typeTable[80] = typeof(TimePickerKeyboardDialogContent);
		_typeTable[81] = typeof(TimePickerListDialogContent);
		_typeTable[82] = typeof(ColorPickerOption);
		_typeTable[83] = typeof(ColorPickerDescriptor);
		_typeTable[84] = typeof(SubHeader);
		_typeTable[85] = typeof(ColorPickerHistory);
		_typeTable[86] = typeof(ColorPickerSwatched);
		_typeTable[87] = typeof(ColorPicker);
		_typeTable[88] = typeof(Color);
		_typeTable[89] = typeof(ColorSpectrumComponents);
		_typeTable[90] = typeof(ColorSpectrumShape);
		_typeTable[91] = typeof(Orientation);
		_typeTable[92] = typeof(Color?);
		_typeTable[93] = typeof(ColorPickerSpectrum);
		_typeTable[94] = typeof(CornerRadius);
		_typeTable[95] = typeof(SolidBrushColorToHexadecimalConverter);
		_typeTable[96] = typeof(ColorPickerTextBox);
		_typeTable[97] = typeof(CheckeredBrush);
		_typeTable[98] = typeof(XamlCompositionBrushBase);
		_typeTable[99] = typeof(TextBox);
		_typeTable[100] = typeof(ColorListItemSelector);
		_typeTable[101] = typeof(Microsoft.UI.Xaml.Controls.StyleSelector);
		_typeTable[102] = typeof(StringToSolidColorBrushConverter);
		_typeTable[103] = typeof(CornerRadiusAutoHalfCorner);
		_typeTable[104] = typeof(DependencyObject);
		_typeTable[105] = typeof(ColorPickerHistoryGridViewCustom);
		_typeTable[106] = typeof(GridView);
		_typeTable[107] = typeof(Thickness);
		_typeTable[108] = typeof(OverlayColorsToSolidColorBrushExtension);
		_typeTable[109] = typeof(MarkupExtension);
		_typeTable[110] = typeof(IList<SolidColorBrush>);
		_typeTable[111] = typeof(FontWeight);
		_typeTable[112] = typeof(ColorPickerOptionCustomButton);
		_typeTable[113] = typeof(ToggleButton);
		_typeTable[114] = typeof(CornerRadiusToDoubleConverter);
		_typeTable[115] = typeof(ICornerRadiusRoundingStrategyConvertion);
		_typeTable[116] = typeof(OneUIColorSpectrum);
		_typeTable[117] = typeof(ColorSpectrum);
		_typeTable[118] = typeof(Vector4);
		_typeTable[119] = typeof(ColorPickerSliderCustom);
		_typeTable[120] = typeof(ColorPickerSlider);
		_typeTable[121] = typeof(Microsoft.UI.Xaml.Controls.Slider);
		_typeTable[122] = typeof(ColorPickerHsvChannel);
		_typeTable[123] = typeof(Thumb);
		_typeTable[124] = typeof(Effects);
		_typeTable[125] = typeof(AttachedShadowBase);
		_typeTable[126] = typeof(AttachedCardShadow);
		_typeTable[127] = typeof(InnerContentClipMode);
		_typeTable[128] = typeof(CornerRadiusCornersConverter);
		_typeTable[129] = typeof(ColorPickerGridViewItemRadiusSelector);
		_typeTable[130] = typeof(ColorPickerSwatchedGridViewCustom);
		_typeTable[131] = typeof(OneUIResources);
		_typeTable[132] = typeof(ResourceDictionary);
		_typeTable[133] = typeof(NumberBadge);
		_typeTable[134] = typeof(BadgeBase);
		_typeTable[135] = typeof(TextBadge);
		_typeTable[136] = typeof(AlertBadge);
		_typeTable[137] = typeof(DotBadge);
		_typeTable[138] = typeof(AddButton);
		_typeTable[139] = typeof(DeleteButton);
		_typeTable[140] = typeof(ContainedButtonBodyColored);
		_typeTable[141] = typeof(ContainedButtonBase);
		_typeTable[142] = typeof(Interaction);
		_typeTable[143] = typeof(BehaviorCollection);
		_typeTable[144] = typeof(DependencyObjectCollection);
		_typeTable[145] = typeof(ProgressCircleIndeterminate);
		_typeTable[146] = typeof(ProgressCircle);
		_typeTable[147] = typeof(ProgressCircleSize);
		_typeTable[148] = typeof(TooltipForTrimmedButtonBehavior);
		_typeTable[149] = typeof(Behavior<FrameworkElement>);
		_typeTable[150] = typeof(Behavior);
		_typeTable[151] = typeof(ContainedButtonBody);
		_typeTable[152] = typeof(ContainedButtonColored);
		_typeTable[153] = typeof(ContainedButton);
		_typeTable[154] = typeof(ContainedButtonType);
		_typeTable[155] = typeof(ContainedButtonSize);
		_typeTable[156] = typeof(ContentButton);
		_typeTable[157] = typeof(ButtonShapeEnum);
		_typeTable[158] = typeof(ContentToggleButton);
		_typeTable[159] = typeof(EditButton);
		_typeTable[160] = typeof(EditButtonType);
		_typeTable[161] = typeof(FlatUnderlineButton);
		_typeTable[162] = typeof(FloatingActionButton);
		_typeTable[163] = typeof(ElevationCorner);
		_typeTable[164] = typeof(BlurLayer);
		_typeTable[165] = typeof(BlurLevel);
		_typeTable[166] = typeof(Samsung.OneUI.WinUI.Tokens.VibrancyLevel);
		_typeTable[167] = typeof(GoToTopButton);
		_typeTable[168] = typeof(Samsung.OneUI.WinUI.Controls.HyperlinkButton);
		_typeTable[169] = typeof(Microsoft.UI.Xaml.Controls.HyperlinkButton);
		_typeTable[170] = typeof(ProgressButton);
		_typeTable[171] = typeof(ProgressButtonType);
		_typeTable[172] = typeof(StringToVisibilityConverter);
		_typeTable[173] = typeof(ImageToVisibilityConverter);
		_typeTable[174] = typeof(Samsung.OneUI.WinUI.Controls.ToolTip);
		_typeTable[175] = typeof(Microsoft.UI.Xaml.Controls.ToolTip);
		_typeTable[176] = typeof(TooltipForTrimmedTextBlockBehavior);
		_typeTable[177] = typeof(KeyTime);
		_typeTable[178] = typeof(NullToVisibilityConverter);
		_typeTable[179] = typeof(Samsung.OneUI.WinUI.Controls.CheckBox);
		_typeTable[180] = typeof(Microsoft.UI.Xaml.Controls.CheckBox);
		_typeTable[181] = typeof(IconElement);
		_typeTable[182] = typeof(CheckBoxType);
		_typeTable[183] = typeof(ChipsItemIconVisibilityConverter);
		_typeTable[184] = typeof(ChipsItemImageIconVisibilityConverter);
		_typeTable[185] = typeof(ChipsItemIconStyleConverter);
		_typeTable[186] = typeof(ChipsItemStyleSelector);
		_typeTable[187] = typeof(CornerRadiusBorderCompensationBehavior);
		_typeTable[188] = typeof(ImageIcon);
		_typeTable[189] = typeof(byte);
		_typeTable[190] = typeof(CommandBarButton);
		_typeTable[191] = typeof(AppBarButton);
		_typeTable[192] = typeof(IntToEnumConverter);
		_typeTable[193] = typeof(Samsung.OneUI.WinUI.Controls.CommandBar);
		_typeTable[194] = typeof(Microsoft.UI.Xaml.Controls.CommandBar);
		_typeTable[195] = typeof(ObservableCollection<MenuFlyoutItemBase>);
		_typeTable[196] = typeof(Collection<MenuFlyoutItemBase>);
		_typeTable[197] = typeof(MenuFlyoutItemBase);
		_typeTable[198] = typeof(ICommand);
		_typeTable[199] = typeof(CommandBarBackButtonType);
		_typeTable[200] = typeof(FlyoutPlacementMode?);
		_typeTable[201] = typeof(FlyoutPlacementMode);
		_typeTable[202] = typeof(FlexibleSpacingBehavior);
		_typeTable[203] = typeof(IconButton);
		_typeTable[204] = typeof(ListFlyout);
		_typeTable[205] = typeof(MenuFlyout);
		_typeTable[206] = typeof(Tooltip);
		_typeTable[207] = typeof(CommandBarToggleButton);
		_typeTable[208] = typeof(AppBarToggleButton);
		_typeTable[209] = typeof(MaxNumberCornerRadiusRoundingStrategy);
		_typeTable[210] = typeof(ContextMenuItem);
		_typeTable[211] = typeof(ListFlyoutItem);
		_typeTable[212] = typeof(MenuFlyoutItem);
		_typeTable[213] = typeof(ICommandBarItemOverflowable);
		_typeTable[214] = typeof(ContextMenuToggle);
		_typeTable[215] = typeof(ListFlyoutToggle);
		_typeTable[216] = typeof(ToggleMenuFlyoutItem);
		_typeTable[217] = typeof(ContextMenuSeparator);
		_typeTable[218] = typeof(ListFlyoutSeparator);
		_typeTable[219] = typeof(MenuFlyoutSeparator);
		_typeTable[220] = typeof(ColorWithTransparencyConverter);
		_typeTable[221] = typeof(DatePickerSpinnerList);
		_typeTable[222] = typeof(DatePickerSpinnerListItem);
		_typeTable[223] = typeof(ScrollList);
		_typeTable[224] = typeof(ObservableCollection<object>);
		_typeTable[225] = typeof(Collection<object>);
		_typeTable[226] = typeof(TypeDate);
		_typeTable[227] = typeof(StringToUpperConverter);
		_typeTable[228] = typeof(DatePickerTextScaleSizeConverter);
		_typeTable[229] = typeof(CalendarViewDayItem);
		_typeTable[230] = typeof(PeriodStyleSelector);
		_typeTable[231] = typeof(PeriodScrollList);
		_typeTable[232] = typeof(DpiChangedTo175StateTrigger);
		_typeTable[233] = typeof(DpiChangedStateTriggerBase);
		_typeTable[234] = typeof(StateTriggerBase);
		_typeTable[235] = typeof(OSVersionType);
		_typeTable[236] = typeof(DpiChangedTo125StateTrigger);
		_typeTable[237] = typeof(Samsung.OneUI.WinUI.Controls.ContentDialog);
		_typeTable[238] = typeof(Microsoft.UI.Xaml.Controls.ContentDialog);
		_typeTable[239] = typeof(HorizontalAlignment);
		_typeTable[240] = typeof(Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer);
		_typeTable[241] = typeof(GridLength);
		_typeTable[242] = typeof(ShowVerticalScrollableIndicatorBehavior);
		_typeTable[243] = typeof(Behavior<DependencyObject>);
		_typeTable[244] = typeof(Divider);
		_typeTable[245] = typeof(DividerType);
		_typeTable[246] = typeof(DropdownList);
		_typeTable[247] = typeof(IList);
		_typeTable[248] = typeof(DropdownListType);
		_typeTable[249] = typeof(DropdownListViewCustom);
		_typeTable[250] = typeof(DropdownCustomControl);
		_typeTable[251] = typeof(OpacityToVisibilityConverter);
		_typeTable[252] = typeof(ExpandableList);
		_typeTable[253] = typeof(TreeView);
		_typeTable[254] = typeof(TreeViewSelectionMode);
		_typeTable[255] = typeof(TransitionCollection);
		_typeTable[256] = typeof(DataTemplate);
		_typeTable[257] = typeof(DataTemplateSelector);
		_typeTable[258] = typeof(IList<TreeViewNode>);
		_typeTable[259] = typeof(TreeViewNode);
		_typeTable[260] = typeof(IList<object>);
		_typeTable[261] = typeof(ExpandableListItemHeader);
		_typeTable[262] = typeof(TreeViewItem);
		_typeTable[263] = typeof(TreeViewItemTemplateSettings);
		_typeTable[264] = typeof(ExpandButton);
		_typeTable[265] = typeof(TreeViewList);
		_typeTable[266] = typeof(FlipViewButton);
		_typeTable[267] = typeof(Samsung.OneUI.WinUI.Controls.FlipView);
		_typeTable[268] = typeof(Microsoft.UI.Xaml.Controls.FlipView);
		_typeTable[269] = typeof(Samsung.OneUI.WinUI.Controls.FlipViewItem);
		_typeTable[270] = typeof(Microsoft.UI.Xaml.Controls.FlipViewItem);
		_typeTable[271] = typeof(IconToggleButton);
		_typeTable[272] = typeof(LevelSlider);
		_typeTable[273] = typeof(SliderMarkerControl);
		_typeTable[274] = typeof(LevelBar);
		_typeTable[275] = typeof(IValueConverter);
		_typeTable[276] = typeof(ItemsRepeater);
		_typeTable[277] = typeof(Layout);
		_typeTable[278] = typeof(ItemCollectionTransitionProvider);
		_typeTable[279] = typeof(ItemsSourceView);
		_typeTable[280] = typeof(StackLayout);
		_typeTable[281] = typeof(VirtualizingLayout);
		_typeTable[282] = typeof(IndexBasedLayoutOrientation);
		_typeTable[283] = typeof(UniformGridLayout);
		_typeTable[284] = typeof(UniformGridLayoutItemsJustification);
		_typeTable[285] = typeof(UniformGridLayoutItemsStretch);
		_typeTable[286] = typeof(MenuFlyoutSubItem);
		_typeTable[287] = typeof(MenuFlyoutPresenter);
		_typeTable[288] = typeof(MultiPane);
		_typeTable[289] = typeof(SplitView);
		_typeTable[290] = typeof(SplitBar);
		_typeTable[291] = typeof(SplitBar.GridResizeDirection);
		_typeTable[292] = typeof(SplitBar.GridResizeBehavior);
		_typeTable[293] = typeof(SplitBar.GripperCursorType);
		_typeTable[294] = typeof(SplitBar.SplitterCursorBehavior);
		_typeTable[295] = typeof(NavigationRail);
		_typeTable[296] = typeof(Microsoft.UI.Xaml.Controls.NavigationView);
		_typeTable[297] = typeof(AutoSuggestBox);
		_typeTable[298] = typeof(NavigationViewDisplayMode);
		_typeTable[299] = typeof(NavigationViewBackButtonVisible);
		_typeTable[300] = typeof(NavigationViewOverflowLabelMode);
		_typeTable[301] = typeof(NavigationViewPaneDisplayMode);
		_typeTable[302] = typeof(NavigationViewSelectionFollowsFocus);
		_typeTable[303] = typeof(NavigationViewShoulderNavigationEnabled);
		_typeTable[304] = typeof(NavigationViewTemplateSettings);
		_typeTable[305] = typeof(NavigationRailItem);
		_typeTable[306] = typeof(Microsoft.UI.Xaml.Controls.NavigationViewItem);
		_typeTable[307] = typeof(NavigationViewItemBase);
		_typeTable[308] = typeof(InfoBadge);
		_typeTable[309] = typeof(NavigationRailItemHeader);
		_typeTable[310] = typeof(Microsoft.UI.Xaml.Controls.NavigationViewItemHeader);
		_typeTable[311] = typeof(NavigationRailItemSeparator);
		_typeTable[312] = typeof(Microsoft.UI.Xaml.Controls.NavigationViewItemSeparator);
		_typeTable[313] = typeof(NavigationRailItemPresenter);
		_typeTable[314] = typeof(Microsoft.UI.Xaml.Controls.Primitives.NavigationViewItemPresenter);
		_typeTable[315] = typeof(NavigationViewItemPresenterTemplateSettings);
		_typeTable[316] = typeof(AnimatedIcon);
		_typeTable[317] = typeof(IAnimatedVisualSource2);
		_typeTable[318] = typeof(IconSource);
		_typeTable[319] = typeof(AnimatedChevronUpDownSmallVisualSource);
		_typeTable[320] = typeof(IReadOnlyDictionary<string, double>);
		_typeTable[321] = typeof(FlyoutPresenter);
		_typeTable[322] = typeof(ItemsRepeaterScrollHost);
		_typeTable[323] = typeof(Samsung.OneUI.WinUI.Controls.NavigationView);
		_typeTable[324] = typeof(Samsung.OneUI.WinUI.Controls.NavigationViewItem);
		_typeTable[325] = typeof(Samsung.OneUI.WinUI.Controls.NavigationViewItemHeader);
		_typeTable[326] = typeof(Samsung.OneUI.WinUI.Controls.NavigationViewItemSeparator);
		_typeTable[327] = typeof(Samsung.OneUI.WinUI.Controls.NavigationViewItemPresenter);
		_typeTable[328] = typeof(PageIndicator);
		_typeTable[329] = typeof(PopOverPresenter);
		_typeTable[330] = typeof(Samsung.OneUI.WinUI.Controls.ProgressBar);
		_typeTable[331] = typeof(Microsoft.UI.Xaml.Controls.ProgressBar);
		_typeTable[332] = typeof(RangeBase);
		_typeTable[333] = typeof(ProgressBarTemplateSettings);
		_typeTable[334] = typeof(ProgressCircleDeterminate);
		_typeTable[335] = typeof(ProgressCircleDeterminateType);
		_typeTable[336] = typeof(Samsung.OneUI.WinUI.Controls.RadioButtons);
		_typeTable[337] = typeof(Microsoft.UI.Xaml.Controls.RadioButtons);
		_typeTable[338] = typeof(Samsung.OneUI.WinUI.Controls.RadioButton);
		_typeTable[339] = typeof(Microsoft.UI.Xaml.Controls.RadioButton);
		_typeTable[340] = typeof(GridUnitType);
		_typeTable[341] = typeof(ScrollModeToBoolConverter);
		_typeTable[342] = typeof(DoubleToThicknessTopAndBottomConverter);
		_typeTable[343] = typeof(ScrollBar);
		_typeTable[344] = typeof(RepeatButton);
		_typeTable[345] = typeof(ThumbDisabledScrollBarDimensionsBehavior);
		_typeTable[346] = typeof(Behavior<Thumb>);
		_typeTable[347] = typeof(DataTriggerBehavior);
		_typeTable[348] = typeof(Trigger);
		_typeTable[349] = typeof(ActionCollection);
		_typeTable[350] = typeof(ComparisonConditionType);
		_typeTable[351] = typeof(GoToStateAction);
		_typeTable[352] = typeof(ThumbCompositeTransformScaleStateTrigger);
		_typeTable[353] = typeof(SearchPopupListFooterButton);
		_typeTable[354] = typeof(SearchPopupList);
		_typeTable[355] = typeof(ObservableCollection<SearchPopupListItem>);
		_typeTable[356] = typeof(Collection<SearchPopupListItem>);
		_typeTable[357] = typeof(SearchPopupListItem);
		_typeTable[358] = typeof(FilterTextBlock);
		_typeTable[359] = typeof(SearchPopup);
		_typeTable[360] = typeof(SearchPopupRemoveButton);
		_typeTable[361] = typeof(SearchPopupTextBox);
		_typeTable[362] = typeof(BackdropBlurExtension);
		_typeTable[363] = typeof(Samsung.OneUI.WinUI.Controls.Slider);
		_typeTable[364] = typeof(SliderBase);
		_typeTable[365] = typeof(ShockValueType);
		_typeTable[366] = typeof(SliderType);
		_typeTable[367] = typeof(BufferSlider);
		_typeTable[368] = typeof(CenterSlider);
		_typeTable[369] = typeof(SubAppBar);
		_typeTable[370] = typeof(Samsung.OneUI.WinUI.Controls.TabView);
		_typeTable[371] = typeof(Pivot);
		_typeTable[372] = typeof(TabViewType);
		_typeTable[373] = typeof(TabItem);
		_typeTable[374] = typeof(PivotItem);
		_typeTable[375] = typeof(PivotHeaderItem);
		_typeTable[376] = typeof(ThicknessSideConverter);
		_typeTable[377] = typeof(TextField);
		_typeTable[378] = typeof(TextFieldType);
		_typeTable[379] = typeof(ThumbnailRadious);
		_typeTable[380] = typeof(ThumbnailRadiousVisualizationMode);
		_typeTable[381] = typeof(ThumbnailRadiousGridView);
		_typeTable[382] = typeof(Titlebar);
		_typeTable[383] = typeof(Samsung.OneUI.WinUI.Controls.ToggleSwitch);
		_typeTable[384] = typeof(ToggleSwitchType);
		_typeTable[385] = typeof(ToggleSwitchGroup);
		_typeTable[386] = typeof(Microsoft.UI.Xaml.Controls.ToggleSwitch);
		_typeTable[387] = typeof(DpiChangedTo150StateTrigger);
		_typeTable[388] = typeof(DpiChangedTo100StateTrigger);
		_typeTable[389] = typeof(ToggleSwitchLoadedBehavior);
		_typeTable[390] = typeof(Behavior<Microsoft.UI.Xaml.Controls.ToggleSwitch>);
	}

	private int LookupTypeIndexByName(string typeName)
	{
		if (_typeNameTable == null)
		{
			InitTypeTables();
		}
		for (int i = 0; i < _typeNameTable.Length; i++)
		{
			if (string.CompareOrdinal(_typeNameTable[i], typeName) == 0)
			{
				return i;
			}
		}
		return -1;
	}

	private int LookupTypeIndexByType(Type type)
	{
		if (_typeTable == null)
		{
			InitTypeTables();
		}
		for (int i = 0; i < _typeTable.Length; i++)
		{
			if (type == _typeTable[i])
			{
				return i;
			}
		}
		return -1;
	}

	private object Activate_0_BGBlur()
	{
		return new BGBlur();
	}

	private object Activate_10_CardType()
	{
		return new CardType();
	}

	private object Activate_15_CardTypeListView()
	{
		return new CardTypeListView();
	}

	private object Activate_16_List()
	{
		return new List<CardTypeItem>();
	}

	private object Activate_17_CardTypeItem()
	{
		return new CardTypeItem();
	}

	private object Activate_21_WrapPanel()
	{
		return new WrapPanel();
	}

	private object Activate_24_Chips()
	{
		return new Chips();
	}

	private object Activate_25_ObservableCollection()
	{
		return new ObservableCollection<ChipsItem>();
	}

	private object Activate_26_Collection()
	{
		return new Collection<ChipsItem>();
	}

	private object Activate_27_ChipsItem()
	{
		return new ChipsItem();
	}

	private object Activate_34_Toast()
	{
		return new Toast();
	}

	private object Activate_37_ColorPickerControl()
	{
		return new ColorPickerControl();
	}

	private object Activate_42_List()
	{
		return new List<ColorInfo>();
	}

	private object Activate_44_FlatButton()
	{
		return new FlatButton();
	}

	private object Activate_45_FlatButtonBase()
	{
		return new FlatButtonBase();
	}

	private object Activate_51_ColorPickerDialog()
	{
		return new ColorPickerDialog();
	}

	private object Activate_52_List()
	{
		return new List<string>();
	}

	private object Activate_53_DatePicker()
	{
		return new Samsung.OneUI.WinUI.Controls.DatePicker();
	}

	private object Activate_56_DatePickerDialogContent()
	{
		return new DatePickerDialogContent();
	}

	private object Activate_58_DateTimePickerList()
	{
		return new DateTimePickerList();
	}

	private object Activate_59_TimePickerList()
	{
		return new TimePickerList();
	}

	private object Activate_65_OneUIContentDialogContent()
	{
		return new OneUIContentDialogContent();
	}

	private object Activate_68_ListViewCustom()
	{
		return new ListViewCustom();
	}

	private object Activate_70_Responsiveness()
	{
		return new Responsiveness();
	}

	private object Activate_74_BoolToVisibilityConverter()
	{
		return new BoolToVisibilityConverter();
	}

	private object Activate_75_SnackBarButton()
	{
		return new SnackBarButton();
	}

	private object Activate_77_SnackBar()
	{
		return new SnackBar();
	}

	private object Activate_79_TimePickerKeyboard()
	{
		return new TimePickerKeyboard();
	}

	private object Activate_82_ColorPickerOption()
	{
		return new ColorPickerOption();
	}

	private object Activate_83_ColorPickerDescriptor()
	{
		return new ColorPickerDescriptor();
	}

	private object Activate_84_SubHeader()
	{
		return new SubHeader();
	}

	private object Activate_85_ColorPickerHistory()
	{
		return new ColorPickerHistory();
	}

	private object Activate_86_ColorPickerSwatched()
	{
		return new ColorPickerSwatched();
	}

	private object Activate_87_ColorPicker()
	{
		return new ColorPicker();
	}

	private object Activate_93_ColorPickerSpectrum()
	{
		return new ColorPickerSpectrum();
	}

	private object Activate_95_SolidBrushColorToHexadecimalConverter()
	{
		return new SolidBrushColorToHexadecimalConverter();
	}

	private object Activate_96_ColorPickerTextBox()
	{
		return new ColorPickerTextBox();
	}

	private object Activate_97_CheckeredBrush()
	{
		return new CheckeredBrush();
	}

	private object Activate_100_ColorListItemSelector()
	{
		return new ColorListItemSelector();
	}

	private object Activate_102_StringToSolidColorBrushConverter()
	{
		return new StringToSolidColorBrushConverter();
	}

	private object Activate_103_CornerRadiusAutoHalfCorner()
	{
		return new CornerRadiusAutoHalfCorner();
	}

	private object Activate_105_ColorPickerHistoryGridViewCustom()
	{
		return new ColorPickerHistoryGridViewCustom();
	}

	private object Activate_108_OverlayColorsToSolidColorBrushExtension()
	{
		return new OverlayColorsToSolidColorBrushExtension();
	}

	private object Activate_112_ColorPickerOptionCustomButton()
	{
		return new ColorPickerOptionCustomButton();
	}

	private object Activate_114_CornerRadiusToDoubleConverter()
	{
		return new CornerRadiusToDoubleConverter();
	}

	private object Activate_116_OneUIColorSpectrum()
	{
		return new OneUIColorSpectrum();
	}

	private object Activate_117_ColorSpectrum()
	{
		return new ColorSpectrum();
	}

	private object Activate_119_ColorPickerSliderCustom()
	{
		return new ColorPickerSliderCustom();
	}

	private object Activate_120_ColorPickerSlider()
	{
		return new ColorPickerSlider();
	}

	private object Activate_126_AttachedCardShadow()
	{
		return new AttachedCardShadow();
	}

	private object Activate_128_CornerRadiusCornersConverter()
	{
		return new CornerRadiusCornersConverter();
	}

	private object Activate_129_ColorPickerGridViewItemRadiusSelector()
	{
		return new ColorPickerGridViewItemRadiusSelector();
	}

	private object Activate_130_ColorPickerSwatchedGridViewCustom()
	{
		return new ColorPickerSwatchedGridViewCustom();
	}

	private object Activate_131_OneUIResources()
	{
		return new OneUIResources();
	}

	private object Activate_133_NumberBadge()
	{
		return new NumberBadge();
	}

	private object Activate_135_TextBadge()
	{
		return new TextBadge();
	}

	private object Activate_136_AlertBadge()
	{
		return new AlertBadge();
	}

	private object Activate_137_DotBadge()
	{
		return new DotBadge();
	}

	private object Activate_138_AddButton()
	{
		return new AddButton();
	}

	private object Activate_139_DeleteButton()
	{
		return new DeleteButton();
	}

	private object Activate_140_ContainedButtonBodyColored()
	{
		return new ContainedButtonBodyColored();
	}

	private object Activate_141_ContainedButtonBase()
	{
		return new ContainedButtonBase();
	}

	private object Activate_143_BehaviorCollection()
	{
		return new BehaviorCollection();
	}

	private object Activate_145_ProgressCircleIndeterminate()
	{
		return new ProgressCircleIndeterminate();
	}

	private object Activate_148_TooltipForTrimmedButtonBehavior()
	{
		return new TooltipForTrimmedButtonBehavior();
	}

	private object Activate_151_ContainedButtonBody()
	{
		return new ContainedButtonBody();
	}

	private object Activate_152_ContainedButtonColored()
	{
		return new ContainedButtonColored();
	}

	private object Activate_153_ContainedButton()
	{
		return new ContainedButton();
	}

	private object Activate_156_ContentButton()
	{
		return new ContentButton();
	}

	private object Activate_158_ContentToggleButton()
	{
		return new ContentToggleButton();
	}

	private object Activate_159_EditButton()
	{
		return new EditButton();
	}

	private object Activate_161_FlatUnderlineButton()
	{
		return new FlatUnderlineButton();
	}

	private object Activate_162_FloatingActionButton()
	{
		return new FloatingActionButton();
	}

	private object Activate_163_ElevationCorner()
	{
		return new ElevationCorner();
	}

	private object Activate_164_BlurLayer()
	{
		return new BlurLayer();
	}

	private object Activate_167_GoToTopButton()
	{
		return new GoToTopButton();
	}

	private object Activate_168_HyperlinkButton()
	{
		return new Samsung.OneUI.WinUI.Controls.HyperlinkButton();
	}

	private object Activate_170_ProgressButton()
	{
		return new ProgressButton();
	}

	private object Activate_172_StringToVisibilityConverter()
	{
		return new StringToVisibilityConverter();
	}

	private object Activate_173_ImageToVisibilityConverter()
	{
		return new ImageToVisibilityConverter();
	}

	private object Activate_174_ToolTip()
	{
		return new Samsung.OneUI.WinUI.Controls.ToolTip();
	}

	private object Activate_176_TooltipForTrimmedTextBlockBehavior()
	{
		return new TooltipForTrimmedTextBlockBehavior();
	}

	private object Activate_178_NullToVisibilityConverter()
	{
		return new NullToVisibilityConverter();
	}

	private object Activate_179_CheckBox()
	{
		return new Samsung.OneUI.WinUI.Controls.CheckBox();
	}

	private object Activate_183_ChipsItemIconVisibilityConverter()
	{
		return new ChipsItemIconVisibilityConverter();
	}

	private object Activate_184_ChipsItemImageIconVisibilityConverter()
	{
		return new ChipsItemImageIconVisibilityConverter();
	}

	private object Activate_185_ChipsItemIconStyleConverter()
	{
		return new ChipsItemIconStyleConverter();
	}

	private object Activate_186_ChipsItemStyleSelector()
	{
		return new ChipsItemStyleSelector();
	}

	private object Activate_187_CornerRadiusBorderCompensationBehavior()
	{
		return new CornerRadiusBorderCompensationBehavior();
	}

	private object Activate_188_ImageIcon()
	{
		return new ImageIcon();
	}

	private object Activate_190_CommandBarButton()
	{
		return new CommandBarButton();
	}

	private object Activate_192_IntToEnumConverter()
	{
		return new IntToEnumConverter();
	}

	private object Activate_193_CommandBar()
	{
		return new Samsung.OneUI.WinUI.Controls.CommandBar();
	}

	private object Activate_195_ObservableCollection()
	{
		return new ObservableCollection<MenuFlyoutItemBase>();
	}

	private object Activate_196_Collection()
	{
		return new Collection<MenuFlyoutItemBase>();
	}

	private object Activate_202_FlexibleSpacingBehavior()
	{
		return new FlexibleSpacingBehavior();
	}

	private object Activate_203_IconButton()
	{
		return new IconButton();
	}

	private object Activate_204_ListFlyout()
	{
		return new ListFlyout();
	}

	private object Activate_207_CommandBarToggleButton()
	{
		return new CommandBarToggleButton();
	}

	private object Activate_209_MaxNumberCornerRadiusRoundingStrategy()
	{
		return new MaxNumberCornerRadiusRoundingStrategy();
	}

	private object Activate_210_ContextMenuItem()
	{
		return new ContextMenuItem();
	}

	private object Activate_211_ListFlyoutItem()
	{
		return new ListFlyoutItem();
	}

	private object Activate_214_ContextMenuToggle()
	{
		return new ContextMenuToggle();
	}

	private object Activate_215_ListFlyoutToggle()
	{
		return new ListFlyoutToggle();
	}

	private object Activate_217_ContextMenuSeparator()
	{
		return new ContextMenuSeparator();
	}

	private object Activate_218_ListFlyoutSeparator()
	{
		return new ListFlyoutSeparator();
	}

	private object Activate_220_ColorWithTransparencyConverter()
	{
		return new ColorWithTransparencyConverter();
	}

	private object Activate_223_ScrollList()
	{
		return new ScrollList();
	}

	private object Activate_224_ObservableCollection()
	{
		return new ObservableCollection<object>();
	}

	private object Activate_225_Collection()
	{
		return new Collection<object>();
	}

	private object Activate_227_StringToUpperConverter()
	{
		return new StringToUpperConverter();
	}

	private object Activate_228_DatePickerTextScaleSizeConverter()
	{
		return new DatePickerTextScaleSizeConverter();
	}

	private object Activate_230_PeriodStyleSelector()
	{
		return new PeriodStyleSelector();
	}

	private object Activate_231_PeriodScrollList()
	{
		return new PeriodScrollList();
	}

	private object Activate_232_DpiChangedTo175StateTrigger()
	{
		return new DpiChangedTo175StateTrigger();
	}

	private object Activate_236_DpiChangedTo125StateTrigger()
	{
		return new DpiChangedTo125StateTrigger();
	}

	private object Activate_237_ContentDialog()
	{
		return new Samsung.OneUI.WinUI.Controls.ContentDialog();
	}

	private object Activate_240_ScrollViewer()
	{
		return new Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer();
	}

	private object Activate_242_ShowVerticalScrollableIndicatorBehavior()
	{
		return new ShowVerticalScrollableIndicatorBehavior();
	}

	private object Activate_244_Divider()
	{
		return new Divider();
	}

	private object Activate_246_DropdownList()
	{
		return new DropdownList();
	}

	private object Activate_249_DropdownListViewCustom()
	{
		return new DropdownListViewCustom();
	}

	private object Activate_250_DropdownCustomControl()
	{
		return new DropdownCustomControl();
	}

	private object Activate_251_OpacityToVisibilityConverter()
	{
		return new OpacityToVisibilityConverter();
	}

	private object Activate_252_ExpandableList()
	{
		return new ExpandableList();
	}

	private object Activate_253_TreeView()
	{
		return new TreeView();
	}

	private object Activate_259_TreeViewNode()
	{
		return new TreeViewNode();
	}

	private object Activate_261_ExpandableListItemHeader()
	{
		return new ExpandableListItemHeader();
	}

	private object Activate_262_TreeViewItem()
	{
		return new TreeViewItem();
	}

	private object Activate_263_TreeViewItemTemplateSettings()
	{
		return new TreeViewItemTemplateSettings();
	}

	private object Activate_264_ExpandButton()
	{
		return new ExpandButton();
	}

	private object Activate_265_TreeViewList()
	{
		return new TreeViewList();
	}

	private object Activate_266_FlipViewButton()
	{
		return new FlipViewButton();
	}

	private object Activate_267_FlipView()
	{
		return new Samsung.OneUI.WinUI.Controls.FlipView();
	}

	private object Activate_271_IconToggleButton()
	{
		return new IconToggleButton();
	}

	private object Activate_272_LevelSlider()
	{
		return new LevelSlider();
	}

	private object Activate_273_SliderMarkerControl()
	{
		return new SliderMarkerControl();
	}

	private object Activate_274_LevelBar()
	{
		return new LevelBar();
	}

	private object Activate_276_ItemsRepeater()
	{
		return new ItemsRepeater();
	}

	private object Activate_278_ItemCollectionTransitionProvider()
	{
		return new ItemCollectionTransitionProvider();
	}

	private object Activate_280_StackLayout()
	{
		return new StackLayout();
	}

	private object Activate_281_VirtualizingLayout()
	{
		return new VirtualizingLayout();
	}

	private object Activate_283_UniformGridLayout()
	{
		return new UniformGridLayout();
	}

	private object Activate_288_MultiPane()
	{
		return new MultiPane();
	}

	private object Activate_290_SplitBar()
	{
		return new SplitBar();
	}

	private object Activate_295_NavigationRail()
	{
		return new NavigationRail();
	}

	private object Activate_296_NavigationView()
	{
		return new Microsoft.UI.Xaml.Controls.NavigationView();
	}

	private object Activate_304_NavigationViewTemplateSettings()
	{
		return new NavigationViewTemplateSettings();
	}

	private object Activate_305_NavigationRailItem()
	{
		return new NavigationRailItem();
	}

	private object Activate_306_NavigationViewItem()
	{
		return new Microsoft.UI.Xaml.Controls.NavigationViewItem();
	}

	private object Activate_308_InfoBadge()
	{
		return new InfoBadge();
	}

	private object Activate_309_NavigationRailItemHeader()
	{
		return new NavigationRailItemHeader();
	}

	private object Activate_310_NavigationViewItemHeader()
	{
		return new Microsoft.UI.Xaml.Controls.NavigationViewItemHeader();
	}

	private object Activate_311_NavigationRailItemSeparator()
	{
		return new NavigationRailItemSeparator();
	}

	private object Activate_312_NavigationViewItemSeparator()
	{
		return new Microsoft.UI.Xaml.Controls.NavigationViewItemSeparator();
	}

	private object Activate_313_NavigationRailItemPresenter()
	{
		return new NavigationRailItemPresenter();
	}

	private object Activate_314_NavigationViewItemPresenter()
	{
		return new Microsoft.UI.Xaml.Controls.Primitives.NavigationViewItemPresenter();
	}

	private object Activate_315_NavigationViewItemPresenterTemplateSettings()
	{
		return new NavigationViewItemPresenterTemplateSettings();
	}

	private object Activate_316_AnimatedIcon()
	{
		return new AnimatedIcon();
	}

	private object Activate_319_AnimatedChevronUpDownSmallVisualSource()
	{
		return new AnimatedChevronUpDownSmallVisualSource();
	}

	private object Activate_322_ItemsRepeaterScrollHost()
	{
		return new ItemsRepeaterScrollHost();
	}

	private object Activate_323_NavigationView()
	{
		return new Samsung.OneUI.WinUI.Controls.NavigationView();
	}

	private object Activate_324_NavigationViewItem()
	{
		return new Samsung.OneUI.WinUI.Controls.NavigationViewItem();
	}

	private object Activate_325_NavigationViewItemHeader()
	{
		return new Samsung.OneUI.WinUI.Controls.NavigationViewItemHeader();
	}

	private object Activate_326_NavigationViewItemSeparator()
	{
		return new Samsung.OneUI.WinUI.Controls.NavigationViewItemSeparator();
	}

	private object Activate_327_NavigationViewItemPresenter()
	{
		return new Samsung.OneUI.WinUI.Controls.NavigationViewItemPresenter();
	}

	private object Activate_328_PageIndicator()
	{
		return new PageIndicator();
	}

	private object Activate_329_PopOverPresenter()
	{
		return new PopOverPresenter();
	}

	private object Activate_330_ProgressBar()
	{
		return new Samsung.OneUI.WinUI.Controls.ProgressBar();
	}

	private object Activate_331_ProgressBar()
	{
		return new Microsoft.UI.Xaml.Controls.ProgressBar();
	}

	private object Activate_334_ProgressCircleDeterminate()
	{
		return new ProgressCircleDeterminate();
	}

	private object Activate_336_RadioButtons()
	{
		return new Samsung.OneUI.WinUI.Controls.RadioButtons();
	}

	private object Activate_337_RadioButtons()
	{
		return new Microsoft.UI.Xaml.Controls.RadioButtons();
	}

	private object Activate_338_RadioButton()
	{
		return new Samsung.OneUI.WinUI.Controls.RadioButton();
	}

	private object Activate_341_ScrollModeToBoolConverter()
	{
		return new ScrollModeToBoolConverter();
	}

	private object Activate_342_DoubleToThicknessTopAndBottomConverter()
	{
		return new DoubleToThicknessTopAndBottomConverter();
	}

	private object Activate_345_ThumbDisabledScrollBarDimensionsBehavior()
	{
		return new ThumbDisabledScrollBarDimensionsBehavior();
	}

	private object Activate_347_DataTriggerBehavior()
	{
		return new DataTriggerBehavior();
	}

	private object Activate_349_ActionCollection()
	{
		return new ActionCollection();
	}

	private object Activate_351_GoToStateAction()
	{
		return new GoToStateAction();
	}

	private object Activate_352_ThumbCompositeTransformScaleStateTrigger()
	{
		return new ThumbCompositeTransformScaleStateTrigger();
	}

	private object Activate_353_SearchPopupListFooterButton()
	{
		return new SearchPopupListFooterButton();
	}

	private object Activate_354_SearchPopupList()
	{
		return new SearchPopupList();
	}

	private object Activate_355_ObservableCollection()
	{
		return new ObservableCollection<SearchPopupListItem>();
	}

	private object Activate_356_Collection()
	{
		return new Collection<SearchPopupListItem>();
	}

	private object Activate_357_SearchPopupListItem()
	{
		return new SearchPopupListItem();
	}

	private object Activate_358_FilterTextBlock()
	{
		return new FilterTextBlock();
	}

	private object Activate_359_SearchPopup()
	{
		return new SearchPopup();
	}

	private object Activate_360_SearchPopupRemoveButton()
	{
		return new SearchPopupRemoveButton();
	}

	private object Activate_361_SearchPopupTextBox()
	{
		return new SearchPopupTextBox();
	}

	private object Activate_362_BackdropBlurExtension()
	{
		return new BackdropBlurExtension();
	}

	private object Activate_363_Slider()
	{
		return new Samsung.OneUI.WinUI.Controls.Slider();
	}

	private object Activate_367_BufferSlider()
	{
		return new BufferSlider();
	}

	private object Activate_368_CenterSlider()
	{
		return new CenterSlider();
	}

	private object Activate_369_SubAppBar()
	{
		return new SubAppBar();
	}

	private object Activate_370_TabView()
	{
		return new Samsung.OneUI.WinUI.Controls.TabView();
	}

	private object Activate_373_TabItem()
	{
		return new TabItem();
	}

	private object Activate_376_ThicknessSideConverter()
	{
		return new ThicknessSideConverter();
	}

	private object Activate_377_TextField()
	{
		return new TextField();
	}

	private object Activate_379_ThumbnailRadious()
	{
		return new ThumbnailRadious();
	}

	private object Activate_381_ThumbnailRadiousGridView()
	{
		return new ThumbnailRadiousGridView();
	}

	private object Activate_382_Titlebar()
	{
		return new Titlebar();
	}

	private object Activate_383_ToggleSwitch()
	{
		return new Samsung.OneUI.WinUI.Controls.ToggleSwitch();
	}

	private object Activate_385_ToggleSwitchGroup()
	{
		return new ToggleSwitchGroup();
	}

	private object Activate_387_DpiChangedTo150StateTrigger()
	{
		return new DpiChangedTo150StateTrigger();
	}

	private object Activate_388_DpiChangedTo100StateTrigger()
	{
		return new DpiChangedTo100StateTrigger();
	}

	private object Activate_389_ToggleSwitchLoadedBehavior()
	{
		return new ToggleSwitchLoadedBehavior();
	}

	private void StaticInitializer_0_BGBlur()
	{
		RuntimeHelpers.RunClassConstructor(typeof(BGBlur).TypeHandle);
	}

	private void StaticInitializer_3_VibrancyLevel()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.Controls.VibrancyLevel).TypeHandle);
	}

	private void StaticInitializer_4_Enum()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Enum).TypeHandle);
	}

	private void StaticInitializer_5_ValueType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ValueType).TypeHandle);
	}

	private void StaticInitializer_10_CardType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(CardType).TypeHandle);
	}

	private void StaticInitializer_15_CardTypeListView()
	{
		RuntimeHelpers.RunClassConstructor(typeof(CardTypeListView).TypeHandle);
	}

	private void StaticInitializer_16_List()
	{
		RuntimeHelpers.RunClassConstructor(typeof(List<CardTypeItem>).TypeHandle);
	}

	private void StaticInitializer_17_CardTypeItem()
	{
		RuntimeHelpers.RunClassConstructor(typeof(CardTypeItem).TypeHandle);
	}

	private void StaticInitializer_18_EventHandler()
	{
		RuntimeHelpers.RunClassConstructor(typeof(EventHandler).TypeHandle);
	}

	private void StaticInitializer_19_MulticastDelegate()
	{
		RuntimeHelpers.RunClassConstructor(typeof(MulticastDelegate).TypeHandle);
	}

	private void StaticInitializer_20_Delegate()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Delegate).TypeHandle);
	}

	private void StaticInitializer_21_WrapPanel()
	{
		RuntimeHelpers.RunClassConstructor(typeof(WrapPanel).TypeHandle);
	}

	private void StaticInitializer_24_Chips()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Chips).TypeHandle);
	}

	private void StaticInitializer_25_ObservableCollection()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ObservableCollection<ChipsItem>).TypeHandle);
	}

	private void StaticInitializer_26_Collection()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Collection<ChipsItem>).TypeHandle);
	}

	private void StaticInitializer_27_ChipsItem()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ChipsItem).TypeHandle);
	}

	private void StaticInitializer_30_ChipsItemTemplate()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ChipsItemTemplate).TypeHandle);
	}

	private void StaticInitializer_31_ChipsItemType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ChipsItemType).TypeHandle);
	}

	private void StaticInitializer_33_ChipsItemGroupTemplate()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ChipsItemGroupTemplate).TypeHandle);
	}

	private void StaticInitializer_34_Toast()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Toast).TypeHandle);
	}

	private void StaticInitializer_35_ToastDuration()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ToastDuration).TypeHandle);
	}

	private void StaticInitializer_37_ColorPickerControl()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorPickerControl).TypeHandle);
	}

	private void StaticInitializer_38_Nullable()
	{
		RuntimeHelpers.RunClassConstructor(typeof(double?).TypeHandle);
	}

	private void StaticInitializer_42_List()
	{
		RuntimeHelpers.RunClassConstructor(typeof(List<ColorInfo>).TypeHandle);
	}

	private void StaticInitializer_43_ColorInfo()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorInfo).TypeHandle);
	}

	private void StaticInitializer_44_FlatButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(FlatButton).TypeHandle);
	}

	private void StaticInitializer_45_FlatButtonBase()
	{
		RuntimeHelpers.RunClassConstructor(typeof(FlatButtonBase).TypeHandle);
	}

	private void StaticInitializer_47_FlatButtonSize()
	{
		RuntimeHelpers.RunClassConstructor(typeof(FlatButtonSize).TypeHandle);
	}

	private void StaticInitializer_48_FlatButtonType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(FlatButtonType).TypeHandle);
	}

	private void StaticInitializer_51_ColorPickerDialog()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorPickerDialog).TypeHandle);
	}

	private void StaticInitializer_52_List()
	{
		RuntimeHelpers.RunClassConstructor(typeof(List<string>).TypeHandle);
	}

	private void StaticInitializer_53_DatePicker()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.Controls.DatePicker).TypeHandle);
	}

	private void StaticInitializer_55_DateTime()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DateTime).TypeHandle);
	}

	private void StaticInitializer_56_DatePickerDialogContent()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DatePickerDialogContent).TypeHandle);
	}

	private void StaticInitializer_58_DateTimePickerList()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DateTimePickerList).TypeHandle);
	}

	private void StaticInitializer_59_TimePickerList()
	{
		RuntimeHelpers.RunClassConstructor(typeof(TimePickerList).TypeHandle);
	}

	private void StaticInitializer_60_Nullable()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DateTime?).TypeHandle);
	}

	private void StaticInitializer_61_TimeType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(TimeType).TypeHandle);
	}

	private void StaticInitializer_62_TimePeriod()
	{
		RuntimeHelpers.RunClassConstructor(typeof(TimePeriod).TypeHandle);
	}

	private void StaticInitializer_63_TimeSpan()
	{
		RuntimeHelpers.RunClassConstructor(typeof(TimeSpan).TypeHandle);
	}

	private void StaticInitializer_64_DateTimePickerDialogContent()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DateTimePickerDialogContent).TypeHandle);
	}

	private void StaticInitializer_65_OneUIContentDialogContent()
	{
		RuntimeHelpers.RunClassConstructor(typeof(OneUIContentDialogContent).TypeHandle);
	}

	private void StaticInitializer_68_ListViewCustom()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ListViewCustom).TypeHandle);
	}

	private void StaticInitializer_70_Responsiveness()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Responsiveness).TypeHandle);
	}

	private void StaticInitializer_71_FlexibleSpacingType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(FlexibleSpacingType).TypeHandle);
	}

	private void StaticInitializer_72_Nullable()
	{
		RuntimeHelpers.RunClassConstructor(typeof(bool?).TypeHandle);
	}

	private void StaticInitializer_73_SingleChoiceDialogContent()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SingleChoiceDialogContent).TypeHandle);
	}

	private void StaticInitializer_74_BoolToVisibilityConverter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(BoolToVisibilityConverter).TypeHandle);
	}

	private void StaticInitializer_75_SnackBarButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SnackBarButton).TypeHandle);
	}

	private void StaticInitializer_76_SnackBarButtonType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SnackBarButtonType).TypeHandle);
	}

	private void StaticInitializer_77_SnackBar()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SnackBar).TypeHandle);
	}

	private void StaticInitializer_78_SnackBarDuration()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SnackBarDuration).TypeHandle);
	}

	private void StaticInitializer_79_TimePickerKeyboard()
	{
		RuntimeHelpers.RunClassConstructor(typeof(TimePickerKeyboard).TypeHandle);
	}

	private void StaticInitializer_80_TimePickerKeyboardDialogContent()
	{
		RuntimeHelpers.RunClassConstructor(typeof(TimePickerKeyboardDialogContent).TypeHandle);
	}

	private void StaticInitializer_81_TimePickerListDialogContent()
	{
		RuntimeHelpers.RunClassConstructor(typeof(TimePickerListDialogContent).TypeHandle);
	}

	private void StaticInitializer_82_ColorPickerOption()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorPickerOption).TypeHandle);
	}

	private void StaticInitializer_83_ColorPickerDescriptor()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorPickerDescriptor).TypeHandle);
	}

	private void StaticInitializer_84_SubHeader()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SubHeader).TypeHandle);
	}

	private void StaticInitializer_85_ColorPickerHistory()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorPickerHistory).TypeHandle);
	}

	private void StaticInitializer_86_ColorPickerSwatched()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorPickerSwatched).TypeHandle);
	}

	private void StaticInitializer_87_ColorPicker()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorPicker).TypeHandle);
	}

	private void StaticInitializer_88_Color()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Color).TypeHandle);
	}

	private void StaticInitializer_89_ColorSpectrumComponents()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorSpectrumComponents).TypeHandle);
	}

	private void StaticInitializer_90_ColorSpectrumShape()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorSpectrumShape).TypeHandle);
	}

	private void StaticInitializer_92_Nullable()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Color?).TypeHandle);
	}

	private void StaticInitializer_93_ColorPickerSpectrum()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorPickerSpectrum).TypeHandle);
	}

	private void StaticInitializer_94_CornerRadius()
	{
		RuntimeHelpers.RunClassConstructor(typeof(CornerRadius).TypeHandle);
	}

	private void StaticInitializer_95_SolidBrushColorToHexadecimalConverter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SolidBrushColorToHexadecimalConverter).TypeHandle);
	}

	private void StaticInitializer_96_ColorPickerTextBox()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorPickerTextBox).TypeHandle);
	}

	private void StaticInitializer_97_CheckeredBrush()
	{
		RuntimeHelpers.RunClassConstructor(typeof(CheckeredBrush).TypeHandle);
	}

	private void StaticInitializer_100_ColorListItemSelector()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorListItemSelector).TypeHandle);
	}

	private void StaticInitializer_102_StringToSolidColorBrushConverter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(StringToSolidColorBrushConverter).TypeHandle);
	}

	private void StaticInitializer_103_CornerRadiusAutoHalfCorner()
	{
		RuntimeHelpers.RunClassConstructor(typeof(CornerRadiusAutoHalfCorner).TypeHandle);
	}

	private void StaticInitializer_105_ColorPickerHistoryGridViewCustom()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorPickerHistoryGridViewCustom).TypeHandle);
	}

	private void StaticInitializer_107_Thickness()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Thickness).TypeHandle);
	}

	private void StaticInitializer_108_OverlayColorsToSolidColorBrushExtension()
	{
		RuntimeHelpers.RunClassConstructor(typeof(OverlayColorsToSolidColorBrushExtension).TypeHandle);
	}

	private void StaticInitializer_110_IList()
	{
		RuntimeHelpers.RunClassConstructor(typeof(IList<SolidColorBrush>).TypeHandle);
	}

	private void StaticInitializer_111_FontWeight()
	{
		RuntimeHelpers.RunClassConstructor(typeof(FontWeight).TypeHandle);
	}

	private void StaticInitializer_112_ColorPickerOptionCustomButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorPickerOptionCustomButton).TypeHandle);
	}

	private void StaticInitializer_114_CornerRadiusToDoubleConverter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(CornerRadiusToDoubleConverter).TypeHandle);
	}

	private void StaticInitializer_115_ICornerRadiusRoundingStrategyConvertion()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ICornerRadiusRoundingStrategyConvertion).TypeHandle);
	}

	private void StaticInitializer_116_OneUIColorSpectrum()
	{
		RuntimeHelpers.RunClassConstructor(typeof(OneUIColorSpectrum).TypeHandle);
	}

	private void StaticInitializer_117_ColorSpectrum()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorSpectrum).TypeHandle);
	}

	private void StaticInitializer_118_Vector4()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Vector4).TypeHandle);
	}

	private void StaticInitializer_119_ColorPickerSliderCustom()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorPickerSliderCustom).TypeHandle);
	}

	private void StaticInitializer_120_ColorPickerSlider()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorPickerSlider).TypeHandle);
	}

	private void StaticInitializer_122_ColorPickerHsvChannel()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorPickerHsvChannel).TypeHandle);
	}

	private void StaticInitializer_124_Effects()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Effects).TypeHandle);
	}

	private void StaticInitializer_125_AttachedShadowBase()
	{
		RuntimeHelpers.RunClassConstructor(typeof(AttachedShadowBase).TypeHandle);
	}

	private void StaticInitializer_126_AttachedCardShadow()
	{
		RuntimeHelpers.RunClassConstructor(typeof(AttachedCardShadow).TypeHandle);
	}

	private void StaticInitializer_127_InnerContentClipMode()
	{
		RuntimeHelpers.RunClassConstructor(typeof(InnerContentClipMode).TypeHandle);
	}

	private void StaticInitializer_128_CornerRadiusCornersConverter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(CornerRadiusCornersConverter).TypeHandle);
	}

	private void StaticInitializer_129_ColorPickerGridViewItemRadiusSelector()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorPickerGridViewItemRadiusSelector).TypeHandle);
	}

	private void StaticInitializer_130_ColorPickerSwatchedGridViewCustom()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorPickerSwatchedGridViewCustom).TypeHandle);
	}

	private void StaticInitializer_131_OneUIResources()
	{
		RuntimeHelpers.RunClassConstructor(typeof(OneUIResources).TypeHandle);
	}

	private void StaticInitializer_133_NumberBadge()
	{
		RuntimeHelpers.RunClassConstructor(typeof(NumberBadge).TypeHandle);
	}

	private void StaticInitializer_134_BadgeBase()
	{
		RuntimeHelpers.RunClassConstructor(typeof(BadgeBase).TypeHandle);
	}

	private void StaticInitializer_135_TextBadge()
	{
		RuntimeHelpers.RunClassConstructor(typeof(TextBadge).TypeHandle);
	}

	private void StaticInitializer_136_AlertBadge()
	{
		RuntimeHelpers.RunClassConstructor(typeof(AlertBadge).TypeHandle);
	}

	private void StaticInitializer_137_DotBadge()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DotBadge).TypeHandle);
	}

	private void StaticInitializer_138_AddButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(AddButton).TypeHandle);
	}

	private void StaticInitializer_139_DeleteButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DeleteButton).TypeHandle);
	}

	private void StaticInitializer_140_ContainedButtonBodyColored()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ContainedButtonBodyColored).TypeHandle);
	}

	private void StaticInitializer_141_ContainedButtonBase()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ContainedButtonBase).TypeHandle);
	}

	private void StaticInitializer_142_Interaction()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Interaction).TypeHandle);
	}

	private void StaticInitializer_143_BehaviorCollection()
	{
		RuntimeHelpers.RunClassConstructor(typeof(BehaviorCollection).TypeHandle);
	}

	private void StaticInitializer_145_ProgressCircleIndeterminate()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ProgressCircleIndeterminate).TypeHandle);
	}

	private void StaticInitializer_146_ProgressCircle()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ProgressCircle).TypeHandle);
	}

	private void StaticInitializer_147_ProgressCircleSize()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ProgressCircleSize).TypeHandle);
	}

	private void StaticInitializer_148_TooltipForTrimmedButtonBehavior()
	{
		RuntimeHelpers.RunClassConstructor(typeof(TooltipForTrimmedButtonBehavior).TypeHandle);
	}

	private void StaticInitializer_149_Behavior()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Behavior<FrameworkElement>).TypeHandle);
	}

	private void StaticInitializer_150_Behavior()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Behavior).TypeHandle);
	}

	private void StaticInitializer_151_ContainedButtonBody()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ContainedButtonBody).TypeHandle);
	}

	private void StaticInitializer_152_ContainedButtonColored()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ContainedButtonColored).TypeHandle);
	}

	private void StaticInitializer_153_ContainedButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ContainedButton).TypeHandle);
	}

	private void StaticInitializer_154_ContainedButtonType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ContainedButtonType).TypeHandle);
	}

	private void StaticInitializer_155_ContainedButtonSize()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ContainedButtonSize).TypeHandle);
	}

	private void StaticInitializer_156_ContentButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ContentButton).TypeHandle);
	}

	private void StaticInitializer_157_ButtonShapeEnum()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ButtonShapeEnum).TypeHandle);
	}

	private void StaticInitializer_158_ContentToggleButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ContentToggleButton).TypeHandle);
	}

	private void StaticInitializer_159_EditButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(EditButton).TypeHandle);
	}

	private void StaticInitializer_160_EditButtonType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(EditButtonType).TypeHandle);
	}

	private void StaticInitializer_161_FlatUnderlineButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(FlatUnderlineButton).TypeHandle);
	}

	private void StaticInitializer_162_FloatingActionButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(FloatingActionButton).TypeHandle);
	}

	private void StaticInitializer_163_ElevationCorner()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ElevationCorner).TypeHandle);
	}

	private void StaticInitializer_164_BlurLayer()
	{
		RuntimeHelpers.RunClassConstructor(typeof(BlurLayer).TypeHandle);
	}

	private void StaticInitializer_165_BlurLevel()
	{
		RuntimeHelpers.RunClassConstructor(typeof(BlurLevel).TypeHandle);
	}

	private void StaticInitializer_166_VibrancyLevel()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.Tokens.VibrancyLevel).TypeHandle);
	}

	private void StaticInitializer_167_GoToTopButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(GoToTopButton).TypeHandle);
	}

	private void StaticInitializer_168_HyperlinkButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.Controls.HyperlinkButton).TypeHandle);
	}

	private void StaticInitializer_170_ProgressButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ProgressButton).TypeHandle);
	}

	private void StaticInitializer_171_ProgressButtonType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ProgressButtonType).TypeHandle);
	}

	private void StaticInitializer_172_StringToVisibilityConverter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(StringToVisibilityConverter).TypeHandle);
	}

	private void StaticInitializer_173_ImageToVisibilityConverter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ImageToVisibilityConverter).TypeHandle);
	}

	private void StaticInitializer_174_ToolTip()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.Controls.ToolTip).TypeHandle);
	}

	private void StaticInitializer_176_TooltipForTrimmedTextBlockBehavior()
	{
		RuntimeHelpers.RunClassConstructor(typeof(TooltipForTrimmedTextBlockBehavior).TypeHandle);
	}

	private void StaticInitializer_177_KeyTime()
	{
		RuntimeHelpers.RunClassConstructor(typeof(KeyTime).TypeHandle);
	}

	private void StaticInitializer_178_NullToVisibilityConverter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(NullToVisibilityConverter).TypeHandle);
	}

	private void StaticInitializer_179_CheckBox()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.Controls.CheckBox).TypeHandle);
	}

	private void StaticInitializer_182_CheckBoxType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(CheckBoxType).TypeHandle);
	}

	private void StaticInitializer_183_ChipsItemIconVisibilityConverter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ChipsItemIconVisibilityConverter).TypeHandle);
	}

	private void StaticInitializer_184_ChipsItemImageIconVisibilityConverter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ChipsItemImageIconVisibilityConverter).TypeHandle);
	}

	private void StaticInitializer_185_ChipsItemIconStyleConverter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ChipsItemIconStyleConverter).TypeHandle);
	}

	private void StaticInitializer_186_ChipsItemStyleSelector()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ChipsItemStyleSelector).TypeHandle);
	}

	private void StaticInitializer_187_CornerRadiusBorderCompensationBehavior()
	{
		RuntimeHelpers.RunClassConstructor(typeof(CornerRadiusBorderCompensationBehavior).TypeHandle);
	}

	private void StaticInitializer_188_ImageIcon()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ImageIcon).TypeHandle);
	}

	private void StaticInitializer_189_Byte()
	{
		RuntimeHelpers.RunClassConstructor(typeof(byte).TypeHandle);
	}

	private void StaticInitializer_190_CommandBarButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(CommandBarButton).TypeHandle);
	}

	private void StaticInitializer_192_IntToEnumConverter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(IntToEnumConverter).TypeHandle);
	}

	private void StaticInitializer_193_CommandBar()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.Controls.CommandBar).TypeHandle);
	}

	private void StaticInitializer_195_ObservableCollection()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ObservableCollection<MenuFlyoutItemBase>).TypeHandle);
	}

	private void StaticInitializer_196_Collection()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Collection<MenuFlyoutItemBase>).TypeHandle);
	}

	private void StaticInitializer_198_ICommand()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ICommand).TypeHandle);
	}

	private void StaticInitializer_199_CommandBarBackButtonType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(CommandBarBackButtonType).TypeHandle);
	}

	private void StaticInitializer_200_Nullable()
	{
		RuntimeHelpers.RunClassConstructor(typeof(FlyoutPlacementMode?).TypeHandle);
	}

	private void StaticInitializer_202_FlexibleSpacingBehavior()
	{
		RuntimeHelpers.RunClassConstructor(typeof(FlexibleSpacingBehavior).TypeHandle);
	}

	private void StaticInitializer_203_IconButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(IconButton).TypeHandle);
	}

	private void StaticInitializer_204_ListFlyout()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ListFlyout).TypeHandle);
	}

	private void StaticInitializer_206_Tooltip()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Tooltip).TypeHandle);
	}

	private void StaticInitializer_207_CommandBarToggleButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(CommandBarToggleButton).TypeHandle);
	}

	private void StaticInitializer_209_MaxNumberCornerRadiusRoundingStrategy()
	{
		RuntimeHelpers.RunClassConstructor(typeof(MaxNumberCornerRadiusRoundingStrategy).TypeHandle);
	}

	private void StaticInitializer_210_ContextMenuItem()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ContextMenuItem).TypeHandle);
	}

	private void StaticInitializer_211_ListFlyoutItem()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ListFlyoutItem).TypeHandle);
	}

	private void StaticInitializer_213_ICommandBarItemOverflowable()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ICommandBarItemOverflowable).TypeHandle);
	}

	private void StaticInitializer_214_ContextMenuToggle()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ContextMenuToggle).TypeHandle);
	}

	private void StaticInitializer_215_ListFlyoutToggle()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ListFlyoutToggle).TypeHandle);
	}

	private void StaticInitializer_217_ContextMenuSeparator()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ContextMenuSeparator).TypeHandle);
	}

	private void StaticInitializer_218_ListFlyoutSeparator()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ListFlyoutSeparator).TypeHandle);
	}

	private void StaticInitializer_220_ColorWithTransparencyConverter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ColorWithTransparencyConverter).TypeHandle);
	}

	private void StaticInitializer_221_DatePickerSpinnerList()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DatePickerSpinnerList).TypeHandle);
	}

	private void StaticInitializer_222_DatePickerSpinnerListItem()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DatePickerSpinnerListItem).TypeHandle);
	}

	private void StaticInitializer_223_ScrollList()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ScrollList).TypeHandle);
	}

	private void StaticInitializer_224_ObservableCollection()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ObservableCollection<object>).TypeHandle);
	}

	private void StaticInitializer_225_Collection()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Collection<object>).TypeHandle);
	}

	private void StaticInitializer_226_TypeDate()
	{
		RuntimeHelpers.RunClassConstructor(typeof(TypeDate).TypeHandle);
	}

	private void StaticInitializer_227_StringToUpperConverter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(StringToUpperConverter).TypeHandle);
	}

	private void StaticInitializer_228_DatePickerTextScaleSizeConverter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DatePickerTextScaleSizeConverter).TypeHandle);
	}

	private void StaticInitializer_230_PeriodStyleSelector()
	{
		RuntimeHelpers.RunClassConstructor(typeof(PeriodStyleSelector).TypeHandle);
	}

	private void StaticInitializer_231_PeriodScrollList()
	{
		RuntimeHelpers.RunClassConstructor(typeof(PeriodScrollList).TypeHandle);
	}

	private void StaticInitializer_232_DpiChangedTo175StateTrigger()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DpiChangedTo175StateTrigger).TypeHandle);
	}

	private void StaticInitializer_233_DpiChangedStateTriggerBase()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DpiChangedStateTriggerBase).TypeHandle);
	}

	private void StaticInitializer_235_OSVersionType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(OSVersionType).TypeHandle);
	}

	private void StaticInitializer_236_DpiChangedTo125StateTrigger()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DpiChangedTo125StateTrigger).TypeHandle);
	}

	private void StaticInitializer_237_ContentDialog()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.Controls.ContentDialog).TypeHandle);
	}

	private void StaticInitializer_240_ScrollViewer()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer).TypeHandle);
	}

	private void StaticInitializer_241_GridLength()
	{
		RuntimeHelpers.RunClassConstructor(typeof(GridLength).TypeHandle);
	}

	private void StaticInitializer_242_ShowVerticalScrollableIndicatorBehavior()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ShowVerticalScrollableIndicatorBehavior).TypeHandle);
	}

	private void StaticInitializer_243_Behavior()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Behavior<DependencyObject>).TypeHandle);
	}

	private void StaticInitializer_244_Divider()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Divider).TypeHandle);
	}

	private void StaticInitializer_245_DividerType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DividerType).TypeHandle);
	}

	private void StaticInitializer_246_DropdownList()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DropdownList).TypeHandle);
	}

	private void StaticInitializer_247_IList()
	{
		RuntimeHelpers.RunClassConstructor(typeof(IList).TypeHandle);
	}

	private void StaticInitializer_248_DropdownListType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DropdownListType).TypeHandle);
	}

	private void StaticInitializer_249_DropdownListViewCustom()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DropdownListViewCustom).TypeHandle);
	}

	private void StaticInitializer_250_DropdownCustomControl()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DropdownCustomControl).TypeHandle);
	}

	private void StaticInitializer_251_OpacityToVisibilityConverter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(OpacityToVisibilityConverter).TypeHandle);
	}

	private void StaticInitializer_252_ExpandableList()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ExpandableList).TypeHandle);
	}

	private void StaticInitializer_253_TreeView()
	{
		RuntimeHelpers.RunClassConstructor(typeof(TreeView).TypeHandle);
	}

	private void StaticInitializer_254_TreeViewSelectionMode()
	{
		RuntimeHelpers.RunClassConstructor(typeof(TreeViewSelectionMode).TypeHandle);
	}

	private void StaticInitializer_258_IList()
	{
		RuntimeHelpers.RunClassConstructor(typeof(IList<TreeViewNode>).TypeHandle);
	}

	private void StaticInitializer_259_TreeViewNode()
	{
		RuntimeHelpers.RunClassConstructor(typeof(TreeViewNode).TypeHandle);
	}

	private void StaticInitializer_260_IList()
	{
		RuntimeHelpers.RunClassConstructor(typeof(IList<object>).TypeHandle);
	}

	private void StaticInitializer_261_ExpandableListItemHeader()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ExpandableListItemHeader).TypeHandle);
	}

	private void StaticInitializer_262_TreeViewItem()
	{
		RuntimeHelpers.RunClassConstructor(typeof(TreeViewItem).TypeHandle);
	}

	private void StaticInitializer_263_TreeViewItemTemplateSettings()
	{
		RuntimeHelpers.RunClassConstructor(typeof(TreeViewItemTemplateSettings).TypeHandle);
	}

	private void StaticInitializer_264_ExpandButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ExpandButton).TypeHandle);
	}

	private void StaticInitializer_265_TreeViewList()
	{
		RuntimeHelpers.RunClassConstructor(typeof(TreeViewList).TypeHandle);
	}

	private void StaticInitializer_266_FlipViewButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(FlipViewButton).TypeHandle);
	}

	private void StaticInitializer_267_FlipView()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.Controls.FlipView).TypeHandle);
	}

	private void StaticInitializer_269_FlipViewItem()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.Controls.FlipViewItem).TypeHandle);
	}

	private void StaticInitializer_271_IconToggleButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(IconToggleButton).TypeHandle);
	}

	private void StaticInitializer_272_LevelSlider()
	{
		RuntimeHelpers.RunClassConstructor(typeof(LevelSlider).TypeHandle);
	}

	private void StaticInitializer_273_SliderMarkerControl()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SliderMarkerControl).TypeHandle);
	}

	private void StaticInitializer_274_LevelBar()
	{
		RuntimeHelpers.RunClassConstructor(typeof(LevelBar).TypeHandle);
	}

	private void StaticInitializer_276_ItemsRepeater()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ItemsRepeater).TypeHandle);
	}

	private void StaticInitializer_277_Layout()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Layout).TypeHandle);
	}

	private void StaticInitializer_278_ItemCollectionTransitionProvider()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ItemCollectionTransitionProvider).TypeHandle);
	}

	private void StaticInitializer_279_ItemsSourceView()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ItemsSourceView).TypeHandle);
	}

	private void StaticInitializer_280_StackLayout()
	{
		RuntimeHelpers.RunClassConstructor(typeof(StackLayout).TypeHandle);
	}

	private void StaticInitializer_281_VirtualizingLayout()
	{
		RuntimeHelpers.RunClassConstructor(typeof(VirtualizingLayout).TypeHandle);
	}

	private void StaticInitializer_282_IndexBasedLayoutOrientation()
	{
		RuntimeHelpers.RunClassConstructor(typeof(IndexBasedLayoutOrientation).TypeHandle);
	}

	private void StaticInitializer_283_UniformGridLayout()
	{
		RuntimeHelpers.RunClassConstructor(typeof(UniformGridLayout).TypeHandle);
	}

	private void StaticInitializer_284_UniformGridLayoutItemsJustification()
	{
		RuntimeHelpers.RunClassConstructor(typeof(UniformGridLayoutItemsJustification).TypeHandle);
	}

	private void StaticInitializer_285_UniformGridLayoutItemsStretch()
	{
		RuntimeHelpers.RunClassConstructor(typeof(UniformGridLayoutItemsStretch).TypeHandle);
	}

	private void StaticInitializer_288_MultiPane()
	{
		RuntimeHelpers.RunClassConstructor(typeof(MultiPane).TypeHandle);
	}

	private void StaticInitializer_290_SplitBar()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SplitBar).TypeHandle);
	}

	private void StaticInitializer_291_GridResizeDirection()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SplitBar.GridResizeDirection).TypeHandle);
	}

	private void StaticInitializer_292_GridResizeBehavior()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SplitBar.GridResizeBehavior).TypeHandle);
	}

	private void StaticInitializer_293_GripperCursorType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SplitBar.GripperCursorType).TypeHandle);
	}

	private void StaticInitializer_294_SplitterCursorBehavior()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SplitBar.SplitterCursorBehavior).TypeHandle);
	}

	private void StaticInitializer_295_NavigationRail()
	{
		RuntimeHelpers.RunClassConstructor(typeof(NavigationRail).TypeHandle);
	}

	private void StaticInitializer_296_NavigationView()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Microsoft.UI.Xaml.Controls.NavigationView).TypeHandle);
	}

	private void StaticInitializer_298_NavigationViewDisplayMode()
	{
		RuntimeHelpers.RunClassConstructor(typeof(NavigationViewDisplayMode).TypeHandle);
	}

	private void StaticInitializer_299_NavigationViewBackButtonVisible()
	{
		RuntimeHelpers.RunClassConstructor(typeof(NavigationViewBackButtonVisible).TypeHandle);
	}

	private void StaticInitializer_300_NavigationViewOverflowLabelMode()
	{
		RuntimeHelpers.RunClassConstructor(typeof(NavigationViewOverflowLabelMode).TypeHandle);
	}

	private void StaticInitializer_301_NavigationViewPaneDisplayMode()
	{
		RuntimeHelpers.RunClassConstructor(typeof(NavigationViewPaneDisplayMode).TypeHandle);
	}

	private void StaticInitializer_302_NavigationViewSelectionFollowsFocus()
	{
		RuntimeHelpers.RunClassConstructor(typeof(NavigationViewSelectionFollowsFocus).TypeHandle);
	}

	private void StaticInitializer_303_NavigationViewShoulderNavigationEnabled()
	{
		RuntimeHelpers.RunClassConstructor(typeof(NavigationViewShoulderNavigationEnabled).TypeHandle);
	}

	private void StaticInitializer_304_NavigationViewTemplateSettings()
	{
		RuntimeHelpers.RunClassConstructor(typeof(NavigationViewTemplateSettings).TypeHandle);
	}

	private void StaticInitializer_305_NavigationRailItem()
	{
		RuntimeHelpers.RunClassConstructor(typeof(NavigationRailItem).TypeHandle);
	}

	private void StaticInitializer_306_NavigationViewItem()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Microsoft.UI.Xaml.Controls.NavigationViewItem).TypeHandle);
	}

	private void StaticInitializer_307_NavigationViewItemBase()
	{
		RuntimeHelpers.RunClassConstructor(typeof(NavigationViewItemBase).TypeHandle);
	}

	private void StaticInitializer_308_InfoBadge()
	{
		RuntimeHelpers.RunClassConstructor(typeof(InfoBadge).TypeHandle);
	}

	private void StaticInitializer_309_NavigationRailItemHeader()
	{
		RuntimeHelpers.RunClassConstructor(typeof(NavigationRailItemHeader).TypeHandle);
	}

	private void StaticInitializer_310_NavigationViewItemHeader()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Microsoft.UI.Xaml.Controls.NavigationViewItemHeader).TypeHandle);
	}

	private void StaticInitializer_311_NavigationRailItemSeparator()
	{
		RuntimeHelpers.RunClassConstructor(typeof(NavigationRailItemSeparator).TypeHandle);
	}

	private void StaticInitializer_312_NavigationViewItemSeparator()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Microsoft.UI.Xaml.Controls.NavigationViewItemSeparator).TypeHandle);
	}

	private void StaticInitializer_313_NavigationRailItemPresenter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(NavigationRailItemPresenter).TypeHandle);
	}

	private void StaticInitializer_314_NavigationViewItemPresenter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Microsoft.UI.Xaml.Controls.Primitives.NavigationViewItemPresenter).TypeHandle);
	}

	private void StaticInitializer_315_NavigationViewItemPresenterTemplateSettings()
	{
		RuntimeHelpers.RunClassConstructor(typeof(NavigationViewItemPresenterTemplateSettings).TypeHandle);
	}

	private void StaticInitializer_316_AnimatedIcon()
	{
		RuntimeHelpers.RunClassConstructor(typeof(AnimatedIcon).TypeHandle);
	}

	private void StaticInitializer_317_IAnimatedVisualSource2()
	{
		RuntimeHelpers.RunClassConstructor(typeof(IAnimatedVisualSource2).TypeHandle);
	}

	private void StaticInitializer_319_AnimatedChevronUpDownSmallVisualSource()
	{
		RuntimeHelpers.RunClassConstructor(typeof(AnimatedChevronUpDownSmallVisualSource).TypeHandle);
	}

	private void StaticInitializer_320_IReadOnlyDictionary()
	{
		RuntimeHelpers.RunClassConstructor(typeof(IReadOnlyDictionary<string, double>).TypeHandle);
	}

	private void StaticInitializer_322_ItemsRepeaterScrollHost()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ItemsRepeaterScrollHost).TypeHandle);
	}

	private void StaticInitializer_323_NavigationView()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.Controls.NavigationView).TypeHandle);
	}

	private void StaticInitializer_324_NavigationViewItem()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.Controls.NavigationViewItem).TypeHandle);
	}

	private void StaticInitializer_325_NavigationViewItemHeader()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.Controls.NavigationViewItemHeader).TypeHandle);
	}

	private void StaticInitializer_326_NavigationViewItemSeparator()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.Controls.NavigationViewItemSeparator).TypeHandle);
	}

	private void StaticInitializer_327_NavigationViewItemPresenter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.Controls.NavigationViewItemPresenter).TypeHandle);
	}

	private void StaticInitializer_328_PageIndicator()
	{
		RuntimeHelpers.RunClassConstructor(typeof(PageIndicator).TypeHandle);
	}

	private void StaticInitializer_329_PopOverPresenter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(PopOverPresenter).TypeHandle);
	}

	private void StaticInitializer_330_ProgressBar()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.Controls.ProgressBar).TypeHandle);
	}

	private void StaticInitializer_331_ProgressBar()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Microsoft.UI.Xaml.Controls.ProgressBar).TypeHandle);
	}

	private void StaticInitializer_333_ProgressBarTemplateSettings()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ProgressBarTemplateSettings).TypeHandle);
	}

	private void StaticInitializer_334_ProgressCircleDeterminate()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ProgressCircleDeterminate).TypeHandle);
	}

	private void StaticInitializer_335_ProgressCircleDeterminateType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ProgressCircleDeterminateType).TypeHandle);
	}

	private void StaticInitializer_336_RadioButtons()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.Controls.RadioButtons).TypeHandle);
	}

	private void StaticInitializer_337_RadioButtons()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Microsoft.UI.Xaml.Controls.RadioButtons).TypeHandle);
	}

	private void StaticInitializer_338_RadioButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.Controls.RadioButton).TypeHandle);
	}

	private void StaticInitializer_340_GridUnitType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(GridUnitType).TypeHandle);
	}

	private void StaticInitializer_341_ScrollModeToBoolConverter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ScrollModeToBoolConverter).TypeHandle);
	}

	private void StaticInitializer_342_DoubleToThicknessTopAndBottomConverter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DoubleToThicknessTopAndBottomConverter).TypeHandle);
	}

	private void StaticInitializer_345_ThumbDisabledScrollBarDimensionsBehavior()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ThumbDisabledScrollBarDimensionsBehavior).TypeHandle);
	}

	private void StaticInitializer_346_Behavior()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Behavior<Thumb>).TypeHandle);
	}

	private void StaticInitializer_347_DataTriggerBehavior()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DataTriggerBehavior).TypeHandle);
	}

	private void StaticInitializer_348_Trigger()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Trigger).TypeHandle);
	}

	private void StaticInitializer_349_ActionCollection()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ActionCollection).TypeHandle);
	}

	private void StaticInitializer_350_ComparisonConditionType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ComparisonConditionType).TypeHandle);
	}

	private void StaticInitializer_351_GoToStateAction()
	{
		RuntimeHelpers.RunClassConstructor(typeof(GoToStateAction).TypeHandle);
	}

	private void StaticInitializer_352_ThumbCompositeTransformScaleStateTrigger()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ThumbCompositeTransformScaleStateTrigger).TypeHandle);
	}

	private void StaticInitializer_353_SearchPopupListFooterButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SearchPopupListFooterButton).TypeHandle);
	}

	private void StaticInitializer_354_SearchPopupList()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SearchPopupList).TypeHandle);
	}

	private void StaticInitializer_355_ObservableCollection()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ObservableCollection<SearchPopupListItem>).TypeHandle);
	}

	private void StaticInitializer_356_Collection()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Collection<SearchPopupListItem>).TypeHandle);
	}

	private void StaticInitializer_357_SearchPopupListItem()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SearchPopupListItem).TypeHandle);
	}

	private void StaticInitializer_358_FilterTextBlock()
	{
		RuntimeHelpers.RunClassConstructor(typeof(FilterTextBlock).TypeHandle);
	}

	private void StaticInitializer_359_SearchPopup()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SearchPopup).TypeHandle);
	}

	private void StaticInitializer_360_SearchPopupRemoveButton()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SearchPopupRemoveButton).TypeHandle);
	}

	private void StaticInitializer_361_SearchPopupTextBox()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SearchPopupTextBox).TypeHandle);
	}

	private void StaticInitializer_362_BackdropBlurExtension()
	{
		RuntimeHelpers.RunClassConstructor(typeof(BackdropBlurExtension).TypeHandle);
	}

	private void StaticInitializer_363_Slider()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.Controls.Slider).TypeHandle);
	}

	private void StaticInitializer_364_SliderBase()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SliderBase).TypeHandle);
	}

	private void StaticInitializer_365_ShockValueType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ShockValueType).TypeHandle);
	}

	private void StaticInitializer_366_SliderType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SliderType).TypeHandle);
	}

	private void StaticInitializer_367_BufferSlider()
	{
		RuntimeHelpers.RunClassConstructor(typeof(BufferSlider).TypeHandle);
	}

	private void StaticInitializer_368_CenterSlider()
	{
		RuntimeHelpers.RunClassConstructor(typeof(CenterSlider).TypeHandle);
	}

	private void StaticInitializer_369_SubAppBar()
	{
		RuntimeHelpers.RunClassConstructor(typeof(SubAppBar).TypeHandle);
	}

	private void StaticInitializer_370_TabView()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.Controls.TabView).TypeHandle);
	}

	private void StaticInitializer_372_TabViewType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(TabViewType).TypeHandle);
	}

	private void StaticInitializer_373_TabItem()
	{
		RuntimeHelpers.RunClassConstructor(typeof(TabItem).TypeHandle);
	}

	private void StaticInitializer_376_ThicknessSideConverter()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ThicknessSideConverter).TypeHandle);
	}

	private void StaticInitializer_377_TextField()
	{
		RuntimeHelpers.RunClassConstructor(typeof(TextField).TypeHandle);
	}

	private void StaticInitializer_378_TextFieldType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(TextFieldType).TypeHandle);
	}

	private void StaticInitializer_379_ThumbnailRadious()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ThumbnailRadious).TypeHandle);
	}

	private void StaticInitializer_380_ThumbnailRadiousVisualizationMode()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ThumbnailRadiousVisualizationMode).TypeHandle);
	}

	private void StaticInitializer_381_ThumbnailRadiousGridView()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ThumbnailRadiousGridView).TypeHandle);
	}

	private void StaticInitializer_382_Titlebar()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Titlebar).TypeHandle);
	}

	private void StaticInitializer_383_ToggleSwitch()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Samsung.OneUI.WinUI.Controls.ToggleSwitch).TypeHandle);
	}

	private void StaticInitializer_384_ToggleSwitchType()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ToggleSwitchType).TypeHandle);
	}

	private void StaticInitializer_385_ToggleSwitchGroup()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ToggleSwitchGroup).TypeHandle);
	}

	private void StaticInitializer_387_DpiChangedTo150StateTrigger()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DpiChangedTo150StateTrigger).TypeHandle);
	}

	private void StaticInitializer_388_DpiChangedTo100StateTrigger()
	{
		RuntimeHelpers.RunClassConstructor(typeof(DpiChangedTo100StateTrigger).TypeHandle);
	}

	private void StaticInitializer_389_ToggleSwitchLoadedBehavior()
	{
		RuntimeHelpers.RunClassConstructor(typeof(ToggleSwitchLoadedBehavior).TypeHandle);
	}

	private void StaticInitializer_390_Behavior()
	{
		RuntimeHelpers.RunClassConstructor(typeof(Behavior<Microsoft.UI.Xaml.Controls.ToggleSwitch>).TypeHandle);
	}

	private void VectorAdd_16_List(object instance, object item)
	{
		ICollection<CardTypeItem> obj = (ICollection<CardTypeItem>)instance;
		CardTypeItem item2 = (CardTypeItem)item;
		obj.Add(item2);
	}

	private void VectorAdd_25_ObservableCollection(object instance, object item)
	{
		ICollection<ChipsItem> obj = (ICollection<ChipsItem>)instance;
		ChipsItem item2 = (ChipsItem)item;
		obj.Add(item2);
	}

	private void VectorAdd_26_Collection(object instance, object item)
	{
		ICollection<ChipsItem> obj = (ICollection<ChipsItem>)instance;
		ChipsItem item2 = (ChipsItem)item;
		obj.Add(item2);
	}

	private void VectorAdd_42_List(object instance, object item)
	{
		ICollection<ColorInfo> obj = (ICollection<ColorInfo>)instance;
		ColorInfo item2 = (ColorInfo)item;
		obj.Add(item2);
	}

	private void VectorAdd_52_List(object instance, object item)
	{
		ICollection<string> obj = (ICollection<string>)instance;
		string item2 = (string)item;
		obj.Add(item2);
	}

	private void VectorAdd_110_IList(object instance, object item)
	{
		ICollection<SolidColorBrush> obj = (ICollection<SolidColorBrush>)instance;
		SolidColorBrush item2 = (SolidColorBrush)item;
		obj.Add(item2);
	}

	private void MapAdd_131_OneUIResources(object instance, object key, object item)
	{
		((IDictionary<object, object>)instance).Add(key, item);
	}

	private void VectorAdd_143_BehaviorCollection(object instance, object item)
	{
		ICollection<DependencyObject> obj = (ICollection<DependencyObject>)instance;
		DependencyObject item2 = (DependencyObject)item;
		obj.Add(item2);
	}

	private void VectorAdd_195_ObservableCollection(object instance, object item)
	{
		ICollection<MenuFlyoutItemBase> obj = (ICollection<MenuFlyoutItemBase>)instance;
		MenuFlyoutItemBase item2 = (MenuFlyoutItemBase)item;
		obj.Add(item2);
	}

	private void VectorAdd_196_Collection(object instance, object item)
	{
		ICollection<MenuFlyoutItemBase> obj = (ICollection<MenuFlyoutItemBase>)instance;
		MenuFlyoutItemBase item2 = (MenuFlyoutItemBase)item;
		obj.Add(item2);
	}

	private void VectorAdd_224_ObservableCollection(object instance, object item)
	{
		((ICollection<object>)instance).Add(item);
	}

	private void VectorAdd_225_Collection(object instance, object item)
	{
		((ICollection<object>)instance).Add(item);
	}

	private void VectorAdd_258_IList(object instance, object item)
	{
		ICollection<TreeViewNode> obj = (ICollection<TreeViewNode>)instance;
		TreeViewNode item2 = (TreeViewNode)item;
		obj.Add(item2);
	}

	private void VectorAdd_260_IList(object instance, object item)
	{
		((ICollection<object>)instance).Add(item);
	}

	private void VectorAdd_349_ActionCollection(object instance, object item)
	{
		ICollection<DependencyObject> obj = (ICollection<DependencyObject>)instance;
		DependencyObject item2 = (DependencyObject)item;
		obj.Add(item2);
	}

	private void VectorAdd_355_ObservableCollection(object instance, object item)
	{
		ICollection<SearchPopupListItem> obj = (ICollection<SearchPopupListItem>)instance;
		SearchPopupListItem item2 = (SearchPopupListItem)item;
		obj.Add(item2);
	}

	private void VectorAdd_356_Collection(object instance, object item)
	{
		ICollection<SearchPopupListItem> obj = (ICollection<SearchPopupListItem>)instance;
		SearchPopupListItem item2 = (SearchPopupListItem)item;
		obj.Add(item2);
	}

	private IXamlType CreateXamlType(int typeIndex)
	{
		XamlSystemBaseType result = null;
		string fullName = _typeNameTable[typeIndex];
		Type type = _typeTable[typeIndex];
		switch (typeIndex)
		{
		case 0:
		{
			XamlUserType xamlUserType301 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.UserControl"));
			xamlUserType301.Activator = Activate_0_BGBlur;
			xamlUserType301.StaticInitializer = StaticInitializer_0_BGBlur;
			xamlUserType301.SetContentPropertyName("Samsung.OneUI.WinUI.Controls.BGBlur.LayerContent");
			xamlUserType301.AddMemberName("LayerContent");
			xamlUserType301.AddMemberName("Vibrancy");
			xamlUserType301.AddMemberName("FallbackBackground");
			xamlUserType301.AddMemberName("IsDarkGrayish");
			xamlUserType301.SetIsLocalType();
			result = xamlUserType301;
			break;
		}
		case 1:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 2:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 3:
		{
			XamlUserType xamlUserType300 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType300.StaticInitializer = StaticInitializer_3_VibrancyLevel;
			xamlUserType300.AddEnumValue("Thin", Samsung.OneUI.WinUI.Controls.VibrancyLevel.Thin);
			xamlUserType300.AddEnumValue("Regular", Samsung.OneUI.WinUI.Controls.VibrancyLevel.Regular);
			xamlUserType300.AddEnumValue("Thick", Samsung.OneUI.WinUI.Controls.VibrancyLevel.Thick);
			xamlUserType300.SetIsLocalType();
			result = xamlUserType300;
			break;
		}
		case 4:
			result = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.ValueType"))
			{
				StaticInitializer = StaticInitializer_4_Enum
			};
			break;
		case 5:
			result = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"))
			{
				StaticInitializer = StaticInitializer_5_ValueType
			};
			break;
		case 6:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 7:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 8:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 9:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 10:
		{
			XamlUserType xamlUserType299 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType299.Activator = Activate_10_CardType;
			xamlUserType299.StaticInitializer = StaticInitializer_10_CardType;
			xamlUserType299.AddMemberName("Title");
			xamlUserType299.AddMemberName("ButtonText");
			xamlUserType299.AddMemberName("Description");
			xamlUserType299.AddMemberName("Image");
			xamlUserType299.AddMemberName("SvgImage");
			xamlUserType299.SetIsLocalType();
			result = xamlUserType299;
			break;
		}
		case 11:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 12:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 13:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 14:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 15:
		{
			XamlUserType xamlUserType298 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.UserControl"));
			xamlUserType298.Activator = Activate_15_CardTypeListView;
			xamlUserType298.StaticInitializer = StaticInitializer_15_CardTypeListView;
			xamlUserType298.AddMemberName("ItemsSource");
			xamlUserType298.SetIsLocalType();
			result = xamlUserType298;
			break;
		}
		case 16:
		{
			XamlUserType xamlUserType297 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType297.StaticInitializer = StaticInitializer_16_List;
			xamlUserType297.CollectionAdd = VectorAdd_16_List;
			xamlUserType297.SetIsReturnTypeStub();
			result = xamlUserType297;
			break;
		}
		case 17:
		{
			XamlUserType xamlUserType296 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType296.Activator = Activate_17_CardTypeItem;
			xamlUserType296.StaticInitializer = StaticInitializer_17_CardTypeItem;
			xamlUserType296.AddMemberName("Image");
			xamlUserType296.AddMemberName("SvgStyle");
			xamlUserType296.AddMemberName("Title");
			xamlUserType296.AddMemberName("Description");
			xamlUserType296.AddMemberName("ButtonText");
			xamlUserType296.AddMemberName("Click_Event");
			xamlUserType296.SetIsLocalType();
			result = xamlUserType296;
			break;
		}
		case 18:
		{
			XamlUserType xamlUserType295 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.MulticastDelegate"));
			xamlUserType295.StaticInitializer = StaticInitializer_18_EventHandler;
			xamlUserType295.SetIsReturnTypeStub();
			result = xamlUserType295;
			break;
		}
		case 19:
			result = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Delegate"))
			{
				StaticInitializer = StaticInitializer_19_MulticastDelegate
			};
			break;
		case 20:
			result = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"))
			{
				StaticInitializer = StaticInitializer_20_Delegate
			};
			break;
		case 21:
		{
			XamlUserType xamlUserType294 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Panel"));
			xamlUserType294.Activator = Activate_21_WrapPanel;
			xamlUserType294.StaticInitializer = StaticInitializer_21_WrapPanel;
			xamlUserType294.AddMemberName("HorizontalSpacing");
			xamlUserType294.AddMemberName("VerticalSpacing");
			xamlUserType294.SetIsLocalType();
			result = xamlUserType294;
			break;
		}
		case 22:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 23:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 24:
		{
			XamlUserType xamlUserType293 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.UserControl"));
			xamlUserType293.Activator = Activate_24_Chips;
			xamlUserType293.StaticInitializer = StaticInitializer_24_Chips;
			xamlUserType293.AddMemberName("Items");
			xamlUserType293.AddMemberName("SelectionState");
			xamlUserType293.AddMemberName("AllLabels");
			xamlUserType293.SetIsLocalType();
			result = xamlUserType293;
			break;
		}
		case 25:
		{
			XamlUserType xamlUserType292 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Collections.ObjectModel.Collection`1<Samsung.OneUI.WinUI.Controls.ChipsItem>"));
			xamlUserType292.StaticInitializer = StaticInitializer_25_ObservableCollection;
			xamlUserType292.CollectionAdd = VectorAdd_25_ObservableCollection;
			xamlUserType292.SetIsReturnTypeStub();
			result = xamlUserType292;
			break;
		}
		case 26:
			result = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"))
			{
				Activator = Activate_26_Collection,
				StaticInitializer = StaticInitializer_26_Collection,
				CollectionAdd = VectorAdd_26_Collection
			};
			break;
		case 27:
		{
			XamlUserType xamlUserType291 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.GridViewItem"));
			xamlUserType291.Activator = Activate_27_ChipsItem;
			xamlUserType291.StaticInitializer = StaticInitializer_27_ChipsItem;
			xamlUserType291.AddMemberName("Title");
			xamlUserType291.AddMemberName("Label");
			xamlUserType291.AddMemberName("Id");
			xamlUserType291.AddMemberName("Type");
			xamlUserType291.AddMemberName("Icon");
			xamlUserType291.AddMemberName("IconSvgStyle");
			xamlUserType291.SetIsLocalType();
			result = xamlUserType291;
			break;
		}
		case 28:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 29:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 30:
		{
			XamlUserType xamlUserType290 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType290.StaticInitializer = StaticInitializer_30_ChipsItemTemplate;
			xamlUserType290.AddEnumValue("Default", ChipsItemTemplate.Default);
			xamlUserType290.AddEnumValue("Cancel", ChipsItemTemplate.Cancel);
			xamlUserType290.AddEnumValue("Minus", ChipsItemTemplate.Minus);
			xamlUserType290.AddEnumValue("Tag", ChipsItemTemplate.Tag);
			xamlUserType290.AddEnumValue("Custom", ChipsItemTemplate.Custom);
			xamlUserType290.SetIsLocalType();
			result = xamlUserType290;
			break;
		}
		case 31:
		{
			XamlUserType xamlUserType289 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType289.StaticInitializer = StaticInitializer_31_ChipsItemType;
			xamlUserType289.AddEnumValue("Default", ChipsItemType.Default);
			xamlUserType289.AddEnumValue("Border", ChipsItemType.Border);
			xamlUserType289.SetIsLocalType();
			result = xamlUserType289;
			break;
		}
		case 32:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 33:
		{
			XamlUserType xamlUserType288 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType288.StaticInitializer = StaticInitializer_33_ChipsItemGroupTemplate;
			xamlUserType288.AddEnumValue("None", ChipsItemGroupTemplate.None);
			xamlUserType288.AddEnumValue("Default", ChipsItemGroupTemplate.Default);
			xamlUserType288.AddEnumValue("Cancel", ChipsItemGroupTemplate.Cancel);
			xamlUserType288.AddEnumValue("Minus", ChipsItemGroupTemplate.Minus);
			xamlUserType288.AddEnumValue("Tag", ChipsItemGroupTemplate.Tag);
			xamlUserType288.AddEnumValue("Custom", ChipsItemGroupTemplate.Custom);
			xamlUserType288.SetIsLocalType();
			result = xamlUserType288;
			break;
		}
		case 34:
		{
			XamlUserType xamlUserType287 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ContentControl"));
			xamlUserType287.Activator = Activate_34_Toast;
			xamlUserType287.StaticInitializer = StaticInitializer_34_Toast;
			xamlUserType287.AddMemberName("Message");
			xamlUserType287.AddMemberName("ToastDuration");
			xamlUserType287.AddMemberName("Target");
			xamlUserType287.SetIsLocalType();
			result = xamlUserType287;
			break;
		}
		case 35:
		{
			XamlUserType xamlUserType286 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType286.StaticInitializer = StaticInitializer_35_ToastDuration;
			xamlUserType286.AddEnumValue("Short", ToastDuration.Short);
			xamlUserType286.AddEnumValue("Long", ToastDuration.Long);
			xamlUserType286.SetIsLocalType();
			result = xamlUserType286;
			break;
		}
		case 36:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 37:
		{
			XamlUserType xamlUserType285 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType285.Activator = Activate_37_ColorPickerControl;
			xamlUserType285.StaticInitializer = StaticInitializer_37_ColorPickerControl;
			xamlUserType285.AddMemberName("AlphaSliderValue");
			xamlUserType285.AddMemberName("IsColorPickerAlphaSliderEditable");
			xamlUserType285.AddMemberName("IsAlphaSliderVisible");
			xamlUserType285.AddMemberName("IsSaturationSliderVisible");
			xamlUserType285.AddMemberName("SelectedColorDescription");
			xamlUserType285.AddMemberName("SelectedColor");
			xamlUserType285.AddMemberName("IsColorPickerSwatchedSelected");
			xamlUserType285.AddMemberName("SwatchedVisibility");
			xamlUserType285.AddMemberName("SpectrumVisibility");
			xamlUserType285.AddMemberName("Theme");
			xamlUserType285.AddMemberName("RecentColors");
			xamlUserType285.SetIsLocalType();
			result = xamlUserType285;
			break;
		}
		case 38:
		{
			XamlUserType xamlUserType284 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.ValueType"));
			xamlUserType284.SetBoxedType(GetXamlTypeByName("Double"));
			xamlUserType284.BoxInstance = xamlUserType284.BoxType<double>;
			xamlUserType284.StaticInitializer = StaticInitializer_38_Nullable;
			xamlUserType284.SetIsReturnTypeStub();
			result = xamlUserType284;
			break;
		}
		case 39:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 40:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 41:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 42:
		{
			XamlUserType xamlUserType283 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType283.StaticInitializer = StaticInitializer_42_List;
			xamlUserType283.CollectionAdd = VectorAdd_42_List;
			xamlUserType283.SetIsReturnTypeStub();
			result = xamlUserType283;
			break;
		}
		case 43:
		{
			XamlUserType xamlUserType282 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType282.StaticInitializer = StaticInitializer_43_ColorInfo;
			xamlUserType282.AddMemberName("Name");
			xamlUserType282.AddMemberName("Description");
			xamlUserType282.AddMemberName("HexValue");
			xamlUserType282.AddMemberName("ColorBrush");
			xamlUserType282.SetIsLocalType();
			result = xamlUserType282;
			break;
		}
		case 44:
		{
			XamlUserType xamlUserType281 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FlatButtonBase"));
			xamlUserType281.Activator = Activate_44_FlatButton;
			xamlUserType281.StaticInitializer = StaticInitializer_44_FlatButton;
			xamlUserType281.AddMemberName("Size");
			xamlUserType281.AddMemberName("Type");
			xamlUserType281.SetIsLocalType();
			result = xamlUserType281;
			break;
		}
		case 45:
		{
			XamlUserType xamlUserType280 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Button"));
			xamlUserType280.Activator = Activate_45_FlatButtonBase;
			xamlUserType280.StaticInitializer = StaticInitializer_45_FlatButtonBase;
			xamlUserType280.AddMemberName("TextTrimming");
			xamlUserType280.AddMemberName("MaxTextLines");
			xamlUserType280.AddMemberName("IsProgressEnabled");
			xamlUserType280.SetIsLocalType();
			result = xamlUserType280;
			break;
		}
		case 46:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 47:
		{
			XamlUserType xamlUserType279 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType279.StaticInitializer = StaticInitializer_47_FlatButtonSize;
			xamlUserType279.AddEnumValue("Medium", FlatButtonSize.Medium);
			xamlUserType279.AddEnumValue("Small", FlatButtonSize.Small);
			xamlUserType279.SetIsLocalType();
			result = xamlUserType279;
			break;
		}
		case 48:
		{
			XamlUserType xamlUserType278 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType278.StaticInitializer = StaticInitializer_48_FlatButtonType;
			xamlUserType278.AddEnumValue("Primary", FlatButtonType.Primary);
			xamlUserType278.AddEnumValue("Secondary", FlatButtonType.Secondary);
			xamlUserType278.AddEnumValue("Red", FlatButtonType.Red);
			xamlUserType278.SetIsLocalType();
			result = xamlUserType278;
			break;
		}
		case 49:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 50:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 51:
		{
			XamlUserType xamlUserType277 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.UserControl"));
			xamlUserType277.Activator = Activate_51_ColorPickerDialog;
			xamlUserType277.StaticInitializer = StaticInitializer_51_ColorPickerDialog;
			xamlUserType277.AddMemberName("SelectedColorDescription");
			xamlUserType277.AddMemberName("SelectedColor");
			xamlUserType277.AddMemberName("IsColorPickerSwatchedSelected");
			xamlUserType277.AddMemberName("PickedColors");
			xamlUserType277.AddMemberName("AlphaSliderValue");
			xamlUserType277.AddMemberName("IsColorPickerAlphaSliderEditable");
			xamlUserType277.AddMemberName("IsOpen");
			xamlUserType277.AddMemberName("IsAlphaSliderVisible");
			xamlUserType277.AddMemberName("IsSaturationSliderVisible");
			xamlUserType277.AddMemberName("isDialogViewBoxEnabled");
			xamlUserType277.AddMemberName("DialogViewBoxWidth");
			xamlUserType277.AddMemberName("DialogViewBoxHeight");
			xamlUserType277.AddMemberName("RecentColors");
			xamlUserType277.SetIsLocalType();
			result = xamlUserType277;
			break;
		}
		case 52:
		{
			XamlUserType xamlUserType276 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType276.StaticInitializer = StaticInitializer_52_List;
			xamlUserType276.CollectionAdd = VectorAdd_52_List;
			xamlUserType276.SetIsReturnTypeStub();
			result = xamlUserType276;
			break;
		}
		case 53:
		{
			XamlUserType xamlUserType275 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.CalendarView"));
			xamlUserType275.Activator = Activate_53_DatePicker;
			xamlUserType275.StaticInitializer = StaticInitializer_53_DatePicker;
			xamlUserType275.AddMemberName("ActualDateTimeScope");
			xamlUserType275.AddMemberName("SelectedDate");
			xamlUserType275.AddMemberName("SundayDayIndicator");
			xamlUserType275.SetIsLocalType();
			result = xamlUserType275;
			break;
		}
		case 54:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 55:
			result = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.ValueType"))
			{
				StaticInitializer = StaticInitializer_55_DateTime
			};
			break;
		case 56:
		{
			XamlUserType xamlUserType274 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Page"));
			xamlUserType274.Activator = Activate_56_DatePickerDialogContent;
			xamlUserType274.StaticInitializer = StaticInitializer_56_DatePickerDialogContent;
			xamlUserType274.AddMemberName("SelectedDate");
			xamlUserType274.SetIsLocalType();
			result = xamlUserType274;
			break;
		}
		case 57:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 58:
		{
			XamlUserType xamlUserType273 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.TimePickerList"));
			xamlUserType273.Activator = Activate_58_DateTimePickerList;
			xamlUserType273.StaticInitializer = StaticInitializer_58_DateTimePickerList;
			xamlUserType273.AddMemberName("Date");
			xamlUserType273.AddMemberName("StartRangeDate");
			xamlUserType273.AddMemberName("RangeDays");
			xamlUserType273.SetIsLocalType();
			result = xamlUserType273;
			break;
		}
		case 59:
		{
			XamlUserType xamlUserType272 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType272.Activator = Activate_59_TimePickerList;
			xamlUserType272.StaticInitializer = StaticInitializer_59_TimePickerList;
			xamlUserType272.AddMemberName("Type");
			xamlUserType272.AddMemberName("Period");
			xamlUserType272.AddMemberName("Hour");
			xamlUserType272.AddMemberName("Minute");
			xamlUserType272.AddMemberName("TimeResult");
			xamlUserType272.SetIsLocalType();
			result = xamlUserType272;
			break;
		}
		case 60:
		{
			XamlUserType xamlUserType271 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.ValueType"));
			xamlUserType271.SetBoxedType(GetXamlTypeByName("System.DateTime"));
			xamlUserType271.BoxInstance = xamlUserType271.BoxType<DateTime>;
			xamlUserType271.StaticInitializer = StaticInitializer_60_Nullable;
			xamlUserType271.SetIsReturnTypeStub();
			result = xamlUserType271;
			break;
		}
		case 61:
		{
			XamlUserType xamlUserType270 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType270.StaticInitializer = StaticInitializer_61_TimeType;
			xamlUserType270.AddEnumValue("MidDay", TimeType.MidDay);
			xamlUserType270.AddEnumValue("FullDay", TimeType.FullDay);
			xamlUserType270.SetIsLocalType();
			result = xamlUserType270;
			break;
		}
		case 62:
		{
			XamlUserType xamlUserType269 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType269.StaticInitializer = StaticInitializer_62_TimePeriod;
			xamlUserType269.AddEnumValue("AM", TimePeriod.AM);
			xamlUserType269.AddEnumValue("PM", TimePeriod.PM);
			xamlUserType269.SetIsLocalType();
			result = xamlUserType269;
			break;
		}
		case 63:
		{
			XamlUserType xamlUserType268 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.ValueType"));
			xamlUserType268.StaticInitializer = StaticInitializer_63_TimeSpan;
			xamlUserType268.SetIsReturnTypeStub();
			result = xamlUserType268;
			break;
		}
		case 64:
		{
			XamlUserType xamlUserType267 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Page"));
			xamlUserType267.StaticInitializer = StaticInitializer_64_DateTimePickerDialogContent;
			xamlUserType267.AddMemberName("DateResult");
			xamlUserType267.SetIsLocalType();
			result = xamlUserType267;
			break;
		}
		case 65:
		{
			XamlUserType xamlUserType266 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Page"));
			xamlUserType266.Activator = Activate_65_OneUIContentDialogContent;
			xamlUserType266.StaticInitializer = StaticInitializer_65_OneUIContentDialogContent;
			xamlUserType266.AddMemberName("ScrollViewer");
			xamlUserType266.SetIsLocalType();
			result = xamlUserType266;
			break;
		}
		case 66:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 67:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 68:
		{
			XamlUserType xamlUserType265 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ListView"));
			xamlUserType265.Activator = Activate_68_ListViewCustom;
			xamlUserType265.StaticInitializer = StaticInitializer_68_ListViewCustom;
			xamlUserType265.AddMemberName("NoItemsText");
			xamlUserType265.AddMemberName("NoItemsDescription");
			xamlUserType265.AddMemberName("CounterText");
			xamlUserType265.SetIsLocalType();
			result = xamlUserType265;
			break;
		}
		case 69:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 70:
		{
			XamlUserType xamlUserType264 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType264.Activator = Activate_70_Responsiveness;
			xamlUserType264.StaticInitializer = StaticInitializer_70_Responsiveness;
			xamlUserType264.AddMemberName("FlexibleSpacingType");
			xamlUserType264.AddMemberName("IsFlexibleSpacing");
			xamlUserType264.SetIsLocalType();
			result = xamlUserType264;
			break;
		}
		case 71:
		{
			XamlUserType xamlUserType263 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType263.StaticInitializer = StaticInitializer_71_FlexibleSpacingType;
			xamlUserType263.AddEnumValue("Wide", FlexibleSpacingType.Wide);
			xamlUserType263.AddEnumValue("Narrow", FlexibleSpacingType.Narrow);
			xamlUserType263.SetIsLocalType();
			result = xamlUserType263;
			break;
		}
		case 72:
		{
			XamlUserType xamlUserType262 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.ValueType"));
			xamlUserType262.SetBoxedType(GetXamlTypeByName("Boolean"));
			xamlUserType262.BoxInstance = xamlUserType262.BoxType<bool>;
			xamlUserType262.StaticInitializer = StaticInitializer_72_Nullable;
			xamlUserType262.SetIsReturnTypeStub();
			result = xamlUserType262;
			break;
		}
		case 73:
		{
			XamlUserType xamlUserType261 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Page"));
			xamlUserType261.StaticInitializer = StaticInitializer_73_SingleChoiceDialogContent;
			xamlUserType261.SetIsLocalType();
			result = xamlUserType261;
			break;
		}
		case 74:
		{
			XamlUserType xamlUserType260 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType260.Activator = Activate_74_BoolToVisibilityConverter;
			xamlUserType260.StaticInitializer = StaticInitializer_74_BoolToVisibilityConverter;
			xamlUserType260.SetIsLocalType();
			result = xamlUserType260;
			break;
		}
		case 75:
		{
			XamlUserType xamlUserType259 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FlatButtonBase"));
			xamlUserType259.Activator = Activate_75_SnackBarButton;
			xamlUserType259.StaticInitializer = StaticInitializer_75_SnackBarButton;
			xamlUserType259.AddMemberName("Type");
			xamlUserType259.SetIsLocalType();
			result = xamlUserType259;
			break;
		}
		case 76:
		{
			XamlUserType xamlUserType258 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType258.StaticInitializer = StaticInitializer_76_SnackBarButtonType;
			xamlUserType258.AddEnumValue("Secondary", SnackBarButtonType.Secondary);
			xamlUserType258.AddEnumValue("Red", SnackBarButtonType.Red);
			xamlUserType258.SetIsLocalType();
			result = xamlUserType258;
			break;
		}
		case 77:
		{
			XamlUserType xamlUserType257 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ContentControl"));
			xamlUserType257.Activator = Activate_77_SnackBar;
			xamlUserType257.StaticInitializer = StaticInitializer_77_SnackBar;
			xamlUserType257.AddMemberName("Message");
			xamlUserType257.AddMemberName("SnackBarDuration");
			xamlUserType257.AddMemberName("Target");
			xamlUserType257.AddMemberName("IsShowButton");
			xamlUserType257.AddMemberName("ButtonText");
			xamlUserType257.SetIsLocalType();
			result = xamlUserType257;
			break;
		}
		case 78:
		{
			XamlUserType xamlUserType256 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType256.StaticInitializer = StaticInitializer_78_SnackBarDuration;
			xamlUserType256.AddEnumValue("Short", SnackBarDuration.Short);
			xamlUserType256.AddEnumValue("Long", SnackBarDuration.Long);
			xamlUserType256.AddEnumValue("Custom", SnackBarDuration.Custom);
			xamlUserType256.SetIsLocalType();
			result = xamlUserType256;
			break;
		}
		case 79:
		{
			XamlUserType xamlUserType255 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType255.Activator = Activate_79_TimePickerKeyboard;
			xamlUserType255.StaticInitializer = StaticInitializer_79_TimePickerKeyboard;
			xamlUserType255.AddMemberName("TimeResult");
			xamlUserType255.AddMemberName("Hour");
			xamlUserType255.AddMemberName("Minute");
			xamlUserType255.AddMemberName("Type");
			xamlUserType255.SetIsLocalType();
			result = xamlUserType255;
			break;
		}
		case 80:
		{
			XamlUserType xamlUserType254 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Page"));
			xamlUserType254.StaticInitializer = StaticInitializer_80_TimePickerKeyboardDialogContent;
			xamlUserType254.AddMemberName("TimerResult");
			xamlUserType254.SetIsLocalType();
			result = xamlUserType254;
			break;
		}
		case 81:
		{
			XamlUserType xamlUserType253 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Page"));
			xamlUserType253.StaticInitializer = StaticInitializer_81_TimePickerListDialogContent;
			xamlUserType253.AddMemberName("TimerResult");
			xamlUserType253.SetIsLocalType();
			result = xamlUserType253;
			break;
		}
		case 82:
		{
			XamlUserType xamlUserType252 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType252.Activator = Activate_82_ColorPickerOption;
			xamlUserType252.StaticInitializer = StaticInitializer_82_ColorPickerOption;
			xamlUserType252.AddMemberName("IsColorPickerSwatchedSelected");
			xamlUserType252.SetIsLocalType();
			result = xamlUserType252;
			break;
		}
		case 83:
		{
			XamlUserType xamlUserType251 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType251.Activator = Activate_83_ColorPickerDescriptor;
			xamlUserType251.StaticInitializer = StaticInitializer_83_ColorPickerDescriptor;
			xamlUserType251.AddMemberName("SelectedColor");
			xamlUserType251.AddMemberName("PreviousSelectedColor");
			xamlUserType251.SetIsLocalType();
			result = xamlUserType251;
			break;
		}
		case 84:
		{
			XamlUserType xamlUserType250 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType250.Activator = Activate_84_SubHeader;
			xamlUserType250.StaticInitializer = StaticInitializer_84_SubHeader;
			xamlUserType250.AddMemberName("IsShowDivider");
			xamlUserType250.AddMemberName("HeaderText");
			xamlUserType250.SetIsLocalType();
			result = xamlUserType250;
			break;
		}
		case 85:
		{
			XamlUserType xamlUserType249 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType249.Activator = Activate_85_ColorPickerHistory;
			xamlUserType249.StaticInitializer = StaticInitializer_85_ColorPickerHistory;
			xamlUserType249.AddMemberName("RecentColors");
			xamlUserType249.AddMemberName("SelectedColorDescription");
			xamlUserType249.AddMemberName("ItemColorBackground");
			xamlUserType249.SetIsLocalType();
			result = xamlUserType249;
			break;
		}
		case 86:
		{
			XamlUserType xamlUserType248 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ColorPicker"));
			xamlUserType248.Activator = Activate_86_ColorPickerSwatched;
			xamlUserType248.StaticInitializer = StaticInitializer_86_ColorPickerSwatched;
			xamlUserType248.AddMemberName("AlphaSliderValue");
			xamlUserType248.AddMemberName("IsColorPickerAlphaSliderEditable");
			xamlUserType248.AddMemberName("IsAlphaSliderVisible");
			xamlUserType248.AddMemberName("SelectedColorDescription");
			xamlUserType248.AddMemberName("SelectedColor");
			xamlUserType248.SetIsLocalType();
			result = xamlUserType248;
			break;
		}
		case 87:
		{
			XamlUserType xamlUserType247 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType247.Activator = Activate_87_ColorPicker;
			xamlUserType247.StaticInitializer = StaticInitializer_87_ColorPicker;
			xamlUserType247.AddMemberName("Color");
			xamlUserType247.AddMemberName("ColorSpectrumComponents");
			xamlUserType247.AddMemberName("ColorSpectrumShape");
			xamlUserType247.AddMemberName("IsAlphaEnabled");
			xamlUserType247.AddMemberName("IsAlphaTextInputVisible");
			xamlUserType247.AddMemberName("IsColorChannelTextInputVisible");
			xamlUserType247.AddMemberName("IsColorPreviewVisible");
			xamlUserType247.AddMemberName("IsColorSliderVisible");
			xamlUserType247.AddMemberName("IsColorSpectrumVisible");
			xamlUserType247.AddMemberName("IsHexInputVisible");
			xamlUserType247.AddMemberName("IsMoreButtonVisible");
			xamlUserType247.AddMemberName("MaxHue");
			xamlUserType247.AddMemberName("MaxSaturation");
			xamlUserType247.AddMemberName("MaxValue");
			xamlUserType247.AddMemberName("MinHue");
			xamlUserType247.AddMemberName("MinSaturation");
			xamlUserType247.AddMemberName("MinValue");
			xamlUserType247.AddMemberName("Orientation");
			xamlUserType247.AddMemberName("PreviousColor");
			result = xamlUserType247;
			break;
		}
		case 88:
		{
			XamlUserType xamlUserType246 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.ValueType"));
			xamlUserType246.StaticInitializer = StaticInitializer_88_Color;
			xamlUserType246.AddMemberName("A");
			xamlUserType246.AddMemberName("R");
			xamlUserType246.AddMemberName("G");
			xamlUserType246.AddMemberName("B");
			result = xamlUserType246;
			break;
		}
		case 89:
		{
			XamlUserType xamlUserType245 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType245.StaticInitializer = StaticInitializer_89_ColorSpectrumComponents;
			xamlUserType245.AddEnumValue("HueValue", ColorSpectrumComponents.HueValue);
			xamlUserType245.AddEnumValue("ValueHue", ColorSpectrumComponents.ValueHue);
			xamlUserType245.AddEnumValue("HueSaturation", ColorSpectrumComponents.HueSaturation);
			xamlUserType245.AddEnumValue("SaturationHue", ColorSpectrumComponents.SaturationHue);
			xamlUserType245.AddEnumValue("SaturationValue", ColorSpectrumComponents.SaturationValue);
			xamlUserType245.AddEnumValue("ValueSaturation", ColorSpectrumComponents.ValueSaturation);
			result = xamlUserType245;
			break;
		}
		case 90:
		{
			XamlUserType xamlUserType244 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType244.StaticInitializer = StaticInitializer_90_ColorSpectrumShape;
			xamlUserType244.AddEnumValue("Box", ColorSpectrumShape.Box);
			xamlUserType244.AddEnumValue("Ring", ColorSpectrumShape.Ring);
			result = xamlUserType244;
			break;
		}
		case 91:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 92:
		{
			XamlUserType xamlUserType243 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.ValueType"));
			xamlUserType243.SetBoxedType(GetXamlTypeByName("Windows.UI.Color"));
			xamlUserType243.BoxInstance = xamlUserType243.BoxType<Color>;
			xamlUserType243.StaticInitializer = StaticInitializer_92_Nullable;
			xamlUserType243.SetIsReturnTypeStub();
			result = xamlUserType243;
			break;
		}
		case 93:
		{
			XamlUserType xamlUserType242 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ColorPicker"));
			xamlUserType242.Activator = Activate_93_ColorPickerSpectrum;
			xamlUserType242.StaticInitializer = StaticInitializer_93_ColorPickerSpectrum;
			xamlUserType242.AddMemberName("AlphaSliderValue");
			xamlUserType242.AddMemberName("IsColorPickerAlphaSliderEditable");
			xamlUserType242.AddMemberName("IsAlphaSliderVisible");
			xamlUserType242.AddMemberName("IsSaturationSliderVisible");
			xamlUserType242.AddMemberName("SelectedColorDescription");
			xamlUserType242.SetIsLocalType();
			result = xamlUserType242;
			break;
		}
		case 94:
		{
			XamlUserType xamlUserType241 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.ValueType"));
			xamlUserType241.StaticInitializer = StaticInitializer_94_CornerRadius;
			xamlUserType241.AddMemberName("TopLeft");
			xamlUserType241.AddMemberName("TopRight");
			xamlUserType241.AddMemberName("BottomRight");
			xamlUserType241.AddMemberName("BottomLeft");
			result = xamlUserType241;
			break;
		}
		case 95:
		{
			XamlUserType xamlUserType240 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType240.Activator = Activate_95_SolidBrushColorToHexadecimalConverter;
			xamlUserType240.StaticInitializer = StaticInitializer_95_SolidBrushColorToHexadecimalConverter;
			xamlUserType240.SetIsLocalType();
			result = xamlUserType240;
			break;
		}
		case 96:
		{
			XamlUserType xamlUserType239 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.UserControl"));
			xamlUserType239.Activator = Activate_96_ColorPickerTextBox;
			xamlUserType239.StaticInitializer = StaticInitializer_96_ColorPickerTextBox;
			xamlUserType239.AddMemberName("StringResourceKey");
			xamlUserType239.AddMemberName("TextBoxStyle");
			xamlUserType239.AddMemberName("IsTextBoxLoaded");
			xamlUserType239.AddMemberName("Text");
			xamlUserType239.SetIsLocalType();
			result = xamlUserType239;
			break;
		}
		case 97:
		{
			XamlUserType xamlUserType238 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Media.XamlCompositionBrushBase"));
			xamlUserType238.Activator = Activate_97_CheckeredBrush;
			xamlUserType238.StaticInitializer = StaticInitializer_97_CheckeredBrush;
			xamlUserType238.AddMemberName("BackgroundBrush");
			xamlUserType238.AddMemberName("RectBrush");
			xamlUserType238.SetIsLocalType();
			result = xamlUserType238;
			break;
		}
		case 98:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 99:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 100:
		{
			XamlUserType xamlUserType237 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.StyleSelector"));
			xamlUserType237.Activator = Activate_100_ColorListItemSelector;
			xamlUserType237.StaticInitializer = StaticInitializer_100_ColorListItemSelector;
			xamlUserType237.AddMemberName("EmptyStyle");
			xamlUserType237.AddMemberName("NormalStyle");
			xamlUserType237.SetIsLocalType();
			result = xamlUserType237;
			break;
		}
		case 101:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 102:
		{
			XamlUserType xamlUserType236 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType236.Activator = Activate_102_StringToSolidColorBrushConverter;
			xamlUserType236.StaticInitializer = StaticInitializer_102_StringToSolidColorBrushConverter;
			xamlUserType236.SetIsLocalType();
			result = xamlUserType236;
			break;
		}
		case 103:
		{
			XamlUserType xamlUserType235 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType235.Activator = Activate_103_CornerRadiusAutoHalfCorner;
			xamlUserType235.StaticInitializer = StaticInitializer_103_CornerRadiusAutoHalfCorner;
			xamlUserType235.AddMemberName("CornerPoint");
			xamlUserType235.AddMemberName("CanOverride");
			xamlUserType235.SetIsLocalType();
			result = xamlUserType235;
			break;
		}
		case 104:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 105:
		{
			XamlUserType xamlUserType234 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.GridView"));
			xamlUserType234.Activator = Activate_105_ColorPickerHistoryGridViewCustom;
			xamlUserType234.StaticInitializer = StaticInitializer_105_ColorPickerHistoryGridViewCustom;
			xamlUserType234.SetIsLocalType();
			result = xamlUserType234;
			break;
		}
		case 106:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 107:
		{
			XamlUserType xamlUserType233 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.ValueType"));
			xamlUserType233.StaticInitializer = StaticInitializer_107_Thickness;
			xamlUserType233.AddMemberName("Left");
			xamlUserType233.AddMemberName("Top");
			xamlUserType233.AddMemberName("Right");
			xamlUserType233.AddMemberName("Bottom");
			result = xamlUserType233;
			break;
		}
		case 108:
		{
			XamlUserType xamlUserType232 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Markup.MarkupExtension"));
			xamlUserType232.Activator = Activate_108_OverlayColorsToSolidColorBrushExtension;
			xamlUserType232.StaticInitializer = StaticInitializer_108_OverlayColorsToSolidColorBrushExtension;
			xamlUserType232.AddMemberName("ColorList");
			xamlUserType232.SetIsMarkupExtension();
			xamlUserType232.SetIsLocalType();
			result = xamlUserType232;
			break;
		}
		case 109:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 110:
		{
			XamlUserType xamlUserType231 = new XamlUserType(this, fullName, type, null);
			xamlUserType231.StaticInitializer = StaticInitializer_110_IList;
			xamlUserType231.CollectionAdd = VectorAdd_110_IList;
			xamlUserType231.SetIsReturnTypeStub();
			result = xamlUserType231;
			break;
		}
		case 111:
			result = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.ValueType"))
			{
				StaticInitializer = StaticInitializer_111_FontWeight
			};
			break;
		case 112:
		{
			XamlUserType xamlUserType230 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.ToggleButton"));
			xamlUserType230.Activator = Activate_112_ColorPickerOptionCustomButton;
			xamlUserType230.StaticInitializer = StaticInitializer_112_ColorPickerOptionCustomButton;
			xamlUserType230.SetIsLocalType();
			result = xamlUserType230;
			break;
		}
		case 113:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 114:
		{
			XamlUserType xamlUserType229 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType229.Activator = Activate_114_CornerRadiusToDoubleConverter;
			xamlUserType229.StaticInitializer = StaticInitializer_114_CornerRadiusToDoubleConverter;
			xamlUserType229.AddMemberName("ConvertionRoundingStrategy");
			xamlUserType229.SetIsLocalType();
			result = xamlUserType229;
			break;
		}
		case 115:
		{
			XamlUserType xamlUserType228 = new XamlUserType(this, fullName, type, null);
			xamlUserType228.StaticInitializer = StaticInitializer_115_ICornerRadiusRoundingStrategyConvertion;
			xamlUserType228.SetIsReturnTypeStub();
			xamlUserType228.SetIsLocalType();
			result = xamlUserType228;
			break;
		}
		case 116:
		{
			XamlUserType xamlUserType227 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum"));
			xamlUserType227.Activator = Activate_116_OneUIColorSpectrum;
			xamlUserType227.StaticInitializer = StaticInitializer_116_OneUIColorSpectrum;
			xamlUserType227.SetIsLocalType();
			result = xamlUserType227;
			break;
		}
		case 117:
		{
			XamlUserType xamlUserType226 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType226.Activator = Activate_117_ColorSpectrum;
			xamlUserType226.StaticInitializer = StaticInitializer_117_ColorSpectrum;
			xamlUserType226.AddMemberName("Components");
			xamlUserType226.AddMemberName("MaxHue");
			xamlUserType226.AddMemberName("MaxSaturation");
			xamlUserType226.AddMemberName("MaxValue");
			xamlUserType226.AddMemberName("MinHue");
			xamlUserType226.AddMemberName("MinSaturation");
			xamlUserType226.AddMemberName("MinValue");
			xamlUserType226.AddMemberName("Shape");
			xamlUserType226.AddMemberName("Color");
			xamlUserType226.AddMemberName("HsvColor");
			result = xamlUserType226;
			break;
		}
		case 118:
		{
			XamlUserType xamlUserType225 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.ValueType"));
			xamlUserType225.StaticInitializer = StaticInitializer_118_Vector4;
			xamlUserType225.SetIsReturnTypeStub();
			result = xamlUserType225;
			break;
		}
		case 119:
		{
			XamlUserType xamlUserType224 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.ColorPickerSlider"));
			xamlUserType224.Activator = Activate_119_ColorPickerSliderCustom;
			xamlUserType224.StaticInitializer = StaticInitializer_119_ColorPickerSliderCustom;
			xamlUserType224.SetIsLocalType();
			result = xamlUserType224;
			break;
		}
		case 120:
		{
			XamlUserType xamlUserType223 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Slider"));
			xamlUserType223.Activator = Activate_120_ColorPickerSlider;
			xamlUserType223.StaticInitializer = StaticInitializer_120_ColorPickerSlider;
			xamlUserType223.AddMemberName("ColorChannel");
			result = xamlUserType223;
			break;
		}
		case 121:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 122:
		{
			XamlUserType xamlUserType222 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType222.StaticInitializer = StaticInitializer_122_ColorPickerHsvChannel;
			xamlUserType222.AddEnumValue("Hue", ColorPickerHsvChannel.Hue);
			xamlUserType222.AddEnumValue("Saturation", ColorPickerHsvChannel.Saturation);
			xamlUserType222.AddEnumValue("Value", ColorPickerHsvChannel.Value);
			xamlUserType222.AddEnumValue("Alpha", ColorPickerHsvChannel.Alpha);
			result = xamlUserType222;
			break;
		}
		case 123:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 124:
		{
			XamlUserType xamlUserType221 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType221.StaticInitializer = StaticInitializer_124_Effects;
			xamlUserType221.AddMemberName("Shadow");
			result = xamlUserType221;
			break;
		}
		case 125:
		{
			XamlUserType xamlUserType220 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.DependencyObject"));
			xamlUserType220.StaticInitializer = StaticInitializer_125_AttachedShadowBase;
			xamlUserType220.AddMemberName("BlurRadius");
			xamlUserType220.AddMemberName("Opacity");
			xamlUserType220.AddMemberName("Offset");
			xamlUserType220.AddMemberName("Color");
			result = xamlUserType220;
			break;
		}
		case 126:
		{
			XamlUserType xamlUserType219 = new XamlUserType(this, fullName, type, GetXamlTypeByName("CommunityToolkit.WinUI.AttachedShadowBase"));
			xamlUserType219.Activator = Activate_126_AttachedCardShadow;
			xamlUserType219.StaticInitializer = StaticInitializer_126_AttachedCardShadow;
			xamlUserType219.AddMemberName("CornerRadius");
			xamlUserType219.AddMemberName("InnerContentClipMode");
			result = xamlUserType219;
			break;
		}
		case 127:
		{
			XamlUserType xamlUserType218 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType218.StaticInitializer = StaticInitializer_127_InnerContentClipMode;
			xamlUserType218.AddEnumValue("None", InnerContentClipMode.None);
			xamlUserType218.AddEnumValue("CompositionMaskBrush", InnerContentClipMode.CompositionMaskBrush);
			xamlUserType218.AddEnumValue("CompositionGeometricClip", InnerContentClipMode.CompositionGeometricClip);
			result = xamlUserType218;
			break;
		}
		case 128:
		{
			XamlUserType xamlUserType217 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType217.Activator = Activate_128_CornerRadiusCornersConverter;
			xamlUserType217.StaticInitializer = StaticInitializer_128_CornerRadiusCornersConverter;
			xamlUserType217.SetIsLocalType();
			result = xamlUserType217;
			break;
		}
		case 129:
		{
			XamlUserType xamlUserType216 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.StyleSelector"));
			xamlUserType216.Activator = Activate_129_ColorPickerGridViewItemRadiusSelector;
			xamlUserType216.StaticInitializer = StaticInitializer_129_ColorPickerGridViewItemRadiusSelector;
			xamlUserType216.AddMemberName("BottomLeftItem");
			xamlUserType216.AddMemberName("BottomRightItem");
			xamlUserType216.AddMemberName("MiddleItem");
			xamlUserType216.AddMemberName("TopLeftItem");
			xamlUserType216.AddMemberName("TopRightItem");
			xamlUserType216.SetIsLocalType();
			result = xamlUserType216;
			break;
		}
		case 130:
		{
			XamlUserType xamlUserType215 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.GridView"));
			xamlUserType215.Activator = Activate_130_ColorPickerSwatchedGridViewCustom;
			xamlUserType215.StaticInitializer = StaticInitializer_130_ColorPickerSwatchedGridViewCustom;
			xamlUserType215.SetIsLocalType();
			result = xamlUserType215;
			break;
		}
		case 131:
		{
			XamlUserType xamlUserType214 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.ResourceDictionary"));
			xamlUserType214.Activator = Activate_131_OneUIResources;
			xamlUserType214.StaticInitializer = StaticInitializer_131_OneUIResources;
			xamlUserType214.DictionaryAdd = MapAdd_131_OneUIResources;
			xamlUserType214.SetIsLocalType();
			result = xamlUserType214;
			break;
		}
		case 132:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 133:
		{
			XamlUserType xamlUserType213 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.BadgeBase"));
			xamlUserType213.Activator = Activate_133_NumberBadge;
			xamlUserType213.StaticInitializer = StaticInitializer_133_NumberBadge;
			xamlUserType213.AddMemberName("Value");
			xamlUserType213.SetIsLocalType();
			result = xamlUserType213;
			break;
		}
		case 134:
		{
			XamlUserType xamlUserType212 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType212.StaticInitializer = StaticInitializer_134_BadgeBase;
			xamlUserType212.AddMemberName("IsSelected");
			xamlUserType212.SetIsLocalType();
			result = xamlUserType212;
			break;
		}
		case 135:
		{
			XamlUserType xamlUserType211 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.BadgeBase"));
			xamlUserType211.Activator = Activate_135_TextBadge;
			xamlUserType211.StaticInitializer = StaticInitializer_135_TextBadge;
			xamlUserType211.SetIsLocalType();
			result = xamlUserType211;
			break;
		}
		case 136:
		{
			XamlUserType xamlUserType210 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.BadgeBase"));
			xamlUserType210.Activator = Activate_136_AlertBadge;
			xamlUserType210.StaticInitializer = StaticInitializer_136_AlertBadge;
			xamlUserType210.SetIsLocalType();
			result = xamlUserType210;
			break;
		}
		case 137:
		{
			XamlUserType xamlUserType209 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.BadgeBase"));
			xamlUserType209.Activator = Activate_137_DotBadge;
			xamlUserType209.StaticInitializer = StaticInitializer_137_DotBadge;
			xamlUserType209.SetIsLocalType();
			result = xamlUserType209;
			break;
		}
		case 138:
		{
			XamlUserType xamlUserType208 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Button"));
			xamlUserType208.Activator = Activate_138_AddButton;
			xamlUserType208.StaticInitializer = StaticInitializer_138_AddButton;
			xamlUserType208.SetIsLocalType();
			result = xamlUserType208;
			break;
		}
		case 139:
		{
			XamlUserType xamlUserType207 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Button"));
			xamlUserType207.Activator = Activate_139_DeleteButton;
			xamlUserType207.StaticInitializer = StaticInitializer_139_DeleteButton;
			xamlUserType207.SetIsLocalType();
			result = xamlUserType207;
			break;
		}
		case 140:
		{
			XamlUserType xamlUserType206 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ContainedButtonBase"));
			xamlUserType206.Activator = Activate_140_ContainedButtonBodyColored;
			xamlUserType206.StaticInitializer = StaticInitializer_140_ContainedButtonBodyColored;
			xamlUserType206.SetIsLocalType();
			result = xamlUserType206;
			break;
		}
		case 141:
		{
			XamlUserType xamlUserType205 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Button"));
			xamlUserType205.Activator = Activate_141_ContainedButtonBase;
			xamlUserType205.StaticInitializer = StaticInitializer_141_ContainedButtonBase;
			xamlUserType205.AddMemberName("IsProgressEnabled");
			xamlUserType205.SetIsLocalType();
			result = xamlUserType205;
			break;
		}
		case 142:
		{
			XamlUserType xamlUserType204 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType204.StaticInitializer = StaticInitializer_142_Interaction;
			xamlUserType204.AddMemberName("Behaviors");
			result = xamlUserType204;
			break;
		}
		case 143:
		{
			XamlUserType xamlUserType203 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.DependencyObjectCollection"));
			xamlUserType203.StaticInitializer = StaticInitializer_143_BehaviorCollection;
			xamlUserType203.CollectionAdd = VectorAdd_143_BehaviorCollection;
			xamlUserType203.SetIsReturnTypeStub();
			result = xamlUserType203;
			break;
		}
		case 144:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 145:
		{
			XamlUserType xamlUserType202 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ProgressCircle"));
			xamlUserType202.Activator = Activate_145_ProgressCircleIndeterminate;
			xamlUserType202.StaticInitializer = StaticInitializer_145_ProgressCircleIndeterminate;
			xamlUserType202.AddMemberName("Foreground");
			xamlUserType202.AddMemberName("PointForeground");
			xamlUserType202.SetIsLocalType();
			result = xamlUserType202;
			break;
		}
		case 146:
		{
			XamlUserType xamlUserType201 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType201.StaticInitializer = StaticInitializer_146_ProgressCircle;
			xamlUserType201.AddMemberName("Size");
			xamlUserType201.AddMemberName("Text");
			xamlUserType201.SetIsLocalType();
			result = xamlUserType201;
			break;
		}
		case 147:
		{
			XamlUserType xamlUserType200 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType200.StaticInitializer = StaticInitializer_147_ProgressCircleSize;
			xamlUserType200.AddEnumValue("SmallTitle", ProgressCircleSize.SmallTitle);
			xamlUserType200.AddEnumValue("Small", ProgressCircleSize.Small);
			xamlUserType200.AddEnumValue("Medium", ProgressCircleSize.Medium);
			xamlUserType200.AddEnumValue("Large", ProgressCircleSize.Large);
			xamlUserType200.AddEnumValue("XLarge", ProgressCircleSize.XLarge);
			xamlUserType200.SetIsLocalType();
			result = xamlUserType200;
			break;
		}
		case 148:
		{
			XamlUserType xamlUserType199 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.Xaml.Interactivity.Behavior`1<Microsoft.UI.Xaml.FrameworkElement>"));
			xamlUserType199.Activator = Activate_148_TooltipForTrimmedButtonBehavior;
			xamlUserType199.StaticInitializer = StaticInitializer_148_TooltipForTrimmedButtonBehavior;
			xamlUserType199.SetIsLocalType();
			result = xamlUserType199;
			break;
		}
		case 149:
		{
			XamlUserType xamlUserType198 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.Xaml.Interactivity.Behavior"));
			xamlUserType198.StaticInitializer = StaticInitializer_149_Behavior;
			xamlUserType198.AddMemberName("AssociatedObject");
			result = xamlUserType198;
			break;
		}
		case 150:
		{
			XamlUserType xamlUserType197 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.DependencyObject"));
			xamlUserType197.StaticInitializer = StaticInitializer_150_Behavior;
			xamlUserType197.AddMemberName("AssociatedObject");
			result = xamlUserType197;
			break;
		}
		case 151:
		{
			XamlUserType xamlUserType196 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ContainedButtonBase"));
			xamlUserType196.Activator = Activate_151_ContainedButtonBody;
			xamlUserType196.StaticInitializer = StaticInitializer_151_ContainedButtonBody;
			xamlUserType196.SetIsLocalType();
			result = xamlUserType196;
			break;
		}
		case 152:
		{
			XamlUserType xamlUserType195 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ContainedButtonBase"));
			xamlUserType195.Activator = Activate_152_ContainedButtonColored;
			xamlUserType195.StaticInitializer = StaticInitializer_152_ContainedButtonColored;
			xamlUserType195.SetIsLocalType();
			result = xamlUserType195;
			break;
		}
		case 153:
		{
			XamlUserType xamlUserType194 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ContainedButtonBase"));
			xamlUserType194.Activator = Activate_153_ContainedButton;
			xamlUserType194.StaticInitializer = StaticInitializer_153_ContainedButton;
			xamlUserType194.AddMemberName("Type");
			xamlUserType194.AddMemberName("Size");
			xamlUserType194.SetIsLocalType();
			result = xamlUserType194;
			break;
		}
		case 154:
		{
			XamlUserType xamlUserType193 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType193.StaticInitializer = StaticInitializer_154_ContainedButtonType;
			xamlUserType193.AddEnumValue("Primary", ContainedButtonType.Primary);
			xamlUserType193.AddEnumValue("Secondary", ContainedButtonType.Secondary);
			xamlUserType193.SetIsLocalType();
			result = xamlUserType193;
			break;
		}
		case 155:
		{
			XamlUserType xamlUserType192 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType192.StaticInitializer = StaticInitializer_155_ContainedButtonSize;
			xamlUserType192.AddEnumValue("Large", ContainedButtonSize.Large);
			xamlUserType192.AddEnumValue("Medium", ContainedButtonSize.Medium);
			xamlUserType192.AddEnumValue("Small", ContainedButtonSize.Small);
			xamlUserType192.SetIsLocalType();
			result = xamlUserType192;
			break;
		}
		case 156:
		{
			XamlUserType xamlUserType191 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Button"));
			xamlUserType191.Activator = Activate_156_ContentButton;
			xamlUserType191.StaticInitializer = StaticInitializer_156_ContentButton;
			xamlUserType191.AddMemberName("Shape");
			xamlUserType191.AddMemberName("IsPressAndHoldEnabled");
			xamlUserType191.AddMemberName("PressAndHoldInterval");
			xamlUserType191.SetIsLocalType();
			result = xamlUserType191;
			break;
		}
		case 157:
		{
			XamlUserType xamlUserType190 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType190.StaticInitializer = StaticInitializer_157_ButtonShapeEnum;
			xamlUserType190.AddEnumValue("Default", ButtonShapeEnum.Default);
			xamlUserType190.AddEnumValue("Rounded", ButtonShapeEnum.Rounded);
			xamlUserType190.SetIsLocalType();
			result = xamlUserType190;
			break;
		}
		case 158:
		{
			XamlUserType xamlUserType189 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.ToggleButton"));
			xamlUserType189.Activator = Activate_158_ContentToggleButton;
			xamlUserType189.StaticInitializer = StaticInitializer_158_ContentToggleButton;
			xamlUserType189.AddMemberName("Shape");
			xamlUserType189.SetIsLocalType();
			result = xamlUserType189;
			break;
		}
		case 159:
		{
			XamlUserType xamlUserType188 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Button"));
			xamlUserType188.Activator = Activate_159_EditButton;
			xamlUserType188.StaticInitializer = StaticInitializer_159_EditButton;
			xamlUserType188.AddMemberName("Type");
			xamlUserType188.SetIsLocalType();
			result = xamlUserType188;
			break;
		}
		case 160:
		{
			XamlUserType xamlUserType187 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType187.StaticInitializer = StaticInitializer_160_EditButtonType;
			xamlUserType187.AddEnumValue("Add", EditButtonType.Add);
			xamlUserType187.AddEnumValue("Delete", EditButtonType.Delete);
			xamlUserType187.SetIsLocalType();
			result = xamlUserType187;
			break;
		}
		case 161:
		{
			XamlUserType xamlUserType186 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FlatButtonBase"));
			xamlUserType186.Activator = Activate_161_FlatUnderlineButton;
			xamlUserType186.StaticInitializer = StaticInitializer_161_FlatUnderlineButton;
			xamlUserType186.SetIsLocalType();
			result = xamlUserType186;
			break;
		}
		case 162:
		{
			XamlUserType xamlUserType185 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Button"));
			xamlUserType185.Activator = Activate_162_FloatingActionButton;
			xamlUserType185.StaticInitializer = StaticInitializer_162_FloatingActionButton;
			xamlUserType185.AddMemberName("Visibility");
			xamlUserType185.AddMemberName("IsBlur");
			xamlUserType185.SetIsLocalType();
			result = xamlUserType185;
			break;
		}
		case 163:
		{
			XamlUserType xamlUserType184 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType184.Activator = Activate_163_ElevationCorner;
			xamlUserType184.StaticInitializer = StaticInitializer_163_ElevationCorner;
			xamlUserType184.AddMemberName("CornerRadius");
			xamlUserType184.SetIsLocalType();
			result = xamlUserType184;
			break;
		}
		case 164:
		{
			XamlUserType xamlUserType183 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.UserControl"));
			xamlUserType183.Activator = Activate_164_BlurLayer;
			xamlUserType183.StaticInitializer = StaticInitializer_164_BlurLayer;
			xamlUserType183.SetContentPropertyName("Samsung.OneUI.WinUI.Tokens.BlurLayer.LayerContent");
			xamlUserType183.AddMemberName("LayerContent");
			xamlUserType183.AddMemberName("BlurLevel");
			xamlUserType183.AddMemberName("FallbackBackground");
			xamlUserType183.AddMemberName("IsBlur");
			xamlUserType183.AddMemberName("Vibrancy");
			xamlUserType183.SetIsLocalType();
			result = xamlUserType183;
			break;
		}
		case 165:
		{
			XamlUserType xamlUserType182 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType182.StaticInitializer = StaticInitializer_165_BlurLevel;
			xamlUserType182.AddEnumValue("UltraThin", BlurLevel.UltraThin);
			xamlUserType182.AddEnumValue("Thin", BlurLevel.Thin);
			xamlUserType182.AddEnumValue("Regular", BlurLevel.Regular);
			xamlUserType182.AddEnumValue("Thick", BlurLevel.Thick);
			xamlUserType182.AddEnumValue("UltraThick", BlurLevel.UltraThick);
			xamlUserType182.SetIsLocalType();
			result = xamlUserType182;
			break;
		}
		case 166:
		{
			XamlUserType xamlUserType181 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType181.StaticInitializer = StaticInitializer_166_VibrancyLevel;
			xamlUserType181.AddEnumValue("High", Samsung.OneUI.WinUI.Tokens.VibrancyLevel.High);
			xamlUserType181.AddEnumValue("Medium", Samsung.OneUI.WinUI.Tokens.VibrancyLevel.Medium);
			xamlUserType181.AddEnumValue("Low", Samsung.OneUI.WinUI.Tokens.VibrancyLevel.Low);
			xamlUserType181.SetIsLocalType();
			result = xamlUserType181;
			break;
		}
		case 167:
		{
			XamlUserType xamlUserType180 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Button"));
			xamlUserType180.Activator = Activate_167_GoToTopButton;
			xamlUserType180.StaticInitializer = StaticInitializer_167_GoToTopButton;
			xamlUserType180.AddMemberName("IsBlur");
			xamlUserType180.SetIsLocalType();
			result = xamlUserType180;
			break;
		}
		case 168:
		{
			XamlUserType xamlUserType179 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.HyperlinkButton"));
			xamlUserType179.Activator = Activate_168_HyperlinkButton;
			xamlUserType179.StaticInitializer = StaticInitializer_168_HyperlinkButton;
			xamlUserType179.AddMemberName("TextTrimming");
			xamlUserType179.AddMemberName("IsTextTrimmed");
			xamlUserType179.SetIsLocalType();
			result = xamlUserType179;
			break;
		}
		case 169:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 170:
		{
			XamlUserType xamlUserType178 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Button"));
			xamlUserType178.Activator = Activate_170_ProgressButton;
			xamlUserType178.StaticInitializer = StaticInitializer_170_ProgressButton;
			xamlUserType178.AddMemberName("IsProgressEnabled");
			xamlUserType178.AddMemberName("Type");
			xamlUserType178.SetIsLocalType();
			result = xamlUserType178;
			break;
		}
		case 171:
		{
			XamlUserType xamlUserType177 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType177.StaticInitializer = StaticInitializer_171_ProgressButtonType;
			xamlUserType177.AddEnumValue("Flat", ProgressButtonType.Flat);
			xamlUserType177.AddEnumValue("Contained", ProgressButtonType.Contained);
			xamlUserType177.AddEnumValue("ContainedColored", ProgressButtonType.ContainedColored);
			xamlUserType177.AddEnumValue("ContainedBody", ProgressButtonType.ContainedBody);
			xamlUserType177.AddEnumValue("ContainedBodyColored", ProgressButtonType.ContainedBodyColored);
			xamlUserType177.SetIsLocalType();
			result = xamlUserType177;
			break;
		}
		case 172:
		{
			XamlUserType xamlUserType176 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType176.Activator = Activate_172_StringToVisibilityConverter;
			xamlUserType176.StaticInitializer = StaticInitializer_172_StringToVisibilityConverter;
			xamlUserType176.SetIsLocalType();
			result = xamlUserType176;
			break;
		}
		case 173:
		{
			XamlUserType xamlUserType175 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType175.Activator = Activate_173_ImageToVisibilityConverter;
			xamlUserType175.StaticInitializer = StaticInitializer_173_ImageToVisibilityConverter;
			xamlUserType175.SetIsLocalType();
			result = xamlUserType175;
			break;
		}
		case 174:
		{
			XamlUserType xamlUserType174 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ToolTip"));
			xamlUserType174.Activator = Activate_174_ToolTip;
			xamlUserType174.StaticInitializer = StaticInitializer_174_ToolTip;
			xamlUserType174.SetIsLocalType();
			result = xamlUserType174;
			break;
		}
		case 175:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 176:
		{
			XamlUserType xamlUserType173 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.Xaml.Interactivity.Behavior`1<Microsoft.UI.Xaml.FrameworkElement>"));
			xamlUserType173.Activator = Activate_176_TooltipForTrimmedTextBlockBehavior;
			xamlUserType173.StaticInitializer = StaticInitializer_176_TooltipForTrimmedTextBlockBehavior;
			xamlUserType173.AddMemberName("TextBlockName");
			xamlUserType173.SetIsLocalType();
			result = xamlUserType173;
			break;
		}
		case 177:
		{
			XamlUserType xamlUserType172 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.ValueType"));
			xamlUserType172.StaticInitializer = StaticInitializer_177_KeyTime;
			xamlUserType172.AddMemberName("TimeSpan");
			result = xamlUserType172;
			break;
		}
		case 178:
		{
			XamlUserType xamlUserType171 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType171.Activator = Activate_178_NullToVisibilityConverter;
			xamlUserType171.StaticInitializer = StaticInitializer_178_NullToVisibilityConverter;
			xamlUserType171.SetIsLocalType();
			result = xamlUserType171;
			break;
		}
		case 179:
		{
			XamlUserType xamlUserType170 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.CheckBox"));
			xamlUserType170.Activator = Activate_179_CheckBox;
			xamlUserType170.StaticInitializer = StaticInitializer_179_CheckBox;
			xamlUserType170.AddMemberName("Icon");
			xamlUserType170.AddMemberName("IconSvgStyle");
			xamlUserType170.AddMemberName("Uri");
			xamlUserType170.AddMemberName("Type");
			xamlUserType170.SetIsLocalType();
			result = xamlUserType170;
			break;
		}
		case 180:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 181:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 182:
		{
			XamlUserType xamlUserType169 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType169.StaticInitializer = StaticInitializer_182_CheckBoxType;
			xamlUserType169.AddEnumValue("Default", CheckBoxType.Default);
			xamlUserType169.AddEnumValue("White", CheckBoxType.White);
			xamlUserType169.AddEnumValue("Thumbnail", CheckBoxType.Thumbnail);
			xamlUserType169.AddEnumValue("MultipleSelection", CheckBoxType.MultipleSelection);
			xamlUserType169.AddEnumValue("Ghost", CheckBoxType.Ghost);
			xamlUserType169.SetIsLocalType();
			result = xamlUserType169;
			break;
		}
		case 183:
		{
			XamlUserType xamlUserType168 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType168.Activator = Activate_183_ChipsItemIconVisibilityConverter;
			xamlUserType168.StaticInitializer = StaticInitializer_183_ChipsItemIconVisibilityConverter;
			xamlUserType168.SetIsLocalType();
			result = xamlUserType168;
			break;
		}
		case 184:
		{
			XamlUserType xamlUserType167 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType167.Activator = Activate_184_ChipsItemImageIconVisibilityConverter;
			xamlUserType167.StaticInitializer = StaticInitializer_184_ChipsItemImageIconVisibilityConverter;
			xamlUserType167.SetIsLocalType();
			result = xamlUserType167;
			break;
		}
		case 185:
		{
			XamlUserType xamlUserType166 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType166.Activator = Activate_185_ChipsItemIconStyleConverter;
			xamlUserType166.StaticInitializer = StaticInitializer_185_ChipsItemIconStyleConverter;
			xamlUserType166.SetIsLocalType();
			result = xamlUserType166;
			break;
		}
		case 186:
		{
			XamlUserType xamlUserType165 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.StyleSelector"));
			xamlUserType165.Activator = Activate_186_ChipsItemStyleSelector;
			xamlUserType165.StaticInitializer = StaticInitializer_186_ChipsItemStyleSelector;
			xamlUserType165.AddMemberName("CancelBorderStyle");
			xamlUserType165.AddMemberName("CancelStyle");
			xamlUserType165.AddMemberName("MinusBorderStyle");
			xamlUserType165.AddMemberName("MinusStyle");
			xamlUserType165.AddMemberName("NoneBorderStyle");
			xamlUserType165.AddMemberName("NoneStyle");
			xamlUserType165.AddMemberName("TagBorderStyle");
			xamlUserType165.AddMemberName("TagStyle");
			xamlUserType165.SetIsLocalType();
			result = xamlUserType165;
			break;
		}
		case 187:
		{
			XamlUserType xamlUserType164 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.Xaml.Interactivity.Behavior`1<Microsoft.UI.Xaml.FrameworkElement>"));
			xamlUserType164.Activator = Activate_187_CornerRadiusBorderCompensationBehavior;
			xamlUserType164.StaticInitializer = StaticInitializer_187_CornerRadiusBorderCompensationBehavior;
			xamlUserType164.AddMemberName("Compensation");
			xamlUserType164.SetIsLocalType();
			result = xamlUserType164;
			break;
		}
		case 188:
		{
			XamlUserType xamlUserType163 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.IconElement"));
			xamlUserType163.Activator = Activate_188_ImageIcon;
			xamlUserType163.StaticInitializer = StaticInitializer_188_ImageIcon;
			xamlUserType163.AddMemberName("Source");
			result = xamlUserType163;
			break;
		}
		case 189:
		{
			XamlUserType xamlUserType162 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.ValueType"));
			xamlUserType162.StaticInitializer = StaticInitializer_189_Byte;
			xamlUserType162.SetIsReturnTypeStub();
			result = xamlUserType162;
			break;
		}
		case 190:
		{
			XamlUserType xamlUserType161 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.AppBarButton"));
			xamlUserType161.Activator = Activate_190_CommandBarButton;
			xamlUserType161.StaticInitializer = StaticInitializer_190_CommandBarButton;
			xamlUserType161.AddMemberName("LabelVisibility");
			xamlUserType161.AddMemberName("IconSvgStyle");
			xamlUserType161.SetIsLocalType();
			result = xamlUserType161;
			break;
		}
		case 191:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 192:
		{
			XamlUserType xamlUserType160 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType160.Activator = Activate_192_IntToEnumConverter;
			xamlUserType160.StaticInitializer = StaticInitializer_192_IntToEnumConverter;
			xamlUserType160.SetIsLocalType();
			result = xamlUserType160;
			break;
		}
		case 193:
		{
			XamlUserType xamlUserType159 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.CommandBar"));
			xamlUserType159.Activator = Activate_193_CommandBar;
			xamlUserType159.StaticInitializer = StaticInitializer_193_CommandBar;
			xamlUserType159.AddMemberName("CurrentItemsMaxWidth");
			xamlUserType159.AddMemberName("MoreOptionsOverflowItems");
			xamlUserType159.AddMemberName("BackButtonCommand");
			xamlUserType159.AddMemberName("BackButtonCommandParameter");
			xamlUserType159.AddMemberName("MoreOptionsItems");
			xamlUserType159.AddMemberName("IsBackButtonVisible");
			xamlUserType159.AddMemberName("IsOptionsButtonVisible");
			xamlUserType159.AddMemberName("BackButtonType");
			xamlUserType159.AddMemberName("MoreOptionsBadge");
			xamlUserType159.AddMemberName("MoreOptionsHorizontalOffset");
			xamlUserType159.AddMemberName("MoreOptionsVerticalOffset");
			xamlUserType159.AddMemberName("MoreOptionsPlacement");
			xamlUserType159.AddMemberName("MoreOptionsToolTipContent");
			xamlUserType159.AddMemberName("SubtitleText");
			xamlUserType159.AddMemberName("IsSubtitleVisible");
			xamlUserType159.SetIsLocalType();
			result = xamlUserType159;
			break;
		}
		case 194:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 195:
		{
			XamlUserType xamlUserType158 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Collections.ObjectModel.Collection`1<Microsoft.UI.Xaml.Controls.MenuFlyoutItemBase>"));
			xamlUserType158.StaticInitializer = StaticInitializer_195_ObservableCollection;
			xamlUserType158.CollectionAdd = VectorAdd_195_ObservableCollection;
			xamlUserType158.SetIsReturnTypeStub();
			result = xamlUserType158;
			break;
		}
		case 196:
			result = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"))
			{
				Activator = Activate_196_Collection,
				StaticInitializer = StaticInitializer_196_Collection,
				CollectionAdd = VectorAdd_196_Collection
			};
			break;
		case 197:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 198:
		{
			XamlUserType xamlUserType157 = new XamlUserType(this, fullName, type, null);
			xamlUserType157.StaticInitializer = StaticInitializer_198_ICommand;
			xamlUserType157.SetIsReturnTypeStub();
			result = xamlUserType157;
			break;
		}
		case 199:
		{
			XamlUserType xamlUserType156 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType156.StaticInitializer = StaticInitializer_199_CommandBarBackButtonType;
			xamlUserType156.AddEnumValue("Default", CommandBarBackButtonType.Default);
			xamlUserType156.AddEnumValue("CollapsibleSideMenu", CommandBarBackButtonType.CollapsibleSideMenu);
			xamlUserType156.SetIsLocalType();
			result = xamlUserType156;
			break;
		}
		case 200:
		{
			XamlUserType xamlUserType155 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.ValueType"));
			xamlUserType155.SetBoxedType(GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.FlyoutPlacementMode"));
			xamlUserType155.BoxInstance = xamlUserType155.BoxType<FlyoutPlacementMode>;
			xamlUserType155.StaticInitializer = StaticInitializer_200_Nullable;
			xamlUserType155.SetIsReturnTypeStub();
			result = xamlUserType155;
			break;
		}
		case 201:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 202:
		{
			XamlUserType xamlUserType154 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.Xaml.Interactivity.Behavior`1<Microsoft.UI.Xaml.FrameworkElement>"));
			xamlUserType154.Activator = Activate_202_FlexibleSpacingBehavior;
			xamlUserType154.StaticInitializer = StaticInitializer_202_FlexibleSpacingBehavior;
			xamlUserType154.AddMemberName("FlexibleSpacingTargetContent");
			xamlUserType154.AddMemberName("IsFlexibleSpacing");
			xamlUserType154.AddMemberName("Type");
			xamlUserType154.AddMemberName("MarginTiny");
			xamlUserType154.AddMemberName("MarginSmall");
			xamlUserType154.AddMemberName("MarginMedium");
			xamlUserType154.AddMemberName("MarginLarge");
			xamlUserType154.AddMemberName("MarginHuge");
			xamlUserType154.AddMemberName("MarginOff");
			xamlUserType154.SetIsLocalType();
			result = xamlUserType154;
			break;
		}
		case 203:
		{
			XamlUserType xamlUserType153 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.AppBarButton"));
			xamlUserType153.Activator = Activate_203_IconButton;
			xamlUserType153.StaticInitializer = StaticInitializer_203_IconButton;
			xamlUserType153.AddMemberName("IconSvgStyle");
			xamlUserType153.AddMemberName("LabelVisibility");
			xamlUserType153.SetIsLocalType();
			result = xamlUserType153;
			break;
		}
		case 204:
		{
			XamlUserType xamlUserType152 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.MenuFlyout"));
			xamlUserType152.Activator = Activate_204_ListFlyout;
			xamlUserType152.StaticInitializer = StaticInitializer_204_ListFlyout;
			xamlUserType152.AddMemberName("IsCommandBarChild");
			xamlUserType152.AddMemberName("HorizontalOffset");
			xamlUserType152.AddMemberName("VerticalOffset");
			xamlUserType152.AddMemberName("Placement");
			xamlUserType152.AddMemberName("IsBlur");
			xamlUserType152.SetIsLocalType();
			result = xamlUserType152;
			break;
		}
		case 205:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 206:
		{
			XamlUserType xamlUserType151 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType151.StaticInitializer = StaticInitializer_206_Tooltip;
			xamlUserType151.AddMemberName("TextTrimmedEnabled");
			xamlUserType151.SetIsLocalType();
			result = xamlUserType151;
			break;
		}
		case 207:
		{
			XamlUserType xamlUserType150 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.AppBarToggleButton"));
			xamlUserType150.Activator = Activate_207_CommandBarToggleButton;
			xamlUserType150.StaticInitializer = StaticInitializer_207_CommandBarToggleButton;
			xamlUserType150.AddMemberName("LabelVisibility");
			xamlUserType150.AddMemberName("IconSvgStyle");
			xamlUserType150.SetIsLocalType();
			result = xamlUserType150;
			break;
		}
		case 208:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 209:
		{
			XamlUserType xamlUserType149 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType149.Activator = Activate_209_MaxNumberCornerRadiusRoundingStrategy;
			xamlUserType149.StaticInitializer = StaticInitializer_209_MaxNumberCornerRadiusRoundingStrategy;
			xamlUserType149.SetIsLocalType();
			result = xamlUserType149;
			break;
		}
		case 210:
		{
			XamlUserType xamlUserType148 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ListFlyoutItem"));
			xamlUserType148.Activator = Activate_210_ContextMenuItem;
			xamlUserType148.StaticInitializer = StaticInitializer_210_ContextMenuItem;
			xamlUserType148.SetIsLocalType();
			result = xamlUserType148;
			break;
		}
		case 211:
		{
			XamlUserType xamlUserType147 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.MenuFlyoutItem"));
			xamlUserType147.Activator = Activate_211_ListFlyoutItem;
			xamlUserType147.StaticInitializer = StaticInitializer_211_ListFlyoutItem;
			xamlUserType147.AddMemberName("CommandBarItemOverflowable");
			xamlUserType147.AddMemberName("NotificationBadge");
			xamlUserType147.SetIsLocalType();
			result = xamlUserType147;
			break;
		}
		case 212:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 213:
		{
			XamlUserType xamlUserType146 = new XamlUserType(this, fullName, type, null);
			xamlUserType146.StaticInitializer = StaticInitializer_213_ICommandBarItemOverflowable;
			xamlUserType146.SetIsReturnTypeStub();
			xamlUserType146.SetIsLocalType();
			result = xamlUserType146;
			break;
		}
		case 214:
		{
			XamlUserType xamlUserType145 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ListFlyoutToggle"));
			xamlUserType145.Activator = Activate_214_ContextMenuToggle;
			xamlUserType145.StaticInitializer = StaticInitializer_214_ContextMenuToggle;
			xamlUserType145.SetIsLocalType();
			result = xamlUserType145;
			break;
		}
		case 215:
		{
			XamlUserType xamlUserType144 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem"));
			xamlUserType144.Activator = Activate_215_ListFlyoutToggle;
			xamlUserType144.StaticInitializer = StaticInitializer_215_ListFlyoutToggle;
			xamlUserType144.SetIsLocalType();
			result = xamlUserType144;
			break;
		}
		case 216:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 217:
		{
			XamlUserType xamlUserType143 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ListFlyoutSeparator"));
			xamlUserType143.Activator = Activate_217_ContextMenuSeparator;
			xamlUserType143.StaticInitializer = StaticInitializer_217_ContextMenuSeparator;
			xamlUserType143.SetIsLocalType();
			result = xamlUserType143;
			break;
		}
		case 218:
		{
			XamlUserType xamlUserType142 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.MenuFlyoutSeparator"));
			xamlUserType142.Activator = Activate_218_ListFlyoutSeparator;
			xamlUserType142.StaticInitializer = StaticInitializer_218_ListFlyoutSeparator;
			xamlUserType142.SetIsLocalType();
			result = xamlUserType142;
			break;
		}
		case 219:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 220:
		{
			XamlUserType xamlUserType141 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType141.Activator = Activate_220_ColorWithTransparencyConverter;
			xamlUserType141.StaticInitializer = StaticInitializer_220_ColorWithTransparencyConverter;
			xamlUserType141.SetIsLocalType();
			result = xamlUserType141;
			break;
		}
		case 221:
		{
			XamlUserType xamlUserType140 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType140.StaticInitializer = StaticInitializer_221_DatePickerSpinnerList;
			xamlUserType140.AddMemberName("Day");
			xamlUserType140.AddMemberName("Month");
			xamlUserType140.AddMemberName("Year");
			xamlUserType140.AddMemberName("EnabledEntranceAnimation");
			xamlUserType140.SetIsLocalType();
			result = xamlUserType140;
			break;
		}
		case 222:
		{
			XamlUserType xamlUserType139 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ListViewItem"));
			xamlUserType139.StaticInitializer = StaticInitializer_222_DatePickerSpinnerListItem;
			xamlUserType139.AddMemberName("TypeDate");
			xamlUserType139.AddMemberName("Value");
			xamlUserType139.AddMemberName("FormattedValue");
			xamlUserType139.SetIsLocalType();
			result = xamlUserType139;
			break;
		}
		case 223:
		{
			XamlUserType xamlUserType138 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType138.Activator = Activate_223_ScrollList;
			xamlUserType138.StaticInitializer = StaticInitializer_223_ScrollList;
			xamlUserType138.AddMemberName("SelectedTime");
			xamlUserType138.AddMemberName("TimeItemsSource");
			xamlUserType138.AddMemberName("InfiniteScroll");
			xamlUserType138.SetIsLocalType();
			result = xamlUserType138;
			break;
		}
		case 224:
		{
			XamlUserType xamlUserType137 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Collections.ObjectModel.Collection`1<Object>"));
			xamlUserType137.StaticInitializer = StaticInitializer_224_ObservableCollection;
			xamlUserType137.CollectionAdd = VectorAdd_224_ObservableCollection;
			xamlUserType137.SetIsReturnTypeStub();
			result = xamlUserType137;
			break;
		}
		case 225:
			result = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"))
			{
				Activator = Activate_225_Collection,
				StaticInitializer = StaticInitializer_225_Collection,
				CollectionAdd = VectorAdd_225_Collection
			};
			break;
		case 226:
		{
			XamlUserType xamlUserType136 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType136.StaticInitializer = StaticInitializer_226_TypeDate;
			xamlUserType136.AddEnumValue("Day", TypeDate.Day);
			xamlUserType136.AddEnumValue("Month", TypeDate.Month);
			xamlUserType136.AddEnumValue("Year", TypeDate.Year);
			xamlUserType136.SetIsLocalType();
			result = xamlUserType136;
			break;
		}
		case 227:
		{
			XamlUserType xamlUserType135 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType135.Activator = Activate_227_StringToUpperConverter;
			xamlUserType135.StaticInitializer = StaticInitializer_227_StringToUpperConverter;
			xamlUserType135.SetIsLocalType();
			result = xamlUserType135;
			break;
		}
		case 228:
		{
			XamlUserType xamlUserType134 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType134.Activator = Activate_228_DatePickerTextScaleSizeConverter;
			xamlUserType134.StaticInitializer = StaticInitializer_228_DatePickerTextScaleSizeConverter;
			xamlUserType134.SetIsLocalType();
			result = xamlUserType134;
			break;
		}
		case 229:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 230:
		{
			XamlUserType xamlUserType133 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.StyleSelector"));
			xamlUserType133.Activator = Activate_230_PeriodStyleSelector;
			xamlUserType133.StaticInitializer = StaticInitializer_230_PeriodStyleSelector;
			xamlUserType133.AddMemberName("HiddenStyle");
			xamlUserType133.AddMemberName("NormalStyle");
			xamlUserType133.SetIsLocalType();
			result = xamlUserType133;
			break;
		}
		case 231:
		{
			XamlUserType xamlUserType132 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ScrollList"));
			xamlUserType132.Activator = Activate_231_PeriodScrollList;
			xamlUserType132.StaticInitializer = StaticInitializer_231_PeriodScrollList;
			xamlUserType132.AddMemberName("VerticalOffSetAnimation");
			xamlUserType132.SetIsLocalType();
			result = xamlUserType132;
			break;
		}
		case 232:
		{
			XamlUserType xamlUserType131 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Common.DpiChangedStateTriggerBase"));
			xamlUserType131.Activator = Activate_232_DpiChangedTo175StateTrigger;
			xamlUserType131.StaticInitializer = StaticInitializer_232_DpiChangedTo175StateTrigger;
			xamlUserType131.SetIsLocalType();
			result = xamlUserType131;
			break;
		}
		case 233:
		{
			XamlUserType xamlUserType130 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.StateTriggerBase"));
			xamlUserType130.StaticInitializer = StaticInitializer_233_DpiChangedStateTriggerBase;
			xamlUserType130.AddMemberName("OsVersionExpected");
			xamlUserType130.SetIsLocalType();
			result = xamlUserType130;
			break;
		}
		case 234:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 235:
		{
			XamlUserType xamlUserType129 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType129.StaticInitializer = StaticInitializer_235_OSVersionType;
			xamlUserType129.AddEnumValue("Win10", OSVersionType.Win10);
			xamlUserType129.AddEnumValue("Win11", OSVersionType.Win11);
			xamlUserType129.AddEnumValue("Both", OSVersionType.Both);
			xamlUserType129.SetIsLocalType();
			result = xamlUserType129;
			break;
		}
		case 236:
		{
			XamlUserType xamlUserType128 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Common.DpiChangedStateTriggerBase"));
			xamlUserType128.Activator = Activate_236_DpiChangedTo125StateTrigger;
			xamlUserType128.StaticInitializer = StaticInitializer_236_DpiChangedTo125StateTrigger;
			xamlUserType128.SetIsLocalType();
			result = xamlUserType128;
			break;
		}
		case 237:
		{
			XamlUserType xamlUserType127 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ContentDialog"));
			xamlUserType127.Activator = Activate_237_ContentDialog;
			xamlUserType127.StaticInitializer = StaticInitializer_237_ContentDialog;
			xamlUserType127.AddMemberName("BackgroundDialog");
			xamlUserType127.AddMemberName("ContentMargin");
			xamlUserType127.AddMemberName("DialogMaxHeight");
			xamlUserType127.AddMemberName("DialogWidth");
			xamlUserType127.AddMemberName("DialogTitleAlignment");
			xamlUserType127.AddMemberName("IsCloseButtonEnabled");
			xamlUserType127.AddMemberName("CustomSmokedBackgroundResourceKey");
			xamlUserType127.AddMemberName("CustomAppBarMargin");
			xamlUserType127.SetIsLocalType();
			result = xamlUserType127;
			break;
		}
		case 238:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 239:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 240:
		{
			XamlUserType xamlUserType126 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType126.Activator = Activate_240_ScrollViewer;
			xamlUserType126.StaticInitializer = StaticInitializer_240_ScrollViewer;
			xamlUserType126.AddMemberName("VerticalScrollBarSpacingFromContent");
			xamlUserType126.AddMemberName("IsMaskingRoundCorner");
			xamlUserType126.AddMemberName("IsFirstScrollAnimation");
			xamlUserType126.AddMemberName("VerticalScrollBarMargin");
			xamlUserType126.AddMemberName("MaskingRoundElementReference");
			xamlUserType126.AddMemberName("AutoHideVerticalScrollBar");
			xamlUserType126.AddMemberName("AutoHideHorizontalScrollBar");
			xamlUserType126.SetIsLocalType();
			result = xamlUserType126;
			break;
		}
		case 241:
		{
			XamlUserType xamlUserType125 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.ValueType"));
			xamlUserType125.StaticInitializer = StaticInitializer_241_GridLength;
			xamlUserType125.AddMemberName("Value");
			xamlUserType125.AddMemberName("GridUnitType");
			xamlUserType125.AddMemberName("IsAbsolute");
			xamlUserType125.AddMemberName("IsAuto");
			xamlUserType125.AddMemberName("IsStar");
			result = xamlUserType125;
			break;
		}
		case 242:
		{
			XamlUserType xamlUserType124 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.Xaml.Interactivity.Behavior`1<Microsoft.UI.Xaml.DependencyObject>"));
			xamlUserType124.Activator = Activate_242_ShowVerticalScrollableIndicatorBehavior;
			xamlUserType124.StaticInitializer = StaticInitializer_242_ShowVerticalScrollableIndicatorBehavior;
			xamlUserType124.AddMemberName("BottomIndicator");
			xamlUserType124.AddMemberName("TargetScrollViewer");
			xamlUserType124.AddMemberName("TopIndicator");
			xamlUserType124.SetIsLocalType();
			result = xamlUserType124;
			break;
		}
		case 243:
		{
			XamlUserType xamlUserType123 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.Xaml.Interactivity.Behavior"));
			xamlUserType123.StaticInitializer = StaticInitializer_243_Behavior;
			xamlUserType123.AddMemberName("AssociatedObject");
			result = xamlUserType123;
			break;
		}
		case 244:
		{
			XamlUserType xamlUserType122 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType122.Activator = Activate_244_Divider;
			xamlUserType122.StaticInitializer = StaticInitializer_244_Divider;
			xamlUserType122.AddMemberName("Type");
			xamlUserType122.AddMemberName("Orientation");
			xamlUserType122.AddMemberName("HeaderText");
			xamlUserType122.SetIsLocalType();
			result = xamlUserType122;
			break;
		}
		case 245:
		{
			XamlUserType xamlUserType121 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType121.StaticInitializer = StaticInitializer_245_DividerType;
			xamlUserType121.AddEnumValue("Line", DividerType.Line);
			xamlUserType121.AddEnumValue("Dash", DividerType.Dash);
			xamlUserType121.SetIsLocalType();
			result = xamlUserType121;
			break;
		}
		case 246:
		{
			XamlUserType xamlUserType120 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ItemsControl"));
			xamlUserType120.Activator = Activate_246_DropdownList;
			xamlUserType120.StaticInitializer = StaticInitializer_246_DropdownList;
			xamlUserType120.AddMemberName("ArrowColor");
			xamlUserType120.AddMemberName("IsListEnabled");
			xamlUserType120.AddMemberName("SelectedIndex");
			xamlUserType120.AddMemberName("SelectedItem");
			xamlUserType120.AddMemberName("ItemsSource");
			xamlUserType120.AddMemberName("Header");
			xamlUserType120.AddMemberName("Placeholder");
			xamlUserType120.AddMemberName("ListTitle");
			xamlUserType120.AddMemberName("ListTitleVisibility");
			xamlUserType120.AddMemberName("AppTitleBarHeightOffset");
			xamlUserType120.AddMemberName("VerticalOffset");
			xamlUserType120.AddMemberName("HorizontalOffset");
			xamlUserType120.AddMemberName("DropdownPopupAlignment");
			xamlUserType120.AddMemberName("IsMultilineItem");
			xamlUserType120.AddMemberName("Type");
			xamlUserType120.AddMemberName("IsBlur");
			xamlUserType120.SetIsLocalType();
			result = xamlUserType120;
			break;
		}
		case 247:
		{
			XamlUserType xamlUserType119 = new XamlUserType(this, fullName, type, null);
			xamlUserType119.StaticInitializer = StaticInitializer_247_IList;
			xamlUserType119.SetIsReturnTypeStub();
			result = xamlUserType119;
			break;
		}
		case 248:
		{
			XamlUserType xamlUserType118 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType118.StaticInitializer = StaticInitializer_248_DropdownListType;
			xamlUserType118.AddEnumValue("Default", DropdownListType.Default);
			xamlUserType118.AddEnumValue("SubAppBar", DropdownListType.SubAppBar);
			xamlUserType118.SetIsLocalType();
			result = xamlUserType118;
			break;
		}
		case 249:
		{
			XamlUserType xamlUserType117 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ListView"));
			xamlUserType117.Activator = Activate_249_DropdownListViewCustom;
			xamlUserType117.StaticInitializer = StaticInitializer_249_DropdownListViewCustom;
			xamlUserType117.SetIsLocalType();
			result = xamlUserType117;
			break;
		}
		case 250:
		{
			XamlUserType xamlUserType116 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType116.Activator = Activate_250_DropdownCustomControl;
			xamlUserType116.StaticInitializer = StaticInitializer_250_DropdownCustomControl;
			xamlUserType116.SetContentPropertyName("Samsung.OneUI.WinUI.Controls.DropdownCustomControl.Content");
			xamlUserType116.AddMemberName("Content");
			xamlUserType116.AddMemberName("ArrowColor");
			xamlUserType116.AddMemberName("IsEnabled");
			xamlUserType116.SetIsLocalType();
			result = xamlUserType116;
			break;
		}
		case 251:
		{
			XamlUserType xamlUserType115 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType115.Activator = Activate_251_OpacityToVisibilityConverter;
			xamlUserType115.StaticInitializer = StaticInitializer_251_OpacityToVisibilityConverter;
			xamlUserType115.SetIsLocalType();
			result = xamlUserType115;
			break;
		}
		case 252:
		{
			XamlUserType xamlUserType114 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeView"));
			xamlUserType114.Activator = Activate_252_ExpandableList;
			xamlUserType114.StaticInitializer = StaticInitializer_252_ExpandableList;
			xamlUserType114.SetIsLocalType();
			result = xamlUserType114;
			break;
		}
		case 253:
		{
			XamlUserType xamlUserType113 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType113.Activator = Activate_253_TreeView;
			xamlUserType113.StaticInitializer = StaticInitializer_253_TreeView;
			xamlUserType113.AddMemberName("ItemContainerStyle");
			xamlUserType113.AddMemberName("SelectionMode");
			xamlUserType113.AddMemberName("CanDragItems");
			xamlUserType113.AddMemberName("CanReorderItems");
			xamlUserType113.AddMemberName("ItemContainerTransitions");
			xamlUserType113.AddMemberName("ItemContainerStyleSelector");
			xamlUserType113.AddMemberName("ItemTemplate");
			xamlUserType113.AddMemberName("ItemTemplateSelector");
			xamlUserType113.AddMemberName("ItemsSource");
			xamlUserType113.AddMemberName("RootNodes");
			xamlUserType113.AddMemberName("SelectedItem");
			xamlUserType113.AddMemberName("SelectedItems");
			xamlUserType113.AddMemberName("SelectedNode");
			xamlUserType113.AddMemberName("SelectedNodes");
			result = xamlUserType113;
			break;
		}
		case 254:
		{
			XamlUserType xamlUserType112 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType112.StaticInitializer = StaticInitializer_254_TreeViewSelectionMode;
			xamlUserType112.AddEnumValue("None", TreeViewSelectionMode.None);
			xamlUserType112.AddEnumValue("Single", TreeViewSelectionMode.Single);
			xamlUserType112.AddEnumValue("Multiple", TreeViewSelectionMode.Multiple);
			result = xamlUserType112;
			break;
		}
		case 255:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 256:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 257:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 258:
		{
			XamlUserType xamlUserType111 = new XamlUserType(this, fullName, type, null);
			xamlUserType111.StaticInitializer = StaticInitializer_258_IList;
			xamlUserType111.CollectionAdd = VectorAdd_258_IList;
			xamlUserType111.SetIsReturnTypeStub();
			result = xamlUserType111;
			break;
		}
		case 259:
		{
			XamlUserType xamlUserType110 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.DependencyObject"));
			xamlUserType110.Activator = Activate_259_TreeViewNode;
			xamlUserType110.StaticInitializer = StaticInitializer_259_TreeViewNode;
			xamlUserType110.AddMemberName("Children");
			xamlUserType110.AddMemberName("Content");
			xamlUserType110.AddMemberName("Depth");
			xamlUserType110.AddMemberName("HasChildren");
			xamlUserType110.AddMemberName("HasUnrealizedChildren");
			xamlUserType110.AddMemberName("IsExpanded");
			xamlUserType110.AddMemberName("Parent");
			xamlUserType110.SetIsBindable();
			result = xamlUserType110;
			break;
		}
		case 260:
		{
			XamlUserType xamlUserType109 = new XamlUserType(this, fullName, type, null);
			xamlUserType109.StaticInitializer = StaticInitializer_260_IList;
			xamlUserType109.CollectionAdd = VectorAdd_260_IList;
			xamlUserType109.SetIsReturnTypeStub();
			result = xamlUserType109;
			break;
		}
		case 261:
		{
			XamlUserType xamlUserType108 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewItem"));
			xamlUserType108.Activator = Activate_261_ExpandableListItemHeader;
			xamlUserType108.StaticInitializer = StaticInitializer_261_ExpandableListItemHeader;
			xamlUserType108.SetIsLocalType();
			result = xamlUserType108;
			break;
		}
		case 262:
		{
			XamlUserType xamlUserType107 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ListViewItem"));
			xamlUserType107.Activator = Activate_262_TreeViewItem;
			xamlUserType107.StaticInitializer = StaticInitializer_262_TreeViewItem;
			xamlUserType107.AddMemberName("CollapsedGlyph");
			xamlUserType107.AddMemberName("ExpandedGlyph");
			xamlUserType107.AddMemberName("GlyphBrush");
			xamlUserType107.AddMemberName("GlyphOpacity");
			xamlUserType107.AddMemberName("GlyphSize");
			xamlUserType107.AddMemberName("HasUnrealizedChildren");
			xamlUserType107.AddMemberName("IsExpanded");
			xamlUserType107.AddMemberName("ItemsSource");
			xamlUserType107.AddMemberName("TreeViewItemTemplateSettings");
			result = xamlUserType107;
			break;
		}
		case 263:
		{
			XamlUserType xamlUserType106 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.DependencyObject"));
			xamlUserType106.StaticInitializer = StaticInitializer_263_TreeViewItemTemplateSettings;
			xamlUserType106.SetIsReturnTypeStub();
			result = xamlUserType106;
			break;
		}
		case 264:
		{
			XamlUserType xamlUserType105 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Button"));
			xamlUserType105.Activator = Activate_264_ExpandButton;
			xamlUserType105.StaticInitializer = StaticInitializer_264_ExpandButton;
			xamlUserType105.AddMemberName("IsChecked");
			xamlUserType105.SetIsLocalType();
			result = xamlUserType105;
			break;
		}
		case 265:
			result = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ListView"))
			{
				Activator = Activate_265_TreeViewList,
				StaticInitializer = StaticInitializer_265_TreeViewList
			};
			break;
		case 266:
		{
			XamlUserType xamlUserType104 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Button"));
			xamlUserType104.Activator = Activate_266_FlipViewButton;
			xamlUserType104.StaticInitializer = StaticInitializer_266_FlipViewButton;
			xamlUserType104.AddMemberName("IsBlur");
			xamlUserType104.SetIsLocalType();
			result = xamlUserType104;
			break;
		}
		case 267:
		{
			XamlUserType xamlUserType103 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.FlipView"));
			xamlUserType103.Activator = Activate_267_FlipView;
			xamlUserType103.StaticInitializer = StaticInitializer_267_FlipView;
			xamlUserType103.AddMemberName("Orientation");
			xamlUserType103.AddMemberName("PreviousButtonHorizontalStyle");
			xamlUserType103.AddMemberName("NextButtonHorizontalStyle");
			xamlUserType103.AddMemberName("PreviousButtonVerticalStyle");
			xamlUserType103.AddMemberName("NextButtonVerticalStyle");
			xamlUserType103.AddMemberName("IsClickable");
			xamlUserType103.AddMemberName("IsBlurButton");
			xamlUserType103.SetIsLocalType();
			result = xamlUserType103;
			break;
		}
		case 268:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 269:
		{
			XamlUserType xamlUserType102 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.FlipViewItem"));
			xamlUserType102.StaticInitializer = StaticInitializer_269_FlipViewItem;
			xamlUserType102.SetIsLocalType();
			result = xamlUserType102;
			break;
		}
		case 270:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 271:
		{
			XamlUserType xamlUserType101 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.AppBarToggleButton"));
			xamlUserType101.Activator = Activate_271_IconToggleButton;
			xamlUserType101.StaticInitializer = StaticInitializer_271_IconToggleButton;
			xamlUserType101.AddMemberName("LabelVisibility");
			xamlUserType101.AddMemberName("IconSvgStyle");
			xamlUserType101.SetIsLocalType();
			result = xamlUserType101;
			break;
		}
		case 272:
		{
			XamlUserType xamlUserType100 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Slider"));
			xamlUserType100.Activator = Activate_272_LevelSlider;
			xamlUserType100.StaticInitializer = StaticInitializer_272_LevelSlider;
			xamlUserType100.AddMemberName("Levels");
			xamlUserType100.SetIsLocalType();
			result = xamlUserType100;
			break;
		}
		case 273:
		{
			XamlUserType xamlUserType99 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType99.Activator = Activate_273_SliderMarkerControl;
			xamlUserType99.StaticInitializer = StaticInitializer_273_SliderMarkerControl;
			xamlUserType99.SetIsLocalType();
			result = xamlUserType99;
			break;
		}
		case 274:
		{
			XamlUserType xamlUserType98 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType98.Activator = Activate_274_LevelBar;
			xamlUserType98.StaticInitializer = StaticInitializer_274_LevelBar;
			xamlUserType98.AddMemberName("Levels");
			xamlUserType98.AddMemberName("Maximum");
			xamlUserType98.AddMemberName("Minimum");
			xamlUserType98.AddMemberName("Value");
			xamlUserType98.AddMemberName("IsThumbToolTipEnabled");
			xamlUserType98.AddMemberName("ThumbToolTipValueConverter");
			xamlUserType98.SetIsLocalType();
			result = xamlUserType98;
			break;
		}
		case 275:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 276:
		{
			XamlUserType xamlUserType97 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.FrameworkElement"));
			xamlUserType97.Activator = Activate_276_ItemsRepeater;
			xamlUserType97.StaticInitializer = StaticInitializer_276_ItemsRepeater;
			xamlUserType97.SetContentPropertyName("Microsoft.UI.Xaml.Controls.ItemsRepeater.ItemTemplate");
			xamlUserType97.AddMemberName("ItemTemplate");
			xamlUserType97.AddMemberName("Layout");
			xamlUserType97.AddMemberName("Background");
			xamlUserType97.AddMemberName("HorizontalCacheLength");
			xamlUserType97.AddMemberName("ItemTransitionProvider");
			xamlUserType97.AddMemberName("ItemsSource");
			xamlUserType97.AddMemberName("ItemsSourceView");
			xamlUserType97.AddMemberName("VerticalCacheLength");
			result = xamlUserType97;
			break;
		}
		case 277:
		{
			XamlUserType xamlUserType96 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.DependencyObject"));
			xamlUserType96.StaticInitializer = StaticInitializer_277_Layout;
			xamlUserType96.AddMemberName("IndexBasedLayoutOrientation");
			result = xamlUserType96;
			break;
		}
		case 278:
		{
			XamlUserType xamlUserType95 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType95.StaticInitializer = StaticInitializer_278_ItemCollectionTransitionProvider;
			xamlUserType95.SetIsReturnTypeStub();
			result = xamlUserType95;
			break;
		}
		case 279:
		{
			XamlUserType xamlUserType94 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType94.StaticInitializer = StaticInitializer_279_ItemsSourceView;
			xamlUserType94.SetIsReturnTypeStub();
			result = xamlUserType94;
			break;
		}
		case 280:
		{
			XamlUserType xamlUserType93 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.VirtualizingLayout"));
			xamlUserType93.Activator = Activate_280_StackLayout;
			xamlUserType93.StaticInitializer = StaticInitializer_280_StackLayout;
			xamlUserType93.AddMemberName("Orientation");
			xamlUserType93.AddMemberName("Spacing");
			result = xamlUserType93;
			break;
		}
		case 281:
			result = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Layout"))
			{
				Activator = Activate_281_VirtualizingLayout,
				StaticInitializer = StaticInitializer_281_VirtualizingLayout
			};
			break;
		case 282:
		{
			XamlUserType xamlUserType92 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType92.StaticInitializer = StaticInitializer_282_IndexBasedLayoutOrientation;
			xamlUserType92.AddEnumValue("None", IndexBasedLayoutOrientation.None);
			xamlUserType92.AddEnumValue("TopToBottom", IndexBasedLayoutOrientation.TopToBottom);
			xamlUserType92.AddEnumValue("LeftToRight", IndexBasedLayoutOrientation.LeftToRight);
			result = xamlUserType92;
			break;
		}
		case 283:
		{
			XamlUserType xamlUserType91 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.VirtualizingLayout"));
			xamlUserType91.Activator = Activate_283_UniformGridLayout;
			xamlUserType91.StaticInitializer = StaticInitializer_283_UniformGridLayout;
			xamlUserType91.AddMemberName("MinColumnSpacing");
			xamlUserType91.AddMemberName("MinItemHeight");
			xamlUserType91.AddMemberName("MinItemWidth");
			xamlUserType91.AddMemberName("MinRowSpacing");
			xamlUserType91.AddMemberName("Orientation");
			xamlUserType91.AddMemberName("ItemsJustification");
			xamlUserType91.AddMemberName("ItemsStretch");
			xamlUserType91.AddMemberName("MaximumRowsOrColumns");
			result = xamlUserType91;
			break;
		}
		case 284:
		{
			XamlUserType xamlUserType90 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType90.StaticInitializer = StaticInitializer_284_UniformGridLayoutItemsJustification;
			xamlUserType90.AddEnumValue("Start", UniformGridLayoutItemsJustification.Start);
			xamlUserType90.AddEnumValue("Center", UniformGridLayoutItemsJustification.Center);
			xamlUserType90.AddEnumValue("End", UniformGridLayoutItemsJustification.End);
			xamlUserType90.AddEnumValue("SpaceAround", UniformGridLayoutItemsJustification.SpaceAround);
			xamlUserType90.AddEnumValue("SpaceBetween", UniformGridLayoutItemsJustification.SpaceBetween);
			xamlUserType90.AddEnumValue("SpaceEvenly", UniformGridLayoutItemsJustification.SpaceEvenly);
			result = xamlUserType90;
			break;
		}
		case 285:
		{
			XamlUserType xamlUserType89 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType89.StaticInitializer = StaticInitializer_285_UniformGridLayoutItemsStretch;
			xamlUserType89.AddEnumValue("None", UniformGridLayoutItemsStretch.None);
			xamlUserType89.AddEnumValue("Fill", UniformGridLayoutItemsStretch.Fill);
			xamlUserType89.AddEnumValue("Uniform", UniformGridLayoutItemsStretch.Uniform);
			result = xamlUserType89;
			break;
		}
		case 286:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 287:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 288:
		{
			XamlUserType xamlUserType88 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.SplitView"));
			xamlUserType88.Activator = Activate_288_MultiPane;
			xamlUserType88.StaticInitializer = StaticInitializer_288_MultiPane;
			xamlUserType88.SetIsLocalType();
			result = xamlUserType88;
			break;
		}
		case 289:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 290:
		{
			XamlUserType xamlUserType87 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType87.Activator = Activate_290_SplitBar;
			xamlUserType87.StaticInitializer = StaticInitializer_290_SplitBar;
			xamlUserType87.AddMemberName("Element");
			xamlUserType87.AddMemberName("ResizeDirection");
			xamlUserType87.AddMemberName("ResizeBehavior");
			xamlUserType87.AddMemberName("GripperForeground");
			xamlUserType87.AddMemberName("ParentLevel");
			xamlUserType87.AddMemberName("GripperCursor");
			xamlUserType87.AddMemberName("GripperCustomCursorResource");
			xamlUserType87.AddMemberName("CursorBehavior");
			xamlUserType87.SetIsLocalType();
			result = xamlUserType87;
			break;
		}
		case 291:
		{
			XamlUserType xamlUserType86 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType86.StaticInitializer = StaticInitializer_291_GridResizeDirection;
			xamlUserType86.AddEnumValue("Auto", SplitBar.GridResizeDirection.Auto);
			xamlUserType86.AddEnumValue("Columns", SplitBar.GridResizeDirection.Columns);
			xamlUserType86.AddEnumValue("Rows", SplitBar.GridResizeDirection.Rows);
			xamlUserType86.SetIsLocalType();
			result = xamlUserType86;
			break;
		}
		case 292:
		{
			XamlUserType xamlUserType85 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType85.StaticInitializer = StaticInitializer_292_GridResizeBehavior;
			xamlUserType85.AddEnumValue("BasedOnAlignment", SplitBar.GridResizeBehavior.BasedOnAlignment);
			xamlUserType85.AddEnumValue("CurrentAndNext", SplitBar.GridResizeBehavior.CurrentAndNext);
			xamlUserType85.AddEnumValue("PreviousAndCurrent", SplitBar.GridResizeBehavior.PreviousAndCurrent);
			xamlUserType85.AddEnumValue("PreviousAndNext", SplitBar.GridResizeBehavior.PreviousAndNext);
			xamlUserType85.SetIsLocalType();
			result = xamlUserType85;
			break;
		}
		case 293:
		{
			XamlUserType xamlUserType84 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType84.StaticInitializer = StaticInitializer_293_GripperCursorType;
			xamlUserType84.AddEnumValue("Default", SplitBar.GripperCursorType.Default);
			xamlUserType84.AddEnumValue("Arrow", SplitBar.GripperCursorType.Arrow);
			xamlUserType84.AddEnumValue("Cross", SplitBar.GripperCursorType.Cross);
			xamlUserType84.AddEnumValue("Custom", SplitBar.GripperCursorType.Custom);
			xamlUserType84.AddEnumValue("Hand", SplitBar.GripperCursorType.Hand);
			xamlUserType84.AddEnumValue("Help", SplitBar.GripperCursorType.Help);
			xamlUserType84.AddEnumValue("IBeam", SplitBar.GripperCursorType.IBeam);
			xamlUserType84.AddEnumValue("SizeAll", SplitBar.GripperCursorType.SizeAll);
			xamlUserType84.AddEnumValue("SizeNortheastSouthwest", SplitBar.GripperCursorType.SizeNortheastSouthwest);
			xamlUserType84.AddEnumValue("SizeNorthSouth", SplitBar.GripperCursorType.SizeNorthSouth);
			xamlUserType84.AddEnumValue("SizeNorthwestSoutheast", SplitBar.GripperCursorType.SizeNorthwestSoutheast);
			xamlUserType84.AddEnumValue("SizeWestEast", SplitBar.GripperCursorType.SizeWestEast);
			xamlUserType84.AddEnumValue("UniversalNo", SplitBar.GripperCursorType.UniversalNo);
			xamlUserType84.AddEnumValue("UpArrow", SplitBar.GripperCursorType.UpArrow);
			xamlUserType84.AddEnumValue("Wait", SplitBar.GripperCursorType.Wait);
			xamlUserType84.SetIsLocalType();
			result = xamlUserType84;
			break;
		}
		case 294:
		{
			XamlUserType xamlUserType83 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType83.StaticInitializer = StaticInitializer_294_SplitterCursorBehavior;
			xamlUserType83.AddEnumValue("ChangeOnSplitterHover", SplitBar.SplitterCursorBehavior.ChangeOnSplitterHover);
			xamlUserType83.AddEnumValue("ChangeOnGripperHover", SplitBar.SplitterCursorBehavior.ChangeOnGripperHover);
			xamlUserType83.SetIsLocalType();
			result = xamlUserType83;
			break;
		}
		case 295:
		{
			XamlUserType xamlUserType82 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView"));
			xamlUserType82.Activator = Activate_295_NavigationRail;
			xamlUserType82.StaticInitializer = StaticInitializer_295_NavigationRail;
			xamlUserType82.AddMemberName("IsPaneAutoCompactEnabled");
			xamlUserType82.AddMemberName("IsInitialPaneOpen");
			xamlUserType82.AddMemberName("PaneToggleNotificationBadge");
			xamlUserType82.AddMemberName("SettingsNavigationItemNotificationBadge");
			xamlUserType82.AddMemberName("CollapseBreakPoint");
			xamlUserType82.AddMemberName("ExpandBreakPoint");
			xamlUserType82.SetIsLocalType();
			result = xamlUserType82;
			break;
		}
		case 296:
		{
			XamlUserType xamlUserType81 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ContentControl"));
			xamlUserType81.Activator = Activate_296_NavigationView;
			xamlUserType81.StaticInitializer = StaticInitializer_296_NavigationView;
			xamlUserType81.AddMemberName("PaneToggleButtonStyle");
			xamlUserType81.AddMemberName("OpenPaneLength");
			xamlUserType81.AddMemberName("CompactPaneLength");
			xamlUserType81.AddMemberName("AlwaysShowHeader");
			xamlUserType81.AddMemberName("AutoSuggestBox");
			xamlUserType81.AddMemberName("CompactModeThresholdWidth");
			xamlUserType81.AddMemberName("ContentOverlay");
			xamlUserType81.AddMemberName("DisplayMode");
			xamlUserType81.AddMemberName("ExpandedModeThresholdWidth");
			xamlUserType81.AddMemberName("FooterMenuItems");
			xamlUserType81.AddMemberName("FooterMenuItemsSource");
			xamlUserType81.AddMemberName("Header");
			xamlUserType81.AddMemberName("HeaderTemplate");
			xamlUserType81.AddMemberName("IsBackButtonVisible");
			xamlUserType81.AddMemberName("IsBackEnabled");
			xamlUserType81.AddMemberName("IsPaneOpen");
			xamlUserType81.AddMemberName("IsPaneToggleButtonVisible");
			xamlUserType81.AddMemberName("IsPaneVisible");
			xamlUserType81.AddMemberName("IsSettingsVisible");
			xamlUserType81.AddMemberName("IsTitleBarAutoPaddingEnabled");
			xamlUserType81.AddMemberName("MenuItemContainerStyle");
			xamlUserType81.AddMemberName("MenuItemContainerStyleSelector");
			xamlUserType81.AddMemberName("MenuItemTemplate");
			xamlUserType81.AddMemberName("MenuItemTemplateSelector");
			xamlUserType81.AddMemberName("MenuItems");
			xamlUserType81.AddMemberName("MenuItemsSource");
			xamlUserType81.AddMemberName("OverflowLabelMode");
			xamlUserType81.AddMemberName("PaneCustomContent");
			xamlUserType81.AddMemberName("PaneDisplayMode");
			xamlUserType81.AddMemberName("PaneFooter");
			xamlUserType81.AddMemberName("PaneHeader");
			xamlUserType81.AddMemberName("PaneTitle");
			xamlUserType81.AddMemberName("SelectedItem");
			xamlUserType81.AddMemberName("SelectionFollowsFocus");
			xamlUserType81.AddMemberName("SettingsItem");
			xamlUserType81.AddMemberName("ShoulderNavigationEnabled");
			xamlUserType81.AddMemberName("TemplateSettings");
			result = xamlUserType81;
			break;
		}
		case 297:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 298:
		{
			XamlUserType xamlUserType80 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType80.StaticInitializer = StaticInitializer_298_NavigationViewDisplayMode;
			xamlUserType80.AddEnumValue("Minimal", NavigationViewDisplayMode.Minimal);
			xamlUserType80.AddEnumValue("Compact", NavigationViewDisplayMode.Compact);
			xamlUserType80.AddEnumValue("Expanded", NavigationViewDisplayMode.Expanded);
			result = xamlUserType80;
			break;
		}
		case 299:
		{
			XamlUserType xamlUserType79 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType79.StaticInitializer = StaticInitializer_299_NavigationViewBackButtonVisible;
			xamlUserType79.AddEnumValue("Collapsed", NavigationViewBackButtonVisible.Collapsed);
			xamlUserType79.AddEnumValue("Visible", NavigationViewBackButtonVisible.Visible);
			xamlUserType79.AddEnumValue("Auto", NavigationViewBackButtonVisible.Auto);
			result = xamlUserType79;
			break;
		}
		case 300:
		{
			XamlUserType xamlUserType78 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType78.StaticInitializer = StaticInitializer_300_NavigationViewOverflowLabelMode;
			xamlUserType78.AddEnumValue("MoreLabel", NavigationViewOverflowLabelMode.MoreLabel);
			xamlUserType78.AddEnumValue("NoLabel", NavigationViewOverflowLabelMode.NoLabel);
			result = xamlUserType78;
			break;
		}
		case 301:
		{
			XamlUserType xamlUserType77 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType77.StaticInitializer = StaticInitializer_301_NavigationViewPaneDisplayMode;
			xamlUserType77.AddEnumValue("Auto", NavigationViewPaneDisplayMode.Auto);
			xamlUserType77.AddEnumValue("Left", NavigationViewPaneDisplayMode.Left);
			xamlUserType77.AddEnumValue("Top", NavigationViewPaneDisplayMode.Top);
			xamlUserType77.AddEnumValue("LeftCompact", NavigationViewPaneDisplayMode.LeftCompact);
			xamlUserType77.AddEnumValue("LeftMinimal", NavigationViewPaneDisplayMode.LeftMinimal);
			result = xamlUserType77;
			break;
		}
		case 302:
		{
			XamlUserType xamlUserType76 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType76.StaticInitializer = StaticInitializer_302_NavigationViewSelectionFollowsFocus;
			xamlUserType76.AddEnumValue("Disabled", NavigationViewSelectionFollowsFocus.Disabled);
			xamlUserType76.AddEnumValue("Enabled", NavigationViewSelectionFollowsFocus.Enabled);
			result = xamlUserType76;
			break;
		}
		case 303:
		{
			XamlUserType xamlUserType75 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType75.StaticInitializer = StaticInitializer_303_NavigationViewShoulderNavigationEnabled;
			xamlUserType75.AddEnumValue("WhenSelectionFollowsFocus", NavigationViewShoulderNavigationEnabled.WhenSelectionFollowsFocus);
			xamlUserType75.AddEnumValue("Always", NavigationViewShoulderNavigationEnabled.Always);
			xamlUserType75.AddEnumValue("Never", NavigationViewShoulderNavigationEnabled.Never);
			result = xamlUserType75;
			break;
		}
		case 304:
		{
			XamlUserType xamlUserType74 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.DependencyObject"));
			xamlUserType74.StaticInitializer = StaticInitializer_304_NavigationViewTemplateSettings;
			xamlUserType74.SetIsReturnTypeStub();
			result = xamlUserType74;
			break;
		}
		case 305:
		{
			XamlUserType xamlUserType73 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationViewItem"));
			xamlUserType73.Activator = Activate_305_NavigationRailItem;
			xamlUserType73.StaticInitializer = StaticInitializer_305_NavigationRailItem;
			xamlUserType73.AddMemberName("NotificationBadge");
			xamlUserType73.AddMemberName("SvgIconStyle");
			xamlUserType73.AddMemberName("PngIconPath");
			xamlUserType73.SetIsLocalType();
			result = xamlUserType73;
			break;
		}
		case 306:
		{
			XamlUserType xamlUserType72 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationViewItemBase"));
			xamlUserType72.Activator = Activate_306_NavigationViewItem;
			xamlUserType72.StaticInitializer = StaticInitializer_306_NavigationViewItem;
			xamlUserType72.AddMemberName("CompactPaneLength");
			xamlUserType72.AddMemberName("HasUnrealizedChildren");
			xamlUserType72.AddMemberName("Icon");
			xamlUserType72.AddMemberName("InfoBadge");
			xamlUserType72.AddMemberName("IsChildSelected");
			xamlUserType72.AddMemberName("IsExpanded");
			xamlUserType72.AddMemberName("MenuItems");
			xamlUserType72.AddMemberName("MenuItemsSource");
			xamlUserType72.AddMemberName("SelectsOnInvoked");
			result = xamlUserType72;
			break;
		}
		case 307:
		{
			XamlUserType xamlUserType71 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ContentControl"));
			xamlUserType71.StaticInitializer = StaticInitializer_307_NavigationViewItemBase;
			xamlUserType71.AddMemberName("IsSelected");
			result = xamlUserType71;
			break;
		}
		case 308:
		{
			XamlUserType xamlUserType70 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType70.StaticInitializer = StaticInitializer_308_InfoBadge;
			xamlUserType70.SetIsReturnTypeStub();
			result = xamlUserType70;
			break;
		}
		case 309:
		{
			XamlUserType xamlUserType69 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationViewItemHeader"));
			xamlUserType69.Activator = Activate_309_NavigationRailItemHeader;
			xamlUserType69.StaticInitializer = StaticInitializer_309_NavigationRailItemHeader;
			xamlUserType69.SetIsLocalType();
			result = xamlUserType69;
			break;
		}
		case 310:
			result = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationViewItemBase"))
			{
				Activator = Activate_310_NavigationViewItemHeader,
				StaticInitializer = StaticInitializer_310_NavigationViewItemHeader
			};
			break;
		case 311:
		{
			XamlUserType xamlUserType68 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationViewItemSeparator"));
			xamlUserType68.Activator = Activate_311_NavigationRailItemSeparator;
			xamlUserType68.StaticInitializer = StaticInitializer_311_NavigationRailItemSeparator;
			xamlUserType68.SetIsLocalType();
			result = xamlUserType68;
			break;
		}
		case 312:
			result = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationViewItemBase"))
			{
				Activator = Activate_312_NavigationViewItemSeparator,
				StaticInitializer = StaticInitializer_312_NavigationViewItemSeparator
			};
			break;
		case 313:
		{
			XamlUserType xamlUserType67 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.NavigationViewItemPresenter"));
			xamlUserType67.Activator = Activate_313_NavigationRailItemPresenter;
			xamlUserType67.StaticInitializer = StaticInitializer_313_NavigationRailItemPresenter;
			xamlUserType67.AddMemberName("NotificationBadge");
			xamlUserType67.AddMemberName("PngIconPath");
			xamlUserType67.AddMemberName("SvgIconStyle");
			xamlUserType67.SetIsLocalType();
			result = xamlUserType67;
			break;
		}
		case 314:
		{
			XamlUserType xamlUserType66 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ContentControl"));
			xamlUserType66.Activator = Activate_314_NavigationViewItemPresenter;
			xamlUserType66.StaticInitializer = StaticInitializer_314_NavigationViewItemPresenter;
			xamlUserType66.AddMemberName("Icon");
			xamlUserType66.AddMemberName("InfoBadge");
			xamlUserType66.AddMemberName("TemplateSettings");
			result = xamlUserType66;
			break;
		}
		case 315:
		{
			XamlUserType xamlUserType65 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.DependencyObject"));
			xamlUserType65.StaticInitializer = StaticInitializer_315_NavigationViewItemPresenterTemplateSettings;
			xamlUserType65.SetIsReturnTypeStub();
			result = xamlUserType65;
			break;
		}
		case 316:
		{
			XamlUserType xamlUserType64 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.IconElement"));
			xamlUserType64.Activator = Activate_316_AnimatedIcon;
			xamlUserType64.StaticInitializer = StaticInitializer_316_AnimatedIcon;
			xamlUserType64.SetContentPropertyName("Microsoft.UI.Xaml.Controls.AnimatedIcon.Source");
			xamlUserType64.AddMemberName("Source");
			xamlUserType64.AddMemberName("FallbackIconSource");
			xamlUserType64.AddMemberName("MirroredWhenRightToLeft");
			xamlUserType64.AddMemberName("State");
			result = xamlUserType64;
			break;
		}
		case 317:
		{
			XamlUserType xamlUserType63 = new XamlUserType(this, fullName, type, null);
			xamlUserType63.StaticInitializer = StaticInitializer_317_IAnimatedVisualSource2;
			xamlUserType63.SetIsReturnTypeStub();
			result = xamlUserType63;
			break;
		}
		case 318:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 319:
		{
			XamlUserType xamlUserType62 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType62.Activator = Activate_319_AnimatedChevronUpDownSmallVisualSource;
			xamlUserType62.StaticInitializer = StaticInitializer_319_AnimatedChevronUpDownSmallVisualSource;
			xamlUserType62.AddMemberName("Markers");
			result = xamlUserType62;
			break;
		}
		case 320:
		{
			XamlUserType xamlUserType61 = new XamlUserType(this, fullName, type, null);
			xamlUserType61.StaticInitializer = StaticInitializer_320_IReadOnlyDictionary;
			xamlUserType61.SetIsReturnTypeStub();
			result = xamlUserType61;
			break;
		}
		case 321:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 322:
		{
			XamlUserType xamlUserType60 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.FrameworkElement"));
			xamlUserType60.Activator = Activate_322_ItemsRepeaterScrollHost;
			xamlUserType60.StaticInitializer = StaticInitializer_322_ItemsRepeaterScrollHost;
			xamlUserType60.SetContentPropertyName("Microsoft.UI.Xaml.Controls.ItemsRepeaterScrollHost.ScrollViewer");
			xamlUserType60.AddMemberName("ScrollViewer");
			xamlUserType60.AddMemberName("CurrentAnchor");
			xamlUserType60.AddMemberName("HorizontalAnchorRatio");
			xamlUserType60.AddMemberName("VerticalAnchorRatio");
			result = xamlUserType60;
			break;
		}
		case 323:
		{
			XamlUserType xamlUserType59 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView"));
			xamlUserType59.Activator = Activate_323_NavigationView;
			xamlUserType59.StaticInitializer = StaticInitializer_323_NavigationView;
			xamlUserType59.AddMemberName("IsSettingsVisible");
			xamlUserType59.AddMemberName("IsPaneAutoCompactEnabled");
			xamlUserType59.AddMemberName("IsInitialPaneOpen");
			xamlUserType59.AddMemberName("PaneToggleNotificationBadge");
			xamlUserType59.AddMemberName("SettingsNavigationItemNotificationBadge");
			xamlUserType59.AddMemberName("CollapseBreakPoint");
			xamlUserType59.AddMemberName("ExpandBreakPoint");
			xamlUserType59.AddMemberName("CompactModeThresholdWidth");
			xamlUserType59.AddMemberName("ExpandedModeThresholdWidth");
			xamlUserType59.SetIsLocalType();
			result = xamlUserType59;
			break;
		}
		case 324:
		{
			XamlUserType xamlUserType58 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationViewItem"));
			xamlUserType58.Activator = Activate_324_NavigationViewItem;
			xamlUserType58.StaticInitializer = StaticInitializer_324_NavigationViewItem;
			xamlUserType58.AddMemberName("SvgIconStyle");
			xamlUserType58.AddMemberName("PngIconPath");
			xamlUserType58.AddMemberName("NotificationBadge");
			xamlUserType58.SetIsLocalType();
			result = xamlUserType58;
			break;
		}
		case 325:
		{
			XamlUserType xamlUserType57 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationViewItemHeader"));
			xamlUserType57.Activator = Activate_325_NavigationViewItemHeader;
			xamlUserType57.StaticInitializer = StaticInitializer_325_NavigationViewItemHeader;
			xamlUserType57.SetIsLocalType();
			result = xamlUserType57;
			break;
		}
		case 326:
		{
			XamlUserType xamlUserType56 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationViewItemSeparator"));
			xamlUserType56.Activator = Activate_326_NavigationViewItemSeparator;
			xamlUserType56.StaticInitializer = StaticInitializer_326_NavigationViewItemSeparator;
			xamlUserType56.SetIsLocalType();
			result = xamlUserType56;
			break;
		}
		case 327:
		{
			XamlUserType xamlUserType55 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.NavigationViewItemPresenter"));
			xamlUserType55.Activator = Activate_327_NavigationViewItemPresenter;
			xamlUserType55.StaticInitializer = StaticInitializer_327_NavigationViewItemPresenter;
			xamlUserType55.AddMemberName("NotificationBadge");
			xamlUserType55.AddMemberName("PngIconPath");
			xamlUserType55.AddMemberName("SvgIconStyle");
			xamlUserType55.SetIsLocalType();
			result = xamlUserType55;
			break;
		}
		case 328:
		{
			XamlUserType xamlUserType54 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType54.Activator = Activate_328_PageIndicator;
			xamlUserType54.StaticInitializer = StaticInitializer_328_PageIndicator;
			xamlUserType54.AddMemberName("AutoPlayInterval");
			xamlUserType54.AddMemberName("NumberOfPages");
			xamlUserType54.AddMemberName("SelectedPageIndex");
			xamlUserType54.AddMemberName("MaxVisiblePips");
			xamlUserType54.AddMemberName("PreviousButtonVisibility");
			xamlUserType54.AddMemberName("NextButtonVisibility");
			xamlUserType54.AddMemberName("PlayPauseButtonVisibility");
			xamlUserType54.AddMemberName("IsClickActionEnable");
			xamlUserType54.AddMemberName("IsLooping");
			xamlUserType54.SetIsLocalType();
			result = xamlUserType54;
			break;
		}
		case 329:
		{
			XamlUserType xamlUserType53 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.FlyoutPresenter"));
			xamlUserType53.Activator = Activate_329_PopOverPresenter;
			xamlUserType53.StaticInitializer = StaticInitializer_329_PopOverPresenter;
			xamlUserType53.SetIsLocalType();
			result = xamlUserType53;
			break;
		}
		case 330:
		{
			XamlUserType xamlUserType52 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ProgressBar"));
			xamlUserType52.Activator = Activate_330_ProgressBar;
			xamlUserType52.StaticInitializer = StaticInitializer_330_ProgressBar;
			xamlUserType52.AddMemberName("Text");
			xamlUserType52.SetIsLocalType();
			result = xamlUserType52;
			break;
		}
		case 331:
		{
			XamlUserType xamlUserType51 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.RangeBase"));
			xamlUserType51.Activator = Activate_331_ProgressBar;
			xamlUserType51.StaticInitializer = StaticInitializer_331_ProgressBar;
			xamlUserType51.AddMemberName("IsIndeterminate");
			xamlUserType51.AddMemberName("ShowError");
			xamlUserType51.AddMemberName("ShowPaused");
			xamlUserType51.AddMemberName("TemplateSettings");
			result = xamlUserType51;
			break;
		}
		case 332:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 333:
		{
			XamlUserType xamlUserType50 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.DependencyObject"));
			xamlUserType50.StaticInitializer = StaticInitializer_333_ProgressBarTemplateSettings;
			xamlUserType50.SetIsReturnTypeStub();
			result = xamlUserType50;
			break;
		}
		case 334:
		{
			XamlUserType xamlUserType49 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ProgressCircle"));
			xamlUserType49.Activator = Activate_334_ProgressCircleDeterminate;
			xamlUserType49.StaticInitializer = StaticInitializer_334_ProgressCircleDeterminate;
			xamlUserType49.AddMemberName("Foreground");
			xamlUserType49.AddMemberName("Background");
			xamlUserType49.AddMemberName("Value");
			xamlUserType49.AddMemberName("Type");
			xamlUserType49.SetIsLocalType();
			result = xamlUserType49;
			break;
		}
		case 335:
		{
			XamlUserType xamlUserType48 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType48.StaticInitializer = StaticInitializer_335_ProgressCircleDeterminateType;
			xamlUserType48.AddEnumValue("Determinate1", ProgressCircleDeterminateType.Determinate1);
			xamlUserType48.AddEnumValue("Determinate2", ProgressCircleDeterminateType.Determinate2);
			xamlUserType48.SetIsLocalType();
			result = xamlUserType48;
			break;
		}
		case 336:
		{
			XamlUserType xamlUserType47 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.RadioButtons"));
			xamlUserType47.Activator = Activate_336_RadioButtons;
			xamlUserType47.StaticInitializer = StaticInitializer_336_RadioButtons;
			xamlUserType47.SetContentPropertyName("Microsoft.UI.Xaml.Controls.RadioButtons.Items");
			xamlUserType47.SetIsLocalType();
			result = xamlUserType47;
			break;
		}
		case 337:
		{
			XamlUserType xamlUserType46 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType46.Activator = Activate_337_RadioButtons;
			xamlUserType46.StaticInitializer = StaticInitializer_337_RadioButtons;
			xamlUserType46.SetContentPropertyName("Microsoft.UI.Xaml.Controls.RadioButtons.Items");
			xamlUserType46.AddMemberName("Items");
			xamlUserType46.AddMemberName("Header");
			xamlUserType46.AddMemberName("HeaderTemplate");
			xamlUserType46.AddMemberName("ItemTemplate");
			xamlUserType46.AddMemberName("ItemsSource");
			xamlUserType46.AddMemberName("MaxColumns");
			xamlUserType46.AddMemberName("SelectedIndex");
			xamlUserType46.AddMemberName("SelectedItem");
			result = xamlUserType46;
			break;
		}
		case 338:
		{
			XamlUserType xamlUserType45 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.RadioButton"));
			xamlUserType45.Activator = Activate_338_RadioButton;
			xamlUserType45.StaticInitializer = StaticInitializer_338_RadioButton;
			xamlUserType45.SetIsLocalType();
			result = xamlUserType45;
			break;
		}
		case 339:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 340:
		{
			XamlUserType xamlUserType44 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType44.StaticInitializer = StaticInitializer_340_GridUnitType;
			xamlUserType44.AddEnumValue("Auto", GridUnitType.Auto);
			xamlUserType44.AddEnumValue("Pixel", GridUnitType.Pixel);
			xamlUserType44.AddEnumValue("Star", GridUnitType.Star);
			result = xamlUserType44;
			break;
		}
		case 341:
		{
			XamlUserType xamlUserType43 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType43.Activator = Activate_341_ScrollModeToBoolConverter;
			xamlUserType43.StaticInitializer = StaticInitializer_341_ScrollModeToBoolConverter;
			xamlUserType43.SetIsLocalType();
			result = xamlUserType43;
			break;
		}
		case 342:
		{
			XamlUserType xamlUserType42 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType42.Activator = Activate_342_DoubleToThicknessTopAndBottomConverter;
			xamlUserType42.StaticInitializer = StaticInitializer_342_DoubleToThicknessTopAndBottomConverter;
			xamlUserType42.SetIsLocalType();
			result = xamlUserType42;
			break;
		}
		case 343:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 344:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 345:
		{
			XamlUserType xamlUserType41 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.Xaml.Interactivity.Behavior`1<Microsoft.UI.Xaml.Controls.Primitives.Thumb>"));
			xamlUserType41.Activator = Activate_345_ThumbDisabledScrollBarDimensionsBehavior;
			xamlUserType41.StaticInitializer = StaticInitializer_345_ThumbDisabledScrollBarDimensionsBehavior;
			xamlUserType41.AddMemberName("LargeRepeatButton");
			xamlUserType41.AddMemberName("ScrollBarReference");
			xamlUserType41.AddMemberName("SmallRepeatButton");
			xamlUserType41.SetIsLocalType();
			result = xamlUserType41;
			break;
		}
		case 346:
		{
			XamlUserType xamlUserType40 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.Xaml.Interactivity.Behavior"));
			xamlUserType40.StaticInitializer = StaticInitializer_346_Behavior;
			xamlUserType40.AddMemberName("AssociatedObject");
			result = xamlUserType40;
			break;
		}
		case 347:
		{
			XamlUserType xamlUserType39 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.Xaml.Interactivity.Trigger"));
			xamlUserType39.Activator = Activate_347_DataTriggerBehavior;
			xamlUserType39.StaticInitializer = StaticInitializer_347_DataTriggerBehavior;
			xamlUserType39.SetContentPropertyName("Microsoft.Xaml.Interactivity.Trigger.Actions");
			xamlUserType39.AddMemberName("Binding");
			xamlUserType39.AddMemberName("Value");
			xamlUserType39.AddMemberName("ComparisonCondition");
			result = xamlUserType39;
			break;
		}
		case 348:
		{
			XamlUserType xamlUserType38 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.Xaml.Interactivity.Behavior"));
			xamlUserType38.StaticInitializer = StaticInitializer_348_Trigger;
			xamlUserType38.SetContentPropertyName("Microsoft.Xaml.Interactivity.Trigger.Actions");
			xamlUserType38.AddMemberName("Actions");
			result = xamlUserType38;
			break;
		}
		case 349:
		{
			XamlUserType xamlUserType37 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.DependencyObjectCollection"));
			xamlUserType37.StaticInitializer = StaticInitializer_349_ActionCollection;
			xamlUserType37.CollectionAdd = VectorAdd_349_ActionCollection;
			xamlUserType37.SetIsReturnTypeStub();
			result = xamlUserType37;
			break;
		}
		case 350:
		{
			XamlUserType xamlUserType36 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType36.StaticInitializer = StaticInitializer_350_ComparisonConditionType;
			xamlUserType36.AddEnumValue("Equal", ComparisonConditionType.Equal);
			xamlUserType36.AddEnumValue("NotEqual", ComparisonConditionType.NotEqual);
			xamlUserType36.AddEnumValue("LessThan", ComparisonConditionType.LessThan);
			xamlUserType36.AddEnumValue("LessThanOrEqual", ComparisonConditionType.LessThanOrEqual);
			xamlUserType36.AddEnumValue("GreaterThan", ComparisonConditionType.GreaterThan);
			xamlUserType36.AddEnumValue("GreaterThanOrEqual", ComparisonConditionType.GreaterThanOrEqual);
			result = xamlUserType36;
			break;
		}
		case 351:
		{
			XamlUserType xamlUserType35 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.DependencyObject"));
			xamlUserType35.Activator = Activate_351_GoToStateAction;
			xamlUserType35.StaticInitializer = StaticInitializer_351_GoToStateAction;
			xamlUserType35.AddMemberName("StateName");
			xamlUserType35.AddMemberName("TargetObject");
			xamlUserType35.AddMemberName("UseTransitions");
			result = xamlUserType35;
			break;
		}
		case 352:
		{
			XamlUserType xamlUserType34 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.StateTriggerBase"));
			xamlUserType34.Activator = Activate_352_ThumbCompositeTransformScaleStateTrigger;
			xamlUserType34.StaticInitializer = StaticInitializer_352_ThumbCompositeTransformScaleStateTrigger;
			xamlUserType34.AddMemberName("ThumbReference");
			xamlUserType34.SetIsLocalType();
			result = xamlUserType34;
			break;
		}
		case 353:
		{
			XamlUserType xamlUserType33 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Button"));
			xamlUserType33.Activator = Activate_353_SearchPopupListFooterButton;
			xamlUserType33.StaticInitializer = StaticInitializer_353_SearchPopupListFooterButton;
			xamlUserType33.SetIsLocalType();
			result = xamlUserType33;
			break;
		}
		case 354:
		{
			XamlUserType xamlUserType32 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ListView"));
			xamlUserType32.Activator = Activate_354_SearchPopupList;
			xamlUserType32.StaticInitializer = StaticInitializer_354_SearchPopupList;
			xamlUserType32.AddMemberName("HighlightSearchedWords");
			xamlUserType32.AddMemberName("TextFilter");
			xamlUserType32.AddMemberName("PopupItems");
			xamlUserType32.AddMemberName("IsCornerRadiusAutoAdjustmentEnabled");
			xamlUserType32.SetIsLocalType();
			result = xamlUserType32;
			break;
		}
		case 355:
		{
			XamlUserType xamlUserType31 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Collections.ObjectModel.Collection`1<Samsung.OneUI.WinUI.Controls.SearchPopupListItem>"));
			xamlUserType31.StaticInitializer = StaticInitializer_355_ObservableCollection;
			xamlUserType31.CollectionAdd = VectorAdd_355_ObservableCollection;
			xamlUserType31.SetIsReturnTypeStub();
			result = xamlUserType31;
			break;
		}
		case 356:
			result = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"))
			{
				Activator = Activate_356_Collection,
				StaticInitializer = StaticInitializer_356_Collection,
				CollectionAdd = VectorAdd_356_Collection
			};
			break;
		case 357:
		{
			XamlUserType xamlUserType30 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ListViewItem"));
			xamlUserType30.Activator = Activate_357_SearchPopupListItem;
			xamlUserType30.StaticInitializer = StaticInitializer_357_SearchPopupListItem;
			xamlUserType30.AddMemberName("RemoveButtonTooltipMargin");
			xamlUserType30.AddMemberName("RemoveButtonTooltipVerticalOffset");
			xamlUserType30.AddMemberName("RemoveButtonTooltipContent");
			xamlUserType30.AddMemberName("Id");
			xamlUserType30.AddMemberName("RemoveButtonVisibility");
			xamlUserType30.AddMemberName("Text");
			xamlUserType30.AddMemberName("Image");
			xamlUserType30.AddMemberName("IconSvgStyle");
			xamlUserType30.SetIsLocalType();
			result = xamlUserType30;
			break;
		}
		case 358:
		{
			XamlUserType xamlUserType29 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType29.Activator = Activate_358_FilterTextBlock;
			xamlUserType29.StaticInitializer = StaticInitializer_358_FilterTextBlock;
			xamlUserType29.AddMemberName("CustomText");
			xamlUserType29.AddMemberName("TextHighlightBackgroundColor");
			xamlUserType29.AddMemberName("TextHighlightForegroundColor");
			xamlUserType29.AddMemberName("TextTrimming");
			xamlUserType29.AddMemberName("SearchedText");
			xamlUserType29.AddMemberName("ForceApplyTemplate");
			xamlUserType29.SetIsLocalType();
			result = xamlUserType29;
			break;
		}
		case 359:
		{
			XamlUserType xamlUserType28 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType28.Activator = Activate_359_SearchPopup;
			xamlUserType28.StaticInitializer = StaticInitializer_359_SearchPopup;
			xamlUserType28.AddMemberName("VerticalOffset");
			xamlUserType28.AddMemberName("HorizontalOffset");
			xamlUserType28.AddMemberName("PopupContent");
			xamlUserType28.AddMemberName("AttachTo");
			xamlUserType28.SetIsLocalType();
			result = xamlUserType28;
			break;
		}
		case 360:
		{
			XamlUserType xamlUserType27 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Button"));
			xamlUserType27.Activator = Activate_360_SearchPopupRemoveButton;
			xamlUserType27.StaticInitializer = StaticInitializer_360_SearchPopupRemoveButton;
			xamlUserType27.SetIsLocalType();
			result = xamlUserType27;
			break;
		}
		case 361:
		{
			XamlUserType xamlUserType26 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TextBox"));
			xamlUserType26.Activator = Activate_361_SearchPopupTextBox;
			xamlUserType26.StaticInitializer = StaticInitializer_361_SearchPopupTextBox;
			xamlUserType26.SetIsLocalType();
			result = xamlUserType26;
			break;
		}
		case 362:
		{
			XamlUserType xamlUserType25 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType25.Activator = Activate_362_BackdropBlurExtension;
			xamlUserType25.StaticInitializer = StaticInitializer_362_BackdropBlurExtension;
			xamlUserType25.AddMemberName("BlurAmount");
			xamlUserType25.AddMemberName("IsEnabled");
			xamlUserType25.SetIsLocalType();
			result = xamlUserType25;
			break;
		}
		case 363:
		{
			XamlUserType xamlUserType24 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SliderBase"));
			xamlUserType24.Activator = Activate_363_Slider;
			xamlUserType24.StaticInitializer = StaticInitializer_363_Slider;
			xamlUserType24.AddMemberName("ShockValue");
			xamlUserType24.AddMemberName("ShockValueType");
			xamlUserType24.AddMemberName("MaximumValue");
			xamlUserType24.AddMemberName("MinimumValue");
			xamlUserType24.SetIsLocalType();
			result = xamlUserType24;
			break;
		}
		case 364:
		{
			XamlUserType xamlUserType23 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Slider"));
			xamlUserType23.StaticInitializer = StaticInitializer_364_SliderBase;
			xamlUserType23.AddMemberName("IsThumbToolTipEnabled");
			xamlUserType23.AddMemberName("Type");
			xamlUserType23.AddMemberName("TextValueVisibility");
			xamlUserType23.SetIsLocalType();
			result = xamlUserType23;
			break;
		}
		case 365:
		{
			XamlUserType xamlUserType22 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType22.StaticInitializer = StaticInitializer_365_ShockValueType;
			xamlUserType22.AddEnumValue("GreaterOrEqualThan", ShockValueType.GreaterOrEqualThan);
			xamlUserType22.AddEnumValue("LessOrEqualThan", ShockValueType.LessOrEqualThan);
			xamlUserType22.SetIsLocalType();
			result = xamlUserType22;
			break;
		}
		case 366:
		{
			XamlUserType xamlUserType21 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType21.StaticInitializer = StaticInitializer_366_SliderType;
			xamlUserType21.AddEnumValue("Type1", SliderType.Type1);
			xamlUserType21.AddEnumValue("Type2", SliderType.Type2);
			xamlUserType21.AddEnumValue("Ghost", SliderType.Ghost);
			xamlUserType21.SetIsLocalType();
			result = xamlUserType21;
			break;
		}
		case 367:
		{
			XamlUserType xamlUserType20 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SliderBase"));
			xamlUserType20.Activator = Activate_367_BufferSlider;
			xamlUserType20.StaticInitializer = StaticInitializer_367_BufferSlider;
			xamlUserType20.AddMemberName("Buffer");
			xamlUserType20.SetIsLocalType();
			result = xamlUserType20;
			break;
		}
		case 368:
		{
			XamlUserType xamlUserType19 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SliderBase"));
			xamlUserType19.Activator = Activate_368_CenterSlider;
			xamlUserType19.StaticInitializer = StaticInitializer_368_CenterSlider;
			xamlUserType19.AddMemberName("Orientation");
			xamlUserType19.SetIsLocalType();
			result = xamlUserType19;
			break;
		}
		case 369:
		{
			XamlUserType xamlUserType18 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType18.Activator = Activate_369_SubAppBar;
			xamlUserType18.StaticInitializer = StaticInitializer_369_SubAppBar;
			xamlUserType18.AddMemberName("Content");
			xamlUserType18.SetIsLocalType();
			result = xamlUserType18;
			break;
		}
		case 370:
		{
			XamlUserType xamlUserType17 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Pivot"));
			xamlUserType17.Activator = Activate_370_TabView;
			xamlUserType17.StaticInitializer = StaticInitializer_370_TabView;
			xamlUserType17.AddMemberName("HeaderClipperMargin");
			xamlUserType17.AddMemberName("Type");
			xamlUserType17.AddMemberName("MaxVisibleHeaderInViewport");
			xamlUserType17.SetIsLocalType();
			result = xamlUserType17;
			break;
		}
		case 371:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 372:
		{
			XamlUserType xamlUserType16 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType16.StaticInitializer = StaticInitializer_372_TabViewType;
			xamlUserType16.AddEnumValue("FullMode", TabViewType.FullMode);
			xamlUserType16.AddEnumValue("AdaptiveFullMode", TabViewType.AdaptiveFullMode);
			xamlUserType16.AddEnumValue("FlexMode", TabViewType.FlexMode);
			xamlUserType16.SetIsLocalType();
			result = xamlUserType16;
			break;
		}
		case 373:
		{
			XamlUserType xamlUserType15 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.PivotItem"));
			xamlUserType15.Activator = Activate_373_TabItem;
			xamlUserType15.StaticInitializer = StaticInitializer_373_TabItem;
			xamlUserType15.AddMemberName("SelectedByKeyboard");
			xamlUserType15.AddMemberName("NotificationBadge");
			xamlUserType15.SetIsLocalType();
			result = xamlUserType15;
			break;
		}
		case 374:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 375:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 376:
		{
			XamlUserType xamlUserType14 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Object"));
			xamlUserType14.Activator = Activate_376_ThicknessSideConverter;
			xamlUserType14.StaticInitializer = StaticInitializer_376_ThicknessSideConverter;
			xamlUserType14.SetIsLocalType();
			result = xamlUserType14;
			break;
		}
		case 377:
		{
			XamlUserType xamlUserType13 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TextBox"));
			xamlUserType13.Activator = Activate_377_TextField;
			xamlUserType13.StaticInitializer = StaticInitializer_377_TextField;
			xamlUserType13.AddMemberName("Type");
			xamlUserType13.AddMemberName("ErrorMessage");
			xamlUserType13.AddMemberName("SvgIcon");
			xamlUserType13.AddMemberName("ScrollViewerMaxHeight");
			xamlUserType13.SetIsLocalType();
			result = xamlUserType13;
			break;
		}
		case 378:
		{
			XamlUserType xamlUserType12 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType12.StaticInitializer = StaticInitializer_378_TextFieldType;
			xamlUserType12.AddEnumValue("Normal", TextFieldType.Normal);
			xamlUserType12.AddEnumValue("SingleList", TextFieldType.SingleList);
			xamlUserType12.SetIsLocalType();
			result = xamlUserType12;
			break;
		}
		case 379:
		{
			XamlUserType xamlUserType11 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType11.Activator = Activate_379_ThumbnailRadious;
			xamlUserType11.StaticInitializer = StaticInitializer_379_ThumbnailRadious;
			xamlUserType11.AddMemberName("ImageLocation");
			xamlUserType11.AddMemberName("Title");
			xamlUserType11.AddMemberName("Description");
			xamlUserType11.AddMemberName("VisualizationMode");
			xamlUserType11.SetIsLocalType();
			result = xamlUserType11;
			break;
		}
		case 380:
		{
			XamlUserType xamlUserType10 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType10.StaticInitializer = StaticInitializer_380_ThumbnailRadiousVisualizationMode;
			xamlUserType10.AddEnumValue("Large", ThumbnailRadiousVisualizationMode.Large);
			xamlUserType10.AddEnumValue("Small", ThumbnailRadiousVisualizationMode.Small);
			xamlUserType10.SetIsLocalType();
			result = xamlUserType10;
			break;
		}
		case 381:
		{
			XamlUserType xamlUserType9 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.GridView"));
			xamlUserType9.Activator = Activate_381_ThumbnailRadiousGridView;
			xamlUserType9.StaticInitializer = StaticInitializer_381_ThumbnailRadiousGridView;
			xamlUserType9.AddMemberName("VisualizationMode");
			xamlUserType9.SetIsLocalType();
			result = xamlUserType9;
			break;
		}
		case 382:
		{
			XamlUserType xamlUserType8 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType8.Activator = Activate_382_Titlebar;
			xamlUserType8.StaticInitializer = StaticInitializer_382_Titlebar;
			xamlUserType8.AddMemberName("Title");
			xamlUserType8.SetIsLocalType();
			result = xamlUserType8;
			break;
		}
		case 383:
		{
			XamlUserType xamlUserType7 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType7.Activator = Activate_383_ToggleSwitch;
			xamlUserType7.StaticInitializer = StaticInitializer_383_ToggleSwitch;
			xamlUserType7.AddMemberName("Header");
			xamlUserType7.AddMemberName("IsOn");
			xamlUserType7.AddMemberName("OffContent");
			xamlUserType7.AddMemberName("OnContent");
			xamlUserType7.AddMemberName("Style");
			xamlUserType7.AddMemberName("Type");
			xamlUserType7.AddMemberName("HeaderTemplate");
			xamlUserType7.AddMemberName("OnContentTemplate");
			xamlUserType7.AddMemberName("OffContentTemplate");
			xamlUserType7.SetIsLocalType();
			result = xamlUserType7;
			break;
		}
		case 384:
		{
			XamlUserType xamlUserType6 = new XamlUserType(this, fullName, type, GetXamlTypeByName("System.Enum"));
			xamlUserType6.StaticInitializer = StaticInitializer_384_ToggleSwitchType;
			xamlUserType6.AddEnumValue("Default", ToggleSwitchType.Default);
			xamlUserType6.AddEnumValue("DefaultRight", ToggleSwitchType.DefaultRight);
			xamlUserType6.AddEnumValue("Main", ToggleSwitchType.Main);
			xamlUserType6.AddEnumValue("MainRight", ToggleSwitchType.MainRight);
			xamlUserType6.SetIsLocalType();
			result = xamlUserType6;
			break;
		}
		case 385:
		{
			XamlUserType xamlUserType5 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Control"));
			xamlUserType5.Activator = Activate_385_ToggleSwitchGroup;
			xamlUserType5.StaticInitializer = StaticInitializer_385_ToggleSwitchGroup;
			xamlUserType5.SetContentPropertyName("Samsung.OneUI.WinUI.Controls.ToggleSwitchGroup.Content");
			xamlUserType5.AddMemberName("Content");
			xamlUserType5.AddMemberName("Header");
			xamlUserType5.AddMemberName("OnContent");
			xamlUserType5.AddMemberName("OffContent");
			xamlUserType5.AddMemberName("LabelToggleSwitchGroupStyle");
			xamlUserType5.AddMemberName("IsOn");
			xamlUserType5.SetIsLocalType();
			result = xamlUserType5;
			break;
		}
		case 386:
			result = new XamlSystemBaseType(fullName, type);
			break;
		case 387:
		{
			XamlUserType xamlUserType4 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Common.DpiChangedStateTriggerBase"));
			xamlUserType4.Activator = Activate_387_DpiChangedTo150StateTrigger;
			xamlUserType4.StaticInitializer = StaticInitializer_387_DpiChangedTo150StateTrigger;
			xamlUserType4.SetIsLocalType();
			result = xamlUserType4;
			break;
		}
		case 388:
		{
			XamlUserType xamlUserType3 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Samsung.OneUI.WinUI.Common.DpiChangedStateTriggerBase"));
			xamlUserType3.Activator = Activate_388_DpiChangedTo100StateTrigger;
			xamlUserType3.StaticInitializer = StaticInitializer_388_DpiChangedTo100StateTrigger;
			xamlUserType3.SetIsLocalType();
			result = xamlUserType3;
			break;
		}
		case 389:
		{
			XamlUserType xamlUserType2 = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.Xaml.Interactivity.Behavior`1<Microsoft.UI.Xaml.Controls.ToggleSwitch>"));
			xamlUserType2.Activator = Activate_389_ToggleSwitchLoadedBehavior;
			xamlUserType2.StaticInitializer = StaticInitializer_389_ToggleSwitchLoadedBehavior;
			xamlUserType2.SetIsLocalType();
			result = xamlUserType2;
			break;
		}
		case 390:
		{
			XamlUserType xamlUserType = new XamlUserType(this, fullName, type, GetXamlTypeByName("Microsoft.Xaml.Interactivity.Behavior"));
			xamlUserType.StaticInitializer = StaticInitializer_390_Behavior;
			xamlUserType.AddMemberName("AssociatedObject");
			result = xamlUserType;
			break;
		}
		}
		return result;
	}

	private IXamlType CheckOtherMetadataProvidersForName(string typeName)
	{
		IXamlType xamlType = null;
		IXamlType result = null;
		foreach (IXamlMetadataProvider otherProvider in OtherProviders)
		{
			xamlType = otherProvider.GetXamlType(typeName);
			if (xamlType != null)
			{
				if (xamlType.IsConstructible)
				{
					return xamlType;
				}
				result = xamlType;
			}
		}
		return result;
	}

	private IXamlType CheckOtherMetadataProvidersForType(Type type)
	{
		IXamlType xamlType = null;
		IXamlType result = null;
		foreach (IXamlMetadataProvider otherProvider in OtherProviders)
		{
			xamlType = otherProvider.GetXamlType(type);
			if (xamlType != null)
			{
				if (xamlType.IsConstructible)
				{
					return xamlType;
				}
				result = xamlType;
			}
		}
		return result;
	}

	private object get_0_BGBlur_LayerContent(object instance)
	{
		return ((BGBlur)instance).LayerContent;
	}

	private void set_0_BGBlur_LayerContent(object instance, object Value)
	{
		((BGBlur)instance).LayerContent = (UIElement)Value;
	}

	private object get_1_BGBlur_Vibrancy(object instance)
	{
		return ((BGBlur)instance).Vibrancy;
	}

	private void set_1_BGBlur_Vibrancy(object instance, object Value)
	{
		((BGBlur)instance).Vibrancy = (Samsung.OneUI.WinUI.Controls.VibrancyLevel)Value;
	}

	private object get_2_BGBlur_FallbackBackground(object instance)
	{
		return ((BGBlur)instance).FallbackBackground;
	}

	private void set_2_BGBlur_FallbackBackground(object instance, object Value)
	{
		((BGBlur)instance).FallbackBackground = (Brush)Value;
	}

	private object get_3_BGBlur_IsDarkGrayish(object instance)
	{
		return ((BGBlur)instance).IsDarkGrayish;
	}

	private void set_3_BGBlur_IsDarkGrayish(object instance, object Value)
	{
		((BGBlur)instance).IsDarkGrayish = (bool)Value;
	}

	private object get_4_CardType_Title(object instance)
	{
		return ((CardType)instance).Title;
	}

	private void set_4_CardType_Title(object instance, object Value)
	{
		((CardType)instance).Title = (string)Value;
	}

	private object get_5_CardType_ButtonText(object instance)
	{
		return ((CardType)instance).ButtonText;
	}

	private void set_5_CardType_ButtonText(object instance, object Value)
	{
		((CardType)instance).ButtonText = (string)Value;
	}

	private object get_6_CardType_Description(object instance)
	{
		return ((CardType)instance).Description;
	}

	private void set_6_CardType_Description(object instance, object Value)
	{
		((CardType)instance).Description = (string)Value;
	}

	private object get_7_CardType_Image(object instance)
	{
		return ((CardType)instance).Image;
	}

	private void set_7_CardType_Image(object instance, object Value)
	{
		((CardType)instance).Image = (ImageSource)Value;
	}

	private object get_8_CardType_SvgImage(object instance)
	{
		return ((CardType)instance).SvgImage;
	}

	private void set_8_CardType_SvgImage(object instance, object Value)
	{
		((CardType)instance).SvgImage = (Style)Value;
	}

	private object get_9_CardTypeListView_ItemsSource(object instance)
	{
		return ((CardTypeListView)instance).ItemsSource;
	}

	private void set_9_CardTypeListView_ItemsSource(object instance, object Value)
	{
		((CardTypeListView)instance).ItemsSource = (List<CardTypeItem>)Value;
	}

	private object get_10_CardTypeItem_Image(object instance)
	{
		return ((CardTypeItem)instance).Image;
	}

	private void set_10_CardTypeItem_Image(object instance, object Value)
	{
		((CardTypeItem)instance).Image = (ImageSource)Value;
	}

	private object get_11_CardTypeItem_SvgStyle(object instance)
	{
		return ((CardTypeItem)instance).SvgStyle;
	}

	private void set_11_CardTypeItem_SvgStyle(object instance, object Value)
	{
		((CardTypeItem)instance).SvgStyle = (Style)Value;
	}

	private object get_12_CardTypeItem_Title(object instance)
	{
		return ((CardTypeItem)instance).Title;
	}

	private void set_12_CardTypeItem_Title(object instance, object Value)
	{
		((CardTypeItem)instance).Title = (string)Value;
	}

	private object get_13_CardTypeItem_Description(object instance)
	{
		return ((CardTypeItem)instance).Description;
	}

	private void set_13_CardTypeItem_Description(object instance, object Value)
	{
		((CardTypeItem)instance).Description = (string)Value;
	}

	private object get_14_CardTypeItem_ButtonText(object instance)
	{
		return ((CardTypeItem)instance).ButtonText;
	}

	private void set_14_CardTypeItem_ButtonText(object instance, object Value)
	{
		((CardTypeItem)instance).ButtonText = (string)Value;
	}

	private object get_15_CardTypeItem_Click_Event(object instance)
	{
		return ((CardTypeItem)instance).Click_Event;
	}

	private void set_15_CardTypeItem_Click_Event(object instance, object Value)
	{
		((CardTypeItem)instance).Click_Event = (EventHandler)Value;
	}

	private object get_16_WrapPanel_HorizontalSpacing(object instance)
	{
		return ((WrapPanel)instance).HorizontalSpacing;
	}

	private void set_16_WrapPanel_HorizontalSpacing(object instance, object Value)
	{
		((WrapPanel)instance).HorizontalSpacing = (double)Value;
	}

	private object get_17_WrapPanel_VerticalSpacing(object instance)
	{
		return ((WrapPanel)instance).VerticalSpacing;
	}

	private void set_17_WrapPanel_VerticalSpacing(object instance, object Value)
	{
		((WrapPanel)instance).VerticalSpacing = (double)Value;
	}

	private object get_18_Chips_Items(object instance)
	{
		return ((Chips)instance).Items;
	}

	private void set_18_Chips_Items(object instance, object Value)
	{
		((Chips)instance).Items = (ObservableCollection<ChipsItem>)Value;
	}

	private object get_19_ChipsItem_Title(object instance)
	{
		return ((ChipsItem)instance).Title;
	}

	private void set_19_ChipsItem_Title(object instance, object Value)
	{
		((ChipsItem)instance).Title = (string)Value;
	}

	private object get_20_ChipsItem_Label(object instance)
	{
		return ((ChipsItem)instance).Label;
	}

	private void set_20_ChipsItem_Label(object instance, object Value)
	{
		((ChipsItem)instance).Label = (ChipsItemTemplate)Value;
	}

	private object get_21_ChipsItem_Id(object instance)
	{
		return ((ChipsItem)instance).Id;
	}

	private void set_21_ChipsItem_Id(object instance, object Value)
	{
		((ChipsItem)instance).Id = (string)Value;
	}

	private object get_22_ChipsItem_Type(object instance)
	{
		return ((ChipsItem)instance).Type;
	}

	private void set_22_ChipsItem_Type(object instance, object Value)
	{
		((ChipsItem)instance).Type = (ChipsItemType)Value;
	}

	private object get_23_ChipsItem_Icon(object instance)
	{
		return ((ChipsItem)instance).Icon;
	}

	private void set_23_ChipsItem_Icon(object instance, object Value)
	{
		((ChipsItem)instance).Icon = (ImageSource)Value;
	}

	private object get_24_ChipsItem_IconSvgStyle(object instance)
	{
		return ((ChipsItem)instance).IconSvgStyle;
	}

	private void set_24_ChipsItem_IconSvgStyle(object instance, object Value)
	{
		((ChipsItem)instance).IconSvgStyle = (Style)Value;
	}

	private object get_25_Chips_SelectionState(object instance)
	{
		return ((Chips)instance).SelectionState;
	}

	private void set_25_Chips_SelectionState(object instance, object Value)
	{
		((Chips)instance).SelectionState = (ListViewSelectionMode)Value;
	}

	private object get_26_Chips_AllLabels(object instance)
	{
		return ((Chips)instance).AllLabels;
	}

	private void set_26_Chips_AllLabels(object instance, object Value)
	{
		((Chips)instance).AllLabels = (ChipsItemGroupTemplate)Value;
	}

	private object get_27_Toast_Message(object instance)
	{
		return ((Toast)instance).Message;
	}

	private void set_27_Toast_Message(object instance, object Value)
	{
		((Toast)instance).Message = (string)Value;
	}

	private object get_28_Toast_ToastDuration(object instance)
	{
		return ((Toast)instance).ToastDuration;
	}

	private void set_28_Toast_ToastDuration(object instance, object Value)
	{
		((Toast)instance).ToastDuration = (ToastDuration)Value;
	}

	private object get_29_Toast_Target(object instance)
	{
		return ((Toast)instance).Target;
	}

	private void set_29_Toast_Target(object instance, object Value)
	{
		((Toast)instance).Target = (FrameworkElement)Value;
	}

	private object get_30_ColorPickerControl_AlphaSliderValue(object instance)
	{
		return ((ColorPickerControl)instance).AlphaSliderValue;
	}

	private void set_30_ColorPickerControl_AlphaSliderValue(object instance, object Value)
	{
		((ColorPickerControl)instance).AlphaSliderValue = (double?)Value;
	}

	private object get_31_ColorPickerControl_IsColorPickerAlphaSliderEditable(object instance)
	{
		return ((ColorPickerControl)instance).IsColorPickerAlphaSliderEditable;
	}

	private void set_31_ColorPickerControl_IsColorPickerAlphaSliderEditable(object instance, object Value)
	{
		((ColorPickerControl)instance).IsColorPickerAlphaSliderEditable = (bool)Value;
	}

	private object get_32_ColorPickerControl_IsAlphaSliderVisible(object instance)
	{
		return ((ColorPickerControl)instance).IsAlphaSliderVisible;
	}

	private void set_32_ColorPickerControl_IsAlphaSliderVisible(object instance, object Value)
	{
		((ColorPickerControl)instance).IsAlphaSliderVisible = (bool)Value;
	}

	private object get_33_ColorPickerControl_IsSaturationSliderVisible(object instance)
	{
		return ((ColorPickerControl)instance).IsSaturationSliderVisible;
	}

	private void set_33_ColorPickerControl_IsSaturationSliderVisible(object instance, object Value)
	{
		((ColorPickerControl)instance).IsSaturationSliderVisible = (bool)Value;
	}

	private object get_34_ColorPickerControl_SelectedColorDescription(object instance)
	{
		return ((ColorPickerControl)instance).SelectedColorDescription;
	}

	private object get_35_ColorPickerControl_SelectedColor(object instance)
	{
		return ((ColorPickerControl)instance).SelectedColor;
	}

	private void set_35_ColorPickerControl_SelectedColor(object instance, object Value)
	{
		((ColorPickerControl)instance).SelectedColor = (SolidColorBrush)Value;
	}

	private object get_36_ColorPickerControl_IsColorPickerSwatchedSelected(object instance)
	{
		return ((ColorPickerControl)instance).IsColorPickerSwatchedSelected;
	}

	private void set_36_ColorPickerControl_IsColorPickerSwatchedSelected(object instance, object Value)
	{
		((ColorPickerControl)instance).IsColorPickerSwatchedSelected = (bool)Value;
	}

	private object get_37_ColorPickerControl_SwatchedVisibility(object instance)
	{
		return ((ColorPickerControl)instance).SwatchedVisibility;
	}

	private void set_37_ColorPickerControl_SwatchedVisibility(object instance, object Value)
	{
		((ColorPickerControl)instance).SwatchedVisibility = (Visibility)Value;
	}

	private object get_38_ColorPickerControl_SpectrumVisibility(object instance)
	{
		return ((ColorPickerControl)instance).SpectrumVisibility;
	}

	private void set_38_ColorPickerControl_SpectrumVisibility(object instance, object Value)
	{
		((ColorPickerControl)instance).SpectrumVisibility = (Visibility)Value;
	}

	private object get_39_ColorPickerControl_Theme(object instance)
	{
		return ((ColorPickerControl)instance).Theme;
	}

	private void set_39_ColorPickerControl_Theme(object instance, object Value)
	{
		((ColorPickerControl)instance).Theme = (ElementTheme)Value;
	}

	private object get_40_ColorPickerControl_RecentColors(object instance)
	{
		return ((ColorPickerControl)instance).RecentColors;
	}

	private void set_40_ColorPickerControl_RecentColors(object instance, object Value)
	{
		((ColorPickerControl)instance).RecentColors = (List<ColorInfo>)Value;
	}

	private object get_41_ColorInfo_Name(object instance)
	{
		return ((ColorInfo)instance).Name;
	}

	private void set_41_ColorInfo_Name(object instance, object Value)
	{
		((ColorInfo)instance).Name = (string)Value;
	}

	private object get_42_ColorInfo_Description(object instance)
	{
		return ((ColorInfo)instance).Description;
	}

	private void set_42_ColorInfo_Description(object instance, object Value)
	{
		((ColorInfo)instance).Description = (string)Value;
	}

	private object get_43_ColorInfo_HexValue(object instance)
	{
		return ((ColorInfo)instance).HexValue;
	}

	private void set_43_ColorInfo_HexValue(object instance, object Value)
	{
		((ColorInfo)instance).HexValue = (string)Value;
	}

	private object get_44_ColorInfo_ColorBrush(object instance)
	{
		return ((ColorInfo)instance).ColorBrush;
	}

	private object get_45_FlatButton_Size(object instance)
	{
		return ((FlatButton)instance).Size;
	}

	private void set_45_FlatButton_Size(object instance, object Value)
	{
		((FlatButton)instance).Size = (FlatButtonSize)Value;
	}

	private object get_46_FlatButton_Type(object instance)
	{
		return ((FlatButton)instance).Type;
	}

	private void set_46_FlatButton_Type(object instance, object Value)
	{
		((FlatButton)instance).Type = (FlatButtonType)Value;
	}

	private object get_47_FlatButtonBase_TextTrimming(object instance)
	{
		return ((FlatButtonBase)instance).TextTrimming;
	}

	private void set_47_FlatButtonBase_TextTrimming(object instance, object Value)
	{
		((FlatButtonBase)instance).TextTrimming = (TextTrimming)Value;
	}

	private object get_48_FlatButtonBase_MaxTextLines(object instance)
	{
		return ((FlatButtonBase)instance).MaxTextLines;
	}

	private void set_48_FlatButtonBase_MaxTextLines(object instance, object Value)
	{
		((FlatButtonBase)instance).MaxTextLines = (int)Value;
	}

	private object get_49_FlatButtonBase_IsProgressEnabled(object instance)
	{
		return ((FlatButtonBase)instance).IsProgressEnabled;
	}

	private void set_49_FlatButtonBase_IsProgressEnabled(object instance, object Value)
	{
		((FlatButtonBase)instance).IsProgressEnabled = (bool)Value;
	}

	private object get_50_ColorPickerDialog_SelectedColorDescription(object instance)
	{
		return ((ColorPickerDialog)instance).SelectedColorDescription;
	}

	private object get_51_ColorPickerDialog_SelectedColor(object instance)
	{
		return ((ColorPickerDialog)instance).SelectedColor;
	}

	private void set_51_ColorPickerDialog_SelectedColor(object instance, object Value)
	{
		((ColorPickerDialog)instance).SelectedColor = (SolidColorBrush)Value;
	}

	private object get_52_ColorPickerDialog_IsColorPickerSwatchedSelected(object instance)
	{
		return ((ColorPickerDialog)instance).IsColorPickerSwatchedSelected;
	}

	private void set_52_ColorPickerDialog_IsColorPickerSwatchedSelected(object instance, object Value)
	{
		((ColorPickerDialog)instance).IsColorPickerSwatchedSelected = (bool)Value;
	}

	private object get_53_ColorPickerDialog_PickedColors(object instance)
	{
		return ((ColorPickerDialog)instance).PickedColors;
	}

	private object get_54_ColorPickerDialog_AlphaSliderValue(object instance)
	{
		return ((ColorPickerDialog)instance).AlphaSliderValue;
	}

	private void set_54_ColorPickerDialog_AlphaSliderValue(object instance, object Value)
	{
		((ColorPickerDialog)instance).AlphaSliderValue = (double?)Value;
	}

	private object get_55_ColorPickerDialog_IsColorPickerAlphaSliderEditable(object instance)
	{
		return ((ColorPickerDialog)instance).IsColorPickerAlphaSliderEditable;
	}

	private void set_55_ColorPickerDialog_IsColorPickerAlphaSliderEditable(object instance, object Value)
	{
		((ColorPickerDialog)instance).IsColorPickerAlphaSliderEditable = (bool)Value;
	}

	private object get_56_ColorPickerDialog_IsOpen(object instance)
	{
		return ((ColorPickerDialog)instance).IsOpen;
	}

	private void set_56_ColorPickerDialog_IsOpen(object instance, object Value)
	{
		((ColorPickerDialog)instance).IsOpen = (bool)Value;
	}

	private object get_57_ColorPickerDialog_IsAlphaSliderVisible(object instance)
	{
		return ((ColorPickerDialog)instance).IsAlphaSliderVisible;
	}

	private void set_57_ColorPickerDialog_IsAlphaSliderVisible(object instance, object Value)
	{
		((ColorPickerDialog)instance).IsAlphaSliderVisible = (bool)Value;
	}

	private object get_58_ColorPickerDialog_IsSaturationSliderVisible(object instance)
	{
		return ((ColorPickerDialog)instance).IsSaturationSliderVisible;
	}

	private void set_58_ColorPickerDialog_IsSaturationSliderVisible(object instance, object Value)
	{
		((ColorPickerDialog)instance).IsSaturationSliderVisible = (bool)Value;
	}

	private object get_59_ColorPickerDialog_isDialogViewBoxEnabled(object instance)
	{
		return ((ColorPickerDialog)instance).isDialogViewBoxEnabled;
	}

	private void set_59_ColorPickerDialog_isDialogViewBoxEnabled(object instance, object Value)
	{
		((ColorPickerDialog)instance).isDialogViewBoxEnabled = (bool)Value;
	}

	private object get_60_ColorPickerDialog_DialogViewBoxWidth(object instance)
	{
		return ((ColorPickerDialog)instance).DialogViewBoxWidth;
	}

	private void set_60_ColorPickerDialog_DialogViewBoxWidth(object instance, object Value)
	{
		((ColorPickerDialog)instance).DialogViewBoxWidth = (double)Value;
	}

	private object get_61_ColorPickerDialog_DialogViewBoxHeight(object instance)
	{
		return ((ColorPickerDialog)instance).DialogViewBoxHeight;
	}

	private void set_61_ColorPickerDialog_DialogViewBoxHeight(object instance, object Value)
	{
		((ColorPickerDialog)instance).DialogViewBoxHeight = (double)Value;
	}

	private object get_62_ColorPickerDialog_RecentColors(object instance)
	{
		return ((ColorPickerDialog)instance).RecentColors;
	}

	private void set_62_ColorPickerDialog_RecentColors(object instance, object Value)
	{
		((ColorPickerDialog)instance).RecentColors = (List<ColorInfo>)Value;
	}

	private object get_63_DatePicker_ActualDateTimeScope(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.DatePicker)instance).ActualDateTimeScope;
	}

	private void set_63_DatePicker_ActualDateTimeScope(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.DatePicker)instance).ActualDateTimeScope = (DateTime)Value;
	}

	private object get_64_DatePicker_SelectedDate(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.DatePicker)instance).SelectedDate;
	}

	private void set_64_DatePicker_SelectedDate(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.DatePicker)instance).SelectedDate = (DateTime)Value;
	}

	private object get_65_DatePicker_SundayDayIndicator(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.DatePicker)instance).SundayDayIndicator;
	}

	private void set_65_DatePicker_SundayDayIndicator(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.DatePicker)instance).SundayDayIndicator = (int)Value;
	}

	private object get_66_DatePickerDialogContent_SelectedDate(object instance)
	{
		return ((DatePickerDialogContent)instance).SelectedDate;
	}

	private object get_67_DateTimePickerList_Date(object instance)
	{
		return ((DateTimePickerList)instance).Date;
	}

	private void set_67_DateTimePickerList_Date(object instance, object Value)
	{
		((DateTimePickerList)instance).Date = (DateTime)Value;
	}

	private object get_68_DateTimePickerList_StartRangeDate(object instance)
	{
		return ((DateTimePickerList)instance).StartRangeDate;
	}

	private void set_68_DateTimePickerList_StartRangeDate(object instance, object Value)
	{
		((DateTimePickerList)instance).StartRangeDate = (DateTime?)Value;
	}

	private object get_69_DateTimePickerList_RangeDays(object instance)
	{
		return ((DateTimePickerList)instance).RangeDays;
	}

	private void set_69_DateTimePickerList_RangeDays(object instance, object Value)
	{
		((DateTimePickerList)instance).RangeDays = (int)Value;
	}

	private object get_70_TimePickerList_Type(object instance)
	{
		return ((TimePickerList)instance).Type;
	}

	private void set_70_TimePickerList_Type(object instance, object Value)
	{
		((TimePickerList)instance).Type = (TimeType)Value;
	}

	private object get_71_TimePickerList_Period(object instance)
	{
		return ((TimePickerList)instance).Period;
	}

	private void set_71_TimePickerList_Period(object instance, object Value)
	{
		((TimePickerList)instance).Period = (TimePeriod)Value;
	}

	private object get_72_TimePickerList_Hour(object instance)
	{
		return ((TimePickerList)instance).Hour;
	}

	private void set_72_TimePickerList_Hour(object instance, object Value)
	{
		((TimePickerList)instance).Hour = (string)Value;
	}

	private object get_73_TimePickerList_Minute(object instance)
	{
		return ((TimePickerList)instance).Minute;
	}

	private void set_73_TimePickerList_Minute(object instance, object Value)
	{
		((TimePickerList)instance).Minute = (string)Value;
	}

	private object get_74_TimePickerList_TimeResult(object instance)
	{
		return ((TimePickerList)instance).TimeResult;
	}

	private object get_75_DateTimePickerDialogContent_DateResult(object instance)
	{
		return ((DateTimePickerDialogContent)instance).DateResult;
	}

	private object get_76_OneUIContentDialogContent_ScrollViewer(object instance)
	{
		return ((OneUIContentDialogContent)instance).ScrollViewer;
	}

	private void set_76_OneUIContentDialogContent_ScrollViewer(object instance, object Value)
	{
		((OneUIContentDialogContent)instance).ScrollViewer = (Microsoft.UI.Xaml.Controls.ScrollViewer)Value;
	}

	private object get_77_ListViewCustom_NoItemsText(object instance)
	{
		return ((ListViewCustom)instance).NoItemsText;
	}

	private void set_77_ListViewCustom_NoItemsText(object instance, object Value)
	{
		((ListViewCustom)instance).NoItemsText = (string)Value;
	}

	private object get_78_ListViewCustom_NoItemsDescription(object instance)
	{
		return ((ListViewCustom)instance).NoItemsDescription;
	}

	private void set_78_ListViewCustom_NoItemsDescription(object instance, object Value)
	{
		((ListViewCustom)instance).NoItemsDescription = (string)Value;
	}

	private object get_79_ListViewCustom_CounterText(object instance)
	{
		return ((ListViewCustom)instance).CounterText;
	}

	private void set_79_ListViewCustom_CounterText(object instance, object Value)
	{
		((ListViewCustom)instance).CounterText = (string)Value;
	}

	private object get_80_Responsiveness_FlexibleSpacingType(object instance)
	{
		return Responsiveness.GetFlexibleSpacingType((UIElement)instance);
	}

	private void set_80_Responsiveness_FlexibleSpacingType(object instance, object Value)
	{
		Responsiveness.SetFlexibleSpacingType((UIElement)instance, (FlexibleSpacingType)Value);
	}

	private object get_81_Responsiveness_IsFlexibleSpacing(object instance)
	{
		return Responsiveness.GetIsFlexibleSpacing((UIElement)instance);
	}

	private void set_81_Responsiveness_IsFlexibleSpacing(object instance, object Value)
	{
		Responsiveness.SetIsFlexibleSpacing((UIElement)instance, (bool?)Value);
	}

	private object get_82_SnackBarButton_Type(object instance)
	{
		return ((SnackBarButton)instance).Type;
	}

	private void set_82_SnackBarButton_Type(object instance, object Value)
	{
		((SnackBarButton)instance).Type = (SnackBarButtonType)Value;
	}

	private object get_83_SnackBar_Message(object instance)
	{
		return ((SnackBar)instance).Message;
	}

	private void set_83_SnackBar_Message(object instance, object Value)
	{
		((SnackBar)instance).Message = (string)Value;
	}

	private object get_84_SnackBar_SnackBarDuration(object instance)
	{
		return ((SnackBar)instance).SnackBarDuration;
	}

	private void set_84_SnackBar_SnackBarDuration(object instance, object Value)
	{
		((SnackBar)instance).SnackBarDuration = (SnackBarDuration)Value;
	}

	private object get_85_SnackBar_Target(object instance)
	{
		return ((SnackBar)instance).Target;
	}

	private void set_85_SnackBar_Target(object instance, object Value)
	{
		((SnackBar)instance).Target = (FrameworkElement)Value;
	}

	private object get_86_SnackBar_IsShowButton(object instance)
	{
		return ((SnackBar)instance).IsShowButton;
	}

	private void set_86_SnackBar_IsShowButton(object instance, object Value)
	{
		((SnackBar)instance).IsShowButton = (bool)Value;
	}

	private object get_87_SnackBar_ButtonText(object instance)
	{
		return ((SnackBar)instance).ButtonText;
	}

	private void set_87_SnackBar_ButtonText(object instance, object Value)
	{
		((SnackBar)instance).ButtonText = (string)Value;
	}

	private object get_88_TimePickerKeyboard_TimeResult(object instance)
	{
		return ((TimePickerKeyboard)instance).TimeResult;
	}

	private object get_89_TimePickerKeyboard_Hour(object instance)
	{
		return ((TimePickerKeyboard)instance).Hour;
	}

	private void set_89_TimePickerKeyboard_Hour(object instance, object Value)
	{
		((TimePickerKeyboard)instance).Hour = (int)Value;
	}

	private object get_90_TimePickerKeyboard_Minute(object instance)
	{
		return ((TimePickerKeyboard)instance).Minute;
	}

	private void set_90_TimePickerKeyboard_Minute(object instance, object Value)
	{
		((TimePickerKeyboard)instance).Minute = (int)Value;
	}

	private object get_91_TimePickerKeyboard_Type(object instance)
	{
		return ((TimePickerKeyboard)instance).Type;
	}

	private void set_91_TimePickerKeyboard_Type(object instance, object Value)
	{
		((TimePickerKeyboard)instance).Type = (TimeType)Value;
	}

	private object get_92_TimePickerKeyboardDialogContent_TimerResult(object instance)
	{
		return ((TimePickerKeyboardDialogContent)instance).TimerResult;
	}

	private object get_93_TimePickerListDialogContent_TimerResult(object instance)
	{
		return ((TimePickerListDialogContent)instance).TimerResult;
	}

	private object get_94_ColorPickerOption_IsColorPickerSwatchedSelected(object instance)
	{
		return ((ColorPickerOption)instance).IsColorPickerSwatchedSelected;
	}

	private void set_94_ColorPickerOption_IsColorPickerSwatchedSelected(object instance, object Value)
	{
		((ColorPickerOption)instance).IsColorPickerSwatchedSelected = (bool)Value;
	}

	private object get_95_ColorPickerDescriptor_SelectedColor(object instance)
	{
		return ((ColorPickerDescriptor)instance).SelectedColor;
	}

	private void set_95_ColorPickerDescriptor_SelectedColor(object instance, object Value)
	{
		((ColorPickerDescriptor)instance).SelectedColor = (SolidColorBrush)Value;
	}

	private object get_96_ColorPickerDescriptor_PreviousSelectedColor(object instance)
	{
		return ((ColorPickerDescriptor)instance).PreviousSelectedColor;
	}

	private void set_96_ColorPickerDescriptor_PreviousSelectedColor(object instance, object Value)
	{
		((ColorPickerDescriptor)instance).PreviousSelectedColor = (SolidColorBrush)Value;
	}

	private object get_97_SubHeader_IsShowDivider(object instance)
	{
		return ((SubHeader)instance).IsShowDivider;
	}

	private void set_97_SubHeader_IsShowDivider(object instance, object Value)
	{
		((SubHeader)instance).IsShowDivider = (bool)Value;
	}

	private object get_98_SubHeader_HeaderText(object instance)
	{
		return ((SubHeader)instance).HeaderText;
	}

	private void set_98_SubHeader_HeaderText(object instance, object Value)
	{
		((SubHeader)instance).HeaderText = (string)Value;
	}

	private object get_99_ColorPickerHistory_RecentColors(object instance)
	{
		return ((ColorPickerHistory)instance).RecentColors;
	}

	private void set_99_ColorPickerHistory_RecentColors(object instance, object Value)
	{
		((ColorPickerHistory)instance).RecentColors = (List<ColorInfo>)Value;
	}

	private object get_100_ColorPickerHistory_SelectedColorDescription(object instance)
	{
		return ((ColorPickerHistory)instance).SelectedColorDescription;
	}

	private object get_101_ColorPickerHistory_ItemColorBackground(object instance)
	{
		return ((ColorPickerHistory)instance).ItemColorBackground;
	}

	private void set_101_ColorPickerHistory_ItemColorBackground(object instance, object Value)
	{
		((ColorPickerHistory)instance).ItemColorBackground = (SolidColorBrush)Value;
	}

	private object get_102_ColorPickerSwatched_AlphaSliderValue(object instance)
	{
		return ((ColorPickerSwatched)instance).AlphaSliderValue;
	}

	private void set_102_ColorPickerSwatched_AlphaSliderValue(object instance, object Value)
	{
		((ColorPickerSwatched)instance).AlphaSliderValue = (double?)Value;
	}

	private object get_103_ColorPickerSwatched_IsColorPickerAlphaSliderEditable(object instance)
	{
		return ((ColorPickerSwatched)instance).IsColorPickerAlphaSliderEditable;
	}

	private void set_103_ColorPickerSwatched_IsColorPickerAlphaSliderEditable(object instance, object Value)
	{
		((ColorPickerSwatched)instance).IsColorPickerAlphaSliderEditable = (bool)Value;
	}

	private object get_104_ColorPickerSwatched_IsAlphaSliderVisible(object instance)
	{
		return ((ColorPickerSwatched)instance).IsAlphaSliderVisible;
	}

	private void set_104_ColorPickerSwatched_IsAlphaSliderVisible(object instance, object Value)
	{
		((ColorPickerSwatched)instance).IsAlphaSliderVisible = (bool)Value;
	}

	private object get_105_ColorPickerSwatched_SelectedColorDescription(object instance)
	{
		return ((ColorPickerSwatched)instance).SelectedColorDescription;
	}

	private void set_105_ColorPickerSwatched_SelectedColorDescription(object instance, object Value)
	{
		((ColorPickerSwatched)instance).SelectedColorDescription = (string)Value;
	}

	private object get_106_ColorPickerSwatched_SelectedColor(object instance)
	{
		return ((ColorPickerSwatched)instance).SelectedColor;
	}

	private void set_106_ColorPickerSwatched_SelectedColor(object instance, object Value)
	{
		((ColorPickerSwatched)instance).SelectedColor = (SolidColorBrush)Value;
	}

	private object get_107_ColorPicker_Color(object instance)
	{
		return ((ColorPicker)instance).Color;
	}

	private void set_107_ColorPicker_Color(object instance, object Value)
	{
		((ColorPicker)instance).Color = (Color)Value;
	}

	private object get_108_ColorPicker_ColorSpectrumComponents(object instance)
	{
		return ((ColorPicker)instance).ColorSpectrumComponents;
	}

	private void set_108_ColorPicker_ColorSpectrumComponents(object instance, object Value)
	{
		((ColorPicker)instance).ColorSpectrumComponents = (ColorSpectrumComponents)Value;
	}

	private object get_109_ColorPicker_ColorSpectrumShape(object instance)
	{
		return ((ColorPicker)instance).ColorSpectrumShape;
	}

	private void set_109_ColorPicker_ColorSpectrumShape(object instance, object Value)
	{
		((ColorPicker)instance).ColorSpectrumShape = (ColorSpectrumShape)Value;
	}

	private object get_110_ColorPicker_IsAlphaEnabled(object instance)
	{
		return ((ColorPicker)instance).IsAlphaEnabled;
	}

	private void set_110_ColorPicker_IsAlphaEnabled(object instance, object Value)
	{
		((ColorPicker)instance).IsAlphaEnabled = (bool)Value;
	}

	private object get_111_ColorPicker_IsAlphaTextInputVisible(object instance)
	{
		return ((ColorPicker)instance).IsAlphaTextInputVisible;
	}

	private void set_111_ColorPicker_IsAlphaTextInputVisible(object instance, object Value)
	{
		((ColorPicker)instance).IsAlphaTextInputVisible = (bool)Value;
	}

	private object get_112_ColorPicker_IsColorChannelTextInputVisible(object instance)
	{
		return ((ColorPicker)instance).IsColorChannelTextInputVisible;
	}

	private void set_112_ColorPicker_IsColorChannelTextInputVisible(object instance, object Value)
	{
		((ColorPicker)instance).IsColorChannelTextInputVisible = (bool)Value;
	}

	private object get_113_ColorPicker_IsColorPreviewVisible(object instance)
	{
		return ((ColorPicker)instance).IsColorPreviewVisible;
	}

	private void set_113_ColorPicker_IsColorPreviewVisible(object instance, object Value)
	{
		((ColorPicker)instance).IsColorPreviewVisible = (bool)Value;
	}

	private object get_114_ColorPicker_IsColorSliderVisible(object instance)
	{
		return ((ColorPicker)instance).IsColorSliderVisible;
	}

	private void set_114_ColorPicker_IsColorSliderVisible(object instance, object Value)
	{
		((ColorPicker)instance).IsColorSliderVisible = (bool)Value;
	}

	private object get_115_ColorPicker_IsColorSpectrumVisible(object instance)
	{
		return ((ColorPicker)instance).IsColorSpectrumVisible;
	}

	private void set_115_ColorPicker_IsColorSpectrumVisible(object instance, object Value)
	{
		((ColorPicker)instance).IsColorSpectrumVisible = (bool)Value;
	}

	private object get_116_ColorPicker_IsHexInputVisible(object instance)
	{
		return ((ColorPicker)instance).IsHexInputVisible;
	}

	private void set_116_ColorPicker_IsHexInputVisible(object instance, object Value)
	{
		((ColorPicker)instance).IsHexInputVisible = (bool)Value;
	}

	private object get_117_ColorPicker_IsMoreButtonVisible(object instance)
	{
		return ((ColorPicker)instance).IsMoreButtonVisible;
	}

	private void set_117_ColorPicker_IsMoreButtonVisible(object instance, object Value)
	{
		((ColorPicker)instance).IsMoreButtonVisible = (bool)Value;
	}

	private object get_118_ColorPicker_MaxHue(object instance)
	{
		return ((ColorPicker)instance).MaxHue;
	}

	private void set_118_ColorPicker_MaxHue(object instance, object Value)
	{
		((ColorPicker)instance).MaxHue = (int)Value;
	}

	private object get_119_ColorPicker_MaxSaturation(object instance)
	{
		return ((ColorPicker)instance).MaxSaturation;
	}

	private void set_119_ColorPicker_MaxSaturation(object instance, object Value)
	{
		((ColorPicker)instance).MaxSaturation = (int)Value;
	}

	private object get_120_ColorPicker_MaxValue(object instance)
	{
		return ((ColorPicker)instance).MaxValue;
	}

	private void set_120_ColorPicker_MaxValue(object instance, object Value)
	{
		((ColorPicker)instance).MaxValue = (int)Value;
	}

	private object get_121_ColorPicker_MinHue(object instance)
	{
		return ((ColorPicker)instance).MinHue;
	}

	private void set_121_ColorPicker_MinHue(object instance, object Value)
	{
		((ColorPicker)instance).MinHue = (int)Value;
	}

	private object get_122_ColorPicker_MinSaturation(object instance)
	{
		return ((ColorPicker)instance).MinSaturation;
	}

	private void set_122_ColorPicker_MinSaturation(object instance, object Value)
	{
		((ColorPicker)instance).MinSaturation = (int)Value;
	}

	private object get_123_ColorPicker_MinValue(object instance)
	{
		return ((ColorPicker)instance).MinValue;
	}

	private void set_123_ColorPicker_MinValue(object instance, object Value)
	{
		((ColorPicker)instance).MinValue = (int)Value;
	}

	private object get_124_ColorPicker_Orientation(object instance)
	{
		return ((ColorPicker)instance).Orientation;
	}

	private void set_124_ColorPicker_Orientation(object instance, object Value)
	{
		((ColorPicker)instance).Orientation = (Orientation)Value;
	}

	private object get_125_ColorPicker_PreviousColor(object instance)
	{
		return ((ColorPicker)instance).PreviousColor;
	}

	private void set_125_ColorPicker_PreviousColor(object instance, object Value)
	{
		((ColorPicker)instance).PreviousColor = (Color?)Value;
	}

	private object get_126_ColorPickerSpectrum_AlphaSliderValue(object instance)
	{
		return ((ColorPickerSpectrum)instance).AlphaSliderValue;
	}

	private void set_126_ColorPickerSpectrum_AlphaSliderValue(object instance, object Value)
	{
		((ColorPickerSpectrum)instance).AlphaSliderValue = (double?)Value;
	}

	private object get_127_ColorPickerSpectrum_IsColorPickerAlphaSliderEditable(object instance)
	{
		return ((ColorPickerSpectrum)instance).IsColorPickerAlphaSliderEditable;
	}

	private void set_127_ColorPickerSpectrum_IsColorPickerAlphaSliderEditable(object instance, object Value)
	{
		((ColorPickerSpectrum)instance).IsColorPickerAlphaSliderEditable = (bool)Value;
	}

	private object get_128_ColorPickerSpectrum_IsAlphaSliderVisible(object instance)
	{
		return ((ColorPickerSpectrum)instance).IsAlphaSliderVisible;
	}

	private void set_128_ColorPickerSpectrum_IsAlphaSliderVisible(object instance, object Value)
	{
		((ColorPickerSpectrum)instance).IsAlphaSliderVisible = (bool)Value;
	}

	private object get_129_ColorPickerSpectrum_IsSaturationSliderVisible(object instance)
	{
		return ((ColorPickerSpectrum)instance).IsSaturationSliderVisible;
	}

	private void set_129_ColorPickerSpectrum_IsSaturationSliderVisible(object instance, object Value)
	{
		((ColorPickerSpectrum)instance).IsSaturationSliderVisible = (bool)Value;
	}

	private object get_130_ColorPickerSpectrum_SelectedColorDescription(object instance)
	{
		return ((ColorPickerSpectrum)instance).SelectedColorDescription;
	}

	private void set_130_ColorPickerSpectrum_SelectedColorDescription(object instance, object Value)
	{
		((ColorPickerSpectrum)instance).SelectedColorDescription = (string)Value;
	}

	private object get_131_CornerRadius_TopLeft(object instance)
	{
		return ((CornerRadius)instance).TopLeft;
	}

	private void set_131_CornerRadius_TopLeft(object instance, object Value)
	{
		CornerRadius cornerRadius = (CornerRadius)instance;
		cornerRadius.TopLeft = (double)Value;
	}

	private object get_132_CornerRadius_TopRight(object instance)
	{
		return ((CornerRadius)instance).TopRight;
	}

	private void set_132_CornerRadius_TopRight(object instance, object Value)
	{
		CornerRadius cornerRadius = (CornerRadius)instance;
		cornerRadius.TopRight = (double)Value;
	}

	private object get_133_CornerRadius_BottomRight(object instance)
	{
		return ((CornerRadius)instance).BottomRight;
	}

	private void set_133_CornerRadius_BottomRight(object instance, object Value)
	{
		CornerRadius cornerRadius = (CornerRadius)instance;
		cornerRadius.BottomRight = (double)Value;
	}

	private object get_134_CornerRadius_BottomLeft(object instance)
	{
		return ((CornerRadius)instance).BottomLeft;
	}

	private void set_134_CornerRadius_BottomLeft(object instance, object Value)
	{
		CornerRadius cornerRadius = (CornerRadius)instance;
		cornerRadius.BottomLeft = (double)Value;
	}

	private object get_135_ColorPickerTextBox_StringResourceKey(object instance)
	{
		return ((ColorPickerTextBox)instance).StringResourceKey;
	}

	private void set_135_ColorPickerTextBox_StringResourceKey(object instance, object Value)
	{
		((ColorPickerTextBox)instance).StringResourceKey = (string)Value;
	}

	private object get_136_ColorPickerTextBox_TextBoxStyle(object instance)
	{
		return ((ColorPickerTextBox)instance).TextBoxStyle;
	}

	private void set_136_ColorPickerTextBox_TextBoxStyle(object instance, object Value)
	{
		((ColorPickerTextBox)instance).TextBoxStyle = (Style)Value;
	}

	private object get_137_ColorPickerTextBox_IsTextBoxLoaded(object instance)
	{
		return ((ColorPickerTextBox)instance).IsTextBoxLoaded;
	}

	private void set_137_ColorPickerTextBox_IsTextBoxLoaded(object instance, object Value)
	{
		((ColorPickerTextBox)instance).IsTextBoxLoaded = (bool)Value;
	}

	private object get_138_ColorPickerTextBox_Text(object instance)
	{
		return ((ColorPickerTextBox)instance).Text;
	}

	private void set_138_ColorPickerTextBox_Text(object instance, object Value)
	{
		((ColorPickerTextBox)instance).Text = (string)Value;
	}

	private object get_139_CheckeredBrush_BackgroundBrush(object instance)
	{
		return ((CheckeredBrush)instance).BackgroundBrush;
	}

	private void set_139_CheckeredBrush_BackgroundBrush(object instance, object Value)
	{
		((CheckeredBrush)instance).BackgroundBrush = (SolidColorBrush)Value;
	}

	private object get_140_CheckeredBrush_RectBrush(object instance)
	{
		return ((CheckeredBrush)instance).RectBrush;
	}

	private void set_140_CheckeredBrush_RectBrush(object instance, object Value)
	{
		((CheckeredBrush)instance).RectBrush = (SolidColorBrush)Value;
	}

	private object get_141_ColorListItemSelector_EmptyStyle(object instance)
	{
		return ((ColorListItemSelector)instance).EmptyStyle;
	}

	private void set_141_ColorListItemSelector_EmptyStyle(object instance, object Value)
	{
		((ColorListItemSelector)instance).EmptyStyle = (Style)Value;
	}

	private object get_142_ColorListItemSelector_NormalStyle(object instance)
	{
		return ((ColorListItemSelector)instance).NormalStyle;
	}

	private void set_142_ColorListItemSelector_NormalStyle(object instance, object Value)
	{
		((ColorListItemSelector)instance).NormalStyle = (Style)Value;
	}

	private object get_143_CornerRadiusAutoHalfCorner_CornerPoint(object instance)
	{
		return CornerRadiusAutoHalfCorner.GetCornerPoint((DependencyObject)instance);
	}

	private void set_143_CornerRadiusAutoHalfCorner_CornerPoint(object instance, object Value)
	{
		CornerRadiusAutoHalfCorner.SetCornerPoint((DependencyObject)instance, (string)Value);
	}

	private object get_144_CornerRadiusAutoHalfCorner_CanOverride(object instance)
	{
		return CornerRadiusAutoHalfCorner.GetCanOverride((DependencyObject)instance);
	}

	private void set_144_CornerRadiusAutoHalfCorner_CanOverride(object instance, object Value)
	{
		CornerRadiusAutoHalfCorner.SetCanOverride((DependencyObject)instance, (bool)Value);
	}

	private object get_145_Thickness_Left(object instance)
	{
		return ((Thickness)instance).Left;
	}

	private void set_145_Thickness_Left(object instance, object Value)
	{
		Thickness thickness = (Thickness)instance;
		thickness.Left = (double)Value;
	}

	private object get_146_Thickness_Top(object instance)
	{
		return ((Thickness)instance).Top;
	}

	private void set_146_Thickness_Top(object instance, object Value)
	{
		Thickness thickness = (Thickness)instance;
		thickness.Top = (double)Value;
	}

	private object get_147_Thickness_Right(object instance)
	{
		return ((Thickness)instance).Right;
	}

	private void set_147_Thickness_Right(object instance, object Value)
	{
		Thickness thickness = (Thickness)instance;
		thickness.Right = (double)Value;
	}

	private object get_148_Thickness_Bottom(object instance)
	{
		return ((Thickness)instance).Bottom;
	}

	private void set_148_Thickness_Bottom(object instance, object Value)
	{
		Thickness thickness = (Thickness)instance;
		thickness.Bottom = (double)Value;
	}

	private object get_149_OverlayColorsToSolidColorBrushExtension_ColorList(object instance)
	{
		return ((OverlayColorsToSolidColorBrushExtension)instance).ColorList;
	}

	private void set_149_OverlayColorsToSolidColorBrushExtension_ColorList(object instance, object Value)
	{
		((OverlayColorsToSolidColorBrushExtension)instance).ColorList = (IList<SolidColorBrush>)Value;
	}

	private object get_150_CornerRadiusToDoubleConverter_ConvertionRoundingStrategy(object instance)
	{
		return ((CornerRadiusToDoubleConverter)instance).ConvertionRoundingStrategy;
	}

	private void set_150_CornerRadiusToDoubleConverter_ConvertionRoundingStrategy(object instance, object Value)
	{
		((CornerRadiusToDoubleConverter)instance).ConvertionRoundingStrategy = (ICornerRadiusRoundingStrategyConvertion)Value;
	}

	private object get_151_ColorSpectrum_Components(object instance)
	{
		return ((ColorSpectrum)instance).Components;
	}

	private void set_151_ColorSpectrum_Components(object instance, object Value)
	{
		((ColorSpectrum)instance).Components = (ColorSpectrumComponents)Value;
	}

	private object get_152_ColorSpectrum_MaxHue(object instance)
	{
		return ((ColorSpectrum)instance).MaxHue;
	}

	private void set_152_ColorSpectrum_MaxHue(object instance, object Value)
	{
		((ColorSpectrum)instance).MaxHue = (int)Value;
	}

	private object get_153_ColorSpectrum_MaxSaturation(object instance)
	{
		return ((ColorSpectrum)instance).MaxSaturation;
	}

	private void set_153_ColorSpectrum_MaxSaturation(object instance, object Value)
	{
		((ColorSpectrum)instance).MaxSaturation = (int)Value;
	}

	private object get_154_ColorSpectrum_MaxValue(object instance)
	{
		return ((ColorSpectrum)instance).MaxValue;
	}

	private void set_154_ColorSpectrum_MaxValue(object instance, object Value)
	{
		((ColorSpectrum)instance).MaxValue = (int)Value;
	}

	private object get_155_ColorSpectrum_MinHue(object instance)
	{
		return ((ColorSpectrum)instance).MinHue;
	}

	private void set_155_ColorSpectrum_MinHue(object instance, object Value)
	{
		((ColorSpectrum)instance).MinHue = (int)Value;
	}

	private object get_156_ColorSpectrum_MinSaturation(object instance)
	{
		return ((ColorSpectrum)instance).MinSaturation;
	}

	private void set_156_ColorSpectrum_MinSaturation(object instance, object Value)
	{
		((ColorSpectrum)instance).MinSaturation = (int)Value;
	}

	private object get_157_ColorSpectrum_MinValue(object instance)
	{
		return ((ColorSpectrum)instance).MinValue;
	}

	private void set_157_ColorSpectrum_MinValue(object instance, object Value)
	{
		((ColorSpectrum)instance).MinValue = (int)Value;
	}

	private object get_158_ColorSpectrum_Shape(object instance)
	{
		return ((ColorSpectrum)instance).Shape;
	}

	private void set_158_ColorSpectrum_Shape(object instance, object Value)
	{
		((ColorSpectrum)instance).Shape = (ColorSpectrumShape)Value;
	}

	private object get_159_ColorSpectrum_Color(object instance)
	{
		return ((ColorSpectrum)instance).Color;
	}

	private void set_159_ColorSpectrum_Color(object instance, object Value)
	{
		((ColorSpectrum)instance).Color = (Color)Value;
	}

	private object get_160_ColorSpectrum_HsvColor(object instance)
	{
		return ((ColorSpectrum)instance).HsvColor;
	}

	private void set_160_ColorSpectrum_HsvColor(object instance, object Value)
	{
		((ColorSpectrum)instance).HsvColor = (Vector4)Value;
	}

	private object get_161_ColorPickerSlider_ColorChannel(object instance)
	{
		return ((ColorPickerSlider)instance).ColorChannel;
	}

	private void set_161_ColorPickerSlider_ColorChannel(object instance, object Value)
	{
		((ColorPickerSlider)instance).ColorChannel = (ColorPickerHsvChannel)Value;
	}

	private object get_162_Effects_Shadow(object instance)
	{
		return Effects.GetShadow((FrameworkElement)instance);
	}

	private void set_162_Effects_Shadow(object instance, object Value)
	{
		Effects.SetShadow((FrameworkElement)instance, (AttachedShadowBase)Value);
	}

	private object get_163_AttachedShadowBase_BlurRadius(object instance)
	{
		return ((AttachedShadowBase)instance).BlurRadius;
	}

	private void set_163_AttachedShadowBase_BlurRadius(object instance, object Value)
	{
		((AttachedShadowBase)instance).BlurRadius = (double)Value;
	}

	private object get_164_AttachedCardShadow_CornerRadius(object instance)
	{
		return ((AttachedCardShadow)instance).CornerRadius;
	}

	private void set_164_AttachedCardShadow_CornerRadius(object instance, object Value)
	{
		((AttachedCardShadow)instance).CornerRadius = (double)Value;
	}

	private object get_165_AttachedShadowBase_Opacity(object instance)
	{
		return ((AttachedShadowBase)instance).Opacity;
	}

	private void set_165_AttachedShadowBase_Opacity(object instance, object Value)
	{
		((AttachedShadowBase)instance).Opacity = (double)Value;
	}

	private object get_166_AttachedShadowBase_Offset(object instance)
	{
		return ((AttachedShadowBase)instance).Offset;
	}

	private void set_166_AttachedShadowBase_Offset(object instance, object Value)
	{
		((AttachedShadowBase)instance).Offset = (string)Value;
	}

	private object get_167_AttachedShadowBase_Color(object instance)
	{
		return ((AttachedShadowBase)instance).Color;
	}

	private void set_167_AttachedShadowBase_Color(object instance, object Value)
	{
		((AttachedShadowBase)instance).Color = (Color)Value;
	}

	private object get_168_AttachedCardShadow_InnerContentClipMode(object instance)
	{
		return ((AttachedCardShadow)instance).InnerContentClipMode;
	}

	private void set_168_AttachedCardShadow_InnerContentClipMode(object instance, object Value)
	{
		((AttachedCardShadow)instance).InnerContentClipMode = (InnerContentClipMode)Value;
	}

	private object get_169_ColorPickerGridViewItemRadiusSelector_BottomLeftItem(object instance)
	{
		return ((ColorPickerGridViewItemRadiusSelector)instance).BottomLeftItem;
	}

	private void set_169_ColorPickerGridViewItemRadiusSelector_BottomLeftItem(object instance, object Value)
	{
		((ColorPickerGridViewItemRadiusSelector)instance).BottomLeftItem = (Style)Value;
	}

	private object get_170_ColorPickerGridViewItemRadiusSelector_BottomRightItem(object instance)
	{
		return ((ColorPickerGridViewItemRadiusSelector)instance).BottomRightItem;
	}

	private void set_170_ColorPickerGridViewItemRadiusSelector_BottomRightItem(object instance, object Value)
	{
		((ColorPickerGridViewItemRadiusSelector)instance).BottomRightItem = (Style)Value;
	}

	private object get_171_ColorPickerGridViewItemRadiusSelector_MiddleItem(object instance)
	{
		return ((ColorPickerGridViewItemRadiusSelector)instance).MiddleItem;
	}

	private void set_171_ColorPickerGridViewItemRadiusSelector_MiddleItem(object instance, object Value)
	{
		((ColorPickerGridViewItemRadiusSelector)instance).MiddleItem = (Style)Value;
	}

	private object get_172_ColorPickerGridViewItemRadiusSelector_TopLeftItem(object instance)
	{
		return ((ColorPickerGridViewItemRadiusSelector)instance).TopLeftItem;
	}

	private void set_172_ColorPickerGridViewItemRadiusSelector_TopLeftItem(object instance, object Value)
	{
		((ColorPickerGridViewItemRadiusSelector)instance).TopLeftItem = (Style)Value;
	}

	private object get_173_ColorPickerGridViewItemRadiusSelector_TopRightItem(object instance)
	{
		return ((ColorPickerGridViewItemRadiusSelector)instance).TopRightItem;
	}

	private void set_173_ColorPickerGridViewItemRadiusSelector_TopRightItem(object instance, object Value)
	{
		((ColorPickerGridViewItemRadiusSelector)instance).TopRightItem = (Style)Value;
	}

	private object get_174_NumberBadge_Value(object instance)
	{
		return ((NumberBadge)instance).Value;
	}

	private void set_174_NumberBadge_Value(object instance, object Value)
	{
		((NumberBadge)instance).Value = (int)Value;
	}

	private object get_175_BadgeBase_IsSelected(object instance)
	{
		return ((BadgeBase)instance).IsSelected;
	}

	private void set_175_BadgeBase_IsSelected(object instance, object Value)
	{
		((BadgeBase)instance).IsSelected = (bool)Value;
	}

	private object get_176_ContainedButtonBase_IsProgressEnabled(object instance)
	{
		return ((ContainedButtonBase)instance).IsProgressEnabled;
	}

	private void set_176_ContainedButtonBase_IsProgressEnabled(object instance, object Value)
	{
		((ContainedButtonBase)instance).IsProgressEnabled = (bool)Value;
	}

	private object get_177_Interaction_Behaviors(object instance)
	{
		return Interaction.GetBehaviors((DependencyObject)instance);
	}

	private void set_177_Interaction_Behaviors(object instance, object Value)
	{
		Interaction.SetBehaviors((DependencyObject)instance, (BehaviorCollection)Value);
	}

	private object get_178_ProgressCircleIndeterminate_Foreground(object instance)
	{
		return ((ProgressCircleIndeterminate)instance).Foreground;
	}

	private void set_178_ProgressCircleIndeterminate_Foreground(object instance, object Value)
	{
		((ProgressCircleIndeterminate)instance).Foreground = (Brush)Value;
	}

	private object get_179_ProgressCircleIndeterminate_PointForeground(object instance)
	{
		return ((ProgressCircleIndeterminate)instance).PointForeground;
	}

	private void set_179_ProgressCircleIndeterminate_PointForeground(object instance, object Value)
	{
		((ProgressCircleIndeterminate)instance).PointForeground = (Brush)Value;
	}

	private object get_180_ProgressCircle_Size(object instance)
	{
		return ((ProgressCircle)instance).Size;
	}

	private void set_180_ProgressCircle_Size(object instance, object Value)
	{
		((ProgressCircle)instance).Size = (ProgressCircleSize)Value;
	}

	private object get_181_ProgressCircle_Text(object instance)
	{
		return ((ProgressCircle)instance).Text;
	}

	private void set_181_ProgressCircle_Text(object instance, object Value)
	{
		((ProgressCircle)instance).Text = (string)Value;
	}

	private object get_182_Behavior_AssociatedObject(object instance)
	{
		return ((Behavior<FrameworkElement>)instance).AssociatedObject;
	}

	private object get_183_ContainedButton_Type(object instance)
	{
		return ((ContainedButton)instance).Type;
	}

	private void set_183_ContainedButton_Type(object instance, object Value)
	{
		((ContainedButton)instance).Type = (ContainedButtonType)Value;
	}

	private object get_184_ContainedButton_Size(object instance)
	{
		return ((ContainedButton)instance).Size;
	}

	private void set_184_ContainedButton_Size(object instance, object Value)
	{
		((ContainedButton)instance).Size = (ContainedButtonSize)Value;
	}

	private object get_185_ContentButton_Shape(object instance)
	{
		return ((ContentButton)instance).Shape;
	}

	private void set_185_ContentButton_Shape(object instance, object Value)
	{
		((ContentButton)instance).Shape = (ButtonShapeEnum)Value;
	}

	private object get_186_ContentButton_IsPressAndHoldEnabled(object instance)
	{
		return ((ContentButton)instance).IsPressAndHoldEnabled;
	}

	private void set_186_ContentButton_IsPressAndHoldEnabled(object instance, object Value)
	{
		((ContentButton)instance).IsPressAndHoldEnabled = (bool)Value;
	}

	private object get_187_ContentButton_PressAndHoldInterval(object instance)
	{
		return ((ContentButton)instance).PressAndHoldInterval;
	}

	private void set_187_ContentButton_PressAndHoldInterval(object instance, object Value)
	{
		((ContentButton)instance).PressAndHoldInterval = (int)Value;
	}

	private object get_188_ContentToggleButton_Shape(object instance)
	{
		return ((ContentToggleButton)instance).Shape;
	}

	private void set_188_ContentToggleButton_Shape(object instance, object Value)
	{
		((ContentToggleButton)instance).Shape = (ButtonShapeEnum)Value;
	}

	private object get_189_EditButton_Type(object instance)
	{
		return ((EditButton)instance).Type;
	}

	private void set_189_EditButton_Type(object instance, object Value)
	{
		((EditButton)instance).Type = (EditButtonType)Value;
	}

	private object get_190_FloatingActionButton_Visibility(object instance)
	{
		return ((FloatingActionButton)instance).Visibility;
	}

	private void set_190_FloatingActionButton_Visibility(object instance, object Value)
	{
		((FloatingActionButton)instance).Visibility = (Visibility)Value;
	}

	private object get_191_FloatingActionButton_IsBlur(object instance)
	{
		return ((FloatingActionButton)instance).IsBlur;
	}

	private void set_191_FloatingActionButton_IsBlur(object instance, object Value)
	{
		((FloatingActionButton)instance).IsBlur = (bool)Value;
	}

	private object get_192_ElevationCorner_CornerRadius(object instance)
	{
		return ElevationCorner.GetCornerRadius((DependencyObject)instance);
	}

	private void set_192_ElevationCorner_CornerRadius(object instance, object Value)
	{
		ElevationCorner.SetCornerRadius((DependencyObject)instance, (double)Value);
	}

	private object get_193_BlurLayer_LayerContent(object instance)
	{
		return ((BlurLayer)instance).LayerContent;
	}

	private void set_193_BlurLayer_LayerContent(object instance, object Value)
	{
		((BlurLayer)instance).LayerContent = (UIElement)Value;
	}

	private object get_194_BlurLayer_BlurLevel(object instance)
	{
		return ((BlurLayer)instance).BlurLevel;
	}

	private void set_194_BlurLayer_BlurLevel(object instance, object Value)
	{
		((BlurLayer)instance).BlurLevel = (BlurLevel)Value;
	}

	private object get_195_BlurLayer_FallbackBackground(object instance)
	{
		return ((BlurLayer)instance).FallbackBackground;
	}

	private void set_195_BlurLayer_FallbackBackground(object instance, object Value)
	{
		((BlurLayer)instance).FallbackBackground = (Brush)Value;
	}

	private object get_196_BlurLayer_IsBlur(object instance)
	{
		return ((BlurLayer)instance).IsBlur;
	}

	private void set_196_BlurLayer_IsBlur(object instance, object Value)
	{
		((BlurLayer)instance).IsBlur = (bool)Value;
	}

	private object get_197_BlurLayer_Vibrancy(object instance)
	{
		return ((BlurLayer)instance).Vibrancy;
	}

	private void set_197_BlurLayer_Vibrancy(object instance, object Value)
	{
		((BlurLayer)instance).Vibrancy = (Samsung.OneUI.WinUI.Tokens.VibrancyLevel)Value;
	}

	private object get_198_GoToTopButton_IsBlur(object instance)
	{
		return ((GoToTopButton)instance).IsBlur;
	}

	private void set_198_GoToTopButton_IsBlur(object instance, object Value)
	{
		((GoToTopButton)instance).IsBlur = (bool)Value;
	}

	private object get_199_HyperlinkButton_TextTrimming(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.HyperlinkButton)instance).TextTrimming;
	}

	private void set_199_HyperlinkButton_TextTrimming(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.HyperlinkButton)instance).TextTrimming = (TextTrimming)Value;
	}

	private object get_200_HyperlinkButton_IsTextTrimmed(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.HyperlinkButton)instance).IsTextTrimmed;
	}

	private object get_201_ProgressButton_IsProgressEnabled(object instance)
	{
		return ((ProgressButton)instance).IsProgressEnabled;
	}

	private void set_201_ProgressButton_IsProgressEnabled(object instance, object Value)
	{
		((ProgressButton)instance).IsProgressEnabled = (bool)Value;
	}

	private object get_202_ProgressButton_Type(object instance)
	{
		return ((ProgressButton)instance).Type;
	}

	private void set_202_ProgressButton_Type(object instance, object Value)
	{
		((ProgressButton)instance).Type = (ProgressButtonType)Value;
	}

	private object get_203_TooltipForTrimmedTextBlockBehavior_TextBlockName(object instance)
	{
		return ((TooltipForTrimmedTextBlockBehavior)instance).TextBlockName;
	}

	private void set_203_TooltipForTrimmedTextBlockBehavior_TextBlockName(object instance, object Value)
	{
		((TooltipForTrimmedTextBlockBehavior)instance).TextBlockName = (string)Value;
	}

	private object get_204_KeyTime_TimeSpan(object instance)
	{
		return ((KeyTime)instance).TimeSpan;
	}

	private object get_205_CheckBox_Icon(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.CheckBox)instance).Icon;
	}

	private void set_205_CheckBox_Icon(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.CheckBox)instance).Icon = (IconElement)Value;
	}

	private object get_206_CheckBox_IconSvgStyle(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.CheckBox)instance).IconSvgStyle;
	}

	private void set_206_CheckBox_IconSvgStyle(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.CheckBox)instance).IconSvgStyle = (Style)Value;
	}

	private object get_207_CheckBox_Uri(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.CheckBox)instance).Uri;
	}

	private void set_207_CheckBox_Uri(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.CheckBox)instance).Uri = (string)Value;
	}

	private object get_208_CheckBox_Type(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.CheckBox)instance).Type;
	}

	private void set_208_CheckBox_Type(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.CheckBox)instance).Type = (CheckBoxType)Value;
	}

	private object get_209_ChipsItemStyleSelector_CancelBorderStyle(object instance)
	{
		return ((ChipsItemStyleSelector)instance).CancelBorderStyle;
	}

	private void set_209_ChipsItemStyleSelector_CancelBorderStyle(object instance, object Value)
	{
		((ChipsItemStyleSelector)instance).CancelBorderStyle = (Style)Value;
	}

	private object get_210_ChipsItemStyleSelector_CancelStyle(object instance)
	{
		return ((ChipsItemStyleSelector)instance).CancelStyle;
	}

	private void set_210_ChipsItemStyleSelector_CancelStyle(object instance, object Value)
	{
		((ChipsItemStyleSelector)instance).CancelStyle = (Style)Value;
	}

	private object get_211_ChipsItemStyleSelector_MinusBorderStyle(object instance)
	{
		return ((ChipsItemStyleSelector)instance).MinusBorderStyle;
	}

	private void set_211_ChipsItemStyleSelector_MinusBorderStyle(object instance, object Value)
	{
		((ChipsItemStyleSelector)instance).MinusBorderStyle = (Style)Value;
	}

	private object get_212_ChipsItemStyleSelector_MinusStyle(object instance)
	{
		return ((ChipsItemStyleSelector)instance).MinusStyle;
	}

	private void set_212_ChipsItemStyleSelector_MinusStyle(object instance, object Value)
	{
		((ChipsItemStyleSelector)instance).MinusStyle = (Style)Value;
	}

	private object get_213_ChipsItemStyleSelector_NoneBorderStyle(object instance)
	{
		return ((ChipsItemStyleSelector)instance).NoneBorderStyle;
	}

	private void set_213_ChipsItemStyleSelector_NoneBorderStyle(object instance, object Value)
	{
		((ChipsItemStyleSelector)instance).NoneBorderStyle = (Style)Value;
	}

	private object get_214_ChipsItemStyleSelector_NoneStyle(object instance)
	{
		return ((ChipsItemStyleSelector)instance).NoneStyle;
	}

	private void set_214_ChipsItemStyleSelector_NoneStyle(object instance, object Value)
	{
		((ChipsItemStyleSelector)instance).NoneStyle = (Style)Value;
	}

	private object get_215_ChipsItemStyleSelector_TagBorderStyle(object instance)
	{
		return ((ChipsItemStyleSelector)instance).TagBorderStyle;
	}

	private void set_215_ChipsItemStyleSelector_TagBorderStyle(object instance, object Value)
	{
		((ChipsItemStyleSelector)instance).TagBorderStyle = (Style)Value;
	}

	private object get_216_ChipsItemStyleSelector_TagStyle(object instance)
	{
		return ((ChipsItemStyleSelector)instance).TagStyle;
	}

	private void set_216_ChipsItemStyleSelector_TagStyle(object instance, object Value)
	{
		((ChipsItemStyleSelector)instance).TagStyle = (Style)Value;
	}

	private object get_217_CornerRadiusBorderCompensationBehavior_Compensation(object instance)
	{
		return ((CornerRadiusBorderCompensationBehavior)instance).Compensation;
	}

	private void set_217_CornerRadiusBorderCompensationBehavior_Compensation(object instance, object Value)
	{
		((CornerRadiusBorderCompensationBehavior)instance).Compensation = (double)Value;
	}

	private object get_218_ImageIcon_Source(object instance)
	{
		return ((ImageIcon)instance).Source;
	}

	private void set_218_ImageIcon_Source(object instance, object Value)
	{
		((ImageIcon)instance).Source = (ImageSource)Value;
	}

	private object get_219_Color_A(object instance)
	{
		return ((Color)instance).A;
	}

	private void set_219_Color_A(object instance, object Value)
	{
		Color color = (Color)instance;
		color.A = (byte)Value;
	}

	private object get_220_Color_R(object instance)
	{
		return ((Color)instance).R;
	}

	private void set_220_Color_R(object instance, object Value)
	{
		Color color = (Color)instance;
		color.R = (byte)Value;
	}

	private object get_221_Color_G(object instance)
	{
		return ((Color)instance).G;
	}

	private void set_221_Color_G(object instance, object Value)
	{
		Color color = (Color)instance;
		color.G = (byte)Value;
	}

	private object get_222_Color_B(object instance)
	{
		return ((Color)instance).B;
	}

	private void set_222_Color_B(object instance, object Value)
	{
		Color color = (Color)instance;
		color.B = (byte)Value;
	}

	private object get_223_CommandBarButton_LabelVisibility(object instance)
	{
		return ((CommandBarButton)instance).LabelVisibility;
	}

	private void set_223_CommandBarButton_LabelVisibility(object instance, object Value)
	{
		((CommandBarButton)instance).LabelVisibility = (Visibility)Value;
	}

	private object get_224_CommandBarButton_IconSvgStyle(object instance)
	{
		return ((CommandBarButton)instance).IconSvgStyle;
	}

	private void set_224_CommandBarButton_IconSvgStyle(object instance, object Value)
	{
		((CommandBarButton)instance).IconSvgStyle = (Style)Value;
	}

	private object get_225_CommandBar_CurrentItemsMaxWidth(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.CommandBar)instance).CurrentItemsMaxWidth;
	}

	private object get_226_CommandBar_MoreOptionsOverflowItems(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.CommandBar)instance).MoreOptionsOverflowItems;
	}

	private object get_227_CommandBar_BackButtonCommand(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.CommandBar)instance).BackButtonCommand;
	}

	private void set_227_CommandBar_BackButtonCommand(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.CommandBar)instance).BackButtonCommand = (ICommand)Value;
	}

	private object get_228_CommandBar_BackButtonCommandParameter(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.CommandBar)instance).BackButtonCommandParameter;
	}

	private void set_228_CommandBar_BackButtonCommandParameter(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.CommandBar)instance).BackButtonCommandParameter = Value;
	}

	private object get_229_CommandBar_MoreOptionsItems(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.CommandBar)instance).MoreOptionsItems;
	}

	private void set_229_CommandBar_MoreOptionsItems(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.CommandBar)instance).MoreOptionsItems = (ObservableCollection<MenuFlyoutItemBase>)Value;
	}

	private object get_230_CommandBar_IsBackButtonVisible(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.CommandBar)instance).IsBackButtonVisible;
	}

	private void set_230_CommandBar_IsBackButtonVisible(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.CommandBar)instance).IsBackButtonVisible = (bool)Value;
	}

	private object get_231_CommandBar_IsOptionsButtonVisible(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.CommandBar)instance).IsOptionsButtonVisible;
	}

	private void set_231_CommandBar_IsOptionsButtonVisible(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.CommandBar)instance).IsOptionsButtonVisible = (bool)Value;
	}

	private object get_232_CommandBar_BackButtonType(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.CommandBar)instance).BackButtonType;
	}

	private void set_232_CommandBar_BackButtonType(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.CommandBar)instance).BackButtonType = (CommandBarBackButtonType)Value;
	}

	private object get_233_CommandBar_MoreOptionsBadge(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.CommandBar)instance).MoreOptionsBadge;
	}

	private void set_233_CommandBar_MoreOptionsBadge(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.CommandBar)instance).MoreOptionsBadge = (BadgeBase)Value;
	}

	private object get_234_CommandBar_MoreOptionsHorizontalOffset(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.CommandBar)instance).MoreOptionsHorizontalOffset;
	}

	private void set_234_CommandBar_MoreOptionsHorizontalOffset(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.CommandBar)instance).MoreOptionsHorizontalOffset = (double)Value;
	}

	private object get_235_CommandBar_MoreOptionsVerticalOffset(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.CommandBar)instance).MoreOptionsVerticalOffset;
	}

	private void set_235_CommandBar_MoreOptionsVerticalOffset(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.CommandBar)instance).MoreOptionsVerticalOffset = (double)Value;
	}

	private object get_236_CommandBar_MoreOptionsPlacement(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.CommandBar)instance).MoreOptionsPlacement;
	}

	private void set_236_CommandBar_MoreOptionsPlacement(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.CommandBar)instance).MoreOptionsPlacement = (FlyoutPlacementMode?)Value;
	}

	private object get_237_CommandBar_MoreOptionsToolTipContent(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.CommandBar)instance).MoreOptionsToolTipContent;
	}

	private void set_237_CommandBar_MoreOptionsToolTipContent(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.CommandBar)instance).MoreOptionsToolTipContent = (string)Value;
	}

	private object get_238_CommandBar_SubtitleText(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.CommandBar)instance).SubtitleText;
	}

	private void set_238_CommandBar_SubtitleText(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.CommandBar)instance).SubtitleText = (string)Value;
	}

	private object get_239_CommandBar_IsSubtitleVisible(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.CommandBar)instance).IsSubtitleVisible;
	}

	private void set_239_CommandBar_IsSubtitleVisible(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.CommandBar)instance).IsSubtitleVisible = (bool)Value;
	}

	private object get_240_FlexibleSpacingBehavior_FlexibleSpacingTargetContent(object instance)
	{
		return ((FlexibleSpacingBehavior)instance).FlexibleSpacingTargetContent;
	}

	private void set_240_FlexibleSpacingBehavior_FlexibleSpacingTargetContent(object instance, object Value)
	{
		((FlexibleSpacingBehavior)instance).FlexibleSpacingTargetContent = (FrameworkElement)Value;
	}

	private object get_241_FlexibleSpacingBehavior_IsFlexibleSpacing(object instance)
	{
		return ((FlexibleSpacingBehavior)instance).IsFlexibleSpacing;
	}

	private void set_241_FlexibleSpacingBehavior_IsFlexibleSpacing(object instance, object Value)
	{
		((FlexibleSpacingBehavior)instance).IsFlexibleSpacing = (bool)Value;
	}

	private object get_242_FlexibleSpacingBehavior_Type(object instance)
	{
		return ((FlexibleSpacingBehavior)instance).Type;
	}

	private void set_242_FlexibleSpacingBehavior_Type(object instance, object Value)
	{
		((FlexibleSpacingBehavior)instance).Type = (FlexibleSpacingType)Value;
	}

	private object get_243_FlexibleSpacingBehavior_MarginTiny(object instance)
	{
		return ((FlexibleSpacingBehavior)instance).MarginTiny;
	}

	private void set_243_FlexibleSpacingBehavior_MarginTiny(object instance, object Value)
	{
		((FlexibleSpacingBehavior)instance).MarginTiny = (Thickness)Value;
	}

	private object get_244_FlexibleSpacingBehavior_MarginSmall(object instance)
	{
		return ((FlexibleSpacingBehavior)instance).MarginSmall;
	}

	private void set_244_FlexibleSpacingBehavior_MarginSmall(object instance, object Value)
	{
		((FlexibleSpacingBehavior)instance).MarginSmall = (Thickness)Value;
	}

	private object get_245_FlexibleSpacingBehavior_MarginMedium(object instance)
	{
		return ((FlexibleSpacingBehavior)instance).MarginMedium;
	}

	private void set_245_FlexibleSpacingBehavior_MarginMedium(object instance, object Value)
	{
		((FlexibleSpacingBehavior)instance).MarginMedium = (Thickness)Value;
	}

	private object get_246_FlexibleSpacingBehavior_MarginLarge(object instance)
	{
		return ((FlexibleSpacingBehavior)instance).MarginLarge;
	}

	private void set_246_FlexibleSpacingBehavior_MarginLarge(object instance, object Value)
	{
		((FlexibleSpacingBehavior)instance).MarginLarge = (Thickness)Value;
	}

	private object get_247_FlexibleSpacingBehavior_MarginHuge(object instance)
	{
		return ((FlexibleSpacingBehavior)instance).MarginHuge;
	}

	private void set_247_FlexibleSpacingBehavior_MarginHuge(object instance, object Value)
	{
		((FlexibleSpacingBehavior)instance).MarginHuge = (Thickness)Value;
	}

	private object get_248_FlexibleSpacingBehavior_MarginOff(object instance)
	{
		return ((FlexibleSpacingBehavior)instance).MarginOff;
	}

	private void set_248_FlexibleSpacingBehavior_MarginOff(object instance, object Value)
	{
		((FlexibleSpacingBehavior)instance).MarginOff = (Thickness)Value;
	}

	private object get_249_IconButton_IconSvgStyle(object instance)
	{
		return ((IconButton)instance).IconSvgStyle;
	}

	private void set_249_IconButton_IconSvgStyle(object instance, object Value)
	{
		((IconButton)instance).IconSvgStyle = (Style)Value;
	}

	private object get_250_IconButton_LabelVisibility(object instance)
	{
		return ((IconButton)instance).LabelVisibility;
	}

	private void set_250_IconButton_LabelVisibility(object instance, object Value)
	{
		((IconButton)instance).LabelVisibility = (Visibility)Value;
	}

	private object get_251_ListFlyout_IsCommandBarChild(object instance)
	{
		return ((ListFlyout)instance).IsCommandBarChild;
	}

	private void set_251_ListFlyout_IsCommandBarChild(object instance, object Value)
	{
		((ListFlyout)instance).IsCommandBarChild = (bool)Value;
	}

	private object get_252_ListFlyout_HorizontalOffset(object instance)
	{
		return ((ListFlyout)instance).HorizontalOffset;
	}

	private void set_252_ListFlyout_HorizontalOffset(object instance, object Value)
	{
		((ListFlyout)instance).HorizontalOffset = (double)Value;
	}

	private object get_253_ListFlyout_VerticalOffset(object instance)
	{
		return ((ListFlyout)instance).VerticalOffset;
	}

	private void set_253_ListFlyout_VerticalOffset(object instance, object Value)
	{
		((ListFlyout)instance).VerticalOffset = (double)Value;
	}

	private object get_254_ListFlyout_Placement(object instance)
	{
		return ((ListFlyout)instance).Placement;
	}

	private void set_254_ListFlyout_Placement(object instance, object Value)
	{
		((ListFlyout)instance).Placement = (FlyoutPlacementMode)Value;
	}

	private object get_255_ListFlyout_IsBlur(object instance)
	{
		return ((ListFlyout)instance).IsBlur;
	}

	private void set_255_ListFlyout_IsBlur(object instance, object Value)
	{
		((ListFlyout)instance).IsBlur = (bool)Value;
	}

	private object get_256_Tooltip_TextTrimmedEnabled(object instance)
	{
		return Tooltip.GetTextTrimmedEnabled((DependencyObject)instance);
	}

	private void set_256_Tooltip_TextTrimmedEnabled(object instance, object Value)
	{
		Tooltip.SetTextTrimmedEnabled((DependencyObject)instance, (bool)Value);
	}

	private object get_257_CommandBarToggleButton_LabelVisibility(object instance)
	{
		return ((CommandBarToggleButton)instance).LabelVisibility;
	}

	private void set_257_CommandBarToggleButton_LabelVisibility(object instance, object Value)
	{
		((CommandBarToggleButton)instance).LabelVisibility = (Visibility)Value;
	}

	private object get_258_CommandBarToggleButton_IconSvgStyle(object instance)
	{
		return ((CommandBarToggleButton)instance).IconSvgStyle;
	}

	private void set_258_CommandBarToggleButton_IconSvgStyle(object instance, object Value)
	{
		((CommandBarToggleButton)instance).IconSvgStyle = (Style)Value;
	}

	private object get_259_ListFlyoutItem_CommandBarItemOverflowable(object instance)
	{
		return ((ListFlyoutItem)instance).CommandBarItemOverflowable;
	}

	private object get_260_ListFlyoutItem_NotificationBadge(object instance)
	{
		return ((ListFlyoutItem)instance).NotificationBadge;
	}

	private void set_260_ListFlyoutItem_NotificationBadge(object instance, object Value)
	{
		((ListFlyoutItem)instance).NotificationBadge = (BadgeBase)Value;
	}

	private object get_261_DatePickerSpinnerList_Day(object instance)
	{
		return ((DatePickerSpinnerList)instance).Day;
	}

	private void set_261_DatePickerSpinnerList_Day(object instance, object Value)
	{
		((DatePickerSpinnerList)instance).Day = (DatePickerSpinnerListItem)Value;
	}

	private object get_262_DatePickerSpinnerList_Month(object instance)
	{
		return ((DatePickerSpinnerList)instance).Month;
	}

	private void set_262_DatePickerSpinnerList_Month(object instance, object Value)
	{
		((DatePickerSpinnerList)instance).Month = (DatePickerSpinnerListItem)Value;
	}

	private object get_263_DatePickerSpinnerList_Year(object instance)
	{
		return ((DatePickerSpinnerList)instance).Year;
	}

	private void set_263_DatePickerSpinnerList_Year(object instance, object Value)
	{
		((DatePickerSpinnerList)instance).Year = (DatePickerSpinnerListItem)Value;
	}

	private object get_264_DatePickerSpinnerList_EnabledEntranceAnimation(object instance)
	{
		return ((DatePickerSpinnerList)instance).EnabledEntranceAnimation;
	}

	private void set_264_DatePickerSpinnerList_EnabledEntranceAnimation(object instance, object Value)
	{
		((DatePickerSpinnerList)instance).EnabledEntranceAnimation = (bool)Value;
	}

	private object get_265_ScrollList_SelectedTime(object instance)
	{
		return ((ScrollList)instance).SelectedTime;
	}

	private void set_265_ScrollList_SelectedTime(object instance, object Value)
	{
		((ScrollList)instance).SelectedTime = Value;
	}

	private object get_266_ScrollList_TimeItemsSource(object instance)
	{
		return ((ScrollList)instance).TimeItemsSource;
	}

	private void set_266_ScrollList_TimeItemsSource(object instance, object Value)
	{
		((ScrollList)instance).TimeItemsSource = (ObservableCollection<object>)Value;
	}

	private object get_267_ScrollList_InfiniteScroll(object instance)
	{
		return ((ScrollList)instance).InfiniteScroll;
	}

	private void set_267_ScrollList_InfiniteScroll(object instance, object Value)
	{
		((ScrollList)instance).InfiniteScroll = (bool)Value;
	}

	private object get_268_DatePickerSpinnerListItem_TypeDate(object instance)
	{
		return ((DatePickerSpinnerListItem)instance).TypeDate;
	}

	private object get_269_DatePickerSpinnerListItem_Value(object instance)
	{
		return ((DatePickerSpinnerListItem)instance).Value;
	}

	private void set_269_DatePickerSpinnerListItem_Value(object instance, object Value)
	{
		((DatePickerSpinnerListItem)instance).Value = (int)Value;
	}

	private object get_270_DatePickerSpinnerListItem_FormattedValue(object instance)
	{
		return ((DatePickerSpinnerListItem)instance).FormattedValue;
	}

	private void set_270_DatePickerSpinnerListItem_FormattedValue(object instance, object Value)
	{
		((DatePickerSpinnerListItem)instance).FormattedValue = (string)Value;
	}

	private object get_271_PeriodStyleSelector_HiddenStyle(object instance)
	{
		return ((PeriodStyleSelector)instance).HiddenStyle;
	}

	private void set_271_PeriodStyleSelector_HiddenStyle(object instance, object Value)
	{
		((PeriodStyleSelector)instance).HiddenStyle = (Style)Value;
	}

	private object get_272_PeriodStyleSelector_NormalStyle(object instance)
	{
		return ((PeriodStyleSelector)instance).NormalStyle;
	}

	private void set_272_PeriodStyleSelector_NormalStyle(object instance, object Value)
	{
		((PeriodStyleSelector)instance).NormalStyle = (Style)Value;
	}

	private object get_273_PeriodScrollList_VerticalOffSetAnimation(object instance)
	{
		return ((PeriodScrollList)instance).VerticalOffSetAnimation;
	}

	private void set_273_PeriodScrollList_VerticalOffSetAnimation(object instance, object Value)
	{
		((PeriodScrollList)instance).VerticalOffSetAnimation = (double)Value;
	}

	private object get_274_DpiChangedStateTriggerBase_OsVersionExpected(object instance)
	{
		return ((DpiChangedStateTriggerBase)instance).OsVersionExpected;
	}

	private void set_274_DpiChangedStateTriggerBase_OsVersionExpected(object instance, object Value)
	{
		((DpiChangedStateTriggerBase)instance).OsVersionExpected = (OSVersionType)Value;
	}

	private object get_275_ContentDialog_BackgroundDialog(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.ContentDialog)instance).BackgroundDialog;
	}

	private void set_275_ContentDialog_BackgroundDialog(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.ContentDialog)instance).BackgroundDialog = (SolidColorBrush)Value;
	}

	private object get_276_ContentDialog_ContentMargin(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.ContentDialog)instance).ContentMargin;
	}

	private void set_276_ContentDialog_ContentMargin(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.ContentDialog)instance).ContentMargin = (Thickness)Value;
	}

	private object get_277_ContentDialog_DialogMaxHeight(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.ContentDialog)instance).DialogMaxHeight;
	}

	private void set_277_ContentDialog_DialogMaxHeight(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.ContentDialog)instance).DialogMaxHeight = (double)Value;
	}

	private object get_278_ContentDialog_DialogWidth(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.ContentDialog)instance).DialogWidth;
	}

	private void set_278_ContentDialog_DialogWidth(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.ContentDialog)instance).DialogWidth = (double)Value;
	}

	private object get_279_ContentDialog_DialogTitleAlignment(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.ContentDialog)instance).DialogTitleAlignment;
	}

	private void set_279_ContentDialog_DialogTitleAlignment(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.ContentDialog)instance).DialogTitleAlignment = (HorizontalAlignment)Value;
	}

	private object get_280_ContentDialog_IsCloseButtonEnabled(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.ContentDialog)instance).IsCloseButtonEnabled;
	}

	private void set_280_ContentDialog_IsCloseButtonEnabled(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.ContentDialog)instance).IsCloseButtonEnabled = (bool)Value;
	}

	private object get_281_ContentDialog_CustomSmokedBackgroundResourceKey(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.ContentDialog)instance).CustomSmokedBackgroundResourceKey;
	}

	private void set_281_ContentDialog_CustomSmokedBackgroundResourceKey(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.ContentDialog)instance).CustomSmokedBackgroundResourceKey = (string)Value;
	}

	private object get_282_ContentDialog_CustomAppBarMargin(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.ContentDialog)instance).CustomAppBarMargin;
	}

	private void set_282_ContentDialog_CustomAppBarMargin(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.ContentDialog)instance).CustomAppBarMargin = (Thickness)Value;
	}

	private object get_283_ScrollViewer_VerticalScrollBarSpacingFromContent(object instance)
	{
		return Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.GetVerticalScrollBarSpacingFromContent((DependencyObject)instance);
	}

	private void set_283_ScrollViewer_VerticalScrollBarSpacingFromContent(object instance, object Value)
	{
		Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.SetVerticalScrollBarSpacingFromContent((DependencyObject)instance, (GridLength)Value);
	}

	private object get_284_ScrollViewer_IsMaskingRoundCorner(object instance)
	{
		return Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.GetIsMaskingRoundCorner((DependencyObject)instance);
	}

	private void set_284_ScrollViewer_IsMaskingRoundCorner(object instance, object Value)
	{
		Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.SetIsMaskingRoundCorner((DependencyObject)instance, (bool?)Value);
	}

	private object get_285_ScrollViewer_IsFirstScrollAnimation(object instance)
	{
		return Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.GetIsFirstScrollAnimation((DependencyObject)instance);
	}

	private void set_285_ScrollViewer_IsFirstScrollAnimation(object instance, object Value)
	{
		Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.SetIsFirstScrollAnimation((DependencyObject)instance, (bool?)Value);
	}

	private object get_286_ScrollViewer_VerticalScrollBarMargin(object instance)
	{
		return Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.GetVerticalScrollBarMargin((UIElement)instance);
	}

	private void set_286_ScrollViewer_VerticalScrollBarMargin(object instance, object Value)
	{
		Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.SetVerticalScrollBarMargin((UIElement)instance, (double)Value);
	}

	private object get_287_ScrollViewer_MaskingRoundElementReference(object instance)
	{
		return Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.GetMaskingRoundElementReference((DependencyObject)instance);
	}

	private void set_287_ScrollViewer_MaskingRoundElementReference(object instance, object Value)
	{
		Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.SetMaskingRoundElementReference((DependencyObject)instance, (FrameworkElement)Value);
	}

	private object get_288_ScrollViewer_AutoHideVerticalScrollBar(object instance)
	{
		return Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.GetAutoHideVerticalScrollBar((DependencyObject)instance);
	}

	private void set_288_ScrollViewer_AutoHideVerticalScrollBar(object instance, object Value)
	{
		Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.SetAutoHideVerticalScrollBar((DependencyObject)instance, (bool?)Value);
	}

	private object get_289_ScrollViewer_AutoHideHorizontalScrollBar(object instance)
	{
		return Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.GetAutoHideHorizontalScrollBar((DependencyObject)instance);
	}

	private void set_289_ScrollViewer_AutoHideHorizontalScrollBar(object instance, object Value)
	{
		Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.SetAutoHideHorizontalScrollBar((DependencyObject)instance, (bool?)Value);
	}

	private object get_290_ShowVerticalScrollableIndicatorBehavior_BottomIndicator(object instance)
	{
		return ((ShowVerticalScrollableIndicatorBehavior)instance).BottomIndicator;
	}

	private void set_290_ShowVerticalScrollableIndicatorBehavior_BottomIndicator(object instance, object Value)
	{
		((ShowVerticalScrollableIndicatorBehavior)instance).BottomIndicator = (UIElement)Value;
	}

	private object get_291_ShowVerticalScrollableIndicatorBehavior_TargetScrollViewer(object instance)
	{
		return ((ShowVerticalScrollableIndicatorBehavior)instance).TargetScrollViewer;
	}

	private void set_291_ShowVerticalScrollableIndicatorBehavior_TargetScrollViewer(object instance, object Value)
	{
		((ShowVerticalScrollableIndicatorBehavior)instance).TargetScrollViewer = (Microsoft.UI.Xaml.Controls.ScrollViewer)Value;
	}

	private object get_292_ShowVerticalScrollableIndicatorBehavior_TopIndicator(object instance)
	{
		return ((ShowVerticalScrollableIndicatorBehavior)instance).TopIndicator;
	}

	private void set_292_ShowVerticalScrollableIndicatorBehavior_TopIndicator(object instance, object Value)
	{
		((ShowVerticalScrollableIndicatorBehavior)instance).TopIndicator = (UIElement)Value;
	}

	private object get_293_Behavior_AssociatedObject(object instance)
	{
		return ((Behavior<DependencyObject>)instance).AssociatedObject;
	}

	private object get_294_Divider_Type(object instance)
	{
		return ((Divider)instance).Type;
	}

	private void set_294_Divider_Type(object instance, object Value)
	{
		((Divider)instance).Type = (DividerType)Value;
	}

	private object get_295_Divider_Orientation(object instance)
	{
		return ((Divider)instance).Orientation;
	}

	private void set_295_Divider_Orientation(object instance, object Value)
	{
		((Divider)instance).Orientation = (Orientation)Value;
	}

	private object get_296_Divider_HeaderText(object instance)
	{
		return ((Divider)instance).HeaderText;
	}

	private void set_296_Divider_HeaderText(object instance, object Value)
	{
		((Divider)instance).HeaderText = (string)Value;
	}

	private object get_297_DropdownList_ArrowColor(object instance)
	{
		return ((DropdownList)instance).ArrowColor;
	}

	private void set_297_DropdownList_ArrowColor(object instance, object Value)
	{
		((DropdownList)instance).ArrowColor = (SolidColorBrush)Value;
	}

	private object get_298_DropdownList_IsListEnabled(object instance)
	{
		return ((DropdownList)instance).IsListEnabled;
	}

	private void set_298_DropdownList_IsListEnabled(object instance, object Value)
	{
		((DropdownList)instance).IsListEnabled = (bool)Value;
	}

	private object get_299_DropdownList_SelectedIndex(object instance)
	{
		return ((DropdownList)instance).SelectedIndex;
	}

	private void set_299_DropdownList_SelectedIndex(object instance, object Value)
	{
		((DropdownList)instance).SelectedIndex = (int)Value;
	}

	private object get_300_DropdownList_SelectedItem(object instance)
	{
		return ((DropdownList)instance).SelectedItem;
	}

	private void set_300_DropdownList_SelectedItem(object instance, object Value)
	{
		((DropdownList)instance).SelectedItem = Value;
	}

	private object get_301_DropdownList_ItemsSource(object instance)
	{
		return ((DropdownList)instance).ItemsSource;
	}

	private void set_301_DropdownList_ItemsSource(object instance, object Value)
	{
		((DropdownList)instance).ItemsSource = (IList)Value;
	}

	private object get_302_DropdownList_Header(object instance)
	{
		return ((DropdownList)instance).Header;
	}

	private void set_302_DropdownList_Header(object instance, object Value)
	{
		((DropdownList)instance).Header = Value;
	}

	private object get_303_DropdownList_Placeholder(object instance)
	{
		return ((DropdownList)instance).Placeholder;
	}

	private void set_303_DropdownList_Placeholder(object instance, object Value)
	{
		((DropdownList)instance).Placeholder = Value;
	}

	private object get_304_DropdownList_ListTitle(object instance)
	{
		return ((DropdownList)instance).ListTitle;
	}

	private void set_304_DropdownList_ListTitle(object instance, object Value)
	{
		((DropdownList)instance).ListTitle = (string)Value;
	}

	private object get_305_DropdownList_ListTitleVisibility(object instance)
	{
		return ((DropdownList)instance).ListTitleVisibility;
	}

	private void set_305_DropdownList_ListTitleVisibility(object instance, object Value)
	{
		((DropdownList)instance).ListTitleVisibility = (Visibility)Value;
	}

	private object get_306_DropdownList_AppTitleBarHeightOffset(object instance)
	{
		return ((DropdownList)instance).AppTitleBarHeightOffset;
	}

	private void set_306_DropdownList_AppTitleBarHeightOffset(object instance, object Value)
	{
		((DropdownList)instance).AppTitleBarHeightOffset = (int)Value;
	}

	private object get_307_DropdownList_VerticalOffset(object instance)
	{
		return ((DropdownList)instance).VerticalOffset;
	}

	private void set_307_DropdownList_VerticalOffset(object instance, object Value)
	{
		((DropdownList)instance).VerticalOffset = (int)Value;
	}

	private object get_308_DropdownList_HorizontalOffset(object instance)
	{
		return ((DropdownList)instance).HorizontalOffset;
	}

	private void set_308_DropdownList_HorizontalOffset(object instance, object Value)
	{
		((DropdownList)instance).HorizontalOffset = (int)Value;
	}

	private object get_309_DropdownList_DropdownPopupAlignment(object instance)
	{
		return ((DropdownList)instance).DropdownPopupAlignment;
	}

	private void set_309_DropdownList_DropdownPopupAlignment(object instance, object Value)
	{
		((DropdownList)instance).DropdownPopupAlignment = (HorizontalAlignment)Value;
	}

	private object get_310_DropdownList_IsMultilineItem(object instance)
	{
		return ((DropdownList)instance).IsMultilineItem;
	}

	private void set_310_DropdownList_IsMultilineItem(object instance, object Value)
	{
		((DropdownList)instance).IsMultilineItem = (bool)Value;
	}

	private object get_311_DropdownList_Type(object instance)
	{
		return ((DropdownList)instance).Type;
	}

	private void set_311_DropdownList_Type(object instance, object Value)
	{
		((DropdownList)instance).Type = (DropdownListType)Value;
	}

	private object get_312_DropdownList_IsBlur(object instance)
	{
		return ((DropdownList)instance).IsBlur;
	}

	private void set_312_DropdownList_IsBlur(object instance, object Value)
	{
		((DropdownList)instance).IsBlur = (bool)Value;
	}

	private object get_313_DropdownCustomControl_Content(object instance)
	{
		return ((DropdownCustomControl)instance).Content;
	}

	private void set_313_DropdownCustomControl_Content(object instance, object Value)
	{
		((DropdownCustomControl)instance).Content = Value;
	}

	private object get_314_DropdownCustomControl_ArrowColor(object instance)
	{
		return ((DropdownCustomControl)instance).ArrowColor;
	}

	private void set_314_DropdownCustomControl_ArrowColor(object instance, object Value)
	{
		((DropdownCustomControl)instance).ArrowColor = (SolidColorBrush)Value;
	}

	private object get_315_DropdownCustomControl_IsEnabled(object instance)
	{
		return ((DropdownCustomControl)instance).IsEnabled;
	}

	private void set_315_DropdownCustomControl_IsEnabled(object instance, object Value)
	{
		((DropdownCustomControl)instance).IsEnabled = (bool)Value;
	}

	private object get_316_TreeView_ItemContainerStyle(object instance)
	{
		return ((TreeView)instance).ItemContainerStyle;
	}

	private void set_316_TreeView_ItemContainerStyle(object instance, object Value)
	{
		((TreeView)instance).ItemContainerStyle = (Style)Value;
	}

	private object get_317_TreeView_SelectionMode(object instance)
	{
		return ((TreeView)instance).SelectionMode;
	}

	private void set_317_TreeView_SelectionMode(object instance, object Value)
	{
		((TreeView)instance).SelectionMode = (TreeViewSelectionMode)Value;
	}

	private object get_318_TreeView_CanDragItems(object instance)
	{
		return ((TreeView)instance).CanDragItems;
	}

	private void set_318_TreeView_CanDragItems(object instance, object Value)
	{
		((TreeView)instance).CanDragItems = (bool)Value;
	}

	private object get_319_TreeView_CanReorderItems(object instance)
	{
		return ((TreeView)instance).CanReorderItems;
	}

	private void set_319_TreeView_CanReorderItems(object instance, object Value)
	{
		((TreeView)instance).CanReorderItems = (bool)Value;
	}

	private object get_320_TreeView_ItemContainerTransitions(object instance)
	{
		return ((TreeView)instance).ItemContainerTransitions;
	}

	private void set_320_TreeView_ItemContainerTransitions(object instance, object Value)
	{
		((TreeView)instance).ItemContainerTransitions = (TransitionCollection)Value;
	}

	private object get_321_TreeView_ItemContainerStyleSelector(object instance)
	{
		return ((TreeView)instance).ItemContainerStyleSelector;
	}

	private void set_321_TreeView_ItemContainerStyleSelector(object instance, object Value)
	{
		((TreeView)instance).ItemContainerStyleSelector = (Microsoft.UI.Xaml.Controls.StyleSelector)Value;
	}

	private object get_322_TreeView_ItemTemplate(object instance)
	{
		return ((TreeView)instance).ItemTemplate;
	}

	private void set_322_TreeView_ItemTemplate(object instance, object Value)
	{
		((TreeView)instance).ItemTemplate = (DataTemplate)Value;
	}

	private object get_323_TreeView_ItemTemplateSelector(object instance)
	{
		return ((TreeView)instance).ItemTemplateSelector;
	}

	private void set_323_TreeView_ItemTemplateSelector(object instance, object Value)
	{
		((TreeView)instance).ItemTemplateSelector = (DataTemplateSelector)Value;
	}

	private object get_324_TreeView_ItemsSource(object instance)
	{
		return ((TreeView)instance).ItemsSource;
	}

	private void set_324_TreeView_ItemsSource(object instance, object Value)
	{
		((TreeView)instance).ItemsSource = Value;
	}

	private object get_325_TreeView_RootNodes(object instance)
	{
		return ((TreeView)instance).RootNodes;
	}

	private object get_326_TreeViewNode_Children(object instance)
	{
		return ((TreeViewNode)instance).Children;
	}

	private object get_327_TreeViewNode_Content(object instance)
	{
		return ((TreeViewNode)instance).Content;
	}

	private void set_327_TreeViewNode_Content(object instance, object Value)
	{
		((TreeViewNode)instance).Content = Value;
	}

	private object get_328_TreeViewNode_Depth(object instance)
	{
		return ((TreeViewNode)instance).Depth;
	}

	private object get_329_TreeViewNode_HasChildren(object instance)
	{
		return ((TreeViewNode)instance).HasChildren;
	}

	private object get_330_TreeViewNode_HasUnrealizedChildren(object instance)
	{
		return ((TreeViewNode)instance).HasUnrealizedChildren;
	}

	private void set_330_TreeViewNode_HasUnrealizedChildren(object instance, object Value)
	{
		((TreeViewNode)instance).HasUnrealizedChildren = (bool)Value;
	}

	private object get_331_TreeViewNode_IsExpanded(object instance)
	{
		return ((TreeViewNode)instance).IsExpanded;
	}

	private void set_331_TreeViewNode_IsExpanded(object instance, object Value)
	{
		((TreeViewNode)instance).IsExpanded = (bool)Value;
	}

	private object get_332_TreeViewNode_Parent(object instance)
	{
		return ((TreeViewNode)instance).Parent;
	}

	private object get_333_TreeView_SelectedItem(object instance)
	{
		return ((TreeView)instance).SelectedItem;
	}

	private void set_333_TreeView_SelectedItem(object instance, object Value)
	{
		((TreeView)instance).SelectedItem = Value;
	}

	private object get_334_TreeView_SelectedItems(object instance)
	{
		return ((TreeView)instance).SelectedItems;
	}

	private object get_335_TreeView_SelectedNode(object instance)
	{
		return ((TreeView)instance).SelectedNode;
	}

	private void set_335_TreeView_SelectedNode(object instance, object Value)
	{
		((TreeView)instance).SelectedNode = (TreeViewNode)Value;
	}

	private object get_336_TreeView_SelectedNodes(object instance)
	{
		return ((TreeView)instance).SelectedNodes;
	}

	private object get_337_TreeViewItem_CollapsedGlyph(object instance)
	{
		return ((TreeViewItem)instance).CollapsedGlyph;
	}

	private void set_337_TreeViewItem_CollapsedGlyph(object instance, object Value)
	{
		((TreeViewItem)instance).CollapsedGlyph = (string)Value;
	}

	private object get_338_TreeViewItem_ExpandedGlyph(object instance)
	{
		return ((TreeViewItem)instance).ExpandedGlyph;
	}

	private void set_338_TreeViewItem_ExpandedGlyph(object instance, object Value)
	{
		((TreeViewItem)instance).ExpandedGlyph = (string)Value;
	}

	private object get_339_TreeViewItem_GlyphBrush(object instance)
	{
		return ((TreeViewItem)instance).GlyphBrush;
	}

	private void set_339_TreeViewItem_GlyphBrush(object instance, object Value)
	{
		((TreeViewItem)instance).GlyphBrush = (Brush)Value;
	}

	private object get_340_TreeViewItem_GlyphOpacity(object instance)
	{
		return ((TreeViewItem)instance).GlyphOpacity;
	}

	private void set_340_TreeViewItem_GlyphOpacity(object instance, object Value)
	{
		((TreeViewItem)instance).GlyphOpacity = (double)Value;
	}

	private object get_341_TreeViewItem_GlyphSize(object instance)
	{
		return ((TreeViewItem)instance).GlyphSize;
	}

	private void set_341_TreeViewItem_GlyphSize(object instance, object Value)
	{
		((TreeViewItem)instance).GlyphSize = (double)Value;
	}

	private object get_342_TreeViewItem_HasUnrealizedChildren(object instance)
	{
		return ((TreeViewItem)instance).HasUnrealizedChildren;
	}

	private void set_342_TreeViewItem_HasUnrealizedChildren(object instance, object Value)
	{
		((TreeViewItem)instance).HasUnrealizedChildren = (bool)Value;
	}

	private object get_343_TreeViewItem_IsExpanded(object instance)
	{
		return ((TreeViewItem)instance).IsExpanded;
	}

	private void set_343_TreeViewItem_IsExpanded(object instance, object Value)
	{
		((TreeViewItem)instance).IsExpanded = (bool)Value;
	}

	private object get_344_TreeViewItem_ItemsSource(object instance)
	{
		return ((TreeViewItem)instance).ItemsSource;
	}

	private void set_344_TreeViewItem_ItemsSource(object instance, object Value)
	{
		((TreeViewItem)instance).ItemsSource = Value;
	}

	private object get_345_TreeViewItem_TreeViewItemTemplateSettings(object instance)
	{
		return ((TreeViewItem)instance).TreeViewItemTemplateSettings;
	}

	private object get_346_ExpandButton_IsChecked(object instance)
	{
		return ((ExpandButton)instance).IsChecked;
	}

	private void set_346_ExpandButton_IsChecked(object instance, object Value)
	{
		((ExpandButton)instance).IsChecked = (bool)Value;
	}

	private object get_347_FlipViewButton_IsBlur(object instance)
	{
		return ((FlipViewButton)instance).IsBlur;
	}

	private void set_347_FlipViewButton_IsBlur(object instance, object Value)
	{
		((FlipViewButton)instance).IsBlur = (bool)Value;
	}

	private object get_348_FlipView_Orientation(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.FlipView)instance).Orientation;
	}

	private void set_348_FlipView_Orientation(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.FlipView)instance).Orientation = (Orientation)Value;
	}

	private object get_349_FlipView_PreviousButtonHorizontalStyle(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.FlipView)instance).PreviousButtonHorizontalStyle;
	}

	private void set_349_FlipView_PreviousButtonHorizontalStyle(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.FlipView)instance).PreviousButtonHorizontalStyle = (Style)Value;
	}

	private object get_350_FlipView_NextButtonHorizontalStyle(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.FlipView)instance).NextButtonHorizontalStyle;
	}

	private void set_350_FlipView_NextButtonHorizontalStyle(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.FlipView)instance).NextButtonHorizontalStyle = (Style)Value;
	}

	private object get_351_FlipView_PreviousButtonVerticalStyle(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.FlipView)instance).PreviousButtonVerticalStyle;
	}

	private void set_351_FlipView_PreviousButtonVerticalStyle(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.FlipView)instance).PreviousButtonVerticalStyle = (Style)Value;
	}

	private object get_352_FlipView_NextButtonVerticalStyle(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.FlipView)instance).NextButtonVerticalStyle;
	}

	private void set_352_FlipView_NextButtonVerticalStyle(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.FlipView)instance).NextButtonVerticalStyle = (Style)Value;
	}

	private object get_353_FlipView_IsClickable(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.FlipView)instance).IsClickable;
	}

	private void set_353_FlipView_IsClickable(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.FlipView)instance).IsClickable = (bool)Value;
	}

	private object get_354_FlipView_IsBlurButton(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.FlipView)instance).IsBlurButton;
	}

	private void set_354_FlipView_IsBlurButton(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.FlipView)instance).IsBlurButton = (bool)Value;
	}

	private object get_355_IconToggleButton_LabelVisibility(object instance)
	{
		return ((IconToggleButton)instance).LabelVisibility;
	}

	private void set_355_IconToggleButton_LabelVisibility(object instance, object Value)
	{
		((IconToggleButton)instance).LabelVisibility = (Visibility)Value;
	}

	private object get_356_IconToggleButton_IconSvgStyle(object instance)
	{
		return ((IconToggleButton)instance).IconSvgStyle;
	}

	private void set_356_IconToggleButton_IconSvgStyle(object instance, object Value)
	{
		((IconToggleButton)instance).IconSvgStyle = (Style)Value;
	}

	private object get_357_LevelSlider_Levels(object instance)
	{
		return ((LevelSlider)instance).Levels;
	}

	private void set_357_LevelSlider_Levels(object instance, object Value)
	{
		((LevelSlider)instance).Levels = (int)Value;
	}

	private object get_358_LevelBar_Levels(object instance)
	{
		return ((LevelBar)instance).Levels;
	}

	private void set_358_LevelBar_Levels(object instance, object Value)
	{
		((LevelBar)instance).Levels = (int)Value;
	}

	private object get_359_LevelBar_Maximum(object instance)
	{
		return ((LevelBar)instance).Maximum;
	}

	private void set_359_LevelBar_Maximum(object instance, object Value)
	{
		((LevelBar)instance).Maximum = (int)Value;
	}

	private object get_360_LevelBar_Minimum(object instance)
	{
		return ((LevelBar)instance).Minimum;
	}

	private void set_360_LevelBar_Minimum(object instance, object Value)
	{
		((LevelBar)instance).Minimum = (int)Value;
	}

	private object get_361_LevelBar_Value(object instance)
	{
		return ((LevelBar)instance).Value;
	}

	private void set_361_LevelBar_Value(object instance, object Value)
	{
		((LevelBar)instance).Value = (double)Value;
	}

	private object get_362_LevelBar_IsThumbToolTipEnabled(object instance)
	{
		return ((LevelBar)instance).IsThumbToolTipEnabled;
	}

	private void set_362_LevelBar_IsThumbToolTipEnabled(object instance, object Value)
	{
		((LevelBar)instance).IsThumbToolTipEnabled = (bool)Value;
	}

	private object get_363_LevelBar_ThumbToolTipValueConverter(object instance)
	{
		return ((LevelBar)instance).ThumbToolTipValueConverter;
	}

	private void set_363_LevelBar_ThumbToolTipValueConverter(object instance, object Value)
	{
		((LevelBar)instance).ThumbToolTipValueConverter = (IValueConverter)Value;
	}

	private object get_364_ItemsRepeater_ItemTemplate(object instance)
	{
		return ((ItemsRepeater)instance).ItemTemplate;
	}

	private void set_364_ItemsRepeater_ItemTemplate(object instance, object Value)
	{
		((ItemsRepeater)instance).ItemTemplate = Value;
	}

	private object get_365_ItemsRepeater_Layout(object instance)
	{
		return ((ItemsRepeater)instance).Layout;
	}

	private void set_365_ItemsRepeater_Layout(object instance, object Value)
	{
		((ItemsRepeater)instance).Layout = (Layout)Value;
	}

	private object get_366_ItemsRepeater_Background(object instance)
	{
		return ((ItemsRepeater)instance).Background;
	}

	private void set_366_ItemsRepeater_Background(object instance, object Value)
	{
		((ItemsRepeater)instance).Background = (Brush)Value;
	}

	private object get_367_ItemsRepeater_HorizontalCacheLength(object instance)
	{
		return ((ItemsRepeater)instance).HorizontalCacheLength;
	}

	private void set_367_ItemsRepeater_HorizontalCacheLength(object instance, object Value)
	{
		((ItemsRepeater)instance).HorizontalCacheLength = (double)Value;
	}

	private object get_368_ItemsRepeater_ItemTransitionProvider(object instance)
	{
		return ((ItemsRepeater)instance).ItemTransitionProvider;
	}

	private void set_368_ItemsRepeater_ItemTransitionProvider(object instance, object Value)
	{
		((ItemsRepeater)instance).ItemTransitionProvider = (ItemCollectionTransitionProvider)Value;
	}

	private object get_369_ItemsRepeater_ItemsSource(object instance)
	{
		return ((ItemsRepeater)instance).ItemsSource;
	}

	private void set_369_ItemsRepeater_ItemsSource(object instance, object Value)
	{
		((ItemsRepeater)instance).ItemsSource = Value;
	}

	private object get_370_ItemsRepeater_ItemsSourceView(object instance)
	{
		return ((ItemsRepeater)instance).ItemsSourceView;
	}

	private object get_371_ItemsRepeater_VerticalCacheLength(object instance)
	{
		return ((ItemsRepeater)instance).VerticalCacheLength;
	}

	private void set_371_ItemsRepeater_VerticalCacheLength(object instance, object Value)
	{
		((ItemsRepeater)instance).VerticalCacheLength = (double)Value;
	}

	private object get_372_StackLayout_Orientation(object instance)
	{
		return ((StackLayout)instance).Orientation;
	}

	private void set_372_StackLayout_Orientation(object instance, object Value)
	{
		((StackLayout)instance).Orientation = (Orientation)Value;
	}

	private object get_373_StackLayout_Spacing(object instance)
	{
		return ((StackLayout)instance).Spacing;
	}

	private void set_373_StackLayout_Spacing(object instance, object Value)
	{
		((StackLayout)instance).Spacing = (double)Value;
	}

	private object get_374_Layout_IndexBasedLayoutOrientation(object instance)
	{
		return ((Layout)instance).IndexBasedLayoutOrientation;
	}

	private object get_375_UniformGridLayout_MinColumnSpacing(object instance)
	{
		return ((UniformGridLayout)instance).MinColumnSpacing;
	}

	private void set_375_UniformGridLayout_MinColumnSpacing(object instance, object Value)
	{
		((UniformGridLayout)instance).MinColumnSpacing = (double)Value;
	}

	private object get_376_UniformGridLayout_MinItemHeight(object instance)
	{
		return ((UniformGridLayout)instance).MinItemHeight;
	}

	private void set_376_UniformGridLayout_MinItemHeight(object instance, object Value)
	{
		((UniformGridLayout)instance).MinItemHeight = (double)Value;
	}

	private object get_377_UniformGridLayout_MinItemWidth(object instance)
	{
		return ((UniformGridLayout)instance).MinItemWidth;
	}

	private void set_377_UniformGridLayout_MinItemWidth(object instance, object Value)
	{
		((UniformGridLayout)instance).MinItemWidth = (double)Value;
	}

	private object get_378_UniformGridLayout_MinRowSpacing(object instance)
	{
		return ((UniformGridLayout)instance).MinRowSpacing;
	}

	private void set_378_UniformGridLayout_MinRowSpacing(object instance, object Value)
	{
		((UniformGridLayout)instance).MinRowSpacing = (double)Value;
	}

	private object get_379_UniformGridLayout_Orientation(object instance)
	{
		return ((UniformGridLayout)instance).Orientation;
	}

	private void set_379_UniformGridLayout_Orientation(object instance, object Value)
	{
		((UniformGridLayout)instance).Orientation = (Orientation)Value;
	}

	private object get_380_UniformGridLayout_ItemsJustification(object instance)
	{
		return ((UniformGridLayout)instance).ItemsJustification;
	}

	private void set_380_UniformGridLayout_ItemsJustification(object instance, object Value)
	{
		((UniformGridLayout)instance).ItemsJustification = (UniformGridLayoutItemsJustification)Value;
	}

	private object get_381_UniformGridLayout_ItemsStretch(object instance)
	{
		return ((UniformGridLayout)instance).ItemsStretch;
	}

	private void set_381_UniformGridLayout_ItemsStretch(object instance, object Value)
	{
		((UniformGridLayout)instance).ItemsStretch = (UniformGridLayoutItemsStretch)Value;
	}

	private object get_382_UniformGridLayout_MaximumRowsOrColumns(object instance)
	{
		return ((UniformGridLayout)instance).MaximumRowsOrColumns;
	}

	private void set_382_UniformGridLayout_MaximumRowsOrColumns(object instance, object Value)
	{
		((UniformGridLayout)instance).MaximumRowsOrColumns = (int)Value;
	}

	private object get_383_SplitBar_Element(object instance)
	{
		return ((SplitBar)instance).Element;
	}

	private void set_383_SplitBar_Element(object instance, object Value)
	{
		((SplitBar)instance).Element = (UIElement)Value;
	}

	private object get_384_SplitBar_ResizeDirection(object instance)
	{
		return ((SplitBar)instance).ResizeDirection;
	}

	private void set_384_SplitBar_ResizeDirection(object instance, object Value)
	{
		((SplitBar)instance).ResizeDirection = (SplitBar.GridResizeDirection)Value;
	}

	private object get_385_SplitBar_ResizeBehavior(object instance)
	{
		return ((SplitBar)instance).ResizeBehavior;
	}

	private void set_385_SplitBar_ResizeBehavior(object instance, object Value)
	{
		((SplitBar)instance).ResizeBehavior = (SplitBar.GridResizeBehavior)Value;
	}

	private object get_386_SplitBar_GripperForeground(object instance)
	{
		return ((SplitBar)instance).GripperForeground;
	}

	private void set_386_SplitBar_GripperForeground(object instance, object Value)
	{
		((SplitBar)instance).GripperForeground = (Brush)Value;
	}

	private object get_387_SplitBar_ParentLevel(object instance)
	{
		return ((SplitBar)instance).ParentLevel;
	}

	private void set_387_SplitBar_ParentLevel(object instance, object Value)
	{
		((SplitBar)instance).ParentLevel = (int)Value;
	}

	private object get_388_SplitBar_GripperCursor(object instance)
	{
		return ((SplitBar)instance).GripperCursor;
	}

	private void set_388_SplitBar_GripperCursor(object instance, object Value)
	{
		((SplitBar)instance).GripperCursor = (SplitBar.GripperCursorType)Value;
	}

	private object get_389_SplitBar_GripperCustomCursorResource(object instance)
	{
		return ((SplitBar)instance).GripperCustomCursorResource;
	}

	private void set_389_SplitBar_GripperCustomCursorResource(object instance, object Value)
	{
		((SplitBar)instance).GripperCustomCursorResource = (int)Value;
	}

	private object get_390_SplitBar_CursorBehavior(object instance)
	{
		return ((SplitBar)instance).CursorBehavior;
	}

	private void set_390_SplitBar_CursorBehavior(object instance, object Value)
	{
		((SplitBar)instance).CursorBehavior = (SplitBar.SplitterCursorBehavior)Value;
	}

	private object get_391_NavigationView_PaneToggleButtonStyle(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).PaneToggleButtonStyle;
	}

	private void set_391_NavigationView_PaneToggleButtonStyle(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).PaneToggleButtonStyle = (Style)Value;
	}

	private object get_392_NavigationView_OpenPaneLength(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).OpenPaneLength;
	}

	private void set_392_NavigationView_OpenPaneLength(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).OpenPaneLength = (double)Value;
	}

	private object get_393_NavigationView_CompactPaneLength(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).CompactPaneLength;
	}

	private void set_393_NavigationView_CompactPaneLength(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).CompactPaneLength = (double)Value;
	}

	private object get_394_NavigationRail_IsPaneAutoCompactEnabled(object instance)
	{
		return ((NavigationRail)instance).IsPaneAutoCompactEnabled;
	}

	private void set_394_NavigationRail_IsPaneAutoCompactEnabled(object instance, object Value)
	{
		((NavigationRail)instance).IsPaneAutoCompactEnabled = (bool)Value;
	}

	private object get_395_NavigationRail_IsInitialPaneOpen(object instance)
	{
		return ((NavigationRail)instance).IsInitialPaneOpen;
	}

	private void set_395_NavigationRail_IsInitialPaneOpen(object instance, object Value)
	{
		((NavigationRail)instance).IsInitialPaneOpen = (bool?)Value;
	}

	private object get_396_NavigationRail_PaneToggleNotificationBadge(object instance)
	{
		return ((NavigationRail)instance).PaneToggleNotificationBadge;
	}

	private void set_396_NavigationRail_PaneToggleNotificationBadge(object instance, object Value)
	{
		((NavigationRail)instance).PaneToggleNotificationBadge = (BadgeBase)Value;
	}

	private object get_397_NavigationRail_SettingsNavigationItemNotificationBadge(object instance)
	{
		return ((NavigationRail)instance).SettingsNavigationItemNotificationBadge;
	}

	private void set_397_NavigationRail_SettingsNavigationItemNotificationBadge(object instance, object Value)
	{
		((NavigationRail)instance).SettingsNavigationItemNotificationBadge = (BadgeBase)Value;
	}

	private object get_398_NavigationRail_CollapseBreakPoint(object instance)
	{
		return ((NavigationRail)instance).CollapseBreakPoint;
	}

	private void set_398_NavigationRail_CollapseBreakPoint(object instance, object Value)
	{
		((NavigationRail)instance).CollapseBreakPoint = (double)Value;
	}

	private object get_399_NavigationRail_ExpandBreakPoint(object instance)
	{
		return ((NavigationRail)instance).ExpandBreakPoint;
	}

	private void set_399_NavigationRail_ExpandBreakPoint(object instance, object Value)
	{
		((NavigationRail)instance).ExpandBreakPoint = (double)Value;
	}

	private object get_400_NavigationView_AlwaysShowHeader(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).AlwaysShowHeader;
	}

	private void set_400_NavigationView_AlwaysShowHeader(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).AlwaysShowHeader = (bool)Value;
	}

	private object get_401_NavigationView_AutoSuggestBox(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).AutoSuggestBox;
	}

	private void set_401_NavigationView_AutoSuggestBox(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).AutoSuggestBox = (AutoSuggestBox)Value;
	}

	private object get_402_NavigationView_CompactModeThresholdWidth(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).CompactModeThresholdWidth;
	}

	private void set_402_NavigationView_CompactModeThresholdWidth(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).CompactModeThresholdWidth = (double)Value;
	}

	private object get_403_NavigationView_ContentOverlay(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).ContentOverlay;
	}

	private void set_403_NavigationView_ContentOverlay(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).ContentOverlay = (UIElement)Value;
	}

	private object get_404_NavigationView_DisplayMode(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).DisplayMode;
	}

	private object get_405_NavigationView_ExpandedModeThresholdWidth(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).ExpandedModeThresholdWidth;
	}

	private void set_405_NavigationView_ExpandedModeThresholdWidth(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).ExpandedModeThresholdWidth = (double)Value;
	}

	private object get_406_NavigationView_FooterMenuItems(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).FooterMenuItems;
	}

	private object get_407_NavigationView_FooterMenuItemsSource(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).FooterMenuItemsSource;
	}

	private void set_407_NavigationView_FooterMenuItemsSource(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).FooterMenuItemsSource = Value;
	}

	private object get_408_NavigationView_Header(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).Header;
	}

	private void set_408_NavigationView_Header(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).Header = Value;
	}

	private object get_409_NavigationView_HeaderTemplate(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).HeaderTemplate;
	}

	private void set_409_NavigationView_HeaderTemplate(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).HeaderTemplate = (DataTemplate)Value;
	}

	private object get_410_NavigationView_IsBackButtonVisible(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).IsBackButtonVisible;
	}

	private void set_410_NavigationView_IsBackButtonVisible(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).IsBackButtonVisible = (NavigationViewBackButtonVisible)Value;
	}

	private object get_411_NavigationView_IsBackEnabled(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).IsBackEnabled;
	}

	private void set_411_NavigationView_IsBackEnabled(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).IsBackEnabled = (bool)Value;
	}

	private object get_412_NavigationView_IsPaneOpen(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).IsPaneOpen;
	}

	private void set_412_NavigationView_IsPaneOpen(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).IsPaneOpen = (bool)Value;
	}

	private object get_413_NavigationView_IsPaneToggleButtonVisible(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).IsPaneToggleButtonVisible;
	}

	private void set_413_NavigationView_IsPaneToggleButtonVisible(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).IsPaneToggleButtonVisible = (bool)Value;
	}

	private object get_414_NavigationView_IsPaneVisible(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).IsPaneVisible;
	}

	private void set_414_NavigationView_IsPaneVisible(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).IsPaneVisible = (bool)Value;
	}

	private object get_415_NavigationView_IsSettingsVisible(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).IsSettingsVisible;
	}

	private void set_415_NavigationView_IsSettingsVisible(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).IsSettingsVisible = (bool)Value;
	}

	private object get_416_NavigationView_IsTitleBarAutoPaddingEnabled(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).IsTitleBarAutoPaddingEnabled;
	}

	private void set_416_NavigationView_IsTitleBarAutoPaddingEnabled(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).IsTitleBarAutoPaddingEnabled = (bool)Value;
	}

	private object get_417_NavigationView_MenuItemContainerStyle(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).MenuItemContainerStyle;
	}

	private void set_417_NavigationView_MenuItemContainerStyle(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).MenuItemContainerStyle = (Style)Value;
	}

	private object get_418_NavigationView_MenuItemContainerStyleSelector(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).MenuItemContainerStyleSelector;
	}

	private void set_418_NavigationView_MenuItemContainerStyleSelector(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).MenuItemContainerStyleSelector = (Microsoft.UI.Xaml.Controls.StyleSelector)Value;
	}

	private object get_419_NavigationView_MenuItemTemplate(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).MenuItemTemplate;
	}

	private void set_419_NavigationView_MenuItemTemplate(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).MenuItemTemplate = (DataTemplate)Value;
	}

	private object get_420_NavigationView_MenuItemTemplateSelector(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).MenuItemTemplateSelector;
	}

	private void set_420_NavigationView_MenuItemTemplateSelector(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).MenuItemTemplateSelector = (DataTemplateSelector)Value;
	}

	private object get_421_NavigationView_MenuItems(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).MenuItems;
	}

	private object get_422_NavigationView_MenuItemsSource(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).MenuItemsSource;
	}

	private void set_422_NavigationView_MenuItemsSource(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).MenuItemsSource = Value;
	}

	private object get_423_NavigationView_OverflowLabelMode(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).OverflowLabelMode;
	}

	private void set_423_NavigationView_OverflowLabelMode(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).OverflowLabelMode = (NavigationViewOverflowLabelMode)Value;
	}

	private object get_424_NavigationView_PaneCustomContent(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).PaneCustomContent;
	}

	private void set_424_NavigationView_PaneCustomContent(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).PaneCustomContent = (UIElement)Value;
	}

	private object get_425_NavigationView_PaneDisplayMode(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).PaneDisplayMode;
	}

	private void set_425_NavigationView_PaneDisplayMode(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).PaneDisplayMode = (NavigationViewPaneDisplayMode)Value;
	}

	private object get_426_NavigationView_PaneFooter(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).PaneFooter;
	}

	private void set_426_NavigationView_PaneFooter(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).PaneFooter = (UIElement)Value;
	}

	private object get_427_NavigationView_PaneHeader(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).PaneHeader;
	}

	private void set_427_NavigationView_PaneHeader(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).PaneHeader = (UIElement)Value;
	}

	private object get_428_NavigationView_PaneTitle(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).PaneTitle;
	}

	private void set_428_NavigationView_PaneTitle(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).PaneTitle = (string)Value;
	}

	private object get_429_NavigationView_SelectedItem(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).SelectedItem;
	}

	private void set_429_NavigationView_SelectedItem(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).SelectedItem = Value;
	}

	private object get_430_NavigationView_SelectionFollowsFocus(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).SelectionFollowsFocus;
	}

	private void set_430_NavigationView_SelectionFollowsFocus(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).SelectionFollowsFocus = (NavigationViewSelectionFollowsFocus)Value;
	}

	private object get_431_NavigationView_SettingsItem(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).SettingsItem;
	}

	private object get_432_NavigationView_ShoulderNavigationEnabled(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).ShoulderNavigationEnabled;
	}

	private void set_432_NavigationView_ShoulderNavigationEnabled(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationView)instance).ShoulderNavigationEnabled = (NavigationViewShoulderNavigationEnabled)Value;
	}

	private object get_433_NavigationView_TemplateSettings(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationView)instance).TemplateSettings;
	}

	private object get_434_NavigationRailItem_NotificationBadge(object instance)
	{
		return ((NavigationRailItem)instance).NotificationBadge;
	}

	private void set_434_NavigationRailItem_NotificationBadge(object instance, object Value)
	{
		((NavigationRailItem)instance).NotificationBadge = (BadgeBase)Value;
	}

	private object get_435_NavigationRailItem_SvgIconStyle(object instance)
	{
		return ((NavigationRailItem)instance).SvgIconStyle;
	}

	private void set_435_NavigationRailItem_SvgIconStyle(object instance, object Value)
	{
		((NavigationRailItem)instance).SvgIconStyle = (Style)Value;
	}

	private object get_436_NavigationRailItem_PngIconPath(object instance)
	{
		return ((NavigationRailItem)instance).PngIconPath;
	}

	private void set_436_NavigationRailItem_PngIconPath(object instance, object Value)
	{
		((NavigationRailItem)instance).PngIconPath = (string)Value;
	}

	private object get_437_NavigationViewItem_CompactPaneLength(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationViewItem)instance).CompactPaneLength;
	}

	private object get_438_NavigationViewItem_HasUnrealizedChildren(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationViewItem)instance).HasUnrealizedChildren;
	}

	private void set_438_NavigationViewItem_HasUnrealizedChildren(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationViewItem)instance).HasUnrealizedChildren = (bool)Value;
	}

	private object get_439_NavigationViewItem_Icon(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationViewItem)instance).Icon;
	}

	private void set_439_NavigationViewItem_Icon(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationViewItem)instance).Icon = (IconElement)Value;
	}

	private object get_440_NavigationViewItem_InfoBadge(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationViewItem)instance).InfoBadge;
	}

	private void set_440_NavigationViewItem_InfoBadge(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationViewItem)instance).InfoBadge = (InfoBadge)Value;
	}

	private object get_441_NavigationViewItem_IsChildSelected(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationViewItem)instance).IsChildSelected;
	}

	private void set_441_NavigationViewItem_IsChildSelected(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationViewItem)instance).IsChildSelected = (bool)Value;
	}

	private object get_442_NavigationViewItem_IsExpanded(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationViewItem)instance).IsExpanded;
	}

	private void set_442_NavigationViewItem_IsExpanded(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationViewItem)instance).IsExpanded = (bool)Value;
	}

	private object get_443_NavigationViewItem_MenuItems(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationViewItem)instance).MenuItems;
	}

	private object get_444_NavigationViewItem_MenuItemsSource(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationViewItem)instance).MenuItemsSource;
	}

	private void set_444_NavigationViewItem_MenuItemsSource(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationViewItem)instance).MenuItemsSource = Value;
	}

	private object get_445_NavigationViewItem_SelectsOnInvoked(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.NavigationViewItem)instance).SelectsOnInvoked;
	}

	private void set_445_NavigationViewItem_SelectsOnInvoked(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.NavigationViewItem)instance).SelectsOnInvoked = (bool)Value;
	}

	private object get_446_NavigationViewItemBase_IsSelected(object instance)
	{
		return ((NavigationViewItemBase)instance).IsSelected;
	}

	private void set_446_NavigationViewItemBase_IsSelected(object instance, object Value)
	{
		((NavigationViewItemBase)instance).IsSelected = (bool)Value;
	}

	private object get_447_NavigationViewItemPresenter_Icon(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.Primitives.NavigationViewItemPresenter)instance).Icon;
	}

	private void set_447_NavigationViewItemPresenter_Icon(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.Primitives.NavigationViewItemPresenter)instance).Icon = (IconElement)Value;
	}

	private object get_448_NavigationRailItemPresenter_NotificationBadge(object instance)
	{
		return ((NavigationRailItemPresenter)instance).NotificationBadge;
	}

	private void set_448_NavigationRailItemPresenter_NotificationBadge(object instance, object Value)
	{
		((NavigationRailItemPresenter)instance).NotificationBadge = (BadgeBase)Value;
	}

	private object get_449_NavigationRailItemPresenter_PngIconPath(object instance)
	{
		return ((NavigationRailItemPresenter)instance).PngIconPath;
	}

	private void set_449_NavigationRailItemPresenter_PngIconPath(object instance, object Value)
	{
		((NavigationRailItemPresenter)instance).PngIconPath = (string)Value;
	}

	private object get_450_NavigationRailItemPresenter_SvgIconStyle(object instance)
	{
		return ((NavigationRailItemPresenter)instance).SvgIconStyle;
	}

	private void set_450_NavigationRailItemPresenter_SvgIconStyle(object instance, object Value)
	{
		((NavigationRailItemPresenter)instance).SvgIconStyle = (Style)Value;
	}

	private object get_451_NavigationViewItemPresenter_InfoBadge(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.Primitives.NavigationViewItemPresenter)instance).InfoBadge;
	}

	private void set_451_NavigationViewItemPresenter_InfoBadge(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.Primitives.NavigationViewItemPresenter)instance).InfoBadge = (InfoBadge)Value;
	}

	private object get_452_NavigationViewItemPresenter_TemplateSettings(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.Primitives.NavigationViewItemPresenter)instance).TemplateSettings;
	}

	private object get_453_AnimatedIcon_Source(object instance)
	{
		return ((AnimatedIcon)instance).Source;
	}

	private void set_453_AnimatedIcon_Source(object instance, object Value)
	{
		((AnimatedIcon)instance).Source = (IAnimatedVisualSource2)Value;
	}

	private object get_454_AnimatedIcon_FallbackIconSource(object instance)
	{
		return ((AnimatedIcon)instance).FallbackIconSource;
	}

	private void set_454_AnimatedIcon_FallbackIconSource(object instance, object Value)
	{
		((AnimatedIcon)instance).FallbackIconSource = (IconSource)Value;
	}

	private object get_455_AnimatedIcon_MirroredWhenRightToLeft(object instance)
	{
		return ((AnimatedIcon)instance).MirroredWhenRightToLeft;
	}

	private void set_455_AnimatedIcon_MirroredWhenRightToLeft(object instance, object Value)
	{
		((AnimatedIcon)instance).MirroredWhenRightToLeft = (bool)Value;
	}

	private object get_456_AnimatedIcon_State(object instance)
	{
		return AnimatedIcon.GetState((DependencyObject)instance);
	}

	private void set_456_AnimatedIcon_State(object instance, object Value)
	{
		AnimatedIcon.SetState((DependencyObject)instance, (string)Value);
	}

	private object get_457_AnimatedChevronUpDownSmallVisualSource_Markers(object instance)
	{
		return ((AnimatedChevronUpDownSmallVisualSource)instance).Markers;
	}

	private object get_458_ItemsRepeaterScrollHost_ScrollViewer(object instance)
	{
		return ((ItemsRepeaterScrollHost)instance).ScrollViewer;
	}

	private void set_458_ItemsRepeaterScrollHost_ScrollViewer(object instance, object Value)
	{
		((ItemsRepeaterScrollHost)instance).ScrollViewer = (Microsoft.UI.Xaml.Controls.ScrollViewer)Value;
	}

	private object get_459_ItemsRepeaterScrollHost_CurrentAnchor(object instance)
	{
		return ((ItemsRepeaterScrollHost)instance).CurrentAnchor;
	}

	private object get_460_ItemsRepeaterScrollHost_HorizontalAnchorRatio(object instance)
	{
		return ((ItemsRepeaterScrollHost)instance).HorizontalAnchorRatio;
	}

	private void set_460_ItemsRepeaterScrollHost_HorizontalAnchorRatio(object instance, object Value)
	{
		((ItemsRepeaterScrollHost)instance).HorizontalAnchorRatio = (double)Value;
	}

	private object get_461_ItemsRepeaterScrollHost_VerticalAnchorRatio(object instance)
	{
		return ((ItemsRepeaterScrollHost)instance).VerticalAnchorRatio;
	}

	private void set_461_ItemsRepeaterScrollHost_VerticalAnchorRatio(object instance, object Value)
	{
		((ItemsRepeaterScrollHost)instance).VerticalAnchorRatio = (double)Value;
	}

	private object get_462_NavigationView_IsSettingsVisible(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.NavigationView)instance).IsSettingsVisible;
	}

	private void set_462_NavigationView_IsSettingsVisible(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.NavigationView)instance).IsSettingsVisible = (bool)Value;
	}

	private object get_463_NavigationView_IsPaneAutoCompactEnabled(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.NavigationView)instance).IsPaneAutoCompactEnabled;
	}

	private void set_463_NavigationView_IsPaneAutoCompactEnabled(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.NavigationView)instance).IsPaneAutoCompactEnabled = (bool)Value;
	}

	private object get_464_NavigationView_IsInitialPaneOpen(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.NavigationView)instance).IsInitialPaneOpen;
	}

	private void set_464_NavigationView_IsInitialPaneOpen(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.NavigationView)instance).IsInitialPaneOpen = (bool?)Value;
	}

	private object get_465_NavigationView_PaneToggleNotificationBadge(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.NavigationView)instance).PaneToggleNotificationBadge;
	}

	private void set_465_NavigationView_PaneToggleNotificationBadge(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.NavigationView)instance).PaneToggleNotificationBadge = (BadgeBase)Value;
	}

	private object get_466_NavigationView_SettingsNavigationItemNotificationBadge(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.NavigationView)instance).SettingsNavigationItemNotificationBadge;
	}

	private void set_466_NavigationView_SettingsNavigationItemNotificationBadge(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.NavigationView)instance).SettingsNavigationItemNotificationBadge = (BadgeBase)Value;
	}

	private object get_467_NavigationView_CollapseBreakPoint(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.NavigationView)instance).CollapseBreakPoint;
	}

	private void set_467_NavigationView_CollapseBreakPoint(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.NavigationView)instance).CollapseBreakPoint = (double)Value;
	}

	private object get_468_NavigationView_ExpandBreakPoint(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.NavigationView)instance).ExpandBreakPoint;
	}

	private void set_468_NavigationView_ExpandBreakPoint(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.NavigationView)instance).ExpandBreakPoint = (double)Value;
	}

	private object get_469_NavigationView_CompactModeThresholdWidth(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.NavigationView)instance).CompactModeThresholdWidth;
	}

	private void set_469_NavigationView_CompactModeThresholdWidth(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.NavigationView)instance).CompactModeThresholdWidth = (double)Value;
	}

	private object get_470_NavigationView_ExpandedModeThresholdWidth(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.NavigationView)instance).ExpandedModeThresholdWidth;
	}

	private void set_470_NavigationView_ExpandedModeThresholdWidth(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.NavigationView)instance).ExpandedModeThresholdWidth = (double)Value;
	}

	private object get_471_NavigationViewItem_SvgIconStyle(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.NavigationViewItem)instance).SvgIconStyle;
	}

	private void set_471_NavigationViewItem_SvgIconStyle(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.NavigationViewItem)instance).SvgIconStyle = (Style)Value;
	}

	private object get_472_NavigationViewItem_PngIconPath(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.NavigationViewItem)instance).PngIconPath;
	}

	private void set_472_NavigationViewItem_PngIconPath(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.NavigationViewItem)instance).PngIconPath = (string)Value;
	}

	private object get_473_NavigationViewItem_NotificationBadge(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.NavigationViewItem)instance).NotificationBadge;
	}

	private void set_473_NavigationViewItem_NotificationBadge(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.NavigationViewItem)instance).NotificationBadge = (BadgeBase)Value;
	}

	private object get_474_NavigationViewItemPresenter_NotificationBadge(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.NavigationViewItemPresenter)instance).NotificationBadge;
	}

	private void set_474_NavigationViewItemPresenter_NotificationBadge(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.NavigationViewItemPresenter)instance).NotificationBadge = (BadgeBase)Value;
	}

	private object get_475_NavigationViewItemPresenter_PngIconPath(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.NavigationViewItemPresenter)instance).PngIconPath;
	}

	private void set_475_NavigationViewItemPresenter_PngIconPath(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.NavigationViewItemPresenter)instance).PngIconPath = (string)Value;
	}

	private object get_476_NavigationViewItemPresenter_SvgIconStyle(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.NavigationViewItemPresenter)instance).SvgIconStyle;
	}

	private void set_476_NavigationViewItemPresenter_SvgIconStyle(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.NavigationViewItemPresenter)instance).SvgIconStyle = (Style)Value;
	}

	private object get_477_PageIndicator_AutoPlayInterval(object instance)
	{
		return ((PageIndicator)instance).AutoPlayInterval;
	}

	private void set_477_PageIndicator_AutoPlayInterval(object instance, object Value)
	{
		((PageIndicator)instance).AutoPlayInterval = (int)Value;
	}

	private object get_478_PageIndicator_NumberOfPages(object instance)
	{
		return ((PageIndicator)instance).NumberOfPages;
	}

	private void set_478_PageIndicator_NumberOfPages(object instance, object Value)
	{
		((PageIndicator)instance).NumberOfPages = (int)Value;
	}

	private object get_479_PageIndicator_SelectedPageIndex(object instance)
	{
		return ((PageIndicator)instance).SelectedPageIndex;
	}

	private void set_479_PageIndicator_SelectedPageIndex(object instance, object Value)
	{
		((PageIndicator)instance).SelectedPageIndex = (int)Value;
	}

	private object get_480_PageIndicator_MaxVisiblePips(object instance)
	{
		return ((PageIndicator)instance).MaxVisiblePips;
	}

	private void set_480_PageIndicator_MaxVisiblePips(object instance, object Value)
	{
		((PageIndicator)instance).MaxVisiblePips = (int)Value;
	}

	private object get_481_PageIndicator_PreviousButtonVisibility(object instance)
	{
		return ((PageIndicator)instance).PreviousButtonVisibility;
	}

	private void set_481_PageIndicator_PreviousButtonVisibility(object instance, object Value)
	{
		((PageIndicator)instance).PreviousButtonVisibility = (Visibility)Value;
	}

	private object get_482_PageIndicator_NextButtonVisibility(object instance)
	{
		return ((PageIndicator)instance).NextButtonVisibility;
	}

	private void set_482_PageIndicator_NextButtonVisibility(object instance, object Value)
	{
		((PageIndicator)instance).NextButtonVisibility = (Visibility)Value;
	}

	private object get_483_PageIndicator_PlayPauseButtonVisibility(object instance)
	{
		return ((PageIndicator)instance).PlayPauseButtonVisibility;
	}

	private void set_483_PageIndicator_PlayPauseButtonVisibility(object instance, object Value)
	{
		((PageIndicator)instance).PlayPauseButtonVisibility = (Visibility)Value;
	}

	private object get_484_PageIndicator_IsClickActionEnable(object instance)
	{
		return ((PageIndicator)instance).IsClickActionEnable;
	}

	private void set_484_PageIndicator_IsClickActionEnable(object instance, object Value)
	{
		((PageIndicator)instance).IsClickActionEnable = (bool)Value;
	}

	private object get_485_PageIndicator_IsLooping(object instance)
	{
		return ((PageIndicator)instance).IsLooping;
	}

	private void set_485_PageIndicator_IsLooping(object instance, object Value)
	{
		((PageIndicator)instance).IsLooping = (bool)Value;
	}

	private object get_486_ProgressBar_Text(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.ProgressBar)instance).Text;
	}

	private void set_486_ProgressBar_Text(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.ProgressBar)instance).Text = (string)Value;
	}

	private object get_487_ProgressBar_IsIndeterminate(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.ProgressBar)instance).IsIndeterminate;
	}

	private void set_487_ProgressBar_IsIndeterminate(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.ProgressBar)instance).IsIndeterminate = (bool)Value;
	}

	private object get_488_ProgressBar_ShowError(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.ProgressBar)instance).ShowError;
	}

	private void set_488_ProgressBar_ShowError(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.ProgressBar)instance).ShowError = (bool)Value;
	}

	private object get_489_ProgressBar_ShowPaused(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.ProgressBar)instance).ShowPaused;
	}

	private void set_489_ProgressBar_ShowPaused(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.ProgressBar)instance).ShowPaused = (bool)Value;
	}

	private object get_490_ProgressBar_TemplateSettings(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.ProgressBar)instance).TemplateSettings;
	}

	private object get_491_ProgressCircleDeterminate_Foreground(object instance)
	{
		return ((ProgressCircleDeterminate)instance).Foreground;
	}

	private void set_491_ProgressCircleDeterminate_Foreground(object instance, object Value)
	{
		((ProgressCircleDeterminate)instance).Foreground = (Brush)Value;
	}

	private object get_492_ProgressCircleDeterminate_Background(object instance)
	{
		return ((ProgressCircleDeterminate)instance).Background;
	}

	private void set_492_ProgressCircleDeterminate_Background(object instance, object Value)
	{
		((ProgressCircleDeterminate)instance).Background = (Brush)Value;
	}

	private object get_493_ProgressCircleDeterminate_Value(object instance)
	{
		return ((ProgressCircleDeterminate)instance).Value;
	}

	private void set_493_ProgressCircleDeterminate_Value(object instance, object Value)
	{
		((ProgressCircleDeterminate)instance).Value = (double)Value;
	}

	private object get_494_ProgressCircleDeterminate_Type(object instance)
	{
		return ((ProgressCircleDeterminate)instance).Type;
	}

	private void set_494_ProgressCircleDeterminate_Type(object instance, object Value)
	{
		((ProgressCircleDeterminate)instance).Type = (ProgressCircleDeterminateType)Value;
	}

	private object get_495_RadioButtons_Items(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.RadioButtons)instance).Items;
	}

	private object get_496_RadioButtons_Header(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.RadioButtons)instance).Header;
	}

	private void set_496_RadioButtons_Header(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.RadioButtons)instance).Header = Value;
	}

	private object get_497_RadioButtons_HeaderTemplate(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.RadioButtons)instance).HeaderTemplate;
	}

	private void set_497_RadioButtons_HeaderTemplate(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.RadioButtons)instance).HeaderTemplate = (DataTemplate)Value;
	}

	private object get_498_RadioButtons_ItemTemplate(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.RadioButtons)instance).ItemTemplate;
	}

	private void set_498_RadioButtons_ItemTemplate(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.RadioButtons)instance).ItemTemplate = Value;
	}

	private object get_499_RadioButtons_ItemsSource(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.RadioButtons)instance).ItemsSource;
	}

	private void set_499_RadioButtons_ItemsSource(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.RadioButtons)instance).ItemsSource = Value;
	}

	private object get_500_RadioButtons_MaxColumns(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.RadioButtons)instance).MaxColumns;
	}

	private void set_500_RadioButtons_MaxColumns(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.RadioButtons)instance).MaxColumns = (int)Value;
	}

	private object get_501_RadioButtons_SelectedIndex(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.RadioButtons)instance).SelectedIndex;
	}

	private void set_501_RadioButtons_SelectedIndex(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.RadioButtons)instance).SelectedIndex = (int)Value;
	}

	private object get_502_RadioButtons_SelectedItem(object instance)
	{
		return ((Microsoft.UI.Xaml.Controls.RadioButtons)instance).SelectedItem;
	}

	private void set_502_RadioButtons_SelectedItem(object instance, object Value)
	{
		((Microsoft.UI.Xaml.Controls.RadioButtons)instance).SelectedItem = Value;
	}

	private object get_503_GridLength_Value(object instance)
	{
		return ((GridLength)instance).Value;
	}

	private object get_504_GridLength_GridUnitType(object instance)
	{
		return ((GridLength)instance).GridUnitType;
	}

	private object get_505_GridLength_IsAbsolute(object instance)
	{
		return ((GridLength)instance).IsAbsolute;
	}

	private object get_506_GridLength_IsAuto(object instance)
	{
		return ((GridLength)instance).IsAuto;
	}

	private object get_507_GridLength_IsStar(object instance)
	{
		return ((GridLength)instance).IsStar;
	}

	private object get_508_ThumbDisabledScrollBarDimensionsBehavior_LargeRepeatButton(object instance)
	{
		return ((ThumbDisabledScrollBarDimensionsBehavior)instance).LargeRepeatButton;
	}

	private void set_508_ThumbDisabledScrollBarDimensionsBehavior_LargeRepeatButton(object instance, object Value)
	{
		((ThumbDisabledScrollBarDimensionsBehavior)instance).LargeRepeatButton = (RepeatButton)Value;
	}

	private object get_509_ThumbDisabledScrollBarDimensionsBehavior_ScrollBarReference(object instance)
	{
		return ((ThumbDisabledScrollBarDimensionsBehavior)instance).ScrollBarReference;
	}

	private void set_509_ThumbDisabledScrollBarDimensionsBehavior_ScrollBarReference(object instance, object Value)
	{
		((ThumbDisabledScrollBarDimensionsBehavior)instance).ScrollBarReference = (ScrollBar)Value;
	}

	private object get_510_ThumbDisabledScrollBarDimensionsBehavior_SmallRepeatButton(object instance)
	{
		return ((ThumbDisabledScrollBarDimensionsBehavior)instance).SmallRepeatButton;
	}

	private void set_510_ThumbDisabledScrollBarDimensionsBehavior_SmallRepeatButton(object instance, object Value)
	{
		((ThumbDisabledScrollBarDimensionsBehavior)instance).SmallRepeatButton = (RepeatButton)Value;
	}

	private object get_511_Behavior_AssociatedObject(object instance)
	{
		return ((Behavior<Thumb>)instance).AssociatedObject;
	}

	private object get_512_Trigger_Actions(object instance)
	{
		return ((Trigger)instance).Actions;
	}

	private object get_513_DataTriggerBehavior_Binding(object instance)
	{
		return ((DataTriggerBehavior)instance).Binding;
	}

	private void set_513_DataTriggerBehavior_Binding(object instance, object Value)
	{
		((DataTriggerBehavior)instance).Binding = Value;
	}

	private object get_514_DataTriggerBehavior_Value(object instance)
	{
		return ((DataTriggerBehavior)instance).Value;
	}

	private void set_514_DataTriggerBehavior_Value(object instance, object Value)
	{
		((DataTriggerBehavior)instance).Value = Value;
	}

	private object get_515_DataTriggerBehavior_ComparisonCondition(object instance)
	{
		return ((DataTriggerBehavior)instance).ComparisonCondition;
	}

	private void set_515_DataTriggerBehavior_ComparisonCondition(object instance, object Value)
	{
		((DataTriggerBehavior)instance).ComparisonCondition = (ComparisonConditionType)Value;
	}

	private object get_516_Behavior_AssociatedObject(object instance)
	{
		return ((Behavior)instance).AssociatedObject;
	}

	private object get_517_GoToStateAction_StateName(object instance)
	{
		return ((GoToStateAction)instance).StateName;
	}

	private void set_517_GoToStateAction_StateName(object instance, object Value)
	{
		((GoToStateAction)instance).StateName = (string)Value;
	}

	private object get_518_GoToStateAction_TargetObject(object instance)
	{
		return ((GoToStateAction)instance).TargetObject;
	}

	private void set_518_GoToStateAction_TargetObject(object instance, object Value)
	{
		((GoToStateAction)instance).TargetObject = (FrameworkElement)Value;
	}

	private object get_519_GoToStateAction_UseTransitions(object instance)
	{
		return ((GoToStateAction)instance).UseTransitions;
	}

	private void set_519_GoToStateAction_UseTransitions(object instance, object Value)
	{
		((GoToStateAction)instance).UseTransitions = (bool)Value;
	}

	private object get_520_ThumbCompositeTransformScaleStateTrigger_ThumbReference(object instance)
	{
		return ((ThumbCompositeTransformScaleStateTrigger)instance).ThumbReference;
	}

	private void set_520_ThumbCompositeTransformScaleStateTrigger_ThumbReference(object instance, object Value)
	{
		((ThumbCompositeTransformScaleStateTrigger)instance).ThumbReference = (Thumb)Value;
	}

	private object get_521_SearchPopupList_HighlightSearchedWords(object instance)
	{
		return ((SearchPopupList)instance).HighlightSearchedWords;
	}

	private void set_521_SearchPopupList_HighlightSearchedWords(object instance, object Value)
	{
		((SearchPopupList)instance).HighlightSearchedWords = (bool)Value;
	}

	private object get_522_SearchPopupList_TextFilter(object instance)
	{
		return ((SearchPopupList)instance).TextFilter;
	}

	private void set_522_SearchPopupList_TextFilter(object instance, object Value)
	{
		((SearchPopupList)instance).TextFilter = (string)Value;
	}

	private object get_523_SearchPopupList_PopupItems(object instance)
	{
		return ((SearchPopupList)instance).PopupItems;
	}

	private void set_523_SearchPopupList_PopupItems(object instance, object Value)
	{
		((SearchPopupList)instance).PopupItems = (ObservableCollection<SearchPopupListItem>)Value;
	}

	private object get_524_SearchPopupListItem_RemoveButtonTooltipMargin(object instance)
	{
		return ((SearchPopupListItem)instance).RemoveButtonTooltipMargin;
	}

	private void set_524_SearchPopupListItem_RemoveButtonTooltipMargin(object instance, object Value)
	{
		((SearchPopupListItem)instance).RemoveButtonTooltipMargin = (Thickness)Value;
	}

	private object get_525_SearchPopupListItem_RemoveButtonTooltipVerticalOffset(object instance)
	{
		return ((SearchPopupListItem)instance).RemoveButtonTooltipVerticalOffset;
	}

	private void set_525_SearchPopupListItem_RemoveButtonTooltipVerticalOffset(object instance, object Value)
	{
		((SearchPopupListItem)instance).RemoveButtonTooltipVerticalOffset = (double)Value;
	}

	private object get_526_SearchPopupListItem_RemoveButtonTooltipContent(object instance)
	{
		return ((SearchPopupListItem)instance).RemoveButtonTooltipContent;
	}

	private void set_526_SearchPopupListItem_RemoveButtonTooltipContent(object instance, object Value)
	{
		((SearchPopupListItem)instance).RemoveButtonTooltipContent = (string)Value;
	}

	private object get_527_SearchPopupListItem_Id(object instance)
	{
		return ((SearchPopupListItem)instance).Id;
	}

	private void set_527_SearchPopupListItem_Id(object instance, object Value)
	{
		((SearchPopupListItem)instance).Id = (int)Value;
	}

	private object get_528_SearchPopupListItem_RemoveButtonVisibility(object instance)
	{
		return ((SearchPopupListItem)instance).RemoveButtonVisibility;
	}

	private void set_528_SearchPopupListItem_RemoveButtonVisibility(object instance, object Value)
	{
		((SearchPopupListItem)instance).RemoveButtonVisibility = (Visibility)Value;
	}

	private object get_529_SearchPopupListItem_Text(object instance)
	{
		return ((SearchPopupListItem)instance).Text;
	}

	private void set_529_SearchPopupListItem_Text(object instance, object Value)
	{
		((SearchPopupListItem)instance).Text = (string)Value;
	}

	private object get_530_SearchPopupListItem_Image(object instance)
	{
		return ((SearchPopupListItem)instance).Image;
	}

	private void set_530_SearchPopupListItem_Image(object instance, object Value)
	{
		((SearchPopupListItem)instance).Image = (ImageSource)Value;
	}

	private object get_531_SearchPopupListItem_IconSvgStyle(object instance)
	{
		return ((SearchPopupListItem)instance).IconSvgStyle;
	}

	private void set_531_SearchPopupListItem_IconSvgStyle(object instance, object Value)
	{
		((SearchPopupListItem)instance).IconSvgStyle = (Style)Value;
	}

	private object get_532_SearchPopupList_IsCornerRadiusAutoAdjustmentEnabled(object instance)
	{
		return ((SearchPopupList)instance).IsCornerRadiusAutoAdjustmentEnabled;
	}

	private void set_532_SearchPopupList_IsCornerRadiusAutoAdjustmentEnabled(object instance, object Value)
	{
		((SearchPopupList)instance).IsCornerRadiusAutoAdjustmentEnabled = (bool)Value;
	}

	private object get_533_FilterTextBlock_CustomText(object instance)
	{
		return ((FilterTextBlock)instance).CustomText;
	}

	private void set_533_FilterTextBlock_CustomText(object instance, object Value)
	{
		((FilterTextBlock)instance).CustomText = (string)Value;
	}

	private object get_534_FilterTextBlock_TextHighlightBackgroundColor(object instance)
	{
		return ((FilterTextBlock)instance).TextHighlightBackgroundColor;
	}

	private void set_534_FilterTextBlock_TextHighlightBackgroundColor(object instance, object Value)
	{
		((FilterTextBlock)instance).TextHighlightBackgroundColor = (Brush)Value;
	}

	private object get_535_FilterTextBlock_TextHighlightForegroundColor(object instance)
	{
		return ((FilterTextBlock)instance).TextHighlightForegroundColor;
	}

	private void set_535_FilterTextBlock_TextHighlightForegroundColor(object instance, object Value)
	{
		((FilterTextBlock)instance).TextHighlightForegroundColor = (Brush)Value;
	}

	private object get_536_FilterTextBlock_TextTrimming(object instance)
	{
		return ((FilterTextBlock)instance).TextTrimming;
	}

	private void set_536_FilterTextBlock_TextTrimming(object instance, object Value)
	{
		((FilterTextBlock)instance).TextTrimming = (TextTrimming)Value;
	}

	private object get_537_FilterTextBlock_SearchedText(object instance)
	{
		return ((FilterTextBlock)instance).SearchedText;
	}

	private void set_537_FilterTextBlock_SearchedText(object instance, object Value)
	{
		((FilterTextBlock)instance).SearchedText = (string)Value;
	}

	private object get_538_FilterTextBlock_ForceApplyTemplate(object instance)
	{
		return ((FilterTextBlock)instance).ForceApplyTemplate;
	}

	private void set_538_FilterTextBlock_ForceApplyTemplate(object instance, object Value)
	{
		((FilterTextBlock)instance).ForceApplyTemplate = (bool)Value;
	}

	private object get_539_SearchPopup_VerticalOffset(object instance)
	{
		return ((SearchPopup)instance).VerticalOffset;
	}

	private void set_539_SearchPopup_VerticalOffset(object instance, object Value)
	{
		((SearchPopup)instance).VerticalOffset = (int)Value;
	}

	private object get_540_SearchPopup_HorizontalOffset(object instance)
	{
		return ((SearchPopup)instance).HorizontalOffset;
	}

	private void set_540_SearchPopup_HorizontalOffset(object instance, object Value)
	{
		((SearchPopup)instance).HorizontalOffset = (int)Value;
	}

	private object get_541_SearchPopup_PopupContent(object instance)
	{
		return ((SearchPopup)instance).PopupContent;
	}

	private void set_541_SearchPopup_PopupContent(object instance, object Value)
	{
		((SearchPopup)instance).PopupContent = Value;
	}

	private object get_542_SearchPopup_AttachTo(object instance)
	{
		return ((SearchPopup)instance).AttachTo;
	}

	private void set_542_SearchPopup_AttachTo(object instance, object Value)
	{
		((SearchPopup)instance).AttachTo = (Control)Value;
	}

	private object get_543_BackdropBlurExtension_BlurAmount(object instance)
	{
		return BackdropBlurExtension.GetBlurAmount((DependencyObject)instance);
	}

	private void set_543_BackdropBlurExtension_BlurAmount(object instance, object Value)
	{
		BackdropBlurExtension.SetBlurAmount((DependencyObject)instance, (double)Value);
	}

	private object get_544_BackdropBlurExtension_IsEnabled(object instance)
	{
		return BackdropBlurExtension.GetIsEnabled((DependencyObject)instance);
	}

	private void set_544_BackdropBlurExtension_IsEnabled(object instance, object Value)
	{
		BackdropBlurExtension.SetIsEnabled((DependencyObject)instance, (bool)Value);
	}

	private object get_545_Slider_ShockValue(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.Slider)instance).ShockValue;
	}

	private void set_545_Slider_ShockValue(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.Slider)instance).ShockValue = (int)Value;
	}

	private object get_546_Slider_ShockValueType(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.Slider)instance).ShockValueType;
	}

	private void set_546_Slider_ShockValueType(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.Slider)instance).ShockValueType = (ShockValueType)Value;
	}

	private object get_547_Slider_MaximumValue(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.Slider)instance).MaximumValue;
	}

	private void set_547_Slider_MaximumValue(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.Slider)instance).MaximumValue = (double)Value;
	}

	private object get_548_Slider_MinimumValue(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.Slider)instance).MinimumValue;
	}

	private void set_548_Slider_MinimumValue(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.Slider)instance).MinimumValue = (double)Value;
	}

	private object get_549_SliderBase_IsThumbToolTipEnabled(object instance)
	{
		return ((SliderBase)instance).IsThumbToolTipEnabled;
	}

	private void set_549_SliderBase_IsThumbToolTipEnabled(object instance, object Value)
	{
		((SliderBase)instance).IsThumbToolTipEnabled = (bool)Value;
	}

	private object get_550_SliderBase_Type(object instance)
	{
		return ((SliderBase)instance).Type;
	}

	private void set_550_SliderBase_Type(object instance, object Value)
	{
		((SliderBase)instance).Type = (SliderType)Value;
	}

	private object get_551_SliderBase_TextValueVisibility(object instance)
	{
		return ((SliderBase)instance).TextValueVisibility;
	}

	private void set_551_SliderBase_TextValueVisibility(object instance, object Value)
	{
		((SliderBase)instance).TextValueVisibility = (Visibility)Value;
	}

	private object get_552_BufferSlider_Buffer(object instance)
	{
		return ((BufferSlider)instance).Buffer;
	}

	private void set_552_BufferSlider_Buffer(object instance, object Value)
	{
		((BufferSlider)instance).Buffer = (double)Value;
	}

	private object get_553_CenterSlider_Orientation(object instance)
	{
		return ((CenterSlider)instance).Orientation;
	}

	private void set_553_CenterSlider_Orientation(object instance, object Value)
	{
		((CenterSlider)instance).Orientation = (Orientation)Value;
	}

	private object get_554_SubAppBar_Content(object instance)
	{
		return ((SubAppBar)instance).Content;
	}

	private void set_554_SubAppBar_Content(object instance, object Value)
	{
		((SubAppBar)instance).Content = (UIElement)Value;
	}

	private object get_555_TabView_HeaderClipperMargin(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.TabView)instance).HeaderClipperMargin;
	}

	private void set_555_TabView_HeaderClipperMargin(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.TabView)instance).HeaderClipperMargin = (Thickness)Value;
	}

	private object get_556_TabView_Type(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.TabView)instance).Type;
	}

	private void set_556_TabView_Type(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.TabView)instance).Type = (TabViewType)Value;
	}

	private object get_557_TabView_MaxVisibleHeaderInViewport(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.TabView)instance).MaxVisibleHeaderInViewport;
	}

	private void set_557_TabView_MaxVisibleHeaderInViewport(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.TabView)instance).MaxVisibleHeaderInViewport = (int)Value;
	}

	private object get_558_TabItem_SelectedByKeyboard(object instance)
	{
		return ((TabItem)instance).SelectedByKeyboard;
	}

	private object get_559_TabItem_NotificationBadge(object instance)
	{
		return ((TabItem)instance).NotificationBadge;
	}

	private void set_559_TabItem_NotificationBadge(object instance, object Value)
	{
		((TabItem)instance).NotificationBadge = (BadgeBase)Value;
	}

	private object get_560_TextField_Type(object instance)
	{
		return ((TextField)instance).Type;
	}

	private void set_560_TextField_Type(object instance, object Value)
	{
		((TextField)instance).Type = (TextFieldType)Value;
	}

	private object get_561_TextField_ErrorMessage(object instance)
	{
		return ((TextField)instance).ErrorMessage;
	}

	private void set_561_TextField_ErrorMessage(object instance, object Value)
	{
		((TextField)instance).ErrorMessage = (string)Value;
	}

	private object get_562_TextField_SvgIcon(object instance)
	{
		return ((TextField)instance).SvgIcon;
	}

	private void set_562_TextField_SvgIcon(object instance, object Value)
	{
		((TextField)instance).SvgIcon = (Style)Value;
	}

	private object get_563_TextField_ScrollViewerMaxHeight(object instance)
	{
		return ((TextField)instance).ScrollViewerMaxHeight;
	}

	private void set_563_TextField_ScrollViewerMaxHeight(object instance, object Value)
	{
		((TextField)instance).ScrollViewerMaxHeight = (double)Value;
	}

	private object get_564_ThumbnailRadious_ImageLocation(object instance)
	{
		return ((ThumbnailRadious)instance).ImageLocation;
	}

	private void set_564_ThumbnailRadious_ImageLocation(object instance, object Value)
	{
		((ThumbnailRadious)instance).ImageLocation = (string)Value;
	}

	private object get_565_ThumbnailRadious_Title(object instance)
	{
		return ((ThumbnailRadious)instance).Title;
	}

	private void set_565_ThumbnailRadious_Title(object instance, object Value)
	{
		((ThumbnailRadious)instance).Title = (string)Value;
	}

	private object get_566_ThumbnailRadious_Description(object instance)
	{
		return ((ThumbnailRadious)instance).Description;
	}

	private void set_566_ThumbnailRadious_Description(object instance, object Value)
	{
		((ThumbnailRadious)instance).Description = (string)Value;
	}

	private object get_567_ThumbnailRadious_VisualizationMode(object instance)
	{
		return ((ThumbnailRadious)instance).VisualizationMode;
	}

	private void set_567_ThumbnailRadious_VisualizationMode(object instance, object Value)
	{
		((ThumbnailRadious)instance).VisualizationMode = (ThumbnailRadiousVisualizationMode)Value;
	}

	private object get_568_ThumbnailRadiousGridView_VisualizationMode(object instance)
	{
		return ((ThumbnailRadiousGridView)instance).VisualizationMode;
	}

	private void set_568_ThumbnailRadiousGridView_VisualizationMode(object instance, object Value)
	{
		((ThumbnailRadiousGridView)instance).VisualizationMode = (ThumbnailRadiousVisualizationMode)Value;
	}

	private object get_569_Titlebar_Title(object instance)
	{
		return ((Titlebar)instance).Title;
	}

	private void set_569_Titlebar_Title(object instance, object Value)
	{
		((Titlebar)instance).Title = (string)Value;
	}

	private object get_570_ToggleSwitch_Header(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.ToggleSwitch)instance).Header;
	}

	private void set_570_ToggleSwitch_Header(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.ToggleSwitch)instance).Header = (string)Value;
	}

	private object get_571_ToggleSwitch_IsOn(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.ToggleSwitch)instance).IsOn;
	}

	private void set_571_ToggleSwitch_IsOn(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.ToggleSwitch)instance).IsOn = (bool)Value;
	}

	private object get_572_ToggleSwitch_OffContent(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.ToggleSwitch)instance).OffContent;
	}

	private void set_572_ToggleSwitch_OffContent(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.ToggleSwitch)instance).OffContent = (string)Value;
	}

	private object get_573_ToggleSwitch_OnContent(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.ToggleSwitch)instance).OnContent;
	}

	private void set_573_ToggleSwitch_OnContent(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.ToggleSwitch)instance).OnContent = (string)Value;
	}

	private object get_574_ToggleSwitch_Style(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.ToggleSwitch)instance).Style;
	}

	private void set_574_ToggleSwitch_Style(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.ToggleSwitch)instance).Style = (Style)Value;
	}

	private object get_575_ToggleSwitch_Type(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.ToggleSwitch)instance).Type;
	}

	private void set_575_ToggleSwitch_Type(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.ToggleSwitch)instance).Type = (ToggleSwitchType)Value;
	}

	private object get_576_ToggleSwitch_HeaderTemplate(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.ToggleSwitch)instance).HeaderTemplate;
	}

	private void set_576_ToggleSwitch_HeaderTemplate(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.ToggleSwitch)instance).HeaderTemplate = (DataTemplate)Value;
	}

	private object get_577_ToggleSwitch_OnContentTemplate(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.ToggleSwitch)instance).OnContentTemplate;
	}

	private void set_577_ToggleSwitch_OnContentTemplate(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.ToggleSwitch)instance).OnContentTemplate = (DataTemplate)Value;
	}

	private object get_578_ToggleSwitch_OffContentTemplate(object instance)
	{
		return ((Samsung.OneUI.WinUI.Controls.ToggleSwitch)instance).OffContentTemplate;
	}

	private void set_578_ToggleSwitch_OffContentTemplate(object instance, object Value)
	{
		((Samsung.OneUI.WinUI.Controls.ToggleSwitch)instance).OffContentTemplate = (DataTemplate)Value;
	}

	private object get_579_ToggleSwitchGroup_Content(object instance)
	{
		return ((ToggleSwitchGroup)instance).Content;
	}

	private void set_579_ToggleSwitchGroup_Content(object instance, object Value)
	{
		((ToggleSwitchGroup)instance).Content = Value;
	}

	private object get_580_ToggleSwitchGroup_Header(object instance)
	{
		return ((ToggleSwitchGroup)instance).Header;
	}

	private void set_580_ToggleSwitchGroup_Header(object instance, object Value)
	{
		((ToggleSwitchGroup)instance).Header = (string)Value;
	}

	private object get_581_ToggleSwitchGroup_OnContent(object instance)
	{
		return ((ToggleSwitchGroup)instance).OnContent;
	}

	private void set_581_ToggleSwitchGroup_OnContent(object instance, object Value)
	{
		((ToggleSwitchGroup)instance).OnContent = (string)Value;
	}

	private object get_582_ToggleSwitchGroup_OffContent(object instance)
	{
		return ((ToggleSwitchGroup)instance).OffContent;
	}

	private void set_582_ToggleSwitchGroup_OffContent(object instance, object Value)
	{
		((ToggleSwitchGroup)instance).OffContent = (string)Value;
	}

	private object get_583_ToggleSwitchGroup_LabelToggleSwitchGroupStyle(object instance)
	{
		return ((ToggleSwitchGroup)instance).LabelToggleSwitchGroupStyle;
	}

	private void set_583_ToggleSwitchGroup_LabelToggleSwitchGroupStyle(object instance, object Value)
	{
		((ToggleSwitchGroup)instance).LabelToggleSwitchGroupStyle = (Style)Value;
	}

	private object get_584_ToggleSwitchGroup_IsOn(object instance)
	{
		return ((ToggleSwitchGroup)instance).IsOn;
	}

	private void set_584_ToggleSwitchGroup_IsOn(object instance, object Value)
	{
		((ToggleSwitchGroup)instance).IsOn = (bool)Value;
	}

	private object get_585_Behavior_AssociatedObject(object instance)
	{
		return ((Behavior<Microsoft.UI.Xaml.Controls.ToggleSwitch>)instance).AssociatedObject;
	}

	private IXamlMember CreateXamlMember(string longMemberName)
	{
		XamlMember xamlMember = null;
		switch (longMemberName)
		{
		case "Samsung.OneUI.WinUI.Controls.BGBlur.LayerContent":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.BGBlur");
			xamlMember = new XamlMember(this, "LayerContent", "Microsoft.UI.Xaml.UIElement");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_0_BGBlur_LayerContent;
			xamlMember.Setter = set_0_BGBlur_LayerContent;
			break;
		case "Samsung.OneUI.WinUI.Controls.BGBlur.Vibrancy":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.BGBlur");
			xamlMember = new XamlMember(this, "Vibrancy", "Samsung.OneUI.WinUI.Controls.VibrancyLevel");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_1_BGBlur_Vibrancy;
			xamlMember.Setter = set_1_BGBlur_Vibrancy;
			break;
		case "Samsung.OneUI.WinUI.Controls.BGBlur.FallbackBackground":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.BGBlur");
			xamlMember = new XamlMember(this, "FallbackBackground", "Microsoft.UI.Xaml.Media.Brush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_2_BGBlur_FallbackBackground;
			xamlMember.Setter = set_2_BGBlur_FallbackBackground;
			break;
		case "Samsung.OneUI.WinUI.Controls.BGBlur.IsDarkGrayish":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.BGBlur");
			xamlMember = new XamlMember(this, "IsDarkGrayish", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_3_BGBlur_IsDarkGrayish;
			xamlMember.Setter = set_3_BGBlur_IsDarkGrayish;
			break;
		case "Samsung.OneUI.WinUI.Controls.CardType.Title":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CardType");
			xamlMember = new XamlMember(this, "Title", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_4_CardType_Title;
			xamlMember.Setter = set_4_CardType_Title;
			break;
		case "Samsung.OneUI.WinUI.Controls.CardType.ButtonText":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CardType");
			xamlMember = new XamlMember(this, "ButtonText", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_5_CardType_ButtonText;
			xamlMember.Setter = set_5_CardType_ButtonText;
			break;
		case "Samsung.OneUI.WinUI.Controls.CardType.Description":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CardType");
			xamlMember = new XamlMember(this, "Description", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_6_CardType_Description;
			xamlMember.Setter = set_6_CardType_Description;
			break;
		case "Samsung.OneUI.WinUI.Controls.CardType.Image":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CardType");
			xamlMember = new XamlMember(this, "Image", "Microsoft.UI.Xaml.Media.ImageSource");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_7_CardType_Image;
			xamlMember.Setter = set_7_CardType_Image;
			break;
		case "Samsung.OneUI.WinUI.Controls.CardType.SvgImage":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CardType");
			xamlMember = new XamlMember(this, "SvgImage", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_8_CardType_SvgImage;
			xamlMember.Setter = set_8_CardType_SvgImage;
			break;
		case "Samsung.OneUI.WinUI.Controls.CardTypeListView.ItemsSource":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CardTypeListView");
			xamlMember = new XamlMember(this, "ItemsSource", "System.Collections.Generic.List`1<Samsung.OneUI.WinUI.Controls.CardTypeItem>");
			xamlMember.Getter = get_9_CardTypeListView_ItemsSource;
			xamlMember.Setter = set_9_CardTypeListView_ItemsSource;
			break;
		case "Samsung.OneUI.WinUI.Controls.CardTypeItem.Image":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CardTypeItem");
			xamlMember = new XamlMember(this, "Image", "Microsoft.UI.Xaml.Media.ImageSource");
			xamlMember.Getter = get_10_CardTypeItem_Image;
			xamlMember.Setter = set_10_CardTypeItem_Image;
			break;
		case "Samsung.OneUI.WinUI.Controls.CardTypeItem.SvgStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CardTypeItem");
			xamlMember = new XamlMember(this, "SvgStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.Getter = get_11_CardTypeItem_SvgStyle;
			xamlMember.Setter = set_11_CardTypeItem_SvgStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.CardTypeItem.Title":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CardTypeItem");
			xamlMember = new XamlMember(this, "Title", "String");
			xamlMember.Getter = get_12_CardTypeItem_Title;
			xamlMember.Setter = set_12_CardTypeItem_Title;
			break;
		case "Samsung.OneUI.WinUI.Controls.CardTypeItem.Description":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CardTypeItem");
			xamlMember = new XamlMember(this, "Description", "String");
			xamlMember.Getter = get_13_CardTypeItem_Description;
			xamlMember.Setter = set_13_CardTypeItem_Description;
			break;
		case "Samsung.OneUI.WinUI.Controls.CardTypeItem.ButtonText":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CardTypeItem");
			xamlMember = new XamlMember(this, "ButtonText", "String");
			xamlMember.Getter = get_14_CardTypeItem_ButtonText;
			xamlMember.Setter = set_14_CardTypeItem_ButtonText;
			break;
		case "Samsung.OneUI.WinUI.Controls.CardTypeItem.Click_Event":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CardTypeItem");
			xamlMember = new XamlMember(this, "Click_Event", "System.EventHandler");
			xamlMember.Getter = get_15_CardTypeItem_Click_Event;
			xamlMember.Setter = set_15_CardTypeItem_Click_Event;
			break;
		case "Samsung.OneUI.WinUI.Controls.WrapPanel.HorizontalSpacing":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.WrapPanel");
			xamlMember = new XamlMember(this, "HorizontalSpacing", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_16_WrapPanel_HorizontalSpacing;
			xamlMember.Setter = set_16_WrapPanel_HorizontalSpacing;
			break;
		case "Samsung.OneUI.WinUI.Controls.WrapPanel.VerticalSpacing":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.WrapPanel");
			xamlMember = new XamlMember(this, "VerticalSpacing", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_17_WrapPanel_VerticalSpacing;
			xamlMember.Setter = set_17_WrapPanel_VerticalSpacing;
			break;
		case "Samsung.OneUI.WinUI.Controls.Chips.Items":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Chips");
			xamlMember = new XamlMember(this, "Items", "System.Collections.ObjectModel.ObservableCollection`1<Samsung.OneUI.WinUI.Controls.ChipsItem>");
			xamlMember.Getter = get_18_Chips_Items;
			xamlMember.Setter = set_18_Chips_Items;
			break;
		case "Samsung.OneUI.WinUI.Controls.ChipsItem.Title":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ChipsItem");
			xamlMember = new XamlMember(this, "Title", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_19_ChipsItem_Title;
			xamlMember.Setter = set_19_ChipsItem_Title;
			break;
		case "Samsung.OneUI.WinUI.Controls.ChipsItem.Label":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ChipsItem");
			xamlMember = new XamlMember(this, "Label", "Samsung.OneUI.WinUI.Controls.ChipsItemTemplate");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_20_ChipsItem_Label;
			xamlMember.Setter = set_20_ChipsItem_Label;
			break;
		case "Samsung.OneUI.WinUI.Controls.ChipsItem.Id":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ChipsItem");
			xamlMember = new XamlMember(this, "Id", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_21_ChipsItem_Id;
			xamlMember.Setter = set_21_ChipsItem_Id;
			break;
		case "Samsung.OneUI.WinUI.Controls.ChipsItem.Type":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ChipsItem");
			xamlMember = new XamlMember(this, "Type", "Samsung.OneUI.WinUI.Controls.ChipsItemType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_22_ChipsItem_Type;
			xamlMember.Setter = set_22_ChipsItem_Type;
			break;
		case "Samsung.OneUI.WinUI.Controls.ChipsItem.Icon":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ChipsItem");
			xamlMember = new XamlMember(this, "Icon", "Microsoft.UI.Xaml.Media.ImageSource");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_23_ChipsItem_Icon;
			xamlMember.Setter = set_23_ChipsItem_Icon;
			break;
		case "Samsung.OneUI.WinUI.Controls.ChipsItem.IconSvgStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ChipsItem");
			xamlMember = new XamlMember(this, "IconSvgStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_24_ChipsItem_IconSvgStyle;
			xamlMember.Setter = set_24_ChipsItem_IconSvgStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.Chips.SelectionState":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Chips");
			xamlMember = new XamlMember(this, "SelectionState", "Microsoft.UI.Xaml.Controls.ListViewSelectionMode");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_25_Chips_SelectionState;
			xamlMember.Setter = set_25_Chips_SelectionState;
			break;
		case "Samsung.OneUI.WinUI.Controls.Chips.AllLabels":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Chips");
			xamlMember = new XamlMember(this, "AllLabels", "Samsung.OneUI.WinUI.Controls.ChipsItemGroupTemplate");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_26_Chips_AllLabels;
			xamlMember.Setter = set_26_Chips_AllLabels;
			break;
		case "Samsung.OneUI.WinUI.Controls.Toast.Message":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Toast");
			xamlMember = new XamlMember(this, "Message", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_27_Toast_Message;
			xamlMember.Setter = set_27_Toast_Message;
			break;
		case "Samsung.OneUI.WinUI.Controls.Toast.ToastDuration":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Toast");
			xamlMember = new XamlMember(this, "ToastDuration", "Samsung.OneUI.WinUI.Controls.ToastDuration");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_28_Toast_ToastDuration;
			xamlMember.Setter = set_28_Toast_ToastDuration;
			break;
		case "Samsung.OneUI.WinUI.Controls.Toast.Target":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Toast");
			xamlMember = new XamlMember(this, "Target", "Microsoft.UI.Xaml.FrameworkElement");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_29_Toast_Target;
			xamlMember.Setter = set_29_Toast_Target;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl.AlphaSliderValue":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl");
			xamlMember = new XamlMember(this, "AlphaSliderValue", "System.Nullable`1<Double>");
			xamlMember.Getter = get_30_ColorPickerControl_AlphaSliderValue;
			xamlMember.Setter = set_30_ColorPickerControl_AlphaSliderValue;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl.IsColorPickerAlphaSliderEditable":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl");
			xamlMember = new XamlMember(this, "IsColorPickerAlphaSliderEditable", "Boolean");
			xamlMember.Getter = get_31_ColorPickerControl_IsColorPickerAlphaSliderEditable;
			xamlMember.Setter = set_31_ColorPickerControl_IsColorPickerAlphaSliderEditable;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl.IsAlphaSliderVisible":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl");
			xamlMember = new XamlMember(this, "IsAlphaSliderVisible", "Boolean");
			xamlMember.Getter = get_32_ColorPickerControl_IsAlphaSliderVisible;
			xamlMember.Setter = set_32_ColorPickerControl_IsAlphaSliderVisible;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl.IsSaturationSliderVisible":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl");
			xamlMember = new XamlMember(this, "IsSaturationSliderVisible", "Boolean");
			xamlMember.Getter = get_33_ColorPickerControl_IsSaturationSliderVisible;
			xamlMember.Setter = set_33_ColorPickerControl_IsSaturationSliderVisible;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl.SelectedColorDescription":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl");
			xamlMember = new XamlMember(this, "SelectedColorDescription", "String");
			xamlMember.Getter = get_34_ColorPickerControl_SelectedColorDescription;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl.SelectedColor":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl");
			xamlMember = new XamlMember(this, "SelectedColor", "Microsoft.UI.Xaml.Media.SolidColorBrush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_35_ColorPickerControl_SelectedColor;
			xamlMember.Setter = set_35_ColorPickerControl_SelectedColor;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl.IsColorPickerSwatchedSelected":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl");
			xamlMember = new XamlMember(this, "IsColorPickerSwatchedSelected", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_36_ColorPickerControl_IsColorPickerSwatchedSelected;
			xamlMember.Setter = set_36_ColorPickerControl_IsColorPickerSwatchedSelected;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl.SwatchedVisibility":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl");
			xamlMember = new XamlMember(this, "SwatchedVisibility", "Microsoft.UI.Xaml.Visibility");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_37_ColorPickerControl_SwatchedVisibility;
			xamlMember.Setter = set_37_ColorPickerControl_SwatchedVisibility;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl.SpectrumVisibility":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl");
			xamlMember = new XamlMember(this, "SpectrumVisibility", "Microsoft.UI.Xaml.Visibility");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_38_ColorPickerControl_SpectrumVisibility;
			xamlMember.Setter = set_38_ColorPickerControl_SpectrumVisibility;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl.Theme":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl");
			xamlMember = new XamlMember(this, "Theme", "Microsoft.UI.Xaml.ElementTheme");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_39_ColorPickerControl_Theme;
			xamlMember.Setter = set_39_ColorPickerControl_Theme;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl.RecentColors":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerControl");
			xamlMember = new XamlMember(this, "RecentColors", "System.Collections.Generic.List`1<Samsung.OneUI.WinUI.Controls.ColorInfo>");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_40_ColorPickerControl_RecentColors;
			xamlMember.Setter = set_40_ColorPickerControl_RecentColors;
			break;
		case "Samsung.OneUI.WinUI.Controls.ColorInfo.Name":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ColorInfo");
			xamlMember = new XamlMember(this, "Name", "String");
			xamlMember.Getter = get_41_ColorInfo_Name;
			xamlMember.Setter = set_41_ColorInfo_Name;
			break;
		case "Samsung.OneUI.WinUI.Controls.ColorInfo.Description":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ColorInfo");
			xamlMember = new XamlMember(this, "Description", "String");
			xamlMember.Getter = get_42_ColorInfo_Description;
			xamlMember.Setter = set_42_ColorInfo_Description;
			break;
		case "Samsung.OneUI.WinUI.Controls.ColorInfo.HexValue":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ColorInfo");
			xamlMember = new XamlMember(this, "HexValue", "String");
			xamlMember.Getter = get_43_ColorInfo_HexValue;
			xamlMember.Setter = set_43_ColorInfo_HexValue;
			break;
		case "Samsung.OneUI.WinUI.Controls.ColorInfo.ColorBrush":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ColorInfo");
			xamlMember = new XamlMember(this, "ColorBrush", "Microsoft.UI.Xaml.Media.SolidColorBrush");
			xamlMember.Getter = get_44_ColorInfo_ColorBrush;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.FlatButton.Size":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FlatButton");
			xamlMember = new XamlMember(this, "Size", "Samsung.OneUI.WinUI.Controls.FlatButtonSize");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_45_FlatButton_Size;
			xamlMember.Setter = set_45_FlatButton_Size;
			break;
		case "Samsung.OneUI.WinUI.Controls.FlatButton.Type":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FlatButton");
			xamlMember = new XamlMember(this, "Type", "Samsung.OneUI.WinUI.Controls.FlatButtonType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_46_FlatButton_Type;
			xamlMember.Setter = set_46_FlatButton_Type;
			break;
		case "Samsung.OneUI.WinUI.Controls.FlatButtonBase.TextTrimming":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FlatButtonBase");
			xamlMember = new XamlMember(this, "TextTrimming", "Microsoft.UI.Xaml.TextTrimming");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_47_FlatButtonBase_TextTrimming;
			xamlMember.Setter = set_47_FlatButtonBase_TextTrimming;
			break;
		case "Samsung.OneUI.WinUI.Controls.FlatButtonBase.MaxTextLines":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FlatButtonBase");
			xamlMember = new XamlMember(this, "MaxTextLines", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_48_FlatButtonBase_MaxTextLines;
			xamlMember.Setter = set_48_FlatButtonBase_MaxTextLines;
			break;
		case "Samsung.OneUI.WinUI.Controls.FlatButtonBase.IsProgressEnabled":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FlatButtonBase");
			xamlMember = new XamlMember(this, "IsProgressEnabled", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_49_FlatButtonBase_IsProgressEnabled;
			xamlMember.Setter = set_49_FlatButtonBase_IsProgressEnabled;
			break;
		case "Samsung.OneUI.WinUI.Controls.ColorPickerDialog.SelectedColorDescription":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ColorPickerDialog");
			xamlMember = new XamlMember(this, "SelectedColorDescription", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_50_ColorPickerDialog_SelectedColorDescription;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.ColorPickerDialog.SelectedColor":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ColorPickerDialog");
			xamlMember = new XamlMember(this, "SelectedColor", "Microsoft.UI.Xaml.Media.SolidColorBrush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_51_ColorPickerDialog_SelectedColor;
			xamlMember.Setter = set_51_ColorPickerDialog_SelectedColor;
			break;
		case "Samsung.OneUI.WinUI.Controls.ColorPickerDialog.IsColorPickerSwatchedSelected":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ColorPickerDialog");
			xamlMember = new XamlMember(this, "IsColorPickerSwatchedSelected", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_52_ColorPickerDialog_IsColorPickerSwatchedSelected;
			xamlMember.Setter = set_52_ColorPickerDialog_IsColorPickerSwatchedSelected;
			break;
		case "Samsung.OneUI.WinUI.Controls.ColorPickerDialog.PickedColors":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ColorPickerDialog");
			xamlMember = new XamlMember(this, "PickedColors", "System.Collections.Generic.List`1<String>");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_53_ColorPickerDialog_PickedColors;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.ColorPickerDialog.AlphaSliderValue":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ColorPickerDialog");
			xamlMember = new XamlMember(this, "AlphaSliderValue", "System.Nullable`1<Double>");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_54_ColorPickerDialog_AlphaSliderValue;
			xamlMember.Setter = set_54_ColorPickerDialog_AlphaSliderValue;
			break;
		case "Samsung.OneUI.WinUI.Controls.ColorPickerDialog.IsColorPickerAlphaSliderEditable":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ColorPickerDialog");
			xamlMember = new XamlMember(this, "IsColorPickerAlphaSliderEditable", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_55_ColorPickerDialog_IsColorPickerAlphaSliderEditable;
			xamlMember.Setter = set_55_ColorPickerDialog_IsColorPickerAlphaSliderEditable;
			break;
		case "Samsung.OneUI.WinUI.Controls.ColorPickerDialog.IsOpen":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ColorPickerDialog");
			xamlMember = new XamlMember(this, "IsOpen", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_56_ColorPickerDialog_IsOpen;
			xamlMember.Setter = set_56_ColorPickerDialog_IsOpen;
			break;
		case "Samsung.OneUI.WinUI.Controls.ColorPickerDialog.IsAlphaSliderVisible":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ColorPickerDialog");
			xamlMember = new XamlMember(this, "IsAlphaSliderVisible", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_57_ColorPickerDialog_IsAlphaSliderVisible;
			xamlMember.Setter = set_57_ColorPickerDialog_IsAlphaSliderVisible;
			break;
		case "Samsung.OneUI.WinUI.Controls.ColorPickerDialog.IsSaturationSliderVisible":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ColorPickerDialog");
			xamlMember = new XamlMember(this, "IsSaturationSliderVisible", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_58_ColorPickerDialog_IsSaturationSliderVisible;
			xamlMember.Setter = set_58_ColorPickerDialog_IsSaturationSliderVisible;
			break;
		case "Samsung.OneUI.WinUI.Controls.ColorPickerDialog.isDialogViewBoxEnabled":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ColorPickerDialog");
			xamlMember = new XamlMember(this, "isDialogViewBoxEnabled", "Boolean");
			xamlMember.Getter = get_59_ColorPickerDialog_isDialogViewBoxEnabled;
			xamlMember.Setter = set_59_ColorPickerDialog_isDialogViewBoxEnabled;
			break;
		case "Samsung.OneUI.WinUI.Controls.ColorPickerDialog.DialogViewBoxWidth":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ColorPickerDialog");
			xamlMember = new XamlMember(this, "DialogViewBoxWidth", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_60_ColorPickerDialog_DialogViewBoxWidth;
			xamlMember.Setter = set_60_ColorPickerDialog_DialogViewBoxWidth;
			break;
		case "Samsung.OneUI.WinUI.Controls.ColorPickerDialog.DialogViewBoxHeight":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ColorPickerDialog");
			xamlMember = new XamlMember(this, "DialogViewBoxHeight", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_61_ColorPickerDialog_DialogViewBoxHeight;
			xamlMember.Setter = set_61_ColorPickerDialog_DialogViewBoxHeight;
			break;
		case "Samsung.OneUI.WinUI.Controls.ColorPickerDialog.RecentColors":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ColorPickerDialog");
			xamlMember = new XamlMember(this, "RecentColors", "System.Collections.Generic.List`1<Samsung.OneUI.WinUI.Controls.ColorInfo>");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_62_ColorPickerDialog_RecentColors;
			xamlMember.Setter = set_62_ColorPickerDialog_RecentColors;
			break;
		case "Samsung.OneUI.WinUI.Controls.DatePicker.ActualDateTimeScope":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DatePicker");
			xamlMember = new XamlMember(this, "ActualDateTimeScope", "System.DateTime");
			xamlMember.Getter = get_63_DatePicker_ActualDateTimeScope;
			xamlMember.Setter = set_63_DatePicker_ActualDateTimeScope;
			break;
		case "Samsung.OneUI.WinUI.Controls.DatePicker.SelectedDate":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DatePicker");
			xamlMember = new XamlMember(this, "SelectedDate", "System.DateTime");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_64_DatePicker_SelectedDate;
			xamlMember.Setter = set_64_DatePicker_SelectedDate;
			break;
		case "Samsung.OneUI.WinUI.Controls.DatePicker.SundayDayIndicator":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DatePicker");
			xamlMember = new XamlMember(this, "SundayDayIndicator", "Int32");
			xamlMember.Getter = get_65_DatePicker_SundayDayIndicator;
			xamlMember.Setter = set_65_DatePicker_SundayDayIndicator;
			break;
		case "Samsung.OneUI.WinUI.Controls.DatePickerDialogContent.SelectedDate":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DatePickerDialogContent");
			xamlMember = new XamlMember(this, "SelectedDate", "System.DateTime");
			xamlMember.Getter = get_66_DatePickerDialogContent_SelectedDate;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.DateTimePickerList.Date":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DateTimePickerList");
			xamlMember = new XamlMember(this, "Date", "System.DateTime");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_67_DateTimePickerList_Date;
			xamlMember.Setter = set_67_DateTimePickerList_Date;
			break;
		case "Samsung.OneUI.WinUI.Controls.DateTimePickerList.StartRangeDate":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DateTimePickerList");
			xamlMember = new XamlMember(this, "StartRangeDate", "System.Nullable`1<System.DateTime>");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_68_DateTimePickerList_StartRangeDate;
			xamlMember.Setter = set_68_DateTimePickerList_StartRangeDate;
			break;
		case "Samsung.OneUI.WinUI.Controls.DateTimePickerList.RangeDays":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DateTimePickerList");
			xamlMember = new XamlMember(this, "RangeDays", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_69_DateTimePickerList_RangeDays;
			xamlMember.Setter = set_69_DateTimePickerList_RangeDays;
			break;
		case "Samsung.OneUI.WinUI.Controls.TimePickerList.Type":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.TimePickerList");
			xamlMember = new XamlMember(this, "Type", "Samsung.OneUI.WinUI.Controls.TimeType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_70_TimePickerList_Type;
			xamlMember.Setter = set_70_TimePickerList_Type;
			break;
		case "Samsung.OneUI.WinUI.Controls.TimePickerList.Period":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.TimePickerList");
			xamlMember = new XamlMember(this, "Period", "Samsung.OneUI.WinUI.Controls.TimePeriod");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_71_TimePickerList_Period;
			xamlMember.Setter = set_71_TimePickerList_Period;
			break;
		case "Samsung.OneUI.WinUI.Controls.TimePickerList.Hour":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.TimePickerList");
			xamlMember = new XamlMember(this, "Hour", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_72_TimePickerList_Hour;
			xamlMember.Setter = set_72_TimePickerList_Hour;
			break;
		case "Samsung.OneUI.WinUI.Controls.TimePickerList.Minute":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.TimePickerList");
			xamlMember = new XamlMember(this, "Minute", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_73_TimePickerList_Minute;
			xamlMember.Setter = set_73_TimePickerList_Minute;
			break;
		case "Samsung.OneUI.WinUI.Controls.TimePickerList.TimeResult":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.TimePickerList");
			xamlMember = new XamlMember(this, "TimeResult", "TimeSpan");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_74_TimePickerList_TimeResult;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.DateTimePickerDialogContent.DateResult":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DateTimePickerDialogContent");
			xamlMember = new XamlMember(this, "DateResult", "System.DateTime");
			xamlMember.Getter = get_75_DateTimePickerDialogContent_DateResult;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.OneUIContentDialogContent.ScrollViewer":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.OneUIContentDialogContent");
			xamlMember = new XamlMember(this, "ScrollViewer", "Microsoft.UI.Xaml.Controls.ScrollViewer");
			xamlMember.Getter = get_76_OneUIContentDialogContent_ScrollViewer;
			xamlMember.Setter = set_76_OneUIContentDialogContent_ScrollViewer;
			break;
		case "Samsung.OneUI.WinUI.Controls.ListViewCustom.NoItemsText":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ListViewCustom");
			xamlMember = new XamlMember(this, "NoItemsText", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_77_ListViewCustom_NoItemsText;
			xamlMember.Setter = set_77_ListViewCustom_NoItemsText;
			break;
		case "Samsung.OneUI.WinUI.Controls.ListViewCustom.NoItemsDescription":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ListViewCustom");
			xamlMember = new XamlMember(this, "NoItemsDescription", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_78_ListViewCustom_NoItemsDescription;
			xamlMember.Setter = set_78_ListViewCustom_NoItemsDescription;
			break;
		case "Samsung.OneUI.WinUI.Controls.ListViewCustom.CounterText":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ListViewCustom");
			xamlMember = new XamlMember(this, "CounterText", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_79_ListViewCustom_CounterText;
			xamlMember.Setter = set_79_ListViewCustom_CounterText;
			break;
		case "Samsung.OneUI.WinUI.AttachedProperties.Responsiveness.FlexibleSpacingType":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.AttachedProperties.Responsiveness");
			xamlMember = new XamlMember(this, "FlexibleSpacingType", "Samsung.OneUI.WinUI.AttachedProperties.Enums.FlexibleSpacingType");
			xamlMember.SetTargetTypeName("Microsoft.UI.Xaml.UIElement");
			xamlMember.SetIsAttachable();
			xamlMember.Getter = get_80_Responsiveness_FlexibleSpacingType;
			xamlMember.Setter = set_80_Responsiveness_FlexibleSpacingType;
			break;
		case "Samsung.OneUI.WinUI.AttachedProperties.Responsiveness.IsFlexibleSpacing":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.AttachedProperties.Responsiveness");
			xamlMember = new XamlMember(this, "IsFlexibleSpacing", "System.Nullable`1<Boolean>");
			xamlMember.SetTargetTypeName("Microsoft.UI.Xaml.UIElement");
			xamlMember.SetIsAttachable();
			xamlMember.Getter = get_81_Responsiveness_IsFlexibleSpacing;
			xamlMember.Setter = set_81_Responsiveness_IsFlexibleSpacing;
			break;
		case "Samsung.OneUI.WinUI.Controls.SnackBarButton.Type":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SnackBarButton");
			xamlMember = new XamlMember(this, "Type", "Samsung.OneUI.WinUI.Controls.SnackBarButtonType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_82_SnackBarButton_Type;
			xamlMember.Setter = set_82_SnackBarButton_Type;
			break;
		case "Samsung.OneUI.WinUI.Controls.DialogsAndFlyouts.SnackBar.SnackBar.Message":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DialogsAndFlyouts.SnackBar.SnackBar");
			xamlMember = new XamlMember(this, "Message", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_83_SnackBar_Message;
			xamlMember.Setter = set_83_SnackBar_Message;
			break;
		case "Samsung.OneUI.WinUI.Controls.DialogsAndFlyouts.SnackBar.SnackBar.SnackBarDuration":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DialogsAndFlyouts.SnackBar.SnackBar");
			xamlMember = new XamlMember(this, "SnackBarDuration", "Samsung.OneUI.WinUI.Controls.DialogsAndFlyouts.SnackBar.SnackBarDuration");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_84_SnackBar_SnackBarDuration;
			xamlMember.Setter = set_84_SnackBar_SnackBarDuration;
			break;
		case "Samsung.OneUI.WinUI.Controls.DialogsAndFlyouts.SnackBar.SnackBar.Target":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DialogsAndFlyouts.SnackBar.SnackBar");
			xamlMember = new XamlMember(this, "Target", "Microsoft.UI.Xaml.FrameworkElement");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_85_SnackBar_Target;
			xamlMember.Setter = set_85_SnackBar_Target;
			break;
		case "Samsung.OneUI.WinUI.Controls.DialogsAndFlyouts.SnackBar.SnackBar.IsShowButton":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DialogsAndFlyouts.SnackBar.SnackBar");
			xamlMember = new XamlMember(this, "IsShowButton", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_86_SnackBar_IsShowButton;
			xamlMember.Setter = set_86_SnackBar_IsShowButton;
			break;
		case "Samsung.OneUI.WinUI.Controls.DialogsAndFlyouts.SnackBar.SnackBar.ButtonText":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DialogsAndFlyouts.SnackBar.SnackBar");
			xamlMember = new XamlMember(this, "ButtonText", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_87_SnackBar_ButtonText;
			xamlMember.Setter = set_87_SnackBar_ButtonText;
			break;
		case "Samsung.OneUI.WinUI.Controls.TimePickerKeyboard.TimeResult":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.TimePickerKeyboard");
			xamlMember = new XamlMember(this, "TimeResult", "TimeSpan");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_88_TimePickerKeyboard_TimeResult;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.TimePickerKeyboard.Hour":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.TimePickerKeyboard");
			xamlMember = new XamlMember(this, "Hour", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_89_TimePickerKeyboard_Hour;
			xamlMember.Setter = set_89_TimePickerKeyboard_Hour;
			break;
		case "Samsung.OneUI.WinUI.Controls.TimePickerKeyboard.Minute":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.TimePickerKeyboard");
			xamlMember = new XamlMember(this, "Minute", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_90_TimePickerKeyboard_Minute;
			xamlMember.Setter = set_90_TimePickerKeyboard_Minute;
			break;
		case "Samsung.OneUI.WinUI.Controls.TimePickerKeyboard.Type":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.TimePickerKeyboard");
			xamlMember = new XamlMember(this, "Type", "Samsung.OneUI.WinUI.Controls.TimeType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_91_TimePickerKeyboard_Type;
			xamlMember.Setter = set_91_TimePickerKeyboard_Type;
			break;
		case "Samsung.OneUI.WinUI.Controls.TimePickerKeyboardDialogContent.TimerResult":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.TimePickerKeyboardDialogContent");
			xamlMember = new XamlMember(this, "TimerResult", "TimeSpan");
			xamlMember.Getter = get_92_TimePickerKeyboardDialogContent_TimerResult;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.TimePickerListDialogContent.TimerResult":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.TimePickerListDialogContent");
			xamlMember = new XamlMember(this, "TimerResult", "TimeSpan");
			xamlMember.Getter = get_93_TimePickerListDialogContent_TimerResult;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerOption.IsColorPickerSwatchedSelected":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerOption");
			xamlMember = new XamlMember(this, "IsColorPickerSwatchedSelected", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_94_ColorPickerOption_IsColorPickerSwatchedSelected;
			xamlMember.Setter = set_94_ColorPickerOption_IsColorPickerSwatchedSelected;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerDescriptor.SelectedColor":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerDescriptor");
			xamlMember = new XamlMember(this, "SelectedColor", "Microsoft.UI.Xaml.Media.SolidColorBrush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_95_ColorPickerDescriptor_SelectedColor;
			xamlMember.Setter = set_95_ColorPickerDescriptor_SelectedColor;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerDescriptor.PreviousSelectedColor":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerDescriptor");
			xamlMember = new XamlMember(this, "PreviousSelectedColor", "Microsoft.UI.Xaml.Media.SolidColorBrush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_96_ColorPickerDescriptor_PreviousSelectedColor;
			xamlMember.Setter = set_96_ColorPickerDescriptor_PreviousSelectedColor;
			break;
		case "Samsung.OneUI.WinUI.Controls.SubHeader.IsShowDivider":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SubHeader");
			xamlMember = new XamlMember(this, "IsShowDivider", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_97_SubHeader_IsShowDivider;
			xamlMember.Setter = set_97_SubHeader_IsShowDivider;
			break;
		case "Samsung.OneUI.WinUI.Controls.SubHeader.HeaderText":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SubHeader");
			xamlMember = new XamlMember(this, "HeaderText", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_98_SubHeader_HeaderText;
			xamlMember.Setter = set_98_SubHeader_HeaderText;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerHistory.RecentColors":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerHistory");
			xamlMember = new XamlMember(this, "RecentColors", "System.Collections.Generic.List`1<Samsung.OneUI.WinUI.Controls.ColorInfo>");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_99_ColorPickerHistory_RecentColors;
			xamlMember.Setter = set_99_ColorPickerHistory_RecentColors;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerHistory.SelectedColorDescription":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerHistory");
			xamlMember = new XamlMember(this, "SelectedColorDescription", "String");
			xamlMember.Getter = get_100_ColorPickerHistory_SelectedColorDescription;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerHistory.ItemColorBackground":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerHistory");
			xamlMember = new XamlMember(this, "ItemColorBackground", "Microsoft.UI.Xaml.Media.SolidColorBrush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_101_ColorPickerHistory_ItemColorBackground;
			xamlMember.Setter = set_101_ColorPickerHistory_ItemColorBackground;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSwatched.AlphaSliderValue":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSwatched");
			xamlMember = new XamlMember(this, "AlphaSliderValue", "System.Nullable`1<Double>");
			xamlMember.Getter = get_102_ColorPickerSwatched_AlphaSliderValue;
			xamlMember.Setter = set_102_ColorPickerSwatched_AlphaSliderValue;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSwatched.IsColorPickerAlphaSliderEditable":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSwatched");
			xamlMember = new XamlMember(this, "IsColorPickerAlphaSliderEditable", "Boolean");
			xamlMember.Getter = get_103_ColorPickerSwatched_IsColorPickerAlphaSliderEditable;
			xamlMember.Setter = set_103_ColorPickerSwatched_IsColorPickerAlphaSliderEditable;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSwatched.IsAlphaSliderVisible":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSwatched");
			xamlMember = new XamlMember(this, "IsAlphaSliderVisible", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_104_ColorPickerSwatched_IsAlphaSliderVisible;
			xamlMember.Setter = set_104_ColorPickerSwatched_IsAlphaSliderVisible;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSwatched.SelectedColorDescription":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSwatched");
			xamlMember = new XamlMember(this, "SelectedColorDescription", "String");
			xamlMember.Getter = get_105_ColorPickerSwatched_SelectedColorDescription;
			xamlMember.Setter = set_105_ColorPickerSwatched_SelectedColorDescription;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSwatched.SelectedColor":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSwatched");
			xamlMember = new XamlMember(this, "SelectedColor", "Microsoft.UI.Xaml.Media.SolidColorBrush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_106_ColorPickerSwatched_SelectedColor;
			xamlMember.Setter = set_106_ColorPickerSwatched_SelectedColor;
			break;
		case "Microsoft.UI.Xaml.Controls.ColorPicker.Color":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ColorPicker");
			xamlMember = new XamlMember(this, "Color", "Windows.UI.Color");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_107_ColorPicker_Color;
			xamlMember.Setter = set_107_ColorPicker_Color;
			break;
		case "Microsoft.UI.Xaml.Controls.ColorPicker.ColorSpectrumComponents":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ColorPicker");
			xamlMember = new XamlMember(this, "ColorSpectrumComponents", "Microsoft.UI.Xaml.Controls.ColorSpectrumComponents");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_108_ColorPicker_ColorSpectrumComponents;
			xamlMember.Setter = set_108_ColorPicker_ColorSpectrumComponents;
			break;
		case "Microsoft.UI.Xaml.Controls.ColorPicker.ColorSpectrumShape":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ColorPicker");
			xamlMember = new XamlMember(this, "ColorSpectrumShape", "Microsoft.UI.Xaml.Controls.ColorSpectrumShape");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_109_ColorPicker_ColorSpectrumShape;
			xamlMember.Setter = set_109_ColorPicker_ColorSpectrumShape;
			break;
		case "Microsoft.UI.Xaml.Controls.ColorPicker.IsAlphaEnabled":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ColorPicker");
			xamlMember = new XamlMember(this, "IsAlphaEnabled", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_110_ColorPicker_IsAlphaEnabled;
			xamlMember.Setter = set_110_ColorPicker_IsAlphaEnabled;
			break;
		case "Microsoft.UI.Xaml.Controls.ColorPicker.IsAlphaTextInputVisible":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ColorPicker");
			xamlMember = new XamlMember(this, "IsAlphaTextInputVisible", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_111_ColorPicker_IsAlphaTextInputVisible;
			xamlMember.Setter = set_111_ColorPicker_IsAlphaTextInputVisible;
			break;
		case "Microsoft.UI.Xaml.Controls.ColorPicker.IsColorChannelTextInputVisible":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ColorPicker");
			xamlMember = new XamlMember(this, "IsColorChannelTextInputVisible", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_112_ColorPicker_IsColorChannelTextInputVisible;
			xamlMember.Setter = set_112_ColorPicker_IsColorChannelTextInputVisible;
			break;
		case "Microsoft.UI.Xaml.Controls.ColorPicker.IsColorPreviewVisible":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ColorPicker");
			xamlMember = new XamlMember(this, "IsColorPreviewVisible", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_113_ColorPicker_IsColorPreviewVisible;
			xamlMember.Setter = set_113_ColorPicker_IsColorPreviewVisible;
			break;
		case "Microsoft.UI.Xaml.Controls.ColorPicker.IsColorSliderVisible":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ColorPicker");
			xamlMember = new XamlMember(this, "IsColorSliderVisible", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_114_ColorPicker_IsColorSliderVisible;
			xamlMember.Setter = set_114_ColorPicker_IsColorSliderVisible;
			break;
		case "Microsoft.UI.Xaml.Controls.ColorPicker.IsColorSpectrumVisible":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ColorPicker");
			xamlMember = new XamlMember(this, "IsColorSpectrumVisible", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_115_ColorPicker_IsColorSpectrumVisible;
			xamlMember.Setter = set_115_ColorPicker_IsColorSpectrumVisible;
			break;
		case "Microsoft.UI.Xaml.Controls.ColorPicker.IsHexInputVisible":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ColorPicker");
			xamlMember = new XamlMember(this, "IsHexInputVisible", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_116_ColorPicker_IsHexInputVisible;
			xamlMember.Setter = set_116_ColorPicker_IsHexInputVisible;
			break;
		case "Microsoft.UI.Xaml.Controls.ColorPicker.IsMoreButtonVisible":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ColorPicker");
			xamlMember = new XamlMember(this, "IsMoreButtonVisible", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_117_ColorPicker_IsMoreButtonVisible;
			xamlMember.Setter = set_117_ColorPicker_IsMoreButtonVisible;
			break;
		case "Microsoft.UI.Xaml.Controls.ColorPicker.MaxHue":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ColorPicker");
			xamlMember = new XamlMember(this, "MaxHue", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_118_ColorPicker_MaxHue;
			xamlMember.Setter = set_118_ColorPicker_MaxHue;
			break;
		case "Microsoft.UI.Xaml.Controls.ColorPicker.MaxSaturation":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ColorPicker");
			xamlMember = new XamlMember(this, "MaxSaturation", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_119_ColorPicker_MaxSaturation;
			xamlMember.Setter = set_119_ColorPicker_MaxSaturation;
			break;
		case "Microsoft.UI.Xaml.Controls.ColorPicker.MaxValue":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ColorPicker");
			xamlMember = new XamlMember(this, "MaxValue", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_120_ColorPicker_MaxValue;
			xamlMember.Setter = set_120_ColorPicker_MaxValue;
			break;
		case "Microsoft.UI.Xaml.Controls.ColorPicker.MinHue":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ColorPicker");
			xamlMember = new XamlMember(this, "MinHue", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_121_ColorPicker_MinHue;
			xamlMember.Setter = set_121_ColorPicker_MinHue;
			break;
		case "Microsoft.UI.Xaml.Controls.ColorPicker.MinSaturation":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ColorPicker");
			xamlMember = new XamlMember(this, "MinSaturation", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_122_ColorPicker_MinSaturation;
			xamlMember.Setter = set_122_ColorPicker_MinSaturation;
			break;
		case "Microsoft.UI.Xaml.Controls.ColorPicker.MinValue":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ColorPicker");
			xamlMember = new XamlMember(this, "MinValue", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_123_ColorPicker_MinValue;
			xamlMember.Setter = set_123_ColorPicker_MinValue;
			break;
		case "Microsoft.UI.Xaml.Controls.ColorPicker.Orientation":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ColorPicker");
			xamlMember = new XamlMember(this, "Orientation", "Microsoft.UI.Xaml.Controls.Orientation");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_124_ColorPicker_Orientation;
			xamlMember.Setter = set_124_ColorPicker_Orientation;
			break;
		case "Microsoft.UI.Xaml.Controls.ColorPicker.PreviousColor":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ColorPicker");
			xamlMember = new XamlMember(this, "PreviousColor", "System.Nullable`1<Windows.UI.Color>");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_125_ColorPicker_PreviousColor;
			xamlMember.Setter = set_125_ColorPicker_PreviousColor;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSpectrum.AlphaSliderValue":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSpectrum");
			xamlMember = new XamlMember(this, "AlphaSliderValue", "System.Nullable`1<Double>");
			xamlMember.Getter = get_126_ColorPickerSpectrum_AlphaSliderValue;
			xamlMember.Setter = set_126_ColorPickerSpectrum_AlphaSliderValue;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSpectrum.IsColorPickerAlphaSliderEditable":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSpectrum");
			xamlMember = new XamlMember(this, "IsColorPickerAlphaSliderEditable", "Boolean");
			xamlMember.Getter = get_127_ColorPickerSpectrum_IsColorPickerAlphaSliderEditable;
			xamlMember.Setter = set_127_ColorPickerSpectrum_IsColorPickerAlphaSliderEditable;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSpectrum.IsAlphaSliderVisible":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSpectrum");
			xamlMember = new XamlMember(this, "IsAlphaSliderVisible", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_128_ColorPickerSpectrum_IsAlphaSliderVisible;
			xamlMember.Setter = set_128_ColorPickerSpectrum_IsAlphaSliderVisible;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSpectrum.IsSaturationSliderVisible":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSpectrum");
			xamlMember = new XamlMember(this, "IsSaturationSliderVisible", "Boolean");
			xamlMember.Getter = get_129_ColorPickerSpectrum_IsSaturationSliderVisible;
			xamlMember.Setter = set_129_ColorPickerSpectrum_IsSaturationSliderVisible;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSpectrum.SelectedColorDescription":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerSpectrum");
			xamlMember = new XamlMember(this, "SelectedColorDescription", "String");
			xamlMember.Getter = get_130_ColorPickerSpectrum_SelectedColorDescription;
			xamlMember.Setter = set_130_ColorPickerSpectrum_SelectedColorDescription;
			break;
		case "Microsoft.UI.Xaml.CornerRadius.TopLeft":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.CornerRadius");
			xamlMember = new XamlMember(this, "TopLeft", "Double");
			xamlMember.Getter = get_131_CornerRadius_TopLeft;
			xamlMember.Setter = set_131_CornerRadius_TopLeft;
			break;
		case "Microsoft.UI.Xaml.CornerRadius.TopRight":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.CornerRadius");
			xamlMember = new XamlMember(this, "TopRight", "Double");
			xamlMember.Getter = get_132_CornerRadius_TopRight;
			xamlMember.Setter = set_132_CornerRadius_TopRight;
			break;
		case "Microsoft.UI.Xaml.CornerRadius.BottomRight":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.CornerRadius");
			xamlMember = new XamlMember(this, "BottomRight", "Double");
			xamlMember.Getter = get_133_CornerRadius_BottomRight;
			xamlMember.Setter = set_133_CornerRadius_BottomRight;
			break;
		case "Microsoft.UI.Xaml.CornerRadius.BottomLeft":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.CornerRadius");
			xamlMember = new XamlMember(this, "BottomLeft", "Double");
			xamlMember.Getter = get_134_CornerRadius_BottomLeft;
			xamlMember.Setter = set_134_CornerRadius_BottomLeft;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerTextBox.StringResourceKey":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerTextBox");
			xamlMember = new XamlMember(this, "StringResourceKey", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_135_ColorPickerTextBox_StringResourceKey;
			xamlMember.Setter = set_135_ColorPickerTextBox_StringResourceKey;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerTextBox.TextBoxStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerTextBox");
			xamlMember = new XamlMember(this, "TextBoxStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_136_ColorPickerTextBox_TextBoxStyle;
			xamlMember.Setter = set_136_ColorPickerTextBox_TextBoxStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerTextBox.IsTextBoxLoaded":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerTextBox");
			xamlMember = new XamlMember(this, "IsTextBoxLoaded", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_137_ColorPickerTextBox_IsTextBoxLoaded;
			xamlMember.Setter = set_137_ColorPickerTextBox_IsTextBoxLoaded;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerTextBox.Text":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker.ColorPickerTextBox");
			xamlMember = new XamlMember(this, "Text", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_138_ColorPickerTextBox_Text;
			xamlMember.Setter = set_138_ColorPickerTextBox_Text;
			break;
		case "Samsung.OneUI.WinUI.Controls.Brushes.CheckeredBrush.BackgroundBrush":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Brushes.CheckeredBrush");
			xamlMember = new XamlMember(this, "BackgroundBrush", "Microsoft.UI.Xaml.Media.SolidColorBrush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_139_CheckeredBrush_BackgroundBrush;
			xamlMember.Setter = set_139_CheckeredBrush_BackgroundBrush;
			break;
		case "Samsung.OneUI.WinUI.Controls.Brushes.CheckeredBrush.RectBrush":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Brushes.CheckeredBrush");
			xamlMember = new XamlMember(this, "RectBrush", "Microsoft.UI.Xaml.Media.SolidColorBrush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_140_CheckeredBrush_RectBrush;
			xamlMember.Setter = set_140_CheckeredBrush_RectBrush;
			break;
		case "Samsung.OneUI.WinUI.Controls.Selectors.ColorListItemSelector.EmptyStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Selectors.ColorListItemSelector");
			xamlMember = new XamlMember(this, "EmptyStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.Getter = get_141_ColorListItemSelector_EmptyStyle;
			xamlMember.Setter = set_141_ColorListItemSelector_EmptyStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.Selectors.ColorListItemSelector.NormalStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Selectors.ColorListItemSelector");
			xamlMember = new XamlMember(this, "NormalStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.Getter = get_142_ColorListItemSelector_NormalStyle;
			xamlMember.Setter = set_142_ColorListItemSelector_NormalStyle;
			break;
		case "Samsung.OneUI.WinUI.AttachedProperties.CornerRadiusAutoHalfCorner.CornerPoint":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.AttachedProperties.CornerRadiusAutoHalfCorner");
			xamlMember = new XamlMember(this, "CornerPoint", "String");
			xamlMember.SetTargetTypeName("Microsoft.UI.Xaml.DependencyObject");
			xamlMember.SetIsAttachable();
			xamlMember.Getter = get_143_CornerRadiusAutoHalfCorner_CornerPoint;
			xamlMember.Setter = set_143_CornerRadiusAutoHalfCorner_CornerPoint;
			break;
		case "Samsung.OneUI.WinUI.AttachedProperties.CornerRadiusAutoHalfCorner.CanOverride":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.AttachedProperties.CornerRadiusAutoHalfCorner");
			xamlMember = new XamlMember(this, "CanOverride", "Boolean");
			xamlMember.SetTargetTypeName("Microsoft.UI.Xaml.DependencyObject");
			xamlMember.SetIsAttachable();
			xamlMember.Getter = get_144_CornerRadiusAutoHalfCorner_CanOverride;
			xamlMember.Setter = set_144_CornerRadiusAutoHalfCorner_CanOverride;
			break;
		case "Microsoft.UI.Xaml.Thickness.Left":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Thickness");
			xamlMember = new XamlMember(this, "Left", "Double");
			xamlMember.Getter = get_145_Thickness_Left;
			xamlMember.Setter = set_145_Thickness_Left;
			break;
		case "Microsoft.UI.Xaml.Thickness.Top":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Thickness");
			xamlMember = new XamlMember(this, "Top", "Double");
			xamlMember.Getter = get_146_Thickness_Top;
			xamlMember.Setter = set_146_Thickness_Top;
			break;
		case "Microsoft.UI.Xaml.Thickness.Right":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Thickness");
			xamlMember = new XamlMember(this, "Right", "Double");
			xamlMember.Getter = get_147_Thickness_Right;
			xamlMember.Setter = set_147_Thickness_Right;
			break;
		case "Microsoft.UI.Xaml.Thickness.Bottom":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Thickness");
			xamlMember = new XamlMember(this, "Bottom", "Double");
			xamlMember.Getter = get_148_Thickness_Bottom;
			xamlMember.Setter = set_148_Thickness_Bottom;
			break;
		case "Samsung.OneUI.WinUI.Utils.Extensions.OverlayColorsToSolidColorBrushExtension.ColorList":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Utils.Extensions.OverlayColorsToSolidColorBrushExtension");
			xamlMember = new XamlMember(this, "ColorList", "System.Collections.Generic.IList`1<Microsoft.UI.Xaml.Media.SolidColorBrush>");
			xamlMember.Getter = get_149_OverlayColorsToSolidColorBrushExtension_ColorList;
			xamlMember.Setter = set_149_OverlayColorsToSolidColorBrushExtension_ColorList;
			break;
		case "Samsung.OneUI.WinUI.Converters.CornerRadiusToDoubleConverter.ConvertionRoundingStrategy":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Converters.CornerRadiusToDoubleConverter");
			xamlMember = new XamlMember(this, "ConvertionRoundingStrategy", "Samsung.OneUI.WinUI.Converters.ICornerRadiusRoundingStrategyConvertion");
			xamlMember.Getter = get_150_CornerRadiusToDoubleConverter_ConvertionRoundingStrategy;
			xamlMember.Setter = set_150_CornerRadiusToDoubleConverter_ConvertionRoundingStrategy;
			break;
		case "Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum.Components":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum");
			xamlMember = new XamlMember(this, "Components", "Microsoft.UI.Xaml.Controls.ColorSpectrumComponents");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_151_ColorSpectrum_Components;
			xamlMember.Setter = set_151_ColorSpectrum_Components;
			break;
		case "Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum.MaxHue":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum");
			xamlMember = new XamlMember(this, "MaxHue", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_152_ColorSpectrum_MaxHue;
			xamlMember.Setter = set_152_ColorSpectrum_MaxHue;
			break;
		case "Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum.MaxSaturation":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum");
			xamlMember = new XamlMember(this, "MaxSaturation", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_153_ColorSpectrum_MaxSaturation;
			xamlMember.Setter = set_153_ColorSpectrum_MaxSaturation;
			break;
		case "Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum.MaxValue":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum");
			xamlMember = new XamlMember(this, "MaxValue", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_154_ColorSpectrum_MaxValue;
			xamlMember.Setter = set_154_ColorSpectrum_MaxValue;
			break;
		case "Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum.MinHue":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum");
			xamlMember = new XamlMember(this, "MinHue", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_155_ColorSpectrum_MinHue;
			xamlMember.Setter = set_155_ColorSpectrum_MinHue;
			break;
		case "Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum.MinSaturation":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum");
			xamlMember = new XamlMember(this, "MinSaturation", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_156_ColorSpectrum_MinSaturation;
			xamlMember.Setter = set_156_ColorSpectrum_MinSaturation;
			break;
		case "Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum.MinValue":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum");
			xamlMember = new XamlMember(this, "MinValue", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_157_ColorSpectrum_MinValue;
			xamlMember.Setter = set_157_ColorSpectrum_MinValue;
			break;
		case "Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum.Shape":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum");
			xamlMember = new XamlMember(this, "Shape", "Microsoft.UI.Xaml.Controls.ColorSpectrumShape");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_158_ColorSpectrum_Shape;
			xamlMember.Setter = set_158_ColorSpectrum_Shape;
			break;
		case "Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum.Color":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum");
			xamlMember = new XamlMember(this, "Color", "Windows.UI.Color");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_159_ColorSpectrum_Color;
			xamlMember.Setter = set_159_ColorSpectrum_Color;
			break;
		case "Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum.HsvColor":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.ColorSpectrum");
			xamlMember = new XamlMember(this, "HsvColor", "System.Numerics.Vector4");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_160_ColorSpectrum_HsvColor;
			xamlMember.Setter = set_160_ColorSpectrum_HsvColor;
			break;
		case "Microsoft.UI.Xaml.Controls.Primitives.ColorPickerSlider.ColorChannel":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.ColorPickerSlider");
			xamlMember = new XamlMember(this, "ColorChannel", "Microsoft.UI.Xaml.Controls.ColorPickerHsvChannel");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_161_ColorPickerSlider_ColorChannel;
			xamlMember.Setter = set_161_ColorPickerSlider_ColorChannel;
			break;
		case "CommunityToolkit.WinUI.Effects.Shadow":
			_ = (XamlUserType)GetXamlTypeByName("CommunityToolkit.WinUI.Effects");
			xamlMember = new XamlMember(this, "Shadow", "CommunityToolkit.WinUI.AttachedShadowBase");
			xamlMember.SetTargetTypeName("Microsoft.UI.Xaml.FrameworkElement");
			xamlMember.SetIsAttachable();
			xamlMember.Getter = get_162_Effects_Shadow;
			xamlMember.Setter = set_162_Effects_Shadow;
			break;
		case "CommunityToolkit.WinUI.AttachedShadowBase.BlurRadius":
			_ = (XamlUserType)GetXamlTypeByName("CommunityToolkit.WinUI.AttachedShadowBase");
			xamlMember = new XamlMember(this, "BlurRadius", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_163_AttachedShadowBase_BlurRadius;
			xamlMember.Setter = set_163_AttachedShadowBase_BlurRadius;
			break;
		case "CommunityToolkit.WinUI.Media.AttachedCardShadow.CornerRadius":
			_ = (XamlUserType)GetXamlTypeByName("CommunityToolkit.WinUI.Media.AttachedCardShadow");
			xamlMember = new XamlMember(this, "CornerRadius", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_164_AttachedCardShadow_CornerRadius;
			xamlMember.Setter = set_164_AttachedCardShadow_CornerRadius;
			break;
		case "CommunityToolkit.WinUI.AttachedShadowBase.Opacity":
			_ = (XamlUserType)GetXamlTypeByName("CommunityToolkit.WinUI.AttachedShadowBase");
			xamlMember = new XamlMember(this, "Opacity", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_165_AttachedShadowBase_Opacity;
			xamlMember.Setter = set_165_AttachedShadowBase_Opacity;
			break;
		case "CommunityToolkit.WinUI.AttachedShadowBase.Offset":
			_ = (XamlUserType)GetXamlTypeByName("CommunityToolkit.WinUI.AttachedShadowBase");
			xamlMember = new XamlMember(this, "Offset", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_166_AttachedShadowBase_Offset;
			xamlMember.Setter = set_166_AttachedShadowBase_Offset;
			break;
		case "CommunityToolkit.WinUI.AttachedShadowBase.Color":
			_ = (XamlUserType)GetXamlTypeByName("CommunityToolkit.WinUI.AttachedShadowBase");
			xamlMember = new XamlMember(this, "Color", "Windows.UI.Color");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_167_AttachedShadowBase_Color;
			xamlMember.Setter = set_167_AttachedShadowBase_Color;
			break;
		case "CommunityToolkit.WinUI.Media.AttachedCardShadow.InnerContentClipMode":
			_ = (XamlUserType)GetXamlTypeByName("CommunityToolkit.WinUI.Media.AttachedCardShadow");
			xamlMember = new XamlMember(this, "InnerContentClipMode", "CommunityToolkit.WinUI.Media.InnerContentClipMode");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_168_AttachedCardShadow_InnerContentClipMode;
			xamlMember.Setter = set_168_AttachedCardShadow_InnerContentClipMode;
			break;
		case "Samsung.OneUI.WinUI.Controls.Selectors.ColorPickerGridViewItemRadiusSelector.BottomLeftItem":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Selectors.ColorPickerGridViewItemRadiusSelector");
			xamlMember = new XamlMember(this, "BottomLeftItem", "Microsoft.UI.Xaml.Style");
			xamlMember.Getter = get_169_ColorPickerGridViewItemRadiusSelector_BottomLeftItem;
			xamlMember.Setter = set_169_ColorPickerGridViewItemRadiusSelector_BottomLeftItem;
			break;
		case "Samsung.OneUI.WinUI.Controls.Selectors.ColorPickerGridViewItemRadiusSelector.BottomRightItem":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Selectors.ColorPickerGridViewItemRadiusSelector");
			xamlMember = new XamlMember(this, "BottomRightItem", "Microsoft.UI.Xaml.Style");
			xamlMember.Getter = get_170_ColorPickerGridViewItemRadiusSelector_BottomRightItem;
			xamlMember.Setter = set_170_ColorPickerGridViewItemRadiusSelector_BottomRightItem;
			break;
		case "Samsung.OneUI.WinUI.Controls.Selectors.ColorPickerGridViewItemRadiusSelector.MiddleItem":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Selectors.ColorPickerGridViewItemRadiusSelector");
			xamlMember = new XamlMember(this, "MiddleItem", "Microsoft.UI.Xaml.Style");
			xamlMember.Getter = get_171_ColorPickerGridViewItemRadiusSelector_MiddleItem;
			xamlMember.Setter = set_171_ColorPickerGridViewItemRadiusSelector_MiddleItem;
			break;
		case "Samsung.OneUI.WinUI.Controls.Selectors.ColorPickerGridViewItemRadiusSelector.TopLeftItem":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Selectors.ColorPickerGridViewItemRadiusSelector");
			xamlMember = new XamlMember(this, "TopLeftItem", "Microsoft.UI.Xaml.Style");
			xamlMember.Getter = get_172_ColorPickerGridViewItemRadiusSelector_TopLeftItem;
			xamlMember.Setter = set_172_ColorPickerGridViewItemRadiusSelector_TopLeftItem;
			break;
		case "Samsung.OneUI.WinUI.Controls.Selectors.ColorPickerGridViewItemRadiusSelector.TopRightItem":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Selectors.ColorPickerGridViewItemRadiusSelector");
			xamlMember = new XamlMember(this, "TopRightItem", "Microsoft.UI.Xaml.Style");
			xamlMember.Getter = get_173_ColorPickerGridViewItemRadiusSelector_TopRightItem;
			xamlMember.Setter = set_173_ColorPickerGridViewItemRadiusSelector_TopRightItem;
			break;
		case "Samsung.OneUI.WinUI.Controls.NumberBadge.Value":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NumberBadge");
			xamlMember = new XamlMember(this, "Value", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_174_NumberBadge_Value;
			xamlMember.Setter = set_174_NumberBadge_Value;
			break;
		case "Samsung.OneUI.WinUI.Controls.BadgeBase.IsSelected":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.BadgeBase");
			xamlMember = new XamlMember(this, "IsSelected", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_175_BadgeBase_IsSelected;
			xamlMember.Setter = set_175_BadgeBase_IsSelected;
			break;
		case "Samsung.OneUI.WinUI.Controls.ContainedButtonBase.IsProgressEnabled":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ContainedButtonBase");
			xamlMember = new XamlMember(this, "IsProgressEnabled", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_176_ContainedButtonBase_IsProgressEnabled;
			xamlMember.Setter = set_176_ContainedButtonBase_IsProgressEnabled;
			break;
		case "Microsoft.Xaml.Interactivity.Interaction.Behaviors":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.Xaml.Interactivity.Interaction");
			xamlMember = new XamlMember(this, "Behaviors", "Microsoft.Xaml.Interactivity.BehaviorCollection");
			xamlMember.SetTargetTypeName("Microsoft.UI.Xaml.DependencyObject");
			xamlMember.SetIsAttachable();
			xamlMember.Getter = get_177_Interaction_Behaviors;
			xamlMember.Setter = set_177_Interaction_Behaviors;
			break;
		case "Samsung.OneUI.WinUI.Controls.ProgressCircleIndeterminate.Foreground":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ProgressCircleIndeterminate");
			xamlMember = new XamlMember(this, "Foreground", "Microsoft.UI.Xaml.Media.Brush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_178_ProgressCircleIndeterminate_Foreground;
			xamlMember.Setter = set_178_ProgressCircleIndeterminate_Foreground;
			break;
		case "Samsung.OneUI.WinUI.Controls.ProgressCircleIndeterminate.PointForeground":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ProgressCircleIndeterminate");
			xamlMember = new XamlMember(this, "PointForeground", "Microsoft.UI.Xaml.Media.Brush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_179_ProgressCircleIndeterminate_PointForeground;
			xamlMember.Setter = set_179_ProgressCircleIndeterminate_PointForeground;
			break;
		case "Samsung.OneUI.WinUI.Controls.ProgressCircle.Size":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ProgressCircle");
			xamlMember = new XamlMember(this, "Size", "Samsung.OneUI.WinUI.Controls.ProgressCircleSize");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_180_ProgressCircle_Size;
			xamlMember.Setter = set_180_ProgressCircle_Size;
			break;
		case "Samsung.OneUI.WinUI.Controls.ProgressCircle.Text":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ProgressCircle");
			xamlMember = new XamlMember(this, "Text", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_181_ProgressCircle_Text;
			xamlMember.Setter = set_181_ProgressCircle_Text;
			break;
		case "Microsoft.Xaml.Interactivity.Behavior`1<Microsoft.UI.Xaml.FrameworkElement>.AssociatedObject":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.Xaml.Interactivity.Behavior`1<Microsoft.UI.Xaml.FrameworkElement>");
			xamlMember = new XamlMember(this, "AssociatedObject", "Microsoft.UI.Xaml.FrameworkElement");
			xamlMember.Getter = get_182_Behavior_AssociatedObject;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.ContainedButton.Type":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ContainedButton");
			xamlMember = new XamlMember(this, "Type", "Samsung.OneUI.WinUI.Controls.ContainedButtonType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_183_ContainedButton_Type;
			xamlMember.Setter = set_183_ContainedButton_Type;
			break;
		case "Samsung.OneUI.WinUI.Controls.ContainedButton.Size":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ContainedButton");
			xamlMember = new XamlMember(this, "Size", "Samsung.OneUI.WinUI.Controls.ContainedButtonSize");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_184_ContainedButton_Size;
			xamlMember.Setter = set_184_ContainedButton_Size;
			break;
		case "Samsung.OneUI.WinUI.Controls.ContentButton.Shape":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ContentButton");
			xamlMember = new XamlMember(this, "Shape", "Samsung.OneUI.WinUI.Controls.ButtonShapeEnum");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_185_ContentButton_Shape;
			xamlMember.Setter = set_185_ContentButton_Shape;
			break;
		case "Samsung.OneUI.WinUI.Controls.ContentButton.IsPressAndHoldEnabled":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ContentButton");
			xamlMember = new XamlMember(this, "IsPressAndHoldEnabled", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_186_ContentButton_IsPressAndHoldEnabled;
			xamlMember.Setter = set_186_ContentButton_IsPressAndHoldEnabled;
			break;
		case "Samsung.OneUI.WinUI.Controls.ContentButton.PressAndHoldInterval":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ContentButton");
			xamlMember = new XamlMember(this, "PressAndHoldInterval", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_187_ContentButton_PressAndHoldInterval;
			xamlMember.Setter = set_187_ContentButton_PressAndHoldInterval;
			break;
		case "Samsung.OneUI.WinUI.Controls.ContentToggleButton.Shape":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ContentToggleButton");
			xamlMember = new XamlMember(this, "Shape", "Samsung.OneUI.WinUI.Controls.ButtonShapeEnum");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_188_ContentToggleButton_Shape;
			xamlMember.Setter = set_188_ContentToggleButton_Shape;
			break;
		case "Samsung.OneUI.WinUI.Controls.EditButton.Type":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.EditButton");
			xamlMember = new XamlMember(this, "Type", "Samsung.OneUI.WinUI.Controls.EditButtonType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_189_EditButton_Type;
			xamlMember.Setter = set_189_EditButton_Type;
			break;
		case "Samsung.OneUI.WinUI.Controls.FloatingActionButton.Visibility":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FloatingActionButton");
			xamlMember = new XamlMember(this, "Visibility", "Microsoft.UI.Xaml.Visibility");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_190_FloatingActionButton_Visibility;
			xamlMember.Setter = set_190_FloatingActionButton_Visibility;
			break;
		case "Samsung.OneUI.WinUI.Controls.FloatingActionButton.IsBlur":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FloatingActionButton");
			xamlMember = new XamlMember(this, "IsBlur", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_191_FloatingActionButton_IsBlur;
			xamlMember.Setter = set_191_FloatingActionButton_IsBlur;
			break;
		case "Samsung.OneUI.WinUI.AttachedProperties.ElevationCorner.CornerRadius":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.AttachedProperties.ElevationCorner");
			xamlMember = new XamlMember(this, "CornerRadius", "Double");
			xamlMember.SetTargetTypeName("Microsoft.UI.Xaml.DependencyObject");
			xamlMember.SetIsAttachable();
			xamlMember.Getter = get_192_ElevationCorner_CornerRadius;
			xamlMember.Setter = set_192_ElevationCorner_CornerRadius;
			break;
		case "Samsung.OneUI.WinUI.Tokens.BlurLayer.LayerContent":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Tokens.BlurLayer");
			xamlMember = new XamlMember(this, "LayerContent", "Microsoft.UI.Xaml.UIElement");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_193_BlurLayer_LayerContent;
			xamlMember.Setter = set_193_BlurLayer_LayerContent;
			break;
		case "Samsung.OneUI.WinUI.Tokens.BlurLayer.BlurLevel":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Tokens.BlurLayer");
			xamlMember = new XamlMember(this, "BlurLevel", "Samsung.OneUI.WinUI.Tokens.BlurLevel");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_194_BlurLayer_BlurLevel;
			xamlMember.Setter = set_194_BlurLayer_BlurLevel;
			break;
		case "Samsung.OneUI.WinUI.Tokens.BlurLayer.FallbackBackground":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Tokens.BlurLayer");
			xamlMember = new XamlMember(this, "FallbackBackground", "Microsoft.UI.Xaml.Media.Brush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_195_BlurLayer_FallbackBackground;
			xamlMember.Setter = set_195_BlurLayer_FallbackBackground;
			break;
		case "Samsung.OneUI.WinUI.Tokens.BlurLayer.IsBlur":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Tokens.BlurLayer");
			xamlMember = new XamlMember(this, "IsBlur", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_196_BlurLayer_IsBlur;
			xamlMember.Setter = set_196_BlurLayer_IsBlur;
			break;
		case "Samsung.OneUI.WinUI.Tokens.BlurLayer.Vibrancy":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Tokens.BlurLayer");
			xamlMember = new XamlMember(this, "Vibrancy", "Samsung.OneUI.WinUI.Tokens.VibrancyLevel");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_197_BlurLayer_Vibrancy;
			xamlMember.Setter = set_197_BlurLayer_Vibrancy;
			break;
		case "Samsung.OneUI.WinUI.Controls.GoToTopButton.IsBlur":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.GoToTopButton");
			xamlMember = new XamlMember(this, "IsBlur", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_198_GoToTopButton_IsBlur;
			xamlMember.Setter = set_198_GoToTopButton_IsBlur;
			break;
		case "Samsung.OneUI.WinUI.Controls.HyperlinkButton.TextTrimming":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.HyperlinkButton");
			xamlMember = new XamlMember(this, "TextTrimming", "Microsoft.UI.Xaml.TextTrimming");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_199_HyperlinkButton_TextTrimming;
			xamlMember.Setter = set_199_HyperlinkButton_TextTrimming;
			break;
		case "Samsung.OneUI.WinUI.Controls.HyperlinkButton.IsTextTrimmed":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.HyperlinkButton");
			xamlMember = new XamlMember(this, "IsTextTrimmed", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_200_HyperlinkButton_IsTextTrimmed;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.ProgressButton.IsProgressEnabled":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ProgressButton");
			xamlMember = new XamlMember(this, "IsProgressEnabled", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_201_ProgressButton_IsProgressEnabled;
			xamlMember.Setter = set_201_ProgressButton_IsProgressEnabled;
			break;
		case "Samsung.OneUI.WinUI.Controls.ProgressButton.Type":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ProgressButton");
			xamlMember = new XamlMember(this, "Type", "Samsung.OneUI.WinUI.Controls.ProgressButtonType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_202_ProgressButton_Type;
			xamlMember.Setter = set_202_ProgressButton_Type;
			break;
		case "Samsung.OneUI.WinUI.Controls.Behaviors.TooltipForTrimmedTextBlockBehavior.TextBlockName":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Behaviors.TooltipForTrimmedTextBlockBehavior");
			xamlMember = new XamlMember(this, "TextBlockName", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_203_TooltipForTrimmedTextBlockBehavior_TextBlockName;
			xamlMember.Setter = set_203_TooltipForTrimmedTextBlockBehavior_TextBlockName;
			break;
		case "Microsoft.UI.Xaml.Media.Animation.KeyTime.TimeSpan":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Media.Animation.KeyTime");
			xamlMember = new XamlMember(this, "TimeSpan", "TimeSpan");
			xamlMember.Getter = get_204_KeyTime_TimeSpan;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.CheckBox.Icon":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CheckBox");
			xamlMember = new XamlMember(this, "Icon", "Microsoft.UI.Xaml.Controls.IconElement");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_205_CheckBox_Icon;
			xamlMember.Setter = set_205_CheckBox_Icon;
			break;
		case "Samsung.OneUI.WinUI.Controls.CheckBox.IconSvgStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CheckBox");
			xamlMember = new XamlMember(this, "IconSvgStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_206_CheckBox_IconSvgStyle;
			xamlMember.Setter = set_206_CheckBox_IconSvgStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.CheckBox.Uri":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CheckBox");
			xamlMember = new XamlMember(this, "Uri", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_207_CheckBox_Uri;
			xamlMember.Setter = set_207_CheckBox_Uri;
			break;
		case "Samsung.OneUI.WinUI.Controls.CheckBox.Type":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CheckBox");
			xamlMember = new XamlMember(this, "Type", "Samsung.OneUI.WinUI.Controls.Inputs.CheckBox.CheckBoxType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_208_CheckBox_Type;
			xamlMember.Setter = set_208_CheckBox_Type;
			break;
		case "Samsung.OneUI.WinUI.Controls.Selectors.ChipsItemStyleSelector.CancelBorderStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Selectors.ChipsItemStyleSelector");
			xamlMember = new XamlMember(this, "CancelBorderStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.Getter = get_209_ChipsItemStyleSelector_CancelBorderStyle;
			xamlMember.Setter = set_209_ChipsItemStyleSelector_CancelBorderStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.Selectors.ChipsItemStyleSelector.CancelStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Selectors.ChipsItemStyleSelector");
			xamlMember = new XamlMember(this, "CancelStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.Getter = get_210_ChipsItemStyleSelector_CancelStyle;
			xamlMember.Setter = set_210_ChipsItemStyleSelector_CancelStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.Selectors.ChipsItemStyleSelector.MinusBorderStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Selectors.ChipsItemStyleSelector");
			xamlMember = new XamlMember(this, "MinusBorderStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.Getter = get_211_ChipsItemStyleSelector_MinusBorderStyle;
			xamlMember.Setter = set_211_ChipsItemStyleSelector_MinusBorderStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.Selectors.ChipsItemStyleSelector.MinusStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Selectors.ChipsItemStyleSelector");
			xamlMember = new XamlMember(this, "MinusStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.Getter = get_212_ChipsItemStyleSelector_MinusStyle;
			xamlMember.Setter = set_212_ChipsItemStyleSelector_MinusStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.Selectors.ChipsItemStyleSelector.NoneBorderStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Selectors.ChipsItemStyleSelector");
			xamlMember = new XamlMember(this, "NoneBorderStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.Getter = get_213_ChipsItemStyleSelector_NoneBorderStyle;
			xamlMember.Setter = set_213_ChipsItemStyleSelector_NoneBorderStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.Selectors.ChipsItemStyleSelector.NoneStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Selectors.ChipsItemStyleSelector");
			xamlMember = new XamlMember(this, "NoneStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.Getter = get_214_ChipsItemStyleSelector_NoneStyle;
			xamlMember.Setter = set_214_ChipsItemStyleSelector_NoneStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.Selectors.ChipsItemStyleSelector.TagBorderStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Selectors.ChipsItemStyleSelector");
			xamlMember = new XamlMember(this, "TagBorderStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.Getter = get_215_ChipsItemStyleSelector_TagBorderStyle;
			xamlMember.Setter = set_215_ChipsItemStyleSelector_TagBorderStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.Selectors.ChipsItemStyleSelector.TagStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Selectors.ChipsItemStyleSelector");
			xamlMember = new XamlMember(this, "TagStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.Getter = get_216_ChipsItemStyleSelector_TagStyle;
			xamlMember.Setter = set_216_ChipsItemStyleSelector_TagStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.Behaviors.CornerRadiusBorderCompensationBehavior.Compensation":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Behaviors.CornerRadiusBorderCompensationBehavior");
			xamlMember = new XamlMember(this, "Compensation", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_217_CornerRadiusBorderCompensationBehavior_Compensation;
			xamlMember.Setter = set_217_CornerRadiusBorderCompensationBehavior_Compensation;
			break;
		case "Microsoft.UI.Xaml.Controls.ImageIcon.Source":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ImageIcon");
			xamlMember = new XamlMember(this, "Source", "Microsoft.UI.Xaml.Media.ImageSource");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_218_ImageIcon_Source;
			xamlMember.Setter = set_218_ImageIcon_Source;
			break;
		case "Windows.UI.Color.A":
			_ = (XamlUserType)GetXamlTypeByName("Windows.UI.Color");
			xamlMember = new XamlMember(this, "A", "Byte");
			xamlMember.Getter = get_219_Color_A;
			xamlMember.Setter = set_219_Color_A;
			break;
		case "Windows.UI.Color.R":
			_ = (XamlUserType)GetXamlTypeByName("Windows.UI.Color");
			xamlMember = new XamlMember(this, "R", "Byte");
			xamlMember.Getter = get_220_Color_R;
			xamlMember.Setter = set_220_Color_R;
			break;
		case "Windows.UI.Color.G":
			_ = (XamlUserType)GetXamlTypeByName("Windows.UI.Color");
			xamlMember = new XamlMember(this, "G", "Byte");
			xamlMember.Getter = get_221_Color_G;
			xamlMember.Setter = set_221_Color_G;
			break;
		case "Windows.UI.Color.B":
			_ = (XamlUserType)GetXamlTypeByName("Windows.UI.Color");
			xamlMember = new XamlMember(this, "B", "Byte");
			xamlMember.Getter = get_222_Color_B;
			xamlMember.Setter = set_222_Color_B;
			break;
		case "Samsung.OneUI.WinUI.Controls.CommandBarButton.LabelVisibility":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CommandBarButton");
			xamlMember = new XamlMember(this, "LabelVisibility", "Microsoft.UI.Xaml.Visibility");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_223_CommandBarButton_LabelVisibility;
			xamlMember.Setter = set_223_CommandBarButton_LabelVisibility;
			break;
		case "Samsung.OneUI.WinUI.Controls.CommandBarButton.IconSvgStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CommandBarButton");
			xamlMember = new XamlMember(this, "IconSvgStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_224_CommandBarButton_IconSvgStyle;
			xamlMember.Setter = set_224_CommandBarButton_IconSvgStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.CommandBar.CurrentItemsMaxWidth":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CommandBar");
			xamlMember = new XamlMember(this, "CurrentItemsMaxWidth", "Double");
			xamlMember.Getter = get_225_CommandBar_CurrentItemsMaxWidth;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.CommandBar.MoreOptionsOverflowItems":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CommandBar");
			xamlMember = new XamlMember(this, "MoreOptionsOverflowItems", "System.Collections.ObjectModel.ObservableCollection`1<Microsoft.UI.Xaml.Controls.MenuFlyoutItemBase>");
			xamlMember.Getter = get_226_CommandBar_MoreOptionsOverflowItems;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.CommandBar.BackButtonCommand":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CommandBar");
			xamlMember = new XamlMember(this, "BackButtonCommand", "System.Windows.Input.ICommand");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_227_CommandBar_BackButtonCommand;
			xamlMember.Setter = set_227_CommandBar_BackButtonCommand;
			break;
		case "Samsung.OneUI.WinUI.Controls.CommandBar.BackButtonCommandParameter":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CommandBar");
			xamlMember = new XamlMember(this, "BackButtonCommandParameter", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_228_CommandBar_BackButtonCommandParameter;
			xamlMember.Setter = set_228_CommandBar_BackButtonCommandParameter;
			break;
		case "Samsung.OneUI.WinUI.Controls.CommandBar.MoreOptionsItems":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CommandBar");
			xamlMember = new XamlMember(this, "MoreOptionsItems", "System.Collections.ObjectModel.ObservableCollection`1<Microsoft.UI.Xaml.Controls.MenuFlyoutItemBase>");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_229_CommandBar_MoreOptionsItems;
			xamlMember.Setter = set_229_CommandBar_MoreOptionsItems;
			break;
		case "Samsung.OneUI.WinUI.Controls.CommandBar.IsBackButtonVisible":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CommandBar");
			xamlMember = new XamlMember(this, "IsBackButtonVisible", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_230_CommandBar_IsBackButtonVisible;
			xamlMember.Setter = set_230_CommandBar_IsBackButtonVisible;
			break;
		case "Samsung.OneUI.WinUI.Controls.CommandBar.IsOptionsButtonVisible":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CommandBar");
			xamlMember = new XamlMember(this, "IsOptionsButtonVisible", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_231_CommandBar_IsOptionsButtonVisible;
			xamlMember.Setter = set_231_CommandBar_IsOptionsButtonVisible;
			break;
		case "Samsung.OneUI.WinUI.Controls.CommandBar.BackButtonType":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CommandBar");
			xamlMember = new XamlMember(this, "BackButtonType", "Samsung.OneUI.WinUI.Controls.CommandBarBackButtonType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_232_CommandBar_BackButtonType;
			xamlMember.Setter = set_232_CommandBar_BackButtonType;
			break;
		case "Samsung.OneUI.WinUI.Controls.CommandBar.MoreOptionsBadge":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CommandBar");
			xamlMember = new XamlMember(this, "MoreOptionsBadge", "Samsung.OneUI.WinUI.Controls.BadgeBase");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_233_CommandBar_MoreOptionsBadge;
			xamlMember.Setter = set_233_CommandBar_MoreOptionsBadge;
			break;
		case "Samsung.OneUI.WinUI.Controls.CommandBar.MoreOptionsHorizontalOffset":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CommandBar");
			xamlMember = new XamlMember(this, "MoreOptionsHorizontalOffset", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_234_CommandBar_MoreOptionsHorizontalOffset;
			xamlMember.Setter = set_234_CommandBar_MoreOptionsHorizontalOffset;
			break;
		case "Samsung.OneUI.WinUI.Controls.CommandBar.MoreOptionsVerticalOffset":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CommandBar");
			xamlMember = new XamlMember(this, "MoreOptionsVerticalOffset", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_235_CommandBar_MoreOptionsVerticalOffset;
			xamlMember.Setter = set_235_CommandBar_MoreOptionsVerticalOffset;
			break;
		case "Samsung.OneUI.WinUI.Controls.CommandBar.MoreOptionsPlacement":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CommandBar");
			xamlMember = new XamlMember(this, "MoreOptionsPlacement", "System.Nullable`1<Microsoft.UI.Xaml.Controls.Primitives.FlyoutPlacementMode>");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_236_CommandBar_MoreOptionsPlacement;
			xamlMember.Setter = set_236_CommandBar_MoreOptionsPlacement;
			break;
		case "Samsung.OneUI.WinUI.Controls.CommandBar.MoreOptionsToolTipContent":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CommandBar");
			xamlMember = new XamlMember(this, "MoreOptionsToolTipContent", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_237_CommandBar_MoreOptionsToolTipContent;
			xamlMember.Setter = set_237_CommandBar_MoreOptionsToolTipContent;
			break;
		case "Samsung.OneUI.WinUI.Controls.CommandBar.SubtitleText":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CommandBar");
			xamlMember = new XamlMember(this, "SubtitleText", "String");
			xamlMember.Getter = get_238_CommandBar_SubtitleText;
			xamlMember.Setter = set_238_CommandBar_SubtitleText;
			break;
		case "Samsung.OneUI.WinUI.Controls.CommandBar.IsSubtitleVisible":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CommandBar");
			xamlMember = new XamlMember(this, "IsSubtitleVisible", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_239_CommandBar_IsSubtitleVisible;
			xamlMember.Setter = set_239_CommandBar_IsSubtitleVisible;
			break;
		case "Samsung.OneUI.WinUI.Controls.Behaviors.FlexibleSpacingBehavior.FlexibleSpacingTargetContent":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Behaviors.FlexibleSpacingBehavior");
			xamlMember = new XamlMember(this, "FlexibleSpacingTargetContent", "Microsoft.UI.Xaml.FrameworkElement");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_240_FlexibleSpacingBehavior_FlexibleSpacingTargetContent;
			xamlMember.Setter = set_240_FlexibleSpacingBehavior_FlexibleSpacingTargetContent;
			break;
		case "Samsung.OneUI.WinUI.Controls.Behaviors.FlexibleSpacingBehavior.IsFlexibleSpacing":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Behaviors.FlexibleSpacingBehavior");
			xamlMember = new XamlMember(this, "IsFlexibleSpacing", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_241_FlexibleSpacingBehavior_IsFlexibleSpacing;
			xamlMember.Setter = set_241_FlexibleSpacingBehavior_IsFlexibleSpacing;
			break;
		case "Samsung.OneUI.WinUI.Controls.Behaviors.FlexibleSpacingBehavior.Type":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Behaviors.FlexibleSpacingBehavior");
			xamlMember = new XamlMember(this, "Type", "Samsung.OneUI.WinUI.AttachedProperties.Enums.FlexibleSpacingType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_242_FlexibleSpacingBehavior_Type;
			xamlMember.Setter = set_242_FlexibleSpacingBehavior_Type;
			break;
		case "Samsung.OneUI.WinUI.Controls.Behaviors.FlexibleSpacingBehavior.MarginTiny":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Behaviors.FlexibleSpacingBehavior");
			xamlMember = new XamlMember(this, "MarginTiny", "Microsoft.UI.Xaml.Thickness");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_243_FlexibleSpacingBehavior_MarginTiny;
			xamlMember.Setter = set_243_FlexibleSpacingBehavior_MarginTiny;
			break;
		case "Samsung.OneUI.WinUI.Controls.Behaviors.FlexibleSpacingBehavior.MarginSmall":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Behaviors.FlexibleSpacingBehavior");
			xamlMember = new XamlMember(this, "MarginSmall", "Microsoft.UI.Xaml.Thickness");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_244_FlexibleSpacingBehavior_MarginSmall;
			xamlMember.Setter = set_244_FlexibleSpacingBehavior_MarginSmall;
			break;
		case "Samsung.OneUI.WinUI.Controls.Behaviors.FlexibleSpacingBehavior.MarginMedium":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Behaviors.FlexibleSpacingBehavior");
			xamlMember = new XamlMember(this, "MarginMedium", "Microsoft.UI.Xaml.Thickness");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_245_FlexibleSpacingBehavior_MarginMedium;
			xamlMember.Setter = set_245_FlexibleSpacingBehavior_MarginMedium;
			break;
		case "Samsung.OneUI.WinUI.Controls.Behaviors.FlexibleSpacingBehavior.MarginLarge":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Behaviors.FlexibleSpacingBehavior");
			xamlMember = new XamlMember(this, "MarginLarge", "Microsoft.UI.Xaml.Thickness");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_246_FlexibleSpacingBehavior_MarginLarge;
			xamlMember.Setter = set_246_FlexibleSpacingBehavior_MarginLarge;
			break;
		case "Samsung.OneUI.WinUI.Controls.Behaviors.FlexibleSpacingBehavior.MarginHuge":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Behaviors.FlexibleSpacingBehavior");
			xamlMember = new XamlMember(this, "MarginHuge", "Microsoft.UI.Xaml.Thickness");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_247_FlexibleSpacingBehavior_MarginHuge;
			xamlMember.Setter = set_247_FlexibleSpacingBehavior_MarginHuge;
			break;
		case "Samsung.OneUI.WinUI.Controls.Behaviors.FlexibleSpacingBehavior.MarginOff":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Behaviors.FlexibleSpacingBehavior");
			xamlMember = new XamlMember(this, "MarginOff", "Microsoft.UI.Xaml.Thickness");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_248_FlexibleSpacingBehavior_MarginOff;
			xamlMember.Setter = set_248_FlexibleSpacingBehavior_MarginOff;
			break;
		case "Samsung.OneUI.WinUI.Controls.IconButton.IconSvgStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.IconButton");
			xamlMember = new XamlMember(this, "IconSvgStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_249_IconButton_IconSvgStyle;
			xamlMember.Setter = set_249_IconButton_IconSvgStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.IconButton.LabelVisibility":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.IconButton");
			xamlMember = new XamlMember(this, "LabelVisibility", "Microsoft.UI.Xaml.Visibility");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_250_IconButton_LabelVisibility;
			xamlMember.Setter = set_250_IconButton_LabelVisibility;
			break;
		case "Samsung.OneUI.WinUI.Controls.ListFlyout.IsCommandBarChild":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ListFlyout");
			xamlMember = new XamlMember(this, "IsCommandBarChild", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_251_ListFlyout_IsCommandBarChild;
			xamlMember.Setter = set_251_ListFlyout_IsCommandBarChild;
			break;
		case "Samsung.OneUI.WinUI.Controls.ListFlyout.HorizontalOffset":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ListFlyout");
			xamlMember = new XamlMember(this, "HorizontalOffset", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_252_ListFlyout_HorizontalOffset;
			xamlMember.Setter = set_252_ListFlyout_HorizontalOffset;
			break;
		case "Samsung.OneUI.WinUI.Controls.ListFlyout.VerticalOffset":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ListFlyout");
			xamlMember = new XamlMember(this, "VerticalOffset", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_253_ListFlyout_VerticalOffset;
			xamlMember.Setter = set_253_ListFlyout_VerticalOffset;
			break;
		case "Samsung.OneUI.WinUI.Controls.ListFlyout.Placement":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ListFlyout");
			xamlMember = new XamlMember(this, "Placement", "Microsoft.UI.Xaml.Controls.Primitives.FlyoutPlacementMode");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_254_ListFlyout_Placement;
			xamlMember.Setter = set_254_ListFlyout_Placement;
			break;
		case "Samsung.OneUI.WinUI.Controls.ListFlyout.IsBlur":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ListFlyout");
			xamlMember = new XamlMember(this, "IsBlur", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_255_ListFlyout_IsBlur;
			xamlMember.Setter = set_255_ListFlyout_IsBlur;
			break;
		case "Samsung.OneUI.WinUI.AttachedProperties.Tooltip.TextTrimmedEnabled":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.AttachedProperties.Tooltip");
			xamlMember = new XamlMember(this, "TextTrimmedEnabled", "Boolean");
			xamlMember.SetTargetTypeName("Microsoft.UI.Xaml.DependencyObject");
			xamlMember.SetIsAttachable();
			xamlMember.Getter = get_256_Tooltip_TextTrimmedEnabled;
			xamlMember.Setter = set_256_Tooltip_TextTrimmedEnabled;
			break;
		case "Samsung.OneUI.WinUI.Controls.CommandBarToggleButton.LabelVisibility":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CommandBarToggleButton");
			xamlMember = new XamlMember(this, "LabelVisibility", "Microsoft.UI.Xaml.Visibility");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_257_CommandBarToggleButton_LabelVisibility;
			xamlMember.Setter = set_257_CommandBarToggleButton_LabelVisibility;
			break;
		case "Samsung.OneUI.WinUI.Controls.CommandBarToggleButton.IconSvgStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CommandBarToggleButton");
			xamlMember = new XamlMember(this, "IconSvgStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_258_CommandBarToggleButton_IconSvgStyle;
			xamlMember.Setter = set_258_CommandBarToggleButton_IconSvgStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.ListFlyoutItem.CommandBarItemOverflowable":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ListFlyoutItem");
			xamlMember = new XamlMember(this, "CommandBarItemOverflowable", "Samsung.OneUI.WinUI.Controls.ICommandBarItemOverflowable");
			xamlMember.Getter = get_259_ListFlyoutItem_CommandBarItemOverflowable;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.ListFlyoutItem.NotificationBadge":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ListFlyoutItem");
			xamlMember = new XamlMember(this, "NotificationBadge", "Samsung.OneUI.WinUI.Controls.BadgeBase");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_260_ListFlyoutItem_NotificationBadge;
			xamlMember.Setter = set_260_ListFlyoutItem_NotificationBadge;
			break;
		case "Samsung.OneUI.WinUI.Controls.DatePickerSpinnerList.Day":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DatePickerSpinnerList");
			xamlMember = new XamlMember(this, "Day", "Samsung.OneUI.WinUI.Controls.DatePickerSpinnerListItem");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_261_DatePickerSpinnerList_Day;
			xamlMember.Setter = set_261_DatePickerSpinnerList_Day;
			break;
		case "Samsung.OneUI.WinUI.Controls.DatePickerSpinnerList.Month":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DatePickerSpinnerList");
			xamlMember = new XamlMember(this, "Month", "Samsung.OneUI.WinUI.Controls.DatePickerSpinnerListItem");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_262_DatePickerSpinnerList_Month;
			xamlMember.Setter = set_262_DatePickerSpinnerList_Month;
			break;
		case "Samsung.OneUI.WinUI.Controls.DatePickerSpinnerList.Year":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DatePickerSpinnerList");
			xamlMember = new XamlMember(this, "Year", "Samsung.OneUI.WinUI.Controls.DatePickerSpinnerListItem");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_263_DatePickerSpinnerList_Year;
			xamlMember.Setter = set_263_DatePickerSpinnerList_Year;
			break;
		case "Samsung.OneUI.WinUI.Controls.DatePickerSpinnerList.EnabledEntranceAnimation":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DatePickerSpinnerList");
			xamlMember = new XamlMember(this, "EnabledEntranceAnimation", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_264_DatePickerSpinnerList_EnabledEntranceAnimation;
			xamlMember.Setter = set_264_DatePickerSpinnerList_EnabledEntranceAnimation;
			break;
		case "Samsung.OneUI.WinUI.Controls.ScrollList.SelectedTime":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ScrollList");
			xamlMember = new XamlMember(this, "SelectedTime", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_265_ScrollList_SelectedTime;
			xamlMember.Setter = set_265_ScrollList_SelectedTime;
			break;
		case "Samsung.OneUI.WinUI.Controls.ScrollList.TimeItemsSource":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ScrollList");
			xamlMember = new XamlMember(this, "TimeItemsSource", "System.Collections.ObjectModel.ObservableCollection`1<Object>");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_266_ScrollList_TimeItemsSource;
			xamlMember.Setter = set_266_ScrollList_TimeItemsSource;
			break;
		case "Samsung.OneUI.WinUI.Controls.ScrollList.InfiniteScroll":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ScrollList");
			xamlMember = new XamlMember(this, "InfiniteScroll", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_267_ScrollList_InfiniteScroll;
			xamlMember.Setter = set_267_ScrollList_InfiniteScroll;
			break;
		case "Samsung.OneUI.WinUI.Controls.DatePickerSpinnerListItem.TypeDate":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DatePickerSpinnerListItem");
			xamlMember = new XamlMember(this, "TypeDate", "Samsung.OneUI.WinUI.Utils.Enums.TypeDate");
			xamlMember.Getter = get_268_DatePickerSpinnerListItem_TypeDate;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.DatePickerSpinnerListItem.Value":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DatePickerSpinnerListItem");
			xamlMember = new XamlMember(this, "Value", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_269_DatePickerSpinnerListItem_Value;
			xamlMember.Setter = set_269_DatePickerSpinnerListItem_Value;
			break;
		case "Samsung.OneUI.WinUI.Controls.DatePickerSpinnerListItem.FormattedValue":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DatePickerSpinnerListItem");
			xamlMember = new XamlMember(this, "FormattedValue", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_270_DatePickerSpinnerListItem_FormattedValue;
			xamlMember.Setter = set_270_DatePickerSpinnerListItem_FormattedValue;
			break;
		case "Samsung.OneUI.WinUI.Controls.Selectors.PeriodStyleSelector.HiddenStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Selectors.PeriodStyleSelector");
			xamlMember = new XamlMember(this, "HiddenStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.Getter = get_271_PeriodStyleSelector_HiddenStyle;
			xamlMember.Setter = set_271_PeriodStyleSelector_HiddenStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.Selectors.PeriodStyleSelector.NormalStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Selectors.PeriodStyleSelector");
			xamlMember = new XamlMember(this, "NormalStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.Getter = get_272_PeriodStyleSelector_NormalStyle;
			xamlMember.Setter = set_272_PeriodStyleSelector_NormalStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.PeriodScrollList.VerticalOffSetAnimation":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.PeriodScrollList");
			xamlMember = new XamlMember(this, "VerticalOffSetAnimation", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_273_PeriodScrollList_VerticalOffSetAnimation;
			xamlMember.Setter = set_273_PeriodScrollList_VerticalOffSetAnimation;
			break;
		case "Samsung.OneUI.WinUI.Common.DpiChangedStateTriggerBase.OsVersionExpected":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Common.DpiChangedStateTriggerBase");
			xamlMember = new XamlMember(this, "OsVersionExpected", "Samsung.OneUI.WinUI.Common.OSVersionType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_274_DpiChangedStateTriggerBase_OsVersionExpected;
			xamlMember.Setter = set_274_DpiChangedStateTriggerBase_OsVersionExpected;
			break;
		case "Samsung.OneUI.WinUI.Controls.ContentDialog.BackgroundDialog":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ContentDialog");
			xamlMember = new XamlMember(this, "BackgroundDialog", "Microsoft.UI.Xaml.Media.SolidColorBrush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_275_ContentDialog_BackgroundDialog;
			xamlMember.Setter = set_275_ContentDialog_BackgroundDialog;
			break;
		case "Samsung.OneUI.WinUI.Controls.ContentDialog.ContentMargin":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ContentDialog");
			xamlMember = new XamlMember(this, "ContentMargin", "Microsoft.UI.Xaml.Thickness");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_276_ContentDialog_ContentMargin;
			xamlMember.Setter = set_276_ContentDialog_ContentMargin;
			break;
		case "Samsung.OneUI.WinUI.Controls.ContentDialog.DialogMaxHeight":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ContentDialog");
			xamlMember = new XamlMember(this, "DialogMaxHeight", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_277_ContentDialog_DialogMaxHeight;
			xamlMember.Setter = set_277_ContentDialog_DialogMaxHeight;
			break;
		case "Samsung.OneUI.WinUI.Controls.ContentDialog.DialogWidth":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ContentDialog");
			xamlMember = new XamlMember(this, "DialogWidth", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_278_ContentDialog_DialogWidth;
			xamlMember.Setter = set_278_ContentDialog_DialogWidth;
			break;
		case "Samsung.OneUI.WinUI.Controls.ContentDialog.DialogTitleAlignment":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ContentDialog");
			xamlMember = new XamlMember(this, "DialogTitleAlignment", "Microsoft.UI.Xaml.HorizontalAlignment");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_279_ContentDialog_DialogTitleAlignment;
			xamlMember.Setter = set_279_ContentDialog_DialogTitleAlignment;
			break;
		case "Samsung.OneUI.WinUI.Controls.ContentDialog.IsCloseButtonEnabled":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ContentDialog");
			xamlMember = new XamlMember(this, "IsCloseButtonEnabled", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_280_ContentDialog_IsCloseButtonEnabled;
			xamlMember.Setter = set_280_ContentDialog_IsCloseButtonEnabled;
			break;
		case "Samsung.OneUI.WinUI.Controls.ContentDialog.CustomSmokedBackgroundResourceKey":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ContentDialog");
			xamlMember = new XamlMember(this, "CustomSmokedBackgroundResourceKey", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_281_ContentDialog_CustomSmokedBackgroundResourceKey;
			xamlMember.Setter = set_281_ContentDialog_CustomSmokedBackgroundResourceKey;
			break;
		case "Samsung.OneUI.WinUI.Controls.ContentDialog.CustomAppBarMargin":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ContentDialog");
			xamlMember = new XamlMember(this, "CustomAppBarMargin", "Microsoft.UI.Xaml.Thickness");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_282_ContentDialog_CustomAppBarMargin;
			xamlMember.Setter = set_282_ContentDialog_CustomAppBarMargin;
			break;
		case "Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.VerticalScrollBarSpacingFromContent":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer");
			xamlMember = new XamlMember(this, "VerticalScrollBarSpacingFromContent", "Microsoft.UI.Xaml.GridLength");
			xamlMember.SetTargetTypeName("Microsoft.UI.Xaml.DependencyObject");
			xamlMember.SetIsAttachable();
			xamlMember.Getter = get_283_ScrollViewer_VerticalScrollBarSpacingFromContent;
			xamlMember.Setter = set_283_ScrollViewer_VerticalScrollBarSpacingFromContent;
			break;
		case "Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.IsMaskingRoundCorner":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer");
			xamlMember = new XamlMember(this, "IsMaskingRoundCorner", "System.Nullable`1<Boolean>");
			xamlMember.SetTargetTypeName("Microsoft.UI.Xaml.DependencyObject");
			xamlMember.SetIsAttachable();
			xamlMember.Getter = get_284_ScrollViewer_IsMaskingRoundCorner;
			xamlMember.Setter = set_284_ScrollViewer_IsMaskingRoundCorner;
			break;
		case "Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.IsFirstScrollAnimation":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer");
			xamlMember = new XamlMember(this, "IsFirstScrollAnimation", "System.Nullable`1<Boolean>");
			xamlMember.SetTargetTypeName("Microsoft.UI.Xaml.DependencyObject");
			xamlMember.SetIsAttachable();
			xamlMember.Getter = get_285_ScrollViewer_IsFirstScrollAnimation;
			xamlMember.Setter = set_285_ScrollViewer_IsFirstScrollAnimation;
			break;
		case "Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.VerticalScrollBarMargin":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer");
			xamlMember = new XamlMember(this, "VerticalScrollBarMargin", "Double");
			xamlMember.SetTargetTypeName("Microsoft.UI.Xaml.UIElement");
			xamlMember.SetIsAttachable();
			xamlMember.Getter = get_286_ScrollViewer_VerticalScrollBarMargin;
			xamlMember.Setter = set_286_ScrollViewer_VerticalScrollBarMargin;
			break;
		case "Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.MaskingRoundElementReference":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer");
			xamlMember = new XamlMember(this, "MaskingRoundElementReference", "Microsoft.UI.Xaml.FrameworkElement");
			xamlMember.SetTargetTypeName("Microsoft.UI.Xaml.DependencyObject");
			xamlMember.SetIsAttachable();
			xamlMember.Getter = get_287_ScrollViewer_MaskingRoundElementReference;
			xamlMember.Setter = set_287_ScrollViewer_MaskingRoundElementReference;
			break;
		case "Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.AutoHideVerticalScrollBar":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer");
			xamlMember = new XamlMember(this, "AutoHideVerticalScrollBar", "System.Nullable`1<Boolean>");
			xamlMember.SetTargetTypeName("Microsoft.UI.Xaml.DependencyObject");
			xamlMember.SetIsAttachable();
			xamlMember.Getter = get_288_ScrollViewer_AutoHideVerticalScrollBar;
			xamlMember.Setter = set_288_ScrollViewer_AutoHideVerticalScrollBar;
			break;
		case "Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer.AutoHideHorizontalScrollBar":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.AttachedProperties.ScrollViewer");
			xamlMember = new XamlMember(this, "AutoHideHorizontalScrollBar", "System.Nullable`1<Boolean>");
			xamlMember.SetTargetTypeName("Microsoft.UI.Xaml.DependencyObject");
			xamlMember.SetIsAttachable();
			xamlMember.Getter = get_289_ScrollViewer_AutoHideHorizontalScrollBar;
			xamlMember.Setter = set_289_ScrollViewer_AutoHideHorizontalScrollBar;
			break;
		case "Samsung.OneUI.WinUI.Controls.ShowVerticalScrollableIndicatorBehavior.BottomIndicator":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ShowVerticalScrollableIndicatorBehavior");
			xamlMember = new XamlMember(this, "BottomIndicator", "Microsoft.UI.Xaml.UIElement");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_290_ShowVerticalScrollableIndicatorBehavior_BottomIndicator;
			xamlMember.Setter = set_290_ShowVerticalScrollableIndicatorBehavior_BottomIndicator;
			break;
		case "Samsung.OneUI.WinUI.Controls.ShowVerticalScrollableIndicatorBehavior.TargetScrollViewer":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ShowVerticalScrollableIndicatorBehavior");
			xamlMember = new XamlMember(this, "TargetScrollViewer", "Microsoft.UI.Xaml.Controls.ScrollViewer");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_291_ShowVerticalScrollableIndicatorBehavior_TargetScrollViewer;
			xamlMember.Setter = set_291_ShowVerticalScrollableIndicatorBehavior_TargetScrollViewer;
			break;
		case "Samsung.OneUI.WinUI.Controls.ShowVerticalScrollableIndicatorBehavior.TopIndicator":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ShowVerticalScrollableIndicatorBehavior");
			xamlMember = new XamlMember(this, "TopIndicator", "Microsoft.UI.Xaml.UIElement");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_292_ShowVerticalScrollableIndicatorBehavior_TopIndicator;
			xamlMember.Setter = set_292_ShowVerticalScrollableIndicatorBehavior_TopIndicator;
			break;
		case "Microsoft.Xaml.Interactivity.Behavior`1<Microsoft.UI.Xaml.DependencyObject>.AssociatedObject":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.Xaml.Interactivity.Behavior`1<Microsoft.UI.Xaml.DependencyObject>");
			xamlMember = new XamlMember(this, "AssociatedObject", "Microsoft.UI.Xaml.DependencyObject");
			xamlMember.Getter = get_293_Behavior_AssociatedObject;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.Divider.Type":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Divider");
			xamlMember = new XamlMember(this, "Type", "Samsung.OneUI.WinUI.Controls.DividerType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_294_Divider_Type;
			xamlMember.Setter = set_294_Divider_Type;
			break;
		case "Samsung.OneUI.WinUI.Controls.Divider.Orientation":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Divider");
			xamlMember = new XamlMember(this, "Orientation", "Microsoft.UI.Xaml.Controls.Orientation");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_295_Divider_Orientation;
			xamlMember.Setter = set_295_Divider_Orientation;
			break;
		case "Samsung.OneUI.WinUI.Controls.Divider.HeaderText":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Divider");
			xamlMember = new XamlMember(this, "HeaderText", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_296_Divider_HeaderText;
			xamlMember.Setter = set_296_Divider_HeaderText;
			break;
		case "Samsung.OneUI.WinUI.Controls.DropdownList.ArrowColor":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DropdownList");
			xamlMember = new XamlMember(this, "ArrowColor", "Microsoft.UI.Xaml.Media.SolidColorBrush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_297_DropdownList_ArrowColor;
			xamlMember.Setter = set_297_DropdownList_ArrowColor;
			break;
		case "Samsung.OneUI.WinUI.Controls.DropdownList.IsListEnabled":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DropdownList");
			xamlMember = new XamlMember(this, "IsListEnabled", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_298_DropdownList_IsListEnabled;
			xamlMember.Setter = set_298_DropdownList_IsListEnabled;
			break;
		case "Samsung.OneUI.WinUI.Controls.DropdownList.SelectedIndex":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DropdownList");
			xamlMember = new XamlMember(this, "SelectedIndex", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_299_DropdownList_SelectedIndex;
			xamlMember.Setter = set_299_DropdownList_SelectedIndex;
			break;
		case "Samsung.OneUI.WinUI.Controls.DropdownList.SelectedItem":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DropdownList");
			xamlMember = new XamlMember(this, "SelectedItem", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_300_DropdownList_SelectedItem;
			xamlMember.Setter = set_300_DropdownList_SelectedItem;
			break;
		case "Samsung.OneUI.WinUI.Controls.DropdownList.ItemsSource":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DropdownList");
			xamlMember = new XamlMember(this, "ItemsSource", "System.Collections.IList");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_301_DropdownList_ItemsSource;
			xamlMember.Setter = set_301_DropdownList_ItemsSource;
			break;
		case "Samsung.OneUI.WinUI.Controls.DropdownList.Header":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DropdownList");
			xamlMember = new XamlMember(this, "Header", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_302_DropdownList_Header;
			xamlMember.Setter = set_302_DropdownList_Header;
			break;
		case "Samsung.OneUI.WinUI.Controls.DropdownList.Placeholder":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DropdownList");
			xamlMember = new XamlMember(this, "Placeholder", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_303_DropdownList_Placeholder;
			xamlMember.Setter = set_303_DropdownList_Placeholder;
			break;
		case "Samsung.OneUI.WinUI.Controls.DropdownList.ListTitle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DropdownList");
			xamlMember = new XamlMember(this, "ListTitle", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_304_DropdownList_ListTitle;
			xamlMember.Setter = set_304_DropdownList_ListTitle;
			break;
		case "Samsung.OneUI.WinUI.Controls.DropdownList.ListTitleVisibility":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DropdownList");
			xamlMember = new XamlMember(this, "ListTitleVisibility", "Microsoft.UI.Xaml.Visibility");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_305_DropdownList_ListTitleVisibility;
			xamlMember.Setter = set_305_DropdownList_ListTitleVisibility;
			break;
		case "Samsung.OneUI.WinUI.Controls.DropdownList.AppTitleBarHeightOffset":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DropdownList");
			xamlMember = new XamlMember(this, "AppTitleBarHeightOffset", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_306_DropdownList_AppTitleBarHeightOffset;
			xamlMember.Setter = set_306_DropdownList_AppTitleBarHeightOffset;
			break;
		case "Samsung.OneUI.WinUI.Controls.DropdownList.VerticalOffset":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DropdownList");
			xamlMember = new XamlMember(this, "VerticalOffset", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_307_DropdownList_VerticalOffset;
			xamlMember.Setter = set_307_DropdownList_VerticalOffset;
			break;
		case "Samsung.OneUI.WinUI.Controls.DropdownList.HorizontalOffset":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DropdownList");
			xamlMember = new XamlMember(this, "HorizontalOffset", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_308_DropdownList_HorizontalOffset;
			xamlMember.Setter = set_308_DropdownList_HorizontalOffset;
			break;
		case "Samsung.OneUI.WinUI.Controls.DropdownList.DropdownPopupAlignment":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DropdownList");
			xamlMember = new XamlMember(this, "DropdownPopupAlignment", "Microsoft.UI.Xaml.HorizontalAlignment");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_309_DropdownList_DropdownPopupAlignment;
			xamlMember.Setter = set_309_DropdownList_DropdownPopupAlignment;
			break;
		case "Samsung.OneUI.WinUI.Controls.DropdownList.IsMultilineItem":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DropdownList");
			xamlMember = new XamlMember(this, "IsMultilineItem", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_310_DropdownList_IsMultilineItem;
			xamlMember.Setter = set_310_DropdownList_IsMultilineItem;
			break;
		case "Samsung.OneUI.WinUI.Controls.DropdownList.Type":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DropdownList");
			xamlMember = new XamlMember(this, "Type", "Samsung.OneUI.WinUI.DropdownListType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_311_DropdownList_Type;
			xamlMember.Setter = set_311_DropdownList_Type;
			break;
		case "Samsung.OneUI.WinUI.Controls.DropdownList.IsBlur":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DropdownList");
			xamlMember = new XamlMember(this, "IsBlur", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_312_DropdownList_IsBlur;
			xamlMember.Setter = set_312_DropdownList_IsBlur;
			break;
		case "Samsung.OneUI.WinUI.Controls.DropdownCustomControl.Content":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DropdownCustomControl");
			xamlMember = new XamlMember(this, "Content", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_313_DropdownCustomControl_Content;
			xamlMember.Setter = set_313_DropdownCustomControl_Content;
			break;
		case "Samsung.OneUI.WinUI.Controls.DropdownCustomControl.ArrowColor":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DropdownCustomControl");
			xamlMember = new XamlMember(this, "ArrowColor", "Microsoft.UI.Xaml.Media.SolidColorBrush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_314_DropdownCustomControl_ArrowColor;
			xamlMember.Setter = set_314_DropdownCustomControl_ArrowColor;
			break;
		case "Samsung.OneUI.WinUI.Controls.DropdownCustomControl.IsEnabled":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.DropdownCustomControl");
			xamlMember = new XamlMember(this, "IsEnabled", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_315_DropdownCustomControl_IsEnabled;
			xamlMember.Setter = set_315_DropdownCustomControl_IsEnabled;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeView.ItemContainerStyle":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeView");
			xamlMember = new XamlMember(this, "ItemContainerStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_316_TreeView_ItemContainerStyle;
			xamlMember.Setter = set_316_TreeView_ItemContainerStyle;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeView.SelectionMode":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeView");
			xamlMember = new XamlMember(this, "SelectionMode", "Microsoft.UI.Xaml.Controls.TreeViewSelectionMode");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_317_TreeView_SelectionMode;
			xamlMember.Setter = set_317_TreeView_SelectionMode;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeView.CanDragItems":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeView");
			xamlMember = new XamlMember(this, "CanDragItems", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_318_TreeView_CanDragItems;
			xamlMember.Setter = set_318_TreeView_CanDragItems;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeView.CanReorderItems":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeView");
			xamlMember = new XamlMember(this, "CanReorderItems", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_319_TreeView_CanReorderItems;
			xamlMember.Setter = set_319_TreeView_CanReorderItems;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeView.ItemContainerTransitions":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeView");
			xamlMember = new XamlMember(this, "ItemContainerTransitions", "Microsoft.UI.Xaml.Media.Animation.TransitionCollection");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_320_TreeView_ItemContainerTransitions;
			xamlMember.Setter = set_320_TreeView_ItemContainerTransitions;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeView.ItemContainerStyleSelector":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeView");
			xamlMember = new XamlMember(this, "ItemContainerStyleSelector", "Microsoft.UI.Xaml.Controls.StyleSelector");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_321_TreeView_ItemContainerStyleSelector;
			xamlMember.Setter = set_321_TreeView_ItemContainerStyleSelector;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeView.ItemTemplate":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeView");
			xamlMember = new XamlMember(this, "ItemTemplate", "Microsoft.UI.Xaml.DataTemplate");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_322_TreeView_ItemTemplate;
			xamlMember.Setter = set_322_TreeView_ItemTemplate;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeView.ItemTemplateSelector":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeView");
			xamlMember = new XamlMember(this, "ItemTemplateSelector", "Microsoft.UI.Xaml.Controls.DataTemplateSelector");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_323_TreeView_ItemTemplateSelector;
			xamlMember.Setter = set_323_TreeView_ItemTemplateSelector;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeView.ItemsSource":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeView");
			xamlMember = new XamlMember(this, "ItemsSource", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_324_TreeView_ItemsSource;
			xamlMember.Setter = set_324_TreeView_ItemsSource;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeView.RootNodes":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeView");
			xamlMember = new XamlMember(this, "RootNodes", "System.Collections.Generic.IList`1<Microsoft.UI.Xaml.Controls.TreeViewNode>");
			xamlMember.Getter = get_325_TreeView_RootNodes;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.Controls.TreeViewNode.Children":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewNode");
			xamlMember = new XamlMember(this, "Children", "System.Collections.Generic.IList`1<Microsoft.UI.Xaml.Controls.TreeViewNode>");
			xamlMember.Getter = get_326_TreeViewNode_Children;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.Controls.TreeViewNode.Content":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewNode");
			xamlMember = new XamlMember(this, "Content", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_327_TreeViewNode_Content;
			xamlMember.Setter = set_327_TreeViewNode_Content;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeViewNode.Depth":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewNode");
			xamlMember = new XamlMember(this, "Depth", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_328_TreeViewNode_Depth;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.Controls.TreeViewNode.HasChildren":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewNode");
			xamlMember = new XamlMember(this, "HasChildren", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_329_TreeViewNode_HasChildren;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.Controls.TreeViewNode.HasUnrealizedChildren":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewNode");
			xamlMember = new XamlMember(this, "HasUnrealizedChildren", "Boolean");
			xamlMember.Getter = get_330_TreeViewNode_HasUnrealizedChildren;
			xamlMember.Setter = set_330_TreeViewNode_HasUnrealizedChildren;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeViewNode.IsExpanded":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewNode");
			xamlMember = new XamlMember(this, "IsExpanded", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_331_TreeViewNode_IsExpanded;
			xamlMember.Setter = set_331_TreeViewNode_IsExpanded;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeViewNode.Parent":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewNode");
			xamlMember = new XamlMember(this, "Parent", "Microsoft.UI.Xaml.Controls.TreeViewNode");
			xamlMember.Getter = get_332_TreeViewNode_Parent;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.Controls.TreeView.SelectedItem":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeView");
			xamlMember = new XamlMember(this, "SelectedItem", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_333_TreeView_SelectedItem;
			xamlMember.Setter = set_333_TreeView_SelectedItem;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeView.SelectedItems":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeView");
			xamlMember = new XamlMember(this, "SelectedItems", "System.Collections.Generic.IList`1<Object>");
			xamlMember.Getter = get_334_TreeView_SelectedItems;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.Controls.TreeView.SelectedNode":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeView");
			xamlMember = new XamlMember(this, "SelectedNode", "Microsoft.UI.Xaml.Controls.TreeViewNode");
			xamlMember.Getter = get_335_TreeView_SelectedNode;
			xamlMember.Setter = set_335_TreeView_SelectedNode;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeView.SelectedNodes":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeView");
			xamlMember = new XamlMember(this, "SelectedNodes", "System.Collections.Generic.IList`1<Microsoft.UI.Xaml.Controls.TreeViewNode>");
			xamlMember.Getter = get_336_TreeView_SelectedNodes;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.Controls.TreeViewItem.CollapsedGlyph":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewItem");
			xamlMember = new XamlMember(this, "CollapsedGlyph", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_337_TreeViewItem_CollapsedGlyph;
			xamlMember.Setter = set_337_TreeViewItem_CollapsedGlyph;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeViewItem.ExpandedGlyph":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewItem");
			xamlMember = new XamlMember(this, "ExpandedGlyph", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_338_TreeViewItem_ExpandedGlyph;
			xamlMember.Setter = set_338_TreeViewItem_ExpandedGlyph;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeViewItem.GlyphBrush":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewItem");
			xamlMember = new XamlMember(this, "GlyphBrush", "Microsoft.UI.Xaml.Media.Brush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_339_TreeViewItem_GlyphBrush;
			xamlMember.Setter = set_339_TreeViewItem_GlyphBrush;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeViewItem.GlyphOpacity":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewItem");
			xamlMember = new XamlMember(this, "GlyphOpacity", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_340_TreeViewItem_GlyphOpacity;
			xamlMember.Setter = set_340_TreeViewItem_GlyphOpacity;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeViewItem.GlyphSize":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewItem");
			xamlMember = new XamlMember(this, "GlyphSize", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_341_TreeViewItem_GlyphSize;
			xamlMember.Setter = set_341_TreeViewItem_GlyphSize;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeViewItem.HasUnrealizedChildren":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewItem");
			xamlMember = new XamlMember(this, "HasUnrealizedChildren", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_342_TreeViewItem_HasUnrealizedChildren;
			xamlMember.Setter = set_342_TreeViewItem_HasUnrealizedChildren;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeViewItem.IsExpanded":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewItem");
			xamlMember = new XamlMember(this, "IsExpanded", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_343_TreeViewItem_IsExpanded;
			xamlMember.Setter = set_343_TreeViewItem_IsExpanded;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeViewItem.ItemsSource":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewItem");
			xamlMember = new XamlMember(this, "ItemsSource", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_344_TreeViewItem_ItemsSource;
			xamlMember.Setter = set_344_TreeViewItem_ItemsSource;
			break;
		case "Microsoft.UI.Xaml.Controls.TreeViewItem.TreeViewItemTemplateSettings":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.TreeViewItem");
			xamlMember = new XamlMember(this, "TreeViewItemTemplateSettings", "Microsoft.UI.Xaml.Controls.TreeViewItemTemplateSettings");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_345_TreeViewItem_TreeViewItemTemplateSettings;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.ExpandButton.IsChecked":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ExpandButton");
			xamlMember = new XamlMember(this, "IsChecked", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_346_ExpandButton_IsChecked;
			xamlMember.Setter = set_346_ExpandButton_IsChecked;
			break;
		case "Samsung.OneUI.WinUI.Controls.FlipViewButton.IsBlur":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FlipViewButton");
			xamlMember = new XamlMember(this, "IsBlur", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_347_FlipViewButton_IsBlur;
			xamlMember.Setter = set_347_FlipViewButton_IsBlur;
			break;
		case "Samsung.OneUI.WinUI.Controls.FlipView.Orientation":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FlipView");
			xamlMember = new XamlMember(this, "Orientation", "Microsoft.UI.Xaml.Controls.Orientation");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_348_FlipView_Orientation;
			xamlMember.Setter = set_348_FlipView_Orientation;
			break;
		case "Samsung.OneUI.WinUI.Controls.FlipView.PreviousButtonHorizontalStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FlipView");
			xamlMember = new XamlMember(this, "PreviousButtonHorizontalStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_349_FlipView_PreviousButtonHorizontalStyle;
			xamlMember.Setter = set_349_FlipView_PreviousButtonHorizontalStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.FlipView.NextButtonHorizontalStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FlipView");
			xamlMember = new XamlMember(this, "NextButtonHorizontalStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_350_FlipView_NextButtonHorizontalStyle;
			xamlMember.Setter = set_350_FlipView_NextButtonHorizontalStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.FlipView.PreviousButtonVerticalStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FlipView");
			xamlMember = new XamlMember(this, "PreviousButtonVerticalStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_351_FlipView_PreviousButtonVerticalStyle;
			xamlMember.Setter = set_351_FlipView_PreviousButtonVerticalStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.FlipView.NextButtonVerticalStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FlipView");
			xamlMember = new XamlMember(this, "NextButtonVerticalStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_352_FlipView_NextButtonVerticalStyle;
			xamlMember.Setter = set_352_FlipView_NextButtonVerticalStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.FlipView.IsClickable":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FlipView");
			xamlMember = new XamlMember(this, "IsClickable", "Boolean");
			xamlMember.Getter = get_353_FlipView_IsClickable;
			xamlMember.Setter = set_353_FlipView_IsClickable;
			break;
		case "Samsung.OneUI.WinUI.Controls.FlipView.IsBlurButton":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FlipView");
			xamlMember = new XamlMember(this, "IsBlurButton", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_354_FlipView_IsBlurButton;
			xamlMember.Setter = set_354_FlipView_IsBlurButton;
			break;
		case "Samsung.OneUI.WinUI.Controls.IconToggleButton.LabelVisibility":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.IconToggleButton");
			xamlMember = new XamlMember(this, "LabelVisibility", "Microsoft.UI.Xaml.Visibility");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_355_IconToggleButton_LabelVisibility;
			xamlMember.Setter = set_355_IconToggleButton_LabelVisibility;
			break;
		case "Samsung.OneUI.WinUI.Controls.IconToggleButton.IconSvgStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.IconToggleButton");
			xamlMember = new XamlMember(this, "IconSvgStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_356_IconToggleButton_IconSvgStyle;
			xamlMember.Setter = set_356_IconToggleButton_IconSvgStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.Inputs.Slider.LevelBar.LevelSlider.Levels":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Inputs.Slider.LevelBar.LevelSlider");
			xamlMember = new XamlMember(this, "Levels", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_357_LevelSlider_Levels;
			xamlMember.Setter = set_357_LevelSlider_Levels;
			break;
		case "Samsung.OneUI.WinUI.Controls.LevelBar.Levels":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.LevelBar");
			xamlMember = new XamlMember(this, "Levels", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_358_LevelBar_Levels;
			xamlMember.Setter = set_358_LevelBar_Levels;
			break;
		case "Samsung.OneUI.WinUI.Controls.LevelBar.Maximum":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.LevelBar");
			xamlMember = new XamlMember(this, "Maximum", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_359_LevelBar_Maximum;
			xamlMember.Setter = set_359_LevelBar_Maximum;
			break;
		case "Samsung.OneUI.WinUI.Controls.LevelBar.Minimum":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.LevelBar");
			xamlMember = new XamlMember(this, "Minimum", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_360_LevelBar_Minimum;
			xamlMember.Setter = set_360_LevelBar_Minimum;
			break;
		case "Samsung.OneUI.WinUI.Controls.LevelBar.Value":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.LevelBar");
			xamlMember = new XamlMember(this, "Value", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_361_LevelBar_Value;
			xamlMember.Setter = set_361_LevelBar_Value;
			break;
		case "Samsung.OneUI.WinUI.Controls.LevelBar.IsThumbToolTipEnabled":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.LevelBar");
			xamlMember = new XamlMember(this, "IsThumbToolTipEnabled", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_362_LevelBar_IsThumbToolTipEnabled;
			xamlMember.Setter = set_362_LevelBar_IsThumbToolTipEnabled;
			break;
		case "Samsung.OneUI.WinUI.Controls.LevelBar.ThumbToolTipValueConverter":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.LevelBar");
			xamlMember = new XamlMember(this, "ThumbToolTipValueConverter", "Microsoft.UI.Xaml.Data.IValueConverter");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_363_LevelBar_ThumbToolTipValueConverter;
			xamlMember.Setter = set_363_LevelBar_ThumbToolTipValueConverter;
			break;
		case "Microsoft.UI.Xaml.Controls.ItemsRepeater.ItemTemplate":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ItemsRepeater");
			xamlMember = new XamlMember(this, "ItemTemplate", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_364_ItemsRepeater_ItemTemplate;
			xamlMember.Setter = set_364_ItemsRepeater_ItemTemplate;
			break;
		case "Microsoft.UI.Xaml.Controls.ItemsRepeater.Layout":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ItemsRepeater");
			xamlMember = new XamlMember(this, "Layout", "Microsoft.UI.Xaml.Controls.Layout");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_365_ItemsRepeater_Layout;
			xamlMember.Setter = set_365_ItemsRepeater_Layout;
			break;
		case "Microsoft.UI.Xaml.Controls.ItemsRepeater.Background":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ItemsRepeater");
			xamlMember = new XamlMember(this, "Background", "Microsoft.UI.Xaml.Media.Brush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_366_ItemsRepeater_Background;
			xamlMember.Setter = set_366_ItemsRepeater_Background;
			break;
		case "Microsoft.UI.Xaml.Controls.ItemsRepeater.HorizontalCacheLength":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ItemsRepeater");
			xamlMember = new XamlMember(this, "HorizontalCacheLength", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_367_ItemsRepeater_HorizontalCacheLength;
			xamlMember.Setter = set_367_ItemsRepeater_HorizontalCacheLength;
			break;
		case "Microsoft.UI.Xaml.Controls.ItemsRepeater.ItemTransitionProvider":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ItemsRepeater");
			xamlMember = new XamlMember(this, "ItemTransitionProvider", "Microsoft.UI.Xaml.Controls.ItemCollectionTransitionProvider");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_368_ItemsRepeater_ItemTransitionProvider;
			xamlMember.Setter = set_368_ItemsRepeater_ItemTransitionProvider;
			break;
		case "Microsoft.UI.Xaml.Controls.ItemsRepeater.ItemsSource":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ItemsRepeater");
			xamlMember = new XamlMember(this, "ItemsSource", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_369_ItemsRepeater_ItemsSource;
			xamlMember.Setter = set_369_ItemsRepeater_ItemsSource;
			break;
		case "Microsoft.UI.Xaml.Controls.ItemsRepeater.ItemsSourceView":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ItemsRepeater");
			xamlMember = new XamlMember(this, "ItemsSourceView", "Microsoft.UI.Xaml.Controls.ItemsSourceView");
			xamlMember.Getter = get_370_ItemsRepeater_ItemsSourceView;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.Controls.ItemsRepeater.VerticalCacheLength":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ItemsRepeater");
			xamlMember = new XamlMember(this, "VerticalCacheLength", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_371_ItemsRepeater_VerticalCacheLength;
			xamlMember.Setter = set_371_ItemsRepeater_VerticalCacheLength;
			break;
		case "Microsoft.UI.Xaml.Controls.StackLayout.Orientation":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.StackLayout");
			xamlMember = new XamlMember(this, "Orientation", "Microsoft.UI.Xaml.Controls.Orientation");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_372_StackLayout_Orientation;
			xamlMember.Setter = set_372_StackLayout_Orientation;
			break;
		case "Microsoft.UI.Xaml.Controls.StackLayout.Spacing":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.StackLayout");
			xamlMember = new XamlMember(this, "Spacing", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_373_StackLayout_Spacing;
			xamlMember.Setter = set_373_StackLayout_Spacing;
			break;
		case "Microsoft.UI.Xaml.Controls.Layout.IndexBasedLayoutOrientation":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Layout");
			xamlMember = new XamlMember(this, "IndexBasedLayoutOrientation", "Microsoft.UI.Xaml.Controls.IndexBasedLayoutOrientation");
			xamlMember.Getter = get_374_Layout_IndexBasedLayoutOrientation;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.Controls.UniformGridLayout.MinColumnSpacing":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.UniformGridLayout");
			xamlMember = new XamlMember(this, "MinColumnSpacing", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_375_UniformGridLayout_MinColumnSpacing;
			xamlMember.Setter = set_375_UniformGridLayout_MinColumnSpacing;
			break;
		case "Microsoft.UI.Xaml.Controls.UniformGridLayout.MinItemHeight":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.UniformGridLayout");
			xamlMember = new XamlMember(this, "MinItemHeight", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_376_UniformGridLayout_MinItemHeight;
			xamlMember.Setter = set_376_UniformGridLayout_MinItemHeight;
			break;
		case "Microsoft.UI.Xaml.Controls.UniformGridLayout.MinItemWidth":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.UniformGridLayout");
			xamlMember = new XamlMember(this, "MinItemWidth", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_377_UniformGridLayout_MinItemWidth;
			xamlMember.Setter = set_377_UniformGridLayout_MinItemWidth;
			break;
		case "Microsoft.UI.Xaml.Controls.UniformGridLayout.MinRowSpacing":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.UniformGridLayout");
			xamlMember = new XamlMember(this, "MinRowSpacing", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_378_UniformGridLayout_MinRowSpacing;
			xamlMember.Setter = set_378_UniformGridLayout_MinRowSpacing;
			break;
		case "Microsoft.UI.Xaml.Controls.UniformGridLayout.Orientation":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.UniformGridLayout");
			xamlMember = new XamlMember(this, "Orientation", "Microsoft.UI.Xaml.Controls.Orientation");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_379_UniformGridLayout_Orientation;
			xamlMember.Setter = set_379_UniformGridLayout_Orientation;
			break;
		case "Microsoft.UI.Xaml.Controls.UniformGridLayout.ItemsJustification":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.UniformGridLayout");
			xamlMember = new XamlMember(this, "ItemsJustification", "Microsoft.UI.Xaml.Controls.UniformGridLayoutItemsJustification");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_380_UniformGridLayout_ItemsJustification;
			xamlMember.Setter = set_380_UniformGridLayout_ItemsJustification;
			break;
		case "Microsoft.UI.Xaml.Controls.UniformGridLayout.ItemsStretch":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.UniformGridLayout");
			xamlMember = new XamlMember(this, "ItemsStretch", "Microsoft.UI.Xaml.Controls.UniformGridLayoutItemsStretch");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_381_UniformGridLayout_ItemsStretch;
			xamlMember.Setter = set_381_UniformGridLayout_ItemsStretch;
			break;
		case "Microsoft.UI.Xaml.Controls.UniformGridLayout.MaximumRowsOrColumns":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.UniformGridLayout");
			xamlMember = new XamlMember(this, "MaximumRowsOrColumns", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_382_UniformGridLayout_MaximumRowsOrColumns;
			xamlMember.Setter = set_382_UniformGridLayout_MaximumRowsOrColumns;
			break;
		case "Samsung.OneUI.WinUI.Controls.SplitBar.Element":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SplitBar");
			xamlMember = new XamlMember(this, "Element", "Microsoft.UI.Xaml.UIElement");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_383_SplitBar_Element;
			xamlMember.Setter = set_383_SplitBar_Element;
			break;
		case "Samsung.OneUI.WinUI.Controls.SplitBar.ResizeDirection":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SplitBar");
			xamlMember = new XamlMember(this, "ResizeDirection", "Samsung.OneUI.WinUI.Controls.SplitBar.GridResizeDirection");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_384_SplitBar_ResizeDirection;
			xamlMember.Setter = set_384_SplitBar_ResizeDirection;
			break;
		case "Samsung.OneUI.WinUI.Controls.SplitBar.ResizeBehavior":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SplitBar");
			xamlMember = new XamlMember(this, "ResizeBehavior", "Samsung.OneUI.WinUI.Controls.SplitBar.GridResizeBehavior");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_385_SplitBar_ResizeBehavior;
			xamlMember.Setter = set_385_SplitBar_ResizeBehavior;
			break;
		case "Samsung.OneUI.WinUI.Controls.SplitBar.GripperForeground":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SplitBar");
			xamlMember = new XamlMember(this, "GripperForeground", "Microsoft.UI.Xaml.Media.Brush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_386_SplitBar_GripperForeground;
			xamlMember.Setter = set_386_SplitBar_GripperForeground;
			break;
		case "Samsung.OneUI.WinUI.Controls.SplitBar.ParentLevel":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SplitBar");
			xamlMember = new XamlMember(this, "ParentLevel", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_387_SplitBar_ParentLevel;
			xamlMember.Setter = set_387_SplitBar_ParentLevel;
			break;
		case "Samsung.OneUI.WinUI.Controls.SplitBar.GripperCursor":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SplitBar");
			xamlMember = new XamlMember(this, "GripperCursor", "Samsung.OneUI.WinUI.Controls.SplitBar.GripperCursorType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_388_SplitBar_GripperCursor;
			xamlMember.Setter = set_388_SplitBar_GripperCursor;
			break;
		case "Samsung.OneUI.WinUI.Controls.SplitBar.GripperCustomCursorResource":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SplitBar");
			xamlMember = new XamlMember(this, "GripperCustomCursorResource", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_389_SplitBar_GripperCustomCursorResource;
			xamlMember.Setter = set_389_SplitBar_GripperCustomCursorResource;
			break;
		case "Samsung.OneUI.WinUI.Controls.SplitBar.CursorBehavior":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SplitBar");
			xamlMember = new XamlMember(this, "CursorBehavior", "Samsung.OneUI.WinUI.Controls.SplitBar.SplitterCursorBehavior");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_390_SplitBar_CursorBehavior;
			xamlMember.Setter = set_390_SplitBar_CursorBehavior;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.PaneToggleButtonStyle":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "PaneToggleButtonStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_391_NavigationView_PaneToggleButtonStyle;
			xamlMember.Setter = set_391_NavigationView_PaneToggleButtonStyle;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.OpenPaneLength":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "OpenPaneLength", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_392_NavigationView_OpenPaneLength;
			xamlMember.Setter = set_392_NavigationView_OpenPaneLength;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.CompactPaneLength":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "CompactPaneLength", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_393_NavigationView_CompactPaneLength;
			xamlMember.Setter = set_393_NavigationView_CompactPaneLength;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationRail.IsPaneAutoCompactEnabled":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationRail");
			xamlMember = new XamlMember(this, "IsPaneAutoCompactEnabled", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_394_NavigationRail_IsPaneAutoCompactEnabled;
			xamlMember.Setter = set_394_NavigationRail_IsPaneAutoCompactEnabled;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationRail.IsInitialPaneOpen":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationRail");
			xamlMember = new XamlMember(this, "IsInitialPaneOpen", "System.Nullable`1<Boolean>");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_395_NavigationRail_IsInitialPaneOpen;
			xamlMember.Setter = set_395_NavigationRail_IsInitialPaneOpen;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationRail.PaneToggleNotificationBadge":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationRail");
			xamlMember = new XamlMember(this, "PaneToggleNotificationBadge", "Samsung.OneUI.WinUI.Controls.BadgeBase");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_396_NavigationRail_PaneToggleNotificationBadge;
			xamlMember.Setter = set_396_NavigationRail_PaneToggleNotificationBadge;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationRail.SettingsNavigationItemNotificationBadge":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationRail");
			xamlMember = new XamlMember(this, "SettingsNavigationItemNotificationBadge", "Samsung.OneUI.WinUI.Controls.BadgeBase");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_397_NavigationRail_SettingsNavigationItemNotificationBadge;
			xamlMember.Setter = set_397_NavigationRail_SettingsNavigationItemNotificationBadge;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationRail.CollapseBreakPoint":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationRail");
			xamlMember = new XamlMember(this, "CollapseBreakPoint", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_398_NavigationRail_CollapseBreakPoint;
			xamlMember.Setter = set_398_NavigationRail_CollapseBreakPoint;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationRail.ExpandBreakPoint":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationRail");
			xamlMember = new XamlMember(this, "ExpandBreakPoint", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_399_NavigationRail_ExpandBreakPoint;
			xamlMember.Setter = set_399_NavigationRail_ExpandBreakPoint;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.AlwaysShowHeader":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "AlwaysShowHeader", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_400_NavigationView_AlwaysShowHeader;
			xamlMember.Setter = set_400_NavigationView_AlwaysShowHeader;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.AutoSuggestBox":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "AutoSuggestBox", "Microsoft.UI.Xaml.Controls.AutoSuggestBox");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_401_NavigationView_AutoSuggestBox;
			xamlMember.Setter = set_401_NavigationView_AutoSuggestBox;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.CompactModeThresholdWidth":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "CompactModeThresholdWidth", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_402_NavigationView_CompactModeThresholdWidth;
			xamlMember.Setter = set_402_NavigationView_CompactModeThresholdWidth;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.ContentOverlay":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "ContentOverlay", "Microsoft.UI.Xaml.UIElement");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_403_NavigationView_ContentOverlay;
			xamlMember.Setter = set_403_NavigationView_ContentOverlay;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.DisplayMode":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "DisplayMode", "Microsoft.UI.Xaml.Controls.NavigationViewDisplayMode");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_404_NavigationView_DisplayMode;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.ExpandedModeThresholdWidth":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "ExpandedModeThresholdWidth", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_405_NavigationView_ExpandedModeThresholdWidth;
			xamlMember.Setter = set_405_NavigationView_ExpandedModeThresholdWidth;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.FooterMenuItems":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "FooterMenuItems", "System.Collections.Generic.IList`1<Object>");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_406_NavigationView_FooterMenuItems;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.FooterMenuItemsSource":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "FooterMenuItemsSource", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_407_NavigationView_FooterMenuItemsSource;
			xamlMember.Setter = set_407_NavigationView_FooterMenuItemsSource;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.Header":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "Header", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_408_NavigationView_Header;
			xamlMember.Setter = set_408_NavigationView_Header;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.HeaderTemplate":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "HeaderTemplate", "Microsoft.UI.Xaml.DataTemplate");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_409_NavigationView_HeaderTemplate;
			xamlMember.Setter = set_409_NavigationView_HeaderTemplate;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.IsBackButtonVisible":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "IsBackButtonVisible", "Microsoft.UI.Xaml.Controls.NavigationViewBackButtonVisible");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_410_NavigationView_IsBackButtonVisible;
			xamlMember.Setter = set_410_NavigationView_IsBackButtonVisible;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.IsBackEnabled":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "IsBackEnabled", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_411_NavigationView_IsBackEnabled;
			xamlMember.Setter = set_411_NavigationView_IsBackEnabled;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.IsPaneOpen":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "IsPaneOpen", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_412_NavigationView_IsPaneOpen;
			xamlMember.Setter = set_412_NavigationView_IsPaneOpen;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.IsPaneToggleButtonVisible":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "IsPaneToggleButtonVisible", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_413_NavigationView_IsPaneToggleButtonVisible;
			xamlMember.Setter = set_413_NavigationView_IsPaneToggleButtonVisible;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.IsPaneVisible":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "IsPaneVisible", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_414_NavigationView_IsPaneVisible;
			xamlMember.Setter = set_414_NavigationView_IsPaneVisible;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.IsSettingsVisible":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "IsSettingsVisible", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_415_NavigationView_IsSettingsVisible;
			xamlMember.Setter = set_415_NavigationView_IsSettingsVisible;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.IsTitleBarAutoPaddingEnabled":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "IsTitleBarAutoPaddingEnabled", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_416_NavigationView_IsTitleBarAutoPaddingEnabled;
			xamlMember.Setter = set_416_NavigationView_IsTitleBarAutoPaddingEnabled;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.MenuItemContainerStyle":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "MenuItemContainerStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_417_NavigationView_MenuItemContainerStyle;
			xamlMember.Setter = set_417_NavigationView_MenuItemContainerStyle;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.MenuItemContainerStyleSelector":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "MenuItemContainerStyleSelector", "Microsoft.UI.Xaml.Controls.StyleSelector");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_418_NavigationView_MenuItemContainerStyleSelector;
			xamlMember.Setter = set_418_NavigationView_MenuItemContainerStyleSelector;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.MenuItemTemplate":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "MenuItemTemplate", "Microsoft.UI.Xaml.DataTemplate");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_419_NavigationView_MenuItemTemplate;
			xamlMember.Setter = set_419_NavigationView_MenuItemTemplate;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.MenuItemTemplateSelector":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "MenuItemTemplateSelector", "Microsoft.UI.Xaml.Controls.DataTemplateSelector");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_420_NavigationView_MenuItemTemplateSelector;
			xamlMember.Setter = set_420_NavigationView_MenuItemTemplateSelector;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.MenuItems":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "MenuItems", "System.Collections.Generic.IList`1<Object>");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_421_NavigationView_MenuItems;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.MenuItemsSource":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "MenuItemsSource", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_422_NavigationView_MenuItemsSource;
			xamlMember.Setter = set_422_NavigationView_MenuItemsSource;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.OverflowLabelMode":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "OverflowLabelMode", "Microsoft.UI.Xaml.Controls.NavigationViewOverflowLabelMode");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_423_NavigationView_OverflowLabelMode;
			xamlMember.Setter = set_423_NavigationView_OverflowLabelMode;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.PaneCustomContent":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "PaneCustomContent", "Microsoft.UI.Xaml.UIElement");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_424_NavigationView_PaneCustomContent;
			xamlMember.Setter = set_424_NavigationView_PaneCustomContent;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.PaneDisplayMode":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "PaneDisplayMode", "Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_425_NavigationView_PaneDisplayMode;
			xamlMember.Setter = set_425_NavigationView_PaneDisplayMode;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.PaneFooter":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "PaneFooter", "Microsoft.UI.Xaml.UIElement");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_426_NavigationView_PaneFooter;
			xamlMember.Setter = set_426_NavigationView_PaneFooter;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.PaneHeader":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "PaneHeader", "Microsoft.UI.Xaml.UIElement");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_427_NavigationView_PaneHeader;
			xamlMember.Setter = set_427_NavigationView_PaneHeader;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.PaneTitle":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "PaneTitle", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_428_NavigationView_PaneTitle;
			xamlMember.Setter = set_428_NavigationView_PaneTitle;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.SelectedItem":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "SelectedItem", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_429_NavigationView_SelectedItem;
			xamlMember.Setter = set_429_NavigationView_SelectedItem;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.SelectionFollowsFocus":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "SelectionFollowsFocus", "Microsoft.UI.Xaml.Controls.NavigationViewSelectionFollowsFocus");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_430_NavigationView_SelectionFollowsFocus;
			xamlMember.Setter = set_430_NavigationView_SelectionFollowsFocus;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.SettingsItem":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "SettingsItem", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_431_NavigationView_SettingsItem;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.ShoulderNavigationEnabled":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "ShoulderNavigationEnabled", "Microsoft.UI.Xaml.Controls.NavigationViewShoulderNavigationEnabled");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_432_NavigationView_ShoulderNavigationEnabled;
			xamlMember.Setter = set_432_NavigationView_ShoulderNavigationEnabled;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationView.TemplateSettings":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationView");
			xamlMember = new XamlMember(this, "TemplateSettings", "Microsoft.UI.Xaml.Controls.NavigationViewTemplateSettings");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_433_NavigationView_TemplateSettings;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationRailItem.NotificationBadge":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationRailItem");
			xamlMember = new XamlMember(this, "NotificationBadge", "Samsung.OneUI.WinUI.Controls.BadgeBase");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_434_NavigationRailItem_NotificationBadge;
			xamlMember.Setter = set_434_NavigationRailItem_NotificationBadge;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationRailItem.SvgIconStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationRailItem");
			xamlMember = new XamlMember(this, "SvgIconStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_435_NavigationRailItem_SvgIconStyle;
			xamlMember.Setter = set_435_NavigationRailItem_SvgIconStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationRailItem.PngIconPath":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationRailItem");
			xamlMember = new XamlMember(this, "PngIconPath", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_436_NavigationRailItem_PngIconPath;
			xamlMember.Setter = set_436_NavigationRailItem_PngIconPath;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationViewItem.CompactPaneLength":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationViewItem");
			xamlMember = new XamlMember(this, "CompactPaneLength", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_437_NavigationViewItem_CompactPaneLength;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationViewItem.HasUnrealizedChildren":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationViewItem");
			xamlMember = new XamlMember(this, "HasUnrealizedChildren", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_438_NavigationViewItem_HasUnrealizedChildren;
			xamlMember.Setter = set_438_NavigationViewItem_HasUnrealizedChildren;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationViewItem.Icon":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationViewItem");
			xamlMember = new XamlMember(this, "Icon", "Microsoft.UI.Xaml.Controls.IconElement");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_439_NavigationViewItem_Icon;
			xamlMember.Setter = set_439_NavigationViewItem_Icon;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationViewItem.InfoBadge":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationViewItem");
			xamlMember = new XamlMember(this, "InfoBadge", "Microsoft.UI.Xaml.Controls.InfoBadge");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_440_NavigationViewItem_InfoBadge;
			xamlMember.Setter = set_440_NavigationViewItem_InfoBadge;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationViewItem.IsChildSelected":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationViewItem");
			xamlMember = new XamlMember(this, "IsChildSelected", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_441_NavigationViewItem_IsChildSelected;
			xamlMember.Setter = set_441_NavigationViewItem_IsChildSelected;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationViewItem.IsExpanded":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationViewItem");
			xamlMember = new XamlMember(this, "IsExpanded", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_442_NavigationViewItem_IsExpanded;
			xamlMember.Setter = set_442_NavigationViewItem_IsExpanded;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationViewItem.MenuItems":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationViewItem");
			xamlMember = new XamlMember(this, "MenuItems", "System.Collections.Generic.IList`1<Object>");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_443_NavigationViewItem_MenuItems;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationViewItem.MenuItemsSource":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationViewItem");
			xamlMember = new XamlMember(this, "MenuItemsSource", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_444_NavigationViewItem_MenuItemsSource;
			xamlMember.Setter = set_444_NavigationViewItem_MenuItemsSource;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationViewItem.SelectsOnInvoked":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationViewItem");
			xamlMember = new XamlMember(this, "SelectsOnInvoked", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_445_NavigationViewItem_SelectsOnInvoked;
			xamlMember.Setter = set_445_NavigationViewItem_SelectsOnInvoked;
			break;
		case "Microsoft.UI.Xaml.Controls.NavigationViewItemBase.IsSelected":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.NavigationViewItemBase");
			xamlMember = new XamlMember(this, "IsSelected", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_446_NavigationViewItemBase_IsSelected;
			xamlMember.Setter = set_446_NavigationViewItemBase_IsSelected;
			break;
		case "Microsoft.UI.Xaml.Controls.Primitives.NavigationViewItemPresenter.Icon":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.NavigationViewItemPresenter");
			xamlMember = new XamlMember(this, "Icon", "Microsoft.UI.Xaml.Controls.IconElement");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_447_NavigationViewItemPresenter_Icon;
			xamlMember.Setter = set_447_NavigationViewItemPresenter_Icon;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationRailItemPresenter.NotificationBadge":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationRailItemPresenter");
			xamlMember = new XamlMember(this, "NotificationBadge", "Samsung.OneUI.WinUI.Controls.BadgeBase");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_448_NavigationRailItemPresenter_NotificationBadge;
			xamlMember.Setter = set_448_NavigationRailItemPresenter_NotificationBadge;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationRailItemPresenter.PngIconPath":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationRailItemPresenter");
			xamlMember = new XamlMember(this, "PngIconPath", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_449_NavigationRailItemPresenter_PngIconPath;
			xamlMember.Setter = set_449_NavigationRailItemPresenter_PngIconPath;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationRailItemPresenter.SvgIconStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationRailItemPresenter");
			xamlMember = new XamlMember(this, "SvgIconStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_450_NavigationRailItemPresenter_SvgIconStyle;
			xamlMember.Setter = set_450_NavigationRailItemPresenter_SvgIconStyle;
			break;
		case "Microsoft.UI.Xaml.Controls.Primitives.NavigationViewItemPresenter.InfoBadge":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.NavigationViewItemPresenter");
			xamlMember = new XamlMember(this, "InfoBadge", "Microsoft.UI.Xaml.Controls.InfoBadge");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_451_NavigationViewItemPresenter_InfoBadge;
			xamlMember.Setter = set_451_NavigationViewItemPresenter_InfoBadge;
			break;
		case "Microsoft.UI.Xaml.Controls.Primitives.NavigationViewItemPresenter.TemplateSettings":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.Primitives.NavigationViewItemPresenter");
			xamlMember = new XamlMember(this, "TemplateSettings", "Microsoft.UI.Xaml.Controls.Primitives.NavigationViewItemPresenterTemplateSettings");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_452_NavigationViewItemPresenter_TemplateSettings;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.Controls.AnimatedIcon.Source":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.AnimatedIcon");
			xamlMember = new XamlMember(this, "Source", "Microsoft.UI.Xaml.Controls.IAnimatedVisualSource2");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_453_AnimatedIcon_Source;
			xamlMember.Setter = set_453_AnimatedIcon_Source;
			break;
		case "Microsoft.UI.Xaml.Controls.AnimatedIcon.FallbackIconSource":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.AnimatedIcon");
			xamlMember = new XamlMember(this, "FallbackIconSource", "Microsoft.UI.Xaml.Controls.IconSource");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_454_AnimatedIcon_FallbackIconSource;
			xamlMember.Setter = set_454_AnimatedIcon_FallbackIconSource;
			break;
		case "Microsoft.UI.Xaml.Controls.AnimatedIcon.MirroredWhenRightToLeft":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.AnimatedIcon");
			xamlMember = new XamlMember(this, "MirroredWhenRightToLeft", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_455_AnimatedIcon_MirroredWhenRightToLeft;
			xamlMember.Setter = set_455_AnimatedIcon_MirroredWhenRightToLeft;
			break;
		case "Microsoft.UI.Xaml.Controls.AnimatedIcon.State":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.AnimatedIcon");
			xamlMember = new XamlMember(this, "State", "String");
			xamlMember.SetTargetTypeName("Microsoft.UI.Xaml.DependencyObject");
			xamlMember.SetIsDependencyProperty();
			xamlMember.SetIsAttachable();
			xamlMember.Getter = get_456_AnimatedIcon_State;
			xamlMember.Setter = set_456_AnimatedIcon_State;
			break;
		case "Microsoft.UI.Xaml.Controls.AnimatedVisuals.AnimatedChevronUpDownSmallVisualSource.Markers":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.AnimatedVisuals.AnimatedChevronUpDownSmallVisualSource");
			xamlMember = new XamlMember(this, "Markers", "System.Collections.Generic.IReadOnlyDictionary`2<String, Double>");
			xamlMember.Getter = get_457_AnimatedChevronUpDownSmallVisualSource_Markers;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.Controls.ItemsRepeaterScrollHost.ScrollViewer":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ItemsRepeaterScrollHost");
			xamlMember = new XamlMember(this, "ScrollViewer", "Microsoft.UI.Xaml.Controls.ScrollViewer");
			xamlMember.Getter = get_458_ItemsRepeaterScrollHost_ScrollViewer;
			xamlMember.Setter = set_458_ItemsRepeaterScrollHost_ScrollViewer;
			break;
		case "Microsoft.UI.Xaml.Controls.ItemsRepeaterScrollHost.CurrentAnchor":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ItemsRepeaterScrollHost");
			xamlMember = new XamlMember(this, "CurrentAnchor", "Microsoft.UI.Xaml.UIElement");
			xamlMember.Getter = get_459_ItemsRepeaterScrollHost_CurrentAnchor;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.Controls.ItemsRepeaterScrollHost.HorizontalAnchorRatio":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ItemsRepeaterScrollHost");
			xamlMember = new XamlMember(this, "HorizontalAnchorRatio", "Double");
			xamlMember.Getter = get_460_ItemsRepeaterScrollHost_HorizontalAnchorRatio;
			xamlMember.Setter = set_460_ItemsRepeaterScrollHost_HorizontalAnchorRatio;
			break;
		case "Microsoft.UI.Xaml.Controls.ItemsRepeaterScrollHost.VerticalAnchorRatio":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ItemsRepeaterScrollHost");
			xamlMember = new XamlMember(this, "VerticalAnchorRatio", "Double");
			xamlMember.Getter = get_461_ItemsRepeaterScrollHost_VerticalAnchorRatio;
			xamlMember.Setter = set_461_ItemsRepeaterScrollHost_VerticalAnchorRatio;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationView.IsSettingsVisible":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationView");
			xamlMember = new XamlMember(this, "IsSettingsVisible", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_462_NavigationView_IsSettingsVisible;
			xamlMember.Setter = set_462_NavigationView_IsSettingsVisible;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationView.IsPaneAutoCompactEnabled":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationView");
			xamlMember = new XamlMember(this, "IsPaneAutoCompactEnabled", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_463_NavigationView_IsPaneAutoCompactEnabled;
			xamlMember.Setter = set_463_NavigationView_IsPaneAutoCompactEnabled;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationView.IsInitialPaneOpen":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationView");
			xamlMember = new XamlMember(this, "IsInitialPaneOpen", "System.Nullable`1<Boolean>");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_464_NavigationView_IsInitialPaneOpen;
			xamlMember.Setter = set_464_NavigationView_IsInitialPaneOpen;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationView.PaneToggleNotificationBadge":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationView");
			xamlMember = new XamlMember(this, "PaneToggleNotificationBadge", "Samsung.OneUI.WinUI.Controls.BadgeBase");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_465_NavigationView_PaneToggleNotificationBadge;
			xamlMember.Setter = set_465_NavigationView_PaneToggleNotificationBadge;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationView.SettingsNavigationItemNotificationBadge":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationView");
			xamlMember = new XamlMember(this, "SettingsNavigationItemNotificationBadge", "Samsung.OneUI.WinUI.Controls.BadgeBase");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_466_NavigationView_SettingsNavigationItemNotificationBadge;
			xamlMember.Setter = set_466_NavigationView_SettingsNavigationItemNotificationBadge;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationView.CollapseBreakPoint":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationView");
			xamlMember = new XamlMember(this, "CollapseBreakPoint", "Double");
			xamlMember.Getter = get_467_NavigationView_CollapseBreakPoint;
			xamlMember.Setter = set_467_NavigationView_CollapseBreakPoint;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationView.ExpandBreakPoint":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationView");
			xamlMember = new XamlMember(this, "ExpandBreakPoint", "Double");
			xamlMember.Getter = get_468_NavigationView_ExpandBreakPoint;
			xamlMember.Setter = set_468_NavigationView_ExpandBreakPoint;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationView.CompactModeThresholdWidth":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationView");
			xamlMember = new XamlMember(this, "CompactModeThresholdWidth", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_469_NavigationView_CompactModeThresholdWidth;
			xamlMember.Setter = set_469_NavigationView_CompactModeThresholdWidth;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationView.ExpandedModeThresholdWidth":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationView");
			xamlMember = new XamlMember(this, "ExpandedModeThresholdWidth", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_470_NavigationView_ExpandedModeThresholdWidth;
			xamlMember.Setter = set_470_NavigationView_ExpandedModeThresholdWidth;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationViewItem.SvgIconStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationViewItem");
			xamlMember = new XamlMember(this, "SvgIconStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_471_NavigationViewItem_SvgIconStyle;
			xamlMember.Setter = set_471_NavigationViewItem_SvgIconStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationViewItem.PngIconPath":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationViewItem");
			xamlMember = new XamlMember(this, "PngIconPath", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_472_NavigationViewItem_PngIconPath;
			xamlMember.Setter = set_472_NavigationViewItem_PngIconPath;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationViewItem.NotificationBadge":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationViewItem");
			xamlMember = new XamlMember(this, "NotificationBadge", "Samsung.OneUI.WinUI.Controls.BadgeBase");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_473_NavigationViewItem_NotificationBadge;
			xamlMember.Setter = set_473_NavigationViewItem_NotificationBadge;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationViewItemPresenter.NotificationBadge":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationViewItemPresenter");
			xamlMember = new XamlMember(this, "NotificationBadge", "Samsung.OneUI.WinUI.Controls.BadgeBase");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_474_NavigationViewItemPresenter_NotificationBadge;
			xamlMember.Setter = set_474_NavigationViewItemPresenter_NotificationBadge;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationViewItemPresenter.PngIconPath":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationViewItemPresenter");
			xamlMember = new XamlMember(this, "PngIconPath", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_475_NavigationViewItemPresenter_PngIconPath;
			xamlMember.Setter = set_475_NavigationViewItemPresenter_PngIconPath;
			break;
		case "Samsung.OneUI.WinUI.Controls.NavigationViewItemPresenter.SvgIconStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.NavigationViewItemPresenter");
			xamlMember = new XamlMember(this, "SvgIconStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_476_NavigationViewItemPresenter_SvgIconStyle;
			xamlMember.Setter = set_476_NavigationViewItemPresenter_SvgIconStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.PageIndicator.AutoPlayInterval":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.PageIndicator");
			xamlMember = new XamlMember(this, "AutoPlayInterval", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_477_PageIndicator_AutoPlayInterval;
			xamlMember.Setter = set_477_PageIndicator_AutoPlayInterval;
			break;
		case "Samsung.OneUI.WinUI.Controls.PageIndicator.NumberOfPages":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.PageIndicator");
			xamlMember = new XamlMember(this, "NumberOfPages", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_478_PageIndicator_NumberOfPages;
			xamlMember.Setter = set_478_PageIndicator_NumberOfPages;
			break;
		case "Samsung.OneUI.WinUI.Controls.PageIndicator.SelectedPageIndex":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.PageIndicator");
			xamlMember = new XamlMember(this, "SelectedPageIndex", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_479_PageIndicator_SelectedPageIndex;
			xamlMember.Setter = set_479_PageIndicator_SelectedPageIndex;
			break;
		case "Samsung.OneUI.WinUI.Controls.PageIndicator.MaxVisiblePips":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.PageIndicator");
			xamlMember = new XamlMember(this, "MaxVisiblePips", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_480_PageIndicator_MaxVisiblePips;
			xamlMember.Setter = set_480_PageIndicator_MaxVisiblePips;
			break;
		case "Samsung.OneUI.WinUI.Controls.PageIndicator.PreviousButtonVisibility":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.PageIndicator");
			xamlMember = new XamlMember(this, "PreviousButtonVisibility", "Microsoft.UI.Xaml.Visibility");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_481_PageIndicator_PreviousButtonVisibility;
			xamlMember.Setter = set_481_PageIndicator_PreviousButtonVisibility;
			break;
		case "Samsung.OneUI.WinUI.Controls.PageIndicator.NextButtonVisibility":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.PageIndicator");
			xamlMember = new XamlMember(this, "NextButtonVisibility", "Microsoft.UI.Xaml.Visibility");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_482_PageIndicator_NextButtonVisibility;
			xamlMember.Setter = set_482_PageIndicator_NextButtonVisibility;
			break;
		case "Samsung.OneUI.WinUI.Controls.PageIndicator.PlayPauseButtonVisibility":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.PageIndicator");
			xamlMember = new XamlMember(this, "PlayPauseButtonVisibility", "Microsoft.UI.Xaml.Visibility");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_483_PageIndicator_PlayPauseButtonVisibility;
			xamlMember.Setter = set_483_PageIndicator_PlayPauseButtonVisibility;
			break;
		case "Samsung.OneUI.WinUI.Controls.PageIndicator.IsClickActionEnable":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.PageIndicator");
			xamlMember = new XamlMember(this, "IsClickActionEnable", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_484_PageIndicator_IsClickActionEnable;
			xamlMember.Setter = set_484_PageIndicator_IsClickActionEnable;
			break;
		case "Samsung.OneUI.WinUI.Controls.PageIndicator.IsLooping":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.PageIndicator");
			xamlMember = new XamlMember(this, "IsLooping", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_485_PageIndicator_IsLooping;
			xamlMember.Setter = set_485_PageIndicator_IsLooping;
			break;
		case "Samsung.OneUI.WinUI.Controls.ProgressBar.Text":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ProgressBar");
			xamlMember = new XamlMember(this, "Text", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_486_ProgressBar_Text;
			xamlMember.Setter = set_486_ProgressBar_Text;
			break;
		case "Microsoft.UI.Xaml.Controls.ProgressBar.IsIndeterminate":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ProgressBar");
			xamlMember = new XamlMember(this, "IsIndeterminate", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_487_ProgressBar_IsIndeterminate;
			xamlMember.Setter = set_487_ProgressBar_IsIndeterminate;
			break;
		case "Microsoft.UI.Xaml.Controls.ProgressBar.ShowError":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ProgressBar");
			xamlMember = new XamlMember(this, "ShowError", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_488_ProgressBar_ShowError;
			xamlMember.Setter = set_488_ProgressBar_ShowError;
			break;
		case "Microsoft.UI.Xaml.Controls.ProgressBar.ShowPaused":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ProgressBar");
			xamlMember = new XamlMember(this, "ShowPaused", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_489_ProgressBar_ShowPaused;
			xamlMember.Setter = set_489_ProgressBar_ShowPaused;
			break;
		case "Microsoft.UI.Xaml.Controls.ProgressBar.TemplateSettings":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.ProgressBar");
			xamlMember = new XamlMember(this, "TemplateSettings", "Microsoft.UI.Xaml.Controls.ProgressBarTemplateSettings");
			xamlMember.Getter = get_490_ProgressBar_TemplateSettings;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.ProgressCircleDeterminate.Foreground":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ProgressCircleDeterminate");
			xamlMember = new XamlMember(this, "Foreground", "Microsoft.UI.Xaml.Media.Brush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_491_ProgressCircleDeterminate_Foreground;
			xamlMember.Setter = set_491_ProgressCircleDeterminate_Foreground;
			break;
		case "Samsung.OneUI.WinUI.Controls.ProgressCircleDeterminate.Background":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ProgressCircleDeterminate");
			xamlMember = new XamlMember(this, "Background", "Microsoft.UI.Xaml.Media.Brush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_492_ProgressCircleDeterminate_Background;
			xamlMember.Setter = set_492_ProgressCircleDeterminate_Background;
			break;
		case "Samsung.OneUI.WinUI.Controls.ProgressCircleDeterminate.Value":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ProgressCircleDeterminate");
			xamlMember = new XamlMember(this, "Value", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_493_ProgressCircleDeterminate_Value;
			xamlMember.Setter = set_493_ProgressCircleDeterminate_Value;
			break;
		case "Samsung.OneUI.WinUI.Controls.ProgressCircleDeterminate.Type":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ProgressCircleDeterminate");
			xamlMember = new XamlMember(this, "Type", "Samsung.OneUI.WinUI.Controls.ProgressCircleDeterminateType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_494_ProgressCircleDeterminate_Type;
			xamlMember.Setter = set_494_ProgressCircleDeterminate_Type;
			break;
		case "Microsoft.UI.Xaml.Controls.RadioButtons.Items":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.RadioButtons");
			xamlMember = new XamlMember(this, "Items", "System.Collections.Generic.IList`1<Object>");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_495_RadioButtons_Items;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.Controls.RadioButtons.Header":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.RadioButtons");
			xamlMember = new XamlMember(this, "Header", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_496_RadioButtons_Header;
			xamlMember.Setter = set_496_RadioButtons_Header;
			break;
		case "Microsoft.UI.Xaml.Controls.RadioButtons.HeaderTemplate":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.RadioButtons");
			xamlMember = new XamlMember(this, "HeaderTemplate", "Microsoft.UI.Xaml.DataTemplate");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_497_RadioButtons_HeaderTemplate;
			xamlMember.Setter = set_497_RadioButtons_HeaderTemplate;
			break;
		case "Microsoft.UI.Xaml.Controls.RadioButtons.ItemTemplate":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.RadioButtons");
			xamlMember = new XamlMember(this, "ItemTemplate", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_498_RadioButtons_ItemTemplate;
			xamlMember.Setter = set_498_RadioButtons_ItemTemplate;
			break;
		case "Microsoft.UI.Xaml.Controls.RadioButtons.ItemsSource":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.RadioButtons");
			xamlMember = new XamlMember(this, "ItemsSource", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_499_RadioButtons_ItemsSource;
			xamlMember.Setter = set_499_RadioButtons_ItemsSource;
			break;
		case "Microsoft.UI.Xaml.Controls.RadioButtons.MaxColumns":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.RadioButtons");
			xamlMember = new XamlMember(this, "MaxColumns", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_500_RadioButtons_MaxColumns;
			xamlMember.Setter = set_500_RadioButtons_MaxColumns;
			break;
		case "Microsoft.UI.Xaml.Controls.RadioButtons.SelectedIndex":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.RadioButtons");
			xamlMember = new XamlMember(this, "SelectedIndex", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_501_RadioButtons_SelectedIndex;
			xamlMember.Setter = set_501_RadioButtons_SelectedIndex;
			break;
		case "Microsoft.UI.Xaml.Controls.RadioButtons.SelectedItem":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.Controls.RadioButtons");
			xamlMember = new XamlMember(this, "SelectedItem", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_502_RadioButtons_SelectedItem;
			xamlMember.Setter = set_502_RadioButtons_SelectedItem;
			break;
		case "Microsoft.UI.Xaml.GridLength.Value":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.GridLength");
			xamlMember = new XamlMember(this, "Value", "Double");
			xamlMember.Getter = get_503_GridLength_Value;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.GridLength.GridUnitType":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.GridLength");
			xamlMember = new XamlMember(this, "GridUnitType", "Microsoft.UI.Xaml.GridUnitType");
			xamlMember.Getter = get_504_GridLength_GridUnitType;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.GridLength.IsAbsolute":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.GridLength");
			xamlMember = new XamlMember(this, "IsAbsolute", "Boolean");
			xamlMember.Getter = get_505_GridLength_IsAbsolute;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.GridLength.IsAuto":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.GridLength");
			xamlMember = new XamlMember(this, "IsAuto", "Boolean");
			xamlMember.Getter = get_506_GridLength_IsAuto;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.UI.Xaml.GridLength.IsStar":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.UI.Xaml.GridLength");
			xamlMember = new XamlMember(this, "IsStar", "Boolean");
			xamlMember.Getter = get_507_GridLength_IsStar;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.Behaviors.ThumbDisabledScrollBarDimensionsBehavior.LargeRepeatButton":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Behaviors.ThumbDisabledScrollBarDimensionsBehavior");
			xamlMember = new XamlMember(this, "LargeRepeatButton", "Microsoft.UI.Xaml.Controls.Primitives.RepeatButton");
			xamlMember.Getter = get_508_ThumbDisabledScrollBarDimensionsBehavior_LargeRepeatButton;
			xamlMember.Setter = set_508_ThumbDisabledScrollBarDimensionsBehavior_LargeRepeatButton;
			break;
		case "Samsung.OneUI.WinUI.Controls.Behaviors.ThumbDisabledScrollBarDimensionsBehavior.ScrollBarReference":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Behaviors.ThumbDisabledScrollBarDimensionsBehavior");
			xamlMember = new XamlMember(this, "ScrollBarReference", "Microsoft.UI.Xaml.Controls.Primitives.ScrollBar");
			xamlMember.Getter = get_509_ThumbDisabledScrollBarDimensionsBehavior_ScrollBarReference;
			xamlMember.Setter = set_509_ThumbDisabledScrollBarDimensionsBehavior_ScrollBarReference;
			break;
		case "Samsung.OneUI.WinUI.Controls.Behaviors.ThumbDisabledScrollBarDimensionsBehavior.SmallRepeatButton":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Behaviors.ThumbDisabledScrollBarDimensionsBehavior");
			xamlMember = new XamlMember(this, "SmallRepeatButton", "Microsoft.UI.Xaml.Controls.Primitives.RepeatButton");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_510_ThumbDisabledScrollBarDimensionsBehavior_SmallRepeatButton;
			xamlMember.Setter = set_510_ThumbDisabledScrollBarDimensionsBehavior_SmallRepeatButton;
			break;
		case "Microsoft.Xaml.Interactivity.Behavior`1<Microsoft.UI.Xaml.Controls.Primitives.Thumb>.AssociatedObject":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.Xaml.Interactivity.Behavior`1<Microsoft.UI.Xaml.Controls.Primitives.Thumb>");
			xamlMember = new XamlMember(this, "AssociatedObject", "Microsoft.UI.Xaml.Controls.Primitives.Thumb");
			xamlMember.Getter = get_511_Behavior_AssociatedObject;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.Xaml.Interactivity.Trigger.Actions":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.Xaml.Interactivity.Trigger");
			xamlMember = new XamlMember(this, "Actions", "Microsoft.Xaml.Interactivity.ActionCollection");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_512_Trigger_Actions;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.Xaml.Interactions.Core.DataTriggerBehavior.Binding":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.Xaml.Interactions.Core.DataTriggerBehavior");
			xamlMember = new XamlMember(this, "Binding", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_513_DataTriggerBehavior_Binding;
			xamlMember.Setter = set_513_DataTriggerBehavior_Binding;
			break;
		case "Microsoft.Xaml.Interactions.Core.DataTriggerBehavior.Value":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.Xaml.Interactions.Core.DataTriggerBehavior");
			xamlMember = new XamlMember(this, "Value", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_514_DataTriggerBehavior_Value;
			xamlMember.Setter = set_514_DataTriggerBehavior_Value;
			break;
		case "Microsoft.Xaml.Interactions.Core.DataTriggerBehavior.ComparisonCondition":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.Xaml.Interactions.Core.DataTriggerBehavior");
			xamlMember = new XamlMember(this, "ComparisonCondition", "Microsoft.Xaml.Interactions.Core.ComparisonConditionType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_515_DataTriggerBehavior_ComparisonCondition;
			xamlMember.Setter = set_515_DataTriggerBehavior_ComparisonCondition;
			break;
		case "Microsoft.Xaml.Interactivity.Behavior.AssociatedObject":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.Xaml.Interactivity.Behavior");
			xamlMember = new XamlMember(this, "AssociatedObject", "Microsoft.UI.Xaml.DependencyObject");
			xamlMember.Getter = get_516_Behavior_AssociatedObject;
			xamlMember.SetIsReadOnly();
			break;
		case "Microsoft.Xaml.Interactions.Core.GoToStateAction.StateName":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.Xaml.Interactions.Core.GoToStateAction");
			xamlMember = new XamlMember(this, "StateName", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_517_GoToStateAction_StateName;
			xamlMember.Setter = set_517_GoToStateAction_StateName;
			break;
		case "Microsoft.Xaml.Interactions.Core.GoToStateAction.TargetObject":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.Xaml.Interactions.Core.GoToStateAction");
			xamlMember = new XamlMember(this, "TargetObject", "Microsoft.UI.Xaml.FrameworkElement");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_518_GoToStateAction_TargetObject;
			xamlMember.Setter = set_518_GoToStateAction_TargetObject;
			break;
		case "Microsoft.Xaml.Interactions.Core.GoToStateAction.UseTransitions":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.Xaml.Interactions.Core.GoToStateAction");
			xamlMember = new XamlMember(this, "UseTransitions", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_519_GoToStateAction_UseTransitions;
			xamlMember.Setter = set_519_GoToStateAction_UseTransitions;
			break;
		case "Samsung.OneUI.WinUI.Common.ThumbCompositeTransformScaleStateTrigger.ThumbReference":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Common.ThumbCompositeTransformScaleStateTrigger");
			xamlMember = new XamlMember(this, "ThumbReference", "Microsoft.UI.Xaml.Controls.Primitives.Thumb");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_520_ThumbCompositeTransformScaleStateTrigger_ThumbReference;
			xamlMember.Setter = set_520_ThumbCompositeTransformScaleStateTrigger_ThumbReference;
			break;
		case "Samsung.OneUI.WinUI.Controls.SearchPopupList.HighlightSearchedWords":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SearchPopupList");
			xamlMember = new XamlMember(this, "HighlightSearchedWords", "Boolean");
			xamlMember.Getter = get_521_SearchPopupList_HighlightSearchedWords;
			xamlMember.Setter = set_521_SearchPopupList_HighlightSearchedWords;
			break;
		case "Samsung.OneUI.WinUI.Controls.SearchPopupList.TextFilter":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SearchPopupList");
			xamlMember = new XamlMember(this, "TextFilter", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_522_SearchPopupList_TextFilter;
			xamlMember.Setter = set_522_SearchPopupList_TextFilter;
			break;
		case "Samsung.OneUI.WinUI.Controls.SearchPopupList.PopupItems":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SearchPopupList");
			xamlMember = new XamlMember(this, "PopupItems", "System.Collections.ObjectModel.ObservableCollection`1<Samsung.OneUI.WinUI.Controls.SearchPopupListItem>");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_523_SearchPopupList_PopupItems;
			xamlMember.Setter = set_523_SearchPopupList_PopupItems;
			break;
		case "Samsung.OneUI.WinUI.Controls.SearchPopupListItem.RemoveButtonTooltipMargin":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SearchPopupListItem");
			xamlMember = new XamlMember(this, "RemoveButtonTooltipMargin", "Microsoft.UI.Xaml.Thickness");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_524_SearchPopupListItem_RemoveButtonTooltipMargin;
			xamlMember.Setter = set_524_SearchPopupListItem_RemoveButtonTooltipMargin;
			break;
		case "Samsung.OneUI.WinUI.Controls.SearchPopupListItem.RemoveButtonTooltipVerticalOffset":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SearchPopupListItem");
			xamlMember = new XamlMember(this, "RemoveButtonTooltipVerticalOffset", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_525_SearchPopupListItem_RemoveButtonTooltipVerticalOffset;
			xamlMember.Setter = set_525_SearchPopupListItem_RemoveButtonTooltipVerticalOffset;
			break;
		case "Samsung.OneUI.WinUI.Controls.SearchPopupListItem.RemoveButtonTooltipContent":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SearchPopupListItem");
			xamlMember = new XamlMember(this, "RemoveButtonTooltipContent", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_526_SearchPopupListItem_RemoveButtonTooltipContent;
			xamlMember.Setter = set_526_SearchPopupListItem_RemoveButtonTooltipContent;
			break;
		case "Samsung.OneUI.WinUI.Controls.SearchPopupListItem.Id":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SearchPopupListItem");
			xamlMember = new XamlMember(this, "Id", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_527_SearchPopupListItem_Id;
			xamlMember.Setter = set_527_SearchPopupListItem_Id;
			break;
		case "Samsung.OneUI.WinUI.Controls.SearchPopupListItem.RemoveButtonVisibility":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SearchPopupListItem");
			xamlMember = new XamlMember(this, "RemoveButtonVisibility", "Microsoft.UI.Xaml.Visibility");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_528_SearchPopupListItem_RemoveButtonVisibility;
			xamlMember.Setter = set_528_SearchPopupListItem_RemoveButtonVisibility;
			break;
		case "Samsung.OneUI.WinUI.Controls.SearchPopupListItem.Text":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SearchPopupListItem");
			xamlMember = new XamlMember(this, "Text", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_529_SearchPopupListItem_Text;
			xamlMember.Setter = set_529_SearchPopupListItem_Text;
			break;
		case "Samsung.OneUI.WinUI.Controls.SearchPopupListItem.Image":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SearchPopupListItem");
			xamlMember = new XamlMember(this, "Image", "Microsoft.UI.Xaml.Media.ImageSource");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_530_SearchPopupListItem_Image;
			xamlMember.Setter = set_530_SearchPopupListItem_Image;
			break;
		case "Samsung.OneUI.WinUI.Controls.SearchPopupListItem.IconSvgStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SearchPopupListItem");
			xamlMember = new XamlMember(this, "IconSvgStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_531_SearchPopupListItem_IconSvgStyle;
			xamlMember.Setter = set_531_SearchPopupListItem_IconSvgStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.SearchPopupList.IsCornerRadiusAutoAdjustmentEnabled":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SearchPopupList");
			xamlMember = new XamlMember(this, "IsCornerRadiusAutoAdjustmentEnabled", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_532_SearchPopupList_IsCornerRadiusAutoAdjustmentEnabled;
			xamlMember.Setter = set_532_SearchPopupList_IsCornerRadiusAutoAdjustmentEnabled;
			break;
		case "Samsung.OneUI.WinUI.Controls.FilterTextBlock.CustomText":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FilterTextBlock");
			xamlMember = new XamlMember(this, "CustomText", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_533_FilterTextBlock_CustomText;
			xamlMember.Setter = set_533_FilterTextBlock_CustomText;
			break;
		case "Samsung.OneUI.WinUI.Controls.FilterTextBlock.TextHighlightBackgroundColor":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FilterTextBlock");
			xamlMember = new XamlMember(this, "TextHighlightBackgroundColor", "Microsoft.UI.Xaml.Media.Brush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_534_FilterTextBlock_TextHighlightBackgroundColor;
			xamlMember.Setter = set_534_FilterTextBlock_TextHighlightBackgroundColor;
			break;
		case "Samsung.OneUI.WinUI.Controls.FilterTextBlock.TextHighlightForegroundColor":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FilterTextBlock");
			xamlMember = new XamlMember(this, "TextHighlightForegroundColor", "Microsoft.UI.Xaml.Media.Brush");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_535_FilterTextBlock_TextHighlightForegroundColor;
			xamlMember.Setter = set_535_FilterTextBlock_TextHighlightForegroundColor;
			break;
		case "Samsung.OneUI.WinUI.Controls.FilterTextBlock.TextTrimming":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FilterTextBlock");
			xamlMember = new XamlMember(this, "TextTrimming", "Microsoft.UI.Xaml.TextTrimming");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_536_FilterTextBlock_TextTrimming;
			xamlMember.Setter = set_536_FilterTextBlock_TextTrimming;
			break;
		case "Samsung.OneUI.WinUI.Controls.FilterTextBlock.SearchedText":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FilterTextBlock");
			xamlMember = new XamlMember(this, "SearchedText", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_537_FilterTextBlock_SearchedText;
			xamlMember.Setter = set_537_FilterTextBlock_SearchedText;
			break;
		case "Samsung.OneUI.WinUI.Controls.FilterTextBlock.ForceApplyTemplate":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.FilterTextBlock");
			xamlMember = new XamlMember(this, "ForceApplyTemplate", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_538_FilterTextBlock_ForceApplyTemplate;
			xamlMember.Setter = set_538_FilterTextBlock_ForceApplyTemplate;
			break;
		case "Samsung.OneUI.WinUI.Controls.SearchPopup.VerticalOffset":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SearchPopup");
			xamlMember = new XamlMember(this, "VerticalOffset", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_539_SearchPopup_VerticalOffset;
			xamlMember.Setter = set_539_SearchPopup_VerticalOffset;
			break;
		case "Samsung.OneUI.WinUI.Controls.SearchPopup.HorizontalOffset":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SearchPopup");
			xamlMember = new XamlMember(this, "HorizontalOffset", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_540_SearchPopup_HorizontalOffset;
			xamlMember.Setter = set_540_SearchPopup_HorizontalOffset;
			break;
		case "Samsung.OneUI.WinUI.Controls.SearchPopup.PopupContent":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SearchPopup");
			xamlMember = new XamlMember(this, "PopupContent", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_541_SearchPopup_PopupContent;
			xamlMember.Setter = set_541_SearchPopup_PopupContent;
			break;
		case "Samsung.OneUI.WinUI.Controls.SearchPopup.AttachTo":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SearchPopup");
			xamlMember = new XamlMember(this, "AttachTo", "Microsoft.UI.Xaml.Controls.Control");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_542_SearchPopup_AttachTo;
			xamlMember.Setter = set_542_SearchPopup_AttachTo;
			break;
		case "Samsung.OneUI.WinUI.AttachedProperties.BackdropBlurExtension.BlurAmount":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.AttachedProperties.BackdropBlurExtension");
			xamlMember = new XamlMember(this, "BlurAmount", "Double");
			xamlMember.SetTargetTypeName("Microsoft.UI.Xaml.DependencyObject");
			xamlMember.SetIsAttachable();
			xamlMember.Getter = get_543_BackdropBlurExtension_BlurAmount;
			xamlMember.Setter = set_543_BackdropBlurExtension_BlurAmount;
			break;
		case "Samsung.OneUI.WinUI.AttachedProperties.BackdropBlurExtension.IsEnabled":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.AttachedProperties.BackdropBlurExtension");
			xamlMember = new XamlMember(this, "IsEnabled", "Boolean");
			xamlMember.SetTargetTypeName("Microsoft.UI.Xaml.DependencyObject");
			xamlMember.SetIsAttachable();
			xamlMember.Getter = get_544_BackdropBlurExtension_IsEnabled;
			xamlMember.Setter = set_544_BackdropBlurExtension_IsEnabled;
			break;
		case "Samsung.OneUI.WinUI.Controls.Slider.ShockValue":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Slider");
			xamlMember = new XamlMember(this, "ShockValue", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_545_Slider_ShockValue;
			xamlMember.Setter = set_545_Slider_ShockValue;
			break;
		case "Samsung.OneUI.WinUI.Controls.Slider.ShockValueType":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Slider");
			xamlMember = new XamlMember(this, "ShockValueType", "Samsung.OneUI.WinUI.Controls.ShockValueType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_546_Slider_ShockValueType;
			xamlMember.Setter = set_546_Slider_ShockValueType;
			break;
		case "Samsung.OneUI.WinUI.Controls.Slider.MaximumValue":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Slider");
			xamlMember = new XamlMember(this, "MaximumValue", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_547_Slider_MaximumValue;
			xamlMember.Setter = set_547_Slider_MaximumValue;
			break;
		case "Samsung.OneUI.WinUI.Controls.Slider.MinimumValue":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Slider");
			xamlMember = new XamlMember(this, "MinimumValue", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_548_Slider_MinimumValue;
			xamlMember.Setter = set_548_Slider_MinimumValue;
			break;
		case "Samsung.OneUI.WinUI.Controls.SliderBase.IsThumbToolTipEnabled":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SliderBase");
			xamlMember = new XamlMember(this, "IsThumbToolTipEnabled", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_549_SliderBase_IsThumbToolTipEnabled;
			xamlMember.Setter = set_549_SliderBase_IsThumbToolTipEnabled;
			break;
		case "Samsung.OneUI.WinUI.Controls.SliderBase.Type":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SliderBase");
			xamlMember = new XamlMember(this, "Type", "Samsung.OneUI.WinUI.Controls.SliderType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_550_SliderBase_Type;
			xamlMember.Setter = set_550_SliderBase_Type;
			break;
		case "Samsung.OneUI.WinUI.Controls.SliderBase.TextValueVisibility":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SliderBase");
			xamlMember = new XamlMember(this, "TextValueVisibility", "Microsoft.UI.Xaml.Visibility");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_551_SliderBase_TextValueVisibility;
			xamlMember.Setter = set_551_SliderBase_TextValueVisibility;
			break;
		case "Samsung.OneUI.WinUI.Controls.BufferSlider.Buffer":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.BufferSlider");
			xamlMember = new XamlMember(this, "Buffer", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_552_BufferSlider_Buffer;
			xamlMember.Setter = set_552_BufferSlider_Buffer;
			break;
		case "Samsung.OneUI.WinUI.Controls.CenterSlider.Orientation":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.CenterSlider");
			xamlMember = new XamlMember(this, "Orientation", "Microsoft.UI.Xaml.Controls.Orientation");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_553_CenterSlider_Orientation;
			xamlMember.Setter = set_553_CenterSlider_Orientation;
			break;
		case "Samsung.OneUI.WinUI.Controls.SubAppBar.Content":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.SubAppBar");
			xamlMember = new XamlMember(this, "Content", "Microsoft.UI.Xaml.UIElement");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_554_SubAppBar_Content;
			xamlMember.Setter = set_554_SubAppBar_Content;
			break;
		case "Samsung.OneUI.WinUI.Controls.TabView.HeaderClipperMargin":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.TabView");
			xamlMember = new XamlMember(this, "HeaderClipperMargin", "Microsoft.UI.Xaml.Thickness");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_555_TabView_HeaderClipperMargin;
			xamlMember.Setter = set_555_TabView_HeaderClipperMargin;
			break;
		case "Samsung.OneUI.WinUI.Controls.TabView.Type":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.TabView");
			xamlMember = new XamlMember(this, "Type", "Samsung.OneUI.WinUI.Controls.TabViewType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_556_TabView_Type;
			xamlMember.Setter = set_556_TabView_Type;
			break;
		case "Samsung.OneUI.WinUI.Controls.TabView.MaxVisibleHeaderInViewport":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.TabView");
			xamlMember = new XamlMember(this, "MaxVisibleHeaderInViewport", "Int32");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_557_TabView_MaxVisibleHeaderInViewport;
			xamlMember.Setter = set_557_TabView_MaxVisibleHeaderInViewport;
			break;
		case "Samsung.OneUI.WinUI.Controls.TabItem.SelectedByKeyboard":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.TabItem");
			xamlMember = new XamlMember(this, "SelectedByKeyboard", "Boolean");
			xamlMember.Getter = get_558_TabItem_SelectedByKeyboard;
			xamlMember.SetIsReadOnly();
			break;
		case "Samsung.OneUI.WinUI.Controls.TabItem.NotificationBadge":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.TabItem");
			xamlMember = new XamlMember(this, "NotificationBadge", "Samsung.OneUI.WinUI.Controls.BadgeBase");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_559_TabItem_NotificationBadge;
			xamlMember.Setter = set_559_TabItem_NotificationBadge;
			break;
		case "Samsung.OneUI.WinUI.Controls.TextField.Type":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.TextField");
			xamlMember = new XamlMember(this, "Type", "Samsung.OneUI.WinUI.Controls.TextFieldType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_560_TextField_Type;
			xamlMember.Setter = set_560_TextField_Type;
			break;
		case "Samsung.OneUI.WinUI.Controls.TextField.ErrorMessage":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.TextField");
			xamlMember = new XamlMember(this, "ErrorMessage", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_561_TextField_ErrorMessage;
			xamlMember.Setter = set_561_TextField_ErrorMessage;
			break;
		case "Samsung.OneUI.WinUI.Controls.TextField.SvgIcon":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.TextField");
			xamlMember = new XamlMember(this, "SvgIcon", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_562_TextField_SvgIcon;
			xamlMember.Setter = set_562_TextField_SvgIcon;
			break;
		case "Samsung.OneUI.WinUI.Controls.TextField.ScrollViewerMaxHeight":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.TextField");
			xamlMember = new XamlMember(this, "ScrollViewerMaxHeight", "Double");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_563_TextField_ScrollViewerMaxHeight;
			xamlMember.Setter = set_563_TextField_ScrollViewerMaxHeight;
			break;
		case "Samsung.OneUI.WinUI.Controls.ThumbnailRadious.ImageLocation":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ThumbnailRadious");
			xamlMember = new XamlMember(this, "ImageLocation", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_564_ThumbnailRadious_ImageLocation;
			xamlMember.Setter = set_564_ThumbnailRadious_ImageLocation;
			break;
		case "Samsung.OneUI.WinUI.Controls.ThumbnailRadious.Title":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ThumbnailRadious");
			xamlMember = new XamlMember(this, "Title", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_565_ThumbnailRadious_Title;
			xamlMember.Setter = set_565_ThumbnailRadious_Title;
			break;
		case "Samsung.OneUI.WinUI.Controls.ThumbnailRadious.Description":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ThumbnailRadious");
			xamlMember = new XamlMember(this, "Description", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_566_ThumbnailRadious_Description;
			xamlMember.Setter = set_566_ThumbnailRadious_Description;
			break;
		case "Samsung.OneUI.WinUI.Controls.ThumbnailRadious.VisualizationMode":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ThumbnailRadious");
			xamlMember = new XamlMember(this, "VisualizationMode", "Samsung.OneUI.WinUI.Controls.ThumbnailRadiousVisualizationMode");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_567_ThumbnailRadious_VisualizationMode;
			xamlMember.Setter = set_567_ThumbnailRadious_VisualizationMode;
			break;
		case "Samsung.OneUI.WinUI.Controls.ThumbnailRadiousGridView.VisualizationMode":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ThumbnailRadiousGridView");
			xamlMember = new XamlMember(this, "VisualizationMode", "Samsung.OneUI.WinUI.Controls.ThumbnailRadiousVisualizationMode");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_568_ThumbnailRadiousGridView_VisualizationMode;
			xamlMember.Setter = set_568_ThumbnailRadiousGridView_VisualizationMode;
			break;
		case "Samsung.OneUI.WinUI.Controls.Titlebar.Title":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.Titlebar");
			xamlMember = new XamlMember(this, "Title", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_569_Titlebar_Title;
			xamlMember.Setter = set_569_Titlebar_Title;
			break;
		case "Samsung.OneUI.WinUI.Controls.ToggleSwitch.Header":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ToggleSwitch");
			xamlMember = new XamlMember(this, "Header", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_570_ToggleSwitch_Header;
			xamlMember.Setter = set_570_ToggleSwitch_Header;
			break;
		case "Samsung.OneUI.WinUI.Controls.ToggleSwitch.IsOn":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ToggleSwitch");
			xamlMember = new XamlMember(this, "IsOn", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_571_ToggleSwitch_IsOn;
			xamlMember.Setter = set_571_ToggleSwitch_IsOn;
			break;
		case "Samsung.OneUI.WinUI.Controls.ToggleSwitch.OffContent":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ToggleSwitch");
			xamlMember = new XamlMember(this, "OffContent", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_572_ToggleSwitch_OffContent;
			xamlMember.Setter = set_572_ToggleSwitch_OffContent;
			break;
		case "Samsung.OneUI.WinUI.Controls.ToggleSwitch.OnContent":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ToggleSwitch");
			xamlMember = new XamlMember(this, "OnContent", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_573_ToggleSwitch_OnContent;
			xamlMember.Setter = set_573_ToggleSwitch_OnContent;
			break;
		case "Samsung.OneUI.WinUI.Controls.ToggleSwitch.Style":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ToggleSwitch");
			xamlMember = new XamlMember(this, "Style", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_574_ToggleSwitch_Style;
			xamlMember.Setter = set_574_ToggleSwitch_Style;
			break;
		case "Samsung.OneUI.WinUI.Controls.ToggleSwitch.Type":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ToggleSwitch");
			xamlMember = new XamlMember(this, "Type", "Samsung.OneUI.WinUI.Controls.Inputs.ToggleSwitch.ToggleSwitchType");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_575_ToggleSwitch_Type;
			xamlMember.Setter = set_575_ToggleSwitch_Type;
			break;
		case "Samsung.OneUI.WinUI.Controls.ToggleSwitch.HeaderTemplate":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ToggleSwitch");
			xamlMember = new XamlMember(this, "HeaderTemplate", "Microsoft.UI.Xaml.DataTemplate");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_576_ToggleSwitch_HeaderTemplate;
			xamlMember.Setter = set_576_ToggleSwitch_HeaderTemplate;
			break;
		case "Samsung.OneUI.WinUI.Controls.ToggleSwitch.OnContentTemplate":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ToggleSwitch");
			xamlMember = new XamlMember(this, "OnContentTemplate", "Microsoft.UI.Xaml.DataTemplate");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_577_ToggleSwitch_OnContentTemplate;
			xamlMember.Setter = set_577_ToggleSwitch_OnContentTemplate;
			break;
		case "Samsung.OneUI.WinUI.Controls.ToggleSwitch.OffContentTemplate":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ToggleSwitch");
			xamlMember = new XamlMember(this, "OffContentTemplate", "Microsoft.UI.Xaml.DataTemplate");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_578_ToggleSwitch_OffContentTemplate;
			xamlMember.Setter = set_578_ToggleSwitch_OffContentTemplate;
			break;
		case "Samsung.OneUI.WinUI.Controls.ToggleSwitchGroup.Content":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ToggleSwitchGroup");
			xamlMember = new XamlMember(this, "Content", "Object");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_579_ToggleSwitchGroup_Content;
			xamlMember.Setter = set_579_ToggleSwitchGroup_Content;
			break;
		case "Samsung.OneUI.WinUI.Controls.ToggleSwitchGroup.Header":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ToggleSwitchGroup");
			xamlMember = new XamlMember(this, "Header", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_580_ToggleSwitchGroup_Header;
			xamlMember.Setter = set_580_ToggleSwitchGroup_Header;
			break;
		case "Samsung.OneUI.WinUI.Controls.ToggleSwitchGroup.OnContent":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ToggleSwitchGroup");
			xamlMember = new XamlMember(this, "OnContent", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_581_ToggleSwitchGroup_OnContent;
			xamlMember.Setter = set_581_ToggleSwitchGroup_OnContent;
			break;
		case "Samsung.OneUI.WinUI.Controls.ToggleSwitchGroup.OffContent":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ToggleSwitchGroup");
			xamlMember = new XamlMember(this, "OffContent", "String");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_582_ToggleSwitchGroup_OffContent;
			xamlMember.Setter = set_582_ToggleSwitchGroup_OffContent;
			break;
		case "Samsung.OneUI.WinUI.Controls.ToggleSwitchGroup.LabelToggleSwitchGroupStyle":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ToggleSwitchGroup");
			xamlMember = new XamlMember(this, "LabelToggleSwitchGroupStyle", "Microsoft.UI.Xaml.Style");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_583_ToggleSwitchGroup_LabelToggleSwitchGroupStyle;
			xamlMember.Setter = set_583_ToggleSwitchGroup_LabelToggleSwitchGroupStyle;
			break;
		case "Samsung.OneUI.WinUI.Controls.ToggleSwitchGroup.IsOn":
			_ = (XamlUserType)GetXamlTypeByName("Samsung.OneUI.WinUI.Controls.ToggleSwitchGroup");
			xamlMember = new XamlMember(this, "IsOn", "Boolean");
			xamlMember.SetIsDependencyProperty();
			xamlMember.Getter = get_584_ToggleSwitchGroup_IsOn;
			xamlMember.Setter = set_584_ToggleSwitchGroup_IsOn;
			break;
		case "Microsoft.Xaml.Interactivity.Behavior`1<Microsoft.UI.Xaml.Controls.ToggleSwitch>.AssociatedObject":
			_ = (XamlUserType)GetXamlTypeByName("Microsoft.Xaml.Interactivity.Behavior`1<Microsoft.UI.Xaml.Controls.ToggleSwitch>");
			xamlMember = new XamlMember(this, "AssociatedObject", "Microsoft.UI.Xaml.Controls.ToggleSwitch");
			xamlMember.Getter = get_585_Behavior_AssociatedObject;
			xamlMember.SetIsReadOnly();
			break;
		}
		return xamlMember;
	}
}
