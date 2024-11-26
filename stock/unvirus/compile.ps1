# Installe PS2EXE au besoin
If (-not (Get-Module -Name PS2EXE -ListAvailable)) {
    Install-Module -Name PS2EXE -Force -AllowClobber -Scope CurrentUser -SkipPublisherCheck
}

# Compile le script en exécutable
Invoke-PS2EXE -inputFile ".\unvirus.ps1" -outputFile ".\unvirus.exe" -noConsole -noOutput -noError -noVisualStyles 
