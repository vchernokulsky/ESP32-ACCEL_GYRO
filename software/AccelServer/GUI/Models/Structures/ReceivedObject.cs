namespace GUI
{
    public class ReceivedObject
	{
		public int id;
		public int length;
		public byte[] bytes;

		public ReceivedObject(int id, int length, byte[] bytes)
		{
			this.id = id;
			this.length = length;
			this.bytes = bytes;
		}
	}
}
