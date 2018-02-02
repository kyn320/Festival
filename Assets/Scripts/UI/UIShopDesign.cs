using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopDesign : MonoBehaviour
{

    public int selectShape;
    public Sprite[] shapeList;
    public int selectPattern;
    public Sprite[] patternList;

    public UIColorPicker colorPicker;

    public UISpriteListView signListView;

    [SerializeField]
    ShopBehaviour shop;

    UITargetTracker targetTracker;

    void Awake()
    {
        targetTracker = GetComponent<UITargetTracker>();
    }

    public void View(ShopBehaviour _shop)
    {
        UIInGame.instance.shopMenu.HideAnimate(null);
        gameObject.SetActive(true);
        shop = _shop;
        targetTracker.SetTarget(_shop.transform, Camera.main.WorldToScreenPoint(_shop.transform.position), Vector3.zero);
        colorPicker.Color = _shop.info.tentColor;
        colorPicker.SetOnValueChangeCallback(OnChangeColor);
        signListView.ViewList(true, false, UpdateSign);
    }

    public void OnChangeColor(Color _color)
    {
        shop.ChangeDesign(selectShape, shapeList[selectShape], selectPattern, patternList[selectPattern], _color);
    }

    public void UpdateSign(Sprite _sprite) {
        shop.signRenderer.sprite = _sprite;
    }

    public void UpdateShape(int _next)
    {

        if (selectShape + _next > shapeList.Length - 1)
            selectShape = 0;
        else if (selectShape + _next < 0)
            selectShape = shapeList.Length - 1;
        else
            selectShape += _next;

        shop.ChangeDesign(selectShape, shapeList[selectShape], selectPattern, patternList[selectPattern], colorPicker.Color);
    }

    public void UpdatePattern(int _next)
    {
        if (selectPattern + _next > patternList.Length - 1)
            selectPattern = 0;
        else if (selectPattern + _next < 0)
            selectPattern = patternList.Length - 1;
        else
            selectPattern += _next;

        shop.ChangeDesign(selectShape, shapeList[selectShape], selectPattern, patternList[selectPattern], colorPicker.Color);
    }

    public void ViewExit()
    {
        signListView.ViewList(false, false);
        UIInGame.instance.shopMenu.ViewAnimate();
        gameObject.SetActive(false);
    }

}
