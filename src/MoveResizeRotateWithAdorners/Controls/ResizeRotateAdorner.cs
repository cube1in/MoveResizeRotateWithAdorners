using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

// ReSharper disable once CheckNamespace
namespace MoveResizeRotateWithAdorners;

/// <summary>
/// 提供拖动手柄以调整和旋转项的装饰器
/// </summary>
[SuppressMessage("ReSharper", "IdentifierTypo")]
internal class ResizeRotateAdorner : Adorner
{
    private readonly VisualCollection _visuals;
    private readonly ResizeRotateChrome _chrome;

    protected override int VisualChildrenCount => _visuals.Count;

    public ResizeRotateAdorner(UIElement adornedElement)
        : base(adornedElement)
    {
        SnapsToDevicePixels = true;
        _chrome = new ResizeRotateChrome
        {
            DataContext = adornedElement
        };
        _visuals = new VisualCollection(this) {_chrome};
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        _chrome.Arrange(new Rect(finalSize));
        return finalSize;
    }

    protected override Visual GetVisualChild(int index)
    {
        return _visuals[index];
    }
}