namespace Valr.Client.Websocket.Models;

/// <summary>
/// A list of all known Valr order status types.
/// </summary>
public static class OrderStatusType
{
#pragma warning disable 1591
	public const string Placed = nameof(Placed);
	public const string Failed = nameof(Failed);
	public const string Cancelled = nameof(Cancelled);
	public const string Filled = nameof(Filled);
	public const string PartiallyFilled = "Partially Filled";
	public const string InstantOrderBalanceReserveFailed = "Instant Order Balance Reserve Failed";
	public const string InstantOrderBalanceReserved = "Instant Order Balance Reserved";
	public const string InstantOrderCompleted = "Instant Order Completed";
#pragma warning restore 1591
}
