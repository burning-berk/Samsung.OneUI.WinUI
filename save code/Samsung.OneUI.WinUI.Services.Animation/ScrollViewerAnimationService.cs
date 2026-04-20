using System;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Samsung.OneUI.WinUI.Services.Animation.Timelines;
using Windows.Foundation;

namespace Samsung.OneUI.WinUI.Services.Animation;

internal class ScrollViewerAnimationService : IScrollViewerAnimationService, IAnimationService
{
	private const int ANIMATION_START_TIME = 0;

	private const int ANIMATION_DELAY_TIME = 1000;

	private const int ANIMATION_DURATION = 350;

	private const double SYSTEM_SCROLL_SCALE = 0.333;

	private const double FAST_SCROLL_SCALE = 1.0;

	private const double SYSTEM_SCROLL_TRANSLATE = -4.0;

	private const double FAST_SCROLL_TRANSLATE = 0.0;

	private const double EASING_X1 = 0.22;

	private const double EASING_Y1 = 0.25;

	private const double EASING_X2 = 0.0;

	private const double EASING_Y2 = 1.0;

	private readonly CompositeTransform _verticalThumbTransform;

	private readonly CompositeTransform _horizontalThumbTransform;

	private readonly IAnimationMaker _animationMaker;

	public ScrollViewerAnimationService(CompositeTransform verticalThumbTransform, CompositeTransform horizontalThumbTransform)
	{
		_verticalThumbTransform = verticalThumbTransform;
		_horizontalThumbTransform = horizontalThumbTransform;
		_animationMaker = new AnimationMaker();
	}

	public void CreateFirstScrollAnimation()
	{
		Storyboard storyboard = _animationMaker.Create();
		CreateExpandScrollBarAnimation();
		_animationMaker.Execute();
		storyboard.Completed += delegate
		{
			_animationMaker.Create();
			CreateCollapseScrollBarAnimation();
			_animationMaker.Execute();
		};
	}

	public void CreateExpandScrollBarAnimation()
	{
		if (_horizontalThumbTransform != null)
		{
			AddExpandAnimation(_horizontalThumbTransform, PropertiesEnum.ScaleY, 0.333, 1.0, 0.0, 350.0);
			AddExpandAnimation(_horizontalThumbTransform, PropertiesEnum.TranslateY, -4.0, 0.0, 0.0, 350.0);
		}
		if (_verticalThumbTransform != null)
		{
			AddExpandAnimation(_verticalThumbTransform, PropertiesEnum.ScaleX, 0.333, 1.0, 0.0, 350.0);
			AddExpandAnimation(_verticalThumbTransform, PropertiesEnum.TranslateX, -4.0, 0.0, 0.0, 350.0);
		}
	}

	public void CreateCollapseScrollBarAnimation()
	{
		if (_horizontalThumbTransform != null)
		{
			AddExpandAnimation(_horizontalThumbTransform, PropertiesEnum.ScaleY, 1.0, 0.333, 1000.0, 350.0);
			AddExpandAnimation(_horizontalThumbTransform, PropertiesEnum.TranslateY, 0.0, -4.0, 1000.0, 350.0);
		}
		if (_verticalThumbTransform != null)
		{
			AddExpandAnimation(_verticalThumbTransform, PropertiesEnum.ScaleX, 1.0, 0.333, 1000.0, 350.0);
			AddExpandAnimation(_verticalThumbTransform, PropertiesEnum.TranslateX, 0.0, -4.0, 1000.0, 350.0);
		}
	}

	private void AddExpandAnimation(CompositeTransform target, PropertiesEnum propertiesEnum, double from, double to, double begintTime, double duration)
	{
		TimelineBase timelineBase = new TimelineBase
		{
			From = from,
			To = to,
			BeginTime = TimeSpan.FromMilliseconds(begintTime),
			EndTime = TimeSpan.FromMilliseconds(begintTime + duration),
			KeySpline = new KeySpline
			{
				ControlPoint1 = new Point(0.22, 0.25),
				ControlPoint2 = new Point(0.0, 1.0)
			},
			AnimationTimeline = new DoubleAnimationTimeline()
		};
		_animationMaker.AddAnimation(target, propertiesEnum, timelineBase);
	}
}
