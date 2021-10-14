using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeTinker : MonoBehaviour
{
    GameObject wireManager;
    public List<GameObject> wires;
    NodeTinker[] nodes;
    public bool needSoldering = false;
    public bool isConnectedToBreadboard = false;
    public bool isConnectedToComponent = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        wireManager = AssetManager.wireManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "node" && GetComponentInParent<Drag>().isDraggin == true/*((GetComponentInParent<Drag>()&& GetComponentInParent<Drag>().isDraggin == true)||
            (transform.parent.GetComponentInParent<Drag>() && transform.parent.GetComponentInParent<Drag>().isDraggin == true))*/)
        {
            GetComponentInParent<Drag>().Snap(collision.transform.position, gameObject.transform);

            if (collision.transform.parent.tag == "Breadboard grid")
            {
                isConnectedToBreadboard = true;
            }
            else
            {
                isConnectedToComponent = true;
            }
            if(transform.parent.tag!="Breadboard grid") //should not solder when we drag the breadboard.
            {
                needSoldering = true;
            }
            

            if (collision.transform.parent.tag == "Breadboard grid")
            {
                transform.parent.SetParent(collision.transform.parent.parent);


            }
            else if (transform.parent.tag == "Breadboard grid")
            {
                collision.transform.parent.SetParent(transform.parent.parent);
                nodes = collision.transform.parent.GetComponentsInChildren<NodeTinker>();
                foreach (NodeTinker node in nodes)
                {
                    foreach (GameObject wire in node.wires)
                    {
                        wire.GetComponent<Wire>().isMoving = true;

                    }
                }
            }

            if (transform.parent!=null && transform.parent.tag!="Breadboard grid"  && collision.transform.parent.tag != "Breadboard grid"/*collision.transform.parent.parent != null && collision.transform.parent.parent.tag != "Breadboard"*/)
            {
                transform.parent.SetParent(null);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "node" && GetComponentInParent<Drag>().isDraggin == true/*((GetComponentInParent<Drag>() && GetComponentInParent<Drag>().isDraggin == true) ||
            (transform.parent.GetComponentInParent<Drag>() && transform.parent.GetComponentInParent<Drag>().isDraggin == true))*/)
        {
            needSoldering = false;
            if (collision.transform.parent.tag == "Breadboard grid")
            {
                isConnectedToBreadboard = false;
            }
            else
            {
                isConnectedToComponent = false;
            }
        }

        
    }

    private void OnMouseOver()
    {
        if (!IsMouseOverUI())
        {
            GetComponent<SpriteRenderer>().enabled = true;
            if (Input.GetMouseButtonDown(0) && !WireManager.isDrawingWire && !StaticData.isSoldering)
            {
                if (AssetManager.isSolderingIron && transform.parent.tag!="Breadboard grid")
                {
                    AssetManager.solderingIronIcon.Solder(transform.position);
                    wireManager.GetComponent<WireManager>().DrawWire(gameObject.transform);
                }
                else if(transform.parent.tag != "Breadboard grid")
                {
                    print("there is no soldering iron");
                }
                if(transform.parent.tag == "Breadboard grid")
                {
                    wireManager.GetComponent<WireManager>().DrawWire(gameObject.transform);
                }
            }
        }
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

}
