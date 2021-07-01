using System;
using Valr.Client.Websocket.Messages;

namespace Valr.Client.Websocket.Responses
{
	/// <summary>
	/// New pending crypto deposit message.
	/// </summary>
	public record NewPendingReceiveResponse : MessageBase<CryptoDeposit>;

	/// <summary>
	/// Valr crypto deposit.
	/// </summary>
	public record CryptoDeposit
	{
		/// <summary>
		/// The currency details.
		/// </summary>
		public CurrencyInfo Currency { get; init; } = null!;

		/// <summary>
		/// The receive address.
		/// </summary>
		public string ReceiveAddress { get; init; } = null!;

		/// <summary>
		/// The transaction hash.
		/// </summary>
		public string TransactionHash { get; init; } = null!;

		/// <summary>
		/// The amount.
		/// </summary>
		public double Amount { get; init; }

		/// <summary>
		/// Time deposit was created.
		/// </summary>
		public DateTime CreatedAt { get; init; }

		/// <summary>
		/// Number of transaction confirmations.
		/// </summary>
		public int Confirmations { get; init; }

		/// <summary>
		/// Whether or not the transaction is confirmed.
		/// </summary>
		public bool Confirmed { get; init; }
	}
}
