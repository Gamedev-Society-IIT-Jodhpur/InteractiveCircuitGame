using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class connecting : MonoBehaviour
{
    public ObjectLead res1;
    public ObjectLead res2;
    public VoltageEleme volt0;
    public Grounding ground0;
    Circuit sim;
    
    // Start is called before the first frame update
    void Start()
    {
        sim	 = CIrcuitSim.sim;
        sim.Connect(res1.resistor.leadOut,res2.resistor.leadIn);
        sim.Connect(volt0.voltage.leadPos, res1.resistor.leadIn);
			
		sim.Connect(res2.resistor.leadOut, ground0.ground.leadIn);

			
    }

    // Update is called once per frame
    void Update()
    {
        sim.doTick();
                Debug.Log(ground0.ground.getCurrent());
                
        
    }
}
