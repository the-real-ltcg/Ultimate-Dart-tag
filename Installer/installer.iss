#define MyAppName "Ultimate Dart Tag"
#define MyAppVersion "1.0"
#define MyAppExeName "Ultimate Nerf Dart Tag.exe"
#define MyBuildDir "..\Built EXE"

[Setup]
AppId={{6C2E9C6B-7B1E-4C9E-9C0A-7F9F1E9B6A21}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
OutputDir=Output
OutputBaseFilename=UltimateDartTag-Setup
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
ArchitecturesInstallIn64BitMode=x64compatible
UninstallDisplayIcon={app}\{#MyAppExeName}

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "Create a &desktop shortcut"; GroupDescription: "Additional shortcuts:"

[Files]
Source: "{#MyBuildDir}\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MyBuildDir}\UnityCrashHandler64.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MyBuildDir}\UnityPlayer.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MyBuildDir}\Ultimate Nerf Dart Tag_Data\*"; DestDir: "{app}\Ultimate Nerf Dart Tag_Data"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#MyBuildDir}\MonoBleedingEdge\*"; DestDir: "{app}\MonoBleedingEdge"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#MyBuildDir}\D3D12\*"; DestDir: "{app}\D3D12"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\Uninstall {#MyAppName}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "Launch {#MyAppName}"; Flags: nowait postinstall skipifsilent
