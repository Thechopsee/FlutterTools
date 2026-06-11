# FlutterTools 🛠️

**FlutterTools** is a .NET-based CLI utility designed to streamline the management of large-scale, multi-package Flutter projects. It automates repetitive tasks such as running `pub get` across multiple modules and helps maintain architectural integrity by identifying missing dependencies in `pubspec.yaml` based on Dart imports.

---

## 🚀 Features

- **Multi-package `pub get`**
  Automatically runs `flutter pub get` across all packages located in `modules/` and `packages/` directories.
- **Cross-module Dependency Analysis**
  Analyzes Dart files in your modules to ensure that all internal package imports are correctly declared in their respective `pubspec.yaml` files.
- **Dependency Visualization**
  Generates and opens an interactive HTML dependency graph using `pubviz`.
- **Flutter Environment Check**
  Quickly run `flutter doctor` directly from the tool.
- **Project Information**
  Displays current project path and Flutter SDK information.
- **Dependency Listing**
  Runs `flutter pub deps` for the main application to show a full dependency tree.

---

## 📂 Expected Project Structure

FlutterTools is optimized for projects following this structure:

```
my_flutter_project/
├── application/      # The main Flutter application
├── modules/          # Directory containing internal modules/packages
│   ├── module_a/
│   └── module_b/
└── packages/         # Directory containing additional internal packages
    ├── package_x/
    └── package_y/
```

---

## 🛠️ Prerequisites

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [Flutter SDK](https://docs.flutter.dev/get-started/install)

---

## 📦 Installation & Usage

### From Source

1. Clone the repository:
   ```bash
   git clone https://github.com/[your-username]/fluttertools.git
   cd fluttertools
   ```

2. Run the application using the .NET CLI:
   ```bash
   dotnet run --project FlutterTools/FlutterTools.csproj
   ```

### Building the Executable

To build a standalone executable:
```bash
dotnet build --configuration Release
```
The executable will be located in `FlutterTools/bin/Release/net8.0/`.

---

## 📝 TODO

- [ ] Add lowercase normalization for import paths.
- [ ] Add command-line import visualization (graph view).
- [ ] Support custom folder structures.
- [ ] Improve error handling for missing Flutter SDK.

---

## 💡 Why?

Managing a growing Flutter codebase across dozens of packages can be challenging. FlutterTools aims to automate the "boring parts," detect hidden dependency issues early, and provide better visibility into your project's structure.
