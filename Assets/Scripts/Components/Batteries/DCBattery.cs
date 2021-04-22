using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class DCBattery : MonoBehaviour
{
    
    public VoltageInput DCVolt;

    void Awake()
    {
        DCVolt = CIrcuitSim.sim.Create<VoltageInput>(Voltage.WaveType.DC);
    }

}
