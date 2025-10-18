@echo off
set POSTMAN_EXE=%LocalAppData%\Postman\Postman.exe
if exist "%POSTMAN_EXE%" (
    start "" /max "%POSTMAN_EXE%"
) else (
    echo Postman is not installed in the default location: %POSTMAN_EXE%
    pause
)