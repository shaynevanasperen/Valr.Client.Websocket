using System.Collections.Generic;
using System.Text;

namespace Valr.Client.Websocket
{
	/// <summary>
	/// Record for holding Valr secrets.
	/// </summary>
	public sealed record ValrSecrets
	{
		/// <summary>
		/// The API key.
		/// </summary>
		public string ApiKey { get; init; } = null!;

		/// <summary>
		/// The API secret.
		/// </summary>
		public string ApiSecret { get; init; } = null!;

		/// <summary>
		/// Makes a collection of headers (name + value)
		/// </summary>
		/// <param name="timestamp">The timestamp.</param>
		/// <param name="payload">The payload.</param>
		/// <returns>A collection of headers.</returns>
		public IEnumerable<(string Name, string Value)> GetAuthHeaders(string timestamp, string payload)
		{
			yield return new("X-VALR-API-KEY", ApiKey);
			yield return new("X-VALR-TIMESTAMP", timestamp);
			yield return new("X-VALR-SIGNATURE", ApiSecret.SignPayloadHmacSha512(payload, Encoding.UTF8));
		}
	}
}
