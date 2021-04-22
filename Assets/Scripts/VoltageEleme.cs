using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class VoltageEleme : MonoBehaviour
{
    // Start is called before the first frame update
    public VoltageInput voltage;
    
    void Awake()
    {
        voltage = CIrcuitSim.sim.Create<VoltageInput>(Voltage.WaveType.DC);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
