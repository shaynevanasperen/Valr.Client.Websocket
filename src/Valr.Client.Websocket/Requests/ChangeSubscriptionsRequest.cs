using System.Collections.Generic;
using Valr.Client.Websocket.Messages;

namespace Valr.Client.Websocket.Requests
{
	/// <summary>
	/// A request to change the subscriptions.
	/// </summary>
	public record ChangeSubscriptionsRequest : MessageBase
	{
		/// <summary>
		/// Creates a new instance.
		/// </summary>
		public ChangeSubscriptionsRequest(params Subscription[] subscriptions)
		{
			Type = MessageType.SUBSCRIBE;
			Subscriptions = subscriptions;
		}

		/// <summary>
		/// The subscriptions to change.
		/// </summary>
		public IReadOnlyCollection<Subscription> Subscriptions { get; init; }
	}

	/// <summary>
	/// A subscription definition.
	/// </summary>
	public record Subscription
	{
		/// <summary>
		/// Creates a new instance.
		/// </summary>
		/// <param name="event">The event for which to change subscriptions.</param>
		/// <param name="pairs">The pairs that should be subscribed.</param>
		public Subscription(string @event, params string[] pairs)
		{
			Event = @event;
			Pairs = pairs;
		}

		/// <summary>
		/// The event being referred to.
		/// </summary>
		public string Event { get; init; }

		/// <summary>
		/// Which trading pairs should be subscribed.
		/// </summary>
		public IReadOnlyCollection<string> Pairs { get; init; }
	}
}
