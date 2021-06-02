using System;
using ClassLibraryResources;

namespace ToolIDE.MvvmSamples.Models
{
    public class BlogComment : ObservableObject
    {
        private string _name;
        private string _text;
        private DateTime _when;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value, () => Name);
        }

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value, () => Text);
        }

        public DateTime When
        {
            get => _when;
            set => SetProperty(ref _when, value, () => When);
        }
    }
}