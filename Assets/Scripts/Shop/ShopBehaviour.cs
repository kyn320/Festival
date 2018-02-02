using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBehaviour : MonoBehaviour
{
    Transform tr;
    public Shop info;

    public List<GameObject> batchObjectList;

    public SpriteRenderer signRenderer;
    public SpriteMask tentShapeMask;
    public SpriteRenderer tentPatternRenderer;

    public IsometricSpriteRenderer salePannel;
    public Transform salePannelBottom;

    public GameObject itemPrefab;

    public Vector2 startMargin;
    public Vector2 margin;
    public Vector2 slotSize;

    public PlayerController player;

    public bool isEdit = false, isMine = false;

    void Awake()
    {
        tr = GetComponent<Transform>();
        tentPatternRenderer.color = info.tentColor;
    }

    void Start()
    {
        SetSlot();
        CreateBatchItem();
        info.location = tr.position;
    }

    void Update()
    {
        if (player != null && !GameManager.instance.isOnMenu && Input.GetKeyDown(KeyCode.Space))
        {
            if (isMine)
                UIInGame.instance.ViewShopMenu(this, true);
            else
                UIInGame.instance.ViewSalePannel(this, true, true, false);
        }
    }

    public void SetShopMode(bool _isEdit)
    {
        isEdit = _isEdit;
    }

    public void SetPlayer(PlayerController _player)
    {
        player = _player;
    }

    public void SetSlot()
    {
        int len = info.salePannelSize.x * info.salePannelSize.y - info.itemList.Count;

        for (int i = 0; i < len; ++i)
        {
            info.itemList.Add(null);
        }
    }

    public void AddSlot(int _index, Item _item)
    {
        info.itemList[_index] = _item;
    }

    public void RemoveSlot(int _index)
    {
        info.itemList[_index] = null;
    }

    public Item FindItem(int _index)
    {
        return info.FindItem(_index);
    }

    public List<Item> FindItemList(Item _item)
    {
        return info.FindItemList(_item);
    }

    public int FindItemIndex(int _id, int _saleGold)
    {
        return info.FindItemIndex(_id, _saleGold);
    }

    public void CreateBatchItem()
    {
        ItemBehaviour itemBehaviour;
        IsometricSpriteRenderer itemSprRender;
        GameObject g;

        int len = info.salePannelSize.x * info.salePannelSize.y;

        Vector2 startPos = new Vector2(-((info.salePannelSize.x - 1) * (slotSize.x + margin.x) * 0.5f) + startMargin.x, (info.salePannelSize.y * (slotSize.y + margin.y)) + startMargin.y - margin.y * 0.5f);
        Vector2 size = new Vector2(info.salePannelSize.x * (slotSize.x + margin.x) + 0.3f, info.salePannelSize.y * (slotSize.y + margin.y) + 0.52f);
        salePannel.renderType.sprRender.size = size;

        BoxCollider2D boxCol = salePannel.renderType.sprRender.GetComponent<BoxCollider2D>();

        boxCol.size = size;
        boxCol.offset = new Vector2(0, size.y * 0.5f);

        boxCol = GetComponents<BoxCollider2D>()[1];
        boxCol.size = new Vector2(size.x * 1.5f, size.y * 1.2f);
        boxCol.offset = new Vector2(0, -size.y * 0.9f);

        salePannel.transform.localPosition = new Vector2(0, -(info.salePannelSize.y * (slotSize.y + margin.y) + startMargin.y) - 1);

        ClearBatchItem();

        for (int i = 0; i < len; ++i)
        {
            g = Instantiate(itemPrefab, salePannel.transform);
            batchObjectList.Add(g);
            g.transform.localPosition = new Vector2(startPos.x + (i % info.salePannelSize.x) * (slotSize.x + margin.x), startPos.y - (i / info.salePannelSize.x) * (slotSize.y + margin.y));
            itemSprRender = g.GetComponent<IsometricSpriteRenderer>();
            itemSprRender.SetParent(salePannel, true);
            itemBehaviour = g.GetComponent<ItemBehaviour>();

            if (i > info.itemList.Count - 1 || info.itemList[i] == null || info.itemList[i] == null || info.itemList[i].id == 0)
            {
                continue;
            }

            itemBehaviour.SetItem(info.itemList[i]);
        }
    }

    public void ClearBatchItem()
    {
        for (int i = 0; i < batchObjectList.Count; ++i)
        {
            Destroy(batchObjectList[i]);
        }

        batchObjectList.Clear();
    }

    public void ChangeDesign(int _shapeID, Sprite _shape, int _patternID, Sprite _pattern, Color _color)
    {
        tentShapeMask.sprite = _shape;
        info.tentShape = _shapeID;

        tentPatternRenderer.sprite = _pattern;
        info.tentPattern = _patternID;

        tentPatternRenderer.color = info.tentColor = _color;
    }

}
