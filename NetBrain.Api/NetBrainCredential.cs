namespace NetBrain.Api
{
	internal class NetBrainCredential
	{
		public string Username { get; set; }
		public string Password { get; set; }

		internal void ClearPassword()
		{
			Password = null;
		}
	}
}