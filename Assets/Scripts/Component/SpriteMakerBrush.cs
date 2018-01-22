using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMakerBrush : MonoBehaviour
{
    SpriteRenderer spr;
    CircleCollider2D brushCollider;

    public Color brushColor;
    SpritePixelGrid pixelGrid;

    public float burshSize = 2f;

    public float lerpTime = 10f;

    bool isDown = false;
    public bool eraserMode = false;

    Vector3 brushPos;

    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        brushCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (!isDown && Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            isDown = true;
            brushCollider.enabled = false;
            brushCollider.enabled = true;
        }
        else if (isDown && Input.GetMouseButtonUp(0))
        {
            isDown = false;
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            eraserMode = !eraserMode;
        }
    }

    public void ChangeColor(Color _color)
    {
        brushColor = _color;
        spr.color = _color;
    }

    public void ChangeSize(float _size)
    {
        burshSize = _size;
        transform.localScale = burshSize * Vector3.one;
    }

    void FixedUpdate()
    {
        brushPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        brushPos.z = 0;

        transform.position = Vector3.Lerp(transform.position, brushPos, lerpTime);
    }

    void OnTriggerEnter2D(Collider2D _col)
    {
        pixelGrid = _col.gameObject.GetComponent<SpritePixelGrid>();
        if (isDown)
        {
            if (eraserMode)
                pixelGrid.UpdateColor(Color.white);
            else
                pixelGrid.UpdateColor(brushColor);
        }
        else {
            pixelGrid.UpdateOverColor(true, Color.gray);
        }
    }
    
    void OnTriggerExit2D(Collider2D _col)
    {
        pixelGrid = _col.gameObject.GetComponent<SpritePixelGrid>();
        pixelGrid.UpdateOverColor(false, Color.white);
    }

}
