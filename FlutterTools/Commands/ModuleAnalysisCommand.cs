using FlutterTools.Data;

namespace FlutterTools.Commands
{
    public class ModuleAnalysisCommand : BaseFlutterCommand
    {
        public ModuleAnalysisCommand(string projectPath) : base(projectPath)
        {
        }

        public override void Execute()
        {
            var analyzer = new ModuleAnalyzer(ProjectPath);
            analyzer.AnalyzeModules();
        }
    }
} 