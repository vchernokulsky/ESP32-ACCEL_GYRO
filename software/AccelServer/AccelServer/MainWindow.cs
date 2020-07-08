using System;
using Gtk;
using AccelServer;
using System.Threading;

public partial class MainWindow: Gtk.Window
{
	
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();

		int port = 15000;
		IpBroadcaster controller = new IpBroadcaster (port);
		Thread ipBroadcaster = new Thread(new ThreadStart(controller.IpBroadcast));
		ipBroadcaster.Start();

		Thread synchronizer = new Thread(new ThreadStart(DeviceSynchronizer.StartListening));
		synchronizer.Start();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnButton4Clicked (object sender, EventArgs e)
	{
		DataReceiver.running = true;
		DataReceiver dt_recv = new DataReceiver (10000);

		Thread data_receiver = new Thread(new ThreadStart(dt_recv.StartListening));
		data_receiver.Start();
	}

	protected void OnButton1Clicked (object sender, EventArgs e)
	{
		DataReceiver.running = false;
	}
}
