using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

// ReSharper disable once CheckNamespace
namespace MoveResizeRotateWithAdorners;

/// <summary>
/// 调整大小Thumb 只更新 DesignerItem 的宽度、高度和/或位置，具体取决于 ResizeThumb 的垂直和水平对齐方式。
/// </summary>
internal class ResizeThumb : Thumb
{
    public ResizeThumb()
    {
        DragDelta += HandleDragDelta;
    }

    private void HandleDragDelta(object? sender, DragDeltaEventArgs e)
    {
        // DataContext is DesignItem
        if (DataContext is ContentControl designerItem)
        {
            double deltaVertical, deltaHorizontal;
            switch (VerticalAlignment)
            {
                case VerticalAlignment.Bottom:
                    deltaVertical = Math.Min(-e.VerticalChange, designerItem.ActualHeight - designerItem.MinHeight);
                    designerItem.Height -= deltaVertical;
                    break;
                case VerticalAlignment.Top:
                    deltaVertical = Math.Min(e.VerticalChange, designerItem.ActualHeight - designerItem.MinHeight);
                    Canvas.SetTop(designerItem, Canvas.GetTop(designerItem) + deltaVertical);
                    designerItem.Height -= deltaVertical;
                    break;
            }

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    deltaHorizontal = Math.Min(e.HorizontalChange, designerItem.ActualWidth - designerItem.MinWidth);
                    Canvas.SetLeft(designerItem, Canvas.GetLeft(designerItem) + deltaHorizontal);
                    designerItem.Width -= deltaHorizontal;
                    break;
                case HorizontalAlignment.Right:
                    deltaHorizontal = Math.Min(-e.HorizontalChange, designerItem.ActualWidth - designerItem.MinWidth);
                    designerItem.Width -= deltaHorizontal;
                    break;
            }
        }

        e.Handled = true;
    }
}