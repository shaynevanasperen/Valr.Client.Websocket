using System;
using System.Text.Json;
using Valr.Client.Websocket.Json;
using Valr.Client.Websocket.Messages;
using Websocket.Client;

namespace Valr.Client.Websocket.Client
{
	/// <summary>
	/// Valr websocket client.
	/// Use method `SendOnTradeWebsocket()` to subscribe to channels.
	/// And `AccountStreams` and `TradeStreams` to handle messages.
	/// </summary>
	public class ValrWebsocketClient : IDisposable
	{
		readonly IWebsocketClient _accountClient;
		readonly IWebsocketClient _tradeClient;
		readonly IDisposable _accountClientMessageReceivedSubscription;
		readonly IDisposable _tradeClientMessageReceivedSubscription;

		/// <summary>
		/// Creates a new instance.
		/// </summary>
		/// <param name="accountClient">The client to use for the Account websocket.</param>
		/// <param name="tradeClient">The client to use for the Trade websocket.</param>
		public ValrWebsocketClient(IWebsocketClient accountClient, IWebsocketClient tradeClient)
		{
			_accountClient = accountClient;
			_tradeClient = tradeClient;
			_accountClientMessageReceivedSubscription = _accountClient.MessageReceived.Subscribe(HandleAccountMessage);
			_tradeClientMessageReceivedSubscription = _tradeClient.MessageReceived.Subscribe(HandleTradeMessage);
		}

		/// <summary>
		/// Provided account message streams
		/// </summary>
		public ValrAccountClientStreams AccountStreams { get; } = new();

		/// <summary>
		/// Provided trade message streams
		/// </summary>
		public ValrTradeClientStreams TradeStreams { get; } = new();

		/// <summary>
		/// Serializes request and sends message via Account websocket client. 
		/// </summary>
		/// <param name="request">Request/message to be sent</param>
		public void SendOnAccountWebsocket<T>(T request)
		{
			var serialized = JsonSerializer.Serialize(request, ValrJsonOptions.Default);
			_accountClient.Send(serialized);
		}

		/// <summary>
		/// Serializes request and sends message via Trade websocket client.
		/// </summary>
		/// <param name="request">Request/message to be sent</param>
		public void SendOnTradeWebsocket<T>(T request)
		{
			var serialized = JsonSerializer.Serialize(request, ValrJsonOptions.Default);
			_tradeClient.Send(serialized);
		}

		/// <summary>
		/// Cleanup.
		/// </summary>
		public void Dispose()
		{
			_accountClientMessageReceivedSubscription.Dispose();
			_tradeClientMessageReceivedSubscription.Dispose();
		}

		void HandleAccountMessage(ResponseMessage message) => HandleMessage(message, HandleAccountObjectMessage, HandleAccountRawMessage);

		void HandleTradeMessage(ResponseMessage message) => HandleMessage(message, HandleTradeObjectMessage, HandleTradeRawMessage);

		static void HandleMessage(ResponseMessage message, Func<string, bool> objectMessageHandler, Func<string, bool> rawMessageHandler)
		{
			bool handled;
			var messageSafe = (message.Text ?? string.Empty).Trim();

			if (messageSafe.StartsWith("{", StringComparison.OrdinalIgnoreCase))
			{
				handled = objectMessageHandler(messageSafe);
				if (handled)
					return;
			}

			handled = rawMessageHandler(messageSafe);
			if (handled)
				return;

			throw new Exception($"Unhandled response: '{messageSafe}'");
		}

		static bool HandleAccountRawMessage(string message) => false;

		static bool HandleTradeRawMessage(string message) => false;

		bool HandleAccountObjectMessage(string message)
		{
			var response = JsonSerializer.Deserialize<JsonElement>(message, ValrJsonOptions.Default);

			var messageType = response.GetProperty("type").GetString();

			return MessageBase.TryHandle(MessageType.PONG, messageType, response, AccountStreams.PongSubject) ||
				   MessageBase.TryHandle(MessageType.NEW_ACCOUNT_HISTORY_RECORD, messageType, response, AccountStreams.NewAccountHistoryRecordSubject) ||
				   MessageBase.TryHandle(MessageType.BALANCE_UPDATE, messageType, response, AccountStreams.BalanceUpdateSubject) ||
				   MessageBase.TryHandle(MessageType.NEW_ACCOUNT_TRADE, messageType, response, AccountStreams.NewAccountTradeSubject) ||
				   MessageBase.TryHandle(MessageType.INSTANT_ORDER_COMPLETED, messageType, response, AccountStreams.InstantOrderCompletedSubject) ||
				   MessageBase.TryHandle(MessageType.OPEN_ORDERS_UPDATE, messageType, response, AccountStreams.OpenOrdersUpdateSubject) ||
				   MessageBase.TryHandle(MessageType.ORDER_PROCESSED, messageType, response, AccountStreams.OrderProcessedSubject) ||
				   MessageBase.TryHandle(MessageType.ORDER_STATUS_UPDATE, messageType, response, AccountStreams.OrderStatusUpdateSubject) ||
				   MessageBase.TryHandle(MessageType.FAILED_CANCEL_ORDER, messageType, response, AccountStreams.FailedCancelOrderSubject) ||
				   MessageBase.TryHandle(MessageType.NEW_PENDING_RECEIVE, messageType, response, AccountStreams.NewPendingReceiveSubject) ||
				   MessageBase.TryHandle(MessageType.SEND_STATUS_UPDATE, messageType, response, AccountStreams.SendStatusUpdateSubject);
		}

		bool HandleTradeObjectMessage(string message)
		{
			var response = JsonSerializer.Deserialize<JsonElement>(message, ValrJsonOptions.Default);

			var messageType = response.GetProperty("type").GetString();

			return MessageBase.TryHandle(MessageType.PONG, messageType, response, TradeStreams.PongSubject) ||
				   MessageBase.TryHandle(MessageType.AGGREGATED_ORDERBOOK_UPDATE, messageType, response, TradeStreams.AggregatedOrderBookUpdateSubject) ||
				   MessageBase.TryHandle(MessageType.MARKET_SUMMARY_UPDATE, messageType, response, TradeStreams.MarketSummaryUpdateSubject) ||
				   MessageBase.TryHandle(MessageType.NEW_TRADE_BUCKET, messageType, response, TradeStreams.NewTradeBucketSubject) ||
				   MessageBase.TryHandle(MessageType.NEW_TRADE, messageType, response, TradeStreams.NewTradeSubject);
		}
	}
}
