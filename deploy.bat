@echo off
setlocal
echo =======================================================
echo     DEPLOY FRONTEND BLAZOR TO VERCEL (PRO MAX V2)
echo =======================================================
echo.

set "CLIENT_DIR=DaiNamWeb\DaiNamWeb.Client"
set "PUB_DIR=%CLIENT_DIR%\publish"

echo [1] Dang xoa thu muc publish cu...
if exist "%PUB_DIR%" rmdir /s /q "%PUB_DIR%"

echo.
echo [2] Dang bien dich Blazor WASM (Release)...
cd /d "%~dp0"
cd "%CLIENT_DIR%"
dotnet publish -c Release -o publish

echo.
echo [3] Dang fix loi 404 tren Vercel (Doi _framework =^> framework)...
cd publish\wwwroot
powershell -Command "if (Test-Path '_framework') { Rename-Item -Path '_framework' -NewName 'framework' -Force }; Get-ChildItem -Path '.' -Recurse -File -Include *.html,*.js,*.json,*.css,*.map | ForEach-Object { $c = [System.IO.File]::ReadAllText($_.FullName); if ($c.Contains('_framework')) { [System.IO.File]::WriteAllText($_.FullName, $c.Replace('_framework', 'framework')) } }"

echo.
echo [4] Phuc hoi ket noi den dainam-web-portal...
cd /d "%~dp0"
if exist "%CLIENT_DIR%\.vercel" (
    xcopy /Y /I /E "%CLIENT_DIR%\.vercel" "%PUB_DIR%\wwwroot\.vercel\" >nul
)

echo.
echo [5] Dang chuyen phat nhanh len Vercel...
cd "%PUB_DIR%\wwwroot"
npx vercel --prod --yes

echo.
echo =======================================================
echo DEPLOY HOAN TAT! 
echo Quoc te gio da co the chiem nguong code cua bac qua Vercel.
echo =======================================================
pause
