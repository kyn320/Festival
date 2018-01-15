using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 상점 데이터 클래스
/// </summary>
[System.Serializable]
public class Shop
{
    /// <summary>
    /// 이름
    /// </summary>
    public string name;
    /// <summary>
    /// 위치 좌표
    /// </summary>
    public Vector2 location;
    /// <summary>
    /// 상점 판매판 사이즈
    /// </summary>
    public Vector2Int salePannelSize;
    /// <summary>
    /// 판매 리스트
    /// </summary>
    public List<Item> itemList;
    /// <summary>
    /// 천막 모양
    /// </summary>
    public int tentShape;
    /// <summary>
    /// 천막 패턴
    /// </summary>
    public int tentPattern;
    /// <summary>
    /// 천막 색상
    /// </summary>
    public Color tentColor;

    /// <summary>
    /// 아이템을 인덱스로 검색합니다.
    /// </summary>
    /// <param name="_index">인덱스</param>
    /// <returns></returns>
    public Item FindItem(int _index)
    {
        return itemList[_index];
    }

    /// <summary>
    /// 아이템을 검색하여 리스트로 변환합니다.
    /// </summary>
    /// <param name="_item"></param>
    /// <returns></returns>
    public List<Item> FindItemList(Item _item)
    {
        if (itemList == null || itemList.Count < 1)
            return null;

        List<Item> resultList = new List<Item>();

        for (int i = 0; i < itemList.Count; ++i)
        {
            if (itemList[i] != null && itemList[i].id == _item.id)
                resultList.Add(itemList[i]);
        }

        return resultList;
    }

    /// <summary>
    /// 아이템 고유 ID와 판매 가격으로 아이템 인덱스를 얻습니다.
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_saleGold"></param>
    /// <returns></returns>
    public int FindItemIndex(int _id, int _saleGold)
    {
        for (int i = 0; i < itemList.Count; ++i)
        {
            if (itemList[i] != null && itemList[i].id == _id && itemList[i].saleGold == _saleGold)
            {
                return i;
            }
        }
        return -1;
    }

}
