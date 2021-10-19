using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovemetn : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    private Vector3 dragOrigin;
    [SerializeField]
    private float zoomStap, minZoom, maxZoom;

    void Update()
    {
        if (!IsMouseOverUI())
        {

            // Scroll forward
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                ZoomOrthoCamera(Camera.main.ScreenToWorldPoint(Input.mousePosition), 1);
            }

            // Scoll back
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                ZoomOrthoCamera(Camera.main.ScreenToWorldPoint(Input.mousePosition), -1);
            }

            PanCamera();
        }

        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log(Input.GetMouseButtonDown(2));
        }

    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }


    // Ortographic camera zoom towards a point (in world coordinates). Negative amount zooms in, positive zooms out
    // TODO: when reaching zoom limits, stop camera movement as well
    void ZoomOrthoCamera(Vector3 zoomTowards, float amount)
    {
        // Calculate how much we will have to move towards the zoomTowards position
        float multiplier = (1.0f / cam.orthographicSize * amount);

        // Move camera
        transform.position += (zoomTowards - transform.position) * multiplier;

        // Zoom camera
        cam.orthographicSize -= amount;

        // Limit zoom
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
    }

    void PanCamera()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(1) || Input.GetMouseButtonDown(2))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += difference; 
        }
    }
 
}
