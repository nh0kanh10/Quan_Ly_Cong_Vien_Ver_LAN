@echo off
color 0A

echo =========================================================================
echo       HET THONG KIEM THU TU DONG (AUTOMATION TEST) - QUAN LY CONG VIEN   
echo =========================================================================
echo.
echo Dang quet va thuc thi Unit Tests tren nen tang .NET Framework 4.7.2...
echo (Bo qua Giao dien Visual Studio de dam bao hieu suat CPU).
echo.
echo Xin vui long doi trong giay lat...
echo =========================================================================
echo.

dotnet test SD001.Tests\SD001.Tests.csproj -v m

echo.
echo =========================================================================
echo  DA TONG HOP XONG KET QUA KIEM THU!
echo =========================================================================
pause
