using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMapItemSlot : MonoBehaviour
{
    public static UIMapEditorInventory inventory;

    public int index;
    public MapObjectItem item;
    public Image iconImage;

    public virtual void SetSlot(int _index, MapObjectItem _item)
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

    public void OnSelectSlot()
    {
        UIInGame.instance.itemInfo.SetButton(true, "Batch", CreateObject);
        UIInGame.instance.ViewItemInfo(item, true);
    }

    public void AddSlot(MapObjectItem _item)
    {
        inventory.AddSlot(item);
    }

    public void RemoveSlot()
    {
        inventory.RemoveSlot(index);
    }

    public void CreateObject()
    {
        UIInGame.instance.itemInfo.OnExit();
        UIMapEditor.instance.ViewInventory(false);

        MapEditor.instance.StartEditMode();
        GameObject g = Instantiate(item.mapObjectPrefab, (Vector2)Camera.main.transform.position, Quaternion.identity);
        MapEditor.instance.SetTarget(g.transform);
    }

    public UIMapEditorInventory GetInventory()
    {
        return inventory;
    }
}
