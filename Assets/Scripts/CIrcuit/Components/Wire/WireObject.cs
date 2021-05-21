using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class WireObject : MonoBehaviour
{
    public Wire wire;
    [SerializeField]GameObject CircuitSim;
    CircuitSim Sim;

    private void Awake()
    {
        CircuitSim = GameObject.FindGameObjectWithTag("CircuitSim");
        Sim = CircuitSim.GetComponent<CircuitSim>();
        wire = Sim.sim.Create<Wire>();
        //print(Sim.sim);
    }
    


}
