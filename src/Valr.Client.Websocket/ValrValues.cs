namespace Valr.Client.Websocket
{
	/// <summary>
	/// Valr static urls
	/// </summary>
	public static class ValrValues
	{
		/// <summary>
		/// Main Valr url to websocket API
		/// </summary>
		public const string ApiWebsocketUrl = "wss://api.valr.com";

		/// <summary>
		/// Valr Account websocket path
		/// </summary>
		public const string AccountPath = "/ws/account";

		/// <summary>
		/// Valr Trade websocket path
		/// </summary>
		public const string TradePath = "/ws/trade";
	}
}
