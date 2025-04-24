using System;
using FlutterTools.Data;

namespace FlutterTools.Commands
{
    public enum VisualizeDependencySubMenuAction
    {
        Graphic,
        Console,
        Back,
        None,
        Invalid
    }

    public class ShowSubMenuCommand : BaseFlutterCommand
    {
        private readonly SubMenuType _subMenuType;

        public ShowSubMenuCommand(string projectPath, SubMenuType subMenuType) : base(projectPath)
        {
            _subMenuType = subMenuType;
        }

        public override void Execute()
        {
            while (true)
            {
                switch (_subMenuType)
                {
                    case SubMenuType.VisualizeSubMenu:
                        Console.WriteLine("\nDependency Visualization Menu:");
                        Console.WriteLine("- G  Graphic");
                        Console.WriteLine("- C  Console");
                        Console.WriteLine("- B  Back");
                        Console.Write("Choose an option: ");

                        ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                        VisualizeDependencySubMenuAction action = keyInfo.Key switch
                        {
                            ConsoleKey.G => VisualizeDependencySubMenuAction.Graphic,
                            ConsoleKey.C => VisualizeDependencySubMenuAction.Console,
                            ConsoleKey.B => VisualizeDependencySubMenuAction.Back,
                            ConsoleKey.Enter => VisualizeDependencySubMenuAction.None,
                            _ => VisualizeDependencySubMenuAction.Invalid
                        };

                        Console.WriteLine(); 

                        switch (action)
                        {
                            case VisualizeDependencySubMenuAction.Graphic:
                                new DependencyVisualizationCommand(ProjectPath).Execute();
                                return;
                            case VisualizeDependencySubMenuAction.Console:
                                new DependencyListCommand(ProjectPath).Execute();
                                return;
                            case VisualizeDependencySubMenuAction.Back:
                                return;
                            case VisualizeDependencySubMenuAction.Invalid:
                                Console.WriteLine("Invalid input. Please try again.");
                                break;
                        }
                        break;
                }
            }
        }
    }
}
