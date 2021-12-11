using System;
using Valr.Client.Websocket.Models;

namespace Valr.Client.Websocket.Responses;

/// <summary>
/// New account history record message.
/// </summary>
public record NewAccountHistoryRecordResponse : Message<AccountHistoryRecord>;

/// <summary>
/// Valr trade bucket.
/// </summary>
public record AccountHistoryRecord
{
	/// <summary>
	/// The type of transaction.
	/// </summary>
	public TransactionType TransactionType { get; init; } = null!;

	/// <summary>
	/// The debit currency details.
	/// </summary>
	public CurrencyInfo DebitCurrency { get; init; } = null!;

	/// <summary>
	/// The credit currency details.
	/// </summary>
	public CurrencyInfo CreditCurrency { get; init; } = null!;

	/// <summary>
	/// The fee currency details.
	/// </summary>
	public CurrencyInfo FeeCurrency { get; init; } = null!;

	/// <summary>
	/// Additional transaction information.
	/// </summary>
	public TransactionInfo AdditionalInfo { get; init; } = null!;

	/// <summary>
	/// The debit value.
	/// </summary>
	public double DebitValue { get; init; }

	/// <summary>
	/// The credit value.
	/// </summary>
	public double CreditValue { get; init; }

	/// <summary>
	/// The fee value.
	/// </summary>
	public double FeeValue { get; init; }

	/// <summary>
	/// Time transaction occurred.
	/// </summary>
	public DateTime EventAt { get; init; }
}

/// <summary>
/// Valr transaction type.
/// </summary>
public record TransactionType
{
	/// <summary>
	/// The type.
	/// </summary>
	public string Type { get; init; } = null!;

	/// <summary>
	/// The description.
	/// </summary>
	public string Description { get; init; } = null!;
}

/// <summary>
/// Valr transaction info.
/// </summary>
public record TransactionInfo
{
	/// <summary>
	/// Which trading pair the transaction relates to.
	/// </summary>
	public string CurrencyPairSymbol { get; init; } = null!;

	/// <summary>
	/// The cost per coin symbol.
	/// </summary>
	public string CostPerCoinSymbol { get; init; } = null!;

	/// <summary>
	/// The cost per coin.
	/// </summary>
	public double CostPerCoin { get; init; }
}
