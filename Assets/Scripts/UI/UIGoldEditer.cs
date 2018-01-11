using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIGoldEditer : MonoBehaviour
{
    public Item item;

    public InputField input;

    UnityAction editAction;

    public void View(Item _item)
    {
        item = _item;
        input.text = item.saleGold.ToString();
        gameObject.SetActive(true);
    }

    public void SetEditAction(UnityAction _action)
    {
        editAction = _action;
    }

    public void OnEdit()
    {
        if (input.text == "")
            return;

        int gold = int.Parse(input.text);

        item.saleGold = gold;

        if (editAction != null)
            editAction.Invoke();

        Hide();
    }

    public void OnExit() {
        item.saleGold = item.gold;
        Hide();
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }

}
