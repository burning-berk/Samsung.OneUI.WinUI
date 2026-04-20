using Microsoft.UI.Xaml;
using Windows.UI.Core;

namespace Samsung.OneUI.WinUI.Controls;

internal class GripperHoverWrapper
{
	private readonly SplitBar.GridResizeDirection _gridSplitterDirection;

	private readonly CoreCursor _splitterPreviousPointer;

	private readonly CoreCursor _previousCursor;

	private SplitBar.GripperCursorType _gripperCursor;

	private int _gripperCustomCursorResource;

	private readonly bool _isDragging;

	private readonly UIElement _element;

	internal SplitBar.GripperCursorType GripperCursor
	{
		get
		{
			return _gripperCursor;
		}
		set
		{
			_gripperCursor = value;
		}
	}

	internal int GripperCustomCursorResource
	{
		get
		{
			return _gripperCustomCursorResource;
		}
		set
		{
			_gripperCustomCursorResource = value;
		}
	}

	internal GripperHoverWrapper(UIElement element, SplitBar.GridResizeDirection gridSplitterDirection, SplitBar.GripperCursorType gripperCursor, int gripperCustomCursorResource)
	{
		_gridSplitterDirection = gridSplitterDirection;
		_gripperCursor = gripperCursor;
		_gripperCustomCursorResource = gripperCustomCursorResource;
		_element = element;
	}
}
