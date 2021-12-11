using Valr.Client.Websocket.Models;

namespace Valr.Client.Websocket.Responses;

/// <summary>
/// New account trade message.
/// </summary>
public record NewAccountTradeResponse : Message<AccountTrade>;

/// <summary>
/// Valr account trade.
/// </summary>
public record AccountTrade : Trade
{
	/// <summary>
	/// Which side the trade was. Either "buy" or "sell".
	/// </summary>
	public string Side { get; init; } = null!;

	/// <summary>
	/// The order id.
	/// </summary>
	public string OrderId { get; init; } = null!;

	/// <summary>
	/// The trade id.
	/// </summary>
	public string Id { get; init; } = null!;
}
