using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPC의 행동을 제어하는 컴포넌트
/// </summary>
public class NpcBehaviour : MonoBehaviour
{
    private Transform tr;
    private Rigidbody2D ri;

    /// <summary>
    /// NPC 정보
    /// </summary>
    public NPC info;
    /// <summary>
    /// 랜덤 움직임의 범위
    /// </summary>
    public Vector3 movePosRange;
    /// <summary>
    /// 방향
    /// </summary>
    int dir = -1;
    /// <summary>
    /// 움직이는 중인가?
    /// </summary>
    public bool isMove = false;
    /// <summary>
    /// 랜덤 무브의 기본 딜레이 시간
    /// </summary>
    public float moveDelay;
    /// <summary>
    /// 랜덤 무브의 최소 딜레이 시간
    /// </summary>
    public float moveDelayMin;
    /// <summary>
    /// 랜덤 무브의 최대 딜레이 시간
    /// </summary>
    public float moveDelayMax;
    /// <summary>
    /// 상태
    /// </summary>
    public NpcState state;
    /// <summary>
    /// 소지 골드
    /// </summary>
    public int gold;
    /// <summary>
    /// 애니메이터
    /// </summary>
    public Animator ani;
    /// <summary>
    /// 근처의 상점
    /// </summary>
    [SerializeField]
    ShopBehaviour enterShop = null;
    /// <summary>
    /// 도착 해야할 상점
    /// </summary>
    [SerializeField]
    ShopBehaviour targetShop;
    /// <summary>
    /// NPC가 구매하려는 아이템 리스트
    /// </summary>
    public List<WantItem> wantItemList;
    /// <summary>
    /// 구매 하려는 아이템 인덱스
    /// </summary>
    public int buyIndex;
    /// <summary>
    /// 인벤토리
    /// </summary>
    public List<Item> inventory;
    /// <summary>
    /// 대화 컴포턴트
    /// </summary>
    TalkBehaviour talkBehaviour;
    private PolyNavAgent _agent;
    /// <summary>
    /// 길찾기 AI
    /// </summary>
    public PolyNavAgent agent
    {
        get
        {
            if (!_agent)
                _agent = GetComponent<PolyNavAgent>();
            return _agent;
        }
    }

    void Awake()
    {
        tr = GetComponent<Transform>();
        ri = GetComponent<Rigidbody2D>();
        talkBehaviour = GetComponent<TalkBehaviour>();
    }

    void Start()
    {
        ChangeState(NpcState.Move);
    }

    /// <summary>
    /// NPC의 행동을 제어합니다.
    /// </summary>
    /// <param name="_state">행동 값</param>
    public void ChangeState(NpcState _state)
    {
        agent.ClearDestinationEvent();

        state = _state;

        switch (state)
        {
            case NpcState.Move:
                if (moveCoroutine != null)
                {
                    StopCoroutine(moveCoroutine);
                }
                moveCoroutine = StartCoroutine(Move());
                break;
            case NpcState.Talk:

                break;
            case NpcState.Find:
                if (moveCoroutine != null)
                {
                    StopCoroutine(moveCoroutine);
                }
                targetShop = ShopManager.instance.GetRandomShop();
                moveCoroutine = StartCoroutine(MoveTargetAndAction(targetShop.salePannelBottom.position, Find));
                break;
            case NpcState.Buy:
                Buy();
                break;
            default: break;
        }
    }

    Coroutine moveCoroutine = null;

