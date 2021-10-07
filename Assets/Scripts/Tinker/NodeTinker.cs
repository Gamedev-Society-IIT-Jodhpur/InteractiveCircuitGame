using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeTinker : MonoBehaviour
{
    GameObject wireManager;
    public List<GameObject> wires;
    NodeTinker[] nodes;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        wireManager = AssetManager.wireManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "node" && GetComponentInParent<Drag>().isDraggin == true)
        {
            GetComponentInParent<Drag>().Snap(collision.transform.position, gameObject.transform);

            if (collision.transform.parent.parent!=null && collision.transform.parent.parent.tag == "Breadboard")
            {
                transform.parent.SetParent(collision.transform.parent.parent);


            }
            else if (transform.parent.parent!=null && transform.parent.parent.tag== "Breadboard")
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

            if (transform.parent!=null && collision.transform.parent.parent!=null && collision.transform.parent.parent.tag != "Breadboard")
            {
                transform.parent.SetParent(null);
            }
        }
    }

    private void OnMouseOver()
    {
        if (!IsMouseOverUI())
        {
            GetComponent<SpriteRenderer>().enabled = true;
            if (Input.GetMouseButtonDown(0) && !WireManager.isDrawingWire)
            {
                wireManager.GetComponent<WireManager>().DrawWire(gameObject.transform);
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
