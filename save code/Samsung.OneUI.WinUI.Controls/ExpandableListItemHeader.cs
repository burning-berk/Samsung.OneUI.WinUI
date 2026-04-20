using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Samsung.OneUI.WinUI.Utils.Extensions;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.System;

namespace Samsung.OneUI.WinUI.Controls;

public class ExpandableListItemHeader : TreeViewItem
{
	private const string PART_ICON = "PART_Icon";

	private const string BORDER_OUTSTROKE = "ExpandableListItemHeaderBorder";

	private const string POINTEROVER_STATE = "PointerOver";

	private const string EXPANDED_STATE = "Expanded";

	private const string NORMAL_STATE = "Normal";

	private ExpandButton _expandToggleButton;

	private bool _isClickFromExpanderButton;

	private readonly long _isExpandedChangedToken;

	private bool _isRegisterEvents;

	public ExpandableListItemHeader()
	{
		_isExpandedChangedToken = RegisterPropertyChangedCallback(TreeViewItem.IsExpandedProperty, OnIsExpandedChanged);
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		base.Loaded += ExpandableListItemHeader_Loaded;
		RegisterEvents();
	}

	private void OnExpandButtonPointerExited(object sender, PointerRoutedEventArgs e)
	{
		VisualStateManager.GoToState(this, "PointerOver", useTransitions: true);
	}

	private void OnExpandButtonPointerEntered(object sender, PointerRoutedEventArgs e)
	{
		VisualStateManager.GoToState(this, "Normal", useTransitions: true);
	}

	private void ExpandableListItemHeader_Loaded(object sender, RoutedEventArgs e)
	{
		if (sender is ExpandableListItemHeader item)
		{
			UpdateExpandableListItemHeaderState(item);
			if (!_isRegisterEvents)
			{
				RegisterEvents();
			}
		}
	}

	private void ExpandableListItemHeader_Unloaded(object sender, RoutedEventArgs e)
	{
		UnregisterEvents();
	}

	private void OnGotFocus(object sender, RoutedEventArgs e)
	{
		UpdateFocusVisualState(sender);
	}

	private void OnKeyDown(object sender, KeyRoutedEventArgs e)
	{
		if (e.Key == VirtualKey.Left || e.Key == VirtualKey.Right)
		{
			e.Handled = true;
		}
		else if (e.Key == VirtualKey.Enter || e.Key == VirtualKey.Space)
		{
			ExpandableListItemHeader expandableListItemHeader = sender as ExpandableListItemHeader;
			if (!(expandableListItemHeader == null))
			{
				SetExpandedButton(expandableListItemHeader);
				UpdateFocusVisualState(sender);
			}
		}
	}

	private void OnPointerEntered(object sender, PointerRoutedEventArgs e)
	{
		UpdateFocusVisualState(sender);
	}

	private void OnPointerReleased(object sender, PointerRoutedEventArgs e)
	{
		UpdateFocusVisualState(sender);
	}

	private void OnPointerPressed(object sender, PointerRoutedEventArgs e)
	{
		if (sender is ExpandableListItemHeader expandableListItemHeader && !(_expandToggleButton == null))
		{
			_isClickFromExpanderButton = e.OriginalSource is DependencyObject child && UIExtensionsInternal.IsDescendantOf(child, _expandToggleButton);
			if (_isClickFromExpanderButton)
			{
				SetIsExpandedFromButton(expandableListItemHeader);
			}
			else
			{
				DoExpandOrCollapseItem(expandableListItemHeader);
				UpdateFocusVisualState(expandableListItemHeader);
			}
			e.Handled = true;
		}
	}

	private void OnPointerExited(object sender, PointerRoutedEventArgs e)
	{
		UpdateVisualState("Expanded", "Normal");
		UpdateFocusVisualState(sender);
	}

	private void _expandToggleButton_PointerMoved(object sender, PointerRoutedEventArgs e)
	{
		VisualStateManager.GoToState(this, "Normal", useTransitions: true);
		e.Handled = true;
	}

