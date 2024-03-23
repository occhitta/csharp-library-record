using System;
using System.Collections.Generic;

namespace Occhitta.Libraries.Record;

/// <summary>
/// 既定読込情報クラスです。
/// </summary>
public sealed class DefaultRecord : DataRecord {
	#region メンバー変数定義
	/// <summary>
	/// 要素辞書
	/// </summary>
	private readonly Dictionary<string, object?> source;
	#endregion メンバー変数定義

	#region プロパティー定義
	/// <summary>
	/// 要素個数を取得します。
	/// </summary>
	/// <returns>要素個数</returns>
	public int Count => this.source.Count;
	/// <summary>
	/// 要素情報を取得または設定します。
	/// </summary>
	/// <param name="name">要素名称</param>
	/// <returns>要素情報</returns>
	public object? this[string name] {
		get => GetData(name);
		set => SetData(name, value);
	}
	#endregion プロパティー定義

	#region 生成メソッド定義
	/// <summary>
	/// 既定読込情報を生成します。
	/// </summary>
	public DefaultRecord() {
		this.source = [];
	}
	/// <summary>
	/// 既定読込情報を生成します。
	/// </summary>
	/// <param name="values">要素集合</param>
	public DefaultRecord(IEnumerable<DataPacket> values) : this() {
		foreach (var (name, data) in values) {
			this.source[name] = data;
		}
	}
	/// <summary>
	/// 既定読込情報を生成します。
	/// </summary>
	/// <param name="values">要素配列</param>
	public DefaultRecord(params DataPacket[] values) : this((IEnumerable<DataPacket>)values) {
		// 処理なし
	}
	/// <summary>
	/// 既定読込情報を生成します。
	/// </summary>
	/// <param name="values">要素配列</param>
	public DefaultRecord(params (string, object?)[] values) : this() {
		foreach (var (name, data) in values) {
			this.source[name] = data;
		}
	}
	/// <summary>
	/// 既定読込情報を生成します。
	/// </summary>
	/// <param name="values">要素集合</param>
	public DefaultRecord(IEnumerable<KeyValuePair<string, object?>> values) {
		this.source = new Dictionary<string, object?>(values);
	}
	#endregion 生成メソッド定義

	#region 内部メソッド定義
	/// <summary>
	/// 要素情報を取得します。
	/// </summary>
	/// <param name="name">要素名称</param>
	/// <returns>要素情報</returns>
	private object? GetData(string name) =>
		this.source.TryGetValue(name, out var data)? data: null;
	/// <summary>
	/// 要素情報を設定します。
	/// </summary>
	/// <param name="name">要素名称</param>
	/// <param name="data">要素情報</param>
	private void SetData(string name, object? data) =>
		this.source[name] = data == DBNull.Value? null: data;
	#endregion 内部メソッド定義

	#region 実装メソッド定義
	/// <summary>
	/// 反復処理を取得します。
	/// </summary>
	/// <returns>反復処理</returns>
	IEnumerator<DataPacket> IEnumerable<DataPacket>.GetEnumerator() {
		foreach (var (name, data) in this.source) {
			yield return new(name, data);
		}
	}
	#endregion 実装メソッド定義

	#region 検証メソッド定義
	/// <summary>
	/// 当該文字列を表現文字列へ変換します。
	/// </summary>
	/// <returns>表現文字列</returns>
	public override string ToString() {
		var result = new System.Text.StringBuilder();
		var prefix = "";
		result.Append('{');
		foreach (var choose in this.source) {
			result.Append(prefix);
			result.Append(choose.Key);
			result.Append('=');
			result.Append(DataHelper.ToString(choose.Value));
			prefix = ", ";
		}
		result.Append('}');
		return result.ToString();
	}
	#endregion 検証メソッド定義
}
