using System;
using System.Diagnostics.CodeAnalysis;

namespace Occhitta.Libraries.Record;

/// <summary>
/// 要素共通関数クラスです。
/// </summary>
public static class DataHelper {
	/// <summary>
	/// <paramref name="source" />が基本情報か判定します。
	/// </summary>
	/// <param name="source">判定情報</param>
	/// <returns>基本情報である場合、<c>True</c>を返却</returns>
	private static bool IsAtomic(object source) => source is bool
		|| source is sbyte
		|| source is byte
		|| source is short
		|| source is ushort
		|| source is int
		|| source is uint
		|| source is long
		|| source is ulong
		|| source is decimal
		|| source is float
		|| source is double;
	/// <summary>
	/// <paramref name="source" />を表現文字列へ変換します。
	/// </summary>
	/// <param name="source">判定情報</param>
	/// <param name="result">変換情報</param>
	/// <returns>変換した場合、<c>True</c>を返却</returns>
	private static bool ToString(object source, [MaybeNullWhen(false)]out string result) {
		if (source is string value1) {
			result = $"\"{value1}\"";
			return true;
		} else if (source is char value2) {
			result = $"'{value2}'";
			return true;
		} else if (source is DateTime value3) {
			result = value3.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
			return true;
		} else if (source is DateOnly value4) {
			result = value4.ToString("yyyy-MM-dd");
			return true;
		} else if (source is TimeOnly value5) {
			result = value5.ToString("HH:mm:ss.ffffff");
			return true;
		} else if (source is TimeSpan value6) {
			result = value6.ToString(@"dd\.hh\:mm\:ss\.ffffff");
			return true;
		} else if (IsAtomic(source)) {
			result = $"{source}";
			return true;
		} else {
			result = default;
			return false;
		}
	}
	/// <summary>
	/// <paramref name="source" />を表現文字列へ変換します。
	/// </summary>
	/// <param name="source">判定情報</param>
	/// <returns>表現文字列</returns>
	public static string ToString(object? source) {
		if (source == null || source == DBNull.Value) {
			return "Null";
		} else if (ToString(source, out var result)) {
			return result;
		} else {
			return $"{source.GetType().FullName}:{source}";
		}
	}
}
