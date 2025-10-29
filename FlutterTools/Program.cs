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
        static Flutter? flutter;
        static string? projectPath;
        static ProjectPathManager? pathManager;
        static MenuManager? menuManager;
        //TODO fix optain full info if optainer change for print full info and after print run optain in backgroud
        
        static void Main(string[] args)
        {
            pathManager = new ProjectPathManager();
            menuManager = new MenuManager();
            projectPath = pathManager.GetProjectPath(args);

            Console.WriteLine($"Project path: {projectPath}");
            Console.WriteLine($"Flutter path: {pathManager.FlutterPath}");

            flutter = new Flutter();
            flutter.FlutterPath = pathManager.FlutterPath ?? "Cant be resolved ,propably not in path";
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
                    case MenuAction.VisualizeSubMenu:
                        new ShowSubMenuCommand(projectPath,SubMenuType.VisualizeSubMenu).Execute();
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
