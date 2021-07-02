using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Valr.Client.Websocket.Responses;

namespace Valr.Client.Websocket.Client
{
	/// <summary>
	/// All provided streams.
	/// You need to send subscription request in advance (via method `SendOnTradeWebsocket()` on ValrWebsocketClient)
	/// </summary>
	public class ValrTradeClientStreams
	{
		internal readonly Subject<AuthenticatedResponse> AuthenticatedSubject = new();
		internal readonly Subject<SubscribedResponse> SubscribedSubject = new();
		internal readonly Subject<UnsubscribedResponse> UnsubscribedSubject = new();
		internal readonly Subject<PongResponse> PongSubject = new();
		internal readonly Subject<AggregatedOrderBookUpdateResponse> AggregatedOrderBookUpdateSubject = new();
		internal readonly Subject<MarketSummaryUpdateResponse> MarketSummaryUpdateSubject = new();
		internal readonly Subject<NewTradeBucketResponse> NewTradeBucketSubject = new();
		internal readonly Subject<NewTradeResponse> NewTradeSubject = new();

		/// <summary>
		/// Authenticated stream - emits when authentication succeeded
		/// </summary>
		public IObservable<AuthenticatedResponse> AuthenticatedStream => AuthenticatedSubject.AsObservable();

		/// <summary>
		/// Subscribed stream - emits when a subscription is added/updated
		/// </summary>
		public IObservable<SubscribedResponse> SubscribedStream => SubscribedSubject.AsObservable();

		/// <summary>
		/// Subscribed stream - emits when a subscription is removed
		/// </summary>
		public IObservable<UnsubscribedResponse> UnsubscribedStream => UnsubscribedSubject.AsObservable();

		/// <summary>
		/// Pong stream - emits in response to ping requests
		/// </summary>
		public IObservable<PongResponse> PongStream => PongSubject.AsObservable();

		/// <summary>
		/// Aggregated order book update stream - emits for every change to the order book
		/// </summary>
		public IObservable<AggregatedOrderBookUpdateResponse> AggregatedOrderBookUpdateStream => AggregatedOrderBookUpdateSubject.AsObservable();

		/// <summary>
		/// Market summary update stream - emits for every summary update sent by the server
		/// </summary>
		public IObservable<MarketSummaryUpdateResponse> MarketSummaryUpdateStream => MarketSummaryUpdateSubject.AsObservable();

		/// <summary>
		/// New trade bucket stream - emits every bucket update sent by the server
		/// </summary>
		public IObservable<NewTradeBucketResponse> NewTradeBucketStream => NewTradeBucketSubject.AsObservable();

		/// <summary>
		/// New trade stream - emits for every new trade that occurs
		/// </summary>
		public IObservable<NewTradeResponse> NewTradeStream => NewTradeSubject.AsObservable();
	}
}
