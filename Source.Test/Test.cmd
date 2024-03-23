@ECHO OFF
SETLOCAL
REM 実行環境設定
SET OUTPUT_PATH=..\Result.Data

REM 日時書式設定
SET OUTPUT_DATE=%DATE:~0,4%%DATE:~5,2%%DATE:~8,2%
SET OUTPUT_TIME=%TIME: =0%
SET OUTPUT_TIME=%OUTPUT_TIME:~0,2%%OUTPUT_TIME:~3,2%%OUTPUT_TIME:~6,2%
SET INVOKE_PATH=%OUTPUT_PATH%\%OUTPUT_DATE%_%OUTPUT_TIME%
REM SET INVOKE_PATH=%OUTPUT_PATH%
SET RESULT_FILE=NUnitTestResult.xml

REM コマンド実行
CD %~dp0
REM 出力フォルダ削除
RMDIR /S /Q "%INVOKE_PATH%"
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=%INVOKE_PATH%\%RESULT_FILE%
REM dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=%INVOKE_PATH%\%RESULT_FILE% --filter "FullyQualifiedName~Occhitta.Libraries.Record.Rdb.RdbHelperTest"
IF %ERRORLEVEL% NEQ 0 (
  GOTO Finish
)
reportgenerator -reports:%INVOKE_PATH%\%RESULT_FILE% -targetdir:%INVOKE_PATH% -reporttypes:Html
IF %ERRORLEVEL% NEQ 0 (
  GOTO Finish
)
START %INVOKE_PATH%\index.html
:Finish
ENDLOCAL
EXIT /B
