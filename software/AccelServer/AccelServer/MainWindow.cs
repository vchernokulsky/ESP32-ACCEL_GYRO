using System;
using Gtk;
using AccelServer;
using System.Threading;

public partial class MainWindow: Gtk.Window
{
	
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();

		int port = 9876;
		ConnectionController controller = new ConnectionController (port);
		Thread ipBroadcaster = new Thread(new ThreadStart(controller.IpBroadcast));
		ipBroadcaster.Start();
	
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnButton4Clicked (object sender, EventArgs e)
	{
		AsynchronousSocketListener.StartListening ();
	}
}
