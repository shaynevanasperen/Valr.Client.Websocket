using Valr.Client.Websocket.Messages;

namespace Valr.Client.Websocket.Requests
{
	/// <summary>
	/// Ping message.
	/// </summary>
	public record PingRequest : MessageBase
	{
		/// <summary>
		/// Creates a new instance.
		/// </summary>
		public PingRequest() => Type = MessageType.PING;
	}
}
