using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Valr.Client.Websocket.Json;
using Valr.Client.Websocket.Models;

namespace Valr.Client.Websocket.Responses;

/// <summary>
/// L1 order book snapshot/update message.
/// </summary>
public record L1OrderBookResponse : EfficientPairMessage<L1OrderBook>;

/// <summary>
/// Valr order book.
/// </summary>
public record L1OrderBook
{
	/// <summary>
	/// The asks in ascending order of price.
	/// </summary>
	[JsonPropertyName("a")]
	public IReadOnlyList<L1Quote> Asks { get; init; } = null!;

	/// <summary>
	/// The bids in descending order of price.
	/// </summary>
	[JsonPropertyName("b")]
	public IReadOnlyList<L1Quote> Bids { get; init; } = null!;

	/// <summary>
	/// The last time the order book changed.
	/// </summary>
	[JsonPropertyName("lc")]
	[JsonConverter(typeof(DateTimeIntegerMillisecondsConverter))]
	public DateTime LastChange { get; init; }
}
