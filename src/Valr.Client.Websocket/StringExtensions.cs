using System;
using System.Security.Cryptography;
using System.Text;

namespace Valr.Client.Websocket
{
	static class StringExtensions
	{
		public static string SignPayloadHmacSha512(this string key, string payload, Encoding encoding) => key.SignPayload(payload, encoding, x => new HMACSHA512(x));

		public static string SignPayloadHmacSha256(this string key, string payload, Encoding encoding) => key.SignPayload(payload, encoding, x => new HMACSHA256(x));

		static string SignPayload(this string key, string payload, Encoding encoding, Func<byte[], HashAlgorithm> createHashAlgorithm)
		{
			var bytes = encoding.GetBytes(payload);

			using var hashAlgorithm = createHashAlgorithm(encoding.GetBytes(key));
			var hash = hashAlgorithm.ComputeHash(bytes);

			return BitConverter.ToString(hash).Replace("-", string.Empty, StringComparison.InvariantCulture).ToLowerInvariant();
		}

		public static string AsValue(this string? value) => value ?? Nothing;

		public const string Nothing = "---";
	}
}
