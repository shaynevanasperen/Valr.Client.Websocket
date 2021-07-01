using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Valr.Client.Websocket.Json
{
	/// <summary>
	/// Converts JSON tokens to and from <see cref="double"/>.
	/// </summary>
	public sealed class DoubleConverter : JsonConverter<double>
	{
		/// <inheritdoc cref="JsonConverter{T}.Read"/>
		public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			try
			{
				return reader.TokenType == JsonTokenType.String
					? double.Parse(reader.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture)
					: reader.GetDouble();
			}
			catch (Exception exception)
			{
				throw new Exception($"Invalid value at index {reader.TokenStartIndex}.", exception);
			}
		}

		/// <inheritdoc cref="JsonConverter{T}.Write"/>
		public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options) =>
			writer?.WriteStringValue(value.ToString(CultureInfo.InvariantCulture));
	}
}
