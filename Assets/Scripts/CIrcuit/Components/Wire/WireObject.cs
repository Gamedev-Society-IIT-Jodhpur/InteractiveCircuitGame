using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class WireObject : MonoBehaviour
{
    public Wire wire;
    GameObject CircuitSim;
    CircuitSim Sim;

    void Start()
    {
        CircuitSim = GameObject.FindGameObjectWithTag("CircuitSim");
        Sim = CircuitSim.GetComponent<CircuitSim>();
        wire = Sim.sim.Create<Wire>();
    }
    


}
