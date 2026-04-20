using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Samsung.OneUI.WinUI.Commom.Interfaces;
using Samsung.OneUI.WinUI.Utils.Extensions;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class RadioButton : Microsoft.UI.Xaml.Controls.RadioButton, IDialogComponentNegativeOutStroke
{
	private const string TEXT_CONTENTPRESENT = "ContentPresenter";

	private const string FOCUS_MARGIN_OUT_STROKE_RESOURCE_KEY = "OneUIRadioButtonOutStrokeMargin";

	private const string CHECKED_STATE = "Checked";

	private const string UNCHECKED_STATE = "Unchecked";

	private const string STORYBOARD_UNCHECKED = "StoryboardUnChecked";

	private const string STORYBOARD_CHECKED = "StoryboardChecked";

	private ContentPresenter _content;

	public RadioButton()
	{
		base.Loaded += RadioButton_Loaded;
		base.Checked += RadioButton_Checked;
		base.Unchecked += RadioButton_Unchecked;
	}

	public Thickness GetFocusVisualMargin()
	{
		object key = "OneUIRadioButtonOutStrokeMargin".GetKey();
		if (key is Thickness)
		{
			return (Thickness)key;
		}
		return default(Thickness);
	}

	private void AdjustVisibility(object newContent)
	{
		if (_content != null)
		{
			if (newContent == null)
			{
				_content.Visibility = Visibility.Collapsed;
			}
			else if (newContent is string value && string.IsNullOrEmpty(value))
			{
				_content.Visibility = Visibility.Collapsed;
			}
			else
			{
				_content.Visibility = Visibility.Visible;
			}
		}
	}

	private void SetVisualState()
	{
		if (base.IsChecked == true)
		{
			VisualStateManager.GoToState(this, "Checked", useTransitions: false);
		}
		else
		{
			VisualStateManager.GoToState(this, "Unchecked", useTransitions: false);
		}
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		SetVisualState();
		_content = (ContentPresenter)GetTemplateChild("ContentPresenter");
	}

	protected override void OnContentChanged(object oldContent, object newContent)
	{
		if (!(_content == null))
		{
			AdjustVisibility(newContent);
			base.OnContentChanged(oldContent, newContent);
		}
	}

	private void RadioButton_Loaded(object sender, RoutedEventArgs e)
	{
		AdjustVisibility(_content?.Content);
	}

	private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
	{
		(GetTemplateChild("StoryboardUnChecked") as Storyboard)?.ValidateAnimationEnabled();
	}

	private void RadioButton_Checked(object sender, RoutedEventArgs e)
	{
		(GetTemplateChild("StoryboardChecked") as Storyboard)?.ValidateAnimationEnabled();
	}
}
