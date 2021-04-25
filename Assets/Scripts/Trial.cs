using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class Trial : MonoBehaviour
{
    
    VoltageInput volt0;
    Resistor res0;
    Resistor res1;
    Ground ground0;
    Ground  ground1;
    // Start is called before the first frame update
    public GameObject  CircuitSim;
    public CircuitSim Sim;
    void Awake()
    {
        CircuitSim= GameObject.FindGameObjectWithTag("CircuitSim");
        Sim=CircuitSim.GetComponent<CircuitSim>();


			 volt0 = Sim.sim.Create<VoltageInput>(Voltage.WaveType.DC);
			 res0 = Sim.sim.Create<Resistor>(1);
			 res1 = Sim.sim.Create<Resistor>(1000);
			 ground0 = Sim.sim.Create<Ground>();
			 ground1 = Sim.sim.Create<Ground>();

			Sim.sim.Connect(volt0, 0, res0,    0);
			Sim.sim.Connect(volt0, 0, res1,    0);
			Sim.sim.Connect(res0,  1, ground0, 0);
			Sim.sim.Connect(res1,  1, ground1, 0);
            


			
				
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Hello");
        Sim.sim.doTick();
                Debug.Log(ground0.getCurrent());
                Debug.Log(ground1.getCurrent());
        
        
    }
}
