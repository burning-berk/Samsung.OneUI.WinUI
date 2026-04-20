using System.Collections.Generic;
using System.Threading.Tasks;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class OneUISingleChoiceDialog : OneUIDialog
{
	private const string CONTENT_SINGLE_CHOICE_DIALOG_STYLE = "OneUISingleChoiceDialogStyle";

	public string OptionSelected { get; private set; }

	public OneUISingleChoiceDialog(List<string> options = null)
	{
		SingleChoiceDialogContent singleChoiceDialogContent = new SingleChoiceDialogContent(options);
		SetStyle("OneUISingleChoiceDialogStyle");
		OptionSelected = string.Empty;
		base.Content = singleChoiceDialogContent;
		singleChoiceDialogContent.OptionSelected += SingleChoiceDialogContent_OptionSelected;
	}

	private void SingleChoiceDialogContent_OptionSelected(object sender, string e)
	{
		OptionSelected = e;
		dialog.Hide();
	}

	public async Task<string> ShowAsync()
	{
		dialog.XamlRoot = base.XamlRoot;
		await dialog.ShowAsync();
		return OptionSelected;
	}

	private void SetStyle(string keyName)
	{
		defaultStyle = keyName.GetStyle();
		dialog.Style = defaultStyle;
	}
}
