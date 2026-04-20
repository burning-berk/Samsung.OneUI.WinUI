using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Shapes;
using Windows.System;

namespace Samsung.OneUI.WinUI.Controls.Inputs.Slider.LevelBar;

internal class LevelSlider : Microsoft.UI.Xaml.Controls.Slider
{
	private const int DEFAULT_STEP_FREQUENCY = 1;

	private const string ITEMS_REPEATER_AREA = "itemsRepeaterArea";

	private const string ITEMS_REPEATER_HORIZONTAL = "itemsRepeaterHorizontal";

	private const string HORIZONTAL_STACK_LAYOUT = "HorizontalStackLayout";

	private const string HORIZONTAL_TRACK_RECT = "HorizontalTrackRect";

	private const string REPEATER_GRID_AREA = "RepeaterGridArea";

	private const int MARKER_CONTROL_SIZE = 8;

	private const int INTERVAL_CHECK = 5;

	private DispatcherTimer _tickCompleted;

	private bool _isTickAvailable;

	private ItemsRepeater _itemsRepeaterArea;

	private ItemsRepeater _itemsRepeaterHorizontal;

	private StackLayout _stackLayout;

	private ObservableCollection<double> _itemsStep;

	private Rectangle _horizontalTrackRect;

	private Grid _repeaterGridArea;

	private CustomSliderAutomationPeer _customSliderAutomationPeer;

	public static readonly DependencyProperty LevelsProperty = DependencyProperty.Register("Levels", typeof(int), typeof(LevelSlider), new PropertyMetadata(8, LevelsValueChanged));

	public int Levels
	{
		get
		{
			return (int)GetValue(LevelsProperty);
		}
		set
		{
			SetValue(LevelsProperty, value);
		}
	}

	public event RangeBaseValueChangedEventHandler ValueChangedEvent;

	private static void LevelsValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((LevelSlider)d).UpdateLevels();
	}

	public LevelSlider()
	{
		base.DefaultStyleKey = typeof(LevelSlider);
	}

	public void SetSliderValue(double value)
	{
		double num = 0.0;
		foreach (double item in _itemsStep)
		{
			if (Math.Abs(item - value) < Math.Abs(num - value))
			{
				num = item;
			}
		}
		base.Value = num;
	}

	private void SliderLevelBar_Loaded(object sender, RoutedEventArgs e)
	{
		_itemsRepeaterArea = GetTemplateChild("itemsRepeaterArea") as ItemsRepeater;
		if (_itemsRepeaterArea != null)
		{
			_itemsRepeaterArea.ItemsSource = GetItemsLevel(GetStepFrequency());
		}
		_itemsRepeaterHorizontal = GetTemplateChild("itemsRepeaterHorizontal") as ItemsRepeater;
		_stackLayout = GetTemplateChild("HorizontalStackLayout") as StackLayout;
		_horizontalTrackRect = GetTemplateChild("HorizontalTrackRect") as Rectangle;
		_repeaterGridArea = GetTemplateChild("RepeaterGridArea") as Grid;
		base.SizeChanged += LevelSlider_SizeChanged;
		UpdateLevels();
	}

	private void TickCompletedTimer_Tick(object sender, object e)
	{
		_tickCompleted.Stop();
		UpdateLevels();
		_isTickAvailable = false;
	}

	private void StartDispatcherTick()
	{
		_isTickAvailable = true;
		if (_tickCompleted == null)
		{
			_tickCompleted = new DispatcherTimer();
			_tickCompleted.Tick += TickCompletedTimer_Tick;
			_tickCompleted.Interval = TimeSpan.FromMilliseconds(5.0);
		}
		_tickCompleted.Start();
	}

	private void LevelSlider_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		UpdateLayout();
		if (_horizontalTrackRect == null)
		{
			return;
		}
		_horizontalTrackRect.Width = base.ActualWidth - _horizontalTrackRect.Margin.Left - _horizontalTrackRect.Margin.Right;
		if (!_isTickAvailable)
		{
			if (_tickCompleted != null)
			{
				_tickCompleted.Tick -= TickCompletedTimer_Tick;
				_tickCompleted = null;
			}
			StartDispatcherTick();
		}
	}

	private void LevelSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
	{
		double closestRange = GetClosestRange(base.Value);
		base.Value = closestRange;
		this.ValueChangedEvent?.Invoke(this, e);
	}

	private void UpdateLevels()
	{
		if (!(_itemsRepeaterHorizontal == null) && !(_stackLayout == null))
		{
			double stepFrequency = GetStepFrequency();
			_itemsStep = GetItemsLevel(stepFrequency);
			_itemsRepeaterHorizontal.ItemsSource = _itemsStep;
			_stackLayout.Spacing = SpacingFunction();
			double closestRange = GetClosestRange(base.Value);
			base.Value = closestRange;
		}
	}

	private double GetClosestRange(double value)
	{
		double result = value;
		if (_itemsStep != null)
		{
			result = _itemsStep.Aggregate((double x, double y) => (!(Math.Abs(x - value) < Math.Abs(y - value))) ? y : x);
		}
		return result;
	}

	private double SpacingFunction()
	{
		if (_repeaterGridArea != null && !double.IsNaN(_repeaterGridArea.ActualWidth))
		{
			return (_repeaterGridArea.ActualWidth - (double)(Levels * 8)) / (double)(Levels - 1);
		}
		return 0.0;
	}

	private ObservableCollection<double> GetItemsLevel(double stepFrequency)
	{
		double num = base.Minimum;
		ObservableCollection<double> observableCollection = new ObservableCollection<double>();
		for (int i = 0; i < Levels; i++)
		{
			observableCollection.Add(num);
			num += stepFrequency;
		}
		return observableCollection;
	}

	internal double GetStepFrequency()
	{
		double num = (base.Maximum - base.Minimum) / (double)(Levels - 1);
		if (num == 0.0)
		{
			num = 1.0;
		}
		if (num > 0.0 && num < 1.0)
		{
			base.StepFrequency = num;
		}
		else
		{
			base.StepFrequency = 1.0;
		}
		return num;
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		base.Loaded += SliderLevelBar_Loaded;
		base.ValueChanged += LevelSlider_ValueChanged;
	}

	protected override void OnPreviewKeyDown(KeyRoutedEventArgs e)
	{
		if (e.Key == VirtualKey.Down || e.Key == VirtualKey.Up || e.Key == VirtualKey.Left || e.Key == VirtualKey.Right)
		{
			e.Handled = true;
		}
		else
		{
			base.OnPreviewKeyDown(e);
		}
	}

	protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
	{
		base.OnMaximumChanged(oldMaximum, newMaximum);
		UpdateLevels();
	}

	protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
	{
		base.OnMinimumChanged(oldMinimum, newMinimum);
		UpdateLevels();
	}

	protected override AutomationPeer OnCreateAutomationPeer()
	{
		if (_customSliderAutomationPeer == null)
		{
			_customSliderAutomationPeer = new CustomSliderAutomationPeer(this, "LevelSlider");
		}
		return _customSliderAutomationPeer;
	}
}
