using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class DCBattery : MonoBehaviour
{
    
    public DCVoltageSource DCVolt;
     

public GameObject  CIrcuitSim;
    public CIrcuitSim Sim;
    void Awake()
    {
        CIrcuitSim= GameObject.FindGameObjectWithTag("CIrcuitSim");
        Sim=CIrcuitSim.GetComponent<CIrcuitSim>();

        DCVolt = Sim.sim.Create<DCVoltageSource>(10);
    }

}
