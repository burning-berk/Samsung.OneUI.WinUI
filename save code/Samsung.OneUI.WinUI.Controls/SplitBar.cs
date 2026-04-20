using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.System;
using Windows.UI.Core;
using WinRT;
using WinRT.Samsung_OneUI_WinUIVtableClasses;

namespace Samsung.OneUI.WinUI.Controls;

[WinRTRuntimeClassName("Microsoft.UI.Xaml.IUIElementOverrides")]
[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_DatePickerSpinnerListWinRTTypeDetails))]
public class SplitBar : Control
{
	public enum GridResizeDirection
	{
		Auto,
		Columns,
		Rows
	}

	public enum GridResizeBehavior
	{
		BasedOnAlignment,
		CurrentAndNext,
		PreviousAndCurrent,
		PreviousAndNext
	}

	public enum GripperCursorType
	{
		Default = -1,
		Arrow,
		Cross,
		Custom,
		Hand,
		Help,
		IBeam,
		SizeAll,
		SizeNortheastSouthwest,
		SizeNorthSouth,
		SizeNorthwestSoutheast,
		SizeWestEast,
		UniversalNo,
		UpArrow,
		Wait
	}

	public enum SplitterCursorBehavior
	{
		ChangeOnSplitterHover,
		ChangeOnGripperHover
	}

	private const string VISUAL_STATE_UNFOCUSED = "Unfocused";

	private const string VISUAL_STATE_FOCUSED = "Focused";

	private const string VISUAL_STATE_POINTOVER = "PointerOver";

	private const string VISUAL_STATE_NORMAL = "Normal";

	private const string VISUAL_STATE_PRESSED = "Pressed";

	private const string VISUAL_STATE_DISABLED = "Disabled";

	private const string VISUAL_STATE_RELEASED = "Released";

	private const string SPLIT_BAR = "DREAM_IDLE_TBOPT_SPLIT_BAR";

	internal const int NORMAL_THICKNESS = 2;

	internal const int FOCUSED_THICKNESS = 8;

	internal const int GripperCustomCursorDefaultResource = -1;

	internal static readonly CoreCursor ColumnsSplitterCursor = new CoreCursor(CoreCursorType.SizeWestEast, 1u);

	internal static readonly CoreCursor RowSplitterCursor = new CoreCursor(CoreCursorType.SizeNorthSouth, 1u);

	private GridResizeDirection _resizeDirection;

	private GridResizeBehavior _resizeBehavior;

	private GripperHoverWrapper _hoverWrapper;

	private TextBlock _gripperDisplay;

	private bool _pressed;

	private bool _dragging;

	private bool _pointerEntered;

	private bool _focused;

	private const string GripperDisplayFont = "Segoe MDL2 Assets";

	public static readonly DependencyProperty ElementProperty = DependencyProperty.Register("Element", typeof(UIElement), typeof(SplitBar), new PropertyMetadata(null, OnElementPropertyChanged));

	public static readonly DependencyProperty ResizeDirectionProperty = DependencyProperty.Register("ResizeDirection", typeof(GridResizeDirection), typeof(SplitBar), new PropertyMetadata(GridResizeDirection.Auto));

	public static readonly DependencyProperty ResizeBehaviorProperty = DependencyProperty.Register("ResizeBehavior", typeof(GridResizeBehavior), typeof(SplitBar), new PropertyMetadata(GridResizeBehavior.BasedOnAlignment));

	public static readonly DependencyProperty GripperForegroundProperty = DependencyProperty.Register("GripperForeground", typeof(Brush), typeof(SplitBar), new PropertyMetadata(null, OnGripperForegroundPropertyChanged));

	public static readonly DependencyProperty ParentLevelProperty = DependencyProperty.Register("ParentLevel", typeof(int), typeof(SplitBar), new PropertyMetadata(0));

	public static readonly DependencyProperty GripperCursorProperty = DependencyProperty.RegisterAttached("GripperCursor", typeof(CoreCursorType?), typeof(SplitBar), new PropertyMetadata(GripperCursorType.Default, OnGripperCursorPropertyChanged));

	public static readonly DependencyProperty GripperCustomCursorResourceProperty = DependencyProperty.RegisterAttached("GripperCustomCursorResource", typeof(uint), typeof(SplitBar), new PropertyMetadata(-1, GripperCustomCursorResourcePropertyChanged));

