﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopMenu : UIRadialMenu
{

    public ShopBehaviour shop;

    public void View(ShopBehaviour _shop, bool _isView)
    {
        View(_isView);
        shop = _shop;
    }

    public override void View(bool _isView)
    {
        base.View(_isView);
    }

    public void OnDesign()
    {
        UIInGame.instance.ViewShopDesign(shop);
    }

    public void OnSalePannel()
    {
        UIInGame.instance.ViewSalePannel(shop, true, true, true);
    }
        
}
