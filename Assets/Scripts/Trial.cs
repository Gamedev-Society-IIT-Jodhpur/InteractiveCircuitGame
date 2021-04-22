using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class Trial : MonoBehaviour
{
    Circuit sim;
    VoltageInput volt0;
    Resistor res0;
    Resistor res1;
    Ground ground0;
    Ground  ground1;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Hello");
        	sim	 = CIrcuitSim.sim;

			 volt0 = sim.Create<VoltageInput>(Voltage.WaveType.DC);
			 res0 = sim.Create<Resistor>(1);
			 res1 = sim.Create<Resistor>(1000);
			 ground0 = sim.Create<Ground>();
			 ground1 = sim.Create<Ground>();

			sim.Connect(volt0, 0, res0,    0);
			sim.Connect(volt0, 0, res1,    0);
			sim.Connect(res0,  1, ground0, 0);
			sim.Connect(res1,  1, ground1, 0);
            


			
				
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Hello");
        sim.doTick();
                Debug.Log(ground0.getCurrent());
                Debug.Log(ground1.getCurrent());
        
        
    }
}
