using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class ACBattery : MonoBehaviour
{
    // Start is called before the first frame update
    public ACVoltageSource ACVolt;
    
   public GameObject  CircuitSim;
    public CircuitSim Sim;
    void Awake()
    {
        CircuitSim= GameObject.FindGameObjectWithTag("CircuitSim");
        Sim=CircuitSim.GetComponent<CircuitSim>();
// instantiating 2 lead AC voltage source
        ACVolt=Sim.sim.Create<ACVoltageSource>(5.0f);
    }

    // Update is called once per frame
}
