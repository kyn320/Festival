using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 대화를 하는 컴포넌트 
/// </summary>
public class TalkBehaviour : MonoBehaviour
{
    /// <summary>
    /// 일반적인 대화
    /// </summary>
    public List<Talk> talkList;
    /// <summary>
    /// 원하는 아이템에 대한 대화
    /// </summary>
    public List<Talk> wantTalk;
    /// <summary>
    /// 구매 실패시 대화
    /// </summary>
    public List<Talk> buyFailTalk;
    /// <summary>
    /// 구매 성공시 대화
    /// </summary>
    public List<Talk> buySuccessTalk;
    /// <summary>
    /// 대화 딜레이 시간 
    /// </summary>
    public float talkDelayTime = 0f;
    /// <summary>
    /// 대화 기본 딜레이 시간 
    /// </summary>
    public float talkDelayTimeBase = 10f;
    /// <summary>
    /// 대화 딜레이 시간 랜덤 범위 
    /// </summary>
    public float talkDelayTimeRange = 10f;

    void Start()
    {
        talkDelayTime = talkDelayTimeBase + Random.Range(0, talkDelayTimeRange);
    }

    /// <summary>
    /// 랜덤하게 대화합니다.
    /// </summary>
    public void Talk()
    {
        StartCoroutine(TalkCoroutine());
    }

    /// <summary>
    /// 특정 카테고리 범위내에 랜덤하게 대화합니다.
    /// </summary>
    /// <param name="_category">카테고리 ID | 0 : Normal , 1 : Want , 2 : BuyFail , 3 : BuySuccess </param>
    public void Talk(int _category)
    {
        StartCoroutine(TalkCoroutine(_category));
    }

    /// <summary>
    /// 특정 카테고리의 인덱스로 대화합니다.
    /// </summary>
    /// <param name="_category">카테고리 ID | 0 : Normal , 1 : Want , 2 : BuyFail , 3 : BuySuccess </param>
    /// <param name="_index">인덱스</param>
    public void Talk(int _category, int _index)
    {
        StartCoroutine(TalkCoroutine(_category, _index));
    }

    /// <summary>
    /// 원하는 아이템을 표시합니다.
    /// </summary>
    /// <param name="_item">아이템</param>
    public void Want(Item _item)
    {
        StartCoroutine(WantCoroutine(_item));
    }

    IEnumerator TalkCoroutine()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                UIInGame.instance.ViewSay(this, talkList[Random.Range(0, talkList.Count)]);
                break;
            case 1:
                UIInGame.instance.ViewSay(this, wantTalk[Random.Range(0, wantTalk.Count)]);
                break;
            case 2:
                UIInGame.instance.ViewSay(this, buyFailTalk[Random.Range(0, buyFailTalk.Count)]);
                break;
            case 3:
                UIInGame.instance.ViewSay(this, buySuccessTalk[Random.Range(0, buySuccessTalk.Count)]);
                break;
        }
        yield return new WaitForSeconds(talkDelayTime);
    }

    IEnumerator TalkCoroutine(int _category)
    {
        switch (_category)
        {
            case 0:
                UIInGame.instance.ViewSay(this, talkList[Random.Range(0, talkList.Count)]);
                break;
            case 1:
                UIInGame.instance.ViewSay(this, wantTalk[Random.Range(0, wantTalk.Count)]);
                break;
            case 2:
                UIInGame.instance.ViewSay(this, buyFailTalk[Random.Range(0, buyFailTalk.Count)]);
                break;
            case 3:
                UIInGame.instance.ViewSay(this, buySuccessTalk[Random.Range(0, buySuccessTalk.Count)]);
                break;
        }
        yield return new WaitForSeconds(talkDelayTime);
    }

    IEnumerator TalkCoroutine(int _category, int _index)
    {
        switch (_category)
        {
            case 0:
                UIInGame.instance.ViewSay(this, talkList[_index]);
                break;
            case 1:
                UIInGame.instance.ViewSay(this, wantTalk[_index]);
                break;
            case 2:
                UIInGame.instance.ViewSay(this, buyFailTalk[_index]);
                break;
            case 3:
                UIInGame.instance.ViewSay(this, buySuccessTalk[_index]);
                break;
        }
        yield return new WaitForSeconds(talkDelayTime);
    }

    IEnumerator WantCoroutine(Item _item)
    {
        UIInGame.instance.ViewWant(this, _item);
        yield return null;
    }



}
