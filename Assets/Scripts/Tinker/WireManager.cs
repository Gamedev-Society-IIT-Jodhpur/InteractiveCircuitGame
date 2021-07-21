using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireManager : MonoBehaviour
{
    [SerializeField] GameObject wire;
    [SerializeField] GameObject newWireManager;
    public static bool isDrawingWire=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void DrawWire(Transform node)
    {
        isDrawingWire = true;
        GameObject newWire = Instantiate<GameObject>(wire);
        newWire.GetComponent<Wire>().node1 = node;
        GameObject newNewWireManager = Instantiate<GameObject>(newWireManager);
        newNewWireManager.transform.SetParent(gameObject.transform);
        newNewWireManager.GetComponent<NewWireManager>().nodes.Add(node);
        newWire.transform.SetParent(newNewWireManager.transform);
        node.GetComponent<NodeTinker>().wires.Add(newWire);
        

    }
}
