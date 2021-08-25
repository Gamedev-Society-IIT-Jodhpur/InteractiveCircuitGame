using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWireManager : MonoBehaviour
{
    public List<Transform> nodes;
    [SerializeField] GameObject wireNode;
    GameObject node1;
    public GameObject node2;
    Transform[] childs;

    private void Start()
    {
        node1 = Instantiate<GameObject>(wireNode);
        node1.transform.position = nodes[0].position;
        node1.transform.SetParent(nodes[0]);
    }


    public void DestroyWire()
    {
        childs = GetComponentsInChildren<Transform>();
        WireManager.isDrawingWire = false;
        nodes[0].GetComponent<NodeTinker>().wires.Remove(childs[1].gameObject);
        if (node2 != null)
        {
            nodes[nodes.Count - 1].GetComponent<NodeTinker>().wires.Remove(childs[childs.Length - 1].gameObject);
            Destroy(node2);
        }
        Destroy(node1);
        Destroy(gameObject);
    }
}
