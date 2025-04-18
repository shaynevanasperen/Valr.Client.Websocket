namespace Valr.Client.Websocket.Models;

/// <summary>
/// A list of all known Valr message types.
/// </summary>
public static class MessageType
{
	// ReSharper disable InconsistentNaming
#pragma warning disable 1591
	public const string AUTHENTICATED = nameof(AUTHENTICATED);
	public const string SUBSCRIBE = nameof(SUBSCRIBE);
	public const string SUBSCRIBED = nameof(SUBSCRIBED);
	public const string UNSUBSCRIBED = nameof(UNSUBSCRIBED);
	public const string PING = nameof(PING);
	public const string PONG = nameof(PONG);
	public const string AGGREGATED_ORDERBOOK_UPDATE = nameof(AGGREGATED_ORDERBOOK_UPDATE);
	public const string FULL_ORDERBOOK_SNAPSHOT = nameof(FULL_ORDERBOOK_SNAPSHOT);
	public const string FULL_ORDERBOOK_UPDATE = nameof(FULL_ORDERBOOK_UPDATE);
	public const string OB_L1_SNAPSHOT = nameof(OB_L1_SNAPSHOT);
	public const string OB_L1_DIFF = nameof(OB_L1_DIFF);
	public const string OB_L1_D1_SNAPSHOT = nameof(OB_L1_D1_SNAPSHOT);
	public const string OB_L1_D5_SNAPSHOT = nameof(OB_L1_D5_SNAPSHOT);
	public const string OB_L1_D10_SNAPSHOT = nameof(OB_L1_D10_SNAPSHOT);
	public const string OB_L1_D20_SNAPSHOT = nameof(OB_L1_D20_SNAPSHOT);
	public const string OB_L1_D40_SNAPSHOT = nameof(OB_L1_D40_SNAPSHOT);
	public const string OB_L1_D80_SNAPSHOT = nameof(OB_L1_D80_SNAPSHOT);
	public const string ALLOWED_ORDER_TYPES_UPDATED = nameof(ALLOWED_ORDER_TYPES_UPDATED);
	public const string MARKET_SUMMARY_UPDATE = nameof(MARKET_SUMMARY_UPDATE);
	public const string NEW_TRADE_BUCKET = nameof(NEW_TRADE_BUCKET);
	public const string NEW_TRADE = nameof(NEW_TRADE);
	public const string NEW_ACCOUNT_HISTORY_RECORD = nameof(NEW_ACCOUNT_HISTORY_RECORD);
	public const string BALANCE_UPDATE = nameof(BALANCE_UPDATE);
	public const string NEW_ACCOUNT_TRADE = nameof(NEW_ACCOUNT_TRADE);
	public const string INSTANT_ORDER_COMPLETED = nameof(INSTANT_ORDER_COMPLETED);
	public const string OPEN_ORDERS_UPDATE = nameof(OPEN_ORDERS_UPDATE);
	public const string ORDER_PROCESSED = nameof(ORDER_PROCESSED);
	public const string ORDER_STATUS_UPDATE = nameof(ORDER_STATUS_UPDATE);
	public const string FAILED_CANCEL_ORDER = nameof(FAILED_CANCEL_ORDER);
	public const string NEW_PENDING_RECEIVE = nameof(NEW_PENDING_RECEIVE);
	public const string SEND_STATUS_UPDATE = nameof(SEND_STATUS_UPDATE);
#pragma warning restore 1591
}
