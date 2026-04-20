using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Shapes;
using Samsung.OneUI.WinUI.Commom.Interfaces;
using Samsung.OneUI.WinUI.Controls.Inputs.CheckBox;
using Samsung.OneUI.WinUI.Controls.Selectors;
using Samsung.OneUI.WinUI.Utils.Extensions;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class CheckBox : Microsoft.UI.Xaml.Controls.CheckBox, IDialogComponentNegativeOutStroke
{
	private const string RECTANGLE_IMAGE = "PART_RECTANGLE";

	private const string CONTENT_PRESENTER_NAME = "PART_ContentPresenter";

	private const string CHECKED_DISABLED = "CheckedDisabled";

	private const string DISABLED = "Disabled";

	private const string CHECKED_NORMAL = "CheckedNormal";

	private const string NORMAL = "Normal";

	private const string CHECK_PRESSED = "CheckedPressed";

	private const string PRESSED = "Pressed";

	private const string CHECKED_POINTOVER = "CheckedPointOver";

	private const string POINTOVER = "PointOver";

	private const string CHECKBOX_CHECKED_VIEWSTATE = "Checked";

	private const string CHECKBOX_UNCHECKED_VIEWSTATE = "Unchecked";

	private const string FOCUS_MARGIN_OUT_STROKE_RESOURCE_KEY = "OneUICheckBoxOutStrokeMargin";

	private const string FOCUS_WITH_TEXT = "FocusedWithText";

	private const string FOCUS_WITHOUT_TEXT = "FocusedWithoutText";

	private const string STORYBOARD_CHECKED = "CheckedStoryboard";

	private Rectangle rectangle;

	private ContentPresenter contentPresenter;

	public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(IconElement), typeof(CheckBox), new PropertyMetadata(null));

	public static readonly DependencyProperty IconSvgStyleProperty = DependencyProperty.Register("IconSvgStyle", typeof(Style), typeof(CheckBox), new PropertyMetadata(null));

	public static readonly DependencyProperty UriProperty = DependencyProperty.Register("Uri", typeof(string), typeof(CheckBox), new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(CheckBoxType), typeof(CheckBoxType), new PropertyMetadata(CheckBoxType.Default, TypeChanged));

	public IconElement Icon
	{
		get
		{
			return (IconElement)GetValue(IconProperty);
		}
		set
		{
			SetValue(IconProperty, value);
		}
	}

	public Style IconSvgStyle
	{
		get
		{
			return (Style)GetValue(IconSvgStyleProperty);
		}
		set
		{
			SetValue(IconSvgStyleProperty, value);
		}
	}

	public string Uri
	{
		get
		{
			return (string)GetValue(UriProperty);
		}
		set
		{
			SetValue(UriProperty, value);
		}
	}

	public CheckBoxType Type
	{
		get
		{
			return (CheckBoxType)GetValue(TypeProperty);
		}
		set
		{
			SetValue(TypeProperty, value);
		}
	}

	public CheckBox()
	{
		base.Style = new CheckBoxStyleSelector(Type).SelectStyle();
		base.Loaded += CheckBox_Loaded;
	}

	protected override void OnApplyTemplate()
	{
		rectangle = (Rectangle)GetTemplateChild("PART_RECTANGLE");
		contentPresenter = (ContentPresenter)GetTemplateChild("PART_ContentPresenter");
		CheckContentMargin();
		base.OnApplyTemplate();
	}

	protected override void OnContentChanged(object oldContent, object newContent)
	{
		if (!(contentPresenter == null))
		{
			AdjustMarginVisibility(newContent);
			base.OnContentChanged(oldContent, newContent);
		}
	}

	private void CheckBox_GettingFocus(UIElement sender, GettingFocusEventArgs args)
	{
		if (sender is CheckBox control)
		{
			string stateName = "FocusedWithoutText";
			if (base.Content != null)
			{
				stateName = ((base.Content is string value && string.IsNullOrEmpty(value)) ? "FocusedWithoutText" : "FocusedWithText");
			}
			VisualStateManager.GoToState(control, stateName, useTransitions: true);
		}
	}

	private void CheckBox_Checked(object sender, RoutedEventArgs e)
	{
		if (sender is CheckBox { IsChecked: var isChecked } checkBox)
		{
			string stateName = (isChecked.Value ? "Checked" : "Unchecked");
			VisualStateManager.GoToState(checkBox, stateName, useTransitions: true);
			(GetTemplateChild("CheckedStoryboard") as Storyboard)?.ValidateAnimationEnabled();
		}
	}

	private void CheckBox_Unloaded(object sender, RoutedEventArgs e)
	{
		UnregisterEvents();
	}

	private void CheckBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		CheckBox checkBox = sender as CheckBox;
		if (checkBox.IsChecked == true && !checkBox.IsEnabled)
		{
			VisualStateManager.GoToState((CheckBox)sender, "CheckedDisabled", useTransitions: true);
		}
		else if (checkBox.IsChecked == true && checkBox.IsEnabled)
		{
			VisualStateManager.GoToState((CheckBox)sender, "CheckedNormal", useTransitions: true);
		}
		else if (checkBox.IsChecked == false && !checkBox.IsEnabled)
		{
			VisualStateManager.GoToState((CheckBox)sender, "Disabled", useTransitions: true);
		}
		else if (checkBox.IsChecked == false && checkBox.IsEnabled)
		{
			VisualStateManager.GoToState((CheckBox)sender, "Normal", useTransitions: true);
		}
	}

	private void CheckBox_PointerPressed(object sender, PointerRoutedEventArgs e)
	{
		CheckBox checkBox = sender as CheckBox;
		if (checkBox.IsChecked == true)
		{
			VisualStateManager.GoToState((CheckBox)sender, "CheckedPressed", useTransitions: true);
		}
		else if (checkBox.IsChecked == false)
		{
			VisualStateManager.GoToState((CheckBox)sender, "Pressed", useTransitions: true);
		}
	}

	private void CheckBox_PointerEntered(object sender, PointerRoutedEventArgs e)
	{
		CheckBox checkBox = sender as CheckBox;
		if (checkBox.IsChecked == true)
		{
			VisualStateManager.GoToState((CheckBox)sender, "CheckedPointOver", useTransitions: true);
		}
		else if (checkBox.IsChecked == false)
		{
			VisualStateManager.GoToState((CheckBox)sender, "PointOver", useTransitions: true);
		}
	}

	private void CheckBox_Loaded(object sender, RoutedEventArgs e)
	{
		RegisterEvents();
		if (rectangle != null && IsThumbnailCompliant())
		{
			BitmapImage bitmapImage = new BitmapImage();
			bitmapImage.UriSource = new Uri(Uri);
			ImageBrush imageBrush = new ImageBrush();
			imageBrush.ImageSource = bitmapImage;
			rectangle.Fill = imageBrush;
		}
		CheckContentMargin();
	}

	public Thickness GetFocusVisualMargin()
	{
		object key = "OneUICheckBoxOutStrokeMargin".GetKey();
		if (key is Thickness)
		{
			return (Thickness)key;
		}
		return default(Thickness);
	}

	private void RegisterEvents()
	{
		base.PointerEntered += CheckBox_PointerEntered;
		base.PointerPressed += CheckBox_PointerPressed;
		base.IsEnabledChanged += CheckBox_IsEnabledChanged;
		base.Unloaded += CheckBox_Unloaded;
		base.Checked += CheckBox_Checked;
		base.GettingFocus += CheckBox_GettingFocus;
	}

	private void UnregisterEvents()
	{
		base.PointerEntered -= CheckBox_PointerEntered;
		base.PointerPressed -= CheckBox_PointerPressed;
		base.IsEnabledChanged -= CheckBox_IsEnabledChanged;
		base.Loaded -= CheckBox_Loaded;
		base.Checked -= CheckBox_Checked;
		base.GettingFocus -= CheckBox_GettingFocus;
	}

	private void CheckContentMargin()
	{
		if (!(contentPresenter == null) && string.IsNullOrEmpty(contentPresenter.Content?.ToString()))
		{
			AdjustMarginVisibility(contentPresenter.Content);
		}
	}

	private bool IsThumbnailCompliant()
	{
		if (Type == CheckBoxType.Thumbnail)
		{
			return Uri != string.Empty;
		}
		return false;
	}

	private void AdjustMarginVisibility(object content)
	{
		if (string.IsNullOrEmpty(content?.ToString()))
		{
			contentPresenter.Visibility = Visibility.Collapsed;
		}
		else
		{
			contentPresenter.Visibility = Visibility.Visible;
		}
	}

	private static void TypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is CheckBox checkBox)
		{
			checkBox.Style = new CheckBoxStyleSelector(checkBox.Type).SelectStyle();
		}
	}
}
