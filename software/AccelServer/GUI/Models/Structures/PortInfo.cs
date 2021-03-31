namespace GUI
{
    public class PortInfo
	{
		public int Port { get; set; }
		public int CommandPort { get; set; }

		public PortInfo(int port, int commandPort)
		{
			Port = port;
			CommandPort = commandPort;
		}
	}
}
