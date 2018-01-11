using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class IsometricArraySpriteRenderer : MonoBehaviour
{

    Transform tr;

    public bool isAllowParentRenderer;
    public IsometricSpriteRenderer parentRenderer;

    public IsometricRenderType[] renderTypeArray;

    void Awake()
    {
        tr = GetComponent<Transform>();
    }


    // Update is called once per frame
    void Update()
    {
        if (gameObject.isStatic)
            return;

        Rendering();
    }

    void Rendering()
    {
        for (int i = 0; i < renderTypeArray.Length; ++i)
        {
            if (isAllowParentRenderer)
            {
                if (parentRenderer == null)
                    return;

                renderTypeArray[i].sprRender.sortingOrder = renderTypeArray[i].renderLevel + parentRenderer.renderType.sprRender.sortingOrder;
            }
            else
                renderTypeArray[i].sprRender.sortingOrder = (Mathf.RoundToInt(tr.position.y * 100f) * -1) + renderTypeArray[i].renderLevel;
        }
    }

    public void SetParent(IsometricSpriteRenderer _isoSpr, bool _isAllow)
    {
        parentRenderer = _isoSpr;
        isAllowParentRenderer = _isAllow;
    }


}
