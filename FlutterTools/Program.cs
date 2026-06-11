using FlutterTools.Data;
using FlutterTools.Commands;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Spectre.Console;

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

            flutter = new Flutter();
            flutter.FlutterPath = pathManager.FlutterPath ?? "Cant be resolved, probably not in path";

            AnsiConsole.Status()
                .Start("Obtaining info...", ctx => {
                    new InfoCommand(projectPath, flutter).Execute();
                });

            while (true)
            {
                ShowHeader();
                flutter.PrintInfo();

                MenuAction action = menuManager.HandleKeyPress();
                
                if (action == MenuAction.Exit)
                {
                    AnsiConsole.MarkupLine("[yellow]Program ukončen.[/]");
                    return;
                }

                switch (action)
                {
                    case MenuAction.Doctor:
                        new DoctorCommand(projectPath, flutter).Execute();
                        break;
                    case MenuAction.PubGetAll:
                        new PubGetAllCommand(projectPath).Execute();
                        break;
                    case MenuAction.ModuleAnalysis:
                        new ModuleAnalysisCommand(projectPath).Execute();
                        break;
                    case MenuAction.VisualizeSubMenu:
                        new ShowSubMenuCommand(projectPath, SubMenuType.VisualizeSubMenu).Execute();
                        break;
                    case MenuAction.ChangePath:
                        projectPath = pathManager.ChangeProjectPath();
                        break;
                    case MenuAction.Invalid:
                        AnsiConsole.MarkupLine("[red]Neplatná volba. Zkuste to znovu.[/]");
                        break;
                    case MenuAction.None:
                        break;
                    case MenuAction.OptainInfo:
                         AnsiConsole.Status()
                            .Start("Obtaining info...", ctx => {
                                new InfoCommand(projectPath, flutter).Execute();
                            });
                        break;
                    case MenuAction.PrintInfo:
                        flutter.PrintInfo();
                        break;
                }

                if (action != MenuAction.None && action != MenuAction.VisualizeSubMenu)
                {
                    AnsiConsole.WriteLine();
                    AnsiConsole.MarkupLine("[grey]Press any key to return to menu...[/]");
                    Console.ReadKey(true);
                }
            }
        }

        static void ShowHeader()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("FlutterTools")
                    .LeftJustified()
                    .Color(Color.Blue));

            AnsiConsole.MarkupLine($"[bold]Project path:[/] [blue]{projectPath}[/]");
            AnsiConsole.MarkupLine($"[bold]Flutter path:[/] [blue]{pathManager?.FlutterPath ?? "N/A"}[/]");
            AnsiConsole.WriteLine();
        }

        
    }
}
