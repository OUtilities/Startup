@echo off
set NPP_SHORTCUT="C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Notepad++.lnk"
if exist %NPP_SHORTCUT% (
    start "" %NPP_SHORTCUT%
) else (
    echo Notepad++ shortcut not found: %NPP_SHORTCUT%
    pause
)