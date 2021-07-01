using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Simulations;

public class exp : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        //print(CircuitManager.components["Voltage"]);
        // Build the circuit

        //UnifiedScript.Del("V0", "in", "0", "0.0");


        // CircuitManager.ckt.Add(new VoltageSource("V0", "in", "0", 0.0));
        // CircuitManager.components["Voltage"]+=1;
        // CircuitManager.ckt.Add(new VoltageSource("V1", "0", "b", 5.0));
        // CircuitManager.components["Voltage"]+=1;
        // CircuitManager.ckt.Add(new Resistor("R0", "in", "rout", 2.0e3));
        // CircuitManager.components["Resistor"]+=1;
        // CircuitManager.ckt.Add(new Resistor("R1", "rout", "0", 1.0e3));
        // CircuitManager.components["Resistor"]+=1;

        

        // Create a DC sweep and register to the event for exporting simulation data
       /* var dc = new DC("dc", "V0", 0.0, 5.0, 0.001);
        var currentExport = new RealPropertyExport(dc, "V0", "i");
        dc.ExportSimulationData += (sender, exportDataEventArgs) =>
        {
            // Debug.Log(exportDataEventArgs.GetVoltage("vout"));
            //Debug.Log(currentExport.Value);
        };

        // Run the simulation
        dc.Run(CircuitManager.ckt);*/

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
