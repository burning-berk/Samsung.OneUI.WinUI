using System;
using System.Numerics;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;

namespace Samsung.OneUI.WinUI.Services.Animation;

internal class PageIndicatorAnimationService : IPageIndicatorAnimationService, IAnimationService
{
	private Compositor _compositor;

	private CubicBezierEasingFunction _longDurationCubicBezierEasingFunction;

	private CubicBezierEasingFunction _shortDurationCubicBezierEasingFunction;

	private const string COLOR = "Color";

	private const int LONG_DURATION = 350;

	private const int SHORT_DURATION = 200;

	public void RunColorChangedAnimation(int pipIndex, Panel indicatorContainer, Color beginColor, Color endColor)
	{
		if (!(_compositor == null) && pipIndex >= 0 && pipIndex < indicatorContainer.Children.Count)
		{
			Ellipse ellipse = FindEllipseInPip(indicatorContainer.Children[pipIndex]);
			if (!(ellipse == null))
			{
				CompositionColorBrush compositionColorBrush = _compositor.CreateColorBrush(beginColor);
				ColorKeyFrameAnimation animation = CreateColorPipAnimation(compositionColorBrush, endColor, ellipse);
				CompositionScopedBatch compositionScopedBatch = _compositor.CreateScopedBatch(CompositionBatchTypes.Animation);
				compositionColorBrush.StartAnimation("Color", animation);
				compositionScopedBatch.End();
			}
		}
	}

	public void InitializeCompositor(Compositor compositor)
	{
		if (!(compositor == null))
		{
			_compositor = compositor;
			_longDurationCubicBezierEasingFunction = _compositor.CreateCubicBezierEasingFunction(new Vector2(0.22f, 0.25f), new Vector2(0f, 1f));
			_shortDurationCubicBezierEasingFunction = _compositor.CreateCubicBezierEasingFunction(new Vector2(0.33f, 1f), new Vector2(0.68f, 1f));
		}
	}

	public static Ellipse FindEllipseInPip(UIElement pageIndicatorPip)
	{
		if (!(pageIndicatorPip is Button))
		{
			return null;
		}
		Grid grid = VisualTreeHelper.GetChild(pageIndicatorPip as Button, 0) as Grid;
		if (grid == null)
		{
			return null;
		}
		return VisualTreeHelper.GetChild(grid, 0) as Ellipse;
	}

	public Vector3KeyFrameAnimation CreateScalePipAnimation(Visual visual, Ellipse ellipse, float scale)
	{
		if (visual == null || ellipse == null || _longDurationCubicBezierEasingFunction == null || _compositor == null)
		{
			return null;
		}
		Vector3KeyFrameAnimation vector3KeyFrameAnimation = _compositor.CreateVector3KeyFrameAnimation();
		vector3KeyFrameAnimation.Duration = TimeSpan.FromMilliseconds(350.0);
		vector3KeyFrameAnimation.InsertKeyFrame(0f, new Vector3(1f, 1f, 1f));
		vector3KeyFrameAnimation.InsertKeyFrame(1f, new Vector3(scale, scale, 1f), _longDurationCubicBezierEasingFunction);
		visual.CenterPoint = new Vector3((float)(ellipse.ActualWidth / 2.0), (float)(ellipse.ActualHeight / 2.0), 0f);
		return vector3KeyFrameAnimation;
	}

	public ScalarKeyFrameAnimation CreateOffsetAnimation(Visual visual, double newOffset)
	{
		if (visual == null || _longDurationCubicBezierEasingFunction == null || _compositor == null)
		{
			return null;
		}
		Vector3 offset = visual.Offset;
		ScalarKeyFrameAnimation scalarKeyFrameAnimation = _compositor.CreateScalarKeyFrameAnimation();
		scalarKeyFrameAnimation.Duration = TimeSpan.FromMilliseconds(350.0);
		scalarKeyFrameAnimation.InsertKeyFrame(1f, offset.X + (float)newOffset, _longDurationCubicBezierEasingFunction);
		return scalarKeyFrameAnimation;
	}

	public ScalarKeyFrameAnimation CreateOpacityPipAnimation(double opacity)
	{
		if (_compositor == null || _shortDurationCubicBezierEasingFunction == null)
		{
			return null;
		}
		ScalarKeyFrameAnimation scalarKeyFrameAnimation = _compositor.CreateScalarKeyFrameAnimation();
		scalarKeyFrameAnimation.Duration = TimeSpan.FromMilliseconds(200.0);
		scalarKeyFrameAnimation.InsertKeyFrame(0f, ((float)opacity != 1f) ? 1 : 0, _shortDurationCubicBezierEasingFunction);
		scalarKeyFrameAnimation.InsertKeyFrame(1f, (float)opacity, _shortDurationCubicBezierEasingFunction);
		return scalarKeyFrameAnimation;
	}

	public ColorKeyFrameAnimation CreateColorPipAnimation(CompositionColorBrush brush, Color color, Ellipse ellipse)
	{
		if (ellipse == null || brush == null || _compositor == null || _shortDurationCubicBezierEasingFunction == null)
		{
			return null;
		}
		SpriteVisual visual = CreateEllipseVisual(brush, color, ellipse);
		ElementCompositionPreview.SetElementChildVisual(ellipse, visual);
		ColorKeyFrameAnimation colorKeyFrameAnimation = _compositor.CreateColorKeyFrameAnimation();
		colorKeyFrameAnimation.InsertKeyFrame(1f, color, _shortDurationCubicBezierEasingFunction);
		colorKeyFrameAnimation.Duration = TimeSpan.FromMilliseconds(200.0);
		return colorKeyFrameAnimation;
	}

	public SpriteVisual CreateEllipseVisual(CompositionColorBrush brush, Color color, Ellipse ellipse)
	{
		if (ellipse == null || brush == null || _compositor == null)
		{
			return null;
		}
		SpriteVisual spriteVisual = _compositor.CreateSpriteVisual();
		spriteVisual.Size = new Vector2((float)ellipse.ActualWidth, (float)ellipse.ActualHeight);
		spriteVisual.Brush = brush;
		CompositionEllipseGeometry compositionEllipseGeometry = _compositor.CreateEllipseGeometry();
		compositionEllipseGeometry.Radius = new Vector2((float)ellipse.ActualWidth / 2f, (float)ellipse.ActualHeight / 2f);
		compositionEllipseGeometry.Center = new Vector2((float)ellipse.ActualWidth / 2f, (float)ellipse.ActualHeight / 2f);
		CompositionGeometricClip clip = _compositor.CreateGeometricClip(compositionEllipseGeometry);
		spriteVisual.Clip = clip;
		return spriteVisual;
	}
}
