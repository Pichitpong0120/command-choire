using System.Collections.Generic;
using CommandChoice.Component;

namespace CommandChoice.Model
{
    class StaticText
    {
        public const string MoveUp = "Move Up";
        public const string MoveDown = "Move Down";
        public const string MoveLeft = "Move Left";
        public const string MoveRight = "Move Right";
        public const string Idle = "Idle";
        public const string Break = "Break";
        public const string Count = "Count";
        public const string If = "If";
        public const string Else = "Else";
        public const string Loop = "Loop";
        public const string SkipTo = "Skip To";
        public const string Trigger = "Trigger";

        public static bool CheckCommand(string command)
        {
            if (CheckCommandBehavior(command)) return true;
            return CheckCommandFunction(command);
        }

        public static bool CheckCommandBehavior(string command)
        {
            List<string> listCommand = new() { MoveUp, MoveDown, MoveLeft, MoveRight, Idle };
            foreach (string item in listCommand)
            {
                if (command == item) return true;
            }

            return false;
        }

        public static bool CheckCommandFunction(string command)
        {
            List<string> listCommand = new() { Break, Count, If, Else, Loop, SkipTo, Trigger };
            foreach (string item in listCommand)
            {
                if (command == item) return true;
            }

            return false;
        }

        public static bool CheckCommandCanConfig(string command)
        {
            List<string> listCommand = new() { Loop };
            foreach (string item in listCommand)
            {
                if (command == item) return true;
            }

            return false;
        }

        public static bool CheckCommandCanTrigger(string command)
        {
            List<string> listCommand = new() { If, Else, SkipTo, Trigger };
            foreach (string item in listCommand)
            {
                if (command == item) return true;
            }

            return false;
        }

        public static string CommandDisplay(string text, CommandFunction commandFunction)
        {
            string textUpdate = "";

            switch (text)
            {
                case Idle:
                    textUpdate = text;
                    break;
                case MoveUp:
                    textUpdate = text;
                    break;
                case MoveDown:
                    textUpdate = text;
                    break;
                case MoveLeft:
                    textUpdate = text;
                    break;
                case MoveRight:
                    textUpdate = text;
                    break;
                case Break:
                    textUpdate = text;
                    break;
                case Count:
                    textUpdate = $"{text} : {commandFunction.count}";
                    break;
                case If:
                    textUpdate = $"{text} : {commandFunction.trigger}";
                    break;
                case Else:
                    textUpdate = text;
                    break;
                case Loop:
                    textUpdate = $"{text} : Count {commandFunction.count}";
                    break;
                case SkipTo:
                    textUpdate = $"{text} : {commandFunction.trigger}";
                    break;
                case Trigger:
                    textUpdate = $"{text} : {commandFunction.trigger}";
                    break;
            }
            return textUpdate;
        }
    }
}