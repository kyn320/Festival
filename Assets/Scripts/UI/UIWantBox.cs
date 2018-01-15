using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWantBox : MonoBehaviour
{

    public static UIWantManager wantManager;

    RectTransform tr;
    UITargetTracker targetTracker;

    public TalkBehaviour talker;
    public Item item;
    public Image iconImage;
    public Text contextText;
    public Vector3 margin;

    public float viewTime = 10f;

    void OnEnable()
    {
        tr.SetAsFirstSibling();
        iconImage.sprite = null;
        talker = null;
    }

    void Awake()
    {
        tr = GetComponent<RectTransform>();
        targetTracker = GetComponent<UITargetTracker>();
    }

    public void SetSay(TalkBehaviour _talker, string _context, Item _item)
    {
        talker = _talker;
        item = new Item(_item);
        iconImage.sprite = item.icon;
        contextText.text = _context;
        targetTracker.SetTarget(talker.transform, _talker.transform.position, margin);

        if (delay != null)
        {
            StopCoroutine(delay);
        }

        delay = StartCoroutine(Delay());
    }

    public void OnSelect()
    {
        UIInGame.instance.ViewItemInfo(item, true);
    }

    Coroutine delay = null;

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(viewTime);
        delay = null;
        wantManager.AddList(this);
    }
}
