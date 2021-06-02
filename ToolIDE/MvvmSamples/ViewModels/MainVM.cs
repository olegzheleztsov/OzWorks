#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ClassLibraryResources;
using ToolIDE.MvvmSamples.Models;

#endregion

namespace ToolIDE.MvvmSamples.ViewModels
{
    public class MainVM : ViewModelBase<IEnumerable<Blog>>
    {
        private BlogVM _selectedBlog;

        public MainVM(IEnumerable<Blog> blogs)
        {
            Model = new ObservableCollection<Blog>(blogs);
        }

        public IEnumerable<BlogVM> Blogs => Model.Select(blog => new BlogVM {Model = blog});

        public BlogVM SelectedBlog
        {
            get => _selectedBlog;
            set
            {
                if (SetProperty(ref _selectedBlog, value))
                {
                    OnPropertyChanged(nameof(IsSelectedBlog));
                }
            }
        }

        public Visibility IsSelectedBlog
            => SelectedBlog != null ? Visibility.Visible : Visibility.Collapsed;
    }
}