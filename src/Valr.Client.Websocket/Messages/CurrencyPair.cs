namespace Valr.Client.Websocket.Messages
{
	/// <summary>
	/// Valr currency pair info.
	/// </summary>
	public record CurrencyPair
	{
		/// <summary>
		/// The id.
		/// </summary>
		public int Id { get; init; }

		/// <summary>
		/// The symbol.
		/// </summary>
		public string Symbol { get; init; } = null!;

		/// <summary>
		/// The base currency details.
		/// </summary>
		public CurrencyInfo BaseCurrency { get; init; } = null!;

		/// <summary>
		/// The quote currency details.
		/// </summary>
		public CurrencyInfo QuoteCurrency { get; init; } = null!;

		/// <summary>
		/// The short name.
		/// </summary>
		public string ShortName { get; init; } = null!;

		/// <summary>
		/// The exchange.
		/// </summary>
		public string Exchange { get; init; } = null!;

		/// <summary>
		/// Whether or not the currency pair is active.
		/// </summary>
		public bool IsActive { get; init; }

		/// <summary>
		/// The minimum base amount.
		/// </summary>
		public double MinBaseAmount { get; init; }

		/// <summary>
		/// The maximum base amount.
		/// </summary>
		public double MaxBaseAmount { get; init; }

		/// <summary>
		/// The minimum quote amount.
		/// </summary>
		public double MinQuoteAmount { get; init; }

		/// <summary>
		/// The maximum quote amount.
		/// </summary>
		public double MaxQuoteAmount { get; init; }
	}
}
