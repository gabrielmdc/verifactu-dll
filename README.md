# verifactu-dll

Multi‑target .NET library:

- `net9.0`: modern cross‑platform usage (no COM)
- `net48` (x86): classic COM exposure for Windows clients (VB6, VBA, scripting) via `RegAsm`

## Project Targets

`VerifactuDll.csproj` targets:
- `net9.0` for modern .NET usage on all supported OSes
- `net48` for Windows x86 COM hosting

## Build

Linux (only net9.0):
```bash
dotnet build VerifactuDll.csproj -c Release -f net9.0
```

Windows (both):
```cmd
dotnet build VerifactuDll.csproj -c Release -f net48
dotnet build VerifactuDll.csproj -c Release -f net9.0
```

## COM Registration (net48)

Register (generates TLB for early binding):
```cmd
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe VerifactuDll.dll /tlb /codebase
```

Unregister:
```cmd
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe VerifactuDll.dll /unregister
```

Notes:
- Use the 32‑bit RegAsm path (`Framework\` not `Framework64\`) because build is x86.
- `/codebase` optional; omit if deploying to GAC or same folder as consumer.
- Do NOT use `regsvr32` (assembly is managed; no comhost target here).

## Why no `netstandard` or `net9.0-windows` COM?

- `netstandard2.0` omitted for simplicity; can be added later for broader runtime reach.
- Modern COM via comhost (`net9.0-windows`) was removed to avoid requiring installation of a .NET runtime on client machines; `net48` works with the .NET Framework already present on Windows.

## License

See `LICENSE` for open-source terms.

---
Feel free to open issues or PRs for enhancements.
