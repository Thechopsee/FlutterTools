namespace FlutterTools.Commands
{
    public class InfoCommand : BaseFlutterCommand
    {
        private readonly Flutter _flutter;

        public InfoCommand(string projectPath, Flutter flutter) : base(projectPath)
        {
            _flutter = flutter;
        }

        public override void Execute()
        {
            string command = "flutter --version"; 
            string output = ExecuteCommand(command);
           
            _flutter.parseVersionFromCMDOutput(output);
            Console.WriteLine(outputDoctor);
        }
    }
}