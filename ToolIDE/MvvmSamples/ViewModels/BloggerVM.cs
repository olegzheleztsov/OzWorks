using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ClassLibraryResources;
using ToolIDE.MvvmSamples.Models;

namespace ToolIDE.MvvmSamples.ViewModels
{
    public class BloggerVM : ViewModelBase<Blogger>
    {
        public ImageSource Picture
        {
            get
            {
                if (Model.Picture == null)
                {
                    return new BitmapImage(new Uri("/Images/blogger.png", UriKind.Relative));
                }

                var bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = Model.Picture;
                bmp.EndInit();
                return bmp;
            }
        }

        private ICommand _sendEmailCommand;

        public ICommand SendEmailCommand => 
            _sendEmailCommand ??= new RelayCommand<string>(email => Process.Start("mailto:" + email));
    }
}