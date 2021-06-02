using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ToolIDE.Test
{
    public partial class SelectingOptions : Window
    {
        public SelectingOptions()
        {
            InitializeComponent();
        }

        private void OnMakeTea(object sender, RoutedEventArgs e)
        {
            var sb = new StringBuilder("Tea: ");
            foreach(RadioButton rb in _teaTypePanel.Children)
                if(rb.IsChecked == true) {
                    sb.AppendLine(rb.Content.ToString());
                    break;
                }
            if(_isSugar.IsChecked == true)
                sb.AppendLine("With sugar");
            if(_isMilk.IsChecked == true)
                sb.AppendLine("With milk");
            if(_isLemon.IsChecked == true)
                sb.AppendLine("With lemon");
            if(_isLemon.IsChecked == true && _isMilk.IsChecked == true)
                sb.AppendLine("Very unusual!");
            MessageBox.Show(sb.ToString(), "Tea Maker");
        }
    }
}