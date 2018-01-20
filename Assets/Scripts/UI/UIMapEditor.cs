using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapEditor : MonoBehaviour {

    public static UIMapEditor instance;

    public UIMapEditorInventory inventory;
    
    void Awake() {
        instance = this;
    }

    void Start() {
        OnEdit();
    }

    public void OnEdit() {
        MapEditor.instance.StartMouseControl();
    }

    public void ViewInventory(bool _isView)
    {
        inventory.View(_isView);
    }

}
