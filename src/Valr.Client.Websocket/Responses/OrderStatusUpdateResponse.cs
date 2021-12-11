using System;
using Valr.Client.Websocket.Models;

namespace Valr.Client.Websocket.Responses;

/// <summary>
/// Order status update message.
/// </summary>
public record OrderStatusUpdateResponse : Message<OrderStatus>;

/// <summary>
/// Valr order status.
/// </summary>
public record OrderStatus : OrderInfo
{
	/// <summary>
	/// The order status type. One of the values in <see cref="Models.OrderStatusType"/>.
	/// </summary>
	public string OrderStatusType { get; init; } = null!;

	/// <summary>
	/// Which side of the order book this order is in. Either "buy" or "sell".
	/// </summary>
	public string OrderSide { get; init; } = null!;

	/// <summary>
	/// The currency pair.
	/// </summary>
	public string CurrencyPair { get; init; } = null!;

	/// <summary>
	/// The type of order.
	/// </summary>
	public string OrderType { get; init; } = null!;

	/// <summary>
	/// The failure reason.
	/// </summary>
	public string FailedReason { get; init; } = null!;

	/// <summary>
	/// Time order was created.
	/// </summary>
	public DateTime OrderCreatedAt { get; init; }

	/// <summary>
	/// Time order was updated.
	/// </summary>
	public DateTime OrderUpdatedAt { get; init; }

	/// <summary>
	/// The original price.
	/// </summary>
	public double OriginalPrice { get; init; }

	/// <summary>
	/// The remaining quantity.
	/// </summary>
	public double RemainingQuantity { get; init; }

	/// <summary>
	/// The executed price.
	/// </summary>
	public double ExecutedPrice { get; init; }

	/// <summary>
	/// The executed quantity.
	/// </summary>
	public double ExecutedQuantity { get; init; }

	/// <summary>
	/// The executed fee.
	/// </summary>
	public double ExecutedFee { get; init; }
}
