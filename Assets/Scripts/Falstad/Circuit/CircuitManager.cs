using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Simulations;
using TMPro;
using System.Linq;

public class CircuitManager : MonoBehaviour
{
    public enum component
    {
        resistor,
        wire,
        voltage,
        bjt,
        diode,
        zenerDiode,
    };

    public enum model
    {
        BC547,
        BC557,
    };
    
    public static Circuit ckt;
    public static Dictionary<string, int> components ;
    public static List<GameObject> componentList;
    public static GameObject selected;
    string pos;
    string neg;
    Transform[] childs;
    public static GameObject volt=null;
    string temp;
    [SerializeField] TMP_Text DisplayText;
    public static GameObject valueinput;
    List<List<string>> circuits = new List<List<string>>() { };

    
    private void Awake()
    {
        valueinput = GameObject.FindGameObjectWithTag("value input");
        components = new Dictionary<string, int>();
        componentList = new List<GameObject>();
    }


    public void Play()
    {
        ckt = new Circuit();
        CircuitManager.ckt.Add(UnifiedScript.CreateBJTModel("BC547100", string.Join(" ",
                "IS=1.8E-14 BF=400 NF=0.9955 VAF=80 IKF=0.14 ISE=5E-14 ",
                "NE=1.46 BR=35.5 NR=1.005 VAR=12.5 IKR=0.03 ISC=1.72E-13 NC=1.27 RB=0.56 ",
                " RE=0.6 RC=0.25 CJE=1.3E-11 TF=6.4E-10 CJC=4E-12 VJC=0.54 TR=5.072E-8 " ) , 1));
        CircuitManager.ckt.Add(UnifiedScript.CreateBJTModel("BC557100", string.Join(" ",
        "BF=490 NE=1.5 ISE=12.4e-15 IKF=78e-3 IS=60e-15 VAF=36 ikr=12e-3",
        "nc=2 br=4 var=10 rb=280 re=1 rc=40 vje=0.48 tf=0.5e-9 tr=0.3e-6",
        "cje=12e-12 vje=0.48 mje=0.5 cjc=6e-12 vjc=0.7 mjc=0.33 isc=47.6e-12 kf=2e-15"), 0));
        CircuitManager.ckt.Add(UnifiedScript.CreateDiodeModel("Default", "Is=1e-14 Rs=0 N=1 Cjo=0 M=0.5 tt=0 bv=1e16 vj=1"));
        CircuitManager.ckt.Add(UnifiedScript.CreateDiodeModel("ZenerDiode","Is =18.8e-9 N=2 Cjo=30e-12 M=0.33 bv=6 ibv=5e-6"));
        for (int i = 0; i < componentList.Count; i++)
        {
            //Debug.Log("i :"+i+" " +componentList[i].name);
            print(componentList[i].name);
            childs = componentList[i].GetComponentsInChildren<Transform>();
            
            List<string> nodes= new List<string>();
            for (int j = 1; j <= componentList[i].GetComponent<ComponentInitialization>().no_nodes; j++)
            {
                //print(childs[j].position);
                nodes.Add((Mathf.RoundToInt(childs[j].position.x)).ToString() + " " + (Mathf.RoundToInt(childs[j].position.y)).ToString());
            }

            if (i == 0)
            {
                temp = nodes[1];
                nodes[1] = "0";
            }
            if (componentList[i].GetComponent<ComponentInitialization>().a == component.voltage && volt == null)
            {
                volt = componentList[i];

            }
            for (int j = 0; j < nodes.Count; j++)
            {
                if (nodes[j] == temp)
                {
                    nodes[j] = "0";
                }
            }
           
            componentList[i].GetComponent<ComponentInitialization>().nodes = nodes;
            componentList[i].GetComponent<ComponentInitialization>().Initialize(i,nodes);

            int placed = 0;
            int placedindex = -1;
            for (int j = 0; j < circuits.Count; j++)
            {
                bool contains = false;
                for (int k = 0; k < nodes.Count; k++)
                {
                    if (circuits[j].Contains(nodes[k]))
                    {
                        contains = true;
                        break;
                    }
                }
                if (contains)
                {
                    if (placed == 0)
                    {
                        circuits[j] = circuits[j].Union(nodes).ToList();
                        placed = 1;
                        placedindex = j;
                    }
                    else
                    {
                        Merge(placedindex, j);
                        j--;
                    }
                }
            }
            if (placed == 0)
            {
                circuits.Add(nodes);
            }
            
        }
       
        Groundit();
        //TODO exception for null voltage

        DC dc;
        try
        {
            dc = new DC("dc", volt.GetComponent<ComponentInitialization>().nameInCircuit, double.Parse(volt.GetComponent<ComponentInitialization>().value), double.Parse(volt.GetComponent<ComponentInitialization>().value), 0.001);
            var currentExport = new RealPropertyExport(dc, selected.GetComponent<ComponentInitialization>().nameInCircuit, "i");
            

            dc.ExportSimulationData += (sender, exportDataEventArgs) =>
            {
                if (selected.GetComponent<ComponentInitialization>().a != component.bjt)
                {

                    DisplayText.text = ("Voltage: " + SIUnits.NormalizeRounded(exportDataEventArgs.GetVoltage(selected.GetComponent<ComponentInitialization>().nodes[0], selected.GetComponent<ComponentInitialization>().nodes[1]), 9, "V")
                                     + "\nCurrent: " + SIUnits.NormalizeRounded(currentExport.Value, 9, "A"));
                }
                else
                {
                    var vbe = new RealPropertyExport(dc, selected.GetComponent<ComponentInitialization>().nameInCircuit, "vbe");
                    var vbc = new RealPropertyExport(dc, selected.GetComponent<ComponentInitialization>().nameInCircuit, "vbc");
                    var ib = new RealPropertyExport(dc, selected.GetComponent<ComponentInitialization>().nameInCircuit, "ib");
                    var ic = new RealPropertyExport(dc, selected.GetComponent<ComponentInitialization>().nameInCircuit, "ic");
                    DisplayText.text = ("Vbe: " + SIUnits.NormalizeRounded(vbe.Value, 9, "V")
                                        + "\nVbc: " + SIUnits.NormalizeRounded(vbc.Value, 9, "V")
                                 + "\nIc: " + SIUnits.NormalizeRounded(ic.Value, 9, "A")
                                + "\nIb: " + SIUnits.NormalizeRounded(ib.Value, 9, "A"));
                }

                //Debug.Log(selected.GetComponent<ComponentInitialization>().nameInCircuit + " " + currentExport1.Value);
            };
            
            try
            {
                dc.Run(ckt);
            }
            catch (System.Exception e)
            {

                print(e.Message);
            }
        }
        catch (System.Exception e)
        {

            //throw;
            print(e.Message);
        }
        
        

        // Run the simulation
    }

