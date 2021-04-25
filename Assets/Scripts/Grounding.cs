using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class Grounding : MonoBehaviour
{
    // Start is called before the first frame update
    public Ground ground;
    public GameObject  CircuitSim;
    public CircuitSim Sim;
    void Awake()
    {
        CircuitSim= GameObject.FindGameObjectWithTag("CircuitSim");
        Sim=CircuitSim.GetComponent<CircuitSim>();

        ground = Sim.sim.Create<Ground>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
