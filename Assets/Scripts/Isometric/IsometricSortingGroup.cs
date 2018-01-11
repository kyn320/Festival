using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/*
헤어 앞머리 = 20
헤어 뒷머리 = -40
머리 착용 = 22

머리 = 10
왼 눈 = 11
오른 눈 = 11
입 = 12
얼굴 착용 = 13

왼 팔 1  = 50
왼 팔 1 착용 = 52
왼 팔 2 = 51
왼 팔 2 착용 = 53

오른 팔 1 = -23
오른 팔 1 착용 = -21
오른 팔 2 = -22
오른 팔 2 착용 = - 20


바디 착용 = 1

바디 치마 = 40

바디 = 0


왼 다리 1 = 30
왼 다리 1 착용 = 32
왼 다리 2 = 31
왼 다리 2 착용 = 33

오른 다리 1 = -13
오른 다리 1 착용 = -11
오른 다리 2 = -12
오른 다리 2 착용 = -10
*/

[ExecuteInEditMode]
public class IsometricSortingGroup : MonoBehaviour
{

    Transform tr;

    public bool isAllowParentRenderer;
    public IsometricSpriteRenderer parentRenderer;

    SortingGroup sortingGroup;
    public int renderLevel;

    void Awake()
    {
        tr = GetComponent<Transform>();
        sortingGroup = GetComponent<SortingGroup>();
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
        if (isAllowParentRenderer)
        {
            if (parentRenderer == null)
                return;

            sortingGroup.sortingOrder = renderLevel + parentRenderer.renderType.sprRender.sortingOrder;
        }
        else
            sortingGroup.sortingOrder = Mathf.RoundToInt(tr.position.y * 100f) * -1 + renderLevel;
    }

    public void SetParent(IsometricSpriteRenderer _isoSpr, bool _isAllow)
    {
        parentRenderer = _isoSpr;
        isAllowParentRenderer = _isAllow;
    }

}




