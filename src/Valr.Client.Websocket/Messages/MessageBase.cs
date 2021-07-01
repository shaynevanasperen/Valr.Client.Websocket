using System.Reactive.Subjects;
using System.Text.Json;
using Valr.Client.Websocket.Json;

namespace Valr.Client.Websocket.Messages
{
	/// <summary>
	/// Base class for pair-specific messages.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract record PairMessageBase<T> : MessageBase<T>
	{
		/// <summary>
		/// Which trading pair the message relates to.
		/// </summary>
		public string CurrencyPairSymbol { get; set; } = null!;
	}

	/// <summary>
	/// Base class for non-pair-specific messages.
	/// </summary>
	/// <typeparam name="T">The data type.</typeparam>
	public abstract record MessageBase<T> : MessageBase
	{
		/// <summary>
		/// The message data.
		/// </summary>
		public T Data { get; set; } = default!;
	}

	/// <summary>
	/// Base class for all messages.
	/// </summary>
	public abstract record MessageBase
	{
		/// <summary>
		/// The type of message.
		/// </summary>
		public string Type { get; init; } = null!;

		internal static bool TryHandle<TResponse>(string supportedMessageType, string? messageType, JsonElement response, ISubject<TResponse> subject)
		{
			if (messageType == supportedMessageType)
			{
				var value = response.ToObject<TResponse>(ValrJsonOptions.Default);
				if (value != null)
				{
					subject.OnNext(value);
					return true;
				}
			}

			return false;
		}
	}
}
