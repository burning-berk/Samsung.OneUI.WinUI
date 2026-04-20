using System;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Samsung.OneUI.WinUI.Utils.Extensions;
using Windows.UI.ViewManagement;

namespace Samsung.OneUI.WinUI.Controls;

internal class ScrollListAnimationService
{
	private const string COMPLETED_SCROLLER_STATUS = "CompletedScroller";

	private const string SCROLLING_STATUS = "Scrolling";

	private const string TRANSFORM_TARGET_PROPERTY_Y = "Y";

	private const double AMPLITUDE_ANIMATION = 0.45;

	private const int INDEX_PREVIOUS_ITEM_SELECTED_BEFORE_ENTRANCE_ANIMATION = 4;

	private const int ONE_OPACITY = 1;

	private const int ZERO_OPACITY = 0;

	internal const double DEFAULT_FROM_TO_VALUE = 0.0;

	private const int INVALID_INDEX = -1;

	private bool _isStartedScrollerAnimation;

	private readonly ScrollList _scrollList;

	private readonly AccessibilitySettings _accessibilitySettings;

	public ScrollListAnimationService(ScrollList scrollList)
	{
		_scrollList = scrollList;
		_accessibilitySettings = new AccessibilitySettings();
	}

	internal virtual void ConfigureListViewEntranceAnimation()
	{
		if (_scrollList._listViewEntranceAnimation != null)
		{
			PrepareEntranceAnimation();
		}
	}

	internal virtual void ConfigureFromAndToEntranceAnimation(double verticalOffSet, out double from, out double to)
	{
		from = 0.0;
		to = 0.0 - verticalOffSet;
	}

	internal void EntranceAnimation(double verticalOffSet, TimeSpan duration, Action onCompleted)
	{
		if (_scrollList._listViewEntranceAnimation != null)
		{
			_scrollList.IsHitTestVisible = false;
			SelectItemEntranceAnimation();
			StartScrollAnimation();
			ConfigureFromAndToEntranceAnimation(verticalOffSet, out var from, out var to);
			StartSpringAnimation(_scrollList._listViewEntranceAnimation, from, to, duration, onCompleted);
		}
	}

	internal void StopEntranceAnimation()
	{
		StopScrollAnimation();
		if (_scrollList._listView != null)
		{
			_scrollList._listView.Opacity = 1.0;
		}
		if (_scrollList._listViewEntranceAnimation != null)
		{
			_scrollList._listViewEntranceAnimation.Visibility = Visibility.Collapsed;
			TranslateTransform renderTransform = new TranslateTransform();
			_scrollList._listViewEntranceAnimation.RenderTransform = renderTransform;
		}
		_scrollList.IsHitTestVisible = true;
	}

	internal ObservableCollection<object> CloneObservableCollection(ObservableCollection<object> originalCollection)
	{
		ObservableCollection<object> observableCollection = new ObservableCollection<object>();
		foreach (object item in originalCollection)
		{
			if (item is ICloneable cloneable)
			{
				observableCollection.Add(cloneable.Clone());
			}
		}
		return observableCollection;
	}

	internal void StartScrollAnimation()
	{
		if (!_accessibilitySettings.HighContrast && !IsStartedScrollAnimation())
		{
			VisualStateManager.GoToState(_scrollList, "Scrolling", useTransitions: true);
			_isStartedScrollerAnimation = true;
		}
	}

	internal void StopScrollAnimation()
	{
		if (!_accessibilitySettings.HighContrast && IsStartedScrollAnimation())
		{
			VisualStateManager.GoToState(_scrollList, "CompletedScroller", useTransitions: false);
			_isStartedScrollerAnimation = false;
		}
	}

	internal void PrepareEntranceAnimation()
	{
		if (_scrollList._listViewEntranceAnimation != null && _scrollList._listView != null)
		{
			ConfigureParametersDefaultEntranceAnimation();
			ObservableCollection<object> collection = CloneObservableCollection(_scrollList.TimeItemsSource);
			int indexPreviousItem = 4;
			object obj = FindElementPrevious(collection, _scrollList.SelectedTime, indexPreviousItem);
			_scrollList._listViewEntranceAnimation.ItemsSource = _scrollList.UpdateListViewItemPositionFirstTime(collection, obj);
			_scrollList._listViewEntranceAnimation.SelectedItem = obj;
		}
	}

	internal void ConfigureParametersDefaultEntranceAnimation()
	{
		if (_scrollList._listViewEntranceAnimation != null && _scrollList._listView != null)
		{
			_scrollList._listView.Opacity = 0.0;
			_scrollList._listViewEntranceAnimation.Visibility = Visibility.Visible;
		}
	}

	private object FindElementPrevious(ObservableCollection<object> collection, object element, int indexPreviousItem)
	{
		int num = collection.IndexOf(element);
		if (num < 0)
		{
			return null;
		}
		int num2 = (num - indexPreviousItem) % collection.Count;
		if (num2 < 0)
		{
			num2 += collection.Count;
		}
		return collection[num2];
	}

	private bool IsStartedScrollAnimation()
	{
		return _isStartedScrollerAnimation;
	}

	private void SelectItemEntranceAnimation()
	{
		if (_scrollList._listViewEntranceAnimation != null && _scrollList._listViewEntranceAnimation.ItemsSource != null && _scrollList._listViewEntranceAnimation.ItemsSource is ObservableCollection<object> observableCollection)
		{
			int num = observableCollection.IndexOf(_scrollList.SelectedTime);
			if (num != -1)
			{
				_scrollList._listViewEntranceAnimation.SelectedItem = observableCollection[num];
			}
		}
	}

	private void StartSpringAnimation(UIElement content, double from, double to, TimeSpan duration, Action onCompleted)
	{
		Storyboard storyboard = new Storyboard();
		BackEase easingFunction = new BackEase
		{
			Amplitude = 0.45
		};
		TranslateTransform target = (TranslateTransform)(content.RenderTransform = new TranslateTransform());
		DoubleAnimation doubleAnimation = new DoubleAnimation
		{
			From = from,
			To = to,
			Duration = duration,
			EasingFunction = easingFunction
		};
		Storyboard.SetTarget(doubleAnimation, target);
		Storyboard.SetTargetProperty(doubleAnimation, "Y");
		storyboard.Children.Add(doubleAnimation);
		storyboard.Completed += delegate
		{
			_scrollList.OnEntranceAnimationCompleted();
			onCompleted?.Invoke();
		};
		storyboard.SafeBegin();
	}
}
