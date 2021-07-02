using System;

namespace Valr.Client.Websocket.Models
{
	/// <summary>
	/// Valr trade base class.
	/// </summary>
	public abstract record Trade
	{
		/// <summary>
		/// Which trading pair the trade relates to.
		/// </summary>
		public string CurrencyPair { get; init; } = null!;

		/// <summary>
		/// The open price.
		/// </summary>
		public double Price { get; init; }

		/// <summary>
		/// The high price.
		/// </summary>
		public double Quantity { get; init; }

		/// <summary>
		/// Time trade occurred.
		/// </summary>
		public DateTime TradedAt { get; init; }
	}
}
