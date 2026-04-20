using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Behaviors;

public class TooltipForTrimmedTextBlockBehavior : Behavior<FrameworkElement>
{
	private ToolTip _toolTip;

	private TextBlock _textBlock;

	public static readonly DependencyProperty TextBlockNameProperty = DependencyProperty.Register("TextBlockName", typeof(string), typeof(TooltipForTrimmedTextBlockBehavior), new PropertyMetadata(null));

	public string TextBlockName
	{
		get
		{
			return (string)GetValue(TextBlockNameProperty);
		}
		set
		{
			SetValue(TextBlockNameProperty, value);
		}
	}

	protected override void OnAttached()
	{
		if (!(base.AssociatedObject == null))
		{
			base.AssociatedObject.Loaded += OnAssociatedObjectLoaded;
			OnAssociatedObjectUnloaded();
		}
	}

	protected override void OnDetaching()
	{
		if (!(base.AssociatedObject == null))
		{
			base.AssociatedObject.Loaded -= OnAssociatedObjectLoaded;
			UpdateToolTipVisibility(isOnAttached: false);
		}
	}

	private void OnAssociatedObjectLoaded(object sender, RoutedEventArgs e)
	{
		_toolTip = ToolTipService.GetToolTip(base.AssociatedObject) as ToolTip;
		if (!(_toolTip == null))
		{
			_textBlock = GetTextBlock(base.AssociatedObject);
			if (!(_textBlock == null))
			{
				UpdateToolTipVisibility(isOnAttached: true);
			}
		}
	}

	private void OnAssociatedObjectUnloaded()
	{
		RoutedEventHandler onUnloaded = null;
		onUnloaded = delegate
		{
			if (!(base.AssociatedObject == null))
			{
				base.AssociatedObject.Unloaded -= onUnloaded;
				_toolTip = null;
				_textBlock = null;
			}
		};
		base.AssociatedObject.Unloaded += onUnloaded;
	}

	private void UpdateToolTipVisibility(bool isOnAttached)
	{
		if (!(_toolTip == null) && !(_textBlock == null))
		{
			if (isOnAttached)
			{
				_toolTip.Visibility = ((!_textBlock.IsTextTrimmed) ? Visibility.Collapsed : Visibility.Visible);
				_textBlock.IsTextTrimmedChanged += OnTextTrimmedChanged;
			}
			else
			{
				_toolTip.Visibility = Visibility.Collapsed;
				_textBlock.IsTextTrimmedChanged -= OnTextTrimmedChanged;
			}
		}
	}

	private void OnTextTrimmedChanged(object sender, IsTextTrimmedChangedEventArgs e)
	{
		if (!(_toolTip == null) && !(_textBlock == null))
		{
			_toolTip.Visibility = ((!_textBlock.IsTextTrimmed) ? Visibility.Collapsed : Visibility.Visible);
		}
	}

	private TextBlock GetTextBlock(object sender)
	{
		if (sender is TextBlock)
		{
			return (TextBlock)sender;
		}
		return UIExtensionsInternal.FindChildByName<TextBlock>(TextBlockName, (FrameworkElement)sender);
	}
}
