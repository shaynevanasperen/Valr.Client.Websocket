using System;
using System.Text.Json;
using Valr.Client.Websocket.Json;
using Valr.Client.Websocket.Models;
using Websocket.Client;

namespace Valr.Client.Websocket.Client
{
	/// <summary>
	/// Valr trade websocket client.
	/// Use method `Send()` to subscribe to channels.
	/// And `Streams` to handle messages.
	/// </summary>
	public class ValrTradeWebsocketClient : IDisposable
	{
		readonly IWebsocketClient _client;
		readonly IDisposable _clientMessageReceivedSubscription;

		/// <summary>
		/// Creates a new instance.
		/// </summary>
		/// <param name="client">The client to use for the trade websocket.</param>
		public ValrTradeWebsocketClient(IWebsocketClient client)
		{
			_client = client;
			_clientMessageReceivedSubscription = _client.MessageReceived.Subscribe(HandleMessage);
		}

		/// <summary>
		/// Provided account message streams
		/// </summary>
		public ValrTradeClientStreams Streams { get; } = new();

		/// <summary>
		/// Serializes request and sends message via Account websocket client. 
		/// </summary>
		/// <param name="request">Request/message to be sent</param>
		public void Send<T>(T request) where T : Message
		{
			var serialized = JsonSerializer.Serialize(request, ValrJsonOptions.Default);
			_client.Send(serialized);
		}

		/// <summary>
		/// Cleanup.
		/// </summary>
		public void Dispose() => _clientMessageReceivedSubscription.Dispose();

		void HandleMessage(ResponseMessage message)
		{
			bool handled;
			var messageSafe = (message.Text ?? string.Empty).Trim();

			if (messageSafe.StartsWith("{", StringComparison.OrdinalIgnoreCase))
			{
				handled = HandleObjectMessage(messageSafe);
				if (handled)
					return;
			}

			handled = HandleRawMessage(messageSafe);
			if (handled)
				return;

			throw new Exception($"Unhandled response: '{messageSafe}'");
		}

		static bool HandleRawMessage(string message) => false;

		static bool HandleTradeRawMessage(string message) => false;

		bool HandleObjectMessage(string message)
		{
			var response = JsonSerializer.Deserialize<JsonElement>(message, ValrJsonOptions.Default);

			var messageType = response.GetProperty("type").GetString();

			return Message.TryHandle(MessageType.AUTHENTICATED, messageType, response, Streams.AuthenticatedSubject) ||
				   Message.TryHandle(MessageType.SUBSCRIBED, messageType, response, Streams.SubscribedSubject) ||
				   Message.TryHandle(MessageType.UNSUBSCRIBED, messageType, response, Streams.UnsubscribedSubject) ||
				   Message.TryHandle(MessageType.PONG, messageType, response, Streams.PongSubject) ||
				   Message.TryHandle(MessageType.AGGREGATED_ORDERBOOK_UPDATE, messageType, response, Streams.AggregatedOrderBookUpdateSubject) ||
				   Message.TryHandle(MessageType.MARKET_SUMMARY_UPDATE, messageType, response, Streams.MarketSummaryUpdateSubject) ||
				   Message.TryHandle(MessageType.NEW_TRADE_BUCKET, messageType, response, Streams.NewTradeBucketSubject) ||
				   Message.TryHandle(MessageType.NEW_TRADE, messageType, response, Streams.NewTradeSubject);
		}
	}
}