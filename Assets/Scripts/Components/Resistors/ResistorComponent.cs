using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class ResistorComponent : MonoBehaviour
{
    
    public Resistor resistor;
    

    /// <summary>
    /// value of resistor 
    /// </summary>
    public float resistorValue = 1000;
public GameObject  CIrcuitSim;
    public CIrcuitSim Sim;
    void Awake()
    {
        CIrcuitSim= GameObject.FindGameObjectWithTag("CIrcuitSim");
        Sim=CIrcuitSim.GetComponent<CIrcuitSim>();

        resistor = Sim.sim.Create<Resistor>(resistorValue);
    }
}