    /// <summary>
    /// 랜덤한 범위 내로 이동합니다.
    /// </summary>
    /// <returns></returns>
    IEnumerator Move()
    {
        isMove = true;
        agent.OnDestinationInvalid += Stop;
        agent.OnDestinationReached += Stop;
        agent.SetDestination(GetRandomMovePos());

        while (isMove)
        {
            MoveAnimate();
            Flip();
            yield return null;
        }
        MoveAnimate();

        yield return new WaitForSeconds(moveDelay + Random.Range(moveDelayMin, moveDelayMax));

        moveCoroutine = null;
        if (Random.Range(0f, 1f) < info.findItemPercent * 0.01f)
        {
            ChangeState(NpcState.Find);
        }
        else if (Random.Range(0f, 1f) < 0.5f)
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    Say();
                    break;
                case 1:
                    Want();
                    Say(1);
                    break;
            }
            ChangeState(NpcState.Move);
        }
        else {
            ChangeState(NpcState.Move);
        }
    }

    /// <summary>
    /// 특정 지점으로 이동합니다.
    /// </summary>
    /// <param name="movePos">도착 지점</param>
    /// <returns></returns>
    IEnumerator MoveTarget(Vector2 movePos)
    {
        isMove = true;
        agent.SetDestination(movePos);
        while (isMove)
        {
            MoveAnimate();
            Flip();
            yield return null;
        }
        MoveAnimate();
        moveCoroutine = null;
    }

    /// <summary>
    /// 특정 지점으로 이동하고 메소드를 실행합니다.
    /// </summary>
    /// <param name="movePos">도착 지점</param>
    /// <param name="_action">메소드</param>
    /// <returns></returns>
    IEnumerator MoveTargetAndAction(Vector2 movePos, System.Action _action)
    {
        isMove = true;
        agent.OnDestinationInvalid += ChangeStateToMove;
        agent.OnDestinationReached += _action;
        agent.SetDestination(movePos);
        while (isMove)
        {
            MoveAnimate();
            Flip();
            yield return null;
        }
        MoveAnimate();
        moveCoroutine = null;
    }

    /// <summary>
    /// 움직임을 멈춥니다.
    /// </summary>
    void Stop()
    {
        isMove = false;
    }

    /// <summary>
    /// 랜덤 무브 상태로 변경합니다.
    /// </summary>
    void ChangeStateToMove()
    {
        isMove = false;
        ChangeState(NpcState.Move);
    }

    /// <summary>
    /// 플레이어 주변으로 랜덤 범위내의 도착 지점을 얻습니다.
    /// </summary>
    /// <returns></returns>
    Vector2 GetRandomMovePos()
    {
        return new Vector2(tr.position.x + Random.Range(-movePosRange.x, movePosRange.x), tr.position.y + Random.Range(-movePosRange.y, movePosRange.y));
    }

    /// <summary>
    /// 움직임 애니메이션을 실행합니다.
    /// </summary>
    void MoveAnimate()
    {
        ani.SetFloat("Move", agent.GetSpeedRatio());
    }

    /// <summary>
    /// 캐릭터의 방향에 따라 스케일을 조정합니다.
    /// </summary>
    public void Flip()
    {
        if (agent.movingDirection.x < 0)
        {
            tr.localScale = new Vector3(-1, 1, 1);
            dir = -1;
        }
        else if (agent.movingDirection.x > 0)
        {
            tr.localScale = new Vector3(1, 1, 1);
            dir = 1;
        }
    }

    /// <summary>
    /// 랜덤하게 대화합니다.
    /// </summary>
    public void Say()
    {
        talkBehaviour.Talk();
    }

    /// <summary>
    /// 특정 카테고리 범위내에 랜덤하게 대화합니다.
    /// </summary>
    /// <param name="_category">카테고리 ID | 0 : Normal , 1 : Want , 2 : BuyFail , 3 : BuySuccess </param>
    public void Say(int _category)
    {
        talkBehaviour.Talk(_category);
    }

    /// <summary>
    /// 특정 카테고리의 인덱스로 대화합니다.
    /// </summary>
    /// <param name="_category">카테고리 ID | 0 : Normal , 1 : Want , 2 : BuyFail , 3 : BuySuccess</param>
    /// <param name="_index">인덱스</param>
    public void Say(int _category, int _index)
    {
        talkBehaviour.Talk(_category, _index);
    }

    /// <summary>
    /// 랜덤하게 원하는 아이템을 표시합니다.
    /// </summary>
    public void Want()
    {
        talkBehaviour.Want(wantItemList[Random.Range(0, wantItemList.Count)].item);
    }

    /// <summary>
    /// 특정 인덱스의 원하는 아이템을 표시합니다.
    /// </summary>
    /// <param name="_itemIndex">인덱스</param>
    public void Want(int _itemIndex)
    {
        talkBehaviour.Want(wantItemList[_itemIndex].item);
    }

    /// <summary>
    /// 상점에서 아이템을 검색합니다.
    /// </summary>
    void Find()
    {
        agent.Stop();

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }

        isMove = false;

        int randIndex = Random.Range(0, wantItemList.Count);

        if (enterShop != null)
        {
            List<Item> resultList = enterShop.FindItemList(wantItemList[randIndex].item);

            if (resultList.Count > 0)
            {
                //학습
                wantItemList[randIndex].UpdateSaleGold(enterShop, resultList);

                if (Random.Range(0f, 1f) < info.buyItemPercent * 0.01f)
                {
                    //바로 구매
                    buyIndex = randIndex;
                    ChangeState(NpcState.Buy);
                    return;
                }
            }
            else {
                Want(randIndex);
                Say(1);
            }
        }

        ChangeState(NpcState.Move);
    }

    /// <summary>
    /// 상점에서 아이템을 구매합니다.
    /// </summary>
    public void Buy()
    {

        int index = enterShop.FindItemIndex(wantItemList[buyIndex].item.id, wantItemList[buyIndex].lowGold);

        //아이템이 존재하는지 체크
        if (index > -1)
        {
            Item item = enterShop.FindItem(index);
            if (CheckGold(-item.saleGold))
            {
                //구매 성공
                print("buy item : " + item.name + " | " + gameObject.name);
                //인벤에 아이템 등록
                inventory.Add(new Item(item));

                PlayDataManager.instance.AddGold(item.saleGold);
                AddGold(-item.saleGold);

                //슬롯에 아이템 삭제
                enterShop.RemoveSlot(index);

                Want(buyIndex);
                Say(3);

                //리스트 업데이트
                enterShop.CreateBatchItem();
                UIInGame.instance.UpdateSalePannel();
                UIInGame.instance.UpdateInventory();
            }
            else {
                //구매 실패
                Say(2);
            }
        }

        buyIndex = -1;
        ChangeState(NpcState.Move);
    }

    /// <summary>
    /// 골드를 체크합니다.
    /// </summary>
    /// <param name="_value">골드 값</param>
    /// <returns></returns>
    public bool CheckGold(int _value)
    {
        if (gold + _value < 0)
            return false;
        else
            return true;
    }

    /// <summary>
    /// 골드를 추가합니다.
    /// </summary>
    /// <param name="_value">골드 값</param>
    public void AddGold(int _value)
    {
        gold += _value;
    }

    void OnTriggerEnter2D(Collider2D _col)
    {
        if ((state == NpcState.Find || state == NpcState.Buy) && enterShop == null && _col.CompareTag("Shop"))
        {
            enterShop = _col.GetComponent<ShopBehaviour>();
        }
    }

    void OnTriggerExit2D(Collider2D _col)
    {
        if (enterShop != null && _col.CompareTag("Shop"))
        {
            enterShop = null;
        }
    }
}

/// <summary>
/// NPC의 상태
/// </summary>
public enum NpcState
{
    Move,
    Talk,
    Find,
    Buy
}
