using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentNode : MonoBehaviour
{
    //[SerializeField] GameObject simArea;
    [SerializeField]SimArea simarea;
    float originalRadius;
    public bool colliderCheck = false;
    Vector3 originalLocalPosition;

    


    void Start()
    {
        //simarea = simArea.GetComponent<SimArea>();
        originalRadius = GetComponent<CircleCollider2D>().radius;
        originalLocalPosition = transform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        if (colliderCheck)
        {
            GetComponent<CircleCollider2D>().radius += 1000f * Time.deltaTime;

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        print("collision name: " + collision.name);
        if(colliderCheck && collision.gameObject.tag=="Grid Node")
        {
            colliderCheck = false;
            GetComponent<CircleCollider2D>().radius = originalRadius;
            transform.position=collision.transform.position;
            transform.parent.position = transform.position+new Vector3(0.25f,0,0);
            transform.localPosition = originalLocalPosition;

            
        }
    }
}
