# AnnoyingWare

⚠️ **WARNING: Educational Purpose Only** ⚠️

This project demonstrates malicious file manipulation for cybersecurity education. **DO NOT** use on production systems.

## What It Does

AnnoyingWare is a simulated ransomware-like script that:
1. Scans all NTFS volumes on the computer
2. Finds all user files (excluding Windows and Program Files)
3. Renames them to sequential letters (a, b, c... z, aa, ab... az, ba, bb... etc.)
4. Moves all files to a single `C:\annoying\` directory

## File Naming Scheme

Files are renamed using a base-26 alphabetic sequence:
- Files 1-26: `a.ext`, `b.ext`, ... `z.ext`
- Files 27-52: `aa.ext`, `ab.ext`, ... `az.ext`
- Files 53-78: `ba.ext`, `bb.ext`, ... `bz.ext`
- And so on...

## Usage

### Safe Mode (Dry Run - Recommended)
```powershell
# Load the script
. .\AnnoyingWare.ps1

# Run in dry-run mode (shows what would happen without actually doing it)
Start-AnnoyingWare -DryRun
```

### Danger Mode (Actually Executes)
```powershell
# ⚠️ THIS WILL ACTUALLY MOVE AND RENAME FILES! ⚠️
Start-AnnoyingWare -DryRun:$false -TargetVolume 'C'
```

## Parameters

- **`-TargetVolume`**: The drive letter where the `\annoying\` folder will be created (default: "C")
- **`-DryRun`**: When `$true`, only simulates the operation without making changes (default: `$true`)

## Features

- ✅ Scans all NTFS volumes automatically
- ✅ Generates sequential alphabetic file names
- ✅ Preserves file extensions
- ✅ Handles duplicate names with counters
- ✅ Excludes critical system directories (Windows, Program Files)
- ✅ Safe dry-run mode by default
- ✅ Progress tracking and error reporting

## Educational Context

This demonstrates:
- **File system manipulation** across multiple volumes
- **Ransomware-like behavior** (file relocation/renaming)
- **PowerShell system access** capabilities
- **Destructive payload** execution

## Defense Mechanisms

Organizations should protect against such attacks using:
- Proper file system permissions
- Behavior-based antivirus detection
- Application whitelisting
- Regular backups with offline copies
- User education and awareness

## Legal Notice

This code is provided for **educational purposes only** in a controlled cybersecurity learning environment. Unauthorized use of this code against systems you don't own or have permission to test is **illegal** and unethical.

## Author

Created for 3U4 Cybersecurity course demonstrations.
