using System;
using Valr.Client.Websocket.Models;

namespace Valr.Client.Websocket.Responses
{
	/// <summary>
	/// New trade bucket message.
	/// </summary>
	public record NewTradeBucketResponse : PairMessage<TradeBucket>;

	/// <summary>
	/// Valr trade bucket.
	/// </summary>
	public record TradeBucket
	{
		/// <summary>
		/// Which trading pair the data relates to.
		/// </summary>
		public string CurrencyPairSymbol { get; init; } = null!;

		/// <summary>
		/// Bucket period in seconds.
		/// </summary>
		public int BucketPeriodInSeconds { get; init; }

		/// <summary>
		/// Time bucket period started.
		/// </summary>
		public DateTime StartTime { get; init; }

		/// <summary>
		/// The open price.
		/// </summary>
		public double Open { get; init; }

		/// <summary>
		/// The high price.
		/// </summary>
		public double High { get; init; }

		/// <summary>
		/// The low price.
		/// </summary>
		public double Low { get; init; }

		/// <summary>
		/// The close price.
		/// </summary>
		public double Close { get; init; }

		/// <summary>
		/// The base currency volume.
		/// </summary>
		public double Volume { get; init; }
	}
}
