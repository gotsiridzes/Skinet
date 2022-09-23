using System;

namespace Api.Errors
{
	public class ApiResponse
	{
		public int StatusCode { get; }
		public string Message { get; }

		public ApiResponse(int statusCode, string message = null)
		{
			StatusCode = statusCode;
			Message = message ?? GetDefaultMessageForStatusCode(statusCode);
		}

		private string GetDefaultMessageForStatusCode(int statusCode)
		{
			return statusCode switch
			{
				400 => "A bad request",
				401 => "Not authoruzed",
				404 => "Not found",
				500 => "Server side error",
				_ => null
			};
		}
	}
}
