@echo off
echo Dang dung cac server cu...
taskkill /F /FI "WINDOWTITLE eq DaiNam-API*" >nul 2>&1
taskkill /F /FI "WINDOWTITLE eq DaiNam-Client*" >nul 2>&1
taskkill /F /FI "WINDOWTITLE eq DaiNam-Staff*" >nul 2>&1
timeout /t 2 >nul

echo [1] Khoi dong API (port 5245)...
start "DaiNam-API" cmd /k "cd /d "%~dp0DaiNamWeb.Api" && dotnet run --urls http://localhost:5245"

echo Cho API khoi dong...
timeout /t 5 >nul

echo [2] Khoi dong Web Khach Hang (port 5246)...
start "DaiNam-Client" cmd /k "cd /d "%~dp0DaiNamWeb.Client" && dotnet run --urls http://localhost:5246"

echo [3] Khoi dong Staff App (port 5247)...
start "DaiNam-Staff" cmd /k "cd /d "%~dp0DaiNamStaff" && dotnet run --urls http://localhost:5247"

timeout /t 6 >nul

echo.
echo Tat ca da san sang!
echo   API    : http://localhost:5245/api/health
echo   Khach  : http://localhost:5246
echo   Staff  : http://localhost:5247
echo.
echo Mo trinh duyet...
start "" "http://localhost:5246"
start "" "http://localhost:5247"
