using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Animation;
using Windows.UI.ViewManagement;

namespace Samsung.OneUI.WinUI.Utils.Extensions;

internal static class StoryboardExtension
{
	internal static void SafeBegin(this Storyboard storyboard)
	{
		storyboard?.Begin();
		storyboard.ValidateAnimationEnabled();
	}

	internal static void ValidateAnimationEnabled(this Storyboard storyboard)
	{
		if (!IsAnimationEnabled())
		{
			storyboard?.SkipToFill();
		}
	}

	private static bool IsAnimationEnabled()
	{
		return new UISettings().AnimationsEnabled;
	}

	internal static void ValidadeAllAnimationVisualState(this FrameworkElement frameworkElement)
	{
		if (!IsAnimationEnabled())
		{
			(from state in VisualStateManager.GetVisualStateGroups(frameworkElement)?.SelectMany((VisualStateGroup @group) => @group.States)
				where state.Storyboard != null
				select state).ToList().ForEach(delegate(VisualState state)
			{
				state.Storyboard.SkipToFill();
			});
		}
	}
}
