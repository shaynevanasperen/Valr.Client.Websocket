using Valr.Client.Websocket.Models;

namespace Valr.Client.Websocket.Responses;

/// <summary>
/// Pong message.
/// </summary>
public record PongResponse : Message
{
	/// <summary>
	/// The message.
	/// </summary>
	public string Message { get; init; } = null!;
}
