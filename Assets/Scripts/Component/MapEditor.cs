using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditor : MonoBehaviour
{
    public static MapEditor instance;
    public static bool isEditMode = false;
    public static bool isMouseCamControl = false;

    [SerializeField]
    public Transform target;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartEditMode();
    }

    public void StartEditMode()
    {
        isEditMode = true;
        PlayDataManager.instance.player.SetInput(false);
    }

    public void StartMouseControl()
    {
        isMouseCamControl = true;
    }

    public void SetTarget(Transform _target) {
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
