using System.Windows;

namespace ToolIDE.MvvmSamples.Views
{
    public partial class NewPostWindow : Window
    {
        public NewPostWindow()
        {
            InitializeComponent();
        }
        
        private void OnOK(object sender, RoutedEventArgs e) {
            DialogResult = true;
            Close();
        }
    }
}