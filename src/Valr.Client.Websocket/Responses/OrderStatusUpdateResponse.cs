using System;
using Valr.Client.Websocket.Messages;

namespace Valr.Client.Websocket.Responses
{
	/// <summary>
	/// Order status update message.
	/// </summary>
	public record OrderStatusUpdateResponse : MessageBase<OrderStatus>;

	/// <summary>
	/// Valr order status.
	/// </summary>
	public record OrderStatus : OrderInfo
	{
		/// <summary>
		/// The order status type. One of the values in <see cref="Messages.OrderStatusType"/>.
		/// </summary>
		public string OrderStatusType { get; init; } = null!;

		/// <summary>
		/// Which side of the order book this order is in. Either "buy" or "sell".
		/// </summary>
		public string OrderSide { get; init; } = null!;

		/// <summary>
		/// The type of order.
		/// </summary>
		public string OrderType { get; init; } = null!;

		/// <summary>
		/// The failure reason.
		/// </summary>
		public string FailedReason { get; init; } = null!;

		/// <summary>
		/// Time order was created.
		/// </summary>
		public DateTime OrderCreatedAt { get; init; }

		/// <summary>
		/// Time order was updated.
		/// </summary>
		public DateTime OrderUpdatedAt { get; init; }
	}
}
