using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Samsung.OneUI.WinUI.Controls;
using Samsung.OneUI.WinUI.Services.Animation.Timelines;
using Windows.Foundation;

namespace Samsung.OneUI.WinUI.Services.Animation.Services.Implementations;

internal class DatePickerAnimationService : IDatePickerAnimationService, IAnimationService
{
	private const int ANIMATION_START_TIME = 0;

	private const int ANIMATION_TOTAL_DURATION = 300;

	private const int FADE_OUT_DURATION = 100;

	private const int FADE_IN_START = 101;

	private const double ARROW_UP_ROTATION = 180.0;

	private const double ARROW_DOWN_ROTATION = 0.0;

	private const double OPACITY_VISIBLE = 1.0;

	private const double OPACITY_INVISIBLE = 0.0;

	private const double EASING_X1 = 0.22;

	private const double EASING_Y1 = 0.25;

	private const double EASING_X2 = 0.0;

	private const double EASING_Y2 = 1.0;

	private const string ROTATION_PROPERTY = "(UIElement.RenderTransform).(RotateTransform.Angle)";

	private readonly FrameworkElement _iconContainer;

	private readonly FrameworkElement _leftArrowButton;

	private readonly FrameworkElement _rightArrowButton;

	private readonly FrameworkElement _calendarView;

	private readonly FrameworkElement _spinnerView;

	private readonly IAnimationMaker _animationMaker;

	public DatePickerAnimationService(FrameworkElement iconContainer, FrameworkElement leftArrowButton, FrameworkElement rightArrowButton, FrameworkElement calendarView, FrameworkElement spinnerView)
	{
		_iconContainer = iconContainer;
		_leftArrowButton = leftArrowButton;
		_rightArrowButton = rightArrowButton;
		_calendarView = calendarView;
		_spinnerView = spinnerView;
		_animationMaker = new AnimationMaker();
		SetupIconTransform();
	}

	public void CreateTransitionAnimation(TransitionType transitionType)
	{
		_animationMaker.Create();
		double fromAngle = ((transitionType == TransitionType.SpinnerToCalendar) ? (-180.0) : 0.0);
		double toAngle = ((transitionType == TransitionType.SpinnerToCalendar) ? 0.0 : (-180.0));
		AddRotationAnimation(_iconContainer, fromAngle, toAngle);
		if (transitionType == TransitionType.SpinnerToCalendar)
		{
			AddDelayedFadeInAnimation(_leftArrowButton);
			AddDelayedFadeInAnimation(_rightArrowButton);
			AddDelayedFadeInAnimation(_calendarView);
			AddFadeOutAnimation(_spinnerView);
		}
		else
		{
			AddFadeOutAnimation(_leftArrowButton);
			AddFadeOutAnimation(_rightArrowButton);
			AddFadeOutAnimation(_calendarView);
			AddDelayedFadeInAnimation(_spinnerView);
		}
		_animationMaker.Execute();
	}

	private void SetupIconTransform()
	{
		RotateTransform renderTransform = new RotateTransform
		{
			CenterX = _iconContainer.ActualWidth / 2.0,
			CenterY = _iconContainer.ActualHeight / 2.0
		};
		_iconContainer.RenderTransform = renderTransform;
	}

	private void AddFadeAnimation(FrameworkElement target, FadeType fadeType, int beginTime, int endTime)
	{
		TimelineBase timelineBase = new TimelineBase
		{
			From = ((fadeType == FadeType.FadeIn) ? 0.0 : 1.0),
			To = ((fadeType == FadeType.FadeIn) ? 1.0 : 0.0),
			BeginTime = TimeSpan.FromMilliseconds(beginTime),
			EndTime = TimeSpan.FromMilliseconds(endTime),
			AnimationTimeline = new DoubleAnimationTimeline()
		};
		_animationMaker.AddAnimation(target, PropertiesEnum.Opacity, timelineBase);
	}

	private void AddFadeOutAnimation(FrameworkElement target)
	{
		AddFadeAnimation(target, FadeType.FadeOut, 0, 100);
	}

	private void AddDelayedFadeInAnimation(FrameworkElement target)
	{
		AddFadeAnimation(target, FadeType.FadeIn, 101, 300);
	}

	private void AddRotationAnimation(FrameworkElement target, double fromAngle, double toAngle)
	{
		TimelineBase timelineBase = new TimelineBase
		{
			From = fromAngle,
			To = toAngle,
			BeginTime = TimeSpan.Zero,
			EndTime = TimeSpan.FromMilliseconds(300.0),
			KeySpline = new KeySpline
			{
				ControlPoint1 = new Point(0.22, 0.25),
				ControlPoint2 = new Point(0.0, 1.0)
			},
			AnimationTimeline = new DoubleAnimationTimeline()
		};
		_animationMaker.AddAnimation(target, "(UIElement.RenderTransform).(RotateTransform.Angle)", timelineBase);
	}
}
