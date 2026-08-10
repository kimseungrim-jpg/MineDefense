using UnityEngine;
using UnityEngine.UI;

public class TowerSelectButton : MonoBehaviour
{
    public GameObject towerPrefab;

    public Image buttonImage;

    public Color normalColor = Color.white;
    public Color selectColor = Color.gray;

    private void Start()
    {
        UpdateVisual();
    }

    public void SelectTower()
    {
        BuildManager.instance.SelectTower(towerPrefab);
        
        //AllUpdateVisual();
    }

    public void UpdateVisual()
    {
        if (BuildManager.instance.SelectedTowerPrefab == towerPrefab && BuildManager.instance.CurrentMode == BuildMode.Build)
        {
            buttonImage.color = selectColor;
        }
        else
        {
            buttonImage.color = normalColor;
        }
    }

    //public void AllUpdateVisual()
    //{
    //    TowerSelectButton[] buttons = FindObjectsOfType<TowerSelectButton>();
    //    foreach (var btn in buttons)
    //    {
    //        btn.UpdateVisual();
    //    }
    //}
}
