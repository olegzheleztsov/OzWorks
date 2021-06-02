#region

using System;
using System.Windows.Input;
using ClassLibraryResources;
using ToolIDE.MvvmSamples.Models;
using ToolIDE.MvvmSamples.Views;

#endregion

namespace ToolIDE.MvvmSamples.ViewModels
{
    public class BlogPostVM : ViewModelBase<BlogPost>
    {
        private ICommand _newCommentCommand;

        public string Title
        {
            get => Model.Title;
            set
            {
                Model.Title = value;
                OnPropertyChanged(nameof(IsPostOK));
            }
        }

        public string Text
        {
            get => Model.Text;
            set
            {
                Model.Text = value;
                OnPropertyChanged(nameof(IsPostOK));
            }
        }

        public bool IsPostOK
            => !string.IsNullOrWhiteSpace(Model.Title) && !string.IsNullOrWhiteSpace(Model.Text);

        public ICommand NewCommentCommand
            => _newCommentCommand ??= new RelayCommand(() =>
            {
                var comment = new BlogComment();
                var dlg = new NewCommentWindow {DataContext = new BlogCommentVM {Model = comment}};
                if (dlg.ShowDialog() == true)
                {
                    comment.When = DateTime.Now;
                    Model.Comments.Add(comment);
                }
            });
    }
}