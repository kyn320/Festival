using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Transform tr;
    private Rigidbody2D ri;

    public bool isLocal = false;

    public bool isInput = false, isMove = false;

    public Animator ani;

    Vector3 movePos;
    int dir = -1;

    public Transform anker;

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
        agent.OnDestinationReached += StopMove;
        agent.OnDestinationInvalid += StopMove;
    }

    void OnDisable()
    {
        agent.OnDestinationReached -= StopMove;
        agent.OnDestinationInvalid -= StopMove;
    }

    void Awake()
    {
        tr = GetComponent<Transform>();
        ri = GetComponent<Rigidbody2D>();
        anker.position = tr.position;
    }

    void Update()
    {
        if (isLocal)
        {
            if (isInput)
            {
                if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    movePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    movePos.z = 0;
                    agent.SetDestination(movePos);
                    anker.position = movePos;
                    isMove = true;
                }
            }
            Move();
            Flip();
        }
    }

    void StopMove()
    {
        isMove = false;
    }

    void Move()
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

    public void SetMove(bool _isMove)
    {
        isMove = _isMove;
    }

    public void SetInput(bool _isInput)
    {
        isInput = _isInput;
    }

    public void SetStop(bool _isStop)
    {
        SetInput(!_isStop);
        SetMove(!_isStop);
        if (_isStop)
            ri.constraints = RigidbodyConstraints2D.FreezeAll;
        else
            ri.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (_isStop)
        {
            agent.Stop();
            isMove = false;
        }

        ani.SetFloat("Move", 0);

    }


    void OnTriggerEnter2D(Collider2D _col)
    {
        if (isLocal && _col.CompareTag("Shop"))
        {
            _col.GetComponent<ShopBehaviour>().SetPlayer(this);
        }
    }

    void OnTriggerExit2D(Collider2D _col)
    {
        if (isLocal && _col.CompareTag("Shop"))
        {
            _col.GetComponent<ShopBehaviour>().SetPlayer(null);
        }
    }

}
