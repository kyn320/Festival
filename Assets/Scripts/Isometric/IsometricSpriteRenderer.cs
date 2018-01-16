using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class IsometricSpriteRenderer : MonoBehaviour
{

    Transform tr;

    public bool isAllowParentRenderer;
    public IsometricSpriteRenderer parentRenderer;

    public IsometricRenderType renderType;

    void OnEnable()
    {
        if (renderType.sprRender == null)
            renderType.sprRender = GetComponent<SpriteRenderer>();
    }

    void Awake()
    {
        tr = GetComponent<Transform>();
        Rendering();

        if (gameObject.isStatic)
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        Rendering();
    }

    void Rendering()
    {
        if (isAllowParentRenderer)
        {
            if (parentRenderer == null)
                return;

            renderType.sprRender.sortingOrder = renderType.renderLevel + parentRenderer.renderType.sprRender.sortingOrder;
        }
        else
            renderType.sprRender.sortingOrder = Mathf.RoundToInt(tr.position.y * 100f) * -1 + renderType.renderLevel;
    }


    public void SetParent(IsometricSpriteRenderer _isoSpr, bool _isAllow)
    {
        parentRenderer = _isoSpr;
        isAllowParentRenderer = _isAllow;
    }
}
