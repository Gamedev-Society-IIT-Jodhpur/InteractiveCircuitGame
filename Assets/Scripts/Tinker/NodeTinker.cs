using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeTinker : MonoBehaviour
{
    [SerializeField]GameObject wireManager;
    public List<GameObject> wires;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "node" && GetComponentInParent<Drag>().isDraggin == true)
        {
            GetComponentInParent<Drag>().Snap(collision.transform.position, gameObject.transform);
        }
    }

    private void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().enabled=true;
        if (Input.GetMouseButtonDown(0) && !WireManager.isDrawingWire)
        {
            wireManager.GetComponent<WireManager>().DrawWire(gameObject.transform);
        }
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

}
