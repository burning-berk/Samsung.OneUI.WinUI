using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public sealed class ThumbnailRadious : Control
{
	private Border thumbnailRadiousBorder;

	private CheckBox thumbnailRadiousCheckbox;

	public const string THUMBNAILRADIOUS_BORDER = "ThumbnailRadiousBorder";

	private const string THUMBNAILRADIOUS_CHECKBOX = "ThumbnailRadiousCheckBox";

	public const double THUMBNAILRADIOUS_SMALL_SIZE = 100.0;

	public const double THUMBNAILRADIOUS_LARGE_SIZE = 136.0;

	public static readonly DependencyProperty ImageLocationProperty = DependencyProperty.Register("ImageLocation", typeof(string), typeof(ThumbnailRadious), new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(ThumbnailRadious), new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(ThumbnailRadious), new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty VisualizationModeProperty = DependencyProperty.Register("VisualizationMode", typeof(ThumbnailRadiousVisualizationMode), typeof(ThumbnailRadious), new PropertyMetadata(ThumbnailRadiousVisualizationMode.Large, VisualizationModeChangedCallback));

	public string ImageLocation
	{
		get
		{
			return (string)GetValue(ImageLocationProperty);
		}
		set
		{
			SetValue(ImageLocationProperty, value);
		}
	}

	public string Title
	{
		get
		{
			return (string)GetValue(TitleProperty);
		}
		set
		{
			SetValue(TitleProperty, value);
		}
	}

	public string Description
	{
		get
		{
			return (string)GetValue(DescriptionProperty);
		}
		set
		{
			SetValue(DescriptionProperty, value);
		}
	}

	public ThumbnailRadiousVisualizationMode VisualizationMode
	{
		get
		{
			return (ThumbnailRadiousVisualizationMode)GetValue(VisualizationModeProperty);
		}
		set
		{
			SetValue(VisualizationModeProperty, value);
		}
	}

	private static void VisualizationModeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		ThumbnailRadious thumbnailRadious = (ThumbnailRadious)d;
		if (thumbnailRadious != null)
		{
			thumbnailRadious.UpdateVisualizationMode();
		}
	}

	public ThumbnailRadious()
	{
		base.DefaultStyleKey = typeof(ThumbnailRadious);
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		thumbnailRadiousBorder = GetTemplateChild("ThumbnailRadiousBorder") as Border;
		GridViewItem visualParent = UIExtensionsInternal.GetVisualParent<GridViewItem>(this);
		if (visualParent != null)
		{
			thumbnailRadiousCheckbox = UIExtensionsInternal.FindChildByName<CheckBox>("ThumbnailRadiousCheckBox", visualParent);
		}
		UpdateVisualizationMode();
	}

	private void UpdateVisualizationMode()
	{
		if (!(thumbnailRadiousBorder == null) && !(thumbnailRadiousCheckbox == null))
		{
			if (ThumbnailRadiousVisualizationMode.Large.Equals(VisualizationMode))
			{
				ConfigureLargeVisualizationMode(thumbnailRadiousBorder);
				ConfigureLargeVisualizationMode(thumbnailRadiousCheckbox);
			}
			else
			{
				ConfigureSmallVisualizationMode(thumbnailRadiousBorder);
				ConfigureSmallVisualizationMode(thumbnailRadiousCheckbox);
			}
		}
	}

	private void ConfigureSmallVisualizationMode(FrameworkElement thumbnailRadious)
	{
		thumbnailRadious.Height = 100.0;
		thumbnailRadious.Width = 100.0;
	}

	private void ConfigureLargeVisualizationMode(FrameworkElement thumbnailRadious)
	{
		thumbnailRadious.Height = 136.0;
		thumbnailRadious.Width = 136.0;
	}
}
