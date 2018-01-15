using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shop
{
    public string name;

    public Vector2 location;
    public Vector2Int salePannelSize;

    public List<Item> itemList;

    public int tentShape;
    public int tentPattern;
    public Color tentColor;

    public Item FindItem(int _index)
    {
        return itemList[_index];
    }

    public List<Item> FindItemList(Item _item)
    {
        if (itemList == null || itemList.Count < 1)
            return null;

        List<Item> resultList = new List<Item>();

        for (int i = 0; i < itemList.Count; ++i)
        {
            if (itemList[i] != null && itemList[i].id == _item.id)
                resultList.Add(itemList[i]);
        }

        return resultList;
    }

    public int FindItemIndex(int _id, int _saleGold)
    {
        for (int i = 0; i < itemList.Count; ++i)
        {
            if (itemList[i] != null && itemList[i].id == _id && itemList[i].saleGold == _saleGold)
            {
                return i;
            }
        }
        return -1;
    }

}
