﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpritePixelGrid : MonoBehaviour
{

    public static SpriteMaker spriteMaker;

    public int index = 0;
    public Color color;

    public bool isOver = false;

    SpriteRenderer spr;

    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    public void SetColor(int _index, Color _color)
    {
        index = _index;
        color = _color;
        spr.color = color;
    }

    public void UpdateColor(Color _color)
    {
        color = _color;
        spr.color = color;

        spriteMaker.UpdateColor(index, color);
    }

    public void UpdateOverColor(bool _isOver, Color _overColor)
    {
        isOver = _isOver;

        if (isOver)
        {
            spr.color = _overColor;
        }
        else
            spr.color = color;
    }

}
