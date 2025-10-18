@echo off
set POWERSHELL_PATH=%SystemRoot%\System32\WindowsPowerShell\v1.0\powershell.exe
if exist "%POWERSHELL_PATH%" (
    start "" /max "%POWERSHELL_PATH%"
) else (
    echo PowerShell is not installed in the default location: %POWERSHELL_PATH%
    pause
)