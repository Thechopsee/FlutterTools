using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using YamlDotNet.RepresentationModel;

namespace FlutterTools.Data
{
    public class ModuleAnalyzer
    {
        private readonly string projectPath;
        private readonly Dictionary<string, HashSet<string>> moduleDependencies;

        public ModuleAnalyzer(string projectPath)
        {
            this.projectPath = projectPath;
            this.moduleDependencies = new Dictionary<string, HashSet<string>>();
        }

        public void AnalyzeModules()
        {
            var targetModules = new List<string>();
            
            string modulesPath = Path.Combine(projectPath, "modules");
            if (Directory.Exists(modulesPath))
            {
                targetModules.AddRange(Directory.GetDirectories(modulesPath)
                    .Select(dir => Path.GetFileName(dir)));
            }

            string packagesPath = Path.Combine(projectPath, "packages");
            if (Directory.Exists(packagesPath))
            {
                targetModules.AddRange(Directory.GetDirectories(packagesPath)
                    .Select(dir => Path.GetFileName(dir)));
            }

            if (!targetModules.Any())
            {
                Console.WriteLine("Složky modules a packages neexistují!");
                return;
            }

            foreach (var moduleName in targetModules)
            {
                string moduleDir = Path.Combine(
                    Directory.Exists(Path.Combine(projectPath, "modules", moduleName)) ? "modules" : "packages",
                    moduleName);
                moduleDir = Path.Combine(projectPath, moduleDir);
                
                string pubspecPath = Path.Combine(moduleDir, "pubspec.yaml");
                if (!File.Exists(pubspecPath))
                {
                    Console.WriteLine($"pubspec.yaml nenalezen v modulu {moduleName}");
                    continue;
                }

                Console.WriteLine($"\nAnalyzuji modul: {moduleName}");
                var dependencies = ParsePubspecDependencies(pubspecPath);
                moduleDependencies[moduleName] = dependencies;

                CheckModuleImports(moduleDir, moduleName, dependencies, targetModules);
            }
        }

        private HashSet<string> ParsePubspecDependencies(string pubspecPath)
        {
            var dependencies = new HashSet<string>();
            var yaml = new YamlStream();
            
            yaml.Load(File.OpenText(pubspecPath));

            var root = (YamlMappingNode)yaml.Documents[0].RootNode;
            if (root.Children.ContainsKey("dependencies"))
            {
                var depsNode = (YamlMappingNode)root.Children["dependencies"];
                foreach (var dep in depsNode.Children)
                {
                    string depName = ((YamlScalarNode)dep.Key).Value;
                    dependencies.Add(depName);
                    
                }
            }

            return dependencies;
        }

        private void CheckModuleImports(string moduleDir, string moduleName, HashSet<string> allowedDependencies,List<string> targetModules)
        {
            var dartFiles = Directory.GetFiles(moduleDir, "*.dart", SearchOption.AllDirectories);
            bool foundErrors = false;

            foreach (var file in dartFiles)
            {
                string content = File.ReadAllText(file);
                var imports = ExtractImports(content);

                
                foreach (var import in imports)
                {
                    bool cc = moduleDependencies.ContainsKey(import);
                    if (!allowedDependencies.Contains(import) && import != moduleName && targetModules.Contains(import))
                    {
                        if (!foundErrors)
                        {
                            Console.WriteLine($"\nChyby v modulu {moduleName}:");
                            foundErrors = true;
                        }
                        Console.WriteLine($"  Soubor: {Path.GetFileName(file)}");
                        Console.WriteLine($"    Import {import} není definován v pubspec.yaml");
                    }
                }
            }

            if (!foundErrors)
            {
                Console.WriteLine($"  ✓ Všechny importy jsou správně definovány v pubspec.yaml");
            }
        }

        private List<string> ExtractImports(string content)
        {
            var imports = new List<string>();
            var importRegex = new Regex(@"import\s+['""](package:[^'""]+)['""]");
            var matches = importRegex.Matches(content);

            foreach (Match match in matches)
            {
                imports.Add(match.Groups[1].Value.Split(":")[1].Split("/")[0]);
            }

            return imports;
        }

        public Dictionary<string, string> GetDependencies()
        {
            var result = new Dictionary<string, string>();
            foreach (var module in moduleDependencies)
            {
                foreach (var dep in module.Value)
                {
                    result[module.Key] = dep;
                }
            }
            return result;
        }
    }
} 