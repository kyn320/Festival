using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerMenu : UIRadialMenu
{

    public override void View(bool _isView)
    {
        base.View(_isView);
    }

    public void OnStatus()
    {
        HideAnimate(null);
    }

    public void OnInventory()
    {
        HideAnimate(null);
        UIInGame.instance.ViewInventory(true, true);
    }

    public void OnMapEdit()
    {
        HideAnimate(null);
        MapEditor.instance.ui.OnEdit();
    }

    public void OnCalendar()
    {
        HideAnimate(null);
    }

    public void OnGameEnd()
    {
        HideAnimate(null);
    }

    protected override void HideButton()
    {
        base.HideButton();
        SetScale(Vector3.one);
    }

}
