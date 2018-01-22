using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : UIGridPannel
{
    public static bool isSaleAction = false;

    public Text goldText;

    protected override void Awake()
    {
        UIInventorySlot.inventory = this;
        base.Awake();
    }

    public override void View(bool _isView, bool _isExit)
    {
        base.View(_isView, _isExit);

        if (!isView)
        {
            UIInGame.instance.playerMenu.ViewAnimate();
            return;
        }

        itemList = PlayDataManager.instance.inventory;
        gridSize = PlayDataManager.instance.inventorySize;
        goldText.text = PlayDataManager.instance.gold + " G";

        UpdateSlotList();
    }

    public override void AddSlot(Item _item)
    {
        base.AddSlot(_item);
    }

    public override void RemoveSlot(int _index)
    {
        base.RemoveSlot(_index);
    }

    public override void UpdateSlotList()
    {
        base.UpdateSlotList();
        base.UpdateEmptySlot(false);
        goldText.text = PlayDataManager.instance.gold + " G";
    }

}
