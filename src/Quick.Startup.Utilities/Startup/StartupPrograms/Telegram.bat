@echo off
set TELEGRAM_PATH=%USERPROFILE%\AppData\Roaming\Telegram Desktop\Telegram.exe
if exist "%TELEGRAM_PATH%" (
    start "" "%TELEGRAM_PATH%"
) else (
    echo Telegram is not installed in the default location: %TELEGRAM_PATH%
    pause
)