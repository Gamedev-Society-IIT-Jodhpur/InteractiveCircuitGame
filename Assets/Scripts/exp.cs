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
        // Build the circuit
        var ckt = new Circuit(
            new VoltageSource("V1", "in", "0", 0.0),
            new Resistor("R1", "in", "out", 1.0e3),
            new Resistor("R2", "out", "0", 2.0e3)
            );

        // Create a DC sweep and register to the event for exporting simulation data
        var dc = new DC("dc", "V1", 0.0, 5.0, 0.001);
        dc.ExportSimulationData += (sender, exportDataEventArgs) =>
        {
            Debug.Log(exportDataEventArgs.GetVoltage("out"));
        };

        // Run the simulation
        dc.Run(ckt);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
