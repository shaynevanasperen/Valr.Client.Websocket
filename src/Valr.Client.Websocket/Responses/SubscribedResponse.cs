using Valr.Client.Websocket.Models;

namespace Valr.Client.Websocket.Responses
{
	/// <summary>
	/// Subscribed message.
	/// </summary>
	public record SubscribedResponse : Message
	{
		/// <summary>
		/// The message.
		/// </summary>
		public string Message { get; init; } = null!;
	}
}
