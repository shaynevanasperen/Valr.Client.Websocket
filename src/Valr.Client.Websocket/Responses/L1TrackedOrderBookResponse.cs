using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Valr.Client.Websocket.Models;

namespace Valr.Client.Websocket.Responses;

/// <summary>
/// L1 order book snapshot/update message.
/// </summary>
public record L1TrackedOrderBookResponse : EfficientPairMessage<L1TrackedOrderBook>;

/// <summary>
/// Valr order book.
/// </summary>
public record L1TrackedOrderBook : L1OrderBook
{
	/// <summary>
	/// The sequence number.
	/// </summary>
	[JsonPropertyName("sq")]
	public long SequenceNumber { get; init; }

	/// <summary>
	/// The checksum.
	/// </summary>
	[JsonPropertyName("cs")]
	public uint Checksum { get; init; }

	/// <summary>
	/// Validates that the checksum matches the value we calculate ourselves.
	/// </summary>
	/// <returns>True if valid, otherwise false.</returns>
	public bool ValidateChecksum()
	{
		var orders = new List<string>();

		for (var i = 0; i < 25; i++)
		{
			if (Bids.Count > i)
				orders.Add($"{Bids[i].Price}:${Bids[i].Quantity}");
			if (Bids.Count > i)
				orders.Add($"{Asks[i].Price}:${Asks[i].Quantity}");
		}

		var inputBytes = Encoding.UTF8.GetBytes(string.Join(":", orders));

		var crc32 = new System.IO.Hashing.Crc32();
		crc32.Append(inputBytes);
		var checksum = BitConverter.ToUInt32(crc32.GetCurrentHash(), 0);
		return checksum == Checksum;
	}
}
