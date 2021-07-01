using Valr.Client.Websocket.Messages;

namespace Valr.Client.Websocket.Responses
{
	/// <summary>
	/// Order processed message.
	/// </summary>
	public record OrderProcessedResponse : MessageBase<OrderExecution>;

	/// <summary>
	/// Valr order execution info.
	/// </summary>
	public record OrderExecution
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
		/// The failure reason.
		/// </summary>
		public string FailureReason { get; init; } = null!;
	}
}
