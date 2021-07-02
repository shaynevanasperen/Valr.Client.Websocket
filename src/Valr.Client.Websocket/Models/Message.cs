using System;
using System.Reactive.Subjects;
using System.Text.Json;
using Valr.Client.Websocket.Json;

namespace Valr.Client.Websocket.Models
{
	/// <summary>
	/// Base class for pair-specific messages.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract record PairMessage<T> : Message<T>
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
	public abstract record Message<T> : Message
	{
		/// <summary>
		/// The message data.
		/// </summary>
		public T Data { get; set; } = default!;
	}

	/// <summary>
	/// Base class for all messages.
	/// </summary>
	public abstract record Message
	{
		/// <summary>
		/// The type of message.
		/// </summary>
		public string Type { get; init; } = null!;

		internal static bool TryHandle<TResponse>(string supportedMessageType, string? messageType, JsonElement response, ISubject<TResponse> subject)
		{
			if (messageType == supportedMessageType)
			{
				TResponse? value;
				try
				{
					value = response.ToObject<TResponse>(ValrJsonOptions.Default);
				}
				catch (Exception exception)
				{
					throw new Exception($"Failed to deserialize JSON: {JsonSerializer.Serialize(response)}", exception);
				}

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
