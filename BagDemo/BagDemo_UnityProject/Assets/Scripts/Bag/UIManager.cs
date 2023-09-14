using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Tooltip("�������ӵĸ�����")]
    public Transform grid;
    [Tooltip("������Ϣ��")]
    public Transform infoPanel;
    [Tooltip("���ߴ��ڣ�����ȷ����Χ")]
    public RectTransform bagPanel;

    public Image prefabItemIcon;

    PlayerCharacter player;
    //Unity����
    public static UIManager Instance { get; private set; }
    //����ʽд��
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCharacter>();
    }

    //�ѵ���������ʾ������
    public void SetItem(int index, ItemData itemData)
    {
        Transform slot = grid.GetChild(index);
        //ɾ��ԭ���ĵ���ͼ��
        if (slot.childCount > 0)
        {
            Destroy(slot.GetChild(0).gameObject);
        }

        if(itemData != null)
        {
            //���ݵ������ݣ�������Ӧͼ��
            Image image = Instantiate(prefabItemIcon, slot);
            //��UIitem��������������
            UIItem uitem = image.GetComponent<UIItem>();
            uitem.data = itemData;
            image.sprite = Resources.Load<Sprite>(itemData.Json.imagePath);
        }
    }

    public void ShowInfoPanel(ItemData item)
    {
        infoPanel.gameObject.SetActive(true);
        infoPanel.transform.GetChild(0).GetComponent<Text>().text = "���� " + item.Json.name;
        infoPanel.transform.GetChild(1).GetComponent<Text>().text = "���� " + item.Json.type.ToString();
        infoPanel.transform.GetChild(2).GetComponent<Text>().text = "Ʒ�� " + item.quality;
    }

    public void HideInfoPanel()
    {
        infoPanel.gameObject.SetActive(false);
    }

    public void OnItemEndDrag(ItemData item, PointerEventData evt)
    {
        if (!RectTransformUtility.RectangleContainsScreenPoint(bagPanel, evt.position))
        {
            //��������
            player.removeItem(item);
            return;
        }
        bool swap = false;
        for (int i = 0;i < grid.childCount;++i)
        {
            RectTransform slot = grid.GetChild(i).GetComponent<RectTransform>();
            if(RectTransformUtility.RectangleContainsScreenPoint(slot, evt.position))
            {
                player.SwapItem(item, i);
                swap = true;
                break;
            }
        }
        if (!swap)
        {
            //��ԭ
            player.RestoreItem(item);
        }
    }
}
