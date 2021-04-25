using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class DiodeComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public DiodeElm diode;
    public GameObject  CircuitSim;
    public CircuitSim Sim;
    void Awake()
    {
        CircuitSim= GameObject.FindGameObjectWithTag("CircuitSim");
        Sim=CircuitSim.GetComponent<CircuitSim>();

       diode=Sim.sim.Create<DiodeElm>(); 
    }

    // Update is called once per frame
    
}
