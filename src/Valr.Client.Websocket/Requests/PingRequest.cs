using Valr.Client.Websocket.Models;

namespace Valr.Client.Websocket.Requests;

/// <summary>
/// Ping message.
/// </summary>
public record PingRequest : Message
{
	/// <summary>
	/// Creates a new instance.
	/// </summary>
	public PingRequest() => Type = MessageType.PING;
}
