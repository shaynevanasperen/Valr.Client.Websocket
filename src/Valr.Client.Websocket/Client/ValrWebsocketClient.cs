using System;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Valr.Client.Websocket.Json;
using Valr.Client.Websocket.Models;
using Websocket.Client;

namespace Valr.Client.Websocket.Client;

/// <inheritdoc />
public abstract class ValrWebsocketClient : IValrWebsocketClient
{
	readonly ILogger _logger;
	readonly IWebsocketClient _client;
	readonly string _type;
	readonly IDisposable _clientMessageReceivedSubscription;

	/// <summary>
	/// Creates a new instance.
	/// </summary>
	/// <param name="logger">The logger to use for logging any warnings or errors.</param>
	/// <param name="client">The client to use for the trade websocket.</param>
	/// <param name="type">The type of websocket client (TRADE or ACCOUNT).</param>
	protected ValrWebsocketClient(ILogger logger, IWebsocketClient client, string type)
	{
		_logger = logger;
		_client = client;
		_type = type;

		_clientMessageReceivedSubscription = _client.MessageReceived.Subscribe(HandleMessage);
	}

	/// <inheritdoc />
	public void Send<T>(T request) where T : Message
	{
		try
		{
			var serialized = JsonSerializer.Serialize(request, ValrJsonOptions.Default);
			_client.Send(serialized);
		}
		catch (Exception e)
		{
			_logger.LogError(e, LogMessage($"Exception while sending message '{request}'. Error: {e.Message}"));
			throw;
		}
	}

	/// <summary>
	/// Cleanup.
	/// </summary>
	public void Dispose() => _clientMessageReceivedSubscription.Dispose();

	void HandleMessage(ResponseMessage message)
	{
		try
		{
			var messageSafe = (message.Text ?? string.Empty).Trim();

			if (messageSafe.StartsWith("{", StringComparison.OrdinalIgnoreCase))
				if (HandleObjectMessage(messageSafe))
					return;

			_logger.LogWarning(LogMessage($"Unhandled response:  '{messageSafe}'"));
		}
		catch (Exception e)
		{
			_logger.LogError(e, LogMessage("Exception while receiving message"));
		}
	}

	string LogMessage(string message) => $"[VALR {_type} WEBSOCKET CLIENT] {message}";

	/// <summary>
	/// Handles the message and publishes new stream elements.
	/// </summary>
	/// <param name="message">The message to handle.</param>
	/// <returns>A boolean value to signify whether or not the message could be handled.</returns>
	protected abstract bool HandleObjectMessage(string message);
}
