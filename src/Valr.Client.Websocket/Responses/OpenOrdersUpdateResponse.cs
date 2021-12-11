using System;
using System.Collections.Generic;
using Valr.Client.Websocket.Models;

namespace Valr.Client.Websocket.Responses;

/// <summary>
/// Open orders update message.
/// </summary>
public record OpenOrdersUpdateResponse : Message<IReadOnlyCollection<OpenOrder>>;

/// <summary>
/// Valr open order info.
/// </summary>
public record OpenOrder : OrderInfo
{
	/// <summary>
	/// Which side of the order book this order is in. Either "buy" or "sell".
	/// </summary>
	public string Side { get; init; } = null!;

	/// <summary>
	/// The currency pair.
	/// </summary>
	public string CurrencyPair { get; init; } = null!;

	/// <summary>
	/// The type of order.
	/// </summary>
	public string Type { get; init; } = null!;

	/// <summary>
	/// The status of the order. One of the values in <see cref="OrderStatusType"/>.
	/// </summary>
	public string Status { get; init; } = null!;

	/// <summary>
	/// The time in force. Either "GTC", "IOC", or "FOK".
	/// </summary>
	public string TimeInForce { get; init; } = null!;

	/// <summary>
	/// Time order was created.
	/// </summary>
	public DateTime CreatedAt { get; init; }

	/// <summary>
	/// Time order was updated.
	/// </summary>
	public DateTime UpdatedAt { get; init; }

	/// <summary>
	/// The price.
	/// </summary>
	public double Price { get; init; }

	/// <summary>
	/// The quantity.
	/// </summary>
	public double Quantity { get; init; }

	/// <summary>
	/// The filled percentage.
	/// </summary>
	public double FilledPercentage { get; init; }
}
