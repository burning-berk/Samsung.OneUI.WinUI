using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Windows.Foundation;

namespace Samsung.OneUI.WinUI.Controls.DialogsAndFlyouts.SnackBar;

internal class SnackBarAnimationService
{
	private const int SHORT_DURATION_MILLISECCONDS = 1500;

	private const int LONG_DURATION_MILLISECCONDS = 2750;

	private const int CUSTOM_DURATION_MILLISECCONDS = 3500;

	private const int SNACKBAR_SIZE_INIT = 44;

	private const int SNACKBAR_TARGET_Y_SHOW = 24;

	private const int SNACKBAR_TARGET_Y_HIDE = 16;

	private const int ELASTICITY = 8;

	private const int SNACKBAR_ANIMATION_TIME_START = 150;

	private const string OPACITY_PROPERTY = "Opacity";

	private const string SPACING_PROPERTY = "Spacing";

	private const string WIDTH_PROPERTY = "Width";

	private const string HEIGHT_PROPERTY = "Height";

	private const string TRANSLATE_TRANSFORM_Y_PROPERTY = "(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.Y)";

	private const string SCALE_TRANSFORM_X_PROPERTY = "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)";

	private const string SCALE_TRANSFORM_Y_PROPERTY = "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)";

	private readonly Border _snackBarBorder;

	private readonly FrameworkElement _snackBarText;

	private readonly StackPanel _snackBarContainer;

	private readonly FrameworkElement _snackBarButton;

	public SnackBarAnimationService(Border snackBarBorder, StackPanel snackBarContainer, FrameworkElement snackBarText, FrameworkElement snackBarButton)
	{
		_snackBarBorder = snackBarBorder;
		_snackBarText = snackBarText;
		_snackBarButton = snackBarButton;
		_snackBarContainer = snackBarContainer;
	}

	public void CreateAnimation(FrameworkElement target, SnackBarDuration duration, double verticalOffsetY, Action AnimationCompleted)
	{
		int snackbarDuration = GetDurationShownMilliseconds(duration);
		double snackbarWidth = _snackBarBorder.ActualWidth;
		double snackbarHeight = _snackBarBorder.ActualHeight;
		_snackBarBorder.Width = 44.0;
		_snackBarBorder.Height = 44.0;
		SetupTransformGroup();
		Storyboard storyboard = CreateEntranceStoryboard(verticalOffsetY);
		storyboard.Completed += delegate
		{
			CreateMainStoryboard(snackbarDuration, snackbarWidth, snackbarHeight, verticalOffsetY, AnimationCompleted);
		};
		storyboard.Begin();
	}

	private int GetDurationShownMilliseconds(SnackBarDuration duration)
	{
		int num = 1500;
		switch (duration)
		{
		case SnackBarDuration.Short:
			num = 1500;
			break;
		case SnackBarDuration.Long:
			num = 2750;
			break;
		case SnackBarDuration.Custom:
			num = 3500;
			break;
		}
		return num - 150;
	}

