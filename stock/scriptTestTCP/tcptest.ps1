# Script pour tester des ports TCP sur les postes de travail du laboratoire
#
# Auteur:  Vincent Carrier
# Version: 1.0
# Date:    2024-10-09

param(
    [int32[]] $Ports = @(445),
    [uint32] $TimeoutDelay = 100,
    [uint32] $RefreshDelay = 15
)

if (-not (Get-Module -Name AdsiPS -ListAvailable)) {
    Install-Module -Name AdsiPS -Scope CurrentUser -Force
}

while ($true) {

    Clear-Host
    "Chargement en cours..." | Write-Host -ForegroundColor Yellow

    $rapport = 1..24 | ForEach-Object { 
        $ComputerName = '{0}{1:d2}' -f $env:cem_prefix, $_

        $ret = [pscustomobject]@{
            ComputerName = $ComputerName
        }
    
        foreach ($Port in $Ports) {
        
            $client = New-Object System.Net.Sockets.TcpClient
            $client.BeginConnect($ComputerName, $Port, $null, $null) | Out-Null
            Start-Sleep -Milliseconds $TimeoutDelay
    
            $ret | Add-Member -MemberType NoteProperty -Name "$port/tcp" -Value $client.Connected
            $client.Close()
        }
    
        $ret
    
    } 
    
    Clear-Host
    $rapport | Sort-Object ComputerName | Format-Table

    "Prochain rafraichissement dans..." | Write-Host -ForegroundColor Yellow
    for ($i = $RefreshDelay; $i -gt 0; $i--) {
        "$i..." | Write-Host -NoNewline -ForegroundColor Yellow
        Start-Sleep -Seconds 1
    }
}
