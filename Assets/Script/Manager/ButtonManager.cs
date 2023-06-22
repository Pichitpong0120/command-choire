using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [field: SerializeField] public string panelName { get; private set; }
    [field: SerializeField] public HidePanelMode hideMode { get; private set; }
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
}

public enum HidePanelMode
{
    none,
    hideBeforeLastPanel,
    hideLastPanel,
    hideAllPanel
}
