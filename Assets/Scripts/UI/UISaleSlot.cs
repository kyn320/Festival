using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class UISaleSlot : UIItemSlot
{
    public static UISalePannel salePannel;

    public GameObject goldText;

    public Button actionButton;

    public override void SetSlot(int _index, Item _item)
    {
        base.SetSlot(_index, _item);

        SetGold();

        if (salePannel.isEdit)
        {
            SetButton(true, "Edit", Action);
        }
        else if (!CheckEmpty())
        {
            SetButton(true, "Buy", Action);
        }
        else {
            SetButton(false, "", null);
        }

    }

    public override void OnSelectSlot()
    {
        base.OnSelectSlot();
        UIInGame.instance.ViewItemInfo(item, true);
    }

    public void Action()
    {
        if (salePannel.isEdit)
        {
            salePannel.ViewSaleAction(this);
        }
        else {
            Buy();
        }
    }

    public void AddSlot(Item _item)
    {
        salePannel.AddSlot(index, _item);
    }

    public void RemoveSlot()
    {
        salePannel.RemoveSlot(index);
    }

    public void SetButton(bool _isView, string _name, UnityAction _action)
    {
        actionButton.gameObject.SetActive(_isView);

        if (!_isView)
            return;

        actionButton.transform.GetChild(0).GetComponent<Text>().text = _name;
        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(_action);
    }

    public void SetGold()
    {
        bool isEmpty = CheckEmpty();

        goldText.SetActive(!isEmpty);

        if (!isEmpty)
            goldText.transform.GetChild(0).GetComponent<Text>().text = item.saleGold + "G";

    }

    void Buy()
    {
        //비어 있는지 체크
        if (CheckEmpty())
            return;

        //골드 체크
        if (!PlayDataManager.instance.CheckGold(-item.saleGold))
            return;

        //인벤이 가득 찼는지 체크
        bool isFull = PlayDataManager.instance.CheckFullInventory(1);

        if (isFull)
            return;

        //인벤에 아이템 등록
        UIInGame.instance.inventory.AddSlot(new Item(item));
        
        PlayDataManager.instance.AddGold(-item.saleGold);

        //슬롯에 아이템 삭제
        RemoveSlot();


        //리스트 업데이트
        UIInGame.instance.UpdateSalePannel();
        UIInGame.instance.UpdateInventory();
    }

}
