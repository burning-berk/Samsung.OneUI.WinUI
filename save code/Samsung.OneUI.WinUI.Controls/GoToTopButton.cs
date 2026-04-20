using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Animation;
using Samsung.OneUI.WinUI.Utils.Extensions;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class GoToTopButton : Button
{
	private const string VISUAL_STATE_HIDE = "Hide";

	private const string VISUAL_STATE_SHOW = "Show";

	private const int VERTICAL_SCROLL_OFFSET_TO_SHOW_BUTTON_MIN_VALUE = 5;

	private const long VERTICAL_SCROLL_DISAPPEAR_TIME_IN_MILLISECONDS = 2000L;

	private const string STORYBOARD_SHOW = "StoryShow";

	private const string STORYBOARD_HIDE = "StoryHide";

	private bool _show;

	private bool _isPointerOver;

	private ScrollViewer _scroll;

	private long _lastTimeUserHoverOrScrollTimestamp;

	private readonly DateTime _dateTimeBeginToCalculateTimestamp;

	private GoToTopButtonAutomationPeer _goToTopButtonAutomationPeer;

	public static readonly DependencyProperty IsBlurProperty = DependencyProperty.Register("IsBlur", typeof(bool), typeof(GoToTopButton), new PropertyMetadata(false));

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

	protected override AutomationPeer OnCreateAutomationPeer()
	{
		if (_goToTopButtonAutomationPeer == null)
		{
			_goToTopButtonAutomationPeer = new GoToTopButtonAutomationPeer(this);
		}
		return _goToTopButtonAutomationPeer;
	}

	public GoToTopButton()
	{
		base.IsEnabled = false;
		base.Loaded += GoToTopButton_Loaded;
		base.Click += GoToTopButton_Click;
		_dateTimeBeginToCalculateTimestamp = DateTime.UtcNow;
	}

	private void UpdateShowVisualState()
	{
		string stateName = (_show ? "Show" : "Hide");
		VisualStateManager.GoToState(this, stateName, useTransitions: true);
		base.IsEnabled = _show;
		ValidateAnimationEnabled();
	}

	private void ValidateAnimationEnabled()
	{
		Storyboard storyboard = GetTemplateChild("StoryShow") as Storyboard;
		Storyboard storyboard2 = GetTemplateChild("StoryHide") as Storyboard;
		storyboard.ValidateAnimationEnabled();
		storyboard2.ValidateAnimationEnabled();
	}

	private void GoToTopButton_Loaded(object sender, RoutedEventArgs e)
	{
		UpdateShowVisualState();
		SetScrollViewerViewChangedBehavior();
		SetVerticalScrollBarHoverBehavior();
	}

	private void SetScrollViewerViewChangedBehavior()
	{
		if (base.Parent is Panel)
		{
			_scroll = base.Parent.GetScrollViewer();
			if (_scroll != null)
			{
				_scroll.ViewChanged += ScrollViewer_ViewChanged;
			}
		}
	}

	private void SetVerticalScrollBarHoverBehavior()
	{
		if (!(_scroll == null))
		{
			Grid grid = UIExtensionsInternal.FindChildByName<Grid>("VerticalRoot", _scroll);
			if (grid != null)
			{
				grid.PointerExited -= VerticalScrollBar_PointerExited;
				grid.PointerExited += VerticalScrollBar_PointerExited;
				grid.PointerEntered -= VerticalScrollBar_PointerEntered;
				grid.PointerEntered += VerticalScrollBar_PointerEntered;
				base.PointerEntered += GoToTopButton_PointerEntered;
				base.PointerExited += GoToTopButton_PointerExited;
			}
		}
	}

	private void GoToTopButton_PointerExited(object sender, PointerRoutedEventArgs e)
	{
		SetPointerStateToFalse();
	}

	private void GoToTopButton_PointerEntered(object sender, PointerRoutedEventArgs e)
	{
		_isPointerOver = true;
	}

	private void VerticalScrollBar_PointerExited(object sender, PointerRoutedEventArgs e)
	{
		SetPointerStateToFalse();
	}

	private void VerticalScrollBar_PointerEntered(object sender, PointerRoutedEventArgs e)
	{
		_isPointerOver = true;
	}

	private void SetPointerStateToFalse()
	{
		_lastTimeUserHoverOrScrollTimestamp = GetCurrentTimestamp();
		_isPointerOver = false;
	}

	private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
	{
		if (!e.IsIntermediate)
		{
			ManageGoToTopVisibility();
		}
	}

	private void ManageGoToTopVisibility()
	{
		bool flag = HasToShowGoToTopButton();
		if (flag)
		{
			_lastTimeUserHoverOrScrollTimestamp = GetCurrentTimestamp();
			if (!_show)
			{
				CreateAndStartDisappearButtonThread();
			}
		}
		_show = flag;
		UpdateShowVisualState();
	}

	private void CreateAndStartDisappearButtonThread()
	{
		new Thread((ThreadStart)async delegate
		{
			await base.DispatcherQueue.EnqueueAsync(async delegate
			{
				while (_show && (GetCurrentTimestamp() < _lastTimeUserHoverOrScrollTimestamp + 2000 || _isPointerOver))
				{
					long num = _lastTimeUserHoverOrScrollTimestamp + 2000 - GetCurrentTimestamp();
					await Task.Delay((int)((num > 0) ? num : 2000));
				}
				_show = false;
				UpdateShowVisualState();
			});
		}).Start();
	}

	private bool HasToShowGoToTopButton()
	{
		ScrollViewer scroll = _scroll;
		if ((object)scroll != null && scroll.ComputedVerticalScrollBarVisibility == Visibility.Visible)
		{
			ScrollViewer scroll2 = _scroll;
			if ((object)scroll2 == null)
			{
				return false;
			}
			return scroll2.VerticalOffset > 5.0;
		}
		return false;
	}

	private long GetCurrentTimestamp()
	{
		return (long)DateTime.UtcNow.Subtract(_dateTimeBeginToCalculateTimestamp).TotalMilliseconds;
	}

	private void GoToTopButton_Click(object sender, RoutedEventArgs e)
	{
		_scroll?.ChangeView(null, 0.0, null);
	}
}
