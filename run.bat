@echo off
echo =======================================================
echo          KHOI DONG HE THONG DAI NAM WEB (VERCEL)
echo =======================================================
echo.

echo [1] Dang dong cac tien trinh cu...
taskkill /F /FI "WINDOWTITLE eq DaiNamWeb.Api - Backend*" >nul 2>&1
taskkill /F /IM ngrok.exe >nul 2>&1
timeout /t 2 >nul

echo [2] Dang khoi dong Backend API (DaiNamWeb.Api)
start "DaiNamWeb.Api - Backend" cmd /k "cd DaiNamWeb\DaiNamWeb.Api && dotnet run --launch-profile http"

echo [3] Dang tap duong ham Ngrok Tinh (Public API - Mien Phi)
start "Ngrok Tunnel" cmd /k "cd /d "%~dp0" && ngrok http --domain=unscrimped-nonsubmissible-perla.ngrok-free.dev 5245"

echo.
echo =======================================================
echo HE THONG DA SAN SANG!
echo -------------------------------------------------------
echo 1. Web Vercel cua ban dang chay on dinh.
echo 2. API chay tren ngrok: https://unscrimped-nonsubmissible-perla.ngrok-free.dev
echo =======================================================
pause
