using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UISalePannel : UIGridPannel
{
    public static bool isSaleAction = false;

    public UISaleAction saleAction;

    ShopBehaviour shop;

    public bool isEdit = false;
    
    protected override void Awake()
    {
        UISaleSlot.salePannel = this;
        base.Awake();
    }

    public void SetShop(ShopBehaviour _shop)
    {
        shop = _shop;
        
        SetAllSlot(false);

        itemList = shop.info.itemList;

        gridSize = shop.info.salePannelSize;
    }

    public void View(ShopBehaviour _shop, bool _isView, bool _isExit, bool _isEdit)
    {
        isEdit = _isEdit;

        SetShop(_shop);
        base.View(_isView, _isExit);
        isSaleAction = true;
        UpdateSlotList();
    }

    public override void AddSlot(int _index, Item _item)
    {
        base.AddSlot(_index, _item);
        shop.AddSlot(_index, _item);
    }

    public override void RemoveSlot(int _index)
    {
        base.RemoveSlot(_index, null);
        shop.RemoveSlot(_index);
    }

    public override void UpdateSlotList()
    {
        base.UpdateSlotList();
        base.UpdateEmptySlot(false);

        saleAction.GetComponent<RectTransform>().SetAsLastSibling();

        shop.CreateBatchItem();
    }
    
    public void ViewSaleAction(UISaleSlot _slot)
    {
        saleAction.transform.position = _slot.transform.position + Vector3.right * ((slotSize.x * 0.5f) + spacing.x);
        saleAction.gameObject.SetActive(true);
        saleAction.ViewSaleActoin(_slot);
    }

    protected override void Hide()
    {
        isSaleAction = false;
        shop.SetShopMode(false);
        base.Hide();
    }


}
