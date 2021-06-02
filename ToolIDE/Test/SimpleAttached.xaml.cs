using System.Windows;
using System.Windows.Controls;

namespace ToolIDE.Test
{
    public partial class SimpleAttached : Window
    {
        public SimpleAttached()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Canvas.SetLeft(_rect, Canvas.GetLeft(_rect) + 5);
        }
    }
}