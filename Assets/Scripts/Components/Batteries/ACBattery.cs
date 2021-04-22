using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class ACBattery : MonoBehaviour
{
    // Start is called before the first frame update
    public ACVoltageSource ACVolt;
    void Awake()
    {// instantiating 2 lead AC voltage source
        ACVolt=CIrcuitSim.sim.Create<ACVoltageSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
