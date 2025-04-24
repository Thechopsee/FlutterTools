using System;
using System.IO;

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
                string output = ExecuteCommand(command, Path.Combine(ProjectPath, "application"));
                Console.WriteLine("Dependencies:\n");
                Console.WriteLine(output);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while listing dependencies: {ex.Message}");
            }
        }
    }
}
