using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Entities;
using System.Text.RegularExpressions;
using System;
public class UnifiedScript: MonoBehaviour
{
    
    public  delegate void  Del(string name, string pos , string neg, string value);
    public static Dictionary<string, System.Delegate> dict1 = new Dictionary<string, System.Delegate>();

    public static void  ResistorInitialize(string name , string pos , string neg , string value )
    {
        //Debug.Log("resistor value line 15 of UnifiedScript : "+value);
        CircuitManager.ckt.Add(new Resistor(name, pos, neg, double.Parse(value)));
    }
    public static void  VoltageInitialize(string name, string pos, string neg, string value )
    {
        //Debug.Log("yay working ");
        CircuitManager.ckt.Add(new VoltageSource(name, pos, neg, double.Parse(value)));
    }
    
    public static void  WireInitialize(string name, string pos, string neg, string value )
    {
        //Debug.Log("yay working ");
        CircuitManager.ckt.Add(new VoltageSource(name, pos, neg, 0));
    }
  static  BipolarJunctionTransistor CreateBJT(string name,
string c, string b, string e, string subst,
string model)
    {
        // Create the transistor
        var bjt = new BipolarJunctionTransistor(name, c, b, e, subst, model);
        return bjt;
    }
   static  void ApplyParameters(Entity entity, string definition)
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

   public static BipolarJunctionTransistorModel CreateBJTModel(string name, string parameters)
    {
        var bjtmodel = new BipolarJunctionTransistorModel(name);
        ApplyParameters(bjtmodel, parameters);
        return bjtmodel;
    }
    public static void BJTInitialize(string name, string c, string b, string e)
    {
        CircuitManager.ckt.Add(CreateBJT(name, c, b, e, "0", "mjd44h11"));
        

    }

    void Awake()
    {
        
        Del Resistordel = ResistorInitialize;
        
       dict1.Add("res", Resistordel);
        Del Voltagedel = VoltageInitialize;
        dict1.Add("volt", Voltagedel);
        Del Wiredel = WireInitialize;
        dict1.Add("wire", Wiredel);
        Del BJTdel = BJTInitialize;
        dict1.Add("bjt", BJTdel);
    }
    
}
