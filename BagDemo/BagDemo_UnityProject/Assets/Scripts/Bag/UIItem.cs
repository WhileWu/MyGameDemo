using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
                      ,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ItemData data;
    Transform canvas;

    private void Start()
    {
        canvas = GameObject.Find("Canvas").transform;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UIManager.Instance.OnItemEndDrag(data, eventData);
        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.ShowInfoPanel(data);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.HideInfoPanel();
    }
}
