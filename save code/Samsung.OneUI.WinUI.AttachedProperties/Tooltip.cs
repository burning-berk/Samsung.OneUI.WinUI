using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Samsung.OneUI.WinUI.Controls;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.AttachedProperties;

public static class Tooltip
{
	private const string TEXTBLOCK_TEXT_PROPERTY_PATH = "Text";

	public static readonly DependencyProperty TextTrimmedEnabledProperty = DependencyProperty.RegisterAttached("TextTrimmedEnabled", typeof(bool), typeof(Tooltip), new PropertyMetadata(false, OnTextTrimmedEnabledChanged));

	public static bool GetTextTrimmedEnabled(DependencyObject obj)
	{
		return (bool)obj.GetValue(TextTrimmedEnabledProperty);
	}

	public static void SetTextTrimmedEnabled(DependencyObject obj, bool value)
	{
		obj.SetValue(TextTrimmedEnabledProperty, value);
	}

	private static void OnTextTrimmedEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		TextBlock textBlock = d as TextBlock;
		if ((object)textBlock != null)
		{
			UIExtensionsInternal.ExecuteWhenLoaded(textBlock, delegate
			{
				ApplyTooltip((bool)e.NewValue, textBlock);
			});
		}
	}

	private static void ApplyTooltip(bool isTextTrimmedEnabled, TextBlock textBlock)
	{
		if (isTextTrimmedEnabled)
		{
			textBlock.IsTextTrimmedChanged += TextBlock_IsTextTrimmedChanged;
			UpdateTooltip(textBlock);
			RoutedEventHandler onUnloaded = null;
			onUnloaded = delegate
			{
				textBlock.Unloaded -= onUnloaded;
				textBlock.IsTextTrimmedChanged -= TextBlock_IsTextTrimmedChanged;
			};
			textBlock.Unloaded += onUnloaded;
		}
		else
		{
			textBlock.IsTextTrimmedChanged -= TextBlock_IsTextTrimmedChanged;
			ToolTipService.SetToolTip(textBlock, null);
		}
	}

	private static void TextBlock_IsTextTrimmedChanged(TextBlock sender, IsTextTrimmedChangedEventArgs args)
	{
		UpdateTooltip(sender);
	}

	private static void UpdateTooltip(TextBlock textBlock)
	{
		if (textBlock.IsTextTrimmed)
		{
			Microsoft.UI.Xaml.Controls.ToolTip toolTip = new Samsung.OneUI.WinUI.Controls.ToolTip();
			Binding binding = new Binding
			{
				Source = textBlock,
				Path = new PropertyPath("Text"),
				Mode = BindingMode.OneWay
			};
			BindingOperations.SetBinding(toolTip, ContentControl.ContentProperty, binding);
			ToolTipService.SetToolTip(textBlock, toolTip);
		}
		else
		{
			ToolTipService.SetToolTip(textBlock, null);
		}
	}
}
