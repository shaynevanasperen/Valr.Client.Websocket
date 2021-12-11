using System;
using Valr.Client.Websocket.Models;

namespace Valr.Client.Websocket.Responses;

/// <summary>
/// Balance update message.
/// </summary>
public record BalanceUpdateResponse : Message<Balance>;

/// <summary>
/// Valr balance.
/// </summary>
public record Balance
{
	/// <summary>
	/// The debit currency details.
	/// </summary>
	public CurrencyInfo Currency { get; init; } = null!;

	/// <summary>
	/// The available value.
	/// </summary>
	public double Available { get; init; }

	/// <summary>
	/// The reserved value.
	/// </summary>
	public double Reserved { get; init; }

	/// <summary>
	/// The total value.
	/// </summary>
	public double Total { get; init; }

	/// <summary>
	/// Time updated.
	/// </summary>
	public DateTime UpdatedAt { get; init; }
}
