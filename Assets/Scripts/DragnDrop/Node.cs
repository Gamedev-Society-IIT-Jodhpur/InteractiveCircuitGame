using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    //[SerializeField] GameObject wireComponent;
    //GameObject newWire;
    //SimArea simArea;
    //GameObject CircuitSim;
    //CircuitSim Sim;
    //[SerializeField] GameObject wireChild;

    void Start()
    {
        //simArea = GetComponentInParent<SimArea>();
        //CircuitSim = GameObject.FindGameObjectWithTag("CircuitSim");
        //Sim = CircuitSim.GetComponent<CircuitSim>();
        //wireChild.SetActive(false);
    }

    /*public void StartDrawingWire()
    {
        newWire = Instantiate(wireComponent, gameObject.transform.position,Quaternion.identity);
        newWire.transform.SetParent(simArea.transform);
        newWire.transform.localScale = simArea.transform.localScale;
        newWire.transform.SetAsFirstSibling();
        newWire.GetComponent<WireComponent>().Draw();
        wireChild.SetActive(true);
        Sim.sim.Connect(newWire.GetComponent<WireObject>().wire.leadIn, wireChild.GetComponent<WireObject>().wire.leadOut);

    }*/
}
