using System.Windows;

namespace ToolIDE.Test
{
    public partial class Menus : Window
    {
        public Menus()
        {
            InitializeComponent();
        }

        private void OnExit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}