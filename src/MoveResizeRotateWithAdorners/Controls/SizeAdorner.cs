using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

// ReSharper disable once CheckNamespace
namespace MoveResizeRotateWithAdorners;

/// <summary>
/// 显示大小的装饰器
/// </summary>
[SuppressMessage("ReSharper", "IdentifierTypo")]
internal class SizeAdorner : Adorner
{
    private readonly VisualCollection _visuals;
    private readonly SizeChrome _chrome;

    protected override int VisualChildrenCount => _visuals.Count;

    public SizeAdorner(UIElement adornedElement)
        : base(adornedElement)
    {
        SnapsToDevicePixels = true;
        _chrome = new SizeChrome {DataContext = adornedElement};
        _visuals = new VisualCollection(this) {_chrome};
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        _chrome.Arrange(new Rect(new Point(0.0, 0.0), finalSize));
        return finalSize;
    }

    protected override Visual GetVisualChild(int index)
    {
        return _visuals[index];
    }
}