using System;
using Valr.Client.Websocket.Models;

namespace Valr.Client.Websocket.Responses;

/// <summary>
/// Instant order completed message.
/// </summary>
public record InstantOrderCompletedResponse : Message<InstantOrderInfo>;

/// <summary>
/// Valr instant order info.
/// </summary>
public record InstantOrderInfo
{
	/// <summary>
	/// The order id.
	/// </summary>
	public string OrderId { get; init; } = null!;

	/// <summary>
	/// Whether or not the order was successful.
	/// </summary>
	public bool Success { get; init; }

	/// <summary>
	/// The amount spent.
	/// </summary>
	public double PaidAmount { get; init; }

	/// <summary>
	/// The amount received.
	/// </summary>
	public double ReceivedAmount { get; init; }

	/// <summary>
	/// The amount paid in fees.
	/// </summary>
	public double FeeAmount { get; init; }

	/// <summary>
	/// The currency spent.
	/// </summary>
	public string PaidCurrency { get; init; } = null!;

	/// <summary>
	/// The currency received.
	/// </summary>
	public string ReceivedCurrency { get; init; } = null!;

	/// <summary>
	/// The currency for fees.
	/// </summary>
	public string FeeCurrency { get; init; } = null!;

	/// <summary>
	/// Time order was executed.
	/// </summary>
	public DateTime OrderExecutedAt { get; init; }
}
