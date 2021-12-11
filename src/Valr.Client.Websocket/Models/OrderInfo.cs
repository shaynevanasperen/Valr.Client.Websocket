namespace Valr.Client.Websocket.Models;

/// <summary>
/// Valr order info.
/// </summary>
public record OrderInfo
{
	/// <summary>
	/// The order id.
	/// </summary>
	public string OrderId { get; init; } = null!;

	/// <summary>
	/// The customer order id.
	/// </summary>
	public string CustomerOrderId { get; init; } = null!;

	/// <summary>
	/// The original quantity.
	/// </summary>
	public double OriginalQuantity { get; init; }
}
