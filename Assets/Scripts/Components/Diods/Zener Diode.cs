using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class ZenerDiode : MonoBehaviour
{
    // Start is called before the first frame update
    public ZenerElm zener;
    public GameObject  CircuitSim;
    public CircuitSim Sim;
    void Awake()
    {
        CircuitSim= GameObject.FindGameObjectWithTag("CircuitSim");
        Sim=CircuitSim.GetComponent<CircuitSim>();

        zener=Sim.sim.Create<ZenerElm>();
    }

    // Update is called once per frame
    
}
