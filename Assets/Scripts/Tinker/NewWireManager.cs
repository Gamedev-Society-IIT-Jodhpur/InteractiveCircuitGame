using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWireManager : MonoBehaviour
{
    public List<Transform> nodes;
    [SerializeField] GameObject wireNode;
    GameObject newWireNode1;
    public GameObject newWireNode2;
    Transform[] childs;

    private void Start()
    {
        newWireNode1 = Instantiate<GameObject>(wireNode);
        newWireNode1.transform.position = nodes[0].position;
        newWireNode1.transform.SetParent(nodes[0]);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            DestroyWire();
        }
    }


    public void DestroyWire()
    {
        childs = GetComponentsInChildren<Transform>();
        WireManager.isDrawingWire = false;
        nodes[0].GetComponent<NodeTinker>().wires.Remove(childs[1].gameObject);
        if (newWireNode2 != null)
        {
            nodes[nodes.Count - 1].GetComponent<NodeTinker>().wires.Remove(childs[childs.Length - 1].gameObject);
            Destroy(newWireNode2);
        }
        Destroy(newWireNode1);
        Destroy(gameObject);
    }
}
