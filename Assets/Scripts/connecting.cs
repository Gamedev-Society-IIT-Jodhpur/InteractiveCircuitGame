using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class connecting : MonoBehaviour
{
    public ResistorComponent res1;
    public ResistorComponent res2;
   // public VoltageEleme volt0;
   public DCBattery volt0;
   // public Grounding ground0;
   // Circuit sim;
    
    // Start is called before the first frame update
     GameObject  CircuitSim;
     CircuitSim Sim;
    void Start()
    {
        CircuitSim= GameObject.FindGameObjectWithTag("CircuitSim");
        Sim=CircuitSim.GetComponent<CircuitSim>();

        //sim	 = CIrcuitSim.sim;
       // sim.Connect(volt0.voltage.leadPos, res1.resistor.leadIn);
        Sim.sim.Connect(volt0.DCVolt.leadPos, res1.resistor.leadIn);
        //sim.Connect(volt0.voltage.leadPos, res2.resistor.leadIn);
        Sim.sim.Connect(res1.resistor.leadOut,res2.resistor.leadIn);
        //sim.Connect(res2.resistor.leadOut,volt0.voltage.leadPos);
        //sim.Connect(volt0.DCVolt.leadNeg,ground0.ground.leadIn);
        //sim.Connect(res2.resistor.leadOut,ground0.ground.leadIn);	
		//sim.Connect(res1.resistor.leadOut,ground0.ground.leadIn);
        // sim.Connect(res2.resistor.leadOut,volt0.voltage.leadNeg);	
		//sim.Connect(res1.resistor.leadOut,volt0.voltage.leadNeg);
        
        Sim.sim.Connect(res2.resistor.leadOut,volt0.DCVolt.leadNeg);
        

			
    }

    // Update is called once per frame
    void Update()
    {
        
         Sim.sim.doTick();
                Debug.Log(res1.resistor.getCurrent());
               //Debug.Log("Working");
               // Debug.Log(res1.resistor.resistance);
               // Debug.Log(ground0.ground.getCurrent());
               // Debug.Log(res2.resistor.getCurrent());
               // Debug.Log(volt0.DCVolt.maxVoltage);
                
        
    }
}
