using System;
using System.Collections.Generic;
using Valr.Client.Websocket.Models;

namespace Valr.Client.Websocket.Responses;

/// <summary>
/// Allowed order types updated message.
/// </summary>
public record AllowedOrderTypesUpdatedResponse : PairMessage<IReadOnlyCollection<string>>;
