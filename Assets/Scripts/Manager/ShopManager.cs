using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public List<ShopBehaviour> shopList;

    void Awake()
    {
        instance = this;
    }

    public ShopBehaviour GetRandomShop()
    {
        return shopList[Random.Range(0, shopList.Count)];
    }

}
