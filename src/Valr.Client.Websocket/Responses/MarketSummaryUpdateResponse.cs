using System;
using Valr.Client.Websocket.Models;

namespace Valr.Client.Websocket.Responses
{
	/// <summary>
	/// Market summary update message.
	/// </summary>
	public record MarketSummaryUpdateResponse : PairMessage<MarketSummary>;

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
		public double AskPrice { get; init; }

		/// <summary>
		/// The best bid price.
		/// </summary>
		public double BidPrice { get; init; }

		/// <summary>
		/// The last traded price.
		/// </summary>
		public double LastTradedPrice { get; init; }

		/// <summary>
		/// The previous close price.
		/// </summary>
		public double PreviousClosePrice { get; init; }

		/// <summary>
		/// The base currency volume.
		/// </summary>
		public double BaseVolume { get; init; }

		/// <summary>
		/// The high price.
		/// </summary>
		public double HighPrice { get; init; }

		/// <summary>
		/// The low price.
		/// </summary>
		public double LowPrice { get; init; }

		/// <summary>
		/// Time created.
		/// </summary>
		public DateTime Created { get; init; }

		/// <summary>
		/// The change from previous.
		/// </summary>
		public double ChangePromPrevious { get; init; }
	}
}
