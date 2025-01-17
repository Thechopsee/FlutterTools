using FlutterTools.Data;
using FlutterTools.Commands;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace FlutterTools
{
    internal class Program
    {
        static Flutter flutter;
        static String projectPath;
        static ProjectPathManager pathManager;
        static MenuManager menuManager;
        //TODO: LAST output
        //TODO fix optain full info if optainer change for print full info and after print run optain in backgroud
        //TODO: clearing after cmd
        
        static void Main(string[] args)
        {
            pathManager = new ProjectPathManager();
            menuManager = new MenuManager();
            projectPath = pathManager.GetProjectPath(args);

            Console.WriteLine($"Project path: {projectPath}");

            flutter = new Flutter();
            string command = "flutter --version"; 
           // var baseCommand = new BaseFlutterCommand(projectPath);
            string output = ExecuteCommand(command);
           
            flutter.parseVersionFromCMDOutput(output);
            
            Console.WriteLine(flutter.Version);
            Console.WriteLine(flutter.DartVersion);
            Console.WriteLine(flutter.DevToolsVersion);

            menuManager.PrintMenu();
            while (true)
            {
                MenuAction action = menuManager.HandleKeyPress();
                
                switch (action)
                {
                    case MenuAction.Doctor:
                        new DoctorCommand(projectPath, flutter).Execute();
                        break;
                    case MenuAction.Exit:
                        Console.WriteLine("\nProgram ukončen.");
                        return;
                    case MenuAction.PubGetAll:
                        new PubGetAllCommand(projectPath).Execute();
                        break;
                    case MenuAction.ModuleAnalysis:
                        new ModuleAnalysisCommand(projectPath).Execute();
                        break;
                    case MenuAction.Visualize:
                        new DependencyVisualizationCommand(projectPath).Execute();
                        break;
                    case MenuAction.ChangePath:
                        projectPath = pathManager.ChangeProjectPath();
                        break;
                    case MenuAction.Invalid:
                        Console.WriteLine("\nNeplatná volba. Zkuste to znovu.");
                        break;
                    case MenuAction.None:
                        // Nic nedělej při stisku Enter
                        break;
                }
                
                if (action != MenuAction.None)
                {
                    menuManager.PrintMenu();
                }
            }
        }

        static void ExecutePubGetAll()
        {
            //TODO: Progresbar
            string[] targetFolders = { "modules", "packages","application" };

            foreach (var folder in targetFolders)
            {
                string fullPath = Path.Combine(projectPath, folder);

                if (!Directory.Exists(fullPath))
                {
                    Console.WriteLine($"Folder {folder} does not exist, skipping.");
                    continue;
                }

                Console.WriteLine($"Processing: {fullPath}");

                foreach (var subDir in Directory.GetDirectories(fullPath))
                {
                    Console.WriteLine($"Running pub get in: {subDir}");
                    string command = "flutter pub get";
                    string output = ExecuteCommand(command, subDir);
                    Console.WriteLine($"Output from {Path.GetFileName(subDir)}:\n{output}");
                }
            }
        }

        static void ExecuteModuleAnalysis()
        {
            var analyzer = new ModuleAnalyzer(projectPath);
            analyzer.AnalyzeModules();
        }

        static string ExecuteCommand(string command, string workingDirectory = null)
        {
            try
            {
                ProcessStartInfo processInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {command}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8,
                    WorkingDirectory = workingDirectory ?? Directory.GetCurrentDirectory()
                };

                using (Process process = Process.Start(processInfo))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (!string.IsNullOrEmpty(error))
                    {
                        return $"Error: {error}";
                    }

                    return output;
                }
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }
    }
}
