using System.Windows;

namespace ToolIDE.MvvmSamples.Views
{
    public partial class NewCommentWindow : Window
    {
        public NewCommentWindow()
        {
            InitializeComponent();
        }

        private void OnOK(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}