using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentNode : MonoBehaviour
{
    [SerializeField]SimArea simarea;
    float originalRadius;
    public bool colliderCheck = false;
    Vector3 originalLocalPosition;

    [SerializeField] GameObject wire;
    GameObject newWire;
    SimArea simArea;



    void Start()
    {
        originalRadius = GetComponent<CircleCollider2D>().radius;
        originalLocalPosition = transform.localPosition;
        simArea = GameObject.FindWithTag("SimArea").GetComponent<SimArea>();
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
        //print("collision name: " + collision.name);
        if(colliderCheck && collision.gameObject.tag=="Grid Node")
        {
            colliderCheck = false;
            GetComponent<CircleCollider2D>().radius = originalRadius;
            transform.position=collision.transform.position;
            if (transform.parent.GetComponent<RectTransform>().rotation.z == 0)
            {
                transform.parent.position = transform.position + new Vector3(0.25f, 0, 0);
                transform.localPosition = originalLocalPosition;
            }
            else
            {
                transform.parent.position = transform.position + new Vector3(0, 0.25f, 0);
                transform.localPosition = originalLocalPosition;
            }
            

            
        }
    }

    public void StartDrawingWire()
    {
        print(gameObject.transform.parent.transform.parent);
        if (gameObject.transform.parent.transform.parent.tag == "SimArea")
        {
            newWire = Instantiate(wire, gameObject.transform.position, Quaternion.identity);
            newWire.transform.SetParent(simArea.transform);
            newWire.transform.localScale = simArea.transform.localScale;
            newWire.transform.SetAsFirstSibling();
            newWire.GetComponent<WireComponent>().Draw();
        }
        
    }
}
