using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Occhitta.Libraries.Record;

/// <summary>
/// 既定読込処理クラスです。
/// </summary>
public sealed class DefaultReader : DataReader {
	#region メンバー変数定義
	/// <summary>
	/// 要素集合
	/// </summary>
	private Queue<DefaultRecord>? source;
	/// <summary>
	/// 要素情報
	/// </summary>
	private DefaultRecord? record;
	#endregion メンバー変数定義

	#region プロパティー定義
	/// <summary>
	/// 要素集合を取得します。
	/// </summary>
	/// <returns>要素集合</returns>
	/// <exception cref="ObjectDisposedException">当該情報が既に破棄されている場合</exception>
	private Queue<DefaultRecord> Source => this.source ?? throw new ObjectDisposedException(GetType().FullName);
	/// <summary>
	/// 要素情報を取得します。
	/// </summary>
	/// <returns>要素情報</returns>
	/// <exception cref="ObjectDisposedException">当該情報が既に破棄されている場合</exception>
	private DefaultRecord Record => this.record ?? throw new ObjectDisposedException(GetType().FullName);
	/// <summary>
	/// 要素個数を取得します。
	/// </summary>
	/// <exception cref="ObjectDisposedException">当該情報が既に破棄されている場合</exception>
	public int Count => Record.Count;
	/// <summary>
	/// 要素情報を取得します。
	/// </summary>
	/// <param name="name">要素名称</param>
	/// <returns>要素情報</returns>
	/// <exception cref="ObjectDisposedException">当該情報が既に破棄されている場合</exception>
	public object? this[string name] => Record[name];
	#endregion プロパティー定義

	#region 生成メソッド定義
	/// <summary>
	/// 既定読込処理を生成します。
	/// </summary>
	/// <param name="source">要素集合</param>
	public DefaultReader(IEnumerable<DefaultRecord> source) {
		this.source = new Queue<DefaultRecord>(source);
		this.record = null;
	}
	/// <summary>
	/// 既定読込処理を生成します。
	/// </summary>
	/// <param name="source">要素集合</param>
	public DefaultReader(params DefaultRecord[] source) {
		this.source = new Queue<DefaultRecord>(source);
		this.record = null;
	}
	/// <summary>
	/// 既定読込処理を生成します。
	/// </summary>
	/// <param name="source">要素集合</param>
	public DefaultReader(IEnumerable<DataRecord> source) {
		this.source = new Queue<DefaultRecord>(ToList(source));
		this.record = null;
	}
	#endregion 生成メソッド定義

	#region 破棄メソッド定義
	/// <summary>
	/// 保持情報を破棄します。
	/// </summary>
	void IDisposable.Dispose() {
		this.source?.Clear();
		this.source = null;
		this.record = null;
	}
	#endregion 破棄メソッド定義

	#region 内部メソッド定義
	/// <summary>
	/// 要素集合へ変換します。
	/// </summary>
	/// <param name="source">要素集合</param>
	/// <returns>要素集合</returns>
	private static IEnumerable<DefaultRecord> ToList(IEnumerable<DataRecord> source) {
		foreach (var choose in source) {
			yield return new DefaultRecord(choose);
		}
	}
	#endregion 内部メソッド定義

	#region 実装メソッド定義(DataReader)
	/// <summary>
	/// 後続情報を読込みます。
	/// </summary>
	/// <param name="cancelHook">取消処理</param>
	/// <returns>後続情報が存在する場合、<c>True</c>を返却</returns>
	/// <exception cref="ObjectDisposedException">当該情報が既に破棄されている場合</exception>
	public async Task<bool> Next(CancellationToken cancelHook = default) =>
		await Task.Run(() => Source.TryDequeue(out this.record));
	#endregion 実装メソッド定義(DataReader)

	#region 実装メソッド定義(DataRecord)
	/// <summary>
	/// 反復処理を取得します。
	/// </summary>
	/// <returns>反復処理</returns>
	IEnumerator<DataPacket> IEnumerable<DataPacket>.GetEnumerator() {
		foreach (var choose in Record) {
			yield return choose;
		}
	}
	#endregion 実装メソッド定義(DataRecord)
}
