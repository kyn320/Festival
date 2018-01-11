using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerMenu : UIRadialMenu
{

    public override void View(bool _isView)
    {
        base.View(_isView);
    }
    
    public void OnStatus() {

    }

    public void OnInventory()
    {
        UIInGame.instance.ViewInventory(true, true);
    }

    public void OnCalendar()
    {

    }

    public void OnGameEnd()
    {

    }
    
    protected override void HideButton()
    {
        base.HideButton();
        SetScale(Vector3.one);
    }

}
