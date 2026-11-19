# Genera la carpeta de instalacion de la API: Instalador.exe + la subcarpeta Api (la API ya publicada). Windows x64, no necesita instalar .NET en el servidor.
# Uso (en una maquina con el SDK de .NET 9):   .\publicar.ps1 -Salida C:\temp\KonSolutionsApiInstalacion
param([string]$Salida = ".\KonSolutionsApiInstalacion")
$ErrorActionPreference = "Stop"
$raiz = Split-Path $PSScriptRoot -Parent   # la carpeta KonSolutionsApiFull (la API esta un nivel arriba de este instalador)
if (Test-Path $Salida) { Remove-Item $Salida -Recurse -Force }
dotnet publish "$raiz\KonSolutionsApi.csproj" -c Release -r win-x64 --self-contained true -o "$Salida\Api"
if ($LASTEXITCODE -ne 0) { throw "Fallo la publicacion de la API" }
dotnet publish "$PSScriptRoot\KonSolutionsApiInstalador.csproj" -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o $Salida
if ($LASTEXITCODE -ne 0) { throw "Fallo la publicacion del instalador" }
Write-Host ""
Write-Host "Listo en $Salida. Copia esa carpeta al servidor y ejecuta Instalador.exe como Administrador."
