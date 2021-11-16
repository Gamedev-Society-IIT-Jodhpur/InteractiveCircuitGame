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
    GameObject soldered;
    Transform collision;
    public bool needSnapping;
    //Stack<Transform> prevSoldered;
    

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        wireManager = AssetManager.wireManager;
        soldered = AssetManager.soldered;
        needSnapping = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "node" && /*GetComponentInParent<Drag>().isDraggin == true*/ needSnapping)
        {
            GetComponentInParent<Drag>().Snap(collision.transform.position, gameObject.transform);

            if (collision.transform.parent.tag == "Breadboard grid")
            {
                isConnectedToBreadboard = true;
            }
            else
            {
                isConnectedToComponent = true;
                this.collision = collision.transform;

                

                collision.GetComponentInParent<Drag>().connecteds.Add(GetComponentInParent<Drag>());
                GetComponentInParent<Drag>().connecteds.Add(collision.GetComponentInParent<Drag>());
            }

            if(transform.parent.tag!="Breadboard grid") //should not solder when we drag the breadboard.
            {
                needSoldering = true;
            }
            

            if (collision.transform.parent.tag == "Breadboard grid")
            {
                if (transform.parent.parent == null)
                {
                    transform.parent.SetParent(collision.transform.parent.parent);
                }
                else if (transform.parent.parent.tag == "soldered")
                {
                    transform.parent.parent.SetParent(collision.transform.parent.parent);
                }
                //transform.parent.SetParent(collision.transform.parent.parent);

            }
            else if (transform.parent.tag == "Breadboard grid")
            {
                if (collision.transform.parent.parent == null)
                {
                    collision.transform.parent.SetParent(transform.parent.parent);
                }
                else if (collision.transform.parent.parent.tag == "soldered")
                {
                    collision.transform.parent.parent.SetParent(transform.parent.parent);
                }
                //collision.transform.parent.SetParent(transform.parent.parent);

                nodes = collision.transform.parent.GetComponentsInChildren<NodeTinker>();
                foreach (NodeTinker node in nodes)
                {
                    foreach (GameObject wire in node.wires)
                    {
                        wire.GetComponent<Wire>().isMoving = true;

                    }
                }
            }

            if (transform.parent!=null && transform.parent.tag=="Breadboard"  && collision.transform.parent.tag != "Breadboard grid")
            {
                //transform.parent.SetParent(null);
            }

           
        }
    }

    public void SolderParent()
    {
        if (isConnectedToComponent)
        {
            isConnectedToComponent = false;
            if (((transform.parent.parent == null) || (transform.parent.parent != null && transform.parent.parent.tag == "Breadboard")) 
                && collision.transform.parent.parent == null)
            {
                GameObject newSoldered = Instantiate<GameObject>(soldered);
                newSoldered.transform.position = transform.position;
                transform.parent.SetParent(newSoldered.transform);
                collision.transform.parent.SetParent(newSoldered.transform);
            }
            else if (transform.parent.parent != null && transform.parent.parent.tag == "soldered" && collision.transform.parent.parent == null)
            {
                collision.transform.parent.SetParent(transform.parent.parent);
                if(transform.parent.parent.parent != null && transform.parent.parent.parent.tag == "Breadboard")
                {
                    transform.parent.parent.parent = null;
                }

            }
            else if (collision.transform.parent.parent != null && collision.transform.parent.parent.tag == "soldered"
                && (transform.parent.parent == null || (transform.parent.parent != null && transform.parent.parent.tag == "Breadboard")))
            {
                transform.parent.SetParent(collision.transform.parent.parent);
            }
            else if (collision.transform.parent.parent != null && collision.transform.parent.parent.tag == "soldered" && transform.parent.parent != null && transform.parent.parent.tag == "soldered")
            {
                Drag[] connecteds = transform.parent.parent.GetComponentsInChildren<Drag>();
                GameObject currentParent = transform.parent.parent.gameObject;
                for (int i = 0; i < connecteds.Length; i++)
                {
                    connecteds[i].transform.SetParent(collision.transform.parent.parent);
                }
                if (currentParent.GetComponentsInChildren<Drag>().Length == 0)
                {
                    Destroy(currentParent);
                }
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "node" && /*GetComponentInParent<Drag>().isDraggin == true*/ needSnapping)
        {
            needSoldering = false;
            if (collision.transform.parent.tag == "Breadboard grid")
            {
                isConnectedToBreadboard = false;

                
            }
            else
            {
                isConnectedToComponent = false;

                collision.GetComponentInParent<Drag>().connecteds.Remove(GetComponentInParent<Drag>());
                GetComponentInParent<Drag>().connecteds.Remove(collision.GetComponentInParent<Drag>());
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
