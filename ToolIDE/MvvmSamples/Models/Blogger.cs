using System.IO;
using ClassLibraryResources;

namespace ToolIDE.MvvmSamples.Models
{
    public class Blogger : ObservableObject
    {
        private string _name;
        private string _email;
        private Stream _picture;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value, () => Name);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value, () => Email);
        }

        public Stream Picture
        {
            get => _picture;
            set => SetProperty(ref _picture, value, () => Picture);
        }
    }
}