$MyInvocation.MyCommand.Path | Split-Path | Push-Location

if (Get-Process "Cfx.re Platform Server (FXServer)" -ErrorAction SilentlyContinue) {
    .\Dependencies\icecon_windows_amd64.exe -c "restart fivepd" 127.0.0.1:30120 PMdjCw0yPbw*Vn05lvcC | Out-Null
}