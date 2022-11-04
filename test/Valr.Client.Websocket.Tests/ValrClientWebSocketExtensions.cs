using System;
using System.Globalization;
using System.Net.WebSockets;
using System.Text;

namespace Valr.Client.Websocket.Tests;

static class ValrClientWebSocketExtensions
{
	public static ClientWebSocket WithAuthentication(this ClientWebSocket clientWebSocket, string path, string apiKey, string apiSecret, DateTimeOffset utcNow)
	{
		var timestamp = utcNow.ToUnixTimeMilliseconds().ToString(CultureInfo.InvariantCulture);
		var payload = $"{timestamp}GET{path}";

		clientWebSocket.Options.SetRequestHeader("X-VALR-API-KEY", apiKey);
		clientWebSocket.Options.SetRequestHeader("X-VALR-TIMESTAMP", timestamp);
		clientWebSocket.Options.SetRequestHeader("X-VALR-SIGNATURE", apiSecret.SignPayloadHmacSha512(payload, Encoding.UTF8));

		return clientWebSocket;
	}
}
