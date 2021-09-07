using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Simulations;
using TMPro;
using System.Linq;

public class CircuitManagerTinker : MonoBehaviour
{
    public enum component
    {
        resistor,
        wire,
        voltage,
        bjt,
    };

    public static Circuit ckt;
    public static Dictionary<string, int> components;
    public static List<GameObject> componentList;
    public static GameObject selected;
    Transform[] childs;
    GameObject volt = null;
    string temp;
    [SerializeField] Breadboard breadBoard;
    Transform[] rows=new Transform[4];
    Transform[,] columns= new Transform[60,2];
    List<List<string>> circuits = new List<List<string>>() { };

    string a, b;

    private void Awake()
    {
        
        components = new Dictionary<string, int>();
        componentList = new List<GameObject>();
    }

    

    public void Play()
    {
        ckt = new Circuit();
        ckt.Add(UnifiedScript.CreateBJTModel("mjd44h11", string.Join(" ",
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
        rows = breadBoard.rows;
        columns = breadBoard.columns;
        
        for (int i = 0; i < componentList.Count; i++)
        {
            //get nodes of wire component
            if (componentList[i].GetComponent<ComponentTinker>().a == component.wire)
            {
                childs = new Transform[3];
                childs[0] = componentList[i].transform;
                childs[1] = componentList[i].GetComponent<NewWireManager>().node1.transform;
                childs[2] = componentList[i].GetComponent<NewWireManager>().node2.transform;
            }
            //get nodes of other components
            else childs = componentList[i].GetComponent<ComponentTinker>().childs;
            
            List<string> nodes = new List<string>();

            //to check if component is attatched to breadboard and add to the nodes list
            if (componentList[i].GetComponent<ComponentTinker>().a == component.wire)
            {
                for (int j = 1; j <= componentList[i].GetComponent<ComponentTinker>().no_nodes; j++)
                {
                    if (childs[j].parent.parent.parent!=null && childs[j].parent.parent.parent.tag == "Breadboard")
                    {
                        string y;
                        if (childs[j].position.y < 0) y = (childs[j].position.y).ToString().Substring(0, 4);
                        else y = (childs[j].position.y).ToString().Substring(0, 3);
                        string x;
                        if (childs[j].position.x < 0) x = (childs[j].position.x).ToString().Substring(0, 4);
                        else x = (childs[j].position.x).ToString().Substring(0, 3);

                        //check if wire is connected with powergrid in breadboard
                        for (int k = 0; k < 4; k++)
                        {
                            if (rows[k].position.y < 0) b = (rows[k].position.y).ToString().Substring(0, 4);
                            else b = (rows[k].position.y).ToString().Substring(0, 3);
                            if (b == y)
                            {
                                a = "0";
                                nodes.Add(a + " " + y);
                            }
                        }
                        //check wire is connected with grid other than powergrid
                        for (int k = 0; k < 60; k++)
                        {
                            float yy = childs[j].position.y;
                            if (columns[k, 0].position.x < 0) a = (columns[k, 0].position.x).ToString().Substring(0, 4);
                            else a = (columns[k, 0].position.x).ToString().Substring(0, 3);
                            if (a == x && columns[k, 0].position.y <= yy && yy <= columns[k, 1].position.y)
                            {
                                if (childs[j].position.x < 0) a = (childs[j].position.x).ToString().Substring(0, 4);
                                else a = (childs[j].position.x).ToString().Substring(0, 3);
                                if (columns[k, 0].position.y < 0) b = (columns[k, 0].position.y).ToString().Substring(0, 4);
                                else b = (columns[k, 0].position.y).ToString().Substring(0, 3);
                                nodes.Add(a + " " + b);
                            }
                        }
                    }
                    else
                    {
                        if (childs[j].position.x < 0) a = (childs[j].position.x).ToString().Substring(0, 4);
                        else a = (childs[j].position.x).ToString().Substring(0, 3);
                        if (childs[j].position.y < 0) b = (childs[j].position.y).ToString().Substring(0, 4);
                        else b = (childs[j].position.y).ToString().Substring(0, 3);
                        nodes.Add(a + " " + b);
                    }
                }
            }
            else
            {
                if(componentList[i].transform.parent!=null && componentList[i].transform.parent.tag == "Breadboard")
                {
                    for (int j = 1; j <= componentList[i].GetComponent<ComponentTinker>().no_nodes; j++)
                    {
                        string y;
                        if (childs[j].position.y < 0) y = (childs[j].position.y).ToString().Substring(0, 4);
                        else y = (childs[j].position.y).ToString().Substring(0, 3);
                        string x;
                        if (childs[j].position.x < 0) x = (childs[j].position.x).ToString().Substring(0, 4);
                        else x = (childs[j].position.x).ToString().Substring(0, 3);

                        //check if component is connected with powergrid in breadboard
                        for (int k = 0; k < 4; k++)
                        {
                            if (rows[k].position.y < 0) b = (rows[k].position.y).ToString().Substring(0, 4);
                            else b = (rows[k].position.y).ToString().Substring(0, 3);
                            if (b == y)
                            {
                                a = "0";
                                nodes.Add(a + " " + y);
                            }
                        }
                        //check component is connected with grid other than powergrid
                        for (int k = 0; k < 60; k++)
                        {
                            float yy = childs[j].position.y;
                            if (columns[k, 0].position.x < 0) a = (columns[k, 0].position.x).ToString().Substring(0, 4);
                            else a = (columns[k, 0].position.x).ToString().Substring(0, 3);
                            if (a == x && columns[k, 0].position.y <= yy && yy <= columns[k, 1].position.y)
                            {
                                if (childs[j].position.x < 0) a = (childs[j].position.x).ToString().Substring(0, 4);
                                else a = (childs[j].position.x).ToString().Substring(0, 3);
                                if (columns[k, 0].position.y < 0) b = (columns[k, 0].position.y).ToString().Substring(0, 4);
                                else b = (columns[k, 0].position.y).ToString().Substring(0, 3);
                                nodes.Add(a + " " + b);
                            }
                        }
                    }
                }
                else
                {
                    for (int j = 1; j <= componentList[i].GetComponent<ComponentTinker>().no_nodes; j++)
                    {
                        if (childs[j].position.x < 0) a = (childs[j].position.x).ToString().Substring(0, 4);
                        else a = (childs[j].position.x).ToString().Substring(0, 3);
                        if (childs[j].position.y < 0) b = (childs[j].position.y).ToString().Substring(0, 4);
                        else b = (childs[j].position.y).ToString().Substring(0, 3);
                        nodes.Add(a + " " + b);
                    }
                }
                
            }

            if (i == 0)
            {
                temp = nodes[0];
                nodes[0] = "0";
            }
            if (componentList[i].GetComponent<ComponentTinker>().a == component.voltage && volt == null)
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
            print("nodes length " + nodes.Count+" "+componentList[i].name);
            componentList[i].GetComponent<ComponentTinker>().nodes = nodes;
            componentList[i].GetComponent<ComponentTinker>().Initialize(i, nodes);

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

        var dc = new DC("dc", volt.GetComponent<ComponentTinker>().nameInCircuit, 0.0, double.Parse(volt.GetComponent<ComponentTinker>().value), 0.001);
        var currentExport = new RealPropertyExport(dc, selected.GetComponent<ComponentTinker>().nameInCircuit, "i");
        dc.ExportSimulationData += (sender, exportDataEventArgs) =>
        {
            /*voltageText.text =*/ a=("Voltage: " + exportDataEventArgs.GetVoltage(selected.GetComponent<ComponentTinker>().nodes[0], selected.GetComponent<ComponentTinker>().nodes[1]));
            /*currentText.text =*/ b=("Current: " + currentExport.Value);
        };

        // Run the simulation
        dc.Run(ckt);
        print(selected.name+" "+ a);
        print(selected.name + " " + b);
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
            AssetManager.GetInstance().outlineMaterial.SetFloat("_Thickness", 4.0f);
        }
       // selected.GetComponent<Renderer>().material = AssetManager.GetInstance().outlineMaterial;
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
