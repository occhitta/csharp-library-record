namespace Occhitta.Libraries.Record;

/// <summary>
/// 要素関連情報クラスです。
/// </summary>
/// <param name="Name">要素名称</param>
/// <param name="Data">要素内容</param>
public record struct DataPacket(string Name, object? Data) {
	/// <summary>
	/// 当該情報を表現文字列へ変換します。
	/// </summary>
	/// <returns>表現文字列</returns>
	public override readonly string ToString() =>
		$"{Name}={DataHelper.ToString(Data)}";
}
