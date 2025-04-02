using System.Collections.Generic;

namespace Valr.Client.Websocket.Responses;

/// <summary>
/// Valr price and amount.
/// </summary>
public class L1Quote : List<double>
{
	/// <summary>
	/// The price.
	/// </summary>
	public double Price => this[0];

	/// <summary>
	/// The amount.
	/// </summary>
	public double Quantity => this[1];
}
