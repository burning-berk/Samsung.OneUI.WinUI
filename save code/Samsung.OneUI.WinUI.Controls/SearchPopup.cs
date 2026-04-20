using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.System;

namespace Samsung.OneUI.WinUI.Controls;

public class SearchPopup : Control
{
	private const string DELETE_BUTTON_NAME = "DeleteButton";

	private const string QUERY_BUTTON_NAME = "QueryButton";

	private const string SEARCH_POPUP_ELEMENT = "SearchPopupElement";

	private const string SEARCH_POPUP_CONTENT_PRESENTER = "PopupContent";

	private bool _isNextButtonAnInternalButton;

	private ContentPresenter _popupContent;

	private Popup _popupElement;

	private bool _canOpen = true;

	private bool _canNotOpenWhenHasText;

	public static readonly DependencyProperty VerticalOffsetProperty = DependencyProperty.Register("VerticalOffset", typeof(int), typeof(SearchPopup), new PropertyMetadata(0));

	public static readonly DependencyProperty HorizontalOffsetProperty = DependencyProperty.Register("HorizontalOffset", typeof(int), typeof(SearchPopup), new PropertyMetadata(0));

	public static readonly DependencyProperty PopupContentProperty = DependencyProperty.Register("PopupContent", typeof(object), typeof(SearchPopup), new PropertyMetadata(null));

	public static readonly DependencyProperty AttachToProperty = DependencyProperty.Register("AttachTo", typeof(Control), typeof(SearchPopup), new PropertyMetadata(null, OnAttachToPropertyChanged));

	public int VerticalOffset
	{
		get
		{
			return (int)GetValue(VerticalOffsetProperty);
		}
		set
		{
			SetValue(VerticalOffsetProperty, value);
		}
	}

	public int HorizontalOffset
	{
		get
		{
			return (int)GetValue(HorizontalOffsetProperty);
		}
		set
		{
			SetValue(HorizontalOffsetProperty, value);
		}
	}

	public object PopupContent
	{
		get
		{
			return GetValue(PopupContentProperty);
		}
		set
		{
			SetValue(PopupContentProperty, value);
		}
	}

	public Control AttachTo
	{
		get
		{
			return (Control)GetValue(AttachToProperty);
		}
		set
		{
			SetValue(AttachToProperty, value);
		}
	}

	private static void OnAttachToPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is SearchPopup searchPopup && searchPopup.AttachTo != null)
		{
			searchPopup.AttachTo.GettingFocus += searchPopup.Control_GettingFocus;
			searchPopup.AttachTo.GotFocus += searchPopup.Control_GotFocus;
			searchPopup.AttachTo.LosingFocus += searchPopup.Control_LosingFocus;
			searchPopup.AttachTo.LostFocus += searchPopup.Control_LostFocus;
			searchPopup.AttachTo.KeyDown += searchPopup.Control_KeyDown;
			if (searchPopup.AttachTo is AutoSuggestBox autoSuggestBox)
			{
				autoSuggestBox.TextChanged += searchPopup.AutoSuggestedBox_TextChanged;
			}
		}
	}

	public void Show()
	{
		if (AttachTo != null && _canOpen && GetTemplateChild("SearchPopupElement") is Popup popup && popup != null)
		{
			ShowAt(HorizontalOffset, VerticalOffset);
		}
	}

	public void Close(bool canNotOpenWhenHasText)
	{
		CloseCommand();
		_canNotOpenWhenHasText = canNotOpenWhenHasText;
	}

	public void Close()
	{
		CloseCommand();
	}

	private void CloseCommand()
	{
		if (_popupElement != null)
		{
			_popupElement.IsOpen = false;
		}
	}

	private void Control_KeyDown(object sender, KeyRoutedEventArgs e)
	{
		if ((e.Key == VirtualKey.Down || e.Key == VirtualKey.Up) && _popupContent != null)
		{
			StackPanel parent = UIExtensionsInternal.FindChild<StackPanel>(_popupContent);
			PrepareListFocus(parent);
			e.Handled = true;
		}
	}

	private void PrepareListFocus(StackPanel parent)
	{
		if (parent == null)
		{
			return;
		}
		List<SearchPopupList> allChildren = UIExtensionsInternal.GetAllChildren<SearchPopupList>(parent);
		if (!allChildren.Any())
		{
			return;
		}
		foreach (SearchPopupList item in allChildren)
		{
			item.Close -= List_Close;
			item.Close += List_Close;
		}
		allChildren.FirstOrDefault((SearchPopupList e) => e.Visibility != Visibility.Collapsed)?.Focus(FocusState.Keyboard);
	}

	private void List_Close(object sender, EventArgs e)
	{
		Close();
		SetFocusToParent(e);
	}

	private void SetFocusToParent(EventArgs e)
	{
		if (e is SearchEventArgs { Type: SeachPopupCloseEventType.EscapeKey } && AttachTo != null)
		{
			AttachTo.Focus(FocusState.Programmatic);
			_canOpen = false;
		}
	}

	private void AutoSuggestedBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
	{
		if (_canNotOpenWhenHasText)
		{
			_canNotOpenWhenHasText = false;
		}
		else if (!string.IsNullOrWhiteSpace(sender.Text) && args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
		{
			_canOpen = true;
			Show();
		}
	}

	private void Control_GettingFocus(UIElement sender, GettingFocusEventArgs args)
	{
		_canOpen = true;
	}

	private void Control_GotFocus(object sender, RoutedEventArgs e)
	{
		if (!string.IsNullOrWhiteSpace((sender as AutoSuggestBox).Text) && (e.OriginalSource is TextBox || IsSearchTextBoxInternalButton(e.OriginalSource)))
		{
			ContentPresenter popupContent = _popupContent;
			if ((object)popupContent != null && popupContent.ActualHeight > 0.0)
			{
				Show();
			}
		}
	}

	private void Control_LosingFocus(UIElement sender, LosingFocusEventArgs args)
	{
		_canOpen = false;
		_isNextButtonAnInternalButton = IsSearchTextBoxInternalButton(args.NewFocusedElement);
	}

	private void Control_LostFocus(object sender, RoutedEventArgs e)
	{
		object focusedElement = FocusManager.GetFocusedElement(base.XamlRoot);
		if (!(focusedElement is SearchPopupList) && !(focusedElement is SearchPopupListItem) && !_isNextButtonAnInternalButton)
		{
			Close();
			_canOpen = true;
		}
	}

	private bool IsSearchTextBoxInternalButton(object element)
	{
		if (element is Button button)
		{
			if (!button.Name.Equals("DeleteButton"))
			{
				return button.Name.Equals("QueryButton");
			}
			return true;
		}
		return false;
	}

	private void ShowAt(int x, int y)
	{
		if (_popupElement != null)
		{
			_popupElement.HorizontalOffset = x;
			_popupElement.VerticalOffset = y;
			if (!_popupElement.IsOpen)
			{
				_popupElement.IsOpen = true;
			}
		}
	}

	private T GetTemplateChild<T>(string name) where T : class
	{
		object templateChild = GetTemplateChild(name);
		if (!(templateChild is T))
		{
			return null;
		}
		return (T)templateChild;
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_popupElement = GetTemplateChild<Popup>("SearchPopupElement");
		_popupContent = GetTemplateChild("PopupContent") as ContentPresenter;
	}
}
