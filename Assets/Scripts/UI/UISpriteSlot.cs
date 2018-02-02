using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISpriteSlot : MonoBehaviour
{
    public static UISpriteListView listView;

    public int index;

    public Image image;

    public Sprite sprite;
    public Text spriteNameText;

    public UnityAction<Sprite> clickAction;

    public void SetSlot(int _index, Sprite _sprite)
    {
        index = _index;
        sprite = _sprite;
        image.sprite = sprite;
        spriteNameText.text = sprite.name;
    }

    public void SetSlot(int _index, Sprite _sprite, UnityAction<Sprite> _action)
    {
        index = _index;
        sprite = _sprite;
        image.sprite = sprite;
        spriteNameText.text = sprite.name;
        clickAction = _action;
    }

    public void OnClick()
    {
        if (SpriteMaker.instance == null)
        {
            if (clickAction != null)
                clickAction.Invoke(sprite);
        }
        else
        {
            SpriteMaker.instance.CreateFile(sprite);
            UISpriteListView.ui.ViewList(false, true);
        }
    }

}
