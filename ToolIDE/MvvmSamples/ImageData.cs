#region

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ToolIDE.Annotations;
using ToolIDE.MvvmSamples.Commands;

#endregion

namespace ToolIDE.MvvmSamples
{
    public class ImageData : INotifyPropertyChanged
    {
        private double _zoom = 1.0;
        private string _imagePath;
        
        public ICommand OpenImageFileCommand { get; }
        public ICommand ZoomCommand { get; }
        public ImageData()
        {
            OpenImageFileCommand = new OpenImageFileCommand(this);
            ZoomCommand = new ZoomCommand(this);
        }
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                OnPropertyChanged();
            }
        }

        public double Zoom
        {
            get => _zoom;
            set
            {
                _zoom = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}