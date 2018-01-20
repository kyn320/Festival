using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMakerBrush : MonoBehaviour
{
    public Color brushColor;
    SpritePixelGrid pixelGrid;

    bool isDown = false;
    public bool eraserMode = false; 

    void Update()
    {
        if (!isDown && Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            isDown = true;
        }
        else if (isDown && Input.GetMouseButtonUp(0))
        {
            isDown = false;
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            eraserMode = !eraserMode;
        }

    }

    void FixedUpdate()
    {
        if (isDown)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 30f);
            if (hit.collider != null)
            {
                pixelGrid = hit.collider.gameObject.GetComponent<SpritePixelGrid>();
                if(eraserMode)
                    pixelGrid.UpdateColor(Color.white);
                else
                pixelGrid.UpdateColor(brushColor);
            }
        }
    }
}
