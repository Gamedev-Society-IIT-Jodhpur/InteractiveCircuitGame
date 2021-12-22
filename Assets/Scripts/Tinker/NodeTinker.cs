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
    public List<BreakSolder> nodeConnected;
    GameObject[] soldereds;
    public RaycastHit2D[] hits;
    //Stack<Transform> prevSoldered;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        wireManager = AssetManager.wireManager;
        soldered = AssetManager.soldered;
        needSnapping = true;
        if (transform.parent.tag != "Breadboard grid")
        {
            nodeConnected.Add(GetComponentInParent<BreakSolder>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "node" && needSnapping && transform.parent.tag != "Breadboard grid")
        {


            if (collision.transform.parent.tag == "Breadboard grid")
            {
                GetComponentInParent<Drag>().Snap(collision.transform.position, gameObject.transform);
                isConnectedToBreadboard = true;
            }
            else if (collision.GetComponentInParent<Breadboard>())
            {
                GetComponentInParent<Drag>().Snap(collision.transform.position, gameObject.transform);
                isConnectedToComponent = true;

                collision.GetComponentInParent<BreakSolder>().connecteds.Add(GetComponentInParent<BreakSolder>());
                GetComponentInParent<BreakSolder>().connecteds.Add(collision.GetComponentInParent<BreakSolder>());

                nodeConnected.Add(collision.GetComponentInParent<BreakSolder>());
                collision.GetComponent<NodeTinker>().nodeConnected.Add(GetComponentInParent<BreakSolder>());
                //print(transform.parent.name);
            }
            else
            {
                collision.GetComponentInParent<Drag>().Snap(transform.position, collision.transform);
                isConnectedToComponent = true;

                collision.GetComponentInParent<BreakSolder>().connecteds.Add(GetComponentInParent<BreakSolder>());
                GetComponentInParent<BreakSolder>().connecteds.Add(collision.GetComponentInParent<BreakSolder>());

                nodeConnected.Add(collision.GetComponentInParent<BreakSolder>());
                collision.GetComponent<NodeTinker>().nodeConnected.Add(GetComponentInParent<BreakSolder>());

            }
            this.collision = collision.transform;

            if (transform.parent.tag != "Breadboard grid") //should not solder when we drag the breadboard.
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

            }

            if (transform.parent != null && transform.parent.tag == "Breadboard" && collision.transform.parent.tag != "Breadboard grid")
            {
                //transform.parent.SetParent(null);
            }


        }
        else if (collision.tag == "node" && collision.transform.parent.tag != "Breadboard grid" && needSnapping && transform.parent.tag == "Breadboard grid")
        {

            collision.GetComponentInParent<Drag>().Snap(transform.position, collision.transform);
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

    }

    public void SolderParent() //makes all the soldered components childs of same parent
    {
        if (isConnectedToComponent)
        {
            isConnectedToComponent = false;
            Transform prevParent;
            if (!collision.GetComponentInParent<Breadboard>())
            {
                prevParent = transform.parent.parent;
                if (((transform.parent.parent == null) || (transform.parent.parent != null && transform.parent.parent.tag != "soldered"))
                    && collision.transform.parent.parent == null)
                {

                    GameObject newSoldered = Instantiate<GameObject>(soldered);
                    newSoldered.transform.position = transform.position;
                    transform.parent.SetParent(newSoldered.transform);
                    collision.transform.parent.SetParent(newSoldered.transform);
                    newSoldered.transform.SetParent(prevParent);
                }
                else if (transform.parent.parent != null && transform.parent.parent.tag == "soldered" && collision.transform.parent.parent == null)
                {
                    collision.transform.parent.SetParent(transform.parent.parent);
                    if (transform.parent.parent.parent != null && transform.parent.parent.parent.tag == "Breadboard")
                    {
                        // transform.parent.parent.parent = null;
                    }

                }
                else if (collision.transform.parent.parent != null && collision.transform.parent.parent.tag == "soldered"
                    && (transform.parent.parent == null || (transform.parent.parent != null && transform.parent.parent.tag == "Breadboard")))
                {
                    transform.parent.SetParent(collision.transform.parent.parent);
                    collision.transform.parent.parent.SetParent(prevParent);
                }
                else if (collision.transform.parent.parent != null && collision.transform.parent.parent.tag == "soldered" && transform.parent.parent != null && transform.parent.parent.tag == "soldered")
                {
                    prevParent = transform.parent.parent.parent;
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
                    collision.transform.parent.parent.SetParent(prevParent);

                }
            }
            // to attatch a component with other component with 1 node connected to breadboard.
            else if (collision.GetComponentInParent<Breadboard>())
            {
                prevParent = collision.transform.parent.parent;

                if (collision.transform.parent.parent.tag != "soldered" && transform.parent.parent == null)
                {
                    GameObject newSoldered = Instantiate<GameObject>(soldered);
                    newSoldered.transform.position = transform.position;
                    newSoldered.transform.SetParent(prevParent);
                    collision.transform.parent.SetParent(newSoldered.transform);
                    transform.parent.SetParent(newSoldered.transform);
                }
                else if (transform.parent.parent != null && transform.parent.parent.tag == "soldered" && collision.transform.parent.parent.tag != "soldered")
                {
                    collision.transform.parent.SetParent(transform.parent.parent);
                    transform.parent.parent.SetParent(prevParent);
                }
                else if (collision.transform.parent.parent != null && collision.transform.parent.parent.tag == "soldered"
                && transform.parent.parent == null)
                {
                    transform.parent.SetParent(collision.transform.parent.parent);
                }
                else if (collision.transform.parent.parent != null && collision.transform.parent.parent.tag == "soldered" &&
                    transform.parent.parent != null && transform.parent.parent.tag == "soldered")
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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "node" && needSnapping && transform.parent.tag != "Breadboard grid")
        {
            needSoldering = false;
            if (collision.transform.parent.tag == "Breadboard grid")
            {
                isConnectedToBreadboard = false;


            }
            else
            {
                isConnectedToComponent = false;

                collision.GetComponentInParent<BreakSolder>().connecteds.Remove(GetComponentInParent<BreakSolder>());
                GetComponentInParent<BreakSolder>().connecteds.Remove(collision.GetComponentInParent<BreakSolder>());

                nodeConnected.Remove(collision.GetComponentInParent<BreakSolder>());
                collision.GetComponent<NodeTinker>().nodeConnected.Remove(GetComponentInParent<BreakSolder>());
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
                if (AssetManager.isSolderingIron && transform.parent.tag != "Breadboard grid")
                {
                    AssetManager.solderingIronIcon.Solder(transform.position);
                    wireManager.GetComponent<WireManager>().DrawWire(gameObject.transform);
                }
                else if (transform.parent.tag != "Breadboard grid")
                {
                    CustomNotificationManager.Instance.AddNotification(2, "Soldering iron isn't available");
                }
                if (transform.parent.tag == "Breadboard grid")
                {
                    wireManager.GetComponent<WireManager>().DrawWire(gameObject.transform);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                BreakSoldered();
            }
        }
    }

    public void BreakSoldered()
    {
        if (transform.parent.parent != null && transform.parent.parent.tag == "soldered")
        {
            CustomNotificationManager.Instance.AddNotification(1, "Breaking solder costs XP");
            GameObject currentParent = nodeConnected[0].transform.parent.gameObject;
            for (int i = 0; i < nodeConnected.Count; i++)
            {
                for (int j = 0; j < nodeConnected.Count; j++)
                {
                    nodeConnected[i].connecteds.Remove(nodeConnected[j]);
                }

                nodeConnected[i].Break();
            }
            Destroy(currentParent);
            CheckSoldered();
        }

    }

    public void CheckSoldered() //to ensure soldered parent has more than 1 child
    {
        soldereds = GameObject.FindGameObjectsWithTag("soldered");
        for (int i = soldereds.Length - 1; i >= 0; i--)
        {
            Drag[] childs = soldereds[i].GetComponentsInChildren<Drag>();
            if (childs.Length == 1)
            {
                childs[0].transform.SetParent(null);
                Destroy(soldereds[i]);
            }
            else if (childs.Length == 0)
            {
                Destroy(soldereds[i]);
            }
        }
    }

    public void GetRaycastHits()
    {
        hits = Physics2D.RaycastAll(transform.position, Vector2.zero);
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