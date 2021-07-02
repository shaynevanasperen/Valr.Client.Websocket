namespace Valr.Client.Websocket.Models
{
	/// <summary>
	/// Valr currency info.
	/// </summary>
	public record CurrencyInfo
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
		/// The short name.
		/// </summary>
		public string ShortName { get; init; } = null!;

		/// <summary>
		/// The long name.
		/// </summary>
		public string LongName { get; init; } = null!;

		/// <summary>
		/// The supported trading/accounting decimal places.
		/// </summary>
		public int DecimalPlaces { get; init; }

		/// <summary>
		/// The supported withdrawal decimal places.
		/// </summary>
		public int SupportedWithdrawDecimalPlaces { get; init; }

		/// <summary>
		/// Whether or not the currency is active.
		/// </summary>
		public bool IsActive { get; init; }
	}
}
