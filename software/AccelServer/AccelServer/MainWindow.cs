using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{
	private AccelServer.SocketServer socket;
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
	
		socket = new AccelServer.SocketServer("Hello world!!!");
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnButton4Clicked (object sender, EventArgs e)
	{
		socket.PrintStr (10);
	}
}
