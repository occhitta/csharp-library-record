namespace Occhitta.Libraries.Record;

/// <summary>
/// <see cref="DefaultRecord" />検証クラスです。
/// </summary>
[TestFixture]
public class DefaultRecordTest : DataRecordTest {
	#region 検証メソッド定義(AssertDataRecord)
	/// <summary>
	/// 要素情報を生成します。
	/// </summary>
	/// <param name="source">要素一覧</param>
	/// <param name="action">検証処理</param>
	protected override async Task AssertDataRecord(IReadOnlyList<DataPacket> source, Action<DataRecord> action) =>
		await Task.Run(() => action(new DefaultRecord(source)));
	#endregion 検証メソッド定義(AssertDataRecord)

	#region 検証メソッド定義(CreateConstructor/AssertConstructor)
	/// <summary>
	/// 検証情報を生成します。
	/// </summary>
	/// <returns>検証情報</returns>
	private static IEnumerable<TestCaseData> CreateConstructor() {
		yield return new TestCaseData(1, "AA");
	}
	/// <summary>
	/// 引数情報を検証します。
	/// </summary>
	/// <param name="actual">実質情報</param>
	/// <param name="value1">想定情報</param>
	/// <param name="value2">想定情報</param>
	private static void AssertConstructor(DataRecord actual, int? value1, string? value2) {
		Assert.Multiple(() => {
			Assert.That(actual["code"], Is.EqualTo(value1));
			Assert.That(actual["name"], Is.EqualTo(value2));
		});
	}
	/// <summary>
	/// 下記処理を検証します。
	/// <para><see cref="DefaultRecord(IEnumerable{DataPacket})"                    /></para>
	/// <para><see cref="DefaultRecord(DataPacket[])"                               /></para>
	/// <para><see cref="DefaultRecord(ValueTuple{string, object?}[])"              /></para>
	/// <para><see cref="DefaultRecord(IEnumerable{KeyValuePair{string, object?}})" /></para>
	/// </summary>
	/// <param name="code">識別番号</param>
	/// <param name="name">要素名称</param>
	[TestCaseSource(nameof(CreateConstructor))]
	public void AssertConstructor(int code, string name) {
		AssertConstructor(new DefaultRecord(),                                                                            null, null);
		AssertConstructor(new DefaultRecord(new List<DataPacket>()              { new("code", code), new("name", name)}), code, name);
		AssertConstructor(new DefaultRecord(new DataPacket[]                    { new("code", code), new("name", name)}), code, name);
		AssertConstructor(new DefaultRecord(new (string, object?)[]             { new("code", code), new("name", name)}), code, name);
		AssertConstructor(new DefaultRecord(new KeyValuePair<string, object?>[] { new("code", code), new("name", name)}), code, name);
	}
	#endregion 検証メソッド定義(CreateConstructor/AssertConstructor)

	#region 検証メソッド定義(CreateProperties/AssertProperties)
	/// <summary>
	/// 検証情報を生成します。
	/// </summary>
	/// <returns>検証集合</returns>
	private static IEnumerable<TestCaseData> CreateProperties() {
		yield return new TestCaseData(null)         { ExpectedResult = null };
		yield return new TestCaseData(DBNull.Value) { ExpectedResult = null };
		yield return new TestCaseData("AA")         { ExpectedResult = "AA" };
		yield return new TestCaseData(1001)         { ExpectedResult = 1001 };
	}
	/// <summary>
	/// <see cref="DefaultRecord[string]" />を検証します。
	/// </summary>
	[TestCaseSource(nameof(CreateProperties))]
	public object? AssertProperties(object? data) {
		var source = new DefaultRecord();
		source["data"] = data;
		return source["data"];
	}
	#endregion 検証メソッド定義(CreateProperties/AssertProperties)

	#region 検証メソッド定義(CreateToString/AssertToString)
	/// <summary>
	/// 検証情報を生成します。
	/// </summary>
	/// <returns>検証一覧</returns>
	private static IEnumerable<TestCaseData> CreateToString() {
		yield return new TestCaseData(1, "aa", null) { ExpectedResult = "{code=1, name=\"aa\", data=Null}"   };
		yield return new TestCaseData(2, "bb", "11") { ExpectedResult = "{code=2, name=\"bb\", data=\"11\"}" };
	}
	/// <summary>
	/// <see cref="DefaultRecord.ToString()" />を検証します。
	/// </summary>
	/// <param name="code">検証情報</param>
	/// <param name="name">検証情報</param>
	/// <param name="data">検証情報</param>
	/// <returns>表現文字列</returns>
	[TestCaseSource(nameof(CreateToString))]
	public string AssertToString(int code, string name, object? data) {
		var result = new DefaultRecord(("code", code), ("name", name), ("data", data));
		return result.ToString();
	}
	#endregion 検証メソッド定義(CreateToString/AssertToString)
}
