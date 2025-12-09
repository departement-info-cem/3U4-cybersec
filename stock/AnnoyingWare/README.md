# AnnoyingWare

⚠️ **WARNING: Educational Purpose Only** ⚠️

This project demonstrates malicious file manipulation for cybersecurity education. **DO NOT** use on production systems.

## What It Does

AnnoyingWare is a simulated ransomware-like program that:
1. Scans all fixed disk volumes on the computer
2. Finds all user files (excluding Windows and Program Files)
3. Renames them to sequential letters (a, b, c... z, aa, ab... az, ba, bb... etc.)
4. Moves all files to a single `C:\annoying\` directory

## File Naming Scheme

Files are renamed using a base-26 alphabetic sequence:
- Files 1-26: `a.ext`, `b.ext`, ... `z.ext`
- Files 27-52: `aa.ext`, `ab.ext`, ... `az.ext`
- Files 53-78: `ba.ext`, `bb.ext`, ... `bz.ext`
- And so on...

## Files

- **`AnnoyingWare.exe`** - Self-contained executable (~36 MB)
- **`AnnoyingWare/`** - C# source code project

## Usage

### Safe Mode (Dry Run - Recommended)
```cmd
# Run in dry-run mode (shows what would happen without actually doing it)
AnnoyingWare.exe
```

### Danger Mode (Actually Executes)
```cmd
# ⚠️ THIS WILL ACTUALLY MOVE AND RENAME FILES! ⚠️
AnnoyingWare.exe --execute
```

### Custom Volume
```cmd
# Specify a different target volume (default is C)
AnnoyingWare.exe --execute --volume=D
```

## Command Line Arguments

- **`--execute`**: Disables dry-run mode and actually moves/renames files (DANGEROUS!)
- **`--volume=X`**: Specifies the drive letter where `\annoying\` folder will be created (default: "C")

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
