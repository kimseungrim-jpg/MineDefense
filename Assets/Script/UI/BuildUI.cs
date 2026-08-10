using UnityEngine;

public class BuildUI : MonoBehaviour
{
    public GameObject mainOpenButton;
    public GameObject towerButtonPanel;

    void Start()
    {
            
    }

    public void OpenBuildMenu()
    {
        mainOpenButton.SetActive(false);
        towerButtonPanel.SetActive(true);
    }

    public void CloseBuildMenu()
    {
        mainOpenButton.SetActive(true);
        towerButtonPanel.SetActive(false);

        if (BuildManager.instance != null)
        {
            BuildManager.instance.ClearSelection();
        }
    }
}
