using System;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneGameManager
{
    private List<string> listScene;
    private List<string> path;

    private static Action onLoaderCallback;

    public enum Scene
    {
        MainMenu,
        Loading,
        SelectLevel,
        Level_1,
    }

    public int SceneCount()
    {
        // Get the scene count in BuildSettings
        int sceneCount = EditorBuildSettings.scenes.Length;

        //Debug.Log("Scene count: " + sceneCount);

        return sceneCount;
    }

    public int SceneLevelCount()
    {
        List<string> sceneLevels = SceneLevelNames();
        return sceneLevels.Count;
    }

    public List<string> ScenePaths()
    {
        // Get a list of scene paths in BuildSettings
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            //Debug.Log("Scene path: " + scene.path);
            path.Add(scene.path);
        }

        return path;
    }

    public List<string> SceneLevelNames()
    {
        // Get the name of a file from a folder
        string folderPath = "Assets/Scene/Level";
        DirectoryInfo directory = new DirectoryInfo(folderPath);
        FileInfo[] files = directory.GetFiles();
        foreach (FileInfo file in files)
        {
            if(file.Extension == ".unity")
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.Name);
                //Debug.Log(fileNameWithoutExtension); 
                listScene.Add(fileNameWithoutExtension);
            }
        }

        return listScene;
    }
    
    public static void LoadScene(string sceneName)
    {
        onLoaderCallback = () => SceneManager.LoadScene(sceneName);

        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
