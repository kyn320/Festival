using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWantManager : MonoBehaviour {

    public GameObject wantBoxPrefab;
    Queue<UIWantBox> objectPoolList = new Queue<UIWantBox>();

    void Awake()
    {
        UIWantBox.wantManager = this;
    }

    public void View(TalkBehaviour _talker, string _context,Item _item)
    {
        UIWantBox sayBox = GetObject();
        sayBox.gameObject.SetActive(true);
        sayBox.SetSay(_talker, _context, _item);
    }

    public void AddList(UIWantBox _wantBox)
    {
        objectPoolList.Enqueue(_wantBox);
        _wantBox.gameObject.SetActive(false);
    }

    public UIWantBox GetObject()
    {
        if (objectPoolList.Count < 1)
        {
            return Instantiate(wantBoxPrefab, transform).GetComponent<UIWantBox>();
        }
        else {
            return objectPoolList.Dequeue();
        }
    }
}
