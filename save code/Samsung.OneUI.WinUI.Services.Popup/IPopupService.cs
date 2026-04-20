using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Windows.Foundation;

namespace Samsung.OneUI.WinUI.Services.Popup;

internal interface IPopupService
{
	void CloseAllPopups(XamlRoot xamlRoot, Func<Microsoft.UI.Xaml.Controls.Primitives.Popup, bool> predicate = null);

	void CloseSubMenuPopups(XamlRoot xamlRoot, Func<Microsoft.UI.Xaml.Controls.Primitives.Popup, bool> predicate = null);

	void PreventBackdropPupups(XamlRoot xamlRoot, bool isBlur = false, Func<Microsoft.UI.Xaml.Controls.Primitives.Popup, bool> predicate = null);

	List<Microsoft.UI.Xaml.Controls.Primitives.Popup> GetOpenPopups(XamlRoot xamlRoot, Func<Microsoft.UI.Xaml.Controls.Primitives.Popup, bool> predicate = null);

	void SetPopupOffsets(FrameworkElement contentPresenter, FrameworkElement target, Point offset);
}
