namespace Occhitta.Libraries.Record;

/// <summary>
/// <see cref="DataHelper" />検証クラスです。
/// </summary>
[TestFixture]
public class DataHelperTest {
	#region 内部メソッド定義(CreateToString)
	/// <summary>
	/// 検証情報を生成します。
	/// </summary>
	/// <returns>検証情報</returns>
	private static IEnumerable<TestCaseData> CreateToString() {
		yield return new TestCaseData(null                                   ) { ExpectedResult = "Null"                        };
		yield return new TestCaseData(DBNull.Value                           ) { ExpectedResult = "Null"                        };
		yield return new TestCaseData("ABCDEF"                               ) { ExpectedResult = "\"ABCDEF\""                  };
		yield return new TestCaseData('A'                                    ) { ExpectedResult = "'A'"                         };
		yield return new TestCaseData(true                                   ) { ExpectedResult = "True"                        };
		yield return new TestCaseData((sbyte)1                               ) { ExpectedResult = "1"                           };
		yield return new TestCaseData((byte)2                                ) { ExpectedResult = "2"                           };
		yield return new TestCaseData((short)3                               ) { ExpectedResult = "3"                           };
		yield return new TestCaseData((ushort)4                              ) { ExpectedResult = "4"                           };
		yield return new TestCaseData(5                                      ) { ExpectedResult = "5"                           };
		yield return new TestCaseData((uint)6                                ) { ExpectedResult = "6"                           };
		yield return new TestCaseData((long)7                                ) { ExpectedResult = "7"                           };
		yield return new TestCaseData((ulong)8                               ) { ExpectedResult = "8"                           };
		yield return new TestCaseData((decimal)10                            ) { ExpectedResult = "10"                          };
		yield return new TestCaseData((float)11                              ) { ExpectedResult = "11"                          };
		yield return new TestCaseData((double)12                             ) { ExpectedResult = "12"                          };
		yield return new TestCaseData(new DateOnly(2000, 1, 2)               ) { ExpectedResult = "2000-01-02"                  };
		yield return new TestCaseData(new TimeOnly(1, 2, 3, 4, 5)            ) { ExpectedResult = "01:02:03.004005"             };
		yield return new TestCaseData(new DateTime(2000, 1, 2, 3, 4, 5, 6, 7)) { ExpectedResult = "2000-01-02 03:04:05.006007"  };
		yield return new TestCaseData(new TimeSpan(1, 2, 3, 4, 5, 6)         ) { ExpectedResult = "01.02:03:04.005006"          };
		yield return new TestCaseData((int?)21                               ) { ExpectedResult = "21"                          };
		yield return new TestCaseData(new object()                           ) { ExpectedResult = "System.Object:System.Object" };
	}
	#endregion 内部メソッド定義(CreateToString)

	#region 検証メソッド定義(AssertToString)
	/// <summary>
	/// <see cref="DataHelper.ToString(object?)" />を検証します。
	/// </summary>
	/// <param name="source">検証情報</param>
	[TestCaseSource(nameof(CreateToString))]
	public string AssertToString(object? source) =>
		DataHelper.ToString(source);
	#endregion 検証メソッド定義(AssertToString)
}
