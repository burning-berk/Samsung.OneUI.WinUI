using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

[ContentProperty(Name = "Content")]
public class FloatingActionButton : Button
{
	private const string VISIBLE_STATE = "Visible";

	private const string COLLAPSED_STATE = "Collapsed";

	private const string ENABLED_STATE = "Enabled";

	private const string DISABLED_STATE = "Disabled";

	private const string ADD_ICON_STYLE = "AddIcon";

	private const string FLOATING_ICON = "FloatingActionIcon";

	private ContentControl contentControl;

	public new static readonly DependencyProperty VisibilityProperty = DependencyProperty.Register("Visibility", typeof(Visibility), typeof(FloatingActionButton), new PropertyMetadata(Visibility.Visible, OnVisibilityChanged));

	public static readonly DependencyProperty IsBlurProperty = DependencyProperty.Register("IsBlur", typeof(bool), typeof(FloatingActionButton), new PropertyMetadata(false));

	public new Visibility Visibility
	{
		get
		{
			return (Visibility)GetValue(VisibilityProperty);
		}
		set
		{
			SetValue(VisibilityProperty, value);
		}
	}

	public bool IsBlur
	{
		get
		{
			return (bool)GetValue(IsBlurProperty);
		}
		set
		{
			SetValue(IsBlurProperty, value);
		}
	}

	private static void OnVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is FloatingActionButton floatingActionButton)
		{
			if (floatingActionButton.Visibility == Visibility.Visible)
			{
				VisualStateManager.GoToState(floatingActionButton, "Visible", useTransitions: true);
			}
			else
			{
				VisualStateManager.GoToState(floatingActionButton, "Collapsed", useTransitions: true);
			}
		}
	}

	public FloatingActionButton()
	{
		RegisterPropertyChangedCallback(Control.IsEnabledProperty, OnIsEnabledPropertyChanged);
	}

	protected override void OnApplyTemplate()
	{
		contentControl = (ContentControl)GetTemplateChild("FloatingActionIcon");
		base.OnApplyTemplate();
	}

	private static void OnIsEnabledPropertyChanged(DependencyObject sender, DependencyProperty dp)
	{
		if (sender is FloatingActionButton floatingActionButton)
		{
			VisualStateManager.GoToState(floatingActionButton, floatingActionButton.IsEnabled ? "Enabled" : "Disabled", useTransitions: true);
		}
	}

	private void UpdateStyle()
	{
		contentControl.Style = ((contentControl.Content == null) ? "AddIcon".GetStyle() : null);
		UpdateLayout();
	}

	protected override void OnContentChanged(object oldContent, object newContent)
	{
		if (contentControl != null)
		{
			UpdateStyle();
		}
		base.OnContentChanged(oldContent, newContent);
	}
}
