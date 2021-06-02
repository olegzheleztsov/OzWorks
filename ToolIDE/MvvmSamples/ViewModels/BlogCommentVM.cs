using ClassLibraryResources;
using ToolIDE.MvvmSamples.Models;

namespace ToolIDE.MvvmSamples.ViewModels
{
    public class BlogCommentVM : ViewModelBase<BlogComment>
    {
        public string Text
        {
            get => Model.Text;
            set
            {
                Model.Text = value;
                OnPropertyChanged(nameof(IsCommentOK));
            }
        }

        public string Name
        {
            get => Model.Name;
            set
            {
                Model.Name = value;
                OnPropertyChanged(nameof(IsCommentOK));
            }
        }

        public bool IsCommentOK
            => !string.IsNullOrWhiteSpace(Model.Name) && !string.IsNullOrWhiteSpace(Model.Text);
    }
}