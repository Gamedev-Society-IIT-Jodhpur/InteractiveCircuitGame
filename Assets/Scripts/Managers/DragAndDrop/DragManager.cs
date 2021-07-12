using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DragManager : MonoBehaviour
{
    [HideInInspector] public bool isDraggin = false;
    RaycastHit2D hit;
    Vector2 worldPoint;
    float prevX;
    float prevY;
    float prevCursorX;
    float prevCursorY;
    Transform[] childs;
    Vector2 child1Pos;
    Vector2 child2Pos;
    int currentChild;
    public int mode = 0;
    //[SerializeField] Texture2D dragCursorTexture;
    int x2, y2, x1, y1;
    GameObject newComponent;
    bool toDraw = true;
    [SerializeField] List<GameObject> newComponentPrefabs;
    GameObject toInstantiate;
    int axis = 0;
    bool forward = true;


    void Update()
    {
        worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mode == 0)
        {

            if (Input.GetMouseButtonDown(0))
            {

                hit = Physics2D.Raycast(worldPoint, Vector2.zero);

                if (hit.collider != null)
                {
                    isDraggin = true;

                    if (hit.collider.gameObject.tag == "node")
                    {
                        
                        hit.collider.gameObject.GetComponentInParent<Item>().isMoving = true;
                        CircuitManager.ChangeSelected(hit.collider.gameObject.transform.parent.gameObject);
                        childs = hit.collider.gameObject.transform.parent.gameObject.GetComponentsInChildren<Transform>();
                        child1Pos = childs[1].transform.position;
                        child2Pos = childs[2].transform.position;
                        if(Vector2.Distance(worldPoint,child1Pos)< Vector2.Distance(worldPoint, child2Pos))
                        {
                            currentChild = 1;
                        }
                        else
                        {
                            currentChild = 2;
                        }

                    }
                    else if(hit.collider.gameObject.tag == "bjt node")
                    {
                        hit.collider.transform.parent.gameObject.GetComponent<BJTItem>().isMoving = true;
                        CircuitManager.ChangeSelected(hit.collider.gameObject.transform.parent.gameObject);
                        childs = hit.collider.gameObject.transform.parent.gameObject.GetComponentsInChildren<Transform>();
                        child1Pos = childs[0].position;
                        child2Pos = childs[3].position;
                        if (child1Pos.x == child2Pos.x)
                        {
                            axis = 1;
                            if (child1Pos.y < child2Pos.y)
                            {
                                forward = true;
                            }
                            else
                            {
                                forward = false;
                            }
                        }
                        else
                        {
                            axis = 0;
                            if (child1Pos.x < child2Pos.x)
                            {
                                forward = true;
                            }
                            else
                            {
                                forward = false;
                            }
                        }

                    }
                    else
                    {
                        prevX = hit.collider.gameObject.transform.position.x;
                        prevY = hit.collider.gameObject.transform.position.y;
                        prevCursorX = worldPoint.x;
                        prevCursorY = worldPoint.y;
                        CircuitManager.ChangeSelected(hit.collider.gameObject);
                    }
                }
            }
            if (isDraggin && Input.GetMouseButton(0))
            {
                if (hit.collider.gameObject.tag == "node")
                {
                    int x = Mathf.RoundToInt(worldPoint.x);
                    int y = Mathf.RoundToInt(worldPoint.y);


                    if (currentChild== 2)
                    {
                        if (Vector3.Distance(childs[1].transform.position, new Vector3(x, y, 0)) >= 2)
                        {
                            hit.collider.transform.position = new Vector3Int(x, y, 0);
                        }
                    }
                    else
                    {
                        if (Vector3.Distance(childs[2].transform.position, new Vector3(x, y, 0)) >= 2)
                        {
                            hit.collider.transform.position = new Vector3Int(x, y, 0);
                        }
                    }

                }
                else if(hit.collider.gameObject.tag == "bjt node")
                {
                    if (axis == 0)
                    {
                        int x = Mathf.RoundToInt(worldPoint.x);
                        int y = Mathf.RoundToInt(hit.collider.transform.position.y);
                        if (forward)
                        {
                            if (x - childs[0].transform.position.x >= 0)
                            {
                                hit.collider.transform.position = new Vector3Int(x, y, 0);
                            }

                        }
                        else
                        {
                            if (x - childs[0].transform.position.x <= 0)
                            {
                                hit.collider.transform.position = new Vector3Int(x, y, 0);
                            }
                        }
                        
                    }
                    else
                    {
                        int x = Mathf.RoundToInt(hit.collider.transform.position.x);
                        int y = Mathf.RoundToInt(worldPoint.y);
                        if (forward)
                        {
                            if (y - childs[0].transform.position.y >= 0)
                            {
                                hit.collider.transform.position = new Vector3Int(x, y, 0);
                            }

                        }
                        else
                        {
                            if (y - childs[0].transform.position.y <= 0)
                            {
                                hit.collider.transform.position = new Vector3Int(x, y, 0);
                            }
                        }
                        
                    }

                }
                else
                {
                    float x = worldPoint.x;
                    float y = worldPoint.y;

                    if (Vector2.Distance(new Vector2(x,y),new Vector2(prevCursorX, prevCursorY)) >= 1)
                    {
                        if ((x - prevCursorX) >= 1)
                        {
                            prevX += Mathf.RoundToInt(x - prevCursorX);
                            prevCursorX = x;
                        }
                        else if (prevCursorX - x >= 1)
                        {
                            prevX -= Mathf.RoundToInt(prevCursorX - x);
                            prevCursorX = x;
                        }
                        if ((y - prevCursorY) >= 1)
                        {
                            prevY += Mathf.RoundToInt(y - prevCursorY);
                            prevCursorY = y;
                        }
                        else if (prevCursorY - y >= 1)
                        {
                            prevY -= Mathf.RoundToInt(prevCursorY - y);
                            prevCursorY = y;
                        }

                        hit.collider.transform.position = new Vector3(prevX, prevY, 0);
                    }
                }


            }
            else if (isDraggin)
            {
                isDraggin = false;
                if (hit.collider != null && hit.collider.gameObject.tag == "node")
                {
                    hit.collider.gameObject.GetComponentInParent<Item>().isMoving = false;
                }
                else if (hit.collider != null && hit.collider.gameObject.tag == "bjt node")
                {
                    hit.collider.gameObject.GetComponentInParent<BJTItem>().isMoving = false;
                }
            }
        }
        else
        {


            //Cursor.SetCursor(dragCursorTexture, Vector2.zero, CursorMode.Auto);
            if (Input.GetMouseButtonDown(0))
            {
                x1 = Mathf.RoundToInt(worldPoint.x);
                y1 = Mathf.RoundToInt(worldPoint.y);
                isDraggin = true;
            }
            if (isDraggin && Input.GetMouseButton(0))
            {
                x2 = Mathf.RoundToInt(worldPoint.x);
                y2 = Mathf.RoundToInt(worldPoint.y);
                if ((Vector2.Distance(new Vector2(x1, y1), new Vector2(x2, y2))) >= 2 && toDraw)
                {
                    
                    toDraw = false;
                    newComponent = Instantiate<GameObject>(toInstantiate);
                    CircuitManager.ChangeSelected(newComponent);
                    newComponent.GetComponent<Item>().isMoving = true;
                    childs = newComponent.GetComponentsInChildren<Transform>();
                    childs[1].transform.position = new Vector3(x1, y1, 0);


                }
                if (newComponent)
                {
                    int x = Mathf.RoundToInt(worldPoint.x);
                    int y = Mathf.RoundToInt(worldPoint.y);
                    if (Vector3.Distance(childs[1].transform.position, new Vector3(x, y, 0)) >= 2)
                    {
                        childs[2].transform.position = new Vector3(x, y, 0);
                    }
                }
            }
            else if (isDraggin)
            {
                isDraggin = false;
                if (newComponent) newComponent.GetComponent<Item>().isMoving = false;
                newComponent = null;
                toDraw = true;
            }
        }
    }

    public void DragMode(int n)
    {
        if (n == 0)
        {
            mode = 0;
        }
        else
        {
            mode = 1;
            toInstantiate = newComponentPrefabs[n - 1];
        }

    }

    /*public void ChangeValue(string value)
    {
        ComponentInitialization component= CircuitManager.selected.GetComponent<ComponentInitialization>();
        if (component.tag != "Wire")
        {
            component.value = value;
            component.valueText.text = value;
        }
        
    }*/
}
