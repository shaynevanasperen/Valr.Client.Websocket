using System;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Valr.Client.Websocket.Client;
using Websocket.Client;
using FluentAssertions.Extensions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using System.Text.Json;
using System.Threading;
using MartinCostello.Logging.XUnit;
using Valr.Client.Websocket.Models;
using Valr.Client.Websocket.Requests;

namespace Valr.Client.Websocket.Tests;

public class PingPongTests(ITestOutputHelper outputHelper)
{
	readonly ILogger _logger = new XUnitLogger(nameof(PingPongTests), outputHelper, new XUnitLoggerOptions());

	[IntegrationBddfyFact]
	async Task PingPongOnAccountWebsocketWorks()
	{
		await TestConnection(ValrValues.AccountPath, x =>
		{
			var client = new ValrAccountWebsocketClient(_logger, x);
			client.Streams.PongStream.Subscribe(response => _logger.LogInformation(JsonSerializer.Serialize(response)));
			return client;
		});
	}

	[IntegrationBddfyFact]
	async Task PingPongOnTradeWebsocketWorks()
	{
		await TestConnection(ValrValues.TradePath, x =>
		{
			var client = new ValrTradeWebsocketClient(_logger, x);
			client.Streams.PongStream.Subscribe(response => _logger.LogInformation(JsonSerializer.Serialize(response)));
			return client;
		});
	}

	[IntegrationBddfyFact]
	async Task DiffOnTradeWebsocketWorks()
	{
		await TestConnection(ValrValues.TradePath, x =>
		{
			var client = new ValrTradeWebsocketClient(_logger, x);
			client.Streams.L1OrderBookSnapshotStream.Subscribe(response => _logger.LogInformation(JsonSerializer.Serialize(response)));
			client.Streams.L1OrderBookUpdateStream.Subscribe(response => _logger.LogInformation(JsonSerializer.Serialize(response)));

			var subscription = new Subscription(MessageType.OB_L1_DIFF, "BTCZAR");
			client.Send(new ChangeSubscriptionsRequest(subscription));
			return client;
		});
	}

	async Task TestConnection(string path, Func<WebsocketClient, ValrWebsocketClient> createClient)
	{
		var connection = new WebsocketClient(new Uri($"{ValrValues.ApiWebsocketUrl}{path}", UriKind.Absolute), () => new ClientWebSocket()
			.WithDefaultKeepAliveInterval()
			.WithAuthentication(path, ValrApi.Key, ValrApi.Secret, DateTimeOffset.UtcNow))
		{
			Name = path,
			ErrorReconnectTimeout = null
		};

		var client = createClient(connection);

		connection.ReconnectionHappened.Subscribe(info => connection.LogReconnection(_logger, info));
		connection.DisconnectionHappened.Subscribe(info => connection.LogDisconnection(_logger, info));

		await connection.Start();

		using var cancellationTokenSource = new CancellationTokenSource(3.Seconds());

		while (!cancellationTokenSource.Token.IsCancellationRequested)
		{
			client.Send(new PingRequest());
			await Task.Delay(500.Milliseconds(), CancellationToken.None);
		}

		_logger.LogInformation("Test finished");
	}
}
