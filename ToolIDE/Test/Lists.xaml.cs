using System;
using System.Windows;
using System.Windows.Controls;

namespace ToolIDE.Test
{
    public partial class Lists : Window
    {
        public Lists()
        {
            InitializeComponent();
        }

        private void OnLangChanged(object sender, SelectionChangedEventArgs e)
        {
            _keywordList.Items.Clear();
            string[] keywords = null;
            switch(_langCombo.SelectedIndex) {
                case 0: // C++
                    keywords = new string[] {
                        "for", "auto", "mutable", "explicit",
                        "class", "volatile"
                    };
                    break;
                case 1: // C#
                    keywords = new string[] {
                        "while", "var", "implicit", "return",
                        "where", "enum"
                    };
                    break;
                case 2: // VB
                    keywords = new string[] {
                        "Dim", "Select", "While",
                        "Property", "Function", "If"
                    };
                    break;
                case 3: // F#
                    keywords = new string[] {
                        "let", "rec", "mutable",
                        "module", "yield", "type"
                    };
                    break;
            }

            if (keywords != null)
            {
                Array.ForEach(keywords, keyword => _keywordList.Items.Add(keyword));
            }
        }

        private void OnChangeLanguage(object sender, RoutedEventArgs e)
        {
            var item = e.Source as MenuItem;
            _langCombo.SelectedIndex = Convert.ToInt32(item.Tag);
        }
    }
}