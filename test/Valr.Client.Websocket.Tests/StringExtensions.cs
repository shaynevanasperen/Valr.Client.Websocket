using System;
using System.Security.Cryptography;
using System.Text;

namespace Valr.Client.Websocket.Tests;

static class StringExtensions
{
	public static string SignPayloadHmacSha512(this string key, string payload, Encoding encoding)
	{
		var bytes = encoding.GetBytes(payload);

		using var hashAlgorithm = new HMACSHA512(encoding.GetBytes(key));
		var hash = hashAlgorithm.ComputeHash(bytes);

		return Convert.ToHexStringLower(hash).Replace("-", string.Empty, StringComparison.InvariantCulture).ToLowerInvariant();
	}
}
