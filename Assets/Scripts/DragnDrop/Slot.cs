using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour,IDropHandler
{
    [SerializeField] Transform inventory;
    
    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<DragDrop>().isDropped = true;
        eventData.pointerDrag.GetComponent<DragDrop>().isDraggin = false;
        print("dropped");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.SetParent(inventory);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.GetComponentsInChildren<Transform>()[1].GetComponent<Collider2D>().enabled = false;
            eventData.pointerDrag.GetComponentsInChildren<Transform>()[2].GetComponent<Collider2D>().enabled = false;

        }
    }

    
}
