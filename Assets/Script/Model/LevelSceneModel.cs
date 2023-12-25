namespace CommandChoice.Model
{
    public class LevelSceneModel
    {
        public string NameLevelScene { get; private set; } = "LevelScene";
        public LevelSceneDetailModel DetailLevelScene { get; private set; } = new();

        public string getNameForLoadScene(){
            return NameLevelScene.Replace(' ', '_');
        }

        public LevelSceneModel(string NameLevelScene)
        {
            this.NameLevelScene = NameLevelScene.Replace('_', ' ');
        }
    }

    public class LevelSceneDetailModel
    {
        public bool UnLockLevelScene { get; set; } = false;
        public double ScoreLevelScene { get; set; } = 0;
        public int CountBoxCommand { get; set; } = 0;
        public int MailLevelScene { get; set; } = 0;
        public int UseTime { get; set; } = 0;
    }
}