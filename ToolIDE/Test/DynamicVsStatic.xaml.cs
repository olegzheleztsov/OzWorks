using System.Windows;
using System.Windows.Media;

namespace ToolIDE.Test
{
    public partial class DynamicVsStatic : Window
    {
        public DynamicVsStatic()
        {
            InitializeComponent();
        }

        private void OnReplaceBrush(object sender, RoutedEventArgs e)
        {
            var brush = new RadialGradientBrush();
            brush.GradientStops.Add(new GradientStop(Colors.Blue, 0));
            brush.GradientStops.Add(new GradientStop(Colors.White, 1));
            this.Resources["brush1"] = brush;
        }
    }
}