	public static readonly DependencyProperty CursorBehaviorProperty = DependencyProperty.RegisterAttached("CursorBehavior", typeof(SplitterCursorBehavior), typeof(SplitBar), new PropertyMetadata(SplitterCursorBehavior.ChangeOnSplitterHover, CursorBehaviorPropertyChanged));

	internal CoreCursor PreviousCursor { get; set; }

	private FrameworkElement TargetControl
	{
		get
		{
			if (ParentLevel == 0)
			{
				return this;
			}
			DependencyObject parent = base.Parent;
			for (int i = 2; i < ParentLevel; i++)
			{
				FrameworkElement frameworkElement = parent as FrameworkElement;
				if (frameworkElement != null)
				{
					parent = frameworkElement.Parent;
				}
			}
			return parent as FrameworkElement;
		}
	}

	private Grid Resizable => TargetControl?.Parent as Grid;

	private ColumnDefinition CurrentColumn
	{
		get
		{
			if (Resizable == null)
			{
				return null;
			}
			int targetedColumn = GetTargetedColumn();
			if (targetedColumn >= 0 && targetedColumn < Resizable.ColumnDefinitions.Count)
			{
				return Resizable.ColumnDefinitions[targetedColumn];
			}
			return null;
		}
	}

	private ColumnDefinition SiblingColumn
	{
		get
		{
			if (Resizable == null)
			{
				return null;
			}
			int siblingColumn = GetSiblingColumn();
			if (siblingColumn >= 0 && siblingColumn < Resizable.ColumnDefinitions.Count)
			{
				return Resizable.ColumnDefinitions[siblingColumn];
			}
			return null;
		}
	}

	private RowDefinition CurrentRow
	{
		get
		{
			if (Resizable == null)
			{
				return null;
			}
			int targetedRow = GetTargetedRow();
			if (targetedRow >= 0 && targetedRow < Resizable.RowDefinitions.Count)
			{
				return Resizable.RowDefinitions[targetedRow];
			}
			return null;
		}
	}

	private RowDefinition SiblingRow
	{
		get
		{
			if (Resizable == null)
			{
				return null;
			}
			int siblingRow = GetSiblingRow();
			if (siblingRow >= 0 && siblingRow < Resizable.RowDefinitions.Count)
			{
				return Resizable.RowDefinitions[siblingRow];
			}
			return null;
		}
	}

	public UIElement Element
	{
		get
		{
			return (UIElement)GetValue(ElementProperty);
		}
		set
		{
			SetValue(ElementProperty, value);
		}
	}

	public GridResizeDirection ResizeDirection
	{
		get
		{
			return (GridResizeDirection)GetValue(ResizeDirectionProperty);
		}
		set
		{
			SetValue(ResizeDirectionProperty, value);
		}
	}

	public GridResizeBehavior ResizeBehavior
	{
		get
		{
			return (GridResizeBehavior)GetValue(ResizeBehaviorProperty);
		}
		set
		{
			SetValue(ResizeBehaviorProperty, value);
		}
	}

	public Brush GripperForeground
	{
		get
		{
			return (Brush)GetValue(GripperForegroundProperty);
		}
		set
		{
			SetValue(GripperForegroundProperty, value);
		}
	}

	public int ParentLevel
	{
		get
		{
			return (int)GetValue(ParentLevelProperty);
		}
		set
		{
			SetValue(ParentLevelProperty, value);
		}
	}

	public GripperCursorType GripperCursor
	{
		get
		{
			return (GripperCursorType)GetValue(GripperCursorProperty);
		}
		set
		{
			SetValue(GripperCursorProperty, value);
		}
	}

	public int GripperCustomCursorResource
	{
		get
		{
			return (int)GetValue(GripperCustomCursorResourceProperty);
		}
		set
		{
			SetValue(GripperCustomCursorResourceProperty, value);
		}
	}

	public SplitterCursorBehavior CursorBehavior
	{
		get
		{
			return (SplitterCursorBehavior)GetValue(CursorBehaviorProperty);
		}
		set
		{
			SetValue(CursorBehaviorProperty, value);
		}
	}

