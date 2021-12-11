namespace Valr.Client.Websocket.Client;

/// <summary>
/// Valr account websocket client.
/// </summary>
public interface IValrAccountWebsocketClient : IValrWebsocketClient
{
	/// <summary>
	/// Provided account message streams.
	/// </summary>
	ValrAccountClientStreams Streams { get; }
}
