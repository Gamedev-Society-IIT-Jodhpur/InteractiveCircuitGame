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
        diode,
    };
    public enum model
    {
        BC547,
        BC557,
    };

    public static Circuit ckt;
    public static Dictionary<string, int> components;
    public static List<GameObject> componentList;
    public static GameObject selected;
    Transform[] childs;
    GameObject volt = null;
    string temp;
    Breadboard breadBoard;
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
        ckt.Add(UnifiedScript.CreateBJTModel("BC547", string.Join(" ",
                "IS=1.8E-14 BF=400 NF=0.9955 VAF=80 IKF=0.14 ISE=5E-14 ",
                "NE=1.46 BR=35.5 NR=1.005 VAR=12.5 IKR=0.03 ISC=1.72E-13 NC=1.27 RB=0.56 ",
                " RE=0.6 RC=0.25 CJE=1.3E-11 TF=6.4E-10 CJC=4E-12 VJC=0.54 TR=5.072E-8"), 1));
        ckt.Add(UnifiedScript.CreateBJTModel("BC557", string.Join(" ",
     "BF=490 NE=1.5 ISE=12.4e-15 IKF=78e-3 IS=60e-15 VAF=36 ikr=12e-3",
        "nc=2 br=4 var=10 rb=280 re=1 rc=40 vje=0.48 tf=0.5e-9 tr=0.3e-6",
        "cje=12e-12 vje=0.48 mje=0.5 cjc=6e-12 vjc=0.7 mjc=0.33 isc=47.6e-12 kf=2e-15"), 0));
        print(componentList.Count);
        ckt.Add(UnifiedScript.CreateDiodeModel("Default", "Is=1e-14 Rs=0 N=1 Cjo=0 M=0.5 tt=0 bv=1e16 vj=1"));
        print(componentList.Count);
        breadBoard = GameObject.FindGameObjectWithTag("Breadboard").GetComponent<Breadboard>();
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
