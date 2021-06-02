using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ClassLibraryResources;
using ToolIDE.MvvmSamples.Models;
using ToolIDE.MvvmSamples.Views;

namespace ToolIDE.MvvmSamples.ViewModels
{
    public class BlogVM : ViewModelBase<Blog>
    {
        public BloggerVM Blogger => new BloggerVM() {Model = Model.Blogger};

        private ICommand _newPostCommand;

        public ICommand NewPostCommand
            => _newPostCommand ??= new RelayCommand(() =>
            {
                var post = new BlogPostVM() {Model = new BlogPost()};
                var dlg = new NewPostWindow() {DataContext = post};
                if (dlg.ShowDialog() == true)
                {
                    post.Model.When = DateTime.Now;
                    Model.Posts.Add(post.Model);
                    OnPropertyChanged(nameof(Posts));
                }
            });

        public IEnumerable<BlogPostVM> Posts => Model.Posts.Select(post => new BlogPostVM() {Model = post});
    }
}