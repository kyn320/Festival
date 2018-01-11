using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventorySlot : UIItemSlot
{
    public static UIInventory inventory;

    public override void SetSlot(int _index, Item _item)
    {
        base.SetSlot(_index, _item);
    }

    public override void OnSelectSlot()
    {
        if (UIInventory.isSaleAction && !CheckEmpty())
        {
            UIInGame.instance.salePannel.saleAction.SelectInventory(this);
            UIInGame.instance.itemInfo.SetButton(true, "Batch", UIInGame.instance.salePannel.saleAction.InSlot);
        }

        UIInGame.instance.ViewItemInfo(item, true);
    }

    public void AddSlot(Item _item)
    {
        inventory.AddSlot(item);
    }

    public void RemoveSlot()
    {
        inventory.RemoveSlot(index);
    }

    public UIInventory GetInventory()
    {
        return inventory;
    }

}
