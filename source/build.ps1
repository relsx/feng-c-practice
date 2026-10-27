param([string]$OutputName='Feng_C语言练习器_ver1.3.exe')
$ErrorActionPreference = 'Stop'
$sourceRoot = $PSScriptRoot
$cscPath = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
if (-not (Test-Path -LiteralPath $cscPath)) { throw '需要 Windows 64 位及 .NET Framework 4.8 或更新版本。' }
$frameworkRelease = (Get-ItemProperty -LiteralPath 'Registry::HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full' -ErrorAction SilentlyContinue).Release
if ($frameworkRelease -lt 528040) { throw '新版 WPF 高 DPI 界面需要 .NET Framework 4.8 或更新版本。' }
$hash = (Get-FileHash -LiteralPath (Join-Path $sourceRoot 'tcc.zip') -Algorithm SHA256).Hash
if ($hash -ne '34A721949A2583FDFF725312DA092FA0F5F1F284B702E6F811C6954714FAABB2') { throw '内置编译器 ZIP 校验失败。' }
$outputPath = Join-Path $sourceRoot ('../'+$OutputName)
$wpfRoot = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\WPF'
$compilerArgs = @('/nologo','/target:winexe','/platform:x64','/optimize+','/utf8output',('/out:'+$outputPath),('/win32manifest:'+(Join-Path $sourceRoot 'app.manifest')),('/win32icon:'+(Join-Path $sourceRoot 'app.ico')),('/resource:'+(Join-Path $sourceRoot 'tcc.zip')+',compiler.zip'),'/reference:System.Drawing.dll','/reference:System.Windows.Forms.dll','/reference:System.Core.dll','/reference:System.Web.Extensions.dll','/reference:System.Numerics.dll',('/reference:'+(Join-Path $sourceRoot 'ICSharpCode.AvalonEdit.dll')),('/resource:'+(Join-Path $sourceRoot 'ICSharpCode.AvalonEdit.dll')+',AvalonEdit.dll'),'/main:Bootstrap','/reference:System.Xaml.dll','/reference:System.IO.Compression.dll','/reference:System.IO.Compression.FileSystem.dll')
$compilerArgs += '/resource:' + (Join-Path $sourceRoot 'languages.json') + ',languages.json'
foreach ($assemblyName in @('PresentationFramework.dll','PresentationCore.dll','WindowsBase.dll')) { $compilerArgs += '/reference:' + (Join-Path $wpfRoot $assemblyName) }
foreach ($assetName in @('executioner-v2.png','smoke.png','spark.png')) { $compilerArgs += '/resource:' + (Join-Path $sourceRoot ('assets/'+$assetName)) + ',fx.'+$assetName }
foreach ($file in @('AppWpf.cs','BrandingWpf.cs','Views.cs','CodeEditor.cs','UiChecks.cs','Engine.cs','Bank.cs','BankData.cs','SelfTest.cs','AssemblyInfo.cs','AdvancedData.cs','AdvancedTests.cs','CodeMetrics.cs','AnswerWindow.cs','Bootstrap.cs','ChallengeBank.cs','ChallengeWindow.cs','Diagnostics.cs','FailureWindow.cs','V11Checks.cs','Transitions.cs','PracticeFeedback.cs','KnowledgeWindow.cs','V111Checks.cs','AppSettings.cs','UpdateService.cs','SettingsWindow.cs','ConstellationMap.cs','ChallengeCurriculum.cs','V121Checks.cs','DarkControls.cs','GlassTheme.cs','GlassChrome.cs','GlassMotion.cs','GlassChecks.cs','GlassCard.cs','UiTypography.cs','PatchChecks.cs','UpdateInstaller.cs','PracticeTools.cs','MistakeBook.cs','Localization.cs','V13Checks.cs','V13WorkflowChecks.cs')) { $compilerArgs += Join-Path $sourceRoot $file }
& $cscPath @compilerArgs
if ($LASTEXITCODE -ne 0) { throw '编译失败。' }
Write-Output "生成完成：$outputPath"
