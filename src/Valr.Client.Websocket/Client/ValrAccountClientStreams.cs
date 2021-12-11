using System.Reactive.Subjects;
using Valr.Client.Websocket.Responses;

namespace Valr.Client.Websocket.Client;

/// <summary>
/// All provided streams.
/// You don't need to send subscription request in advance (all are subscribed to by default).
/// </summary>
public class ValrAccountClientStreams
{
	/// <summary>
	/// Authenticated stream - emits when authentication succeeded.
	/// </summary>
	public readonly Subject<AuthenticatedResponse> AuthenticatedStream = new();

	/// <summary>
	/// Pong stream - emits in response to ping requests.
	/// </summary>
	public readonly Subject<PongResponse> PongStream = new();

	/// <summary>
	/// New account history record stream - emits every time the account history is appended to.
	/// </summary>
	public readonly Subject<NewAccountHistoryRecordResponse> NewAccountHistoryRecordStream = new();

	/// <summary>
	/// Balance updated stream - emits every time a balance is updated.
	/// </summary>
	public readonly Subject<BalanceUpdateResponse> BalanceUpdateStream = new();

	/// <summary>
	/// Account trades stream - emits when a new trade is executed on the account.
	/// </summary>
	public readonly Subject<NewAccountTradeResponse> NewAccountTradeStream = new();

	/// <summary>
	/// Instant order completed stream - emits when a new simple buy/sell is executed on the account.
	/// </summary>
	public readonly Subject<InstantOrderCompletedResponse> InstantOrderCompletedStream = new();

	/// <summary>
	/// Open orders update stream - emits when new orders are added to the open orders.
	/// </summary>
	public readonly Subject<OpenOrdersUpdateResponse> OpenOrdersUpdateStream = new();

	/// <summary>
	/// Order processed stream - emits when order is processed (accepted or rejected) after being placed.
	/// </summary>
	public readonly Subject<OrderProcessedResponse> OrderProcessedStream = new();

	/// <summary>
	/// Order status updated stream - emits when order is status changes.
	/// </summary>
	public readonly Subject<OrderStatusUpdateResponse> OrderStatusUpdateStream = new();

	/// <summary>
	/// Order cancellation failure stream - emits when order cancellation failed.
	/// </summary>
	public readonly Subject<FailedCancelOrderResponse> FailedCancelOrderStream = new();

	/// <summary>
	/// New pending receive stream - emits when a pending deposit is created.
	/// </summary>
	public readonly Subject<NewPendingReceiveResponse> NewPendingReceiveStream = new();

	/// <summary>
	/// Send status updated stream - emits when a send is updated.
	/// </summary>
	public readonly Subject<SendStatusUpdateResponse> SendStatusUpdateStream = new();
}
