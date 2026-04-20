using System;
using System.Threading;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Samsung.OneUI.WinUI.Services.Animation;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class MultiPane : SplitView
{
	private const string MULTIPANE_MIN_WIDTH = "OneUIMultiPanePaneRootMinWidth";

	private const string ROOT_GRID = "RootGrid";

	private const string CONTENT_ROOT = "ContentRoot";

	private const string PANE_ROOT = "PaneRoot";

	private const string COLUMN_DEFINITION_1 = "ColumnDefinition1";

	private const string COLUMN_DEFINITION_2 = "ColumnDefinition2";

	private const string SPLITBAR_NAME = "SplitBar";

	private const string RESPONSIVITY_STATES_GROUP = "ResponsivityStates";

	private Grid _rootGrid;

	private Grid _contentRoot;

	private Grid _paneRoot;

	private double _multiPaneMinWidth;

	private ColumnDefinition _columnDefinition1;

	private ColumnDefinition _columnDefinition2;

	private readonly MultiPaneAnimationService _multiPaneAnimationService;

	private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

	private bool _isManipulationStarted;

	private double _dragGestureThresholdToOpenOrClosePane;

	private double _lastPaneLengthBeforeClose;

	private double _manipulationArea;

	private bool _isAnimationRunning;

	public MultiPane()
	{
		base.DefaultStyleKey = typeof(MultiPane);
		base.DisplayMode = SplitViewDisplayMode.Inline;
		_multiPaneAnimationService = new MultiPaneAnimationService();
		base.Loaded += MultiPane_Loaded;
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_rootGrid = GetTemplateChild("RootGrid") as Grid;
		_contentRoot = GetTemplateChild("ContentRoot") as Grid;
		_paneRoot = GetTemplateChild("PaneRoot") as Grid;
		_columnDefinition1 = GetTemplateChild("ColumnDefinition1") as ColumnDefinition;
		_columnDefinition2 = GetTemplateChild("ColumnDefinition2") as ColumnDefinition;
		if ("OneUIMultiPanePaneRootMinWidth".GetKey() is double multiPaneMinWidth)
		{
			_multiPaneMinWidth = multiPaneMinWidth;
		}
		SplitBar splitBar = GetTemplateChild("SplitBar") as SplitBar;
		VisualStateGroup visualStateGroup = GetTemplateChild("ResponsivityStates") as VisualStateGroup;
		if (visualStateGroup != null)
		{
			visualStateGroup.CurrentStateChanged += ResponsivityStateGroup_CurrentStateChanged;
		}
		if (splitBar != null)
		{
			splitBar.ManipulationDelta += MultiPaneManipulationDelta;
			splitBar.ManipulationCompleted += MultiPaneManipulationCompleted;
		}
	}

	public void OpenPane(bool isPaneOpen)
	{
		ChangeDisplayModeStates((!isPaneOpen) ? DisplayModeStates.OpenInlineLeft : DisplayModeStates.Closed);
	}

	private void ChangeDisplayModeStates(DisplayModeStates displayModeState)
	{
		switch (displayModeState)
		{
		case DisplayModeStates.Closed:
			ClosePaneAnimation();
			break;
		case DisplayModeStates.OpenInlineLeft:
			OpenPaneAnimation();
			break;
		}
		VisualStateManager.GoToState(this, displayModeState.ToString(), useTransitions: true);
	}

	private void ResponsivityStateGroup_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
	{
		if (base.IsPaneOpen && !IsAnyDefaultVariableNull())
		{
			UpdateLayout();
			base.OpenPaneLength = Math.Max(base.OpenPaneLength, _multiPaneMinWidth);
		}
	}

	private bool IsAnyDefaultVariableNull()
	{
		if (!IsRootOrPaneNull() && !IsAnyColumnNull())
		{
			return IsContentRootNull();
		}
		return true;
	}

	private bool IsRootOrPaneNull()
	{
		if (!(_rootGrid == null))
		{
			return _paneRoot == null;
		}
		return true;
	}

	private bool IsContentRootNull()
	{
		return _contentRoot == null;
	}

	private bool IsAnyColumnNull()
	{
		if (!(_columnDefinition1 == null))
		{
			return _columnDefinition2 == null;
		}
		return true;
	}

	private void UpdateMaxOpenPaneLength()
	{
		_columnDefinition1.MaxWidth = GetMaxOpenPaneLength();
	}

	private double GetMaxOpenPaneLength()
	{
		return _rootGrid.ActualWidth - _columnDefinition2.MinWidth;
	}

	private bool IsMovingLeft(double translationX)
	{
		return translationX < 0.0;
	}

	private bool IsMovingRight(double translationX)
	{
		return translationX > 0.0;
	}

	private void StartToClosePane(double deltaTranslationX)
	{
		deltaTranslationX = Math.Abs(deltaTranslationX);
		_manipulationArea -= deltaTranslationX;
		UpdatePaneLengthAfterManipulation();
	}

	private void UpdatePaneLengthAfterManipulation()
	{
		if (!_isAnimationRunning)
		{
			double maxOpenPaneLength = GetMaxOpenPaneLength();
			if (!IsAnimatingScene())
			{
				base.OpenPaneLength = Math.Min(_manipulationArea, maxOpenPaneLength);
			}
			_columnDefinition1.MinWidth = ((base.OpenPaneLength > maxOpenPaneLength) ? maxOpenPaneLength : base.OpenPaneLength);
		}
	}

	private void SetDefaultManipulationArea()
	{
		_manipulationArea = 0.0;
	}

	private bool IsAnimatingScene()
	{
		if (!IsValidDragGestureThresholdToClosePane(_manipulationArea))
		{
			return IsValidDragGestureThresholdToOpenPane(_manipulationArea);
		}
		return true;
	}

	private void StartToOpenPane(double deltaTranslationX)
	{
		deltaTranslationX = Math.Abs(deltaTranslationX);
		_manipulationArea += deltaTranslationX;
		UpdatePaneLengthAfterManipulation();
	}

	private bool CanOpenPane()
	{
		return base.OpenPaneLength <= GetMaxOpenPaneLength();
	}

	private void UpdateLastPaneLengthBeforeClose()
	{
		_lastPaneLengthBeforeClose = base.OpenPaneLength;
	}

	private bool IsValidDragGestureThresholdToOpenPane(double paneLength)
	{
		if (paneLength > _dragGestureThresholdToOpenOrClosePane)
		{
			return paneLength < _multiPaneMinWidth;
		}
		return false;
	}

	private bool IsValidDragGestureThresholdToClosePane(double paneLength)
	{
		return paneLength < _dragGestureThresholdToOpenOrClosePane;
	}

	private double CalculateDragThreshold()
	{
		return _multiPaneMinWidth / 2.0;
	}

	private void OpenPaneAnimation()
	{
		if (!IsAnyDefaultVariableNull())
		{
			UpdateLayout();
			SetDefaultManipulationArea();
			ExecuteOpenPaneAnimation();
		}
	}

	private void ClosePaneAnimation()
	{
		if (!IsAnyDefaultVariableNull())
		{
			UpdateLayout();
			SetDefaultManipulationArea();
			ExecuteClosePaneAnimation();
		}
	}

	private void ExecuteOpenPaneAnimation()
	{
		if (!base.IsPaneOpen)
		{
			base.IsPaneOpen = true;
			ExecutePaneAnimation(_lastPaneLengthBeforeClose);
		}
	}

	private void ExecuteClosePaneAnimation()
	{
		if (base.IsPaneOpen)
		{
			base.IsPaneOpen = false;
			ExecutePaneAnimation(0.0);
		}
	}

	private void ExecutePaneAnimation(double toPaneLength)
	{
		try
		{
			_semaphore.WaitAsync();
			_columnDefinition1.MinWidth = 0.0;
			UpdateMaxOpenPaneLength();
			_isAnimationRunning = true;
			_multiPaneAnimationService.ExecuteAnimation(this, base.OpenPaneLength, Math.Min(toPaneLength, _columnDefinition1.MaxWidth), delegate
			{
				_isAnimationRunning = false;
				UpdatePaneLengthAfterManipulation();
			});
		}
		finally
		{
			_semaphore.Release();
		}
	}

	private void MultiPane_Loaded(object sender, RoutedEventArgs e)
	{
		base.OpenPaneLength = _multiPaneMinWidth;
		_lastPaneLengthBeforeClose = base.OpenPaneLength;
		_dragGestureThresholdToOpenOrClosePane = CalculateDragThreshold();
	}

	private void MultiPaneManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
	{
		if (!_isManipulationStarted)
		{
			_manipulationArea = base.OpenPaneLength;
		}
		_isManipulationStarted = true;
		UpdateMaxOpenPaneLength();
		double x = e.Delta.Translation.X;
		if (IsMovingLeft(x))
		{
			StartToClosePane(x);
			if (IsValidDragGestureThresholdToClosePane(_manipulationArea))
			{
				ExecuteClosePaneAnimation();
			}
		}
		else if (IsMovingRight(x) && CanOpenPane())
		{
			StartToOpenPane(x);
			if (IsValidDragGestureThresholdToOpenPane(_manipulationArea))
			{
				_lastPaneLengthBeforeClose = _multiPaneMinWidth;
				ExecuteOpenPaneAnimation();
			}
		}
	}

	private void MultiPaneManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
	{
		if (_isManipulationStarted)
		{
			UpdatePaneLengthAfterManipulation();
			if (!IsValidDragGestureThresholdToOpenPane(base.OpenPaneLength) && !IsValidDragGestureThresholdToClosePane(base.OpenPaneLength))
			{
				UpdateLastPaneLengthBeforeClose();
			}
			_isManipulationStarted = false;
			if (IsPanelPartiallyOpened())
			{
				ExecuteOpenPaneAnimation();
			}
			else
			{
				ExecuteClosePaneAnimation();
			}
		}
		bool IsPanelPartiallyOpened()
		{
			return base.OpenPaneLength >= _multiPaneMinWidth / 2.0;
		}
	}
}
