using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour
{

    public int index;
    public Item item;
    public Image iconImage;

    public virtual void SetSlot(int _index, Item _item)
    {
        index = _index;

        item = _item;

        if (item == null || item.id == 0)
        {
            iconImage.gameObject.SetActive(false);
        }
        else
        {
            iconImage.sprite = item.icon;
            iconImage.gameObject.SetActive(true);
        }
    }

    public virtual bool CheckEmpty()
    {
        if (item == null || item.id == 0)
        {
            return true;
        }
        else
            return false;
    }

    public virtual void OnSelectSlot()
    {

    }

}
