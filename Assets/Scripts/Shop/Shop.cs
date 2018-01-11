using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shop {
    public string name;

    public Vector2 location;
    public Vector2Int salePannelSize;
    
    public List<Item> itemList;

    public int tentShape;
    public int tentPattern;
    public Color tentColor;
}
