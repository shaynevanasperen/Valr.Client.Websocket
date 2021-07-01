using System.Text.Json;

namespace Valr.Client.Websocket.Json
{
	static class ValrJsonOptions
	{
		public static JsonSerializerOptions Default = new(JsonSerializerDefaults.Web);
	}
}
