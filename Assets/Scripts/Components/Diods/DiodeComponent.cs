using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class DiodeComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public DiodeElm diode;
    public GameObject  CIrcuitSim;
    public CIrcuitSim Sim;
    void Awake()
    {
        CIrcuitSim= GameObject.FindGameObjectWithTag("CIrcuitSim");
        Sim=CIrcuitSim.GetComponent<CIrcuitSim>();

       diode=Sim.sim.Create<DiodeElm>(); 
    }

    // Update is called once per frame
    
}
