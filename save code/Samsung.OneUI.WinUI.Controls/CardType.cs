using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace Samsung.OneUI.WinUI.Controls;

public sealed class CardType : Control
{
	private const string PART_CARD_BUTTON = "CardButton";

	private const string FOCUSED_STATE = "Focused";

	private const string UNFOCUSED_STATE = "Unfocused";

	public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(CardType), new PropertyMetadata(null));

	public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(CardType), new PropertyMetadata(null));

	public static readonly DependencyProperty ButtonTextProperty = DependencyProperty.Register("ButtonText", typeof(string), typeof(CardType), new PropertyMetadata(null));

	public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(CardType), new PropertyMetadata(null));

	public static readonly DependencyProperty SvgImageProperty = DependencyProperty.Register("SvgImage", typeof(Style), typeof(CardType), new PropertyMetadata(null));

	public string Title
	{
		get
		{
			return (string)GetValue(TitleProperty);
		}
		set
		{
			SetValue(TitleProperty, value);
		}
	}

	public string Description
	{
		get
		{
			return (string)GetValue(DescriptionProperty);
		}
		set
		{
			SetValue(DescriptionProperty, value);
		}
	}

	public string ButtonText
	{
		get
		{
			return (string)GetValue(ButtonTextProperty);
		}
		set
		{
			SetValue(ButtonTextProperty, value);
		}
	}

	public ImageSource Image
	{
		get
		{
			return (ImageSource)GetValue(ImageProperty);
		}
		set
		{
			SetValue(ImageProperty, value);
		}
	}

	public Style SvgImage
	{
		get
		{
			return (Style)GetValue(SvgImageProperty);
		}
		set
		{
			SetValue(SvgImageProperty, value);
		}
	}

	public event EventHandler<RoutedEventArgs> Click;

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		SubscribeButtonEvents();
	}

	public CardType()
	{
		base.Loaded += CardType_Loaded;
	}

	private void CardType_Loaded(object sender, RoutedEventArgs e)
	{
		base.GotFocus += CardType_GotFocus;
		base.LostFocus += CardType_LostFocus;
	}

	private void CardType_LostFocus(object sender, RoutedEventArgs e)
	{
		if (e.OriginalSource is CardType)
		{
			VisualStateManager.GoToState(this, "Unfocused", useTransitions: true);
		}
	}

	private void CardType_GotFocus(object sender, RoutedEventArgs e)
	{
		if (e.OriginalSource is CardType)
		{
			VisualStateManager.GoToState(this, "Focused", useTransitions: true);
		}
	}

	private void SubscribeButtonEvents()
	{
		AddClickEvent("CardButton", this.Click);
	}

	private void AddClickEvent(string PART_CARD_BUTTON, EventHandler<RoutedEventArgs> eventHandler)
	{
		if (GetTemplateChild(PART_CARD_BUTTON) is Button button)
		{
			button.Click += delegate(object s, RoutedEventArgs e)
			{
				eventHandler?.Invoke(this, e);
			};
		}
	}
}
