using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    [SerializeField] GameObject wireNode;
    Vector2 mousePos;
    Vector2 node2Pos;
    bool isDrawing;
    public bool isMoving=false;
    public Transform node1;
    public Transform node2;
    float distance;
    Vector2 angleVector;
    float angle;
    RaycastHit2D hit;


    // Start is called before the first frame update
    void Start()
    {
        isDrawing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrawing)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position=new Vector3((mousePos.x+node1.transform.position.x)/2, (mousePos.y + node1.transform.position.y) / 2,0);
            distance = Vector2.Distance(mousePos, node1.transform.position);
            transform.localScale =new Vector3(distance,transform.localScale.y,transform.localScale.z);
            //transform.localScale =new Vector3(transform.localScale.x,distance/2,transform.localScale.z);

            angleVector = new Vector2(node1.transform.position.x - mousePos.x, node1.transform.position.y - mousePos.y);
            if (angleVector.x < 0)
            {
                angle = Mathf.Atan(-angleVector.y / angleVector.x) * (180 / Mathf.PI);
                transform.eulerAngles = new Vector3(0, 0, -angle);
            }
            else
            {
                angle = Mathf.Atan(angleVector.y / angleVector.x) * (180 / Mathf.PI);
                transform.eulerAngles = new Vector3(0, 0, angle);
            }

            if (Input.GetMouseButtonDown(0))
            {
                hit = Physics2D.Raycast(mousePos, Vector2.zero);
                if (hit.collider!=null && hit.collider.gameObject.tag == "node" && hit.collider.gameObject!=node1.gameObject)
                {
                    isDrawing = false;
                    WireManager.isDrawingWire = false;
                    GetComponentInParent<NewWireManager>().nodes.Add(hit.collider.transform);
                    node2 = hit.collider.transform;

                    mousePos = hit.collider.transform.position;
                    transform.position = new Vector3((mousePos.x + node1.transform.position.x) / 2, (mousePos.y + node1.transform.position.y) / 2, 0);
                    distance = Vector2.Distance(mousePos, node1.transform.position);
                    transform.localScale = new Vector3(distance, transform.localScale.y, transform.localScale.z);
                    //transform.localScale = new Vector3(transform.localScale.x, distance/2, transform.localScale.z);

                    angleVector = new Vector2(node1.transform.position.x - mousePos.x, node1.transform.position.y - mousePos.y);
                    if (angleVector.x < 0)
                    {
                        angle = Mathf.Atan(-angleVector.y / angleVector.x) * (180 / Mathf.PI);
                        transform.eulerAngles = new Vector3(0, 0, -angle);
                    }
                    else
                    {
                        angle = Mathf.Atan(angleVector.y / angleVector.x) * (180 / Mathf.PI);
                        transform.eulerAngles = new Vector3(0, 0, angle);
                    }

                }
                else if((hit.collider==null)||(/*hit.collider!=null &&*/ hit.collider.tag != "node"))
                {
                    node2 = Instantiate<GameObject>(wireNode).transform;
                    node2.transform.position = mousePos+new Vector2(0.01f,0.01f);
                    isDrawing = false;
                    GetComponentInParent<NewWireManager>().nodes.Add(node2);
                    GameObject newWire = Instantiate<GameObject>(gameObject);
                    newWire.GetComponent<Wire>().node1 = node2;
                    newWire.transform.SetParent(gameObject.transform.parent);

                    
                }
            }
        }

        if (isMoving)
        {
            node2Pos = node2.transform.position;
            transform.position = new Vector3((node2Pos.x + node1.transform.position.x) / 2, (node2Pos.y + node1.transform.position.y) / 2, 0);
            distance = Vector2.Distance(node2Pos, node1.transform.position);
            transform.localScale = new Vector3(distance, transform.localScale.y, transform.localScale.z);

            angleVector = new Vector2(node1.transform.position.x - node2Pos.x, node1.transform.position.y - node2Pos.y);
            if (angleVector.x < 0)
            {
                angle = Mathf.Atan(-angleVector.y / angleVector.x) * (180 / Mathf.PI);
                transform.eulerAngles = new Vector3(0, 0, -angle);
            }
            else
            {
                angle = Mathf.Atan(angleVector.y / angleVector.x) * (180 / Mathf.PI);
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
        }

    }
}