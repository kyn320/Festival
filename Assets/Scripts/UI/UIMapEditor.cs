using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapEditor : MonoBehaviour {
    
    public GameObject title;
    public UIMapEditorInventory inventory;

    public void OnEdit()
    {
        gameObject.SetActive(true);
        MapEditor.instance.SetMouseControl(true);
    }

    public void OnExit()
    {
        gameObject.SetActive(false);
        MapEditor.instance.SetMouseControl(false);
        UIInGame.instance.ViewPlayerMenu(true);
    }

    public void ViewInventory(bool _isView)
    {
        inventory.View(_isView);
    }

}
