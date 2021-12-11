using System;
using Valr.Client.Websocket.Models;

namespace Valr.Client.Websocket.Client;

/// <summary>
/// Valr websocket client.
/// </summary>
public interface IValrWebsocketClient : IDisposable
{
	/// <summary>
	/// Serializes request and sends message via websocket client.
	/// </summary>
	/// <param name="request">Request/message to be sent.</param>
	void Send<T>(T request) where T : Message;
}
