using System;
using System.Text.Json.Serialization;
using Valr.Client.Websocket.Json;

namespace Valr.Client.Websocket.Messages
{
	/// <summary>
	/// Valr trade base class.
	/// </summary>
	public abstract record TradeBase
	{
		/// <summary>
		/// Which trading pair the trade relates to.
		/// </summary>
		public string CurrencyPair { get; init; } = null!;

		/// <summary>
		/// The open price.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double Price { get; init; }

		/// <summary>
		/// The high price.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double Quantity { get; init; }

		/// <summary>
		/// Time trade occurred.
		/// </summary>
		public DateTime TradedAt { get; init; }
	}
}
