using System.Reactive.Subjects;
using Valr.Client.Websocket.Responses;

namespace Valr.Client.Websocket.Client;

/// <summary>
/// All provided streams.
/// You need to send subscription request in advance (via method `Send()` on ValrTradeWebsocketClient).
/// </summary>
public class ValrTradeClientStreams
{
	/// <summary>
	/// Authenticated stream - emits when authentication succeeded.
	/// </summary>
	public readonly Subject<AuthenticatedResponse> AuthenticatedStream = new();

	/// <summary>
	/// Subscribed stream - emits when a subscription is added/updated.
	/// </summary>
	public readonly Subject<SubscribedResponse> SubscribedStream = new();

	/// <summary>
	/// Subscribed stream - emits when a subscription is removed.
	/// </summary>
	public readonly Subject<UnsubscribedResponse> UnsubscribedStream = new();

	/// <summary>
	/// Pong stream - emits in response to ping requests.
	/// </summary>
	public readonly Subject<PongResponse> PongStream = new();

	/// <summary>
	/// Aggregated order book update stream - emits for every change to the order book.
	/// </summary>
	public readonly Subject<AggregatedOrderBookUpdateResponse> AggregatedOrderBookUpdateStream = new();

	/// <summary>
	/// Full order book snapshot stream - emits once when subscribing to the update stream.
	/// </summary>
	public readonly Subject<FullOrderBookResponse> FullOrderBookSnapshotStream = new();

	/// <summary>
	/// Full order book update stream - emits for every change to the order book.
	/// </summary>
	public readonly Subject<FullOrderBookResponse> FullOrderBookUpdateStream = new();

	/// <summary>
	/// Efficient aggregated order book snapshot stream - emits once when subscribing to the update stream.
	/// </summary>
	public readonly Subject<L1TrackedOrderBookResponse> L1OrderBookSnapshotStream = new();

	/// <summary>
	/// Efficient aggregated order book update stream - emits for every change to the order book.
	/// </summary>
	public readonly Subject<L1TrackedOrderBookResponse> L1OrderBookUpdateStream = new();

	/// <summary>
	/// Efficient aggregated order book snapshot stream (depth 1) - emits for every change to the top level of the order book.
	/// </summary>
	public readonly Subject<L1OrderBookResponse> L1D1OrderBookSnapshotStream = new();

	/// <summary>
	/// Efficient aggregated order book snapshot stream (depth 5) - emits for every change to the top 5 levels of the order book.
	/// </summary>
	public readonly Subject<L1OrderBookResponse> L1D5OrderBookSnapshotStream = new();

	/// <summary>
	/// Efficient aggregated order book snapshot stream (depth 10) - emits for every change to the top 10 levels of the order book.
	/// </summary>
	public readonly Subject<L1OrderBookResponse> L1D10OrderBookSnapshotStream = new();

	/// <summary>
	/// Efficient aggregated order book snapshot stream (depth 20) - emits for every change to the top 20 levels of the order book.
	/// </summary>
	public readonly Subject<L1OrderBookResponse> L1D20OrderBookSnapshotStream = new();

	/// <summary>
	/// Efficient aggregated order book snapshot stream (depth 40) - emits for every change to the top 40 levels of the order book.
	/// </summary>
	public readonly Subject<L1OrderBookResponse> L1D40OrderBookSnapshotStream = new();

	/// <summary>
	/// Efficient aggregated order book snapshot stream (depth 80) - emits for every change to the top 80 levels of the order book.
	/// </summary>
	public readonly Subject<L1OrderBookResponse> L1D80OrderBookSnapshotStream = new();

	/// <summary>
	/// Allowed order types updated stream - emits when the allowed order types change.
	/// </summary>
	public readonly Subject<AllowedOrderTypesUpdatedResponse> AllowedOrderTypesUpdatedStream = new();

	/// <summary>
	/// Market summary update stream - emits for every summary update sent by the server.
	/// </summary>
	public readonly Subject<MarketSummaryUpdateResponse> MarketSummaryUpdateStream = new();

	/// <summary>
	/// New trade bucket stream - emits every bucket update sent by the server.
	/// </summary>
	public readonly Subject<NewTradeBucketResponse> NewTradeBucketStream = new();

	/// <summary>
	/// New trade stream - emits for every new trade that occurs.
	/// </summary>
	public readonly Subject<NewTradeResponse> NewTradeStream = new();
}
