using UnityEngine;
using UnityEngine.UI;

public class PauseGamePanels : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject ConfirmPanel;

    [SerializeField] private Button ContinueButton;
    [SerializeField] private Button BackButton;
    [SerializeField] private Button UnConfirmButton;

    void Start()
    {
        PausePanel.SetActive(true);
        ConfirmPanel.SetActive(false);
        
        ContinueButton.onClick.AddListener(() =>
        {
            Destroy(gameObject);
        });
        BackButton.onClick.AddListener(() =>
        {
            PausePanel.SetActive(false);
            ConfirmPanel.SetActive(true);
        });
        UnConfirmButton.onClick.AddListener(() =>
        {
            PausePanel.SetActive(true);
            ConfirmPanel.SetActive(false);
        });
    }
}
