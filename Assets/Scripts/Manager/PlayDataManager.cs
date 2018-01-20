using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDataManager : MonoBehaviour
{
    public PlayerController player;

    public static PlayDataManager instance;

    public List<Item> inventory;
    public Vector2Int inventorySize;

    public int gold;

    public List<MapObjectItem> mapObjectInventory;


    void Awake()
    {
        instance = this;
    }

    public bool CheckFullInventory(int _count)
    {
        if (inventory.Count + _count > inventorySize.x * inventorySize.y)
        {
            return true;
        }
        return false;
    }

    public void AddInventoryItem(Item _item)
    {
        inventory.Add(_item);
    }

    public void RemoveInventoryItem(int _index)
    {
        inventory.RemoveAt(_index);
    }

    public bool CheckGold(int _value) {
        if (gold + _value < 0)
            return false;
        else
            return true;
    }

    public void AddGold(int _value) {
        gold += _value;
    }

}
