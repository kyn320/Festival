using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WantItem
{
    public Item item;
    
    public ShopBehaviour lowShop;
    public int lowGold = 99999999;
    
    public ShopBehaviour highShop;
    public int highGold = -1;

    public void UpdateSaleGold(ShopBehaviour _enterShop, List<Item> _itemList)
    {

        _itemList.Sort(delegate (Item a, Item b)
        {
            if (a.saleGold > b.saleGold) return 1;
            else if (a.saleGold < b.saleGold) return -1;
            return 0;
        });
        for (int i = 0; i < _itemList.Count; ++i)
        {
            CheckSaleGold(_enterShop, _itemList[i].saleGold);
        }

    }

    public void CheckSaleGold(ShopBehaviour _shop, int _gold)
    {
        if (_gold < lowGold)
        {
            lowShop = _shop;
            lowGold = _gold;
        }

        if (_gold > highGold)
        {
            highShop = _shop;
            highGold = _gold;
        }
    }


}
