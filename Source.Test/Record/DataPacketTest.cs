namespace Occhitta.Libraries.Record;

/// <summary>
/// <see cref="DataPacket" />検証クラスです。
/// </summary>
[TestFixture]
public class DataPacketTest {
	/// <summary>
	/// <see cref="DataPacket(string, object?)" />を検証します。
	/// <see cref="DataPacket.ToString()" />を検証します。
	/// </summary>
	/// <param name="name">要素名称</param>
	/// <param name="data">要素内容</param>
	/// <returns>表現文字列</returns>
	[TestCase("name", null, ExpectedResult="name=Null"  )]
	[TestCase("name", "11", ExpectedResult="name=\"11\"")]
	public string AssertSuccess(string name, object? data) {
		var result = new DataPacket(name, data);
		return result.ToString();
	}
}
