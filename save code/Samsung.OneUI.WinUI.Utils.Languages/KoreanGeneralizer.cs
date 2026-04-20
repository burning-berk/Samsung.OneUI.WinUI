using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Samsung.OneUI.WinUI.Utils.Languages;

public class KoreanGeneralizer
{
	private const string NON_PRONOUNCEABLE_CHARACTERS = "()[]<>{};:|`'\"\\.=!?&。 ♡♥…«»‘’‚‛“”„‟‹›❛❜❝❞〝〞〟＂＇";

	private const string HAS_JOSA_REGEX = "(?s)(.*)\\((.+)\\)(.*)";

	private static readonly Regex _hasJosaPattern = new Regex("(?s)(.*)\\((.+)\\)(.*)");

	private readonly Dictionary<string, Tuple<string, string>> _josaKoreanMap = new Dictionary<string, Tuple<string, string>>
	{
		["은(는)"] = Tuple.Create("은", "는"),
		["(은)는"] = Tuple.Create("은", "는"),
		["이(가)"] = Tuple.Create("이", "가"),
		["(이)가"] = Tuple.Create("이", "가"),
		["을(를)"] = Tuple.Create("을", "를"),
		["(을)를"] = Tuple.Create("을", "를"),
		["와(과)"] = Tuple.Create("과", "와"),
		["(와)과"] = Tuple.Create("과", "와"),
		["아(야)"] = Tuple.Create("아", "야"),
		["(아)야"] = Tuple.Create("아", "야"),
		["(이)여"] = Tuple.Create("이여", "여"),
		["(으)로"] = Tuple.Create("으로", "로"),
		["(이)라"] = Tuple.Create("이라", "라"),
		["(이에)예"] = Tuple.Create("이에", "예"),
		["이에(예)"] = Tuple.Create("이에", "예"),
		["(이었)였"] = Tuple.Create("이었", "였"),
		["이었(였)"] = Tuple.Create("이었", "였"),
		["(이)네"] = Tuple.Create("이네", "네")
	};

	private readonly Dictionary<char, Tuple<bool, bool>> _pronounceableSymbols = new Dictionary<char, Tuple<bool, bool>>
	{
		['0'] = Tuple.Create(item1: true, item2: false),
		['1'] = Tuple.Create(item1: true, item2: true),
		['2'] = Tuple.Create(item1: false, item2: false),
		['3'] = Tuple.Create(item1: true, item2: false),
		['4'] = Tuple.Create(item1: false, item2: false),
		['5'] = Tuple.Create(item1: false, item2: false),
		['6'] = Tuple.Create(item1: true, item2: false),
		['7'] = Tuple.Create(item1: true, item2: true),
		['8'] = Tuple.Create(item1: true, item2: true),
		['9'] = Tuple.Create(item1: false, item2: false),
		['%'] = Tuple.Create(item1: false, item2: false),
		['％'] = Tuple.Create(item1: false, item2: false),
		['$'] = Tuple.Create(item1: false, item2: false),
		['#'] = Tuple.Create(item1: true, item2: false),
		['℃'] = Tuple.Create(item1: false, item2: false),
		['℉'] = Tuple.Create(item1: false, item2: false),
		['㎥'] = Tuple.Create(item1: false, item2: false),
		['+'] = Tuple.Create(item1: false, item2: false),
		['°'] = Tuple.Create(item1: false, item2: false),
		['º'] = Tuple.Create(item1: false, item2: false),
		['㏄'] = Tuple.Create(item1: false, item2: false),
		['㎖'] = Tuple.Create(item1: false, item2: false),
		['ℓ'] = Tuple.Create(item1: false, item2: false),
		['㏈'] = Tuple.Create(item1: true, item2: true),
		['㎅'] = Tuple.Create(item1: false, item2: false),
		['㎆'] = Tuple.Create(item1: false, item2: false),
		['㎇'] = Tuple.Create(item1: false, item2: false),
		['㏔'] = Tuple.Create(item1: false, item2: false)
	};

	public string NaturalizeText(string originalText)
	{
		return Naturalize(originalText);
	}

	private string Naturalize(string str)
	{
		if (string.IsNullOrEmpty(str))
		{
			return string.Empty;
		}
		if (!HasJosaInString(str))
		{
			return str;
		}
		StringBuilder stringBuilder = new StringBuilder(str.Length);
		char lastCharacter = '\0';
		for (int i = 0; i < str.Length; i++)
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = str.Substring(i);
			string text5 = null;
			bool isOpposite = false;
			foreach (string key in _josaKoreanMap.Keys)
			{
				if (text4.StartsWith(key))
				{
					text5 = key;
					break;
				}
			}
			if (!string.IsNullOrEmpty(text5))
			{
				text3 = text5;
				Tuple<string, string> tuple = _josaKoreanMap[text3];
				text = tuple.Item1;
				text2 = tuple.Item2;
				isOpposite = text3 == "(으)로";
			}
			char c = str[i];
			if (string.IsNullOrEmpty(text) && string.IsNullOrEmpty(text2))
			{
				stringBuilder.Append(c);
			}
			else
			{
				bool? flag = CheckIfEndsWithKoreanJongSung(lastCharacter, isOpposite);
				if (!flag.HasValue)
				{
					flag = CheckIfEndsWithPronounceableSymbols(lastCharacter, isOpposite);
				}
				if (flag.HasValue)
				{
					string text6 = (flag.Value ? text : text2);
					stringBuilder.Append(text6);
					i += text3.Length - 1;
					c = text6[text6.Length - 1];
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			if (!"()[]<>{};:|`'\"\\.=!?&。 ♡♥…«»‘’‚‛“”„‟‹›❛❜❝❞〝〞〟＂＇".Contains(c.ToString()))
			{
				lastCharacter = c;
			}
		}
		return stringBuilder.ToString();
	}

	private bool? CheckIfEndsWithKoreanJongSung(int lastCharacter, bool isOpposite)
	{
		if (44032 <= lastCharacter && lastCharacter <= 55203)
		{
			int num = (lastCharacter - 44032) % 28;
			if (isOpposite && (num == 0 || num == 8))
			{
				num = 0;
			}
			return num > 0;
		}
		return null;
	}

	private bool HasJosaInString(string str)
	{
		return _hasJosaPattern.IsMatch(str);
	}

	private bool? CheckIfEndsWithPronounceableSymbols(char lastCharacter, bool isOpposite)
	{
		if (_pronounceableSymbols.TryGetValue(lastCharacter, out var value))
		{
			bool flag = value.Item1;
			if (value.Item2 && isOpposite)
			{
				flag = !flag;
			}
			return flag;
		}
		return null;
	}
}
