using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public enum BuildMode
{
    None,
    Build,
    Remove
}

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance; //싱글톤 생성

    public GameObject SelectedTowerPrefab; //선택된 타워 프리팹
    public BuildMode CurrentMode = BuildMode.None;

    public List<TowerSelectButton> towerSelectButtons = new List<TowerSelectButton>();
    public RemoveBtn RemoveBtn;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

       
    }

    public void SelectTower(GameObject towerPrefab) // 타워 선택
    {
        Tower cost = towerPrefab.GetComponent<Tower>();
        if (cost != null && GoldManager.instance.gold >= cost.buildCost)
        {
            SelectedTowerPrefab = towerPrefab;
            CurrentMode = BuildMode.Build;
        }else
        {
            ClearSelection();
        }

        RefreshAllBuildButtons();
    }

    public void SelectRemove()
    {
        SelectedTowerPrefab = null;
        CurrentMode = BuildMode.Remove;
        RefreshAllBuildButtons();
    }

    public void ClearSelection() // 선택 해제
    {
        SelectedTowerPrefab = null;
        CurrentMode = BuildMode.None;
    }

    public void RefreshAllBuildButtons()
    {
        foreach (var btn in towerSelectButtons)
        {
            btn.UpdateVisual();
        }

        if (RemoveBtn != null)
        {
            RemoveBtn.UpdateVisual();
        }
    }
}
