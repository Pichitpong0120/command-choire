using UnityEngine;
using UnityEngine.UI;

public class BtnSelectLevelData : MonoBehaviour
{
    public int indexLevel;
    public string nameLevel;
    public bool unLock;

    private Button btnSelectLevel;
    private Text textSelectLevel;

    private void Start()
    {
        btnSelectLevel = this.GetComponent<Button>();
        btnSelectLevel.onClick.AddListener(() => loadScene(nameLevel));

        textSelectLevel = this.GetComponentInChildren<Text>();
        textSelectLevel.text = indexLevel.ToString();

        Image imageSelectLevel = this.GetComponent<Image>();
        Color newColor = imageSelectLevel.color;
        if (!unLock)
        {
            newColor.a = 0.67f;
            imageSelectLevel.color = newColor;
        }
        else
        {
            newColor.a = 1f;
            imageSelectLevel.color = newColor;
        }
    }

    private void loadScene(string scene)
    {
        if (unLock) SceneGameManager.LoadScene(scene);
    }
}
