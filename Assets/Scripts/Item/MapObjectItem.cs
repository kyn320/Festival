using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapObjectItem : Item
{

    public GameObject mapObjectPrefab;

    public MapObjectItem()
    {

    }

    public MapObjectItem(MapObjectItem _item)
    {
        this.name = _item.name;
        this.id = _item.id;
        this.icon = _item.icon;
        this.context = _item.context;
        this.gold = _item.gold;
        this.saleGold = _item.saleGold;
        this.mapObjectPrefab = _item.mapObjectPrefab;
    }

}
