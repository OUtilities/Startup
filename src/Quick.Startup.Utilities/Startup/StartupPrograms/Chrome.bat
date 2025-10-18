@echo off
set CHROME_EXE=%LocalAppData%\Google\Chrome\Application\chrome.exe
if not exist "%CHROME_EXE%" set CHROME_EXE=%ProgramFiles%\Google\Chrome\Application\chrome.exe
if not exist "%CHROME_EXE%" set CHROME_EXE=%ProgramFiles(x86)%\Google\Chrome\Application\chrome.exe

start "" /max "%CHROME_EXE%"