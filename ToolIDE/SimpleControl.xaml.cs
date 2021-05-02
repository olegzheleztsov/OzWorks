using System.Windows;
using System.Windows.Controls;

namespace ToolIDE
{
    public partial class SimpleControl : UserControl
    {
        public SimpleControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty YearPublishedProperty = DependencyProperty.Register(
            "YearPublished", typeof(int), typeof(SimpleControl), 
            new UIPropertyMetadata(2000));

        public int YearPublished
        {
            get => (int) GetValue(YearPublishedProperty);
            set => SetValue(YearPublishedProperty, value);
        }
    }
}