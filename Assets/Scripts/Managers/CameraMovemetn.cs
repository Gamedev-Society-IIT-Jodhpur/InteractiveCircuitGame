using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovemetn : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    private Vector3 dragOrigin;
    [SerializeField]
    private float zoomStap, minZoom, maxZoom;

    void Update()
    {
        if (Input.mouseScrollDelta.y > 0.1f )
        {
            ZoomOut();
        }
        if (Input.mouseScrollDelta.y < -0.1f )
        {
            ZoomIn();
        }
        PanCamera();
    }

    void PanCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += difference; 
        }
    }

    void ZoomIn()
    {
        float newSize = cam.orthographicSize + zoomStap;
        cam.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
    }

    void ZoomOut()
    {
        float newSize = cam.orthographicSize - zoomStap;
        cam.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
    }
}
