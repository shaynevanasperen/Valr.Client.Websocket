using System;
using System.Text.Json;
using Valr.Client.Websocket.Json;
using Valr.Client.Websocket.Models;
using Websocket.Client;

namespace Valr.Client.Websocket.Client
{
	/// <summary>
	/// Valr account websocket client.
	/// Automatically subscribes to all channels.
	/// Use `Streams` to handle messages.
	/// </summary>
	public class ValrAccountWebsocketClient : IDisposable
	{
		readonly IWebsocketClient _client;
		readonly IDisposable _clientMessageReceivedSubscription;

		/// <summary>
		/// Creates a new instance.
		/// </summary>
		/// <param name="client">The client to use for the account websocket.</param>
		public ValrAccountWebsocketClient(IWebsocketClient client)
		{
			_client = client;
			_clientMessageReceivedSubscription = _client.MessageReceived.Subscribe(HandleMessage);
		}

		/// <summary>
		/// Provided account message streams
		/// </summary>
		public ValrAccountClientStreams Streams { get; } = new();

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
				   Message.TryHandle(MessageType.PONG, messageType, response, Streams.PongSubject) ||
				   Message.TryHandle(MessageType.NEW_ACCOUNT_HISTORY_RECORD, messageType, response, Streams.NewAccountHistoryRecordSubject) ||
				   Message.TryHandle(MessageType.BALANCE_UPDATE, messageType, response, Streams.BalanceUpdateSubject) ||
				   Message.TryHandle(MessageType.NEW_ACCOUNT_TRADE, messageType, response, Streams.NewAccountTradeSubject) ||
				   Message.TryHandle(MessageType.INSTANT_ORDER_COMPLETED, messageType, response, Streams.InstantOrderCompletedSubject) ||
				   Message.TryHandle(MessageType.OPEN_ORDERS_UPDATE, messageType, response, Streams.OpenOrdersUpdateSubject) ||
				   Message.TryHandle(MessageType.ORDER_PROCESSED, messageType, response, Streams.OrderProcessedSubject) ||
				   Message.TryHandle(MessageType.ORDER_STATUS_UPDATE, messageType, response, Streams.OrderStatusUpdateSubject) ||
				   Message.TryHandle(MessageType.FAILED_CANCEL_ORDER, messageType, response, Streams.FailedCancelOrderSubject) ||
				   Message.TryHandle(MessageType.NEW_PENDING_RECEIVE, messageType, response, Streams.NewPendingReceiveSubject) ||
				   Message.TryHandle(MessageType.SEND_STATUS_UPDATE, messageType, response, Streams.SendStatusUpdateSubject);
		}
	}
}
