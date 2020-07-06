using System;
using Gtk;
using AccelServer;

public partial class MainWindow: Gtk.Window
{
	
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
	
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnButton4Clicked (object sender, EventArgs e)
	{
		ConnectionController c = new ConnectionController ();
		c.SendIp ();
	}
}
