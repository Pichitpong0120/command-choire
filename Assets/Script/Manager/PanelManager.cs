using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PanelManager : Singleton<PanelManager>
{
    [SerializeField] private string startPanelName;
    [SerializeField] private List<PanelModel> panels;

    private List<PanelInstanceModel> listInstance = new List<PanelInstanceModel>();

    private void Start() 
    {
        if(startPanelName != "") ShowPanel(startPanelName, HidePanelMode.none);
    }

    public void ShowPanel(string panelName, HidePanelMode hideMode)
    {
        PanelModel panelModel = panels.FirstOrDefault(panel => panel.panelName == panelName);

        if(panelModel != null)
        {
            var instantPanel = Instantiate(panelModel.panelPrefab, transform);
            
            HidePanelBehaviour(hideMode);
            
            listInstance.Add(new PanelInstanceModel
            {
                panelName = panelName,
                panelInstance = instantPanel
            });
        }
        else
        {
            Debug.LogWarning($"Trying to use panel : {panelName}, But panel is not found");
        }
    }

    public void HidePanel(HidePanelMode hideMode)
    {
        HidePanelBehaviour(hideMode);
    }

    private void HidePanelBehaviour(HidePanelMode hideMode)
    {
        switch (hideMode)
        {
            case HidePanelMode.none : break;
            case HidePanelMode.hideBeforeLastPanel : HideBeforeLastPanel(); break;
            case HidePanelMode.hideLastPanel : HideLastPanel(); break;
            case HidePanelMode.hideAllPanel : HideAllPanel(); break;
        }
    }
    
    public void HideBeforeLastPanel()
    {
        if(AnyPanelShowing())
        {
            var lastPanel = listInstance[listInstance.Count - 2];

            listInstance.Remove(lastPanel);

            Destroy(lastPanel.panelInstance);
        }
    }

    public void HideLastPanel()
    {
        if(AnyPanelShowing())
        {
            var lastPanel = listInstance.Last();

            listInstance.Remove(lastPanel);

            Destroy(lastPanel.panelInstance);
        }
    }

    public void HideAllPanel()
    {
        if(AnyPanelShowing())
        {
            for(int i = 0; i < listInstance.Count-1 ; i++)
            {
                var lastPanel = listInstance[i];

                listInstance.Remove(lastPanel);

                Destroy(lastPanel.panelInstance);
            }
        }
    }

    public bool AnyPanelShowing()
    {
        return GetAmountPanelInstance() > 0;
    }

    public int GetAmountPanelInstance()
    {
        return listInstance.Count;
    }
}
