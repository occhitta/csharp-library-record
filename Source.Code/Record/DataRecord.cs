using System.Collections.Generic;

namespace Occhitta.Libraries.Record;

/// <summary>
/// 要素読込情報インターフェースです。
/// </summary>
#pragma warning disable IDE1006
public interface DataRecord : IReadOnlyCollection<DataPacket> {
#pragma warning restore IDE1006
	/// <summary>
	/// 要素情報を取得します。
	/// </summary>
	/// <param name="name">要素名称</param>
	/// <returns>要素情報</returns>
	object? this[string name] {
		get;
	}

	/// <summary>
	/// 反復処理を取得します。
	/// </summary>
	/// <returns>反復処理</returns>
	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
		foreach (var choose in this) {
			yield return choose;
		}
	}
}
