using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInGame : MonoBehaviour
{

    public static UIInGame instance;

    public UINotice notice;

    public UIDayHeader dayHeader;
    public UISalePannel salePannel;
    public UISayManager sayManager;
    public UIWantManager wantManager;
    public UIInventory inventory;
    public UIItemInfo itemInfo;
    public UIPlayerMenu playerMenu;
    public UIShopMenu shopMenu;
    public UIShopDesign shopDesign;

    public UIGoldEditer goldEditer;

    public RectTransform backgroundFade;


    void Awake()
    {
        instance = this;
    }

    public void SetNotice(string _notice, bool _isView)
    {
        notice.View(_notice, _isView);
    }

    public void UpdateTime(float _dayTime)
    {
        dayHeader.UpdateTime(_dayTime);
    }

    public void ViewSalePannel(ShopBehaviour _shop, bool _isView, bool _isExit, bool _isEdit)
    {
        salePannel.View(_shop, _isView, _isExit, _isEdit);
    }

    public void ViewSay(TalkBehaviour _talker, Talk _talk)
    {
        sayManager.View(_talker, _talk);
    }

    public void ViewWant(TalkBehaviour _talker, Item _item)
    {
        wantManager.View(_talker, _item);
    }

    public void ViewPlayerMenu(bool _isView)
    {
        playerMenu.View(_isView);
    }

    public void ViewInventory(bool _isView, bool _isExit)
    {
        inventory.View(_isView, _isExit);
    }

    public void ViewItemInfo(Item _item, bool _isView)
    {
        if (_item == null || _item.id == 0)
            return;

        itemInfo.View(_item, _isView);
    }

    public void ViewShopMenu(ShopBehaviour _shop, bool _isView)
    {
        shopMenu.View(_shop, _isView);
    }

    public void ViewShopDesign(ShopBehaviour _shop)
    {
        shopDesign.View(_shop);
    }

    public void SetBackgroundFade(RectTransform _parent, int _addIndex)
    {
        if (_parent == null)
        {
            backgroundFade.SetSiblingIndex(1);
            backgroundFade.gameObject.SetActive(false);
            return;
        }

        backgroundFade.SetSiblingIndex(_parent.GetSiblingIndex() - 1 + _addIndex);
        backgroundFade.gameObject.SetActive(true);
    }

    public void UpdateSalePannel()
    {
        salePannel.UpdateSlotList();
    }

    public void UpdateInventory()
    {
        inventory.UpdateSlotList();
    }

    public void ViewGoldEditor(Item _item)
    {
        goldEditer.View(_item);
    }

}
