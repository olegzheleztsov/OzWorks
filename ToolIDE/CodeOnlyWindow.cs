using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace ToolIDE
{
    public class CodeOnlyWindow : Window
    {
        private Button _button;

        public CodeOnlyWindow()
        {
            InitializeComponent();
        }

        public CodeOnlyWindow(string xamlFile)
        {
            Width = Height = 285;
            Left = Top = 100;
            Title = "Dynamically Loaded xaml";

            DependencyObject rootElement;
            using var fs = new FileStream(xamlFile, FileMode.Open);
            rootElement = XamlReader.Load(fs) as DependencyObject;
            Content = rootElement;
            _button = (Button) LogicalTreeHelper.FindLogicalNode(rootElement, "button1");
            _button.Click += (sender, args) =>
            {
                _button.Content = "Thank you 2";
            };
            
        }

        private void InitializeComponent()
        {
            Width = Height = 285;
            Left = Top = 100;
            Title = "Code-Only Window";

            var panel = new DockPanel();
            _button = new Button()
            {
                Content = "Please click me",
                Margin = new Thickness(30)
            };
            _button.Click += (sender, args) =>
            {
                _button.Content = "Thank you";
            };
            IAddChild container = panel;
            container.AddChild(_button);

            container = this;
            container.AddChild(panel);
        }
    }
}