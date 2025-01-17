using System;
using System.Diagnostics;
using System.Text;

namespace FlutterTools.Commands
{
    public abstract class BaseFlutterCommand : ICommand
    {
        protected readonly string ProjectPath;

        protected BaseFlutterCommand(string projectPath)
        {
            ProjectPath = projectPath;
        }

        public abstract void Execute();

        protected string ExecuteCommand(string command, string workingDirectory = null)
        {
            try
            {
                ProcessStartInfo processInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {command}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8,
                    WorkingDirectory = workingDirectory ?? Directory.GetCurrentDirectory()
                };

                using (Process process = Process.Start(processInfo))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (!string.IsNullOrEmpty(error))
                    {
                        return $"Error: {error}";
                    }

                    return output;
                }
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }
    }
} 