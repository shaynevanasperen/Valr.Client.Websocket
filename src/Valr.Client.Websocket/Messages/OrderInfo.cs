using System;
using System.Text.Json.Serialization;
using Valr.Client.Websocket.Json;

namespace Valr.Client.Websocket.Messages
{
	/// <summary>
	/// Valr order info.
	/// </summary>
	public record OrderInfo
	{
		/// <summary>
		/// The order id.
		/// </summary>
		public string OrderId { get; init; } = null!;

		/// <summary>
		/// The customer order id.
		/// </summary>
		public string CustomerOrderId { get; init; } = null!;

		/// <summary>
		/// The currency pair info.
		/// </summary>
		public CurrencyPair CurrencyPair { get; init; } = null!;

		/// <summary>
		/// The remaining quantity.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double RemainingQuantity { get; init; }

		/// <summary>
		/// The original price.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double OriginalPrice { get; init; }

		/// <summary>
		/// The original quantity.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double OriginalQuantity { get; init; }
	}
}
