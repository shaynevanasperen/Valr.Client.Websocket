using Valr.Client.Websocket.Models;

namespace Valr.Client.Websocket.Responses
{
	/// <summary>
	/// Failed cancel order message.
	/// </summary>
	public record FailedCancelOrderResponse : Message<CancelOrderFailure>;

	/// <summary>
	/// Valr cancel order failure.
	/// </summary>
	public record CancelOrderFailure
	{
		/// <summary>
		/// The order id.
		/// </summary>
		public string OrderId { get; init; } = null!;

		/// <summary>
		/// The message.
		/// </summary>
		public string Message { get; init; } = null!;
	}
}
