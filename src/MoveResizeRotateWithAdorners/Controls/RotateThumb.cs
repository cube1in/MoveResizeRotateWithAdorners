using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

// ReSharper disable once CheckNamespace
namespace MoveResizeRotateWithAdorners;

/// <summary>
/// 旋转 Thumb
/// </summary>
internal class RotateThumb : Thumb
{
    #region Private Members

    /// <summary>
    /// 初始角度
    /// </summary>
    private double _initialAngle;

    /// <summary>
    /// 旋转变换
    /// </summary>
    private RotateTransform? _rotateTransform;

    /// <summary>
    /// 开始向量
    /// </summary>
    private Vector _startVector;

    /// <summary>
    /// 中心点
    /// </summary>
    private Point _centerPoint;

    /// <summary>
    /// DesignerItem
    /// </summary>
    private ContentControl? _designerItem;

    /// <summary>
    /// 画布
    /// </summary>
    private Canvas? _canvas;

    #endregion

    public RotateThumb()
    {
        DragDelta += HandleDragDelta;
        DragStarted += HandleDragStarted;
    }

    private void HandleDragStarted(object sender, DragStartedEventArgs e)
    {
        // DataContext is DesignItem
        _designerItem = DataContext as ContentControl;
        if (_designerItem != null)
        {
            _canvas = VisualTreeHelper.GetParent(_designerItem) as Canvas;
            if (_canvas != null)
            {
                _centerPoint = _designerItem.TranslatePoint(
                    new Point(_designerItem.Width * _designerItem.RenderTransformOrigin.X,
                        _designerItem.Height * _designerItem.RenderTransformOrigin.Y), _canvas);

                var startPoint = Mouse.GetPosition(_canvas);
                _startVector = Point.Subtract(startPoint, _centerPoint);

                _rotateTransform = _designerItem.RenderTransform as RotateTransform;
                if (_rotateTransform == null)
                {
                    _designerItem.RenderTransform = new RotateTransform(0);
                    _initialAngle = 0;
                }
                else
                    _initialAngle = _rotateTransform.Angle;
            }
        }
    }

    private void HandleDragDelta(object sender, DragDeltaEventArgs e)
    {
        if (_designerItem != null && _canvas != null)
        {
            var currentPoint = Mouse.GetPosition(_canvas);
            var deltaVector = Point.Subtract(currentPoint, _centerPoint);

            var angle = Vector.AngleBetween(_startVector, deltaVector);

            var rotateTransform = (_designerItem.RenderTransform as RotateTransform)!;
            rotateTransform.Angle = _initialAngle + Math.Round(angle, 0);
            _designerItem.InvalidateMeasure();
        }
    }
}