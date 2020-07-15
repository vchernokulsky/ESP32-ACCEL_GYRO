using AccelServer;
using System;
using Gtk;



public partial class MainWindow: Gtk.Window
{
	private AccelServer.AccelServer Accel;
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		Accel = new AccelServer.AccelServer (15000);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Accel.FinishThreads ();
		Console.WriteLine ("!!!EXIT!!!");
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnButton4Clicked (object sender, EventArgs e)
	{
		Accel.StartReceiving ();
	}

	protected void OnButton5Clicked (object sender, EventArgs e)
	{
		Accel.StopReceiving ();
	}
}
