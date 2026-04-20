using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.Xaml.Interactivity;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Behaviors;

public class TooltipForTrimmedButtonBehavior : Behavior<FrameworkElement>
{
	private ToolTip _toolTipAttachedByUser;

	private Button _button;

	private TextBlock _textBlock;

	protected override void OnAttached()
	{
		if (!(base.AssociatedObject == null))
		{
			base.AssociatedObject.Loaded += OnAssociatedObjectLoaded;
			base.AssociatedObject.Unloaded += OnAssociatedObjectUnloaded;
		}
	}

	protected override void OnDetaching()
	{
		if (!(base.AssociatedObject == null))
		{
			base.AssociatedObject.Loaded -= OnAssociatedObjectLoaded;
			base.AssociatedObject.Unloaded -= OnAssociatedObjectUnloaded;
		}
	}

	private void OnAssociatedObjectLoaded(object sender, RoutedEventArgs e)
	{
		if (!(base.AssociatedObject == null))
		{
			_textBlock = UIExtensionsInternal.FindFirstChildOfType<TextBlock>(base.AssociatedObject);
			_button = UIExtensionsInternal.GetVisualParent<Button>(base.AssociatedObject);
			if (!(_button == null) && !(_textBlock == null))
			{
				_toolTipAttachedByUser = ToolTipService.GetToolTip(_button) as ToolTip;
				_textBlock.IsTextTrimmedChanged += OnTextTrimmedChanged;
				UpdateToolTip(_textBlock);
			}
		}
	}

	private void OnAssociatedObjectUnloaded(object sender, RoutedEventArgs e)
	{
		if (!(base.AssociatedObject == null))
		{
			_textBlock.IsTextTrimmedChanged -= OnTextTrimmedChanged;
			_textBlock = null;
			_button = null;
			_toolTipAttachedByUser = null;
		}
	}

	private void OnTextTrimmedChanged(TextBlock sender, IsTextTrimmedChangedEventArgs args)
	{
		UpdateToolTip(sender);
	}

	private void UpdateToolTip(TextBlock textBlock)
	{
		if (textBlock.IsTextTrimmed)
		{
			ToolTip toolTip = new ToolTip();
			Binding binding = new Binding
			{
				Path = new PropertyPath("Text"),
				Source = _textBlock,
				Mode = BindingMode.OneWay
			};
			toolTip.SetBinding(ContentControl.ContentProperty, binding);
			ToolTipService.SetToolTip(_button, toolTip);
		}
		else
		{
			ToolTipService.SetToolTip(_button, _toolTipAttachedByUser);
		}
	}
}
