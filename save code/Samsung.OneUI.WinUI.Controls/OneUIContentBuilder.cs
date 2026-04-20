using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Commom.Interfaces;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class OneUIContentBuilder
{
	private interface IUIComponent
	{
		List<UIElement> GetUIComponents();
	}

	private sealed class UIComponent : IUIComponent
	{
		private const double ZERO_FOCUS_VISUAL_LEFT_MARGIN = 0.0;

		private readonly List<UIElement> uiElements = new List<UIElement>();

		public void AddTextBox(string textBoxID, string initialValue = "", string placeholderText = "", TextChangedEventHandler textChangedEventHandler = null, Style style = null, Thickness marging = default(Thickness))
		{
			TextBox textBox = new TextBox
			{
				Name = textBoxID,
				Text = initialValue,
				PlaceholderText = placeholderText,
				Style = (style ?? new Style(typeof(TextBox))),
				Margin = marging
			};
			textBox.TextChanged += textChangedEventHandler;
			uiElements.Add(textBox);
		}

		public void AddTextBlock(string text, Style style = null, Thickness margin = default(Thickness), HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left)
		{
			uiElements.Add(new TextBlock
			{
				Text = text,
				Style = (style ?? new Style(typeof(TextBlock))),
				Margin = margin,
				HorizontalAlignment = horizontalAlignment
			});
		}

		public void AddToggleSwitch(string toggleSwitchID, bool isOn = true, string onContent = "", string offContent = "", Style style = null, Thickness margin = default(Thickness))
		{
			uiElements.Add(new Microsoft.UI.Xaml.Controls.ToggleSwitch
			{
				Name = toggleSwitchID,
				IsOn = isOn,
				OnContent = onContent,
				OffContent = offContent,
				Style = (style ?? new Style(typeof(Microsoft.UI.Xaml.Controls.ToggleSwitch))),
				Margin = margin
			});
		}

		public void AddRadioButton(string radioButtonID, string groupName, string content, bool isChecked = false, Thickness margin = default(Thickness), Thickness? customFocusVisualMargin = null)
		{
			RadioButton radioButton = new RadioButton
			{
				Name = radioButtonID,
				GroupName = groupName,
				IsChecked = isChecked,
				Content = content,
				Margin = margin
			};
			AdjustFocusVisualLeftMargin(radioButton, customFocusVisualMargin);
			uiElements.Add(radioButton);
		}

		public void AddCheckBox(string checkBoxID, string content, bool isChecked = false, Thickness margin = default(Thickness), Thickness? customFocusVisualMargin = null)
		{
			CheckBox checkBox = new CheckBox
			{
				Name = checkBoxID,
				IsChecked = isChecked,
				Content = content,
				Margin = margin
			};
			AdjustFocusVisualLeftMargin(checkBox, customFocusVisualMargin);
			uiElements.Add(checkBox);
		}

		public void AddSlider(string SliderID, double initialValue = 0.0, Style style = null, Thickness margin = default(Thickness), Thickness? customFocusVisualMargin = null)
		{
			Slider slider = new Slider
			{
				Name = SliderID,
				Value = initialValue,
				Style = (style ?? new Style(typeof(Slider))),
				Margin = margin
			};
			AdjustFocusVisualLeftMargin(slider, customFocusVisualMargin);
			uiElements.Add(slider);
		}

		public void AddUIElement(UIElement element)
		{
			uiElements.Add(element);
		}

		List<UIElement> IUIComponent.GetUIComponents()
		{
			return uiElements;
		}

		private void AdjustFocusVisualLeftMargin(UIElement element, Thickness? customFocusVisualMargin)
		{
			FrameworkElement frameworkElement = element as FrameworkElement;
			if (frameworkElement == null)
			{
				return;
			}
			if (customFocusVisualMargin.HasValue)
			{
				frameworkElement.FocusVisualMargin = customFocusVisualMargin.Value;
			}
			else if (frameworkElement is IDialogComponentNegativeOutStroke dialogComponentNegativeOutStroke)
			{
				Thickness focusVisualMargin = dialogComponentNegativeOutStroke.GetFocusVisualMargin();
				if (focusVisualMargin.Left >= 0.0)
				{
					frameworkElement.FocusVisualMargin = focusVisualMargin;
				}
				else
				{
					frameworkElement.FocusVisualMargin = new Thickness(0.0, focusVisualMargin.Top, focusVisualMargin.Right, focusVisualMargin.Bottom);
				}
			}
		}
	}

	private const string INVALID_OPERATION = "This builder has already been built. Do not reuse this instance.";

	private const string ID_ALREADY_MESSAGE = "This ID is already in use. Please insert another one.";

	private const string TOGGLE_SWITCH_STYLE = "OneUIToggleSwitchStyle";

	private const string SLIDER_STYLE = "OneUISliderType1Style";

	private const string DIALOG_TEXTBLOCK_STYLE = "OneUITextBlockDialogStyle";

	private const double MININUM_FOCUS_VISUAL_BOTTOM_MARGIN = 1.0;

	private readonly List<string> globalIDsList = new List<string>();

	private readonly Dictionary<string, object> dictIdValue = new Dictionary<string, object>();

	private bool alreadyBuilt;

	private readonly Stack<UIComponent> gridStack;

	private TextBlock dialogDescription = new TextBlock();

	public OneUIContentBuilder()
	{
		gridStack = new Stack<UIComponent>();
		gridStack.Push(new UIComponent());
	}

	public OneUIContentBuilder AddDescription(string text, Style style = null)
	{
		if (alreadyBuilt)
		{
			throw new InvalidOperationException("This builder has already been built. Do not reuse this instance.");
		}
		Style style2 = ((style == null) ? "OneUITextBlockDialogStyle".GetStyle() : style);
		dialogDescription = new TextBlock
		{
			Text = text,
			Style = style2
		};
		return this;
	}

	public OneUIContentBuilder AddTextBox(string textBoxID, string initialValue = "", string placeholderText = "", TextChangedEventHandler textChangedEventHandler = null, Style style = null, Thickness margin = default(Thickness))
	{
		if (alreadyBuilt)
		{
			throw new InvalidOperationException("This builder has already been built. Do not reuse this instance.");
		}
		if (globalIDsList.Contains(textBoxID))
		{
			throw new InvalidOperationException("This ID is already in use. Please insert another one.");
		}
		globalIDsList.Add(textBoxID);
		dictIdValue.Add(textBoxID, initialValue);
		gridStack.Peek().AddTextBox(textBoxID, initialValue, placeholderText, textChangedEventHandler, style, margin);
		return this;
	}

	public OneUIContentBuilder AddTextBlock(string text, Style style = null, Thickness margin = default(Thickness), HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left)
	{
		if (alreadyBuilt)
		{
			throw new InvalidOperationException("This builder has already been built. Do not reuse this instance.");
		}
		Style style2 = ((style == null) ? "OneUITextBlockDialogStyle".GetStyle() : style);
		gridStack.Peek().AddTextBlock(text, style2, margin, horizontalAlignment);
		return this;
	}

	public OneUIContentBuilder AddToggleSwitch(string toggleSwitchID, bool isOn = true, string onContent = "", string offContent = "", Style style = null, Thickness margin = default(Thickness))
	{
		if (alreadyBuilt)
		{
			throw new InvalidOperationException("This builder has already been built. Do not reuse this instance.");
		}
		if (globalIDsList.Contains(toggleSwitchID))
		{
			throw new InvalidOperationException("This ID is already in use. Please insert another one.");
		}
		globalIDsList.Add(toggleSwitchID);
		dictIdValue.Add(toggleSwitchID, isOn);
		Style style2 = ((style == null) ? "OneUIToggleSwitchStyle".GetStyle() : style);
		gridStack.Peek().AddToggleSwitch(toggleSwitchID, isOn, onContent, offContent, style2, margin);
		return this;
	}

	public OneUIContentBuilder AddRadioButton(string radioButtonID, string groupName, string content, bool isChecked = false, Thickness margin = default(Thickness))
	{
		if (alreadyBuilt)
		{
			throw new InvalidOperationException("This builder has already been built. Do not reuse this instance.");
		}
		if (globalIDsList.Contains(radioButtonID))
		{
			throw new InvalidOperationException("This ID is already in use. Please insert another one.");
		}
		if (isChecked)
		{
			dictIdValue[groupName] = radioButtonID;
		}
		globalIDsList.Add(radioButtonID);
		gridStack.Peek().AddRadioButton(radioButtonID, groupName, content, isChecked, margin);
		return this;
	}

	public OneUIContentBuilder AddCheckBox(string checkBoxID, string content, bool isChecked = false, Thickness margin = default(Thickness))
	{
		if (alreadyBuilt)
		{
			throw new InvalidOperationException("This builder has already been built. Do not reuse this instance.");
		}
		if (globalIDsList.Contains(checkBoxID))
		{
			throw new InvalidOperationException("This ID is already in use. Please insert another one.");
		}
		globalIDsList.Add(checkBoxID);
		gridStack.Peek().AddCheckBox(checkBoxID, content, isChecked, margin);
		return this;
	}

	public OneUIContentBuilder AddSlider(string SliderID, double initialValue, Style style = null, Thickness margin = default(Thickness), Thickness? customFocusVisualMargin = null)
	{
		if (alreadyBuilt)
		{
			throw new InvalidOperationException("This builder has already been built. Do not reuse this instance.");
		}
		if (globalIDsList.Contains(SliderID))
		{
			throw new InvalidOperationException("This ID is already in use. Please insert another one.");
		}
		globalIDsList.Add(SliderID);
		Style style2 = ((style == null) ? "OneUISliderType1Style".GetStyle() : style);
		gridStack.Peek().AddSlider(SliderID, initialValue, style2, margin, customFocusVisualMargin);
		return this;
	}

	public OneUIContentBuilder AddElement(UIElement element)
	{
		if (element != null)
		{
			if (alreadyBuilt)
			{
				throw new InvalidOperationException("This builder has already been built. Do not reuse this instance.");
			}
			gridStack.Peek().AddUIElement(element);
		}
		return this;
	}

	public object Build()
	{
		ScrollViewer scrollViewer = new ScrollViewer
		{
			VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
			HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled
		};
		Grid grid = new Grid
		{
			VerticalAlignment = VerticalAlignment.Top
		};
		RowDefinition item = new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		};
		grid.RowDefinitions.Add(item);
		grid.Children.Add(dialogDescription);
		Grid.SetRow(dialogDescription, 0);
		List<UIElement> uIComponents = ((IUIComponent)gridStack.Peek()).GetUIComponents();
		int num = 1;
		foreach (UIElement item2 in uIComponents)
		{
			FrameworkElement element = item2 as FrameworkElement;
			bool num2 = num == uIComponents.Count;
			item = new RowDefinition
			{
				Height = new GridLength(0.0, GridUnitType.Auto)
			};
			grid.RowDefinitions.Add(item);
			if (num2)
			{
				AdjustBottomFocusVisualMargin(element);
			}
			grid.Children.Add(item2);
			Grid.SetRow(element, num);
			num++;
		}
		alreadyBuilt = true;
		scrollViewer.Content = grid;
		return new OneUIContentDialogContent
		{
			ScrollViewer = scrollViewer
		};
	}

	private void AdjustBottomFocusVisualMargin(FrameworkElement element)
	{
		if (!(element == null) && element is IDialogComponentNegativeOutStroke && element.FocusVisualMargin.Bottom < 0.0)
		{
			Thickness focusVisualMargin = element.FocusVisualMargin;
			element.FocusVisualMargin = new Thickness(focusVisualMargin.Left, focusVisualMargin.Top, focusVisualMargin.Right, 1.0);
		}
	}

	public void Clear()
	{
		dialogDescription = new TextBlock();
		globalIDsList.Clear();
		dictIdValue.Clear();
		alreadyBuilt = false;
		gridStack.Clear();
		gridStack.Push(new UIComponent());
	}
}
