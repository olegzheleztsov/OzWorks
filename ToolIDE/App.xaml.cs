#region

using System.Windows;

#endregion

namespace ToolIDE
{
	/// <summary>
	///     Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
    {
	    protected override void OnStartup(StartupEventArgs e)
	    {
		    var wnd = new CodeOnlyWindow("file.xaml");
		    wnd.ShowDialog();
		    base.OnStartup(e);
	    }
    }
}