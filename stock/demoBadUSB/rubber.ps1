
$slash = '/ '
$ProgressPreference = 'SilentlyContinue'
$Url = 'https://www.nirsoft.net/utils/multimonitortool-x64.zip'
$Dest = Join-Path $env:temp 'multimonitortool-x64.zip'
Invoke-WebRequest -Uri $Url -OutFile $Dest
Expand-Archive $Dest $env:temp -Force
$ExePath = Join-Path $env:temp 'multimonitortool.exe'
$arguments = '\setorientation 1 180'.replace('\',$slash)
Start-Process -FilePath $ExePath -ArgumentList $arguments -Wait -PassThru
$arguments = '\setorientation 2 180'.replace('\',$slash)
Start-Process -FilePath $ExePath -ArgumentList $arguments -Wait -PassThru
Clear-History
Clear-Content (Get-PSReadLineOption).HistorySavePath

