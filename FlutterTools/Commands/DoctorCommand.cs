using FlutterTools.Data;

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
            string outputDoctor = ExecuteCommand(commandDoctor);
            Console.WriteLine(outputDoctor);
            _flutter.parseDoctorFromCMDOutput(outputDoctor);
        }
    }
} 