	private void SetEasingDoubleKeyFrameAnimation(DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames, double value, double time, bool enableDependentAnimation = false)
	{
		doubleAnimationUsingKeyFrames.KeyFrames.Add(new EasingDoubleKeyFrame
		{
			Value = value,
			KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(time))
		});
		doubleAnimationUsingKeyFrames.EnableDependentAnimation = enableDependentAnimation;
	}

	private void SetLinearDoubleKeyFrameAnimation(DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames, double value, double time)
	{
		doubleAnimationUsingKeyFrames.KeyFrames.Add(new LinearDoubleKeyFrame
		{
			Value = value,
			KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(time))
		});
	}

	private void SetAnimation(DependencyObject target, string propName, Timeline timelineBase)
	{
		Storyboard.SetTarget(timelineBase, target);
		Storyboard.SetTargetProperty(timelineBase, propName);
	}

	private Storyboard CreateEntranceStoryboard(double verticalOffsetY)
	{
		Storyboard storyboard = new Storyboard();
		DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
		DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames2 = new DoubleAnimationUsingKeyFrames();
		DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames3 = new DoubleAnimationUsingKeyFrames();
		DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames4 = new DoubleAnimationUsingKeyFrames();
		SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames, verticalOffsetY + _snackBarBorder.ActualHeight + 24.0, 0.0);
		SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames, verticalOffsetY - 24.0, 150.0);
		SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames2, 0.0, 0.0);
		SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames2, 1.0, 150.0);
		SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames3, 0.0, 0.0);
		SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames3, 1.0, 150.0);
		SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames4, 0.0, 0.0);
		SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames4, 1.0, 150.0);
		SetAnimation(_snackBarBorder, "(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.Y)", doubleAnimationUsingKeyFrames);
		SetAnimation(_snackBarBorder, "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)", doubleAnimationUsingKeyFrames3);
		SetAnimation(_snackBarBorder, "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)", doubleAnimationUsingKeyFrames4);
		SetAnimation(_snackBarBorder, "Opacity", doubleAnimationUsingKeyFrames2);
		storyboard.Children.Add(doubleAnimationUsingKeyFrames);
		storyboard.Children.Add(doubleAnimationUsingKeyFrames2);
		storyboard.Children.Add(doubleAnimationUsingKeyFrames3);
		storyboard.Children.Add(doubleAnimationUsingKeyFrames4);
		return storyboard;
	}

	private void CreateMainStoryboard(double snackbarDuration, double snackbarWidth, double snackbarHeight, double verticalOffsetY, Action AnimationCompleted)
	{
		Storyboard storyboard = new Storyboard();
		DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
		DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames2 = new DoubleAnimationUsingKeyFrames();
		DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames3 = new DoubleAnimationUsingKeyFrames();
		DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames4 = new DoubleAnimationUsingKeyFrames();
		new DoubleAnimationUsingKeyFrames();
		DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames5 = new DoubleAnimationUsingKeyFrames();
		DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames6 = new DoubleAnimationUsingKeyFrames();
		DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames7 = new DoubleAnimationUsingKeyFrames();
		SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames, verticalOffsetY - 24.0, 0.0);
		SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames, verticalOffsetY, 150.0);
		SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames, verticalOffsetY, snackbarDuration);
		SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames, verticalOffsetY + 16.0, snackbarDuration + 150.0);
		SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames5, 44.0, 0.0, enableDependentAnimation: true);
		if (_snackBarButton.Visibility == Visibility.Visible && _snackBarContainer.Orientation == Orientation.Horizontal)
		{
			SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames5, snackbarWidth + 8.0, 150.0, enableDependentAnimation: true);
			SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames5, snackbarWidth, 300.0, enableDependentAnimation: true);
			SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames7, 8.0, 150.0, enableDependentAnimation: true);
			SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames7, 0.0, 300.0, enableDependentAnimation: true);
		}
		else
		{
			SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames5, snackbarWidth, 150.0, enableDependentAnimation: true);
			SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames7, 0.0, 150.0, enableDependentAnimation: true);
			SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames7, 0.0, 300.0, enableDependentAnimation: true);
		}
		SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames6, 44.0, 0.0, enableDependentAnimation: true);
		SetEasingDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames6, snackbarHeight, 150.0, enableDependentAnimation: true);
		SetLinearDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames2, 1.0, 0.0);
		SetLinearDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames2, 1.0, 150.0);
		SetLinearDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames2, 1.0, snackbarDuration);
		SetLinearDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames2, 0.0, snackbarDuration + 150.0);
		SetLinearDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames3, 0.0, 150.0);
		SetLinearDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames3, 1.0, 300.0);
		SetLinearDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames3, 1.0, snackbarDuration);
		SetLinearDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames4, 0.0, 150.0);
		SetLinearDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames4, 1.0, 300.0);
		SetLinearDoubleKeyFrameAnimation(doubleAnimationUsingKeyFrames4, 1.0, snackbarDuration);
		SetAnimation(_snackBarBorder, "(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.Y)", doubleAnimationUsingKeyFrames);
		SetAnimation(_snackBarBorder, "Width", doubleAnimationUsingKeyFrames5);
		SetAnimation(_snackBarBorder, "Height", doubleAnimationUsingKeyFrames6);
		SetAnimation(_snackBarBorder, "Opacity", doubleAnimationUsingKeyFrames2);
		SetAnimation(_snackBarText, "Opacity", doubleAnimationUsingKeyFrames3);
		SetAnimation(_snackBarButton, "Opacity", doubleAnimationUsingKeyFrames4);
		SetAnimation(_snackBarContainer, "Spacing", doubleAnimationUsingKeyFrames7);
		storyboard.Children.Add(doubleAnimationUsingKeyFrames);
		storyboard.Children.Add(doubleAnimationUsingKeyFrames5);
		storyboard.Children.Add(doubleAnimationUsingKeyFrames6);
		storyboard.Children.Add(doubleAnimationUsingKeyFrames2);
		storyboard.Children.Add(doubleAnimationUsingKeyFrames3);
		storyboard.Children.Add(doubleAnimationUsingKeyFrames4);
		storyboard.Children.Add(doubleAnimationUsingKeyFrames7);
		storyboard.Completed += delegate
		{
			AnimationCompleted();
		};
		storyboard.Begin();
	}

	private void SetupTransformGroup()
	{
		TransformGroup transformGroup = new TransformGroup();
		transformGroup.Children.Add(new ScaleTransform());
		transformGroup.Children.Add(new TranslateTransform());
		_snackBarBorder.RenderTransform = transformGroup;
		_snackBarBorder.RenderTransformOrigin = new Point(0.5, 0.0);
	}
}
