namespace Occhitta.Libraries.Record;

/// <summary>
/// <see cref="DataRecord" />検証クラスです。
/// </summary>
public abstract class DataRecordTest : CollectionTest {
	#region 検証メソッド定義(AssertDataRecord)
	/// <summary>
	/// 要素情報を生成します。
	/// </summary>
	/// <param name="source">要素一覧</param>
	/// <param name="action">検証処理</param>
	protected abstract Task AssertDataRecord(IReadOnlyList<DataPacket> source, Action<DataRecord> action);
	#endregion 検証メソッド定義(AssertDataRecord)

	#region 実装メソッド定義(AssertCollection)
	/// <summary>
	/// 要素集合を検証します。
	/// </summary>
	/// <param name="source">要素一覧</param>
	/// <param name="action">検証処理</param>
	protected sealed override async Task AssertCollection(IReadOnlyList<DataPacket> source, Action<IReadOnlyCollection<DataPacket>> action) =>
		await AssertDataRecord(source, actual => action(actual));
	#endregion 実装メソッド定義(AssertCollection)
}
