namespace Occhitta.Libraries.Record;

/// <summary>
/// <see cref="DataRecord" />検証クラスです。
/// </summary>
public abstract class DataRecordTest : CollectionTest {
	#region 内部メソッド定義(AssertDataRecord)
	/// <summary>
	/// 引数が等価であるか検証します。
	/// </summary>
	/// <param name="actual">実質情報</param>
	/// <param name="expect">想定情報</param>
	protected static void AssertDataRecord(DataRecord actual, DataRecord expect) {
		var length = expect.Count;
		Assert.That(actual, Has.Count.EqualTo(length));
		Assert.Multiple(() => {
			foreach (var choose in actual) {
				AssertDataPacket(choose, new DataPacket(choose.Name, expect[choose.Name]));
			}
		});
		Assert.Multiple(() => {
			foreach (var choose in expect) {
				Assert.That(actual[choose.Name], Is.EqualTo(choose.Data));
			}
		});
	}
	#endregion 内部メソッド定義(AssertDataRecord)

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
	/// 要素情報を検証します。
	/// </summary>
	/// <param name="source">要素一覧</param>
	/// <param name="action">検証処理</param>
	protected sealed override async Task AssertCollection(IReadOnlyList<DataPacket> source, Action<IReadOnlyCollection<DataPacket>> action) =>
		await AssertDataRecord(source, actual => action(actual));
	#endregion 実装メソッド定義(AssertCollection)
}
