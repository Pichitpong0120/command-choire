using UnityEngine;

public class LoadingScene : MonoBehaviour
{
    private bool startLoading = true;

    private void Update()
    {
        if (startLoading)
        {
            startLoading = false;
            SceneGameManager.LoaderCallback();
        }
    }
}
