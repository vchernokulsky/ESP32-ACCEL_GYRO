﻿using System;
using System.Net;  
using System.Net.Sockets;  
using System.Text; 
using System.Collections.Generic;

namespace AccelServer
{
	public class ReceivedObject {
		public int length; 
		public byte[] bytes;

		public ReceivedObject(int length, byte[] bytes)
		{
			this.length = length;
			this.bytes = bytes;
		}
	}


	public class DataReceiver
	{
		public static bool running = false;
		private List<ReceivedObject> byteList;

		private int id;
		private int type;

		private int sync_ticks;
		private DateTime sync_time;

		private int port;
		private int totalRecv;

		public DataReceiver (int id, int type, int port, DateTime sync_time, int sync_ticks)
		{
			byteList = new List<ReceivedObject> ();
			this.id = id;
			this.type = type;
			this.port = port;
			this.sync_time = sync_time;
			this.sync_ticks = sync_ticks;
		}
			

		public void StartListening()
		{
			IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());  
			IPAddress ipAddress = ipHostInfo.AddressList[0];  
			IPEndPoint ipPoint = new IPEndPoint(ipAddress, port);
			Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			try
			{
				
				listenSocket.Bind(ipPoint);
				listenSocket.Listen(10);
				Console.WriteLine("DataReceiver: Waiting for connection...");

				while (true)
				{
					Socket handler = listenSocket.Accept();
					int bytesRec = 0; 
					byte[] bytes = new byte[6300]; 
					totalRecv = 0;
					while(running)
					{
						bytesRec = handler.Receive(bytes);
						totalRecv += bytesRec;
						byteList.Add(new ReceivedObject(bytesRec, bytes));
						Console.WriteLine( "Received {0} bytes", bytesRec);
						Console.WriteLine( "TOTAL RECEIVED {0} bytes", totalRecv);

					}
						
					handler.Shutdown(SocketShutdown.Both);
					handler.Close();

					if(byteList.Count > 0)
					{
						Console.WriteLine( "byteList len {0} ", byteList.Count);
						ProcessData();
						byteList = new List<ReceivedObject>();
					} else {
						Console.WriteLine( "=== STOPPED ===");
					}
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private void ProcessData()
		{
			int package_size = 19;
			byte[] bytes = new byte[19];
			int cur_len = 0;
			int package_cnt = 0;
			AccGyroList agList = new AccGyroList (sync_time, sync_ticks);
			int ff_found = 0;
			foreach (ReceivedObject recv in byteList) 
			{
				int bytes_proceed = 0;
				while(bytes_proceed < recv.length)
				{
					if (ff_found == 2) {
						int copy_len = Math.Min (package_size - cur_len, recv.length - bytes_proceed);
						Array.Copy (recv.bytes, bytes_proceed, bytes, cur_len, copy_len);
						cur_len += copy_len;
						bytes_proceed += copy_len;
						if (cur_len == package_size) {
							int chk_sum = 0;
							foreach (byte b in bytes) {
								chk_sum += (int) b;
							}
							if (chk_sum % 256 == 0) {
								Console.WriteLine ("found {0} packages", ++package_cnt);
								agList.put (bytes);
								cur_len = 0;
								ff_found = 0;
							} else {
								ff_found = 0;
								for (int i = 0; i < cur_len; i++) {
									if (bytes [i] == (int)255) {
										ff_found++;
									}
									if (ff_found == 2) {
										byte[] tmp_bytes = new byte[16];
										Array.Copy (bytes, i, tmp_bytes, 0, package_size - i - 1);
										bytes = tmp_bytes;
										cur_len = package_size - i - 1;
									} else {
										cur_len = 0;
									}
								}
							}
						}
					} else {
						if (recv.bytes [bytes_proceed] == (byte)255) {
							ff_found++;
						} else {
							ff_found = 0;
						}
						bytes_proceed++;
					}

				}

			}
			Console.WriteLine(agList.agList.Count);
				
		}

	}
}
		