using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Entities;
using System.Text.RegularExpressions;
using System;
using UnityEngine.SceneManagement;

public class UnifiedScript: MonoBehaviour
{
    public static string scene;
    static bool dictCreated = false;
    
    public  delegate void  Del(string name, List<string> nodes, string value1 , string value2);
    public static Dictionary<string, System.Delegate> dict1 = new Dictionary<string, System.Delegate>();

    public static void  ResistorInitialize(string name , List<string> nodes , string value , string val)
    {
        //Debug.Log("resistor value line 15 of UnifiedScript : "+value);
        if (scene == "Falstad")
        {
            CircuitManager.ckt.Add(new Resistor(name, nodes[0], nodes[1], double.Parse(value)));
        }
        else
        {
            CircuitManagerTinker.ckt.Add(new Resistor(name, nodes[0], nodes[1], double.Parse(value)));
        }
    }
    public static void  VoltageInitialize(string name, List<string> nodes, string value , string val )
    {
        //Debug.Log("yay working ");
        if (scene == "Falstad")
        {
            CircuitManager.ckt.Add(new VoltageSource(name, nodes[0],nodes[1], double.Parse(value)));
        }
        else
        {
            CircuitManagerTinker.ckt.Add(new VoltageSource(name, nodes[0], nodes[1], double.Parse(value)));
        }
    }
    
    public static void  WireInitialize(string name, List<string> nodes, string value , string val)
    {
        //Debug.Log("yay working ");
        if (scene == "Falstad")
        {
            CircuitManager.ckt.Add(new VoltageSource(name, nodes[0], nodes[1], 0));
        }
        else
        {
            CircuitManagerTinker.ckt.Add(new VoltageSource(name, nodes[0], nodes[1], 0));
        }
    }

    static Diode CreateDiode(string name, string anode, string cathode, string model)
    {
        var d = new Diode(name, anode, cathode, model);
        return d;
    }

   public static DiodeModel CreateDiodeModel(string name, string parameters)
    {
        var dm = new DiodeModel(name);
        ApplyParameters(dm, parameters);
        return dm;
    }

    static  BipolarJunctionTransistor CreateBJT(string name,string c, string b, string e, string subst,string model)
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

   public static BipolarJunctionTransistorModel CreateBJTModel(string name, string parameters , int i)
    {
        BipolarJunctionTransistorModel bjtmodel = new BipolarJunctionTransistorModel(name);
        ApplyParameters(bjtmodel, parameters);
        if (i == 1)
        {
            bjtmodel.SetParameter<bool>("npn", true);
        }
        else
        {
            bjtmodel.SetParameter<bool>("pnp", true);
        }
        return bjtmodel;
    }
    public static void BJTInitialize(string name, List<string> nodes, string model , string val)
    {
        

        if (scene == "Falstad")
        {
            if (!(CircuitManager.ckt.Contains(model + val)))
            {
                //print("model name :" + model + val);
                if (model == "BC547")
                    CircuitManager.ckt.Add(UnifiedScript.CreateBJTModel(model + val, string.Join(" ",
                   "IS=1.8E-14 BF=400 NF=0.9955 VAF=80 IKF=0.14 ISE=5E-14 ",
                   "NE=1.46 BR=35.5 NR=1.005 VAR=12.5 IKR=0.03 ISC=1.72E-13 NC=1.27 RB=0.56 ",
                   " RE=0.6 RC=0.25 CJE=1.3E-11 TF=6.4E-10 CJC=4E-12 VJC=0.54 TR=5.072E-8 ",
                   "bf=" + val), 1));
                if (model == "BC557")
                    CircuitManager.ckt.Add(UnifiedScript.CreateBJTModel(model + val, string.Join(" ",
                "BF=490 NE=1.5 ISE=12.4e-15 IKF=78e-3 IS=60e-15 VAF=36 ikr=12e-3",
                "nc=2 br=4 var=10 rb=280 re=1 rc=40 vje=0.48 tf=0.5e-9 tr=0.3e-6",
                "cje=12e-12 vje=0.48 mje=0.5 cjc=6e-12 vjc=0.7 mjc=0.33 isc=47.6e-12 kf=2e-15",
                "bf=" + val), 0));
            }

            CircuitManager.ckt.Add(CreateBJT(name, nodes[0], nodes[1], nodes[2], "0", model+val));
        }
        else
        {
            if (!(CircuitManagerTinker.ckt.Contains(model + val)))
            {
                //print("model name :" + model + val);
                if (model == "BC547")
                    CircuitManagerTinker.ckt.Add(UnifiedScript.CreateBJTModel(model + val, string.Join(" ",
                   "IS=1.8E-14 BF=400 NF=0.9955 VAF=80 IKF=0.14 ISE=5E-14 ",
                   "NE=1.46 BR=35.5 NR=1.005 VAR=12.5 IKR=0.03 ISC=1.72E-13 NC=1.27 RB=0.56 ",
                   " RE=0.6 RC=0.25 CJE=1.3E-11 TF=6.4E-10 CJC=4E-12 VJC=0.54 TR=5.072E-8 ",
                   "bf=" + val), 1));
                if (model == "BC557")
                    CircuitManagerTinker.ckt.Add(UnifiedScript.CreateBJTModel(model + val, string.Join(" ",
                "BF=490 NE=1.5 ISE=12.4e-15 IKF=78e-3 IS=60e-15 VAF=36 ikr=12e-3",
                "nc=2 br=4 var=10 rb=280 re=1 rc=40 vje=0.48 tf=0.5e-9 tr=0.3e-6",
                "cje=12e-12 vje=0.48 mje=0.5 cjc=6e-12 vjc=0.7 mjc=0.33 isc=47.6e-12 kf=2e-15",
                "bf=" + val), 0));
            }

            CircuitManagerTinker.ckt.Add(CreateBJT(name, nodes[0], nodes[1], nodes[2], "0", model+val));
        }
       
    }

