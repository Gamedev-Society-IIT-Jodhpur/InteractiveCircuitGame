using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DragManager : MonoBehaviour
{
    [HideInInspector]public bool isDraggin = false;
    RaycastHit2D hit;
    Vector2 worldPoint;
    float prevX;
    float prevY;
    public float gridSpace=0.5f;
    void Update()
    {
        worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            
            hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                isDraggin = true;
                print(hit.collider.gameObject.transform.position);
                print(hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "node")
                {
                    hit.collider.gameObject.GetComponentInParent<Item>().isMoving = true;
                }
                else
                {
                    prevX = hit.collider.gameObject.transform.position.x;
                    prevY = hit.collider.gameObject.transform.position.y;
                }
            }
        }
        if (isDraggin && Input.GetMouseButton(0))
        {
            if (hit.collider.gameObject.tag == "node")
            {
                int x = (int)worldPoint.x;
                int y = (int)worldPoint.y;
                /*if (x % gridSpace != 0)
                {
                    x -= (x % gridSpace);
                }
                if (y % gridSpace != 0)
                {
                    y -= (y % gridSpace);
                }*/
                hit.collider.transform.position = new Vector3(x, y, 0);
            }
            else
            {
                float x = worldPoint.x;
                float y = worldPoint.y;
                
                if ((x-prevX) >= 1)
                {
                    prevX +=1 ;
                }
                else if (prevX-x>=1)
                {
                    prevX -= 1;
                }
                if ((y - prevY) >= 1)
                {
                    prevY += 1;
                }
                else if (prevY - y >= 1)
                {
                    prevY -= 1;
                }
                
                hit.collider.transform.position = new Vector3(prevX, prevY, 0);
            }
            
        }
        else
        {
            isDraggin = false;
            if (hit.collider!=null && hit.collider.gameObject.tag == "node")
            {
                hit.collider.gameObject.GetComponentInParent<Item>().isMoving = false;
            }
        }
    }
}
