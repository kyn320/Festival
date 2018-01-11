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

    public NpcState state;
    public int gold;
    
    public Animator ani;

    public List<Item> wantItemList;

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

    void OnEnable()
    {
        agent.OnDestinationReached += MoveRandom;
        agent.OnDestinationInvalid += MoveRandom;
    }

    void OnDisable()
    {
        agent.OnDestinationReached -= MoveRandom;
        agent.OnDestinationInvalid -= MoveRandom;
    }

    void Awake()
    {
        tr = GetComponent<Transform>();
        ri = GetComponent<Rigidbody2D>();
        talkBehaviour = GetComponent<TalkBehaviour>();
    }

    void Start()
    {
        StartCoroutine(UpdateState());
        SetRandomMovePos();
        Want(0);
    }

    IEnumerator UpdateState()
    {
        while (true)
        {
            switch (state)
            {
                case NpcState.Move:
                    Move();
                    Flip();
                    break;
                case NpcState.Talk:

                    break;
                case NpcState.Buy:

                    break;
            }

            yield return null;
        }
    }

    void MoveRandom()
    {
        if (isMove)
            SetRandomMovePos();
    }

    void Move()
    {
        ani.SetFloat("Move", agent.GetSpeedRatio());
    }

    void SetRandomMovePos() {
        agent.SetDestination(new Vector2(tr.position.x + Random.Range(-movePosRange.x, movePosRange.x), tr.position.y + Random.Range(-movePosRange.y, movePosRange.y)));
    }

    public void Flip()
    {
        if (agent.movingDirection.x < 0)
        {
            tr.localScale = new Vector3(-1, 1, 1);
            dir = -1;
        }
        else if (agent.movingDirection.x > 0) {
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
        talkBehaviour.Want(wantItemList[Random.Range(0, wantItemList.Count)]);
    }

    public void Want(int _index)
    {
        talkBehaviour.Want(wantItemList[_index]);
    }

    public void Buy()
    {

    }
}

public enum NpcState
{
    Move,
    Talk,
    Buy
}
