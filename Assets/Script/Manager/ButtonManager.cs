using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [field: SerializeField] public string panelName { get; private set; }
    [field: SerializeField] public HidePanelMode hideMode { get; private set; }
    [field: SerializeField] public SceneGameManager.Scene scene { get; private set; }
    private PanelManager panelManager;

    private void Start()
    {
        panelManager = PanelManager.instance;
    }
    
    public void ToShowPanel()
    {
        panelManager.ShowPanel(panelName, hideMode);
    }

    public void ToHidePanel()
    {
        panelManager.HidePanel(hideMode);
    }

    public void LoadScene()
    {
        SceneGameManager.LoadScene(scene.ToString());
    }

    public void QuitScene()
    {
        //Debug.Log("Game Quit");
        Application.Quit();
    }
}

public enum HidePanelMode
{
    none,
    hideBeforeLastPanel,
    hideLastPanel,
    hideAllPanel
}
