using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Simulations;
using SpiceSharp.Entities;
using SpiceSharp.Behaviors;
using System;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;


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
         void ApplyParameters(Entity entity, string definition)
        {
            // Get all assignments
            definition = Regex.Replace(definition, @"\s*\=\s*", "=");
            var assignments = definition.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var assignment in assignments)
            {
                // Get the name and value
                var parts = assignment.Split('=');
                if (parts.Length != 2)
                    throw new Exception("Invalid assignment");
                var name = parts[0].ToLower();
                var value = double.Parse(parts[1], System.Globalization.CultureInfo.InvariantCulture);

                // Set the entity parameter
                entity.SetParameter(name, value);
            }
        }
            BipolarJunctionTransistor CreateBJT(string name,
            string c, string b, string e, string subst,
            string model)
        {
            // Create the transistor
            var bjt = new BipolarJunctionTransistor(name, c, b, e, subst, model);
            return bjt;
        }

        BipolarJunctionTransistorModel CreateBJTModel(string name, string parameters)
        {
            var bjtmodel = new BipolarJunctionTransistorModel(name);
            ApplyParameters(bjtmodel, parameters);
            return bjtmodel;
        }

        var ckt = new Circuit(
                 new VoltageSource("V1", "b", "0", 0),
                 new VoltageSource("V2", "c", "0", 0),
                 CreateBJT("Q1", "c", "b", "0", "0", "mjd44h11"),
                 CreateBJTModel("mjd44h11", string.Join(" ",
                     "IS = 1.45468e-14 BF = 135.617 NF = 0.85 VAF = 10",
                     "IKF = 5.15565 ISE = 2.02483e-13 NE = 3.99964 BR = 13.5617",
                     "NR = 0.847424 VAR = 100 IKR = 8.44427 ISC = 1.86663e-13",
                     "NC = 1.00046 RB = 1.35729 IRB = 0.1 RBM = 0.1",
                     "RE = 0.0001 RC = 0.037687 XTB = 0.90331 XTI = 1",
                     "EG = 1.20459 CJE = 3.02297e-09 VJE = 0.649408 MJE = 0.351062",
                     "TF = 2.93022e-09 XTF = 1.5 VTF = 1.00001 ITF = 0.999997",
                     "CJC = 3.0004e-10 VJC = 0.600008 MJC = 0.409966 XCJC = 0.8",
                     "FC = 0.533878 CJS = 0 VJS = 0.75 MJS = 0.5",
                     "TR = 2.73328e-08 PTF = 0 KF = 0 AF = 1"))
                 );

        // Create simulation
        var dc = new DC("dc", new[] {
                new ParameterSweep("V1", new LinearSweep(0, 0.8, 0.1)),
                new ParameterSweep("V2", new LinearSweep(0, 5, 0.5))
            });
        //var currentExport = new RealPropertyExport(dc, "Q1", "i");
        IExport<double>[] exports = { new RealPropertyExport(dc, "V2", "i"), new RealPropertyExport(dc, "V1", "i") };

        // Provided by Spice 3f5
      

        // Run test
       
        dc.ExportSimulationData += (sender, exportDataEventArgs) =>
        {
             Debug.Log("vb ="+exportDataEventArgs.GetVoltage("b"));
            Debug.Log("vc ="+exportDataEventArgs.GetVoltage("c"));
            foreach (var i in exports) {
                Debug.Log("exports =" + i.Value);
            }

            //Debug.Log(currentExport.Value);
        };

        dc.Run(ckt);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
