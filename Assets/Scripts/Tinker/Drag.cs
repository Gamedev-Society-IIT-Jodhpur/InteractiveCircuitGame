using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{
    Vector2 worldPoint;
    public bool isDraggin;
    float prevX;
    float prevY;
    float prevCursorX;
    float prevCursorY;
    float x;
    float y;
    float dragThreshold = 0.01f;
    float diffX;
    float diffY;
    NodeTinker[] nodes;
    RaycastHit2D hit;
    bool hasInitiatated;

    //List<GameObject> wires;

    // Start is called before the first frame update
    void Start()
    {
        hasInitiatated = false;
        isDraggin = true;
        worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = new Vector3(worldPoint.x, worldPoint.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!hasInitiatated && !IsMouseOverUI() && !StaticData.isSoldering)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hasInitiatated = true;
            }
            //gameObject.transform.position = new Vector3(worldPoint.x, worldPoint.y, transform.position.z);
        }
        

        if (((isDraggin && Input.GetMouseButton(0))|| !hasInitiatated) && !IsMouseOverUI() && !StaticData.isSoldering)
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
        else if (isDraggin && hasInitiatated)
        {
            isDraggin = false;
            nodes= GetComponentsInChildren<NodeTinker>();
            foreach (NodeTinker node in nodes)
            {
                foreach (GameObject wire in node.wires)
                {
                    wire.GetComponent<Wire>().isMoving = false;

                }
            }

            if (transform.parent!=null && dragThreshold==0.01f)
            {
                transform.parent = null;
            }
        }
    }

    private void OnMouseOver()
    {
        

        if (Input.GetMouseButtonDown(0) && !isDraggin && !WireManager.isDrawingWire && !StaticData.isSoldering)
        {
            worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDraggin = true;
            nodes = GetComponentsInChildren<NodeTinker>();
            foreach (NodeTinker node in nodes)
            {
                foreach(GameObject wire in node.wires)
                {
                    wire.GetComponent<Wire>().isMoving = true;

                }
            }
            prevX = gameObject.transform.position.x;
            prevY = gameObject.transform.position.y;
            prevCursorX = worldPoint.x;
            prevCursorY = worldPoint.y;
            diffX = prevX - prevCursorX;
            diffY = prevY - prevCursorY;
            CircuitManagerTinker.ChangeSelected(gameObject);

            
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

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}

