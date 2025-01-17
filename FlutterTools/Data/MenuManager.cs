using System;

namespace FlutterTools.Data
{
    public class MenuManager
    {
        public void PrintMenu()
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("- F  Optain full info");
            Console.WriteLine("- I  Print optained info");
            Console.WriteLine("- D  Doctor");
            Console.WriteLine("- P  Run pub get in all modules/packages");
            Console.WriteLine("- M  Analyze module dependencies");
            Console.WriteLine("- V  Visualize dependencies (requires GraphViz)");
            Console.WriteLine("- C  Change project path");
            Console.WriteLine("- Q  EXIT");
        }

        public MenuAction HandleKeyPress()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            MenuAction action = keyInfo.Key switch
            {
                ConsoleKey.D => MenuAction.Doctor,
                ConsoleKey.Q => MenuAction.Exit,
                ConsoleKey.P => MenuAction.PubGetAll,
                ConsoleKey.M => MenuAction.ModuleAnalysis,
                ConsoleKey.V => MenuAction.Visualize,
                ConsoleKey.C => MenuAction.ChangePath,
                ConsoleKey.Enter => MenuAction.None,
                _ => MenuAction.Invalid
            };

            if (action != MenuAction.None && action != MenuAction.Invalid)
            {
                DisplayActionMessage(action);
            }

            return action;
        }

        private void DisplayActionMessage(MenuAction action)
        {
            Console.Write($"Executing {action}");
            for (int i = 0; i < 3; i++)
            {
                Console.Write(".");
                Thread.Sleep(500);
            }
            Console.WriteLine();
        }

    }


    public enum MenuAction
    {
        None,
        Invalid,
        Doctor,
        Exit,
        PubGetAll,
        ModuleAnalysis,
        Visualize,
        ChangePath
    }
} 