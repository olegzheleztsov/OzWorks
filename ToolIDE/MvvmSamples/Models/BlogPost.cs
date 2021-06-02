using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ClassLibraryResources;

namespace ToolIDE.MvvmSamples.Models
{
    public class BlogPost : ObservableObject
    {
        private string _title;
        private string _text;
        private DateTime _when;
        private readonly ObservableCollection<BlogComment> _comments = new();

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value, () => Title);
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

        public IList<BlogComment> Comments => _comments;
    }
}