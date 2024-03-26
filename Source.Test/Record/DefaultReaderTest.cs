namespace Occhitta.Libraries.Record;

/// <summary>
/// <see cref="DefaultReader" />検証クラスです。
/// </summary>
public class DefaultReaderTest : DataReaderTest<DefaultReader> {
	#region 検証メソッド定義(AssertDataReader)
	/// <summary>
	/// 引数情報を検証します。
	/// </summary>
	/// <param name="source">要素一覧</param>
	/// <param name="action">検証処理</param>
	protected override async Task AssertDataReader(IReadOnlyList<DataRecord> source, Action<DefaultReader> action) =>
		await Task.Run(() => {
			using var result = new DefaultReader(source);
			action(result);
		});
	#endregion 検証メソッド定義(AssertDataReader)

	#region 検証メソッド定義(CreateDataRecord/CreateConstructor/AssertConstructor)
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
	private static IEnumerable<TestCaseData> CreateConstructor() {
		yield return new TestCaseData((object)new DefaultRecord[] {CreateDataRecord(1, "AA", null), CreateDataRecord(2, "BB", 1001)});
	}
	/// <summary>
	/// <see cref="DefaultReader(DefaultRecord[])" />を検証します。
	/// </summary>
	/// <param name="source">要素情報</param>
	[TestCaseSource(nameof(CreateConstructor))]
	public async Task AssertConstructor(DefaultRecord[] source) {
		using var reader = new DefaultReader(source);
		await AssertDataReader(reader, source);
	}
	/// <summary>
	/// <see cref="DefaultReader(IEnumerable{DefaultRecord})" />を検証します。
	/// </summary>
	/// <param name="source">要素情報</param>
	[TestCaseSource(nameof(CreateConstructor))]
	public async Task AssertConstructor(IEnumerable<DefaultRecord> source) {
		using var reader = new DefaultReader(source);
		await AssertDataReader(reader, source.ToArray());
	}
	#endregion 検証メソッド定義(CreateDataRecord/CreateConstructor/AssertConstructor)
}
