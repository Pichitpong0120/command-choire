using System.Collections.Generic;

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

        public static bool CheckCommandBehavior(string command)
        {
            List<string> listCommand = new List<string>() { MoveUp, MoveDown, MoveLeft, MoveRight, Idle };
            foreach (string item in listCommand)
            {
                if (command == item) return true;
            }

            return false;
        }

        public static bool CheckCommandFunction(string command)
        {
            List<string> listCommand = new List<string>() { Break, Count, If, Else, Loop, SkipTo, Trigger };
            foreach (string item in listCommand)
            {
                if (command == item) return true;
            }

            return false;
        }
    }
}