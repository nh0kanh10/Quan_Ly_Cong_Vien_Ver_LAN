@echo off
echo.
echo  KHOI DONG DAI NAM WEB - BACKEND API
echo.

echo [1] Dang dong tien trinh cu...
taskkill /F /FI "WINDOWTITLE eq DaiNamWeb.Api*" >nul 2>&1
taskkill /F /IM ngrok.exe >nul 2>&1
timeout /t 2 >nul

echo [2] Dang khoi dong Backend API (port 5245)...
start "DaiNamWeb.Api" cmd /k "cd /d "%~dp0DaiNamWeb.Api" && dotnet run --urls http://localhost:5245"

echo [3] Cho API khoi dong (3 giay)...
timeout /t 3 >nul

echo [4] Dang bat duong ham Ngrok (domain tinh)...
start "Ngrok Tunnel" cmd /k "ngrok http --domain=unscrimped-nonsubmissible-perla.ngrok-free.dev 5245"

echo.
echo  HE THONG DA SAN SANG!
echo  API local : http://localhost:5245
echo  API ngrok : https://unscrimped-nonsubmissible-perla.ngrok-free.dev
echo  Health    : https://unscrimped-nonsubmissible-perla.ngrok-free.dev/api/health
echo.
pause
