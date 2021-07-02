using Valr.Client.Websocket.Models;

namespace Valr.Client.Websocket.Responses
{
	/// <summary>
	/// New trade message.
	/// </summary>
	public record NewTradeResponse : PairMessage<NewTrade>;

	/// <summary>
	/// Valr trade.
	/// </summary>
	public record NewTrade : Trade
	{
		/// <summary>
		/// Which side of the trade was the taker. Either "buy" or "sell".
		/// </summary>
		public string TakerSide { get; init; } = null!;
	}
}
