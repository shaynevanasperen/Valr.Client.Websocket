using Valr.Client.Websocket.Models;

namespace Valr.Client.Websocket.Responses
{
	/// <summary>
	/// Unsubscribed message.
	/// </summary>
	public record UnsubscribedResponse : Message
	{
		/// <summary>
		/// The message.
		/// </summary>
		public string Message { get; init; } = null!;
	}
}