    public static void DiodeInitialize(string name, List<string> nodes, string model , string val)
    {
        if (scene == "Falstad")
        {
            if(model !="ZenerDiode")
            {
                if (!(CircuitManager.ckt.Contains(model)))
                {
                    CircuitManager.ckt.Add(UnifiedScript.CreateDiodeModel(val + "BV", "Is=1e-14 Rs=0 N=1 Cjo=0 M=0.5 tt=0 bv=1e16 vj=1 "));
                }
                try
                {
                    CircuitManager.ckt.Add(CreateDiode(name, nodes[0], nodes[1], model));
                }
                catch (System.Exception e)
                {
                    CircuitManager.ckt.Add(CreateDiode(name, nodes[0], nodes[1], model));
                }
            }
            else
            {
                if (!(CircuitManager.ckt.Contains(val + "BV")))
                {
                    CircuitManager.ckt.Add(UnifiedScript.CreateDiodeModel(val + "BV", " bv=" + val));
                }
                try
                {
                    CircuitManager.ckt.Add(CreateDiode(name, nodes[0], nodes[1], val + "BV"));
                }
                catch (System.Exception e)
                {
                    CircuitManager.ckt.Add(CreateDiode(name, nodes[0], nodes[1], model));
                }
            }
            
        }


        else
        {
            //CircuitManagerTinker.ckt.Add(CreateDiode(name, nodes[0], nodes[1], model));
            if (model != "ZenerDiode")
            {
                if (!(CircuitManagerTinker.ckt.Contains(model)))
                {
                    CircuitManagerTinker.ckt.Add(UnifiedScript.CreateDiodeModel(model + "BV", "Is=1e-14 Rs=0 N=1 Cjo=0 M=0.5 tt=0 bv=1e16 vj=1 "));
                }
                try
                {
                    CircuitManagerTinker.ckt.Add(CreateDiode(name, nodes[0], nodes[1], model));
                }
                catch (System.Exception e)
                {
                    CircuitManagerTinker.ckt.Add(CreateDiode(name, nodes[0], nodes[1], model));
                }
            }
            else
            {
                if (!(CircuitManagerTinker.ckt.Contains(val + "BV")))
                {
                    CircuitManagerTinker.ckt.Add(UnifiedScript.CreateDiodeModel(val + "BV", " bv=" + val));
                }
                try
                {
                    CircuitManagerTinker.ckt.Add(CreateDiode(name, nodes[0], nodes[1], val + "BV"));
                }
                catch (System.Exception e)
                {
                    CircuitManagerTinker.ckt.Add(CreateDiode(name, nodes[0], nodes[1], model));
                }
            }
            
        }
           
        }
        

    
    

    void Awake()
    {
        
       if (!dictCreated)
       {
            Del Resistordel = ResistorInitialize;
            dict1.Add("resistor", Resistordel);

            Del Voltagedel = VoltageInitialize;
            dict1.Add("voltage", Voltagedel);

            Del Wiredel = WireInitialize;
            dict1.Add("wire", Wiredel);

            Del BJTdel = BJTInitialize;
            dict1.Add("bjt", BJTdel);

            Del Diodedel = DiodeInitialize;
            dict1.Add("diode", Diodedel);
            dict1.Add("zenerDiode", Diodedel);
            dictCreated = true;
        }
    }
    

}
