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
            new InfoCommand(projectPath, flutter).Execute();
            flutter.PrintInfo();

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
                        break;
                    case MenuAction.OptainInfo:
                        new InfoCommand(projectPath, flutter).Execute();
                        break;
                    case MenuAction.PrintInfo:
                        flutter.PrintInfo();
                        break;
                }
                
                if (action != MenuAction.None)
                {
                    menuManager.PrintMenu();
                }
            }
        }

        
    }
}
