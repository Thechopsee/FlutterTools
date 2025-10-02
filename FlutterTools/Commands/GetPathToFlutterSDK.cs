using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterTools.Commands;
public class GetPathToFlutterSDK : BaseFlutterCommand
{
    public string? PathToFlutterSDK { get; set; }
    public GetPathToFlutterSDK(string projectPath) : base(projectPath)
    {
    }

    public override void Execute()
    {
        var commnad = "where flutter";
        var output = ExecuteCommand(commnad);
        PathToFlutterSDK = output;
    }
}
