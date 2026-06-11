using System;
using System.IO;
using Spectre.Console;

namespace FlutterTools.Commands
{
    public class DependencyVisualizationCommand : BaseFlutterCommand
    {
        public DependencyVisualizationCommand(string projectPath) : base(projectPath)
        {
        }

        public override void Execute()
        {
            try
            {
                AnsiConsole.Status()
                    .Start("Activating pubviz and generating graph...", ctx => {
                        string installCommand = "flutter pub global activate pubviz";
                        ExecuteCommand(installCommand);

                        string command = "flutter pub global run pubviz open -f html";
                        ExecuteCommand(command, Path.Combine(ProjectPath, "application"));
                    });

                AnsiConsole.MarkupLine("[green]Dependency graph should now be open in your browser.[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error while generating dependency graph: {ex.Message}[/]");
            }
        }
    }
} 