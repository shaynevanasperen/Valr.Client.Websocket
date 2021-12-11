using Valr.Client.Websocket.Models;

namespace Valr.Client.Websocket.Responses;

/// <summary>
/// Send status update message.
/// </summary>
public record SendStatusUpdateResponse : Message<CryptoSend>;

/// <summary>
/// Valr crypto send.
/// </summary>
public record CryptoSend
{
	/// <summary>
	/// The unique id.
	/// </summary>
	public string UniqueId { get; init; } = null!;

	/// <summary>
	/// The send status.
	/// </summary>
	public string Status { get; init; } = null!;

	/// <summary>
	/// Number of transaction confirmations.
	/// </summary>
	public int Confirmations { get; init; }
}
