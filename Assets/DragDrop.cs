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
    Vector3 initialPos;
    Transform initialParent;
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

    }

    public void OnDrag(PointerEventData eventData)
    {
       // print("ondrag");
        isDropped = false;
        rectTranform.anchoredPosition += eventData.delta/canvas.scaleFactor;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("end drag");
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
}
