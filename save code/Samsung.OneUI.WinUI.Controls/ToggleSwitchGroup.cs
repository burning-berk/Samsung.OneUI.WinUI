using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

[ContentProperty(Name = "Content")]
public sealed class ToggleSwitchGroup : Control
{
	private const string PART_LABEL_TOGGLE_SWITCH_GROUP_NAME = "PART_LabelToggleSwitchGroup";

	private const string LABEL_TOGGLE_SWITCH_GROUP_STYLE = "OneUILabelToggleSwitchGroupStyle";

	private ToggleSwitch _toggleSwitchGroup;

	public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(ToggleSwitchGroup), new PropertyMetadata(null));

	public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(string), typeof(ToggleSwitchGroup), new PropertyMetadata(null));

	public static readonly DependencyProperty OnContentProperty = DependencyProperty.Register("OnContent", typeof(string), typeof(ToggleSwitchGroup), new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty OffContentProperty = DependencyProperty.Register("OffContent", typeof(string), typeof(ToggleSwitchGroup), new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty LabelToggleSwitchGroupStyleProperty = DependencyProperty.Register("LabelToggleSwitchGroupStyle", typeof(Style), typeof(ToggleSwitchGroup), new PropertyMetadata(null, OnStylePropertyChanged));

	public static readonly DependencyProperty IsOnProperty = DependencyProperty.Register("IsOn", typeof(bool), typeof(ToggleSwitchGroup), new PropertyMetadata(true, OnIsOnPropertyChanged));

	public object Content
	{
		get
		{
			return GetValue(ContentProperty);
		}
		set
		{
			SetValue(ContentProperty, value);
		}
	}

	public string Header
	{
		get
		{
			return (string)GetValue(HeaderProperty);
		}
		set
		{
			SetValue(HeaderProperty, value);
		}
	}

	public string OnContent
	{
		get
		{
			return (string)GetValue(OnContentProperty);
		}
		set
		{
			SetValue(OnContentProperty, value);
		}
	}

	public string OffContent
	{
		get
		{
			return (string)GetValue(OffContentProperty);
		}
		set
		{
			SetValue(OffContentProperty, value);
		}
	}

	public Style LabelToggleSwitchGroupStyle
	{
		get
		{
			return (Style)GetValue(LabelToggleSwitchGroupStyleProperty);
		}
		set
		{
			SetValue(LabelToggleSwitchGroupStyleProperty, value);
		}
	}

	public bool IsOn
	{
		get
		{
			return (bool)GetValue(IsOnProperty);
		}
		set
		{
			SetValue(IsOnProperty, value);
		}
	}

	public event EventHandler LabelToggleSwitchGroupRequest;

	public ToggleSwitchGroup()
	{
		base.DefaultStyleKey = typeof(ToggleSwitchGroup);
	}

	private static void OnStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ToggleSwitchGroup toggleSwitchGroup)
		{
			toggleSwitchGroup.UpdateStyleMainOnOffToggleSwitch();
		}
	}

	private static void OnIsOnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ToggleSwitchGroup toggleSwitchGroup)
		{
			toggleSwitchGroup.SetToggleSwitch();
		}
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_toggleSwitchGroup = GetTemplateChild("PART_LabelToggleSwitchGroup") as ToggleSwitch;
		if (_toggleSwitchGroup != null)
		{
			UpdateStyleMainOnOffToggleSwitch();
			_toggleSwitchGroup.Toggled += LabelToggleSwitchGroup_Toggled;
		}
	}

	private void UpdateStyleMainOnOffToggleSwitch()
	{
		if (_toggleSwitchGroup != null)
		{
			_toggleSwitchGroup.Style = ((LabelToggleSwitchGroupStyle == null) ? "OneUILabelToggleSwitchGroupStyle".GetStyle() : LabelToggleSwitchGroupStyle);
		}
	}

	private void LabelToggleSwitchGroup_Toggled(object sender, RoutedEventArgs e)
	{
		if (sender is ToggleSwitch)
		{
			IsOn = _toggleSwitchGroup.IsOn;
			InvokeLabelToggleSwitchRequestEvent(sender);
		}
	}

	private void InvokeLabelToggleSwitchRequestEvent(object sender)
	{
		this.LabelToggleSwitchGroupRequest?.Invoke(sender, new EventArgs());
	}

	private void SetToggleSwitch()
	{
		if (_toggleSwitchGroup != null)
		{
			_toggleSwitchGroup.IsOn = IsOn;
		}
	}
}