	public SplitBar()
	{
		base.Loaded += GridSplitter_Loaded;
		AutomationProperties.SetName(this, "DREAM_IDLE_TBOPT_SPLIT_BAR".GetLocalized());
		AutomationProperties.SetLocalizedControlType(this, "DREAM_IDLE_TBOPT_SPLIT_BAR".GetLocalized());
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		base.Loaded -= GridSplitter_Loaded;
		base.PointerEntered -= GridSplitter_PointerEntered;
		base.PointerExited -= GridSplitter_PointerExited;
		base.PointerPressed -= GridSplitter_PointerPressed;
		base.PointerReleased -= GridSplitter_PointerReleased;
		base.GotFocus -= SplitBar_GotFocus;
		base.LostFocus -= SplitBar_LostFocus;
		base.IsEnabledChanged -= SplitBar_IsEnabledChanged;
		base.ManipulationStarted -= GridSplitter_ManipulationStarted;
		base.ManipulationCompleted -= GridSplitter_ManipulationCompleted;
		base.Loaded += GridSplitter_Loaded;
		base.PointerEntered += GridSplitter_PointerEntered;
		base.PointerExited += GridSplitter_PointerExited;
		base.PointerPressed += GridSplitter_PointerPressed;
		base.PointerReleased += GridSplitter_PointerReleased;
		base.GotFocus += SplitBar_GotFocus;
		base.LostFocus += SplitBar_LostFocus;
		base.IsEnabledChanged += SplitBar_IsEnabledChanged;
		base.ManipulationStarted += GridSplitter_ManipulationStarted;
		base.ManipulationCompleted += GridSplitter_ManipulationCompleted;
		base.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
	}

