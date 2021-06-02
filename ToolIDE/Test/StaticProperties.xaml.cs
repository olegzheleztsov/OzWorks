using System.Windows;
using System.Windows.Media;

namespace ToolIDE.Test
{
    public partial class StaticProperties : Window
    {
        public StaticProperties()
        {
            InitializeComponent();
        }

        public static string SomeInterestingText { get; } = "Some interesting text";

        public static Brush NiceColor { get; } = new SolidColorBrush(Colors.Chartreuse);
    }
}