    public static void ChangeSelected(GameObject gameObject)
    {
        

        if (selected)
        {
            selected.GetComponent<Renderer>().material = AssetManager.GetInstance().defaultMaterial;
        }

        //to change input value text field
        selected = gameObject;
        if (selected.tag != "Wire" && selected.tag=="BJT")
        {
            valueinput.GetComponent<TMP_InputField>().text = gameObject.GetComponent<ComponentInitialization>().value;
        }
        else if (selected.tag == "BJT")
        {
            valueinput.GetComponent<TMP_InputField>().text = gameObject.GetComponent<ComponentInitialization>().beta.ToString();
        }


        //for changing outline
        if (selected.tag == "Resistor" || selected.tag=="BJT")
        {
            AssetManager.GetInstance().outlineMaterial.SetFloat("_Thickness", 4.0f);
        }
        else
        {
            AssetManager.GetInstance().outlineMaterial.SetFloat("_Thickness", 15.0f);
        }
        selected.GetComponent<Renderer>().material = AssetManager.GetInstance().outlineMaterial;
    }

    private void Merge(int i, int j)
    {
        circuits[i] = circuits[i].Union(circuits[j]).ToList();
        circuits.RemoveAt(j);
    }

    /**
     * To add 
     */
    private void Groundit()
    {
        Debug.Log("no of circuits =" + circuits.Count);

        for (int i = 1; i < circuits.Count; i++)
        {
            UnifiedScript.WireInitialize("GroundingWire" + i, new List<string> { circuits[i][0], "0" }, "0" , "");

        }
        circuits.Clear();
    }

}
