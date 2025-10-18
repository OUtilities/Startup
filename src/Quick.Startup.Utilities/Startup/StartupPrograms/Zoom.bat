@echo off
set ZOOM_EXE=%AppData%\Zoom\bin\Zoom.exe
if not exist "%ZOOM_EXE%" set ZOOM_EXE=%ProgramFiles%\Zoom\bin\Zoom.exe
if not exist "%ZOOM_EXE%" set ZOOM_EXE=%ProgramFiles(x86)%\Zoom\bin\Zoom.exe

if exist "%ZOOM_EXE%" (
    start "" /max "%ZOOM_EXE%"
) else (
    echo Zoom is not installed in the default location: %ZOOM_EXE%
    pause
)