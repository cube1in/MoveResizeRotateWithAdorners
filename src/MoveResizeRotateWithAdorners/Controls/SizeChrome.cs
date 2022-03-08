using System.Windows;
using System.Windows.Controls;

// ReSharper disable once CheckNamespace
namespace MoveResizeRotateWithAdorners;

/// <summary>
/// 显示大小
/// </summary>
internal class SizeChrome : Control
{
    static SizeChrome()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(SizeChrome),
            new FrameworkPropertyMetadata(typeof(SizeChrome)));
    }
}