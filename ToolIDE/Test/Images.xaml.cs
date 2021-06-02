using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace ToolIDE.Test
{
    public partial class Images : Window
    {
        public Images()
        {
            InitializeComponent();
        }

        private void OnOpenImage(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog {Filter = "Images|*.jpg;*.gif;*.bmp;*.png"};
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    var bmp = new BitmapImage(new Uri(dlg.FileName, UriKind.Absolute));
                    _image.Source = bmp;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}");
                }
            }
        }
    }
}