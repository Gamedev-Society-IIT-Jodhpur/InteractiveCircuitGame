using UnityEngine;
using UnityEngine.EventSystems;

public class Wire : MonoBehaviour
{
    [SerializeField] GameObject wireNode;
    [SerializeField] GameObject wire;
    Vector2 mousePos;
    Vector2 node2Pos;
    bool isDrawing;
    public bool isMoving = false;
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
        GetComponent<BoxCollider2D>().enabled = false;

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3((mousePos.x + node1.transform.position.x) / 2, (mousePos.y + node1.transform.position.y) / 2, 0);
        distance = Vector2.Distance(mousePos, node1.transform.position);
        transform.localScale = new Vector3(distance, transform.localScale.y, transform.localScale.z);
        //transform.localScale =new Vector3(transform.localScale.x,distance/2,transform.localScale.z);

        angleVector = new Vector2(node1.transform.position.x - mousePos.x, node1.transform.position.y - mousePos.y);

        if (angleVector.x != 0 || angleVector.y != 0)
        {
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

    // Update is called once per frame
    void Update()
    {
        if (isDrawing)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3((mousePos.x + node1.transform.position.x) / 2, (mousePos.y + node1.transform.position.y) / 2, 0);
            distance = Vector2.Distance(mousePos, node1.transform.position);
            transform.localScale = new Vector3(distance, transform.localScale.y, transform.localScale.z);
            //transform.localScale =new Vector3(transform.localScale.x,distance/2,transform.localScale.z);

            angleVector = new Vector2(node1.transform.position.x - mousePos.x, node1.transform.position.y - mousePos.y);

            if (angleVector.x != 0 || angleVector.y != 0)
            {
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

            if (Input.GetKeyDown(KeyCode.Escape) && !StaticData.isSoldering)
            {
                GetComponentInParent<NewWireManager>().DestroyWire();
                if (StaticData.isSoldering)
                {
                    AssetManager.solderingIronIcon.DestroySolderingIron();
                }
            }


            if (Input.GetMouseButtonDown(0) && !StaticData.isSoldering && !IsMouseOverUI())
            {
                hit = Physics2D.Raycast(mousePos, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject.tag == "node" && hit.collider.gameObject != GetComponentInParent<NewWireManager>().nodes[0].gameObject)
                {

                    if (hit.transform.parent.tag == "Breadboard grid" || StaticData.isSolderingIron)
                    {
                        if (hit.transform.parent.tag != "Breadboard grid" && StaticData.isSolderingIron)
                        {
                            AssetManager.solderingIronIcon.Solder(hit.transform.position);
                            //AssetManager.solderingIronIcon.HideCursor();
                        }

                        isDrawing = false;
                        GetComponent<BoxCollider2D>().enabled = true;
                        WireManager.isDrawingWire = false;
                        GetComponentInParent<NewWireManager>().nodes.Add(hit.collider.transform);
                        GetComponentInParent<NewWireManager>().node2 = Instantiate<GameObject>(wireNode);
                        GetComponentInParent<NewWireManager>().node2.transform.position = hit.collider.transform.position;
                        GetComponentInParent<NewWireManager>().node2.transform.SetParent(hit.collider.transform);
                        node2 = hit.collider.transform;
                        hit.collider.GetComponent<NodeTinker>().wires.Add(gameObject);

                        mousePos = hit.collider.transform.position;
                        transform.position = new Vector3((mousePos.x + node1.transform.position.x) / 2, (mousePos.y + node1.transform.position.y) / 2, 0);
                        distance = Vector2.Distance(mousePos, node1.transform.position);
                        transform.localScale = new Vector3(distance, transform.localScale.y, transform.localScale.z);
                        //transform.localScale = new Vector3(transform.localScale.x, distance/2, transform.localScale.z);

                        angleVector = new Vector2(node1.transform.position.x - mousePos.x, node1.transform.position.y - mousePos.y);
                        if (angleVector.x != 0 || angleVector.y != 0)
                        {
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
                    else
                    {
                        CustomNotificationManager.Instance.AddNotification(2, "Soldering iron isn't available");
                        //print("there is no soldering iron");
                    }



                }

                else if ((hit.collider == null) || (hit.collider.tag != "node"))
                {
                    node2 = Instantiate<GameObject>(wireNode).transform;
                    node2.transform.position = mousePos/*+new Vector2(0,0.0001f)*/;
                    node2.transform.SetParent(transform.parent);
                    isDrawing = false;
                    GetComponentInParent<NewWireManager>().nodes.Add(node2);
                    GameObject newWire = Instantiate<GameObject>(wire);
                    newWire.GetComponent<Wire>().node1 = node2;
                    newWire.transform.SetParent(gameObject.transform.parent);
                    GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }

        if (isMoving)
        {
            ResetWirePos();
        }

    }

    public void ResetWirePos()
    {
        node2Pos = node2.transform.position;
        transform.position = new Vector3((node2Pos.x + node1.transform.position.x) / 2, (node2Pos.y + node1.transform.position.y) / 2, 0);
        distance = Vector2.Distance(node2Pos, node1.transform.position);
        transform.localScale = new Vector3(distance, transform.localScale.y, transform.localScale.z);

        angleVector = new Vector2(node1.transform.position.x - node2Pos.x, node1.transform.position.y - node2Pos.y);


        if (angleVector.x != 0 || angleVector.y != 0)
        {
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



    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
