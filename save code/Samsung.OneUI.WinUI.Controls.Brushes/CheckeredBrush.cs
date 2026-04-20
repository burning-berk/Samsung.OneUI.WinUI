using System;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Composition;
using Microsoft.Graphics.DirectX;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;

namespace Samsung.OneUI.WinUI.Controls.Brushes;

internal class CheckeredBrush : XamlCompositionBrushBase
{
	private struct CheckerSurfaceData
	{
		public int width;

		public int height;

		public Color backgroundColor;

		public Color rectColor;
	}

	private const int SURFACE_WIDTH = 10;

	private const int SURFACE_HEIGHT = 10;

	private static CompositionGraphicsDevice _graphicsDevice;

	private static CompositionEffectFactory _effectFactory;

	private CompositionBrush _checkeredBrush;

	private IDisposable _surfaceSource;

	public static readonly DependencyProperty BackgroundBrushProperty = DependencyProperty.Register("BackgroundBrush", typeof(SolidColorBrush), typeof(CheckeredBrush), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 252, 252, byte.MaxValue)), OnBackgroundBrushPropertyChanged));

	public static readonly DependencyProperty RectBrushProperty = DependencyProperty.Register("RectBrush", typeof(SolidColorBrush), typeof(CheckeredBrush), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 227, 227, 230)), OnRectBrushPropertyChanged));

	public SolidColorBrush BackgroundBrush
	{
		get
		{
			return (SolidColorBrush)GetValue(BackgroundBrushProperty);
		}
		set
		{
			SetValue(BackgroundBrushProperty, value);
		}
	}

	public SolidColorBrush RectBrush
	{
		get
		{
			return (SolidColorBrush)GetValue(RectBrushProperty);
		}
		set
		{
			SetValue(RectBrushProperty, value);
		}
	}

	private static void OnBackgroundBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is CheckeredBrush checkeredBrush)
		{
			checkeredBrush.UpdateCheckeredBrush();
		}
	}

	private static void OnRectBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is CheckeredBrush checkeredBrush)
		{
			checkeredBrush.UpdateCheckeredBrush();
		}
	}

	protected override void OnConnected()
	{
		base.OnConnected();
		if (base.CompositionBrush == null)
		{
			Initialize();
		}
	}

	protected override void OnDisconnected()
	{
		base.OnDisconnected();
		ClearResources();
	}

	private void Initialize()
	{
		Compositor compositorForCurrentThread = CompositionTarget.GetCompositorForCurrentThread();
		InitializeGraphicsDevice(compositorForCurrentThread);
		InitializeEffectFactory(compositorForCurrentThread);
		CheckerSurfaceData checkerSurfaceData = new CheckerSurfaceData
		{
			width = 10,
			height = 10,
			backgroundColor = BackgroundBrush.Color,
			rectColor = RectBrush.Color
		};
		RenderChecker(compositorForCurrentThread, checkerSurfaceData);
	}

	private static void InitializeGraphicsDevice(Compositor compositor)
	{
		if (_graphicsDevice == null)
		{
			CanvasDevice sharedDevice = CanvasDevice.GetSharedDevice();
			_graphicsDevice = CanvasComposition.CreateCompositionGraphicsDevice(compositor, sharedDevice);
		}
	}

	private static void InitializeEffectFactory(Compositor compositor)
	{
		if (_effectFactory == null)
		{
			using (BorderEffect graphicsEffect = new BorderEffect
			{
				Name = "BorderEffect",
				ExtendY = CanvasEdgeBehavior.Wrap,
				ExtendX = CanvasEdgeBehavior.Wrap,
				Source = new CompositionEffectSourceParameter("Source")
			})
			{
				_effectFactory = compositor.CreateEffectFactory(graphicsEffect);
			}
		}
	}

	private void RenderChecker(Compositor compositor, CheckerSurfaceData checkerSurfaceData)
	{
		ICompositionSurface compositionSurface = DrawCheckeredSurface(compositor, checkerSurfaceData);
		_surfaceSource?.Dispose();
		_surfaceSource = compositionSurface as IDisposable;
		_checkeredBrush?.Dispose();
		_checkeredBrush = CreateSurfaceBrush(compositor, compositionSurface);
		ApplyCheckeredBrush();
	}

	private ICompositionSurface DrawCheckeredSurface(Compositor compositor, CheckerSurfaceData checkerSurfaceData)
	{
		int width = checkerSurfaceData.width;
		int height = checkerSurfaceData.height;
		Color backgroundColor = checkerSurfaceData.backgroundColor;
		Color rectColor = checkerSurfaceData.rectColor;
		CompositionDrawingSurface compositionDrawingSurface = _graphicsDevice.CreateDrawingSurface(new Size(width, height), DirectXPixelFormat.B8G8R8A8UIntNormalized, DirectXAlphaMode.Premultiplied);
		using CanvasDrawingSession canvasDrawingSession = CanvasComposition.CreateDrawingSession(compositionDrawingSurface);
		canvasDrawingSession.Clear(backgroundColor);
		canvasDrawingSession.FillRectangle(new Rect(0f, 0f, width / 2, height / 2), rectColor);
		canvasDrawingSession.FillRectangle(new Rect(width / 2, height / 2, width / 2, height / 2), rectColor);
		return compositionDrawingSurface;
	}

	private CompositionBrush CreateSurfaceBrush(Compositor compositor, ICompositionSurface surface)
	{
		CompositionSurfaceBrush compositionSurfaceBrush = compositor.CreateSurfaceBrush(surface);
		compositionSurfaceBrush.Stretch = CompositionStretch.None;
		compositionSurfaceBrush.VerticalAlignmentRatio = 0f;
		compositionSurfaceBrush.HorizontalAlignmentRatio = 0f;
		return compositionSurfaceBrush;
	}

	private void ApplyCheckeredBrush()
	{
		if (base.CompositionBrush == null)
		{
			base.CompositionBrush = _effectFactory.CreateBrush();
		}
		if (base.CompositionBrush is CompositionEffectBrush compositionEffectBrush)
		{
			compositionEffectBrush.SetSourceParameter("Source", _checkeredBrush);
		}
	}

	private void UpdateCheckeredBrush()
	{
		if (base.CompositionBrush != null)
		{
			Compositor compositor = Window.Current.Compositor;
			CheckerSurfaceData checkerSurfaceData = new CheckerSurfaceData
			{
				width = 10,
				height = 10,
				backgroundColor = BackgroundBrush.Color,
				rectColor = RectBrush.Color
			};
			RenderChecker(compositor, checkerSurfaceData);
		}
	}

	private void ClearResources()
	{
		base.CompositionBrush?.Dispose();
		base.CompositionBrush = null;
		_checkeredBrush?.Dispose();
		_checkeredBrush = null;
		_surfaceSource?.Dispose();
		_surfaceSource = null;
	}
}
