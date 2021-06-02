using System.Windows;
using System.Windows.Media;

namespace ToolIDE.Test
{
    public class RotationManager : DependencyObject
    {
        public static readonly DependencyProperty AngleProperty = DependencyProperty.RegisterAttached(
            "Angle", typeof(double), typeof(RotationManager), 
            new PropertyMetadata(default(double), OnAngleChanged));

        public static void SetAngle(DependencyObject element, double value)
        {
            element.SetValue(AngleProperty, value);
        }

        public static double GetAngle(DependencyObject element)
        {
            return (double) element.GetValue(AngleProperty);
        }

        private static void OnAngleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is UIElement element)
            {
                element.RenderTransformOrigin = new Point(.5, .5);
                element.RenderTransform = new RotateTransform((double) e.NewValue);
            }
        }
    }
}