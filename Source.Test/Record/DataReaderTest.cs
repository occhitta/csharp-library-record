namespace Occhitta.Libraries.Record;

/// <summary>
/// <see cref="DataReader" />検証クラスです。
/// </summary>
/// <typeparam name="TReader">要素種別</typeparam>
public abstract class DataReaderTest<TReader> : DataRecordTest where TReader : DataReader {
	#region 内部メソッド定義(AssertDataReader)
	/// <summary>
	/// 引数情報を検証します。
	/// </summary>
	/// <param name="actual">読込処理</param>
	/// <param name="expect">想定情報</param>
	/// <param name="except">例外番号</param>
	private static async Task AssertDataReader(DataReader actual, IReadOnlyList<DataRecord> expect, int except) {
		if (except == 0) actual.Dispose();
		var offset = 0;
		while (await actual.Next()) {
			AssertDataRecord(actual, expect[offset]);
			offset ++;
		}
		Assert.That(offset, Is.EqualTo(expect.Count));
	}
	/// <summary>
	/// 引数情報を検証します。
	/// </summary>
	/// <param name="actual">読込処理</param>
	/// <param name="expect">想定情報</param>
	protected static async Task AssertDataReader(DataReader actual, IReadOnlyList<DataRecord> expect) =>
		await AssertDataReader(actual, expect, -1);
	#endregion 内部メソッド定義(AssertDataReader)

	#region 検証メソッド定義(AssertDataReader)
	/// <summary>
	/// 引数情報を検証します。
	/// </summary>
	/// <param name="source">要素一覧</param>
	/// <param name="action">検証処理</param>
	protected abstract Task AssertDataReader(IReadOnlyList<DataRecord> source, Action<TReader> action);
	#endregion 検証メソッド定義(AssertDataReader)

	#region 実装メソッド定義(AssertDataRecord)
	/// <summary>
	/// 要素情報を生成します。
	/// </summary>
	/// <param name="source">要素一覧</param>
	/// <param name="action">検証処理</param>
	protected override sealed async Task AssertDataRecord(IReadOnlyList<DataPacket> source, Action<DataRecord> action) =>
		await AssertDataReader([new DefaultRecord(source)], async actual => {
			var amount = 0;
			while (await actual.Next()) {
				action(actual);
				amount ++;
			}
			Assert.That(amount, Is.EqualTo(1));
		});
	#endregion 実装メソッド定義(AssertDataRecord)

	#region 検証メソッド定義(CreateDataRecord/CreateDataReader/AssertDataReader)
	/// <summary>
	/// 検証情報を生成します。
	/// </summary>
	/// <param name="code">識別番号</param>
	/// <param name="name">要素名称</param>
	/// <param name="data">要素内容</param>
	/// <returns>検証情報</returns>
	private static DefaultRecord CreateDataRecord(int code, string name, object? data) =>
		new((nameof(code), code), (nameof(name), name), (nameof(data), data));
	/// <summary>
	/// 検証情報を生成します。
	/// </summary>
	/// <returns>検証情報</returns>
	private static IEnumerable<TestCaseData> CreateDataReader() {
		yield return new TestCaseData((object)new DataRecord[] {CreateDataRecord(1, "AA", "aa"), CreateDataRecord(2, "BB", null)});
	}
	/// <summary>
	/// <see cref="DataReader" />を検証します。
	/// </summary>
	/// <param name="source">要素情報</param>
	[TestCaseSource(nameof(CreateDataReader))]
	public void AssertDataReader(IReadOnlyList<DataRecord> source) {
		// 正常処理
		AssertDataReader(source, result => Assert.Multiple(async () => await AssertDataReader(result, source, -1)));
		// 異常処理
		AssertDataReader(source, result => {
			// Nextメソッドにて発生する想定
			var phase1 = Assert.CatchAsync<ObjectDisposedException>(async () => await AssertDataReader(result, source, 0));
			Assert.That(phase1.Message, Is.EqualTo($"Cannot access a disposed object.\r\nObject name: '{typeof(TReader).FullName}'."));
		});
	}
	#endregion 検証メソッド定義(CreateDataRecord/CreateDataReader/AssertDataReader)
}
