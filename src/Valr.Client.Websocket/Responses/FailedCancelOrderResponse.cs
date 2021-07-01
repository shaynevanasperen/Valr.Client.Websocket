using Valr.Client.Websocket.Messages;

namespace Valr.Client.Websocket.Responses
{
	/// <summary>
	/// Failed cancel order message.
	/// </summary>
	public record FailedCancelOrderResponse : MessageBase<CancelOrderFailure>;

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
