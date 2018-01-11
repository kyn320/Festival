﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkBehaviour : MonoBehaviour
{
    public List<Talk> talkList;

    public float talkDelayTime = 0f, talkDelayTimeBase = 10f, talkDelayTimeRange = 10f;
    
    void Start()
    {
        talkDelayTime = talkDelayTimeBase + Random.Range(0, talkDelayTimeRange);
    }

    public void Talk()
    {
        StartCoroutine(TalkCoroutine());
    }

    public void Talk(int _index)
    {
        StartCoroutine(TalkCoroutine(_index));
    }

    public void Want(Item _item)
    {
        StartCoroutine(WantCoroutine(_item));
    }

    IEnumerator TalkCoroutine()
    {
        UIInGame.instance.ViewSay(this, talkList[Random.Range(0, talkList.Count)]);
        yield return new WaitForSeconds(talkDelayTime);
    }

    IEnumerator TalkCoroutine(int _index)
    {
        UIInGame.instance.ViewSay(this, talkList[_index]);
        yield return new WaitForSeconds(talkDelayTime);
    }

    IEnumerator WantCoroutine(Item _item)
    {
        UIInGame.instance.ViewWant(this, _item);
        yield return null;
    }

}