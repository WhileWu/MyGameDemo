using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelInfo :MonoBehaviour
{
    public ItemData itemData;


    public void OnMouseEnter()
    {
        UIManager.Instance.ShowInfoPanel(itemData);
    }

    public  void OnMouseExit()
    {
        UIManager.Instance.HideInfoPanel();
    }
}
