using System;
using System.Text.Json.Serialization;
using Valr.Client.Websocket.Json;
using Valr.Client.Websocket.Messages;

namespace Valr.Client.Websocket.Responses
{
	/// <summary>
	/// Instant order completed message.
	/// </summary>
	public record InstantOrderCompletedResponse : MessageBase<InstantOrderInfo>;

	/// <summary>
	/// Valr instant order info.
	/// </summary>
	public record InstantOrderInfo
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
		/// The amount spent.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double PaidAmount { get; init; }

		/// <summary>
		/// The amount received.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double ReceivedAmount { get; init; }

		/// <summary>
		/// The amount paid in fees.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double FeeAmount { get; init; }

		/// <summary>
		/// The currency spent.
		/// </summary>
		public string PaidCurrency { get; init; } = null!;

		/// <summary>
		/// The currency received.
		/// </summary>
		public string ReceivedCurrency { get; init; } = null!;

		/// <summary>
		/// The currency for fees.
		/// </summary>
		public string FeeCurrency { get; init; } = null!;

		/// <summary>
		/// Time order was executed.
		/// </summary>
		public DateTime OrderExecutedAt { get; init; }
	}
}
