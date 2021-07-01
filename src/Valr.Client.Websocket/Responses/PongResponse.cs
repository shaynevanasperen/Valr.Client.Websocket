using Valr.Client.Websocket.Messages;

namespace Valr.Client.Websocket.Responses
{
	/// <summary>
	/// Pong message.
	/// </summary>
	public record PongResponse : MessageBase
	{
		/// <summary>
		/// The message.
		/// </summary>
		public string Message { get; init; } = null!;
	}
}
