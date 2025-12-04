# Script pour rickroll un utilisateur
#
# Auteur: Vincent Carrier
# Date: 2024-10-21
# Version: 1.0

$url = "https://www.youtube.com/watch?v=xvFZjo5PgG0&t"
Add-Type -AssemblyName PresentationCore,PresentationFramework
while($true) {
  [System.Diagnostics.Process]::Start("msedge",$url) | Out-Null
  Start-Sleep -Seconds 8
  Get-Process -Name "msedge" | Where-Object MainWindowTitle -like "*Rick roll*" | Stop-Process -Force
  [System.Windows.MessageBox]::Show("Clique OK pour te faire rickroll encore!","RickRoll 1.0",0,0) | Out-Null
}
