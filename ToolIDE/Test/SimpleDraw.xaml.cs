using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ToolIDE.Test
{
    public partial class SimpleDraw : Window
    {
        public SimpleDraw()
        {
            InitializeComponent();
        }

        private Point _pos;
        private bool _isDrawing;
        private Brush _stroke = Brushes.Black;

        private void OnLeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is Rectangle rect)
            {
                _stroke = rect.Fill;
            }
            else
            {
                _isDrawing = true;
                _pos = e.GetPosition(_root);
                _root.CaptureMouse();
            }
        }

        private void OnLeftMouseUp(object sender, MouseButtonEventArgs e)
        {
            _isDrawing = false;
            _root.ReleaseMouseCapture();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {

            
            if (_isDrawing)
            {
                Line line = new Line()
                {
                    X1 = _pos.X,
                    Y1 = _pos.Y
                };
                _pos = e.GetPosition(_root);
                line.X2 = _pos.X;
                line.Y2 = _pos.Y;
                line.Stroke = _stroke;
                line.StrokeThickness = 1;
                _root.Children.Add(line);
            }
        }
    }
}