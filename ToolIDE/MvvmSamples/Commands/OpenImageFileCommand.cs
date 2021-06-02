#region

using System;
using System.Windows.Input;
using Microsoft.Win32;

#endregion

namespace ToolIDE.MvvmSamples.Commands
{
    public class OpenImageFileCommand : ICommand
    {
        private readonly ImageData _imageData;

        public OpenImageFileCommand(ImageData imageData)
        {
            _imageData = imageData;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var dlg = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.png;*.bmp;*.gif"
            };
            if (dlg.ShowDialog() == true)
            {
                _imageData.ImagePath = dlg.FileName;
            }
        }

        public event EventHandler? CanExecuteChanged;
    }
}