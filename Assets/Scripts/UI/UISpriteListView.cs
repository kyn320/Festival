using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISpriteListView : MonoBehaviour
{
    public static UISpriteMaker ui;

    public string folderPath = "Sign";

    public RectTransform pannel;

    public Text pathName;

    public Sprite[] spriteList;

    public GameObject slotPrefab;
    public List<UISpriteSlot> slotList;

    public GameObject exitButton;

    void Start()
    {
        UISpriteSlot.listView = this;

        SearchSprite();
    }

    public void ViewList(bool _isView, bool _isExit)
    {
        gameObject.SetActive(_isView);
        exitButton.SetActive(_isExit);

        SearchSprite();
    }

    public void ViewList(bool _isView, bool _isExit, UnityAction<Sprite> _action)
    {
        gameObject.SetActive(_isView);
        exitButton.SetActive(_isExit);

        SearchSprite(_action);
    }

    public void SearchSprite()
    {
        spriteList = Resources.LoadAll<Sprite>(folderPath);

        UpdateSlotList();
        UpdateEmptySlot(false);
    }

    public void SearchSprite(UnityAction<Sprite> _action)
    {
        spriteList = Resources.LoadAll<Sprite>(folderPath);

        UpdateSlotList(_action);
        UpdateEmptySlot(false);
    }

    public void UpdateSlotList()
    {
        GameObject g;
        UISpriteSlot slot;

        int len = spriteList.Length;

        for (int i = 0; i < len; ++i)
        {
            if (i > (slotList.Count - 1))
            {
                g = Instantiate(slotPrefab, pannel);
                slot = g.GetComponent<UISpriteSlot>();
                slotList.Add(slot);
            }
            else {
                g = slotList[i].gameObject;
                slot = g.GetComponent<UISpriteSlot>();
                g.SetActive(true);
            }

            if (i > spriteList.Length - 1 || spriteList[i] == null)
            {
                slot.SetSlot(i, null);
            }
            else
            {
                slot.SetSlot(i, spriteList[i]);
            }
        }
    }

    public void UpdateSlotList(UnityAction<Sprite> _action)
    {
        GameObject g;
        UISpriteSlot slot;

        int len = spriteList.Length;

        for (int i = 0; i < len; ++i)
        {
            if (i > (slotList.Count - 1))
            {
                g = Instantiate(slotPrefab, pannel);
                slot = g.GetComponent<UISpriteSlot>();
                slotList.Add(slot);
            }
            else {
                g = slotList[i].gameObject;
                slot = g.GetComponent<UISpriteSlot>();
                g.SetActive(true);
            }

            if (i > spriteList.Length - 1 || spriteList[i] == null)
            {
                slot.SetSlot(i, null);
            }
            else
            {
                slot.SetSlot(i, spriteList[i], _action);
            }
        }
    }

    public void UpdateEmptySlot(bool _isView)
    {
        int len = spriteList.Length;

        int emptySize = slotList.Count - len;

        if (emptySize > 0)
        {
            for (int i = 0; i < emptySize; ++i)
            {
                slotList[len + i].gameObject.SetActive(_isView);
            }
        }
    }



}
