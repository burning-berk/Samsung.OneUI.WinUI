using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Services.Animation;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.Foundation;
using Windows.UI.ViewManagement;

namespace Samsung.OneUI.WinUI.Controls;

public class PopOver : Flyout
{
	private const string PART_STYLE_PRESENTER = "OneUIPopOverPresenter";

	private readonly IPopOverAnimationService _popOverAnimation;

	private bool _isPopOverClosing;

	private readonly AccessibilitySettings _accessibilitySettings;

	public static readonly DependencyProperty HorizontalOffsetProperty = DependencyProperty.Register("HorizontalOffset", typeof(double), typeof(PopOver), new PropertyMetadata(0.0));

	public static readonly DependencyProperty VerticalOffsetProperty = DependencyProperty.Register("VerticalOffset", typeof(double), typeof(PopOver), new PropertyMetadata(0.0));

	public static readonly DependencyProperty CloseOnFocusLostProperty = DependencyProperty.Register("CloseOnFocusLost", typeof(bool), typeof(PopOver), new PropertyMetadata(true));

	public double HorizontalOffset
	{
		get
		{
			return (double)GetValue(HorizontalOffsetProperty);
		}
		set
		{
			SetValue(HorizontalOffsetProperty, value);
		}
	}

	public double VerticalOffset
	{
		get
		{
			return (double)GetValue(VerticalOffsetProperty);
		}
		set
		{
			SetValue(VerticalOffsetProperty, value);
		}
	}

	public bool CloseOnFocusLost
	{
		get
		{
			return (bool)GetValue(CloseOnFocusLostProperty);
		}
		set
		{
			SetValue(CloseOnFocusLostProperty, value);
		}
	}

	public PopOver()
	{
		base.Placement = FlyoutPlacementMode.Bottom;
		base.Opening += PopOver_Opening;
		base.Opened += PopOver_Opened;
		base.Closing += PopOver_Closing;
		base.AreOpenCloseAnimationsEnabled = false;
		base.LightDismissOverlayMode = LightDismissOverlayMode.Off;
		_popOverAnimation = new PopOverAnimationService();
		_accessibilitySettings = new AccessibilitySettings();
	}

	private void PopOver_Opened(object sender, object e)
	{
		Popup popUp = GetPopUp();
		if ((object)popUp != null)
		{
			SetPopupSidesOffSet(popUp);
			if (popUp.Child is FlyoutPresenter flyoutPresenter)
			{
				flyoutPresenter.Focus(FocusState.Programmatic);
				_popOverAnimation.OpenAnimation(flyoutPresenter, flyoutPresenter.DesiredSize.Width / 2.0, flyoutPresenter.DesiredSize.Height / 2.0);
			}
		}
	}

	private void PopOver_Closing(FlyoutBase sender, FlyoutBaseClosingEventArgs args)
	{
		args.Cancel = true;
		if (!CloseOnFocusLost)
		{
			return;
		}
		Popup popup = GetPopUp();
		if (popup != null && !_isPopOverClosing && popup.Child is FlyoutPresenter menuFlyoutPresenter)
		{
			_isPopOverClosing = true;
			_popOverAnimation.CloseAnimation(menuFlyoutPresenter, delegate
			{
				popup.IsOpen = false;
				_isPopOverClosing = false;
			});
		}
	}

	private void PopOver_Opening(object sender, object e)
	{
		if (sender is PopOver popOver)
		{
			if ((object)popOver.Content != null)
			{
				popOver.FlyoutPresenterStyle = "OneUIPopOverPresenter".GetStyle();
			}
			else
			{
				Hide();
			}
		}
	}

	private void SetPopupSidesOffSet(Popup popup)
	{
		if (!(popup == null) && !(base.Target == null) && !(base.Target.XamlRoot == null))
		{
			Point point = base.Target.TransformToVisual(base.Target.XamlRoot.Content).TransformPoint(new Point(0f, 0f));
			if (HorizontalOffset != 0.0)
			{
				popup.HorizontalOffset = point.X + HorizontalOffset;
			}
			popup.VerticalOffset = point.Y + VerticalOffset;
		}
	}

	private Popup GetPopUp()
	{
		foreach (Popup item in VisualTreeHelper.GetOpenPopupsForXamlRoot(base.XamlRoot))
		{
			if (item?.Child is FlyoutPresenter { Content: not null } flyoutPresenter && base.Content != null && flyoutPresenter.Content.GetHashCode() == base.Content.GetHashCode())
			{
				return item;
			}
		}
		return null;
	}
}
