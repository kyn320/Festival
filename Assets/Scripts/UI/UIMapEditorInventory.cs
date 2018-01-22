using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMapEditorInventory : MonoBehaviour
{
    protected RectTransform tr;

    public List<MapObjectItem> itemList;
    public List<UIMapItemSlot> slotList;
    public GameObject slotPrefab;

    public RectOffset padding;
    public Vector2 spacing;

    public Transform contentPannel;

    public bool isView = false;

    Animator ani;
    public GameObject exitButton;

    void Awake()
    {
        tr = GetComponent<RectTransform>();
        ani = GetComponent<Animator>();
        UIMapItemSlot.inventory = this;
    }

    public void View(bool _isView)
    {
        isView = _isView;
        ani.SetTrigger("View");

        if (!isView)
            return;

        itemList = PlayDataManager.instance.mapObjectInventory;

        UpdateSlotList();
    }

    public virtual void AddSlot(MapObjectItem _item)
    {
        itemList.Add(_item);
    }


    public virtual void RemoveSlot(int _index)
    {
        itemList.RemoveAt(_index);
    }

    public void UpdateSlotList()
    {
        GameObject g;
        UIMapItemSlot slot;

        int len = itemList.Count;

        for (int i = 0; i < len; ++i)
        {
            if (i > (slotList.Count - 1))
            {
                g = Instantiate(slotPrefab, contentPannel);
                slot = g.GetComponent<UIMapItemSlot>();
                slotList.Add(slot);
            }
            else {
                g = slotList[i].gameObject;
                slot = g.GetComponent<UIMapItemSlot>();
                g.SetActive(true);
            }

            if (i > itemList.Count - 1 || itemList[i] == null || itemList[i].id == 0)
            {
                slot.SetSlot(i, null);
            }
            else
            {
                slot.SetSlot(i, itemList[i]);
            }
        }
        UpdateEmptySlot(false);
    }

    public void UpdateEmptySlot(bool _isView)
    {

        int len = itemList.Count;

        int emptySize = slotList.Count - len;

        if (emptySize > 0)
        {
            for (int i = 0; i < emptySize; ++i)
            {
                slotList[len + i].gameObject.SetActive(_isView);
            }
        }
    }

    protected void Hide()
    {
        View(false);
    }

    public void OnExit()
    {
        Hide();
    }

}
