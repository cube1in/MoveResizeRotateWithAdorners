using System.Windows.Controls;
using System.Windows.Controls.Primitives;

// ReSharper disable once CheckNamespace
namespace MoveResizeRotateWithAdorners;

/// <summary>
/// 拖拽移动Thumb
/// </summary>
internal class MoveThumb : Thumb
{
    public MoveThumb()
    {
        DragDelta += HandleDragDelta;
    }

    private void HandleDragDelta(object? sender, DragDeltaEventArgs e)
    {
        // DataContext is DesignerItem
        if (DataContext is ContentControl designerItem)
        {
            var left = Canvas.GetLeft(designerItem);
            var top = Canvas.GetTop(designerItem);

            Canvas.SetLeft(designerItem, left + e.HorizontalChange);
            Canvas.SetTop(designerItem, top + e.VerticalChange);
        }
    }
}