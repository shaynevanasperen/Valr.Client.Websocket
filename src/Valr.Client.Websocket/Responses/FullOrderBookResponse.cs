using System;
using System.Collections.Generic;
using System.Text;
using Valr.Client.Websocket.Models;

namespace Valr.Client.Websocket.Responses;

/// <summary>
/// Full order book snapshot/update message.
/// </summary>
public record FullOrderBookResponse : PairMessage<FullOrderBook>;

/// <summary>
/// Valr order book.
/// </summary>
public class FullOrderBook
{
	/// <summary>
	/// The asks in ascending order of price.
	/// </summary>
	public IReadOnlyList<Level> Asks { get; init; } = null!;

	/// <summary>
	/// The bids in descending order of price.
	/// </summary>
	public IReadOnlyList<Level> Bids { get; init; } = null!;

	/// <summary>
	/// The last time the order book changed.
	/// </summary>
	public DateTime LastChange { get; init; }

	/// <summary>
	/// The sequence number.
	/// </summary>
	public long SequenceNumber { get; init; }

	/// <summary>
	/// The checksum.
	/// </summary>
	public uint Checksum { get; init; }

	/// <summary>
	/// Validates that the checksum matches the value we calculate ourselves.
	/// </summary>
	/// <returns>True if valid, otherwise false.</returns>
	public bool ValidateChecksum()
	{
		var bids = SelectBestOrders(Bids);
		var asks = SelectBestOrders(Asks);

		var orders = new List<string>();
		for (var i = 0; i < 25; i++)
		{
			if (i < bids.Count)
				orders.Add(bids[i]);
			if (i < asks.Count)
				orders.Add(asks[i]);
		}

		var inputBytes = Encoding.UTF8.GetBytes(string.Join(":", orders));

		var crc32 = new System.IO.Hashing.Crc32();
		crc32.Append(inputBytes);
		var checksum = BitConverter.ToUInt32(crc32.GetCurrentHash(), 0);
		return checksum == Checksum;

		List<string> SelectBestOrders(IReadOnlyList<Level> side)
		{
			var list = new List<string>();
			foreach (var priceLevel in side)
			{
				foreach (var order in priceLevel.Orders)
				{
					if (list.Count == 25)
						return list;
					list.Add($"{order.OrderId}:{order.Quantity}");
				}
			}
			return list;
		}
	}
}

/// <summary>
/// Represents a trading level with a specified price and an array of associated orders.
/// </summary>
public record Level
{
	/// <summary>
	/// The price.
	/// </summary>
	public double Price { get; init; }

	/// <summary>
	/// The orders.
	/// </summary>
	public Order[] Orders { get; init; } = null!;
}

/// <summary>
/// Represents an order with an identifier and a quantity.
/// </summary>
public record Order
{
	/// <summary>
	/// The order id.
	/// </summary>
	public string OrderId { get; init; } = null!;

	/// <summary>
	/// The amount.
	/// </summary>
	public string Quantity { get; init; } = null!;
}
