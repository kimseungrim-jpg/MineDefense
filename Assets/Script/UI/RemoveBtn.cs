using UnityEngine;
using UnityEngine.UI;

public class RemoveBtn : MonoBehaviour
{
    public Image buttonImage;

    public Color normalColor = Color.white;
    public Color selectColor = Color.gray;

    private void Awake()
    {
        BuildManager.instance.RemoveBtn = this;
    }

    private void Start()
    {
        UpdateVisual();
    }

    public void OnClickRemove()
    {
        BuildManager.instance.SelectRemove();

        if (TowerSelectManager.instance != null)
        {
            TowerSelectManager.instance.Clear();
        }

        //AllUpdateVisual();
    }

    public void UpdateVisual()
    {
        if (BuildManager.instance.CurrentMode == BuildMode.Remove)
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
