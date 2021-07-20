using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    Vector2 worldPoint;
    public bool isDraggin = false;
    float prevX;
    float prevY;
    float prevCursorX;
    float prevCursorY;
    float x;
    float y;
    float dragThreshold = 0.01f;
    float diffX;
    float diffY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (isDraggin && Input.GetMouseButton(0))
        {
            x = worldPoint.x;
            y = worldPoint.y;

            if (Vector2.Distance(new Vector2(x, y), new Vector2(prevCursorX, prevCursorY)) >= dragThreshold)
            {
                if ((x - prevCursorX) >= dragThreshold)
                {
                    prevX += (x - prevCursorX);
                    prevCursorX = x;
                    dragThreshold = 0.01f;
                }
                else if (prevCursorX - x >= dragThreshold)
                {
                    prevX -= (prevCursorX - x);
                    prevCursorX = x;
                    dragThreshold = 0.01f;
                }
                if ((y - prevCursorY) >= dragThreshold)
                {
                    prevY += (y - prevCursorY);
                    prevCursorY = y;
                    dragThreshold = 0.01f;
                }
                else if (prevCursorY - y >= dragThreshold)
                {
                    prevY -= (prevCursorY - y);
                    prevCursorY = y;
                    dragThreshold = 0.01f;
                }

                gameObject.transform.position = new Vector3(prevX, prevY, gameObject.transform.position.z);
                
            }

            
        }
        else if (isDraggin)
        {
            isDraggin = false;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !isDraggin && !WireManager.isDrawingWire)
        {
            worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDraggin = true;
            prevX = gameObject.transform.position.x;
            prevY = gameObject.transform.position.y;
            prevCursorX = worldPoint.x;
            prevCursorY = worldPoint.y;
            diffX = prevX - prevCursorX;
            diffY = prevY - prevCursorY;
            //CircuitManager.ChangeSelected(hit.collider.gameObject);
        }
    }

    public void Snap(Vector3 snapPos,Transform childPos)
    {
        x = gameObject.transform.position.x - childPos.position.x;
        y = gameObject.transform.position.y - childPos.position.y;
        prevX = snapPos.x + x;
        prevY = snapPos.y + y;
        gameObject.transform.position = new Vector3(prevX, prevY, gameObject.transform.position.z);
        //dragThreshold = Vector2.Distance(worldPoint, new Vector2(prevX, prevY));
        dragThreshold = childPos.localScale.x*1.33f;
    }
}








/*if (Vector2.Distance(new Vector2(x, y), new Vector2(prevCursorX, prevCursorY)) >= dragThreshold)
            {
                if ((x - prevCursorX) >= dragThreshold)
                {
                    if (dragThreshold > 0.01f)
                    {
                        dragThreshold = 0.01f;
                        prevX = x + diffX;
                    }
                    else prevX += (x - prevCursorX);
                    prevCursorX = x;
                }
                else if (prevCursorX - x >= dragThreshold)
                {
                    if (dragThreshold > 0.01f)
                    {
                        dragThreshold = 0.01f;
                        prevX = x + diffX;
                    }
                    else prevX -= (prevCursorX - x);
                    prevCursorX = x;

                }
                if ((y - prevCursorY) >= dragThreshold)
                {
                    if (dragThreshold > 0.01f)
                    {
                        dragThreshold = 0.01f;
                        prevY = y + diffY;
                    }
                    else prevY += (y - prevCursorY);
                    prevCursorY = y;
                }
                else if (prevCursorY - y >= dragThreshold)
                {
                    if (dragThreshold > 0.01f)
                    {
                        dragThreshold = 0.01f;
                        prevY = y + diffY;
                    }
                    else prevY -= (prevCursorY - y);
                    prevCursorY = y;
                }

                gameObject.transform.position = new Vector3(prevX, prevY, gameObject.transform.position.z);

            }*/
