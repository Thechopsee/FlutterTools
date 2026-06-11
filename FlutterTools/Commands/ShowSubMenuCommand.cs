using System;
using FlutterTools.Data;
using Spectre.Console;

namespace FlutterTools.Commands
{
    public enum VisualizeDependencySubMenuAction
    {
        Graphic,
        Console,
        Back,
        None,
        Invalid
    }

    public class ShowSubMenuCommand : BaseFlutterCommand
    {
        private readonly SubMenuType _subMenuType;

        public ShowSubMenuCommand(string projectPath, SubMenuType subMenuType) : base(projectPath)
        {
            _subMenuType = subMenuType;
        }

        public override void Execute()
        {
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new FigletText("FlutterTools")
                        .LeftJustified()
                        .Color(Color.Blue));

                switch (_subMenuType)
                {
                    case SubMenuType.VisualizeSubMenu:
                        var action = AnsiConsole.Prompt(
                            new SelectionPrompt<VisualizeDependencySubMenuAction>()
                                .Title("[yellow]Dependency Visualization Menu[/]")
                                .AddChoices(new[] {
                                    VisualizeDependencySubMenuAction.Graphic,
                                    VisualizeDependencySubMenuAction.Console,
                                    VisualizeDependencySubMenuAction.Back
                                })
                                .UseConverter(action => action switch
                                {
                                    VisualizeDependencySubMenuAction.Graphic => "Graphic",
                                    VisualizeDependencySubMenuAction.Console => "Console",
                                    VisualizeDependencySubMenuAction.Back => "Back",
                                    _ => action.ToString()
                                }));

                        switch (action)
                        {
                            case VisualizeDependencySubMenuAction.Graphic:
                                new DependencyVisualizationCommand(ProjectPath).Execute();
                                WaitForAnyKey();
                                break;
                            case VisualizeDependencySubMenuAction.Console:
                                new DependencyListCommand(ProjectPath).Execute();
                                WaitForAnyKey();
                                break;
                            case VisualizeDependencySubMenuAction.Back:
                                return;
                        }
                        break;
                }
            }
        }

        private void WaitForAnyKey()
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[grey]Press any key to return to menu...[/]");
            Console.ReadKey(true);
        }
    }
}
