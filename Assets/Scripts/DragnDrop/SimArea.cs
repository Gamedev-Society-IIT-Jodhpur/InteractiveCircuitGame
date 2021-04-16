using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimArea : MonoBehaviour, IDropHandler
{
    public Transform[] childs;
    GameObject dragginObject;
    public GameObject selectedNode;

    //public bool colliderCheck = false;
    //float originalRadius;
    public void OnDrop(PointerEventData eventData)
    {
        dragginObject = eventData.pointerDrag;
        dragginObject.GetComponent<DragDrop>().isDropped = true;
        print("dropped");
        dragginObject.transform.SetParent(gameObject.transform);
        childs = dragginObject.GetComponentsInChildren<Transform>();
        childs[1].GetComponent<ComponentNode>().colliderCheck = true;
        //childs[2].GetComponent<ComponentNode>().colliderCheck = true;
        //originalRadius = childs[2].GetComponent<CircleCollider2D>().radius;

    }

    void Update()
    {
        
    }

    

    
}