	private void SplitBar_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		SetDisabledVisualState();
	}

	private void SplitBar_LostFocus(object sender, RoutedEventArgs e)
	{
		_focused = false;
		SetSplitBarThickness(2);
		VisualStateManager.GoToState(this, "Unfocused", useTransitions: true);
	}

	private void SplitBar_GotFocus(object sender, RoutedEventArgs e)
	{
		if (base.FocusState == FocusState.Keyboard)
		{
			_focused = true;
			SetSplitBarThickness(8);
			VisualStateManager.GoToState(this, (base.FocusState == FocusState.Keyboard) ? "Focused" : "Unfocused", useTransitions: true);
		}
	}

	private void GridSplitter_PointerReleased(object sender, PointerRoutedEventArgs e)
	{
		_pressed = false;
		if (_pointerEntered && !_focused)
		{
			VisualStateManager.GoToState(this, "Released", useTransitions: true);
		}
		else if (!_focused)
		{
			VisualStateManager.GoToState(this, "Normal", useTransitions: true);
		}
	}

	private void GridSplitter_PointerPressed(object sender, PointerRoutedEventArgs e)
	{
		_pressed = true;
		VisualStateManager.GoToState(this, "Pressed", useTransitions: true);
	}

	private void GridSplitter_PointerExited(object sender, PointerRoutedEventArgs e)
	{
		_pointerEntered = false;
		if (!_pressed && !_dragging && !_focused)
		{
			VisualStateManager.GoToState(this, "Normal", useTransitions: true);
		}
	}

	private void GridSplitter_PointerEntered(object sender, PointerRoutedEventArgs e)
	{
		_pointerEntered = true;
		if (!_pressed && !_dragging && !_focused)
		{
			VisualStateManager.GoToState(this, "PointerOver", useTransitions: true);
		}
	}

	private void GridSplitter_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
	{
		_dragging = false;
		_pressed = false;
		if (_pointerEntered && !_focused)
		{
			VisualStateManager.GoToState(this, "PointerOver", useTransitions: true);
		}
		else if (!_focused)
		{
			VisualStateManager.GoToState(this, "Normal", useTransitions: true);
		}
	}

	private void GridSplitter_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
	{
		_dragging = true;
		VisualStateManager.GoToState(this, "Pressed", useTransitions: true);
	}

	private void GridSplitter_Loaded(object sender, RoutedEventArgs e)
	{
		SetSplitBarThickness(2);
		_resizeDirection = GetResizeDirection();
		_resizeBehavior = GetResizeBehavior();
		ChangeCursor(InputSystemCursor.Create((_resizeDirection == GridResizeDirection.Columns) ? InputSystemCursorShape.SizeWestEast : InputSystemCursorShape.SizeNorthSouth));
		if (Element == null)
		{
			CreateGripperDisplay();
			Element = _gripperDisplay;
		}
		if (_hoverWrapper == null)
		{
			GripperHoverWrapper hoverWrapper = new GripperHoverWrapper((CursorBehavior == SplitterCursorBehavior.ChangeOnSplitterHover) ? this : Element, _resizeDirection, GripperCursor, GripperCustomCursorResource);
			_hoverWrapper = hoverWrapper;
		}
		SetDisabledVisualState();
	}

	private void CreateGripperDisplay()
	{
		if (_gripperDisplay == null)
		{
			_gripperDisplay = new TextBlock
			{
				FontFamily = new FontFamily("Segoe MDL2 Assets"),
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
				Foreground = GripperForeground
			};
			_gripperDisplay.SetValue(AutomationProperties.AccessibilityViewProperty, AccessibilityView.Raw);
		}
	}

	protected override void OnKeyDown(KeyRoutedEventArgs e)
	{
		int num = 1;
		CoreVirtualKeyStates keyStateForCurrentThread = InputKeyboardSource.GetKeyStateForCurrentThread(VirtualKey.Control);
		if (keyStateForCurrentThread.HasFlag(CoreVirtualKeyStates.Down))
		{
			num = 5;
		}
		if (_resizeDirection == GridResizeDirection.Columns)
		{
			if (e.Key == VirtualKey.Left)
			{
				HorizontalMove(-num);
			}
			else
			{
				if (e.Key != VirtualKey.Right)
				{
					return;
				}
				HorizontalMove(num);
			}
			e.Handled = true;
			return;
		}
		if (_resizeDirection == GridResizeDirection.Rows)
		{
			if (e.Key == VirtualKey.Up)
			{
				VerticalMove(-num);
			}
			else
			{
				if (e.Key != VirtualKey.Down)
				{
					return;
				}
				VerticalMove(num);
			}
			e.Handled = true;
		}
		base.OnKeyDown(e);
	}

	protected override void OnManipulationStarted(ManipulationStartedRoutedEventArgs e)
	{
		_resizeDirection = GetResizeDirection();
		_resizeBehavior = GetResizeBehavior();
		if (_resizeDirection != GridResizeDirection.Columns)
		{
			_ = _resizeDirection;
			_ = 2;
		}
		base.OnManipulationStarted(e);
	}

	protected override void OnManipulationCompleted(ManipulationCompletedRoutedEventArgs e)
	{
		base.OnManipulationCompleted(e);
	}

	protected override void OnManipulationDelta(ManipulationDeltaRoutedEventArgs e)
	{
		double num = e.Delta.Translation.X;
		double y = e.Delta.Translation.Y;
		if (base.FlowDirection == FlowDirection.RightToLeft)
		{
			num *= -1.0;
		}
		if ((_resizeDirection != GridResizeDirection.Columns || !HorizontalMove(num)) && (_resizeDirection != GridResizeDirection.Rows || !VerticalMove(y)))
		{
			base.OnManipulationDelta(e);
		}
	}

	private bool VerticalMove(double verticalChange)
	{
		if (CurrentRow == null || SiblingRow == null)
		{
			return true;
		}
		if (!IsStarRow(CurrentRow))
		{
			if (!SetRowHeight(CurrentRow, verticalChange, GridUnitType.Pixel))
			{
				return true;
			}
		}
		else if (!IsStarRow(SiblingRow))
		{
			if (!IsValidRowHeight(CurrentRow, verticalChange))
			{
				return false;
			}
			if (!SetRowHeight(SiblingRow, verticalChange * -1.0, GridUnitType.Pixel))
			{
				return true;
			}
		}
		else
		{
			if (!IsValidRowHeight(CurrentRow, verticalChange) || !IsValidRowHeight(SiblingRow, verticalChange * -1.0))
			{
				return true;
			}
			DefineRowHeight(verticalChange);
		}
		return false;
	}

	private bool HorizontalMove(double horizontalChange)
	{
		if (CurrentColumn == null || SiblingColumn == null)
		{
			return true;
		}
		if (!IsStarColumn(CurrentColumn))
		{
			if (!SetColumnWidth(CurrentColumn, horizontalChange, GridUnitType.Pixel))
			{
				return true;
			}
		}
		else if (!IsStarColumn(SiblingColumn))
		{
			if (!IsValidColumnWidth(CurrentColumn, horizontalChange))
			{
				return false;
			}
			if (!SetColumnWidth(SiblingColumn, horizontalChange * -1.0, GridUnitType.Pixel))
			{
				return true;
			}
		}
		else
		{
			if (!IsValidColumnWidth(CurrentColumn, horizontalChange) || !IsValidColumnWidth(SiblingColumn, horizontalChange * -1.0))
			{
				return true;
			}
			DefineColumnWidth(horizontalChange);
		}
		return false;
	}

	private void DefineRowHeight(double verticalChange)
	{
		foreach (RowDefinition item in Resizable?.RowDefinitions)
		{
			if (CurrentRow != null && item == CurrentRow)
			{
				SetRowHeight(CurrentRow, verticalChange, GridUnitType.Star);
			}
			else if (SiblingRow != null && item == SiblingRow)
			{
				SetRowHeight(SiblingRow, verticalChange * -1.0, GridUnitType.Star);
			}
			else if (IsStarRow(item))
			{
				item.Height = new GridLength(item.ActualHeight, GridUnitType.Star);
			}
		}
	}

	private void DefineColumnWidth(double horizontalChange)
	{
		foreach (ColumnDefinition item in Resizable?.ColumnDefinitions)
		{
			if (CurrentColumn != null && item == CurrentColumn)
			{
				SetColumnWidth(CurrentColumn, horizontalChange, GridUnitType.Star);
			}
			else if (SiblingColumn != null && item == SiblingColumn)
			{
				SetColumnWidth(SiblingColumn, horizontalChange * -1.0, GridUnitType.Star);
			}
			else if (IsStarColumn(item))
			{
				item.Width = new GridLength(item.ActualWidth, GridUnitType.Star);
			}
		}
	}

	public void ChangeCursor(InputCursor cursor)
	{
		base.ProtectedCursor = cursor;
	}

	private static bool IsStarColumn(ColumnDefinition definition)
	{
		return ((GridLength)definition.GetValue(ColumnDefinition.WidthProperty)).IsStar;
	}

	private static bool IsStarRow(RowDefinition definition)
	{
		return ((GridLength)definition.GetValue(RowDefinition.HeightProperty)).IsStar;
	}

	private bool SetColumnWidth(ColumnDefinition columnDefinition, double horizontalChange, GridUnitType unitType)
	{
		double num = columnDefinition.ActualWidth + horizontalChange;
		double minWidth = columnDefinition.MinWidth;
		if (!double.IsNaN(minWidth) && num < minWidth)
		{
			num = minWidth;
		}
		double maxWidth = columnDefinition.MaxWidth;
		if (!double.IsNaN(maxWidth) && num > maxWidth)
		{
			num = maxWidth;
		}
		if (num > base.ActualWidth)
		{
			columnDefinition.Width = new GridLength(num, unitType);
			return true;
		}
		return false;
	}

	private bool IsValidColumnWidth(ColumnDefinition columnDefinition, double horizontalChange)
	{
		double num = columnDefinition.ActualWidth + horizontalChange;
		double minWidth = columnDefinition.MinWidth;
		if (!double.IsNaN(minWidth) && num < minWidth)
		{
			return false;
		}
		double maxWidth = columnDefinition.MaxWidth;
		if (!double.IsNaN(maxWidth) && num > maxWidth)
		{
			return false;
		}
		if (num <= base.ActualWidth)
		{
			return false;
		}
		return true;
	}

	private bool SetRowHeight(RowDefinition rowDefinition, double verticalChange, GridUnitType unitType)
	{
		double num = rowDefinition.ActualHeight + verticalChange;
		double minHeight = rowDefinition.MinHeight;
		if (!double.IsNaN(minHeight) && num < minHeight)
		{
			num = minHeight;
		}
		double maxHeight = rowDefinition.MaxHeight;
		if (!double.IsNaN(maxHeight) && num > maxHeight)
		{
			num = maxHeight;
		}
		if (num > base.ActualHeight)
		{
			rowDefinition.Height = new GridLength(num, unitType);
			return true;
		}
		return false;
	}

	private bool IsValidRowHeight(RowDefinition rowDefinition, double verticalChange)
	{
		double num = rowDefinition.ActualHeight + verticalChange;
		double minHeight = rowDefinition.MinHeight;
		if (!double.IsNaN(minHeight) && num < minHeight)
		{
			return false;
		}
		double maxHeight = rowDefinition.MaxHeight;
		if (!double.IsNaN(maxHeight) && num > maxHeight)
		{
			return false;
		}
		if (num <= base.ActualHeight)
		{
			return false;
		}
		return true;
	}

	private int GetTargetedColumn()
	{
		int column = Grid.GetColumn(TargetControl);
		return GetTargetIndex(column);
	}

	private int GetTargetedRow()
	{
		int row = Grid.GetRow(TargetControl);
		return GetTargetIndex(row);
	}

	private int GetSiblingColumn()
	{
		int column = Grid.GetColumn(TargetControl);
		return GetSiblingIndex(column);
	}

	private int GetSiblingRow()
	{
		int row = Grid.GetRow(TargetControl);
		return GetSiblingIndex(row);
	}

	private int GetTargetIndex(int currentIndex)
	{
		return _resizeBehavior switch
		{
			GridResizeBehavior.CurrentAndNext => currentIndex, 
			GridResizeBehavior.PreviousAndNext => currentIndex - 1, 
			GridResizeBehavior.PreviousAndCurrent => currentIndex - 1, 
			_ => -1, 
		};
	}

	private int GetSiblingIndex(int currentIndex)
	{
		return _resizeBehavior switch
		{
			GridResizeBehavior.CurrentAndNext => currentIndex + 1, 
			GridResizeBehavior.PreviousAndNext => currentIndex + 1, 
			GridResizeBehavior.PreviousAndCurrent => currentIndex, 
			_ => -1, 
		};
	}

	private GridResizeDirection GetResizeDirection()
	{
		GridResizeDirection gridResizeDirection = ResizeDirection;
		if (gridResizeDirection == GridResizeDirection.Auto)
		{
			gridResizeDirection = ((base.HorizontalAlignment != HorizontalAlignment.Stretch) ? GridResizeDirection.Columns : ((base.VerticalAlignment != VerticalAlignment.Stretch) ? GridResizeDirection.Rows : ((base.ActualWidth <= base.ActualHeight) ? GridResizeDirection.Columns : GridResizeDirection.Rows)));
		}
		return gridResizeDirection;
	}

	private GridResizeBehavior GetResizeBehavior()
	{
		GridResizeBehavior gridResizeBehavior = ResizeBehavior;
		if (gridResizeBehavior == GridResizeBehavior.BasedOnAlignment)
		{
			gridResizeBehavior = ((_resizeDirection != GridResizeDirection.Columns) ? SetVerticalBehavior() : SetHorizontalBehavior());
		}
		return gridResizeBehavior;
	}

	private GridResizeBehavior SetVerticalBehavior()
	{
		return base.VerticalAlignment switch
		{
			VerticalAlignment.Top => GridResizeBehavior.PreviousAndCurrent, 
			VerticalAlignment.Bottom => GridResizeBehavior.CurrentAndNext, 
			_ => GridResizeBehavior.PreviousAndNext, 
		};
	}

	private GridResizeBehavior SetHorizontalBehavior()
	{
		return base.HorizontalAlignment switch
		{
			HorizontalAlignment.Left => GridResizeBehavior.PreviousAndCurrent, 
			HorizontalAlignment.Right => GridResizeBehavior.CurrentAndNext, 
			_ => GridResizeBehavior.PreviousAndNext, 
		};
	}

	private void SetDisabledVisualState()
	{
		VisualStateManager.GoToState(this, base.IsEnabled ? "Normal" : "Disabled", useTransitions: true);
	}

	private void SetSplitBarThickness(int thicknessValue)
	{
		if (base.HorizontalAlignment != HorizontalAlignment.Stretch)
		{
			base.Width = thicknessValue;
		}
		else if (base.VerticalAlignment != VerticalAlignment.Stretch)
		{
			base.Height = thicknessValue;
		}
	}

	private static void OnGripperForegroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		SplitBar splitBar = (SplitBar)d;
		if (!(splitBar._gripperDisplay == null))
		{
			splitBar._gripperDisplay.Foreground = splitBar.GripperForeground;
		}
	}

	private static void OnGripperCursorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		SplitBar splitBar = (SplitBar)d;
		if (splitBar._hoverWrapper != null)
		{
			splitBar._hoverWrapper.GripperCursor = splitBar.GripperCursor;
		}
	}

	private static void GripperCustomCursorResourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		SplitBar splitBar = (SplitBar)d;
		if (splitBar._hoverWrapper != null)
		{
			splitBar._hoverWrapper.GripperCustomCursorResource = splitBar.GripperCustomCursorResource;
		}
	}

	private static void CursorBehaviorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		_ = (SplitBar)d;
	}

	private static void OnElementPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		_ = (SplitBar)d;
	}
}
