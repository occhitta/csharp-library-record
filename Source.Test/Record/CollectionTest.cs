namespace Occhitta.Libraries.Record;

/// <summary>
/// <see cref="IReadOnlyCollection{DataPacket}" />検証クラスです。
/// </summary>
public abstract class CollectionTest {
	#region 内部メソッド定義(AssertEquals)
	/// <summary>
	/// 引数が等価であるか判定します。
	/// </summary>
	/// <param name="actual">実質情報</param>
	/// <param name="expect">想定情報</param>
	private static void AssertEquals(DataPacket actual, DataPacket expect) {
		Assert.Multiple(() => {
			Assert.That(actual.Name, Is.EqualTo(expect.Name));
			Assert.That(actual.Data, Is.EqualTo(expect.Data));
		});
	}
	#endregion 内部メソッド定義(AssertEquals)

	#region 内部メソッド定義(AssertEnumerable)
	/// <summary>
	/// <see cref="System.Collections.IEnumerator" />を検証します。
	/// </summary>
	/// <param name="actual">実質情報</param>
	/// <param name="expect">想定情報</param>
	private static void AssertEnumerable(System.Collections.IEnumerator actual, IReadOnlyList<DataPacket> expect) {
		var index = 0;
		while (actual.MoveNext()) {
			if (actual.Current is DataPacket choose) {
				AssertEquals(choose, expect[index]);
			} else {
				Assert.Fail();
			}
			index ++;
		}
		Assert.That(index, Is.EqualTo(expect.Count));
	}
	/// <summary>
	/// <see cref="System.Collections.IEnumerable" />を検証します。
	/// </summary>
	/// <param name="actual">実質情報</param>
	/// <param name="expect">想定情報</param>
	private static void AssertEnumerable(System.Collections.IEnumerable actual, IReadOnlyList<DataPacket> expect) =>
		AssertEnumerable(actual.GetEnumerator(), expect);
	#endregion 内部メソッド定義(AssertEnumerable)

	#region 内部メソッド定義(AssertEnumerable)
	/// <summary>
	/// <see cref="IEnumerator{DataPacket}" />を検証します。
	/// </summary>
	/// <param name="actual">実質情報</param>
	/// <param name="expect">想定情報</param>
	private static void AssertEnumerable(IEnumerator<DataPacket> actual, IReadOnlyList<DataPacket> expect) {
		var index = 0;
		while (actual.MoveNext()) {
			AssertEquals(actual.Current, expect[index]);
			index ++;
		}
		Assert.That(index, Is.EqualTo(expect.Count));
	}
	/// <summary>
	/// <see cref="IEnumerable{DataPacket}" />を検証します。
	/// </summary>
	/// <param name="actual">実質情報</param>
	/// <param name="expect">想定情報</param>
	private static void AssertEnumerable(IEnumerable<DataPacket> actual, IReadOnlyList<DataPacket> expect) =>
		AssertEnumerable(actual.GetEnumerator(), expect);
	#endregion 内部メソッド定義(AssertEnumerable)

	#region 検証メソッド定義(CreateCollection/AssertCollection)
	/// <summary>
	/// 検証情報を生成します。
	/// </summary>
	/// <returns>検証一覧</returns>
	private static IEnumerable<TestCaseData> CreateCollection() {
		yield return new TestCaseData(new DataPacket[] {new("code", 1), new("name", "AA"), new("memo", "aa")                }, new DataPacket[] {new("code", 1), new("name", "AA"), new("memo", "aa")});
		yield return new TestCaseData(new DataPacket[] {new("code", 2), new("name", "BB"), new("memo", null)                }, new DataPacket[] {new("code", 2), new("name", "BB"), new("memo", null)});
		yield return new TestCaseData(new DataPacket[] {new("code", 3), new("name", "CC"), new("memo", 1234), new("code", 4)}, new DataPacket[] {new("code", 4), new("name", "CC"), new("memo", 1234)});
	}
	/// <summary>
	/// 要素集合を検証します。
	/// </summary>
	/// <param name="source">要素一覧</param>
	/// <param name="action">検証処理</param>
	protected abstract Task AssertCollection(IReadOnlyList<DataPacket> source, Action<IReadOnlyCollection<DataPacket>> action);
	/// <summary>
	/// <see cref="IReadOnlyCollection{DataPacket}" />を検証します。
	/// </summary>
	/// <param name="source">要素一覧</param>
	/// <param name="expect">想定情報</param>
	[TestCaseSource(nameof(CreateCollection))]
	public async Task AssertCollection(IReadOnlyList<DataPacket> source, IReadOnlyList<DataPacket> expect) {
		await AssertCollection(source, actual => {
			Assert.That(actual, Has.Count.EqualTo(expect.Count));
			AssertEnumerable((System.Collections.IEnumerable)actual, expect);
			AssertEnumerable(actual, expect);
		});
	}
	#endregion 検証メソッド定義(CreateCollection/AssertCollection)
}
