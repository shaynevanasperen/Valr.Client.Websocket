namespace Valr.Client.Websocket.Client;

/// <summary>
/// Valr trade websocket client.
/// </summary>
public interface IValrTradeWebsocketClient : IValrWebsocketClient
{
	/// <summary>
	/// Provided trade message streams.
	/// </summary>
	ValrTradeClientStreams Streams { get; }
}
