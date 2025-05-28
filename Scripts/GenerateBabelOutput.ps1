$MyInvocation.MyCommand.Path | Split-Path | Push-Location

$msbuildPath = &"${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe" -latest -prerelease -products * -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe

&$msbuildPath ".\FivePD.babel"