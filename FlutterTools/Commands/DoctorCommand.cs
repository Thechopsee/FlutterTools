using FlutterTools.Data;
using Spectre.Console;

namespace FlutterTools.Commands
{
    public class DoctorCommand : BaseFlutterCommand
    {
        private readonly Flutter _flutter;

        public DoctorCommand(string projectPath, Flutter flutter) : base(projectPath)
        {
            _flutter = flutter;
        }

        public override void Execute()
        {
            string commandDoctor = "flutter doctor";
            string outputDoctor = "";

            AnsiConsole.Status()
                .Start("Running flutter doctor...", ctx => {
                    outputDoctor = ExecuteCommand(commandDoctor);
                });

            AnsiConsole.Write(new Panel(Markup.Escape(outputDoctor)).Header("Flutter Doctor Output").Border(BoxBorder.Rounded));
            _flutter.parseDoctorFromCMDOutput(outputDoctor);
        }
    }
} 