using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Shapes;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.Foundation;

namespace Samsung.OneUI.WinUI.Controls;

public sealed class ProgressBar : Microsoft.UI.Xaml.Controls.ProgressBar
{
	private const string PART_ANIMATION = "IndeterminateAnimation";

	private const string PART_PROGRESSTEXT = "OneUIProgressTextBlock";

	private const string STATE_NORMAL = "Normal";

	private const string STATE_INDETERMINATE = "Indeterminate";

	private const string PROGRESS_BAR_INDICATOR = "ProgressBarIndicator";

	private const string DETERMINATE_STYLE = "OneUIProgressBarDeterminateStyle";

	private const string INDETERMINATE_STYLE = "OneUIProgressBarIndeterminateStyle";

	private Storyboard _indeterminateAnimation;

	private TextBlock _progressText;

	private Rectangle _progressBarIndicator;

	public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ProgressBar), new PropertyMetadata(null, OnTextPropertyChanged));

	public string Text
	{
		get
		{
			return (string)GetValue(TextProperty);
		}
		set
		{
			SetValue(TextProperty, value);
		}
	}

	public ProgressBar()
	{
		base.Style = (base.IsIndeterminate ? "OneUIProgressBarIndeterminateStyle".GetStyle() : "OneUIProgressBarDeterminateStyle".GetStyle());
		RegisterPropertyChangedCallback(Microsoft.UI.Xaml.Controls.ProgressBar.IsIndeterminateProperty, OnIndeterminatePropertyChanged);
		RegisterPropertyChangedCallback(UIElement.VisibilityProperty, OnVisibilityPropertyChanged);
		AssignInternalEvents();
		base.SizeChanged += ProgressBar_SizeChanged;
	}

	private void SetAnimation()
	{
		_indeterminateAnimation = GetTemplateChild("IndeterminateAnimation") as Storyboard;
		if (base.IsIndeterminate && _indeterminateAnimation != null)
		{
			_indeterminateAnimation.Stop();
			DoubleAnimationUsingKeyFrames obj = _indeterminateAnimation.Children[0] as DoubleAnimationUsingKeyFrames;
			obj.KeyFrames.Clear();
			SplineDoubleKeyFrame item = CreateSplineDoubleKeyFrame(0.0, -1.448);
			SplineDoubleKeyFrame item2 = CreateSplineDoubleKeyFrame(1.28, 1.096);
			obj.KeyFrames.Add(item);
			obj.KeyFrames.Add(item2);
			DoubleAnimationUsingKeyFrames obj2 = _indeterminateAnimation.Children[1] as DoubleAnimationUsingKeyFrames;
			obj2.KeyFrames.Clear();
			SplineDoubleKeyFrame item3 = CreateSplineDoubleKeyFrame(0.35, -0.537);
			SplineDoubleKeyFrame item4 = CreateSplineDoubleKeyFrame(1.55, 1.048);
			obj2.KeyFrames.Add(item3);
			obj2.KeyFrames.Add(item4);
			DoubleAnimationUsingKeyFrames obj3 = _indeterminateAnimation.Children[2] as DoubleAnimationUsingKeyFrames;
			obj3.KeyFrames.Clear();
			SplineDoubleKeyFrame item5 = CreateSplineDoubleKeyFrame(0.5, -0.281);
			SplineDoubleKeyFrame item6 = CreateSplineDoubleKeyFrame(1.75, 1.015);
			obj3.KeyFrames.Add(item5);
			obj3.KeyFrames.Add(item6);
			DoubleAnimationUsingKeyFrames obj4 = _indeterminateAnimation.Children[3] as DoubleAnimationUsingKeyFrames;
			obj4.KeyFrames.Clear();
			SplineDoubleKeyFrame item7 = CreateSplineDoubleKeyFrame(0.666, -0.015);
			SplineDoubleKeyFrame item8 = CreateSplineDoubleKeyFrame(1.916, 1.015);
			obj4.KeyFrames.Add(item7);
			obj4.KeyFrames.Add(item8);
			_indeterminateAnimation.Begin();
			_indeterminateAnimation.Completed += IndeterminateAnimation_Completed;
			_indeterminateAnimation.RepeatBehavior = new RepeatBehavior(1.0);
		}
	}

	private SplineDoubleKeyFrame CreateSplineDoubleKeyFrame(double initialTime, double value)
	{
		return new SplineDoubleKeyFrame
		{
			KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(initialTime)),
			Value = base.ActualWidth * value,
			KeySpline = new KeySpline
			{
				ControlPoint1 = new Point(0.33, 0.0),
				ControlPoint2 = new Point(0.2, 1.0)
			}
		};
	}

	private static void OnVisibilityPropertyChanged(DependencyObject d, DependencyProperty dp)
	{
		if (d is ProgressBar { IsIndeterminate: not false } progressBar)
		{
			if (progressBar.Visibility == Visibility.Collapsed)
			{
				progressBar.StopAnimation();
			}
			else
			{
				progressBar.SetAnimation();
			}
		}
	}

	private void ProgressBar_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		ForceUpdateProgressIndicator();
		SetAnimation();
	}

	private void ProgressBar_Loaded(object sender, RoutedEventArgs e)
	{
		base.ValueChanged += ProgressBar_ValueChanged;
	}

	private void ProgressBar_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
	{
		ForceUpdateProgressIndicator();
	}

	private void ProgressBar_Unloaded(object sender, RoutedEventArgs e)
	{
		StopAnimation();
	}

	private void StopAnimation()
	{
		if (_indeterminateAnimation != null)
		{
			_indeterminateAnimation.Completed -= IndeterminateAnimation_Completed;
			_indeterminateAnimation?.Stop();
		}
	}

	private void AssignInternalEvents()
	{
		base.Loaded += ProgressBar_Loaded;
		base.SizeChanged += ProgressBar_SizeChanged;
		base.Unloaded += ProgressBar_Unloaded;
	}

	private void ForceUpdateProgressIndicator()
	{
		UpdateIndicatorElement();
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		InitiateIndeterminateAnimation();
		UpdateStyle();
		UpdateLayoutProgressText();
	}

	private void InitiateIndeterminateAnimation()
	{
		SetAnimation();
	}

	private void UpdateLayoutProgressText()
	{
		if (_progressText == null)
		{
			_progressText = GetTemplateChild("OneUIProgressTextBlock") as TextBlock;
		}
		if (_progressText != null && !string.IsNullOrEmpty(Text))
		{
			_progressText.Text = Text;
			_progressText.Visibility = Visibility.Visible;
		}
	}

	private void OnIndeterminatePropertyChanged(DependencyObject sender, DependencyProperty dp)
	{
		if (sender is ProgressBar progressBar)
		{
			progressBar.UpdateStyle();
		}
	}

	private void UpdateStyle()
	{
		base.Style = (base.IsIndeterminate ? "OneUIProgressBarIndeterminateStyle".GetStyle() : "OneUIProgressBarDeterminateStyle".GetStyle());
		UpdateIndicatorElement();
	}

	private void UpdateIndicatorElement()
	{
		if (!base.IsIndeterminate)
		{
			_progressBarIndicator = GetTemplateChild("ProgressBarIndicator") as Rectangle;
			if (_progressBarIndicator != null && base.Maximum > 0.0)
			{
				_progressBarIndicator.Width = base.ActualWidth * (base.Value / base.Maximum);
			}
		}
	}

	private void IndeterminateAnimation_Completed(object sender, object e)
	{
		RestartAnimation();
	}

	private void RestartAnimation()
	{
		VisualStateManager.GoToState(this, "Normal", useTransitions: false);
		VisualStateManager.GoToState(this, "Indeterminate", useTransitions: false);
	}

	private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ProgressBar progressBar && !(progressBar._progressText == null))
		{
			progressBar._progressText.Visibility = (string.IsNullOrEmpty(progressBar.Text) ? Visibility.Collapsed : Visibility.Visible);
		}
	}
}
