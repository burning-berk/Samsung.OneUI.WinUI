using Microsoft.UI.Composition;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;

namespace Samsung.OneUI.WinUI.Services.Animation;

internal interface IPageIndicatorAnimationService : IAnimationService
{
	void RunColorChangedAnimation(int pipIndex, Panel indicatorContainer, Color beginColor, Color endColor);

	void InitializeCompositor(Compositor compositor);

	Vector3KeyFrameAnimation CreateScalePipAnimation(Visual visual, Ellipse ellipse, float scale);

	ScalarKeyFrameAnimation CreateOffsetAnimation(Visual visual, double newOffset);

	ScalarKeyFrameAnimation CreateOpacityPipAnimation(double opacity);

	ColorKeyFrameAnimation CreateColorPipAnimation(CompositionColorBrush brush, Color color, Ellipse ellipse);

	SpriteVisual CreateEllipseVisual(CompositionColorBrush brush, Color color, Ellipse ellipse);
}
