using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Samsung.OneUI.WinUI.Utils.Extensions;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class ExpandButton : Button
{
	public const string COLLAPSING_ANIMATION = "RotateToCollapse";

	public const string EXPANDING_ANIMATION = "RotateToExpand";

	private const string TOOLTIP_NAME = "ToolTip";

	private const string EXPANDED_STRING_ID = "DREAM_EXPANDED_TBOPT/Text";

	private const string COLLAPSED_STRING_ID = "DREAM_COLLAPSED_TBOPT/Text";

	private const string BUTTON_STRING_ID = "SS_BUTTON_TTS/Text";

	private ToolTip _toolTip;

	private ExpandButtonAutomationPeer _expandButtonAutomationPeer;

	public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register("IsChecked", typeof(bool), typeof(ExpandButton), new PropertyMetadata(false, OnIsCheckedPropertyChanged));

	public bool IsChecked
	{
		get
		{
			return (bool)GetValue(IsCheckedProperty);
		}
		set
		{
			SetValue(IsCheckedProperty, value);
		}
	}

	private static void OnIsCheckedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		ExpandButton expandButton = d as ExpandButton;
		if ((object)expandButton != null)
		{
			UIExtensionsInternal.ExecuteWhenLoaded(expandButton, delegate
			{
				expandButton.UpdateState();
			});
		}
	}

	public ExpandButton()
	{
		base.Loaded += ExpandButton_Loaded;
		base.Click += ExpandButton_Click;
	}

	protected override void OnApplyTemplate()
	{
		_toolTip = GetTemplateChild("ToolTip") as ToolTip;
		UpdateToolTipContent();
	}

	protected override AutomationPeer OnCreateAutomationPeer()
	{
		if ((object)_expandButtonAutomationPeer == null)
		{
			_expandButtonAutomationPeer = new ExpandButtonAutomationPeer(this);
		}
		return _expandButtonAutomationPeer;
	}

	private void ExpandButton_Loaded(object sender, RoutedEventArgs e)
	{
		base.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, delegate
		{
			UpdateState();
		});
	}

	private void ExpandButton_Click(object sender, RoutedEventArgs e)
	{
		IsChecked = !IsChecked;
	}

	private void ExecuteAnimation()
	{
		string stateName = (IsChecked ? "RotateToExpand" : "RotateToCollapse");
		VisualStateManager.GoToState(this, stateName, useTransitions: true);
		CheckAnimationEnabled();
	}

	private void CheckAnimationEnabled()
	{
		Storyboard storyboard = GetTemplateChild("StoryRotateToCollapse") as Storyboard;
		Storyboard obj = GetTemplateChild("StoryRotateToExpand") as Storyboard;
		storyboard?.ValidateAnimationEnabled();
		obj?.ValidateAnimationEnabled();
	}

	private void UpdateToolTipContent()
	{
		if (!(_toolTip == null))
		{
			_toolTip.Content = GetLocalizedName();
			ToolTipService.SetToolTip(this, _toolTip);
		}
	}

	private void UpdateState()
	{
		ExecuteAnimation();
		UpdateToolTipContent();
	}

	internal string GetLocalizedName()
	{
		return (IsChecked ? "DREAM_EXPANDED_TBOPT/Text".GetLocalized() : "DREAM_COLLAPSED_TBOPT/Text".GetLocalized()) + " " + "SS_BUTTON_TTS/Text".GetLocalized();
	}

	public bool IsExpanded()
	{
		return IsChecked;
	}
}
