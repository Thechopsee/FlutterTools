using FlutterTools.Commands;
using System;
using System.IO;
using System.Text.Json;
using Spectre.Console;

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
            if (string.IsNullOrEmpty(projectPath) || !Directory.Exists(projectPath))
            {
                projectPath = AnsiConsole.Prompt(
                    new TextPrompt<string>("Enter [green]project path[/]:")
                        .Validate(path =>
                        {
                            if (string.IsNullOrWhiteSpace(path))
                            {
                                return ValidationResult.Error("[red]Path cannot be empty[/]");
                            }
                            if (!Directory.Exists(path))
                            {
                                return ValidationResult.Error("[red]Specified folder does not exist[/]");
                            }
                            return ValidationResult.Success();
                        }));
            }

            var pathObj= new GetPathToFlutterSDK(projectPath);
            pathObj.Execute();
            FlutterPath = pathObj.PathToFlutterSDK ?? "";

            SaveLastProjectPath(projectPath);
            return projectPath;
        }

        public string ChangeProjectPath()
        {
            AnsiConsole.MarkupLine($"Current project path: [blue]{_lastProjectPath}[/]");

            string newPath = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter [green]new project path[/] (or press Enter to keep current):")
                    .AllowEmpty()
                    .Validate(path =>
                    {
                        if (string.IsNullOrWhiteSpace(path)) return ValidationResult.Success();
                        if (!Directory.Exists(path))
                        {
                            return ValidationResult.Error("[red]Specified folder does not exist[/]");
                        }
                        return ValidationResult.Success();
                    }));

            if (string.IsNullOrEmpty(newPath))
            {
                AnsiConsole.MarkupLine("[yellow]Keeping current path.[/]");
                return _lastProjectPath;
            }

            SaveLastProjectPath(newPath);
            _lastProjectPath = newPath;
            AnsiConsole.MarkupLine($"Project path changed to: [blue]{newPath}[/]");
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