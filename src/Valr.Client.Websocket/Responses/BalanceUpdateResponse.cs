using System;
using System.Text.Json.Serialization;
using Valr.Client.Websocket.Json;
using Valr.Client.Websocket.Messages;

namespace Valr.Client.Websocket.Responses
{
	/// <summary>
	/// Balance update message.
	/// </summary>
	public record BalanceUpdateResponse : MessageBase<Balance>;

	/// <summary>
	/// Valr balance.
	/// </summary>
	public record Balance
	{
		/// <summary>
		/// The debit currency details.
		/// </summary>
		public CurrencyInfo Currency { get; init; } = null!;

		/// <summary>
		/// The available value.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double Available { get; init; }

		/// <summary>
		/// The reserved value.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double Reserved { get; init; }

		/// <summary>
		/// The total value.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double Total { get; init; }

		/// <summary>
		/// Time updated.
		/// </summary>
		public DateTime UpdatedAt { get; init; }
	}
}
