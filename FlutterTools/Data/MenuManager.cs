using System;
using Spectre.Console;

namespace FlutterTools.Data
{
    public class MenuManager
    {
        public void PrintMenu()
        {
            // Empty because we use SelectionPrompt in HandleKeyPress
        }

        public MenuAction HandleKeyPress()
        {
            var action = AnsiConsole.Prompt(
                new SelectionPrompt<MenuAction>()
                    .Title("[yellow]Main Menu[/]")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                    .AddChoices(new[] {
                        MenuAction.OptainInfo,
                        MenuAction.PrintInfo,
                        MenuAction.Doctor,
                        MenuAction.PubGetAll,
                        MenuAction.ModuleAnalysis,
                        MenuAction.VisualizeSubMenu,
                        MenuAction.ChangePath,
                        MenuAction.Exit
                    })
                    .UseConverter(action => action switch
                    {
                        MenuAction.OptainInfo => "Obtain full info",
                        MenuAction.PrintInfo => "Print obtained info",
                        MenuAction.Doctor => "Doctor",
                        MenuAction.PubGetAll => "Run pub get in all modules/packages",
                        MenuAction.ModuleAnalysis => "Analyze module dependencies",
                        MenuAction.VisualizeSubMenu => "Show Dependencies",
                        MenuAction.ChangePath => "Change project path",
                        MenuAction.Exit => "EXIT",
                        _ => action.ToString()
                    }));

            if (action != MenuAction.Exit)
            {
                AnsiConsole.MarkupLine($"[blue]Executing {action}...[/]");
            }

            return action;
        }

        private void DisplayActionMessage(MenuAction action)
        {
            // No longer used, replaced by inline message in HandleKeyPress or Status
        }

    }


    public enum MenuAction
    {
        None,
        Invalid,
        Doctor,
        Exit,
        PubGetAll,
        ModuleAnalysis,
        Visualize,
        ChangePath,
        OptainInfo,
        VisualizeSubMenu,
        PrintInfo
    }

    public enum SubMenuType
    {
        VisualizeSubMenu,
    }
} 