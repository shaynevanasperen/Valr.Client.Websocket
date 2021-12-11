using System.Text.Json;
using Microsoft.Extensions.Logging;
using Valr.Client.Websocket.Json;
using Valr.Client.Websocket.Models;
using Websocket.Client;

namespace Valr.Client.Websocket.Client;

/// <summary>
/// Valr trade websocket client.
/// Use method `Send()` to subscribe to channels.
/// And `Streams` to handle messages.
/// </summary>
public class ValrTradeWebsocketClient : ValrWebsocketClient, IValrTradeWebsocketClient
{
	/// <inheritdoc />
	public ValrTradeWebsocketClient(ILogger logger, IWebsocketClient client) : base(logger, client, "TRADE") { }

	/// <inheritdoc />
	public ValrTradeClientStreams Streams { get; } = new();

	/// <inheritdoc />
	protected override bool HandleObjectMessage(string message)
	{
		var response = JsonSerializer.Deserialize<JsonElement>(message, ValrJsonOptions.Default);

		var messageType = response.GetProperty("type").GetString();

		return Message.TryHandle(MessageType.AUTHENTICATED, messageType, response, Streams.AuthenticatedStream) ||
		       Message.TryHandle(MessageType.SUBSCRIBED, messageType, response, Streams.SubscribedStream) ||
		       Message.TryHandle(MessageType.UNSUBSCRIBED, messageType, response, Streams.UnsubscribedStream) ||
		       Message.TryHandle(MessageType.PONG, messageType, response, Streams.PongStream) ||
		       Message.TryHandle(MessageType.AGGREGATED_ORDERBOOK_UPDATE, messageType, response, Streams.AggregatedOrderBookUpdateStream) ||
		       Message.TryHandle(MessageType.ALLOWED_ORDER_TYPES_UPDATED, messageType, response, Streams.AllowedOrderTypesUpdatedStream) ||
		       Message.TryHandle(MessageType.MARKET_SUMMARY_UPDATE, messageType, response, Streams.MarketSummaryUpdateStream) ||
		       Message.TryHandle(MessageType.NEW_TRADE_BUCKET, messageType, response, Streams.NewTradeBucketStream) ||
		       Message.TryHandle(MessageType.NEW_TRADE, messageType, response, Streams.NewTradeStream);
	}
}
