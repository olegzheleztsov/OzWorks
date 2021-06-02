using System.Collections.Generic;
using System.Collections.ObjectModel;
using ClassLibraryResources;

namespace ToolIDE.MvvmSamples.Models
{
    public class Blog : ObservableObject
    {
        private Blogger _blogger;
        private readonly ObservableCollection<BlogPost> _posts = new();
        private string _blogTitle;

        public Blogger Blogger
        {
            get => _blogger;
            set => SetProperty(ref _blogger, value);
        }

        public IList<BlogPost> Posts => _posts;

        public string BlogTitle
        {
            get => _blogTitle;
            set => SetProperty(ref _blogTitle, value);
        }
    }
}