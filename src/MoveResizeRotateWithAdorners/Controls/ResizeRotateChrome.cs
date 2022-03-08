using System.Windows;
using System.Windows.Controls;

// ReSharper disable once CheckNamespace
namespace MoveResizeRotateWithAdorners;

/// <summary>
/// 提供拖动手柄以调整和旋转项
/// </summary>
internal class ResizeRotateChrome : Control
{
    static ResizeRotateChrome()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizeRotateChrome),
            new FrameworkPropertyMetadata(typeof(ResizeRotateChrome)));
    }
}