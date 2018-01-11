using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIItemInfo : MonoBehaviour
{
    public Item item;

    public Text titleText;
    public Image iconImage;
    public Text contextText;
    public Text goldText;

    public Button goldEdit;
    public Button actionButton;

    public void View(Item _item, bool _isView)
    {
        if (!_isView)
            Hide();

        item = _item;

        goldEdit.gameObject.SetActive(UIInventory.isSaleAction);

        UpdateItem();
        UIInGame.instance.SetBackgroundFade(GetComponent<RectTransform>(), 0);
        gameObject.SetActive(true);
    }

    void UpdateItem()
    {
        if (item.saleGold < 1)
            item.saleGold = item.gold;

        titleText.text = item.name;
        iconImage.sprite = item.icon;
        contextText.text = item.context;

        if (UISalePannel.isSaleAction)
            goldText.text = "Gold " + item.saleGold;
        else
            goldText.text = "Gold " + item.gold;

        RectTransform rect = contextText.GetComponent<RectTransform>();
        rect.sizeDelta = GetResize(rect);
    }

    Vector2 GetResize(RectTransform _rect)
    {
        return new Vector2(LayoutUtility.GetPreferredWidth(_rect), LayoutUtility.GetPreferredHeight(_rect));
    }

    public void OnExit()
    {
        if (UIInventory.isSaleAction)
        {
            UIInGame.instance.ViewInventory(false, false);
            UIInventory.isSaleAction = false;
        }
        Hide();
    }

    void Hide()
    {
        UIInGame.instance.SetBackgroundFade(null, 0);
        SetButton(false, "", null);
        gameObject.SetActive(false);
    }

    public void SetButton(bool _isView, string _name, UnityAction _action)
    {
        actionButton.gameObject.SetActive(_isView);

        if (!_isView)
            return;

        actionButton.transform.GetChild(0).GetComponent<Text>().text = _name;
        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(_action);
    }

    public void OnGoldEdit()
    {
        UIInGame.instance.ViewGoldEditor(item);
        UIInGame.instance.goldEditer.SetEditAction(UpdateItem);
    }


}
