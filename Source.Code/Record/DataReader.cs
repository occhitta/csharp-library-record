using System.Threading;
using System.Threading.Tasks;

namespace Occhitta.Libraries.Record;

/// <summary>
/// 要素読込処理インターフェースです。
/// </summary>
#pragma warning disable IDE1006
public interface DataReader : DataRecord, System.IDisposable {
#pragma warning restore IDE1006
	/// <summary>
	/// 後続情報を読込みます。
	/// </summary>
	/// <param name="cancelHook">取消処理</param>
	/// <returns>後続情報が存在する場合、<c>True</c>を返却</returns>
	/// <exception cref="System.ObjectDisposedException">当該情報が既に破棄されている場合</exception>
	Task<bool> Next(CancellationToken cancelHook = default);
}