	private void RegisterEvents()
	{
		_expandToggleButton = GetTemplateChild("PART_Icon") as ExpandButton;
		_expandToggleButton.PointerEntered += OnExpandButtonPointerEntered;
		_expandToggleButton.PointerExited += OnExpandButtonPointerExited;
		_expandToggleButton.PointerMoved += _expandToggleButton_PointerMoved;
		AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(OnPointerPressed), handledEventsToo: true);
		AddHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(OnPointerReleased), handledEventsToo: true);
		base.PointerEntered += OnPointerEntered;
		base.PointerExited += OnPointerExited;
		base.KeyDown += OnKeyDown;
		base.GotFocus += OnGotFocus;
		base.Unloaded += ExpandableListItemHeader_Unloaded;
		_isRegisterEvents = true;
	}

	private void UnregisterEvents()
	{
		_expandToggleButton.PointerEntered -= OnExpandButtonPointerEntered;
		_expandToggleButton.PointerExited -= OnExpandButtonPointerExited;
		_expandToggleButton.PointerMoved -= _expandToggleButton_PointerMoved;
		RemoveHandler(UIElement.PointerPressedEvent, new PointerEventHandler(OnPointerPressed));
		RemoveHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(OnPointerReleased));
		base.PointerEntered -= OnPointerEntered;
		base.PointerExited -= OnPointerExited;
		base.KeyDown -= OnKeyDown;
		base.GotFocus -= OnGotFocus;
		base.Unloaded -= ExpandableListItemHeader_Unloaded;
		UnregisterPropertyChangedCallback(TreeViewItem.IsExpandedProperty, _isExpandedChangedToken);
		_isRegisterEvents = false;
	}

	private void OnIsExpandedChanged(DependencyObject sender, DependencyProperty dependencyProperty)
	{
		if (sender is ExpandableListItemHeader item)
		{
			UpdateExpandableListItemHeaderState(item);
		}
	}

	private void DoExpandOrCollapseItem(object sender)
	{
		if (sender is ExpandableListItemHeader expandedButton)
		{
			SetExpandedButton(expandedButton);
		}
	}

	private void SetIsExpandedFromButton(ExpandableListItemHeader treeViewItem)
	{
		treeViewItem.IsExpanded = !treeViewItem.IsExpanded;
	}

	private void SetExpandedButton(ExpandableListItemHeader treeViewItem)
	{
		if (treeViewItem.ItemsSource != null)
		{
			treeViewItem.IsExpanded = !treeViewItem.IsExpanded;
		}
	}

	private void UpdateVisualState(string firstState, string secondState)
	{
		string text = ((!_expandToggleButton.IsChecked) ? secondState : firstState);
		string stateName = text;
		VisualStateManager.GoToState(this, stateName, useTransitions: true);
	}

	private void UpdateFocusVisualState(object sender)
	{
		ExpandableListItemHeader expandableListItemHeader = sender as ExpandableListItemHeader;
		ContentControl contentControl = UIExtensionsInternal.FindChildByName<ContentControl>("ExpandableListItemHeaderBorder", expandableListItemHeader);
		bool flag = (object)contentControl != null && contentControl.Visibility == Visibility.Visible;
		if (((object)expandableListItemHeader != null && expandableListItemHeader.FocusState == FocusState.Keyboard) || ((object)expandableListItemHeader != null && expandableListItemHeader.FocusState == FocusState.Pointer && flag))
		{
			expandableListItemHeader.SetFocus();
		}
	}

	private void UpdateExpandableListItemHeaderState(ExpandableListItemHeader item)
	{
		if (!(_expandToggleButton == null))
		{
			if (!_isClickFromExpanderButton)
			{
				_expandToggleButton.IsChecked = item.IsExpanded;
			}
			_isClickFromExpanderButton = false;
			UpdateVisualState("Expanded", "Normal");
		}
	}
}
