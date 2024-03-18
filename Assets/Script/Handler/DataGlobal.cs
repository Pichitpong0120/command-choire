using CommandChoice.Model;

namespace CommandChoice.Data
{
    class DataGlobal
    {
        public static float timeDeray = 2f;
        public static int HpDefault { get; private set; } = 3;
        public static int MailDefault { get; private set; } = 0;
        public static SceneModel Scene { get; private set; } = new(StaticText.PathLevelScene);
    }
}
