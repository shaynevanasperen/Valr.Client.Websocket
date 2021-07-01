using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Valr.Client.Websocket.Json;
using Valr.Client.Websocket.Messages;

namespace Valr.Client.Websocket.Responses
{
	/// <summary>
	/// Open orders update message.
	/// </summary>
	public record OpenOrdersUpdateResponse : MessageBase<IReadOnlyCollection<OpenOrder>>;

	/// <summary>
	/// Valr open order info.
	/// </summary>
	public record OpenOrder : OrderInfo
	{
		/// <summary>
		/// Which side of the order book this order is in. Either "buy" or "sell".
		/// </summary>
		public string Side { get; init; } = null!;

		/// <summary>
		/// Time order was created.
		/// </summary>
		public DateTime CreatedAt { get; init; }

		/// <summary>
		/// The filled percentage.
		/// </summary>
		[JsonConverter(typeof(DoubleConverter))]
		public double FilledPercentage { get; init; }
	}
}
