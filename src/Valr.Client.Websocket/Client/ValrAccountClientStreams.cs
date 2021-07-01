using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Valr.Client.Websocket.Responses;

namespace Valr.Client.Websocket.Client
{
	/// <summary>
	/// All provided streams.
	/// You don't need to send subscription request in advance (all are subscribed to by default)
	/// </summary>
	public class ValrAccountClientStreams
	{
		internal readonly Subject<PongResponse> PongSubject = new();
		internal readonly Subject<NewAccountHistoryRecordResponse> NewAccountHistoryRecordSubject = new();
		internal readonly Subject<BalanceUpdateResponse> BalanceUpdateSubject = new();
		internal readonly Subject<NewAccountTradeResponse> NewAccountTradeSubject = new();
		internal readonly Subject<InstantOrderCompletedResponse> InstantOrderCompletedSubject = new();
		internal readonly Subject<OpenOrdersUpdateResponse> OpenOrdersUpdateSubject = new();
		internal readonly Subject<OrderProcessedResponse> OrderProcessedSubject = new();
		internal readonly Subject<OrderStatusUpdateResponse> OrderStatusUpdateSubject = new();
		internal readonly Subject<FailedCancelOrderResponse> FailedCancelOrderSubject = new();
		internal readonly Subject<NewPendingReceiveResponse> NewPendingReceiveSubject = new();
		internal readonly Subject<SendStatusUpdateResponse> SendStatusUpdateSubject = new();

		/// <summary>
		/// Pong stream - emits in response to ping requests
		/// </summary>
		public IObservable<PongResponse> PongStream => PongSubject.AsObservable();

		/// <summary>
		/// New account history record stream - emits every time the account history is appended to
		/// </summary>
		public IObservable<NewAccountHistoryRecordResponse> NewAccountHistoryRecordStream => NewAccountHistoryRecordSubject.AsObservable();

		/// <summary>
		/// Balance updated stream - emits every time a balance is updated
		/// </summary>
		public IObservable<BalanceUpdateResponse> BalanceUpdateStream => BalanceUpdateSubject.AsObservable();

		/// <summary>
		/// Account trades stream - emits when a new trade is executed on the account
		/// </summary>
		public IObservable<NewAccountTradeResponse> NewAccountTradeStream => NewAccountTradeSubject.AsObservable();

		/// <summary>
		/// Instant order completed stream - emits when a new simple buy/sell is executed on the account
		/// </summary>
		public IObservable<InstantOrderCompletedResponse> InstantOrderCompletedStream => InstantOrderCompletedSubject.AsObservable();

		/// <summary>
		/// Open orders update stream - emits when new orders are added to the open orders
		/// </summary>
		public IObservable<OpenOrdersUpdateResponse> OpenOrdersUpdateStream => OpenOrdersUpdateSubject.AsObservable();

		/// <summary>
		/// Order processed stream - emits when order is processed (accepted or rejected) after being placed
		/// </summary>
		public IObservable<OrderProcessedResponse> OrderProcessedStream => OrderProcessedSubject.AsObservable();

		/// <summary>
		/// Order status updated stream - emits when order is status changes
		/// </summary>
		public IObservable<OrderStatusUpdateResponse> OrderStatusUpdateStream => OrderStatusUpdateSubject.AsObservable();

		/// <summary>
		/// Order cancellation failure stream - emits when order cancellation failed
		/// </summary>
		public IObservable<FailedCancelOrderResponse> FailedCancelOrderStream => FailedCancelOrderSubject.AsObservable();

		/// <summary>
		/// New pending receive stream - emits when a pending deposit is created
		/// </summary>
		public IObservable<NewPendingReceiveResponse> NewPendingReceiveStream => NewPendingReceiveSubject.AsObservable();

		/// <summary>
		/// Send status updated stream - emits when a send is updated
		/// </summary>
		public IObservable<SendStatusUpdateResponse> SendStatusUpdateStream => SendStatusUpdateSubject.AsObservable();
	}
}
