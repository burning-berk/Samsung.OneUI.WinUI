using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace Samsung.OneUI.WinUI.Controls;

[TemplatePart(Name = "PART_HighlightedTextBlock", Type = typeof(TextBlock))]
internal class FilterTextBlock : Control
{
	private const string PART_HIGHLIGHTED_TEXTBLOCK = "PART_HighlightedTextBlock";

	private TextBlock _highlightedTextBlock;

	private List<char> _charIndexes;

	public static readonly DependencyProperty CustomTextProperty = DependencyProperty.Register("CustomText", typeof(string), typeof(FilterTextBlock), new PropertyMetadata(string.Empty, OnCustomTextChanged));

	public static readonly DependencyProperty SearchedTextProperty = DependencyProperty.Register("SearchedText", typeof(string), typeof(FilterTextBlock), new PropertyMetadata(string.Empty, OnSearchedTextChanged));

	public static readonly DependencyProperty TextTrimmingProperty = DependencyProperty.Register("TextTrimming", typeof(TextTrimming), typeof(FilterTextBlock), new PropertyMetadata(TextTrimming.CharacterEllipsis));

	public static readonly DependencyProperty TextHighlightForegroundColorProperty = DependencyProperty.Register("TextHighlightForegroundColor", typeof(Brush), typeof(FilterTextBlock), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 114, 222))));

	public static readonly DependencyProperty TextHighlightBackgroundColorProperty = DependencyProperty.Register("TextHighlightBackgroundColor", typeof(Brush), typeof(FilterTextBlock), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

	public static readonly DependencyProperty ForceApplyTemplateProperty = DependencyProperty.Register("ForceApplyTemplate", typeof(bool), typeof(FilterTextBlock), new PropertyMetadata(false));

	public string CustomText
	{
		get
		{
			return (string)GetValue(CustomTextProperty);
		}
		set
		{
			SetValue(CustomTextProperty, value);
		}
	}

	public string SearchedText
	{
		get
		{
			return (string)GetValue(SearchedTextProperty);
		}
		set
		{
			SetValue(SearchedTextProperty, value);
		}
	}

	public TextTrimming TextTrimming
	{
		get
		{
			return (TextTrimming)GetValue(TextTrimmingProperty);
		}
		set
		{
			SetValue(TextTrimmingProperty, value);
		}
	}

	public Brush TextHighlightForegroundColor
	{
		get
		{
			return (Brush)GetValue(TextHighlightForegroundColorProperty);
		}
		set
		{
			SetValue(TextHighlightForegroundColorProperty, value);
		}
	}

	public Brush TextHighlightBackgroundColor
	{
		get
		{
			return (Brush)GetValue(TextHighlightBackgroundColorProperty);
		}
		set
		{
			SetValue(TextHighlightBackgroundColorProperty, value);
		}
	}

	public bool ForceApplyTemplate
	{
		get
		{
			return (bool)GetValue(ForceApplyTemplateProperty);
		}
		set
		{
			SetValue(ForceApplyTemplateProperty, value);
		}
	}

	private static void OnCustomTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is FilterTextBlock filterTextBlock)
		{
			filterTextBlock.LoadCustomText();
		}
	}

	private static void OnSearchedTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is FilterTextBlock filterTextBlock)
		{
			filterTextBlock.LoadSearchedText();
		}
	}

	public FilterTextBlock()
	{
		base.DefaultStyleKey = typeof(FilterTextBlock);
	}

	private void LoadSearchedText()
	{
		if (ForceApplyTemplate)
		{
			ApplyTemplate();
		}
		ChangeTextColor();
	}

	private void LoadCustomText()
	{
		if (ForceApplyTemplate)
		{
			ApplyTemplate();
		}
		LoadComponents();
		ChangeTextColor();
	}

	private void LoadComponents()
	{
		if (_highlightedTextBlock == null)
		{
			_highlightedTextBlock = GetTemplateChild("PART_HighlightedTextBlock") as TextBlock;
		}
		if (_charIndexes != null)
		{
			_charIndexes.Clear();
		}
		LoadCharIndexes();
	}

	private void LoadCharIndexes()
	{
		List<char> charIndexes = _charIndexes;
		if (charIndexes == null || charIndexes.Count <= 0)
		{
			if (!string.IsNullOrEmpty(CustomText))
			{
				_charIndexes = CustomText.ToLowerInvariant().ToCharArray().ToList();
			}
			else
			{
				_charIndexes = new List<char>();
			}
		}
	}

	private void ChangeTextColor()
	{
		if (_highlightedTextBlock != null)
		{
			ClearTextHighlight();
			if (!string.IsNullOrEmpty(SearchedText))
			{
				ApplyHighlight(_highlightedTextBlock, CustomText, SearchedText);
			}
		}
	}

	private void ApplyHighlight(TextBlock textBlock, string fullText, string query)
	{
		textBlock.Inlines.Clear();
		int num = 0;
		foreach (Match item in Regex.Matches(fullText, Regex.Escape(query), RegexOptions.IgnoreCase))
		{
			if (item.Index > num)
			{
				textBlock.Inlines.Add(new Run
				{
					Text = fullText.Substring(num, item.Index - num)
				});
			}
			textBlock.Inlines.Add(new Run
			{
				Text = item.Value,
				Foreground = TextHighlightForegroundColor,
				FontFamily = base.FontFamily,
				FontWeight = FontWeights.SemiBold
			});
			num = item.Index + item.Length;
		}
		if (num < fullText.Length)
		{
			textBlock.Inlines.Add(new Run
			{
				Text = fullText.Substring(num)
			});
		}
	}

	public void ClearTextHighlight()
	{
		if (_highlightedTextBlock != null)
		{
			_highlightedTextBlock.TextHighlighters.Clear();
			_highlightedTextBlock.Inlines.Clear();
			_highlightedTextBlock.Inlines.Add(new Run
			{
				Text = CustomText
			});
		}
	}

	public void SetFocusTrimmedTextTooltip(bool isOpen)
	{
		if (_highlightedTextBlock != null && ToolTipService.GetToolTip(_highlightedTextBlock) is ToolTip toolTip)
		{
			toolTip.IsOpen = isOpen;
		}
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		LoadComponents();
	}
}
