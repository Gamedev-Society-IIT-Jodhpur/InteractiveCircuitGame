using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class VoltageEleme : MonoBehaviour
{
    // Start is called before the first frame update
    public VoltageInput voltage;
    
 public GameObject  CIrcuitSim;
    public CIrcuitSim Sim;
    void Awake()
    {
        CIrcuitSim= GameObject.FindGameObjectWithTag("CIrcuitSim");
        Sim=CIrcuitSim.GetComponent<CIrcuitSim>();

        voltage = Sim.sim.Create<VoltageInput>(Voltage.WaveType.DC);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
