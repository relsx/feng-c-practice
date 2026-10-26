$ErrorActionPreference='Stop'
$fixtureRoot=Join-Path $PSScriptRoot ('update-fixture-'+[Guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $fixtureRoot -Force | Out-Null
$compiler=Join-Path $env:WINDIR 'Microsoft.NET/Framework64/v4.0.30319/csc.exe'
$beforeSource=@'
using System;using System.IO;using System.Reflection;using System.Threading;
[assembly: AssemblyVersion("1.2.1.2")]
[assembly: AssemblyFileVersion("1.2.1.2")]
class Program {static void Main(){File.WriteAllText("old-started.txt","ok");Thread.Sleep(2000);}}
'@
$afterSource=@'
using System;using System.IO;using System.Reflection;
[assembly: AssemblyVersion("1.2.1.3")]
[assembly: AssemblyFileVersion("1.2.1.3")]
class Program {static void Main(){File.WriteAllText("updated-ok.txt","1.2.1.3");}}
'@
[IO.File]::WriteAllText((Join-Path $fixtureRoot 'before.cs'),$beforeSource)
[IO.File]::WriteAllText((Join-Path $fixtureRoot 'after.cs'),$afterSource)
$target=Join-Path $fixtureRoot 'FengTest.exe'
$downloaded=Join-Path $fixtureRoot 'Downloaded_new.exe'
$pending=Join-Path $fixtureRoot '.Feng-update-test.exe'
& $compiler /nologo /target:winexe /platform:x64 ('/out:'+$target) (Join-Path $fixtureRoot 'before.cs')
if ($LASTEXITCODE -ne 0) {throw 'fixture compile failed'}
& $compiler /nologo /target:winexe /platform:x64 ('/out:'+$downloaded) (Join-Path $fixtureRoot 'after.cs')
if ($LASTEXITCODE -ne 0) {throw 'fixture compile failed'}
$oldHash=(Get-FileHash -LiteralPath $target -Algorithm SHA256).Hash
$newHash=(Get-FileHash -LiteralPath $downloaded -Algorithm SHA256).Hash
$helper=Join-Path $fixtureRoot 'updater-runner.exe'
Copy-Item -LiteralPath (Join-Path $PSScriptRoot '../Feng_C语言练习器_ver1.2.1.2.exe') -Destination $helper
$parent=Start-Process -FilePath $target -WorkingDirectory $fixtureRoot -PassThru -WindowStyle Hidden
$plan=@{target=$target;pending=$pending;source=$downloaded;sha=$newHash.ToLowerInvariant();oldSha=$oldHash.ToLowerInvariant();size=(Get-Item -LiteralPath $downloaded).Length;version='1.2.1.3';pid=$parent.Id;started=$parent.StartTime.ToUniversalTime().Ticks;elevate=$false}
$manifest=Join-Path $fixtureRoot 'apply.json'
[IO.File]::WriteAllText($manifest,($plan | ConvertTo-Json -Compress))
$helperProcess=Start-Process -FilePath $helper -ArgumentList @('--apply-update-test',('"'+$manifest+'"')) -WorkingDirectory $fixtureRoot -PassThru -WindowStyle Hidden
if (-not $helperProcess.WaitForExit(30000)) {throw 'update helper did not complete'}
if ($helperProcess.ExitCode -ne 0) {throw ('update failed: '+(Get-Content -LiteralPath (Join-Path $fixtureRoot 'update-result.txt') -Raw))}
$marker=Join-Path $fixtureRoot 'updated-ok.txt'
for ($i=0; $i -lt 20 -and -not(Test-Path -LiteralPath $marker); $i++) {Start-Sleep -Milliseconds 100}
$checks=@(
 @{ok=(Test-Path -LiteralPath $marker);text='New EXE automatically launches after old process exits'},
 @{ok=((Get-FileHash -LiteralPath $target -Algorithm SHA256).Hash -eq $newHash);text='Target contains verified new binary'},
 @{ok=((Get-FileHash -LiteralPath ($target+'.previous') -Algorithm SHA256).Hash -eq $oldHash);text='Old binary backed up as .previous'},
 @{ok=(-not(Test-Path -LiteralPath $pending));text='Atomic pending replacement consumed'},
 @{ok=(Test-Path -LiteralPath $downloaded);text='Download remains cached without manual file handling'},
 @{ok=((Get-Content -LiteralPath (Join-Path $fixtureRoot 'update-result.txt') -Raw).StartsWith('SUCCESS'));text='Successful update result recorded'}
)
# A bad checksum must fail before replacement; silence only the test fixture's error dialog.
$plan.sha='a'*64
$plan.oldSha=$newHash.ToLowerInvariant()
$plan.pid=2147483647
[IO.File]::WriteAllText($manifest,($plan | ConvertTo-Json -Compress))
$negative=Start-Process -FilePath $helper -ArgumentList @('--apply-update-test',('"'+$manifest+'"')) -WorkingDirectory $fixtureRoot -PassThru -WindowStyle Hidden
if (-not $negative.WaitForExit(10000)) {throw 'negative updater test did not complete'}
$checks+=@{ok=($negative.ExitCode -eq 1 -and (Get-FileHash -LiteralPath $target -Algorithm SHA256).Hash -eq $newHash);text='Tampered cached download cannot replace the application'}
$plan.sha=$newHash.ToLowerInvariant()
$plan.version='1.2.1.1'
[IO.File]::WriteAllText($manifest,($plan | ConvertTo-Json -Compress))
$downgrade=Start-Process -FilePath $helper -ArgumentList @('--apply-update-test',('"'+$manifest+'"')) -WorkingDirectory $fixtureRoot -PassThru -WindowStyle Hidden
if (-not $downgrade.WaitForExit(10000)) {throw 'downgrade updater test did not complete'}
$checks+=@{ok=($downgrade.ExitCode -eq 1 -and (Get-FileHash -LiteralPath $target -Algorithm SHA256).Hash -eq $newHash);text='Older version rejected without modifying target'}
$report=@()
foreach($check in $checks){$report+=(('FAIL ','PASS ')[[int]$check.ok]+$check.text)}
[IO.File]::WriteAllLines((Join-Path $PSScriptRoot 'updater.txt'),$report)
if ($checks | Where-Object {-not $_.ok}) {throw 'updater checks failed'}
$report+='ALL UPDATER INTEGRATION CHECKS PASSED'
[IO.File]::WriteAllLines((Join-Path $PSScriptRoot 'updater.txt'),$report)
$report
