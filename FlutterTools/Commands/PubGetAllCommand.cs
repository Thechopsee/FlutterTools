using System;
using System.IO;

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

            foreach (var folder in targetFolders)
            {
                string fullPath = Path.Combine(ProjectPath, folder);

                if (!Directory.Exists(fullPath))
                {
                    Console.WriteLine($"Folder {folder} does not exist, skipping.");
                    continue;
                }

                Console.WriteLine($"Processing: {fullPath}");

                foreach (var subDir in Directory.GetDirectories(fullPath))
                {
                    Console.WriteLine($"Running pub get in: {subDir}");
                    string command = "flutter pub get";
                    string output = ExecuteCommand(command, subDir);
                    Console.WriteLine($"Output from {Path.GetFileName(subDir)}:\n{output}");
                }
            }
        }
    }
} 