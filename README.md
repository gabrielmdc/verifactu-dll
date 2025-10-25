# verifactu-dll

This repository contains a multi-targeted .NET library intended for:

1. Cross-platform usage (modern .NET 9 runtime).
2. Windows-specific COM exposure (`net9.0-windows` target) so that parts of the library can be consumed via classic COM registration (`regsvr32`).

## Project Structure & Targets

`VerifactuDll.csproj` targets:
- `net9.0` for modern .NET usage on all supported OSes.
- `net9.0-windows` for Windows COM hosting (gates COM features behind OS check in GitHub Actions).

### Why no netstandard target?
Originally the project targeted `netstandard2.1` for broader compatibility. It was removed to simplify the build and because intended consumers are expected to be on .NET 9. If broader compatibility becomes necessary later (e.g. Unity or older LTS versions), a `net8.0` or `netstandard2.1` target can be reintroduced easily.

COM hosting is enabled conditionally only when building on Windows via `<EnableComHosting>` and `<ComVisible>true</ComVisible>` in the Windows target property group.

## COM Exposure

The file `ComInterfaces.cs` defines:
- `IVerifactu` (COM-visible interface) with simple methods.
- `VerifactuComClass` (COM class) implementing that interface.

GUIDs are hard-coded for determinism. Update them if you add more interfaces/classes (use `uuidgen` or `New-Guid` in PowerShell).

### Building the COM Host

When building/publishing the Windows target on a Windows runner:

```
dotnet publish VerifactuDll.csproj -c Release -f net9.0-windows -r win-x64 --self-contained false
```

The output includes:
- `VerifactuDll.dll` (managed assembly)
- `VerifactuDll.comhost.dll` (native COM host shim created by the SDK)

### Registering with regsvr32

Use `regsvr32` on the generated `VerifactuDll.comhost.dll` (NOT the managed DLL):

```
regsvr32 /s VerifactuDll.comhost.dll
```

Silent (`/s`) is optional; omit it to see dialogs.

To unregister:

```
regsvr32 /u VerifactuDll.comhost.dll
```

### Consuming via COM (Example)

VBScript late-binding example after registration:

```vbscript
Dim obj
Set obj = CreateObject("VerifactuDll.VerifactuComClass")
WScript.Echo obj.Echo("Hello")
WScript.Echo obj.Add(2,3)
```

Early binding (e.g., from C++ or .NET Framework) requires referencing the type library (`tlb`) which can be generated via `tlbexp` if needed. The .NET 9 comhost exports a type library automatically discoverable through registration; if you need a physical `.tlb`, run:

```
tlbexp VerifactuDll.dll /out:VerifactuDll.tlb
```

## GitHub Actions

The workflow (`.github/workflows/build.yml`) performs matrix builds on Ubuntu and Windows:
- Ubuntu: produces cross-platform DLLs.
- Windows: also publishes the COM host artifacts.

Artifacts uploaded per OS include all built DLLs; Windows artifacts contain the `comhost` DLL needed for registration.

## Building COM from Linux?

You cannot produce a usable Windows COM registration (`comhost` ready for `regsvr32`) from a Linux build alone because registration must occur on Windows and COM host generation requires the Windows OS environment to properly test registration. While the managed `VerifactuDll.dll` builds fine on Linux, generating and validating `VerifactuDll.comhost.dll` should be done on a Windows runner (GitHub Actions `windows-latest`).

However, publishing on Linux with the Windows target RID may produce files, but they are not guaranteed/tested for registration there. Best practice: rely on Windows CI for COM artifacts.

## Cross-Platform API

`Class1.PlatformInfo()` returns the current OS description, demonstrating a universal API not tied to COM.

## Next Steps / Contributions

1. Add actual validation + hashing logic for Verifactu data structures.
2. Expand COM surface only as needed (keep small to avoid versioning pains).
3. Add unit tests across all TFMs.
4. Consider strong-naming if required for certain host environments.

## License

See `LICENSE` for open-source terms.

---
Feel free to open issues or PRs for enhancements.
