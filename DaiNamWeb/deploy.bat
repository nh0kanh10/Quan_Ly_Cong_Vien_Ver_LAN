@echo off
echo.
echo  DEPLOY DAI NAM WEB CLIENT LEN VERCEL
echo.

echo [1] Kiem tra Vercel CLI...
where vercel >nul 2>&1
if %errorlevel% neq 0 (
    echo  Chua co Vercel CLI. Dang cai...
    npm install -g vercel
)

echo  API Backend: https://unscrimped-nonsubmissible-perla.ngrok-free.dev
echo  (Dam bao run.bat dang chay truoc khi test)
echo.

echo [2] Dang build Client (Release)...
cd /d "%~dp0DaiNamWeb.Client"
dotnet publish -c Release -o bin\vercel-publish
if %errorlevel% neq 0 (
    echo  Build that bai! Kiem tra loi o tren.
    pause
    exit /b
)

echo [3] Dang deploy len Vercel...
cd bin\vercel-publish\wwwroot
vercel --prod

echo.
echo  Deploy hoan tat!
echo  Kiem tra website tren dashboard Vercel cua ban.
echo.
pause
