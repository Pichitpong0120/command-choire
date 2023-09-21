using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class SceneGameManager
{
    private class LoadingMonoBehaviour : MonoBehaviour { }

    private static Action onLoaderCallback;
    private static AsyncOperation loadingAsyncOperation;

    public enum Scene
    {
        MainMenu,
        Loading,
        SelectLevel,
        Level_1,
    }

    public static int SceneCount()
    {
        // Get the scene count in BuildSettings
        int sceneCount = EditorBuildSettings.scenes.Length;

        //Debug.Log("Scene count: " + sceneCount);

        return sceneCount;
    }

    public static List<string> ScenePaths()
    {
        List<string> path = new List<string>();
        // Get a list of scene paths in BuildSettings
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            //Debug.Log("Scene path: " + scene.path);
            path.Add(scene.path);
        }

        return path;
    }

    public static int SceneLevelCount()
    {
        List<string> sceneLevels = SceneLevelNames();
        return sceneLevels.Count;
    }

    public static List<string> SceneLevelNames()
    {
        List<string> listScenes = new List<string>();
        // Get the name of a file from a folder
        string folderPath = "Assets/Scene/Level";
        DirectoryInfo directory = new DirectoryInfo(folderPath);
        FileInfo[] files = directory.GetFiles();
        foreach (FileInfo file in files)
        {
            if (file.Extension == ".unity")
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.Name);
                //Debug.Log(fileNameWithoutExtension); 
                listScenes.Add(fileNameWithoutExtension);
            }
        }

        listScenes = listScenes.OrderBy(listScene => int.Parse(listScene.Substring(6))).ToList();

        return listScenes;
    }

    public static void LoadScene(string sceneName)
    {
        onLoaderCallback = () =>
        {
            GameObject loadingGameObject = new GameObject("Loading...");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(sceneName));
        };

        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    private static IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return null;
        loadingAsyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!loadingAsyncOperation.isDone)
        {
            yield return null;
        }
    }

    public static float GetLoadingProgress()
    {
        if (loadingAsyncOperation != null)
        {
            return loadingAsyncOperation.progress;
        }
        else
        {
            return 0f;
        }
    }

    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }

    public static void SpawnGameObject(GameObject gameObjectPrefab, Transform transform)
    {
        UnityEngine.Object.Instantiate(gameObjectPrefab, transform);
    }
}
