using System.Numerics;
using CommunityToolkit.WinUI.Helpers;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;

namespace Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker;

internal class OneUIColorSpectrum : ColorSpectrum
{
	private const float DEFAULT_OPACITY = 1f;

	private const float DEFAULT_SATURATION = 1f;

	private const float FAST_SPEED = 0.03f;

	private const float REGULAR_SPEED = 0.005f;

	private const string FILL_ELLIPSE_COMPONENT_ID = "FillEllipse";

	private Ellipse fillEllipse;

	private SolidColorBrush fillEllipseBrush;

	public OneUIColorSpectrum()
	{
		base.Loaded += OneUIColorSpectrum_Loaded;
		base.Unloaded += OneUIColorSpectrum_Unloaded;
		RegisterPropertyChangedCallback(ColorSpectrum.HsvColorProperty, OnHsvColorPropertyChanged);
	}

	private void OneUIColorSpectrum_Loaded(object sender, RoutedEventArgs e)
	{
		base.PreviewKeyDown += OnKeyDownAction;
		OneUIColorSpectrum oneUIColorSpectrum = (OneUIColorSpectrum)sender;
		fillEllipse = (Ellipse)oneUIColorSpectrum.GetTemplateChild("FillEllipse");
		UpdateFillEllipse(oneUIColorSpectrum);
	}

	private void OnHsvColorPropertyChanged(DependencyObject sender, DependencyProperty dp)
	{
		UpdateFillEllipse(sender as OneUIColorSpectrum);
	}

	private void OneUIColorSpectrum_Unloaded(object sender, RoutedEventArgs e)
	{
		base.PreviewKeyDown -= OnKeyDownAction;
	}

	private void OnKeyDownAction(object sender, KeyRoutedEventArgs e)
	{
		float speed = 0.005f;
		if (InputKeyboardSource.GetKeyStateForCurrentThread(VirtualKey.Control).HasFlag(CoreVirtualKeyStates.Down))
		{
			speed = 0.03f;
		}
		InvertingControlDirection(e, speed, handleKeyDown: true);
	}

	private void UpdateFillEllipse(OneUIColorSpectrum spectrum)
	{
		if (fillEllipse != null && spectrum != null)
		{
			Color color = FromHSVToColor(spectrum.HsvColor, ignoreSaturation: true, ignoreOpacity: true);
			if (fillEllipseBrush == null)
			{
				fillEllipseBrush = new SolidColorBrush(color);
			}
			else
			{
				fillEllipseBrush.Color = color;
			}
			fillEllipse.Fill = fillEllipseBrush;
		}
	}

	private Color FromHSVToColor(Vector4 hsvColor, bool ignoreSaturation = false, bool ignoreOpacity = false)
	{
		if (ignoreSaturation)
		{
			hsvColor.Z = 1f;
		}
		if (ignoreOpacity)
		{
			hsvColor.W = 1f;
		}
		return ColorHelper.FromHsv(hsvColor.X, hsvColor.Y, hsvColor.Z, hsvColor.W);
	}

	private void InvertingControlDirection(KeyRoutedEventArgs e, float speed, bool handleKeyDown)
	{
		if (e.Key == VirtualKey.Up)
		{
			Vector4 hsvColor = Vector4.Subtract(base.HsvColor, new Vector4(0f, speed, 0f, 0f));
			if (hsvColor.Y < 0f)
			{
				hsvColor = new Vector4(hsvColor.X, 1f, hsvColor.Z, hsvColor.W);
			}
			base.HsvColor = hsvColor;
			if (handleKeyDown)
			{
				e.Handled = true;
			}
		}
		else if (e.Key == VirtualKey.Down)
		{
			Vector4 hsvColor2 = Vector4.Add(base.HsvColor, new Vector4(0f, speed, 0f, 0f));
			if (hsvColor2.Y > 1f)
			{
				hsvColor2 = new Vector4(hsvColor2.X, 0f, hsvColor2.Z, hsvColor2.W);
			}
			base.HsvColor = hsvColor2;
			if (handleKeyDown)
			{
				e.Handled = true;
			}
		}
	}
}
