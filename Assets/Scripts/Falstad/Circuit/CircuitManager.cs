using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Simulations;
//using UnityEngine.UI;
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

    };

    public static Circuit ckt;
    public static Dictionary<string, int> components ;
    public static List<GameObject> componentList;
    public static GameObject selected;
    string pos;
    string neg;
    Transform[] childs;
    GameObject volt=null;
    string temp;
    [SerializeField] TMP_Text voltageText;
    [SerializeField] TMP_Text currentText;
    List<List<string>> circuits = new List<List<string>>() { };

    
    private void Awake()
    {
        components = new Dictionary<string, int>();
        componentList = new List<GameObject>();
    }
    public void Play()
    {
        ckt = new Circuit();
        CircuitManager.ckt.Add(UnifiedScript.CreateBJTModel("mjd44h11", string.Join(" ",
                "IS = 1.45468e-14 BF = 135.617 NF = 0.85 VAF = 10",
                "IKF = 5.15565 ISE = 2.02483e-13 NE = 3.99964 BR = 13.5617",
                "NR = 0.847424 VAR = 100 IKR = 8.44427 ISC = 1.86663e-13",
                "NC = 1.00046 RB = 1.35729 IRB = 0.1 RBM = 0.1",
                "RE = 0.0001 RC = 0.037687 XTB = 0.90331 XTI = 1",
                "EG = 1.20459 CJE = 3.02297e-09 VJE = 0.649408 MJE = 0.351062",
                "TF = 2.93022e-09 XTF = 1.5 VTF = 1.00001 ITF = 0.999997",
                "CJC = 3.0004e-10 VJC = 0.600008 MJC = 0.409966 XCJC = 0.8",
                "FC = 0.533878 CJS = 0 VJS = 0.75 MJS = 0.5",
                "TR = 2.73328e-08 PTF = 0 KF = 0 AF = 1")));
        print(componentList.Count);
        for (int i = 0; i < componentList.Count; i++)
        {
            Debug.Log("i :"+i+" " +componentList[i].name);
            print(componentList[i].name);
            childs = componentList[i].GetComponentsInChildren<Transform>();
            
            List<string> nodes= new List<string>();
            for (int j = 1; j <= componentList[i].GetComponent<ComponentInitialization>().no_nodes; j++)
            {
                print(childs[j].position);
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
            if (componentList[i].GetComponent<ComponentInitialization>().a != component.bjt)
            {
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
            else
            {
                string node3 = (Mathf.RoundToInt(childs[3].position.x)).ToString() + " " + (Mathf.RoundToInt(childs[3].position.y)).ToString();
                for (int j = 0; j < circuits.Count; j++)
                {
                    if (circuits[j].Contains(pos) || circuits[j].Contains(neg) || circuits[j].Contains(node3))
                    {
                        if (placed == 0)
                        {
                            circuits[j] = circuits[j].Union(new List<string> { pos, neg, node3 }).ToList();
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
                    circuits.Add(new List<string>() { pos, neg, node3 });
                }
            }
        }
       
        Groundit();

        var dc = new DC("dc", volt.GetComponent<ComponentInitialization>().nameInCircuit, 0.0,double.Parse(volt.GetComponent<ComponentInitialization>().value), 0.001);
        var currentExport = new RealPropertyExport(dc, selected.GetComponent<ComponentInitialization>().nameInCircuit, "i");
        dc.ExportSimulationData += (sender, exportDataEventArgs) =>
        {
            voltageText.text= ("Voltage: "+ exportDataEventArgs.GetVoltage(selected.GetComponent<ComponentInitialization>().nodes[0] ,selected.GetComponent<ComponentInitialization>().nodes[1]));
            currentText.text= ("Current: " + currentExport.Value);
        };

        // Run the simulation
        dc.Run(ckt);
    }

    public static void ChangeSelected(GameObject gameObject)
    {
        if (selected)
        {
            selected.GetComponent<Renderer>().material = AssetManager.GetInstance().defaultMaterial;
        }

        selected = gameObject;

        if (selected.tag == "Resistor")
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
            UnifiedScript.WireInitialize("GroundingWire" + i, new List<string> { circuits[i][0], "0" }, "0");

        }
        circuits.Clear();
    }

}
