using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour,IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler,IDropHandler
{
    RectTransform rectTranform;
    Canvas canvas;
    CanvasGroup canvasGroup;
    public bool isDropped = false;
    public bool isDraggin = false;
    Vector3 initialPos;
    Transform initialParent;
    Vector3 mousePos;

    private void Awake()
    {
        rectTranform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        print("begin drag");
        initialParent = eventData.pointerDrag.transform.parent;
        eventData.pointerDrag.transform.SetParent(canvas.transform);
        initialPos = GetComponent<RectTransform>().anchoredPosition;
        canvasGroup.blocksRaycasts = false;
        isDraggin = true;

    }

    public void OnDrag(PointerEventData eventData)
    {
       // print("ondrag");
        isDropped = false;
        //rectTranform.anchoredPosition += eventData.delta/canvas.scaleFactor;
       // rectTranform.anchoredPosition += eventData.delta*1.101f;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("end drag");
        isDraggin = false;
        canvasGroup.blocksRaycasts = true;
        if (!isDropped)
        {
            GetComponent<RectTransform>().anchoredPosition = initialPos;
            gameObject.transform.parent = initialParent;
            //print(initialPos.x);
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("on pointer down");
    }


    public void OnDrop(PointerEventData eventData)
    {
       
    }

    void Update()
    {
        if (isDraggin)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.localPosition = mousePos/canvas.transform.localScale.x;
        }
    }
}
