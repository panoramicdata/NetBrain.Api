namespace NetBrain.Api.Exceptions
{
	public class AuthenticationException : NetBrainException
	{
		public AuthenticationException(Response response) : base(response)
		{
		}
	}
}
