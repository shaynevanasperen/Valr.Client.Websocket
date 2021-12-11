using System;
using System.Collections.Generic;
using Valr.Client.Websocket.Models;

namespace Valr.Client.Websocket.Responses;

/// <summary>
/// Aggregated order book update message.
/// </summary>
public record AggregatedOrderBookUpdateResponse : PairMessage<OrderBook>;

/// <summary>
/// Valr order book snapshot.
/// </summary>
public record OrderBook
{
	/// <summary>
	/// The asks in ascending order of price.
	/// </summary>
	public IReadOnlyList<Quote> Asks { get; init; } = null!;

	/// <summary>
	/// The bids in descending order of price.
	/// </summary>
	public IReadOnlyList<Quote> Bids { get; init; } = null!;

	/// <summary>
	/// The last time the order book changed.
	/// </summary>
	public DateTime LastChange { get; init; }
}

/// <summary>
/// A row in the order book.
/// </summary>
public record Quote
{
	/// <summary>
	/// Which side of the order book this quote belongs in. Either "buy" or "sell".
	/// </summary>
	public string Side { get; init; } = null!;

	/// <summary>
	/// The currency pair.
	/// </summary>
	public string CurrencyPair { get; init; } = null!;

	/// <summary>
	/// The number of orders at this price.
	/// </summary>
	public int OrderCount { get; init; }

	/// <summary>
	/// The price.
	/// </summary>
	public double Price { get; init; }

	/// <summary>
	/// The amount.
	/// </summary>
	public double Quantity { get; init; }
}
