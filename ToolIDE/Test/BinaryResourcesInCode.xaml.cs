using System;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace ToolIDE.Test
{
    public partial class BinaryResourcesInCode : Window
    {
        public BinaryResourcesInCode()
        {
            InitializeComponent();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var info = Application.GetResourceStream(new Uri("books.xml", UriKind.Relative));
            var books = XElement.Load(info.Stream);
            var bookList = from book in books.Elements("Book")
                orderby (string) book.Attribute("Author")
                select new
                {
                    Name = (string) book.Attribute("Name"),
                    Author = (string) book.Attribute("Author")
                };
            foreach (var book in bookList)
            {
                _text.Text += book.ToString() + Environment.NewLine;
            }
        }
    }
}