using System;
using System.Text.Json.Serialization;
using Valr.Client.Websocket.Json;
using Valr.Client.Websocket.Messages;

namespace Valr.Client.Websocket.Responses
{
	/// <summary>
	/// New trade bucket message.
	/// </summary>
	public record NewTradeBucketResponse : PairMessageBase<TradeBucket>;

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
		[JsonConverter(typeof(DoubleConverter))]
		public double Open { get; init; }

		/// <summary>
		/// The high price.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double High { get; init; }

		/// <summary>
		/// The low price.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double Low { get; init; }

		/// <summary>
		/// The close price.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double Close { get; init; }

		/// <summary>
		/// The base currency volume.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double Volume { get; init; }
	}
}
