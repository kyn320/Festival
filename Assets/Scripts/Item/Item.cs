using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public int id;
    public Sprite icon;

    [TextArea]
    public string context;

    public int gold;
    public int saleGold;

    public Item()
    {

    }

    public Item(Item _item)
    {
        this.name = _item.name;
        this.id = _item.id;
        this.icon = _item.icon;
        this.context = _item.context;
        this.gold = _item.gold;
        this.saleGold = _item.saleGold;
    }

}
