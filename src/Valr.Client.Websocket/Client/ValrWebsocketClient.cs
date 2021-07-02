using System;
using System.Net.WebSockets;
using System.Reactive.PlatformServices;
using System.Text.Json;
using System.Threading.Tasks;
using Valr.Client.Websocket.Json;
using Valr.Client.Websocket.Models;
using Valr.Client.Websocket.Requests;
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
		readonly IDisposable _accountClientMessageReceivedSubscription;
		readonly IDisposable _tradeClientMessageReceivedSubscription;

		/// <summary>
		/// Creates a real live websocket connection to Valr.
		/// </summary>
		/// <param name="clock">A system clock.</param>
		/// <param name="secrets">The secrets.</param>
		/// <param name="subscriptions">The required subscriptions.</param>
		/// <returns>A connected Valr websocket client.</returns>
		public static ValrWebsocketClient Create(
			ISystemClock clock,
			ValrSecrets secrets,
			params Subscription[] subscriptions)
		{
			const string baseAddress = "wss://api.valr.com";
			const string accountPath = "/ws/account";
			const string tradePath = "/ws/trade";

			var accountClient = new WebsocketClient(new Uri($"{baseAddress}{accountPath}", UriKind.Absolute), () => new ClientWebSocket()
				.WithKeepAliveInterval(TimeSpan.FromSeconds(3))
				.WithAuthentication(accountPath, secrets, clock))
			{
				Name = "Valr (Account)"
			};
			var tradeClient = new WebsocketClient(new Uri($"{baseAddress}{tradePath}", UriKind.Absolute), () => new ClientWebSocket()
				.WithKeepAliveInterval(TimeSpan.FromSeconds(3))
				.WithAuthentication(tradePath, secrets, clock))
			{
				Name = "Valr (Trade)"
			};

			var client = new ValrWebsocketClient(accountClient, tradeClient);

			tradeClient.ReconnectionHappened.Subscribe(_ =>
			{
				foreach (var subscription in subscriptions)
					client.SendOnTradeWebsocket(new ChangeSubscriptionsRequest(subscription));
			});

			_ = accountClient.Start();
			_ = tradeClient.Start();

			_ = Task.Run(async () =>
			{
				while (true)
				{
					await Task.Delay(TimeSpan.FromSeconds(30));
					client.SendOnAccountWebsocket(new PingRequest());
					client.SendOnTradeWebsocket(new PingRequest());
				}
			});

			return client;
		}

		/// <summary>
		/// Creates a new instance.
		/// </summary>
		/// <param name="accountClient">The client to use for the Account websocket.</param>
		/// <param name="tradeClient">The client to use for the Trade websocket.</param>
		public ValrWebsocketClient(IWebsocketClient accountClient, IWebsocketClient tradeClient)
		{
			AccountClient = accountClient;
			TradeClient = tradeClient;
			_accountClientMessageReceivedSubscription = AccountClient.MessageReceived.Subscribe(HandleAccountMessage);
			_tradeClientMessageReceivedSubscription = TradeClient.MessageReceived.Subscribe(HandleTradeMessage);
		}

		/// <summary>
		/// The Account websocket client.
		/// </summary>
		public IWebsocketClient AccountClient { get; }

		/// <summary>
		/// The Trade websocket client.
		/// </summary>
		public IWebsocketClient TradeClient { get; }

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
		public void SendOnAccountWebsocket<T>(T request) where T : Message
		{
			var serialized = JsonSerializer.Serialize(request, ValrJsonOptions.Default);
			AccountClient.Send(serialized);
		}

		/// <summary>
		/// Serializes request and sends message via Trade websocket client.
		/// </summary>
		/// <param name="request">Request/message to be sent</param>
		public void SendOnTradeWebsocket<T>(T request) where T : Message
		{
			var serialized = JsonSerializer.Serialize(request, ValrJsonOptions.Default);
			TradeClient.Send(serialized);
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

			return Message.TryHandle(MessageType.AUTHENTICATED, messageType, response, AccountStreams.AuthenticatedSubject) ||
				   Message.TryHandle(MessageType.PONG, messageType, response, AccountStreams.PongSubject) ||
				   Message.TryHandle(MessageType.NEW_ACCOUNT_HISTORY_RECORD, messageType, response, AccountStreams.NewAccountHistoryRecordSubject) ||
				   Message.TryHandle(MessageType.BALANCE_UPDATE, messageType, response, AccountStreams.BalanceUpdateSubject) ||
				   Message.TryHandle(MessageType.NEW_ACCOUNT_TRADE, messageType, response, AccountStreams.NewAccountTradeSubject) ||
				   Message.TryHandle(MessageType.INSTANT_ORDER_COMPLETED, messageType, response, AccountStreams.InstantOrderCompletedSubject) ||
				   Message.TryHandle(MessageType.OPEN_ORDERS_UPDATE, messageType, response, AccountStreams.OpenOrdersUpdateSubject) ||
				   Message.TryHandle(MessageType.ORDER_PROCESSED, messageType, response, AccountStreams.OrderProcessedSubject) ||
				   Message.TryHandle(MessageType.ORDER_STATUS_UPDATE, messageType, response, AccountStreams.OrderStatusUpdateSubject) ||
				   Message.TryHandle(MessageType.FAILED_CANCEL_ORDER, messageType, response, AccountStreams.FailedCancelOrderSubject) ||
				   Message.TryHandle(MessageType.NEW_PENDING_RECEIVE, messageType, response, AccountStreams.NewPendingReceiveSubject) ||
				   Message.TryHandle(MessageType.SEND_STATUS_UPDATE, messageType, response, AccountStreams.SendStatusUpdateSubject);
		}

		bool HandleTradeObjectMessage(string message)
		{
			var response = JsonSerializer.Deserialize<JsonElement>(message, ValrJsonOptions.Default);

			var messageType = response.GetProperty("type").GetString();

			return Message.TryHandle(MessageType.AUTHENTICATED, messageType, response, TradeStreams.AuthenticatedSubject) ||
				   Message.TryHandle(MessageType.SUBSCRIBED, messageType, response, TradeStreams.SubscribedSubject) ||
				   Message.TryHandle(MessageType.UNSUBSCRIBED, messageType, response, TradeStreams.UnsubscribedSubject) ||
				   Message.TryHandle(MessageType.PONG, messageType, response, TradeStreams.PongSubject) ||
				   Message.TryHandle(MessageType.AGGREGATED_ORDERBOOK_UPDATE, messageType, response, TradeStreams.AggregatedOrderBookUpdateSubject) ||
				   Message.TryHandle(MessageType.MARKET_SUMMARY_UPDATE, messageType, response, TradeStreams.MarketSummaryUpdateSubject) ||
				   Message.TryHandle(MessageType.NEW_TRADE_BUCKET, messageType, response, TradeStreams.NewTradeBucketSubject) ||
				   Message.TryHandle(MessageType.NEW_TRADE, messageType, response, TradeStreams.NewTradeSubject);
		}
	}
}
