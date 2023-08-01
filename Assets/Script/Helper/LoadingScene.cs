using UnityEngine;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    private bool startLoading = true;
    [field: SerializeField] private Scrollbar imageProcess;

    private void Update()
    {
        if (startLoading)
        {
            startLoading = false;
            SceneGameManager.LoaderCallback();
        }

        imageProcess.size = SceneGameManager.GetLoadingProgress();
    }
}
