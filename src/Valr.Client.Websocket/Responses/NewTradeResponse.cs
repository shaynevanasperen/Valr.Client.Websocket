using Valr.Client.Websocket.Messages;

namespace Valr.Client.Websocket.Responses
{
	/// <summary>
	/// New trade message.
	/// </summary>
	public record NewTradeResponse : PairMessageBase<Trade>;

	/// <summary>
	/// Valr trade.
	/// </summary>
	public record Trade : TradeBase
	{
		/// <summary>
		/// Which side of the trade was the taker. Either "buy" or "sell".
		/// </summary>
		public string TakerSide { get; init; } = null!;
	}
}
