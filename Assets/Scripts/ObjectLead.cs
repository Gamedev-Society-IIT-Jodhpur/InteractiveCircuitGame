using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class ObjectLead : MonoBehaviour
{
    // Start is called before the first frame update
    public Resistor resistor;
    
    void Awake()
    {
        
        
        resistor=CIrcuitSim.sim.Create<Resistor>(5000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
