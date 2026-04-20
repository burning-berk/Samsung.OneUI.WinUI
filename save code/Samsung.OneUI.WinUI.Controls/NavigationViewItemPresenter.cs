using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Samsung.OneUI.WinUI.AttachedProperties;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public sealed class NavigationViewItemPresenter : Microsoft.UI.Xaml.Controls.Primitives.NavigationViewItemPresenter
{
	private const int ICON_COLLAPSED_WIDTH = 8;

	private const string PNG_EXTENSION = ".png";

	private const string ICON_BOX_ELEMENT_NAME = "IconBox";

	private const string ICON_COLUMN_ELEMENT_NAME = "IconColumn";

	private const string SVG_ICON_ELEMENT_NAME = "SVGIcon";

	private const string DEFAULT_ICON_ELEMENT_NAME = "Icon";

	private const string CONTENT_PRESENTER = "ContentPresenter";

	private const string LAYOUT_ROOT = "LayoutRoot";

	private Viewbox _iconBox;

	private Border _iconColumn;

	private ContentControl _svgContentControl;

	private ContentPresenter _iconContentPresenter;

	private ContentPresenter _contentPresenter;

	private Grid _layoutRoot;

	public static readonly DependencyProperty SvgIconStyleProperty = DependencyProperty.Register("SvgIconStyle", typeof(Style), typeof(NavigationViewItemPresenter), new PropertyMetadata(null, SvgPngChanged));

	public static readonly DependencyProperty PngIconPathProperty = DependencyProperty.Register("PngIconPath", typeof(string), typeof(NavigationViewItemPresenter), new PropertyMetadata(null, SvgPngChanged));

	public static readonly DependencyProperty NotificationBadgeProperty = DependencyProperty.Register("NotificationBadge", typeof(BadgeBase), typeof(NavigationViewItemPresenter), new PropertyMetadata(null));

	public Style SvgIconStyle
	{
		get
		{
			return (Style)GetValue(SvgIconStyleProperty);
		}
		set
		{
			SetValue(SvgIconStyleProperty, value);
		}
	}

	public string PngIconPath
	{
		get
		{
			return (string)GetValue(PngIconPathProperty);
		}
		set
		{
			SetValue(PngIconPathProperty, value);
		}
	}

	public BadgeBase NotificationBadge
	{
		get
		{
			return (BadgeBase)GetValue(NotificationBadgeProperty);
		}
		set
		{
			SetValue(NotificationBadgeProperty, value);
		}
	}

	protected override void OnApplyTemplate()
	{
		_iconBox = (Viewbox)GetTemplateChild("IconBox");
		_iconColumn = (Border)GetTemplateChild("IconColumn");
		_svgContentControl = (ContentControl)GetTemplateChild("SVGIcon");
		_iconContentPresenter = (ContentPresenter)GetTemplateChild("Icon");
		_contentPresenter = GetTemplateChild("ContentPresenter") as ContentPresenter;
		_layoutRoot = GetTemplateChild("LayoutRoot") as Grid;
		UpdateIconElements();
		if (_contentPresenter != null)
		{
			_contentPresenter.Loaded += ContentPresenter_Loaded;
		}
		base.OnApplyTemplate();
	}

	private static void SvgPngChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is NavigationViewItemPresenter navigationViewItemPresenter)
		{
			navigationViewItemPresenter.UpdateIconElements();
		}
	}

	private void UpdateIconElements()
	{
		if (PngIconPath != null)
		{
			if (PngIconPath.ToLowerInvariant().EndsWith(".png"))
			{
				base.Icon = new BitmapIcon
				{
					UriSource = new Uri(PngIconPath, UriKind.RelativeOrAbsolute),
					ShowAsMonochrome = false
				};
			}
			else
			{
				base.Icon = new FontIcon
				{
					Glyph = PngIconPath
				};
			}
		}
		UpdateSVGAndIconVisibility();
	}

	private void UpdateSVGAndIconVisibility()
	{
		if (!(_svgContentControl == null) || !(_iconContentPresenter == null))
		{
			SetSVGAndIconVisibility();
		}
	}

	private void SetSVGAndIconVisibility()
	{
		if (base.Icon == null && SvgIconStyle == null)
		{
			_iconColumn.Width = 8.0;
			_iconBox.Visibility = Visibility.Collapsed;
		}
		else if (!(_svgContentControl == null) && !(_iconContentPresenter == null))
		{
			if (SvgIconStyle != null)
			{
				_svgContentControl.Visibility = Visibility.Visible;
				_iconContentPresenter.Visibility = Visibility.Collapsed;
			}
			else
			{
				_svgContentControl.Visibility = Visibility.Collapsed;
				_iconContentPresenter.Visibility = Visibility.Visible;
			}
			_iconBox.Visibility = Visibility.Visible;
		}
	}

	internal void SetOpacity(double opacity)
	{
		_contentPresenter.Opacity = opacity;
	}

	internal ContentPresenter GetContentPresenter()
	{
		return _contentPresenter;
	}

	internal Grid GetLayoutRoot()
	{
		return _layoutRoot;
	}

	private void ContentPresenter_Loaded(object sender, RoutedEventArgs e)
	{
		if (!(_contentPresenter == null))
		{
			TextBlock textBlock = UIExtensionsInternal.FindFirstChildOfType<TextBlock>(_contentPresenter);
			if ((object)textBlock != null)
			{
				textBlock.TextTrimming = TextTrimming.CharacterEllipsis;
				Tooltip.SetTextTrimmedEnabled(textBlock, value: true);
			}
		}
	}
}
