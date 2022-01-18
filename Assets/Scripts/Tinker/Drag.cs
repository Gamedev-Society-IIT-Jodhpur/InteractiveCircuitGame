using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{
    Vector2 worldPoint;
    public bool isDraggin;
    float prevX;
    float prevY;
    float prevCursorX;
    float prevCursorY;
    float x;
    float y;
    //float dragThreshold = 0.01f;
    //float diffX;
    //float diffY;
    NodeTinker[] nodes;
    [SerializeField] bool hasInitiated;
    public Vector3 previousPos;
    bool hasTrulyInitiated;

    //List<GameObject> wires;

    // Start is called before the first frame update
    void Start()
    {
        hasInitiated = false;
        hasTrulyInitiated = false;
        isDraggin = true;
        worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = new Vector3(worldPoint.x, worldPoint.y, transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!hasInitiated && !StaticData.isSoldering)
        {
            if (Input.GetMouseButtonDown(0) && !IsMouseOverUI())
            {
                hasInitiated = true;
            }
            if (!hasInitiated && Input.GetKeyDown(KeyCode.Escape))
            {
                AssetManager.deleteButton.Delete();
            }
        }


        if (((isDraggin && Input.GetMouseButton(0)) || !hasInitiated) && !IsMouseOverUI() && !StaticData.isSoldering)
        {
            x = worldPoint.x;
            y = worldPoint.y;


            if (Vector2.Distance(new Vector2(x, y), new Vector2(prevCursorX, prevCursorY)) >= StaticData.dragThreshold)
            {
                if ((x - prevCursorX) >= StaticData.dragThreshold)
                {
                    prevX += (x - prevCursorX);
                    prevCursorX = x;
                    StaticData.dragThreshold = 0.01f;
                }
                else if (prevCursorX - x >= StaticData.dragThreshold)
                {
                    prevX -= (prevCursorX - x);
                    prevCursorX = x;
                    StaticData.dragThreshold = 0.01f;
                }
                if ((y - prevCursorY) >= StaticData.dragThreshold)
                {
                    prevY += (y - prevCursorY);
                    prevCursorY = y;
                    StaticData.dragThreshold = 0.01f;
                }
                else if (prevCursorY - y >= StaticData.dragThreshold)
                {
                    prevY -= (prevCursorY - y);
                    prevCursorY = y;
                    StaticData.dragThreshold = 0.01f;
                }

                if (transform.parent != null && transform.parent.tag == "soldered")
                {
                    gameObject.transform.parent.position = new Vector3(prevX, prevY, gameObject.transform.parent.position.z);
                }
                else
                {
                    gameObject.transform.position = new Vector3(prevX, prevY, gameObject.transform.position.z);
                }

                //gameObject.transform.position = new Vector3(prevX, prevY, gameObject.transform.position.z);

            }


        }
        else if (isDraggin && hasInitiated)
        {
            isDraggin = (false);

            if (transform.parent != null && transform.parent.tag == "soldered")
            {
                nodes = transform.parent.GetComponentsInChildren<NodeTinker>();

            }
            else
            {
                nodes = GetComponentsInChildren<NodeTinker>();
            }

            bool isConnectedToBreadboard = false;

            foreach (NodeTinker node in nodes)
            {
                foreach (GameObject wire in node.wires)
                {
                    wire.GetComponent<Wire>().isMoving = false;

                }

                node.needSnapping = false;

                if (node.needSoldering)
                {
                    node.needSoldering = false;
                    if (node.isConnectedToComponent && !node.isConnectedToBreadboard)
                    {
                        if (StaticData.isSolderingIron)
                        {
                            AssetManager.solderingIronIcon.Solder(node.transform.position);
                            node.SolderParent();

                            //need to update nodes list after solder parent is called.
                            if (transform.parent != null && transform.parent.tag == "soldered")
                            {
                                nodes = transform.parent.GetComponentsInChildren<NodeTinker>();

                            }
                            else
                            {
                                nodes = GetComponentsInChildren<NodeTinker>();
                            }
                        }
                        else if (hasTrulyInitiated)
                        {
                            SnapBack();
                            CustomNotificationManager.Instance.AddNotification(2, "Soldering iron isn't available");
                            //print("soldering iron is not available");
                        }
                        else
                        {
                            AssetManager.deleteButton.DeleteComponent(gameObject);
                            CustomNotificationManager.Instance.AddNotification(2, "Soldering iron isn't available");
                            //print("soldering iron is not available");
                        }
                    }
                }
            }

            //new loop with updated nodes list.
            foreach (NodeTinker node in nodes)
            {
                if (/*(transform.parent != null && transform.parent.tag == "Breadboard")||(transform.parent != null && transform.parent.tag == "soldered" 
                    && transform.parent.parent != null && transform.parent.parent.tag == "Breadboard")*/
                    transform.GetComponentInParent<Breadboard>())
                {
                    node.GetRaycastHits();
                    foreach (var hit in node.hits)
                    {
                        if (!isConnectedToBreadboard)
                        {
                            if (hit.transform.parent != null && hit.transform.parent.tag == "Breadboard grid")
                            {
                                isConnectedToBreadboard = true;
                                break;
                            }

                        }
                    }
                }
            }

            if (transform.parent != null && transform.parent.tag == "Breadboard" && !isConnectedToBreadboard)
            {
                transform.parent = null;
            }
            else if (transform.parent != null && transform.parent.tag == "soldered" && transform.parent.parent != null &&
                transform.parent.parent.tag == "Breadboard" && !isConnectedToBreadboard)
            {
                transform.parent.parent = null;
            }
            hasTrulyInitiated = true;
            StaticData.dragThreshold = 0.01f;
        }


    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !isDraggin && !WireManager.isDrawingWire && !StaticData.isSoldering)
        {
            worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDraggin = (true);
            if (transform.parent != null && transform.parent.tag == "soldered")
            {
                nodes = transform.parent.GetComponentsInChildren<NodeTinker>();

            }
            else
            {
                nodes = GetComponentsInChildren<NodeTinker>();
            }
            foreach (NodeTinker node in nodes)
            {
                foreach (GameObject wire in node.wires)
                {
                    wire.GetComponent<Wire>().isMoving = true;

                }

                node.needSnapping = true;
            }

            if (transform.parent != null && transform.parent.tag == "soldered")
            {
                prevX = gameObject.transform.parent.position.x;
                prevY = gameObject.transform.parent.position.y;
            }
            else
            {
                prevX = gameObject.transform.position.x;
                prevY = gameObject.transform.position.y;
            }

            //prevX = gameObject.transform.position.x;
            //prevY = gameObject.transform.position.y;
            prevCursorX = worldPoint.x;
            prevCursorX = worldPoint.x;
            prevCursorY = worldPoint.y;
            //diffX = prevX - prevCursorX;
            //diffY = prevY - prevCursorY;
            CircuitManagerTinker.ChangeSelected(gameObject);
            previousPos = transform.position;

        }
    }





    public void Snap(Vector3 snapPos, Transform childPos)
    {
        if (transform.parent != null && transform.parent.tag == "soldered")
        {
            nodes = transform.parent.GetComponentsInChildren<NodeTinker>();

        }
        else
        {
            nodes = GetComponentsInChildren<NodeTinker>();
        }

        if (transform.parent != null && transform.parent.tag == "soldered")
        {
            x = gameObject.transform.parent.position.x - childPos.position.x;
            y = gameObject.transform.parent.position.y - childPos.position.y;
            prevX = snapPos.x + x;
            prevY = snapPos.y + y;
            gameObject.transform.parent.position = new Vector3(prevX, prevY, gameObject.transform.parent.position.z);
        }
        else
        {
            x = gameObject.transform.position.x - childPos.position.x;
            y = gameObject.transform.position.y - childPos.position.y;
            prevX = snapPos.x + x;
            prevY = snapPos.y + y;
            gameObject.transform.position = new Vector3(prevX, prevY, gameObject.transform.position.z);
        }
        //print('x');
        foreach (NodeTinker node in nodes)
        {
            foreach (GameObject wire in node.wires)
            {
                wire.GetComponent<Wire>().ResetWirePos();

            }
        }

        //gameObject.transform.position = new Vector3(prevX, prevY, gameObject.transform.position.z);
        StaticData.dragThreshold = childPos.localScale.x * 1.33f;
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void SnapBack()
    {
        transform.position = previousPos;
    }
}

