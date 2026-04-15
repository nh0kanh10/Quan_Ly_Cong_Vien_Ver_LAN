$path = "c:\Users\ADMIN\Desktop\Quan_Ly_Cong_Vien_Ver_1.2\Docs\04_SPRINT_03_Winform_CorePOS\POS_Integration_TestCases.csv"
$bytes = [System.IO.File]::ReadAllBytes($path)
$bom = [byte[]]@(0xEF,0xBB,0xBF)
if($bytes[0] -ne 0xEF -or $bytes[1] -ne 0xBB -or $bytes[2] -ne 0xBF){
    $newBytes = $bom + $bytes
    [System.IO.File]::WriteAllBytes($path, $newBytes)
    Write-Host "BOM added to Integration CSV"
} else {
    Write-Host "BOM already present"
}
