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
