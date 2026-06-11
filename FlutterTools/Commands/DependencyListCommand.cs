using System;
using System.IO;
using Spectre.Console;

namespace FlutterTools.Commands
{
    public class DependencyListCommand : BaseFlutterCommand
    {
        public DependencyListCommand(string projectPath) : base(projectPath)
        {
        }

        public override void Execute()
        {
            try
            {
                string command = "flutter pub deps";
                string output = "";
                AnsiConsole.Status()
                    .Start("Fetching dependencies...", ctx => {
                        output = ExecuteCommand(command, Path.Combine(ProjectPath, "application"));
                    });
                AnsiConsole.Write(new Panel(output).Header("Dependencies").Border(BoxBorder.Rounded));
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error while listing dependencies: {ex.Message}[/]");
            }
        }
    }
}
