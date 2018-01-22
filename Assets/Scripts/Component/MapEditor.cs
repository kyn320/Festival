using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapEditor : MonoBehaviour
{
    public static MapEditor instance;
    public static bool isEditMode = false;
    public static bool isMouseCamControl = false;

    public UIMapEditor ui;

    [SerializeField]
    public Transform target;

    UnityAction action = null;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    public void SetEditMode(bool _isEdit)
    {
        isEditMode = _isEdit;
    }

    public void SetMouseControl(bool _isMouse)
    {
        isMouseCamControl = _isMouse;
        PlayDataManager.instance.player.SetInput(!_isMouse);
    }

    public void SetTarget(Transform _target, UnityAction _action)
    {
        target = _target;
        action = _action;
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (!isEditMode)
            return;

        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            if (target == null)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 30f, LayerMask.GetMask("Click"));
                if (hit.collider != null)
                {
                    target = hit.collider.gameObject.transform.root;
                }
            }
            else {
                target = null;

                if (action != null)
                {
                    action.Invoke();
                    action = null;
                }

                isEditMode = false;
                PlayDataManager.instance.player.SetInput(false);
            }
        }

        if (target != null)
        {
            target.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

    }

}
