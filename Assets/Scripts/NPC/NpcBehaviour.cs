using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBehaviour : MonoBehaviour
{
    /*
    - 아이템 구매
    - 대화
    - 상점 탐색
    */

    private Transform tr;
    private Rigidbody2D ri;

    public NPC info;

    public Vector3 movePosRange;
    int dir = -1;

    public bool isMove = false;
    public float moveDelay, moveDelayMin, moveDelayMax;

    public NpcState state;
    public int gold;

    public Animator ani;

    [SerializeField]
    ShopBehaviour enterShop = null;

    [SerializeField]
    ShopBehaviour targetShop;

    public List<WantItem> wantItemList;
    public int buyIndex;

    public List<Item> inventory;

    TalkBehaviour talkBehaviour;
    private PolyNavAgent _agent;
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
                    break;
            }
        }
        else {
            ChangeState(NpcState.Move);
        }
    }

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

    void Stop()
    {
        isMove = false;
    }

    void ChangeStateToMove()
    {
        isMove = false;
        ChangeState(NpcState.Move);
    }

    Vector2 GetRandomMovePos()
    {
        return new Vector2(tr.position.x + Random.Range(-movePosRange.x, movePosRange.x), tr.position.y + Random.Range(-movePosRange.y, movePosRange.y));
    }

    void MoveAnimate()
    {
        ani.SetFloat("Move", agent.GetSpeedRatio());
    }

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

    public void Say()
    {
        talkBehaviour.Talk();
    }

    public void Say(int _index)
    {
        talkBehaviour.Talk(_index);
    }

    public void Want()
    {
        talkBehaviour.Want(wantItemList[Random.Range(0, wantItemList.Count)].item);
    }

    public void Want(int _itemIndex)
    {
        talkBehaviour.Want(wantItemList[_itemIndex].item);
    }

    public void Want(int _index, int _itemIndex)
    {
        talkBehaviour.Want(_index, wantItemList[_itemIndex].item);
    }

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
            }
        }

        ChangeState(NpcState.Move);
    }

    public void Buy()
    {

        int index = enterShop.FindItemIndex(wantItemList[buyIndex].item.id, wantItemList[buyIndex].lowGold);

        //아이템이 존재하는지 체크
        if (index > -1)
        {
            Item item = enterShop.FindItem(index);
            if (CheckGold(-item.saleGold))
            {
                print("buy item : " + item.name + " | " + gameObject.name);
                //인벤에 아이템 등록
                inventory.Add(new Item(item));

                PlayDataManager.instance.AddGold(item.saleGold);
                AddGold(-item.saleGold);

                //슬롯에 아이템 삭제
                //enterShop.RemoveSlot(index);

                Want((3 + Random.Range(0, 2)), buyIndex);
                //리스트 업데이트
                enterShop.CreateBatchItem();
                UIInGame.instance.UpdateSalePannel();
                UIInGame.instance.UpdateInventory();
            }
            else {
                Say(5 + Random.Range(0, 2));
            }
        }

        buyIndex = -1;
        ChangeState(NpcState.Move);
    }

    public bool CheckGold(int _value)
    {
        if (gold + _value < 0)
            return false;
        else
            return true;
    }

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

public enum NpcState
{
    Move,
    Talk,
    Find,
    Buy
}
