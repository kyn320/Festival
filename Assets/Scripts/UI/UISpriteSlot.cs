using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteSlot : MonoBehaviour
{
    public static UISpriteListView listView;

    public int index;

    public Image image;

    public Sprite sprite;
    public Text spriteNameText;
    
    public void SetSlot(int _index, Sprite _sprite)
    {
        index = _index;
        sprite = _sprite;
        image.sprite = sprite;
        spriteNameText.text = sprite.name;
    }

    public void OnClick()
    {
        SpriteMaker.instance.CreateFile(sprite);
        UISpriteListView.ui.ViewList(false, true);
    }

}
