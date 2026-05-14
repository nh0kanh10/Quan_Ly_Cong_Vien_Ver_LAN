$files = Get-ChildItem -Path "C:\Users\ADMIN\Desktop\DaiNamNew" -Recurse -Filter "*.cs" | Where-Object { $_.FullName -match "\\(BUS|DAL)\\.*" }
foreach ($file in $files) {
    $content = Get-Content $file.FullName -Raw
    
    $className = $null
    if ($content -match "public class\s+([A-Za-z0-9_]+)") {
        $className = $matches[1]
    }
    
    if ($className -and $content -match "public static  Instance \{ get; \} = new \(\);") {
        $content = $content -replace "public static  Instance \{ get; \} = new \(\);", "public static $className Instance { get; } = new ${className}();"
        Set-Content -Path $file.FullName -Value $content -Encoding UTF8
        Write-Host "Fixed $($file.Name)"
    }
}
