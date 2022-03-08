using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MoveResizeRotateWithAdorners
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox {IsChecked: true})
            {
                foreach (Control child in DesignerCanvas.Children)
                {
                    Selector.SetIsSelected(child, true);
                }
            }
            else
            {
                foreach (Control child in DesignerCanvas.Children)
                {
                    Selector.SetIsSelected(child, false);
                }
            }
        }
    }
}