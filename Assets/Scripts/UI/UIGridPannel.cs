using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGridPannel : MonoBehaviour
{
    protected RectTransform tr;
    protected GridLayoutGroup gridLayout;

    public Text titleText;

    public List<Item> itemList;
    public List<UIItemSlot> slotList;
    public GameObject slotPrefab;

    public Vector2Int gridSize;
    public RectOffset padding;
    public Vector2 spacing;
    public Vector2 slotSize;

    public bool isView = false, isExit = false;

    public GameObject exitButton;

    protected virtual void Awake()
    {
        tr = GetComponent<RectTransform>();
        gridLayout = GetComponent<GridLayoutGroup>();
    }

    public void SetTitle(string _title) {
        titleText.text = _title;
    }

    public virtual void View(bool _isView, bool _isExit)
    {
        isView = _isView;
        isExit = _isExit;

        gameObject.SetActive(isView);
        exitButton.SetActive(isExit);

        gridLayout.padding = padding;
        gridLayout.spacing = spacing;
        gridLayout.cellSize = slotSize;

        Vector2 size = new Vector2(gridSize.x * (slotSize.x + spacing.x) + padding.left + padding.right, gridSize.y * (slotSize.y + spacing.y) + padding.top + padding.bottom);
        tr.sizeDelta = size;
    }

    public virtual void AddSlot(Item _item)
    {
        itemList.Add(_item);
    }

    public virtual void AddSlot(int _index, Item _item)
    {
        itemList[_index] = _item;
    }

    public virtual void RemoveSlot(int _index)
    {
        itemList.RemoveAt(_index);
    }

    public virtual void RemoveSlot(int _index, Item _item = null)
    {
        itemList[_index] = _item;
    }


    public virtual void UpdateSlotList()
    {
        GameObject g;
        UIItemSlot slot;

        int len = gridSize.x * gridSize.y;

        for (int i = 0; i < len; ++i)
        {
            if (i > (slotList.Count - 1))
            {
                g = Instantiate(slotPrefab, tr);
                slot = g.GetComponent<UIItemSlot>();
                slotList.Add(slot);
            }
            else {
                g = slotList[i].gameObject;
                slot = g.GetComponent<UIItemSlot>();
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
    }

    public virtual void UpdateEmptySlot(bool _isView)
    {

        int len = gridSize.x * gridSize.y;

        int emptySize = itemList.Count - len;

        if (emptySize > 0)
        {
            for (int i = 0; i < emptySize; ++i)
            {
                slotList[len + i].gameObject.SetActive(_isView);
            }
        }
    }

    public virtual void SetAllSlot(bool _isView)
    {
        for (int i = 0; i < slotList.Count; ++i)
        {
            slotList[i].gameObject.SetActive(_isView);
        }
    }

    protected virtual void Hide()
    {
        View(false, isExit);
    }

    public virtual void OnExit()
    {
        Hide();
    }
}
