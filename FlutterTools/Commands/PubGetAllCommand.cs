using System;
using System.IO;
using Spectre.Console;

namespace FlutterTools.Commands
{
    public class PubGetAllCommand : BaseFlutterCommand
    {
        public PubGetAllCommand(string projectPath) : base(projectPath)
        {
        }

        public override void Execute()
        {
            string[] targetFolders = { "modules", "packages", "application" };
            var table = new Table();
            table.AddColumn("Module/Package");
            table.AddColumn("Status");

            AnsiConsole.Status()
                .Start("Running pub get...", ctx => {
                    foreach (var folder in targetFolders)
                    {
                        string fullPath = Path.Combine(ProjectPath, folder);

                        if (!Directory.Exists(fullPath))
                        {
                            continue;
                        }

                        if (folder == "application")
                        {
                            ctx.Status($"Running pub get in: [blue]application[/]");
                            string output = ExecuteCommand("flutter pub get", fullPath);
                            table.AddRow("application", output.Contains("Error") ? $"[red]{output}[/]" : "[green]Success[/]");
                        }
                        else
                        {
                            foreach (var subDir in Directory.GetDirectories(fullPath))
                            {
                                string folderName = Path.GetFileName(subDir);
                                ctx.Status($"Running pub get in: [blue]{folderName}[/]");
                                string output = ExecuteCommand("flutter pub get", subDir);
                                table.AddRow(folderName, output.Contains("Error") ? $"[red]{output}[/]" : "[green]Success[/]");
                            }
                        }
                    }
                });

            AnsiConsole.Write(table);
        }
    }
} 