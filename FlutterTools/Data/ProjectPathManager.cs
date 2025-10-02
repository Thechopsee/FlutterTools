using FlutterTools.Commands;
using System;
using System.IO;
using System.Text.Json;

namespace FlutterTools.Data
{
    public class ProjectPathManager
    {
        private const string ConfigFileName = "last_project_path.json";
        private string _lastProjectPath;
        public string? FlutterPath;

        public string GetProjectPath(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-f" && i + 1 < args.Length)
                {
                    return args[i + 1];
                }
            }

            _lastProjectPath = LoadLastProjectPath();

            string projectPath = _lastProjectPath;
            while (string.IsNullOrEmpty(projectPath) || !Directory.Exists(projectPath))
            {
                if (!string.IsNullOrEmpty(projectPath))
                {
                    Console.WriteLine($"Specified folder does not exist: {projectPath}");
                }

                Console.WriteLine("Enter project path:");
                projectPath = Console.ReadLine();

                if (string.IsNullOrEmpty(projectPath))
                {
                    Console.WriteLine("No path was entered. Please try again.");
                }
            }
            var pathObj= new GetPathToFlutterSDK(projectPath);
            pathObj.Execute();
            FlutterPath = pathObj.PathToFlutterSDK ?? "";

            SaveLastProjectPath(projectPath);
            return projectPath;
        }

        public string ChangeProjectPath()
        {
            Console.WriteLine($"Current project path: {_lastProjectPath}");
            Console.WriteLine("Enter new project path (or press Enter to keep current):");
            string newPath = Console.ReadLine();

            if (string.IsNullOrEmpty(newPath))
            {
                Console.WriteLine("Keeping current path.");
                return _lastProjectPath;
            }

            if (!Directory.Exists(newPath))
            {
                Console.WriteLine($"Specified folder does not exist: {newPath}");
                return _lastProjectPath;
            }

            SaveLastProjectPath(newPath);
            _lastProjectPath = newPath;
            Console.WriteLine($"Project path changed to: {newPath}");
            return newPath;
        }

        private string LoadLastProjectPath()
        {
            try
            {
                string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFileName);
                if (File.Exists(configPath))
                {
                    string json = File.ReadAllText(configPath);
                    return JsonSerializer.Deserialize<string>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading last project path: {ex.Message}");
            }
            return null;
        }

        private void SaveLastProjectPath(string path)
        {
            try
            {
                string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFileName);
                string json = JsonSerializer.Serialize(path);
                File.WriteAllText(configPath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving last project path: {ex.Message}");
            }
        }
    }
} 