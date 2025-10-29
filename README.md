# FlutterTools 🛠️

**FlutterTools** is a small CLI app with a growing set of helpful utilities for working with large-scale Flutter projects.

> ⚠️ This is a work-in-progress project. New features are added as they're needed in my primary development workflow.

---

## 🚀 Features

- **Multi-package `pub get`**  
  Easily run `pub get` across multiple packages or modules — no more switching folders manually.

- **Cross-module import checker** *(fixing coming soon)*  
  Identify problematic imports between packages that can cause issues when:
  - Building in Linux containers
  - Using IntelliSense in Android Studio

- **Flutter environment check**  
  Run `flutter doctor` and get a quick overview of your Flutter/Dart setup.

- **HTML import visualization**  
  Generate an interactive HTML file to visualize your project's import structure.

---

## 📝 TODO

- [ ] Add lowercase normalization for import paths  
- [ ] Add command-line import visualization (graph view)

---

## 💡 Why?

Managing a growing Flutter codebase across multiple packages can get messy. This tool aims to automate the boring parts, spot hidden issues, and make dev life just a bit smoother.

---

## 📦 Installation


```bash
git clone https://github.com/yourusername/fluttertools.git
cd fluttertools
dart run
