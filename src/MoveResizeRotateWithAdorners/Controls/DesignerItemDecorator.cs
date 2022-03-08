using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

// ReSharper disable once CheckNamespace
namespace MoveResizeRotateWithAdorners;

/// <summary>
/// DesignerItem 装饰器
/// </summary>
[SuppressMessage("ReSharper", "IdentifierTypo")]
internal class DesignerItemDecorator : Control
{
    /// <summary>
    /// 装饰器
    /// </summary>
    private Adorner[]? _adorners;

    #region Dependency Properties Definitions

    /// <summary>
    /// 显示装饰器
    /// </summary>
    public static readonly DependencyProperty ShowDecoratorProperty = DependencyProperty.Register(nameof(ShowDecorator),
        typeof(bool), typeof(DesignerItemDecorator), new FrameworkPropertyMetadata(false, ShowDecoratorChanged));

    /// <summary>
    /// 显示装饰器
    /// </summary>
    public bool ShowDecorator
    {
        get => (bool) GetValue(ShowDecoratorProperty);
        set => SetValue(ShowDecoratorProperty, value);
    }

    /// <summary>
    /// ShowDecoratorChanged Callback
    /// </summary>
    /// <param name="d">DependencyObject</param>
    /// <param name="e">DependencyPropertyChangedEventArgs</param>
    private static void ShowDecoratorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var decorator = (DesignerItemDecorator) d;
        var showDecorator = (bool) e.NewValue;

        if (showDecorator) decorator.ShowAdorner();
        else decorator.HideAdorner();
    }

    #endregion

    /// <summary>
    /// 默认构造函数
    /// </summary>
    public DesignerItemDecorator()
    {
        Unloaded += HandleUnloaded;
    }

    #region Private Methods

    /// <summary>
    /// 处理卸载
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">RoutedEventArgs</param>
    private void HandleUnloaded(object sender, RoutedEventArgs e)
    {
        if (_adorners != null)
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(this);
            foreach (var adorner in _adorners)
            {
                adornerLayer?.Remove(adorner);
            }

            _adorners = null;
        }
    }

    /// <summary>
    /// 隐藏装饰器
    /// </summary>
    private void HideAdorner()
    {
        if (_adorners != null)
        {
            foreach (var adorner in _adorners)
            {
                adorner.Visibility = Visibility.Hidden;
            }
        }
    }

    /// <summary>
    /// 显示装饰器
    /// </summary>
    private void ShowAdorner()
    {
        if (_adorners == null)
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(this);
            if (adornerLayer != null)
            {
                if (DataContext is ContentControl designerItem && VisualTreeHelper.GetParent(designerItem) is Canvas)
                {
                    var resizeRotateAdorner = new ResizeRotateAdorner(designerItem);
                    var sizeAdorner = new SizeAdorner(designerItem);
                    adornerLayer.Add(resizeRotateAdorner);
                    adornerLayer.Add(sizeAdorner);

                    _adorners = new Adorner[] {resizeRotateAdorner, sizeAdorner};
                }
            }
        }

        if (_adorners != null)
        {
            foreach (var adorner in _adorners)
            {
                adorner.Visibility = Visibility.Visible;
            }
        }
    }

    #endregion
}