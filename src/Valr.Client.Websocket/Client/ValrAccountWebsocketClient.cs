using System.Text.Json;
using Microsoft.Extensions.Logging;
using Valr.Client.Websocket.Json;
using Valr.Client.Websocket.Models;
using Websocket.Client;

namespace Valr.Client.Websocket.Client;

/// <summary>
/// Valr account websocket client.
/// Automatically subscribes to all channels.
/// Use `Streams` to handle messages.
/// </summary>
public class ValrAccountWebsocketClient(ILogger logger, IWebsocketClient client) : ValrWebsocketClient(logger, client, "ACCOUNT"), IValrAccountWebsocketClient
{

	/// <inheritdoc />
	public ValrAccountClientStreams Streams { get; } = new();

	/// <inheritdoc />
	protected override bool HandleObjectMessage(string message)
	{
		var response = JsonSerializer.Deserialize<JsonElement>(message, ValrJsonOptions.Default);

		var messageType = response.GetProperty("type").GetString();

		return Message.TryHandle(MessageType.AUTHENTICATED, messageType, response, Streams.AuthenticatedStream) ||
		       Message.TryHandle(MessageType.PONG, messageType, response, Streams.PongStream) ||
		       Message.TryHandle(MessageType.NEW_ACCOUNT_HISTORY_RECORD, messageType, response, Streams.NewAccountHistoryRecordStream) ||
		       Message.TryHandle(MessageType.BALANCE_UPDATE, messageType, response, Streams.BalanceUpdateStream) ||
		       Message.TryHandle(MessageType.NEW_ACCOUNT_TRADE, messageType, response, Streams.NewAccountTradeStream) ||
		       Message.TryHandle(MessageType.INSTANT_ORDER_COMPLETED, messageType, response, Streams.InstantOrderCompletedStream) ||
		       Message.TryHandle(MessageType.OPEN_ORDERS_UPDATE, messageType, response, Streams.OpenOrdersUpdateStream) ||
		       Message.TryHandle(MessageType.ORDER_PROCESSED, messageType, response, Streams.OrderProcessedStream) ||
		       Message.TryHandle(MessageType.ORDER_STATUS_UPDATE, messageType, response, Streams.OrderStatusUpdateStream) ||
		       Message.TryHandle(MessageType.FAILED_CANCEL_ORDER, messageType, response, Streams.FailedCancelOrderStream) ||
		       Message.TryHandle(MessageType.NEW_PENDING_RECEIVE, messageType, response, Streams.NewPendingReceiveStream) ||
		       Message.TryHandle(MessageType.SEND_STATUS_UPDATE, messageType, response, Streams.SendStatusUpdateStream);
	}
}
