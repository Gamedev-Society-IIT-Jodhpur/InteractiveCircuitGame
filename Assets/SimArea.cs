using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimArea : MonoBehaviour, IDropHandler
{
    public Transform[] childs;
    Collider2D closestCollider;
    float distance = 10000f;
    GameObject dragginObject;
    //public bool colliderCheck = false;
    //float originalRadius;
    public void OnDrop(PointerEventData eventData)
    {
        dragginObject = eventData.pointerDrag;
        dragginObject.GetComponent<DragDrop>().isDropped = true;
        print("dropped");
        dragginObject.transform.SetParent(gameObject.transform);
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(dragginObject.transform.localPosition, 50f);
        childs = dragginObject.GetComponentsInChildren<Transform>();
        childs[1].GetComponent<ComponentNode>().colliderCheck = true;
        //childs[2].GetComponent<ComponentNode>().colliderCheck = true;
        //originalRadius = childs[2].GetComponent<CircleCollider2D>().radius;
        //colliderCheck = true;
        //print("here"+childs[2].name);
        //childs[2].GetComponent<CircleCollider2D>().
        //print("colliders lenght: " + colliders.Length);
        /*for (int i = 0; i < colliders.Length; i++)
        {

        }*/

        //childs[2].GetComponent<RectTransform>().anchoredPosition = colliders[0].GetComponent<RectTransform>().anchoredPosition;

    }

    void Update()
    {
        /*if (colliderCheck)
        {
            childs[2].GetComponent<CircleCollider2D>().radius += 12f * Time.deltaTime;
          
        }*/
    }

    
}

