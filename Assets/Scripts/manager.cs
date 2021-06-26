using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class manager : MonoBehaviour
{
    bool isDraggin = false;
    RaycastHit2D hit;
    Vector2 worldPoint;
    [SerializeField] float gridSpace=0.5f;
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
                //hit.collider.gameObject.GetComponent<Wire>
            }
        }
        if (isDraggin && Input.GetMouseButton(0))
        {
            float x = worldPoint.x;
            float y = worldPoint.y;
            if (x % gridSpace != 0)
            {
                x -= (x % gridSpace);
            }
            if (y % gridSpace != 0)
            {
                y -= (y % gridSpace);
            }
            hit.collider.transform.position = new Vector3(x, y,0);
        }
        else
        {
            isDraggin = false;
        }
    }
}
