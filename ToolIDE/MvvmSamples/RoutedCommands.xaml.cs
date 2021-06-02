#region

using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

#endregion

namespace ToolIDE.MvvmSamples
{
    public partial class RoutedCommands : Window
    {
        private ImageData _imageData;

        public RoutedCommands()
        {
            InitializeComponent();
            DataContext = new ImageData();
        }
    }
}