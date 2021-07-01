using System;
using System.Text.Json.Serialization;
using Valr.Client.Websocket.Json;
using Valr.Client.Websocket.Messages;

namespace Valr.Client.Websocket.Responses
{
	/// <summary>
	/// Market summary update message.
	/// </summary>
	public record MarketSummaryUpdateResponse : PairMessageBase<MarketSummary>;

	/// <summary>
	/// Valr market summary snapshot.
	/// </summary>
	public record MarketSummary
	{
		/// <summary>
		/// Which trading pair the summary relates to.
		/// </summary>
		public string CurrencyPairSymbol { get; init; } = null!;

		/// <summary>
		/// The best ask price.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double AskPrice { get; init; }

		/// <summary>
		/// The best bid price.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double BidPrice { get; init; }

		/// <summary>
		/// The last traded price.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double LastTradedPrice { get; init; }

		/// <summary>
		/// The previous close price.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double PreviousClosePrice { get; init; }

		/// <summary>
		/// The base currency volume.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double BaseVolume { get; init; }

		/// <summary>
		/// The high price.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double HighPrice { get; init; }

		/// <summary>
		/// The low price.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double LowPrice { get; init; }

		/// <summary>
		/// Time created.
		/// </summary>
		public DateTime Created { get; init; }

		/// <summary>
		/// The change from previous.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double ChangePromPrevious { get; init; }
	}
}
