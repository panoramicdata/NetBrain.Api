using System;

namespace NetBrain.Api.Exceptions
{
	public abstract class NetBrainException : Exception
	{
		private readonly Response _response;

		protected NetBrainException(Response response)
		{
			_response = response;
		}

		public override string ToString()
			=> $"{_response.StatusCode}: {_response.StatusDescription}";
	}
}
