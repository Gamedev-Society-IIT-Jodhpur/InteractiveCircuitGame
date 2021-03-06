using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragManager : MonoBehaviour
{
    [HideInInspector] public bool isDraggin = false;
    RaycastHit2D hit;
    Vector2 worldPoint;
    float prevX;
    float prevY;
    float prevCursorX;
    float prevCursorY;
    Transform[] childs;
    Vector2 child1Pos;
    Vector2 child2Pos;
    int currentChild;
    public int mode = 0;
    int x2, y2, x1, y1;
    GameObject newComponent;
    bool toDraw = true;
    [SerializeField] List<GameObject> newComponentPrefabs;
    GameObject toInstantiate;
    int axis = 0;
    bool forward = true;
    [SerializeField] TMP_Dropdown dropDown;
    public static bool isGizmoPresent = false;
    [SerializeField] ButtonManager buttonManager;


    private void Start()
    {
        isGizmoPresent = false;
    }

    void Update()
    {
        #region Keyboard Shortcuts
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mode = 0;
            dropDown.value = 0;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            mode = 1;
            dropDown.value = 1;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            mode = 1;
            dropDown.value = 2;
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            mode = 1;
            dropDown.value = 3;
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            mode = 1;
            dropDown.value = 4;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            mode = 1;
            dropDown.value = 5;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            mode = 1;
            dropDown.value = 6;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            mode = 1;
            dropDown.value = 7;
        }
        else if (Input.GetKeyDown(KeyCode.G) && !isGizmoPresent)
        {
            mode = 1;
            dropDown.value = 8;
        }
        else if (Input.GetKeyDown(KeyCode.Delete))
        {
            buttonManager.DeleteComponent();
        }
        #endregion


        worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!IsMouseOverUI())
        {
            if (mode == 0)
            {

                if (Input.GetMouseButtonDown(0))
                {

                    hit = Physics2D.Raycast(worldPoint, Vector2.zero);

                    if (hit.collider != null)
                    {
                        isDraggin = true;

                        if (hit.collider.gameObject.tag == "node")
                        {

                            hit.collider.gameObject.GetComponentInParent<Item>().isMoving = true;
                            CircuitManager.ChangeSelected(hit.collider.gameObject.transform.parent.gameObject);
                            childs = hit.collider.gameObject.transform.parent.gameObject.GetComponentsInChildren<Transform>();
                            child1Pos = childs[1].transform.position;
                            child2Pos = childs[2].transform.position;
                            if (Vector2.Distance(worldPoint, child1Pos) < Vector2.Distance(worldPoint, child2Pos))
                            {
                                currentChild = 1;
                            }
                            else
                            {
                                currentChild = 2;
                            }

                        }
                        else if (hit.collider.gameObject.tag == "bjt node")
                        {
                            hit.collider.transform.parent.gameObject.GetComponent<BJTItem>().isMoving = true;
                            CircuitManager.ChangeSelected(hit.collider.gameObject.transform.parent.gameObject);
                            childs = hit.collider.gameObject.transform.parent.gameObject.GetComponentsInChildren<Transform>();
                            child1Pos = childs[0].position;
                            child2Pos = childs[1].position;
                            if (child1Pos.x == child2Pos.x)
                            {
                                axis = 1;
                                if (child1Pos.y < child2Pos.y)
                                {
                                    forward = true;
                                }
                                else
                                {
                                    forward = false;
                                }
                            }
                            else
                            {
                                axis = 0;
                                if (child1Pos.x < child2Pos.x)
                                {
                                    forward = true;
                                }
                                else
                                {
                                    forward = false;
                                }
                            }

                        }
                        else
                        {
                            prevX = hit.collider.gameObject.transform.position.x;
                            prevY = hit.collider.gameObject.transform.position.y;
                            prevCursorX = worldPoint.x;
                            prevCursorY = worldPoint.y;
                            CircuitManager.ChangeSelected(hit.collider.gameObject);
                        }
                    }
                }
                if (isDraggin && Input.GetMouseButton(0))
                {
                    if (hit.collider.gameObject.tag == "node")
                    {
                        int x = Mathf.RoundToInt(worldPoint.x);
                        int y = Mathf.RoundToInt(worldPoint.y);


                        if (currentChild == 2)
                        {
                            if (Vector3.Distance(childs[1].transform.position, new Vector3(x, y, 0)) >= 2)
                            {
                                hit.collider.transform.position = new Vector3Int(x, y, 0);
                            }
                        }
                        else
                        {
                            if (Vector3.Distance(childs[2].transform.position, new Vector3(x, y, 0)) >= 2)
                            {
                                hit.collider.transform.position = new Vector3Int(x, y, 0);
                            }
                        }

                    }
                    else if (hit.collider.gameObject.tag == "bjt node")
                    {
                        if (axis == 0)
                        {
                            int x = Mathf.RoundToInt(worldPoint.x);
                            int y = Mathf.RoundToInt(hit.collider.transform.position.y);
                            if (forward)
                            {
                                if (x - childs[0].transform.position.x >= 0)
                                {
                                    hit.collider.transform.position = new Vector3Int(x, y, 0);
                                }

                            }
                            else
                            {
                                if (x - childs[0].transform.position.x <= 0)
                                {
                                    hit.collider.transform.position = new Vector3Int(x, y, 0);
                                }
                            }

                        }
                        else
                        {
                            int x = Mathf.RoundToInt(hit.collider.transform.position.x);
                            int y = Mathf.RoundToInt(worldPoint.y);
                            if (forward)
                            {
                                if (y - childs[0].transform.position.y >= 0)
                                {
                                    hit.collider.transform.position = new Vector3Int(x, y, 0);
                                }

                            }
                            else
                            {
                                if (y - childs[0].transform.position.y <= 0)
                                {
                                    hit.collider.transform.position = new Vector3Int(x, y, 0);
                                }
                            }

                        }

                    }
                    else
                    {
                        float x = worldPoint.x;
                        float y = worldPoint.y;

                        if (Vector2.Distance(new Vector2(x, y), new Vector2(prevCursorX, prevCursorY)) >= 1)
                        {
                            if ((x - prevCursorX) >= 1)
                            {
                                prevX += Mathf.RoundToInt(x - prevCursorX);
                                prevCursorX = x;
                            }
                            else if (prevCursorX - x >= 1)
                            {
                                prevX -= Mathf.RoundToInt(prevCursorX - x);
                                prevCursorX = x;
                            }
                            if ((y - prevCursorY) >= 1)
                            {
                                prevY += Mathf.RoundToInt(y - prevCursorY);
                                prevCursorY = y;
                            }
                            else if (prevCursorY - y >= 1)
                            {
                                prevY -= Mathf.RoundToInt(prevCursorY - y);
                                prevCursorY = y;
                            }

                            hit.collider.transform.position = new Vector3(prevX, prevY, 0);
                        }
                    }


                }
                else if (isDraggin)
                {
                    isDraggin = false;
                    if (hit.collider != null && hit.collider.gameObject.tag == "node")
                    {
                        hit.collider.gameObject.GetComponentInParent<Item>().isMoving = false;
                    }
                    else if (hit.collider != null && hit.collider.gameObject.tag == "bjt node")
                    {
                        hit.collider.gameObject.GetComponentInParent<BJTItem>().isMoving = false;
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    x1 = Mathf.RoundToInt(worldPoint.x);
                    y1 = Mathf.RoundToInt(worldPoint.y);
                    isDraggin = true;
                    if (toInstantiate.tag == "Gizmo" && isGizmoPresent)
                    {
                        mode = 0;
                        dropDown.value = 0;
                        isDraggin = false;
                        CustomNotificationManager.Instance.AddNotification(2, "Gizmo already present");

                        //print("one gizmo already present"); //TODO add notification.
                    }
                }
                if (isDraggin && Input.GetMouseButton(0))
                {
                    x2 = Mathf.RoundToInt(worldPoint.x);
                    y2 = Mathf.RoundToInt(worldPoint.y);
                    if ((Vector2.Distance(new Vector2(x1, y1), new Vector2(x2, y2))) >= 3.1f && toDraw)
                    {
                        toDraw = false;
                        newComponent = Instantiate<GameObject>(toInstantiate);
                        //print(newComponent.name);
                        CircuitManager.ChangeSelected(newComponent);
                        if (newComponent.tag == "BJT") newComponent.GetComponent<BJTItem>().isMoving = true;
                        else newComponent.GetComponent<Item>().isMoving = true;
                        childs = newComponent.GetComponentsInChildren<Transform>();
                        childs[1].transform.position = new Vector3(x1, y1, 0);
                        if (toInstantiate.tag == "Gizmo")
                        {
                            isGizmoPresent = true;
                            ValidateScript.gizmo = newComponent;
                        }

                    }
                    //for initializing bjt component
                    if (newComponent && newComponent.tag == "BJT")
                    {
                        int x = Mathf.RoundToInt(worldPoint.x);
                        int y = Mathf.RoundToInt(worldPoint.y);
                        if (Mathf.Abs(x1 - x) > Mathf.Abs(y1 - y))
                        {
                            if (x1 > x)
                            {
                                newComponent.transform.eulerAngles = new Vector3(0, 0, -90);
                                newComponent.transform.position = new Vector3(x + 0.5f, y1, 0);
                                childs[1].transform.position = new Vector3(x1, y1, 0);
                            }
                            else
                            {
                                newComponent.transform.eulerAngles = new Vector3(0, 0, 90);
                                newComponent.transform.position = new Vector3(x + 0.5f, y1, 0);
                                //print(childs.Length);
                                childs[1].transform.position = new Vector3(x1, y1, 0);
                            }
                        }
                        else
                        {
                            if (y1 > y)
                            {
                                newComponent.transform.eulerAngles = new Vector3(0, 0, 0);
                                newComponent.transform.position = new Vector3(x1, y + 0.5f, 0);
                                childs[1].transform.position = new Vector3(x1, y1, 0);
                            }
                            else
                            {
                                newComponent.transform.eulerAngles = new Vector3(0, 0, 180);
                                newComponent.transform.position = new Vector3(x1, y + 0.5f, 0);
                                childs[1].transform.position = new Vector3(x1, y1, 0);
                            }
                        }

                    }
                    //for initailizing other components
                    else if (newComponent)
                    {
                        int x = Mathf.RoundToInt(worldPoint.x);
                        int y = Mathf.RoundToInt(worldPoint.y);
                        if (Vector3.Distance(childs[1].transform.position, new Vector3(x, y, 0)) >= 3)
                        {
                            childs[2].transform.position = new Vector3(x, y, 0);
                        }

                    }
                }
                //to stop dragging after initialization
                else if (isDraggin)
                {
                    isDraggin = false;
                    if (newComponent && newComponent.tag == "BJT") newComponent.GetComponent<BJTItem>().isMoving = false;
                    else if (newComponent) newComponent.GetComponent<Item>().isMoving = false;
                    newComponent = null;
                    toDraw = true;
                }
            }
        }
    }

    //for dropdown menu
    public void DragMode(int n)
    {
        if (n == 0)
        {
            mode = 0;
        }
        /*else if (n==8 && isGizmoPresent)
        {
            print("one gizmo already present");  //TODO add notification
            mode = 0;
            dropDown.value = 0;
        }*/
        else
        {
            mode = 1;
            toInstantiate = newComponentPrefabs[n - 1];
        }

    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }


}
