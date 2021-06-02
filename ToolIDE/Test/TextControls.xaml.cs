using System;
using System.Windows;

namespace ToolIDE.Test
{
    public partial class TextControls : Window
    {
        public TextControls()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"User: {_name.Text}, Comment: {Environment.NewLine}{_comment.Text}");
        }
    }
}