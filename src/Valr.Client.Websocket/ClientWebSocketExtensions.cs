using System;
using System.Globalization;
using System.Net.WebSockets;
using System.Reactive.PlatformServices;

namespace Valr.Client.Websocket
{
	/// <summary>
	/// Extension methods for <see cref="ClientWebSocket"/>.
	/// </summary>
	public static class ClientWebSocketExtensions
	{
		/// <summary>
		/// Sets the authentication headers and returns this instance.
		/// </summary>
		/// <param name="clientWebSocket">The websocket.</param>
		/// <param name="path">The relative path.</param>
		/// <param name="secrets">The secrets.</param>
		/// <param name="clock">A system clock.</param>
		/// <returns>The current instance.</returns>
		public static ClientWebSocket WithAuthentication(this ClientWebSocket clientWebSocket, string path, ValrSecrets secrets, ISystemClock clock)
		{
			var timestamp = clock.UtcNow.ToUnixTimeMilliseconds().ToString(CultureInfo.InvariantCulture);
			var payload = $"{timestamp}GET{path}";

			foreach (var (name, value) in secrets.GetAuthHeaders(timestamp, payload))
				clientWebSocket.Options.SetRequestHeader(name, value);

			return clientWebSocket;
		}

		/// <summary>
		/// Sets the KeepAlive interval and returns this instance.
		/// </summary>
		/// <param name="clientWebSocket">The websocket.</param>
		/// <param name="interval">The interval.</param>
		/// <returns>The current instance.</returns>
		public static ClientWebSocket WithKeepAliveInterval(this ClientWebSocket clientWebSocket, TimeSpan interval)
		{
			clientWebSocket.Options.KeepAliveInterval = interval;
			return clientWebSocket;
		}
	}
}
