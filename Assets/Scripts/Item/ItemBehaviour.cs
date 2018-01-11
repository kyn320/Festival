using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{

    public Item item;
    SpriteRenderer sprRender;

    void Awake() {
        sprRender = GetComponent<SpriteRenderer>();
    }

    public void SetItem(Item _item)
    {
        sprRender.sprite = _item.icon;
    }

}
