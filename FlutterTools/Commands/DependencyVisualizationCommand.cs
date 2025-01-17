using System;
using System.IO;

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
                string installCommand = "flutter pub global activate pubviz";
                string installOutput = ExecuteCommand(installCommand);
                Console.WriteLine(installOutput);

                string command = "flutter pub global run pubviz open -f html";
                string output = ExecuteCommand(command, Path.Combine(ProjectPath, "application"));
                Console.WriteLine(output);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while generating dependency graph: {ex.Message}");
            }
        }
    }
} 