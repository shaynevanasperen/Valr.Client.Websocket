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
/// <inheritdoc />
public class ValrTradeWebsocketClient(ILogger logger, IWebsocketClient client) : ValrWebsocketClient(logger, client, "TRADE"), IValrTradeWebsocketClient
{

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
		       Message.TryHandle(MessageType.FULL_ORDERBOOK_SNAPSHOT, messageType, response, Streams.FullOrderBookSnapshotStream) ||
			   Message.TryHandle(MessageType.FULL_ORDERBOOK_UPDATE, messageType, response, Streams.FullOrderBookUpdateStream) ||
		       Message.TryHandle(MessageType.OB_L1_SNAPSHOT, messageType, response, Streams.L1OrderBookSnapshotStream) ||
			   Message.TryHandle(MessageType.OB_L1_DIFF, messageType, response, Streams.L1OrderBookUpdateStream) ||
		       Message.TryHandle(MessageType.OB_L1_D1_SNAPSHOT, messageType, response, Streams.L1D1OrderBookSnapshotStream) ||
		       Message.TryHandle(MessageType.OB_L1_D5_SNAPSHOT, messageType, response, Streams.L1D5OrderBookSnapshotStream) ||
		       Message.TryHandle(MessageType.OB_L1_D10_SNAPSHOT, messageType, response, Streams.L1D10OrderBookSnapshotStream) ||
		       Message.TryHandle(MessageType.OB_L1_D20_SNAPSHOT, messageType, response, Streams.L1D20OrderBookSnapshotStream) ||
		       Message.TryHandle(MessageType.OB_L1_D40_SNAPSHOT, messageType, response, Streams.L1D40OrderBookSnapshotStream) ||
		       Message.TryHandle(MessageType.OB_L1_D80_SNAPSHOT, messageType, response, Streams.L1D80OrderBookSnapshotStream) ||
			   Message.TryHandle(MessageType.ALLOWED_ORDER_TYPES_UPDATED, messageType, response, Streams.AllowedOrderTypesUpdatedStream) ||
		       Message.TryHandle(MessageType.MARKET_SUMMARY_UPDATE, messageType, response, Streams.MarketSummaryUpdateStream) ||
		       Message.TryHandle(MessageType.NEW_TRADE_BUCKET, messageType, response, Streams.NewTradeBucketStream) ||
		       Message.TryHandle(MessageType.NEW_TRADE, messageType, response, Streams.NewTradeStream);
	}
}
