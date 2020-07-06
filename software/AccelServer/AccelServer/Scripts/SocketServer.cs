using System;

namespace AccelServer
{
	public class SocketServer
	{
		private String str;
		public SocketServer (String s)
		{
			str = s;
		}
		public void PrintStr(int t)
		{
			for (int i = 0; i < t; i++) 
			{
				Console.WriteLine (String.Concat(i.ToString(), str));
			}
		}
	}
}

