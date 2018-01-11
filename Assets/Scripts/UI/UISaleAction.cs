using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISaleAction : MonoBehaviour
{

    UISaleSlot selectSaleSlot;
    UIInventorySlot selectInventorySlot;

    public GameObject[] actionButton;

    GraphicRaycaster gr;
    PointerEventData ped = new PointerEventData(null);

    bool isInSlot = false;

    void Awake()
    {
        gr = UIInGame.instance.GetComponent<GraphicRaycaster>();
    }

    public void ViewSaleActoin(UISaleSlot _slot)
    {
        selectSaleSlot = _slot;

        if (selectSaleSlot.item == null || selectSaleSlot.item.id == 0)
        {
            actionButton[0].SetActive(true);
            actionButton[1].SetActive(false);
            actionButton[2].SetActive(false);
        }
        else {
            actionButton[1].SetActive(true);
            actionButton[2].SetActive(true);
            actionButton[0].SetActive(false);
        }

    }

    public void OnInSlot()
    {
        isInSlot = true;
        UIInventory.isSaleAction = true;
        UIInGame.instance.ViewInventory(true, true);
    }

    public void InSlot()
    {
        //슬롯에 아이템 등록
        selectSaleSlot.AddSlot(new Item(selectInventorySlot.item));
        //인벤에 아이템 삭제
        selectInventorySlot.RemoveSlot();

        //리스트 업데이트
        UIInGame.instance.UpdateSalePannel();
        UIInGame.instance.UpdateInventory();

        isInSlot = false;
        UIInventory.isSaleAction = false;

        UIInGame.instance.itemInfo.OnExit();
        UIInGame.instance.ViewInventory(false, false);

        OnHide();
    }

    public void OnOutSlot()
    {
        OutSlot();
    }

    void OutSlot()
    {
        //인벤이 가득 찼는지 체크
        bool isFull = PlayDataManager.instance.CheckFullInventory(1);

        if (isFull)
            return;

        //인벤에 아이템 등록
        UIInGame.instance.inventory.AddSlot(new Item(selectSaleSlot.item));
        //슬롯에 아이템 삭제
        selectSaleSlot.RemoveSlot();

        //리스트 업데이트
        UIInGame.instance.UpdateSalePannel();
        UIInGame.instance.UpdateInventory();

        OnHide();
    }

    public void SelectInventory(UIInventorySlot _selectInventorySlot)
    {
        selectInventorySlot = _selectInventorySlot;
    }

    public void OnHide()
    {
        if (isInSlot)
        {
            isInSlot = false;
        }
        gameObject.SetActive(false);
    }

    public void OnResetGold()
    {
        UIInventory.isSaleAction = true;
        UIInGame.instance.itemInfo.SetButton(true, "Gold Change", ResetGold);
        UIInGame.instance.ViewItemInfo(selectSaleSlot.item, true);
    }

    void ResetGold()
    {
        //리스트 업데이트
        UIInGame.instance.UpdateSalePannel();
        UIInGame.instance.UpdateInventory();
        
        UIInventory.isSaleAction = false;

        UIInGame.instance.itemInfo.OnExit();
        UIInGame.instance.ViewInventory(false, false);

        OnHide();
    }

}
