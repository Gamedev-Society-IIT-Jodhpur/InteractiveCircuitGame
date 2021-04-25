using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class Node : MonoBehaviour
{
    [SerializeField] GameObject wireComponent;
    GameObject newWire;
    SimArea simArea;
    Wire wire;
    Circuit circuit;

    // Start is called before the first frame update
    void Awake()
    {
        circuit = new Circuit();
        simArea = GetComponentInParent<SimArea>();
        wire = circuit.Create<Wire>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDrawingWire()
    {
        newWire = Instantiate(wireComponent, gameObject.transform.position,Quaternion.identity);
        newWire.transform.SetParent(simArea.transform);
        newWire.transform.localScale = simArea.transform.localScale;
        newWire.transform.SetAsFirstSibling();
        newWire.GetComponent<WireComponent>().Draw();
    }
}
