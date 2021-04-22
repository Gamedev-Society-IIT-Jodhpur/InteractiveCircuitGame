using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class DiodeComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public DiodeElm diode;
    void Awake()
    {
       diode=CIrcuitSim.sim.Create<DiodeElm>(); 
    }

    // Update is called once per frame
    
}
