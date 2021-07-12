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
    public static Circuit ckt;
    public static Dictionary<string, int> components ;
    public static List<GameObject> componentList;
    public static GameObject selected;
    //[SerializeField] 
    //public static Material outlineMaterial;
    string pos;
    string neg;
    Transform[] childs;
    GameObject volt=null;
    string temp;
    [SerializeField] TMP_Text voltageText;
    [SerializeField] TMP_Text currentText;
    List<List<string>> circuits = new List<List<string>>() { };


    //[SerializeField] GameObject resistor;
    //private IList<string> componentList = new List<string>(){"Voltage","Resistor"};

    private void Awake(){
        //ckt = new Circuit();
        components = new Dictionary<string, int>();
        componentList = new List<GameObject>();
    }
    public void Play()
    {
        ckt = new Circuit();
        for (int i = 0; i < componentList.Count; i++)
        {

            //print(componentList[i].name);
            childs = componentList[i].GetComponentsInChildren<Transform>();
            pos = (Mathf.RoundToInt(childs[1].position.x)).ToString() +" "+ (Mathf.RoundToInt(childs[1].position.y)).ToString();
            neg = (Mathf.RoundToInt(childs[2].position.x)).ToString() +" "+ (Mathf.RoundToInt(childs[2].position.y)).ToString();
            if (i == 0)
            {
                temp = neg;
                neg = "0";
            }
            if (componentList[i].GetComponent<ComponentInitialization>().a == "volt" && volt==null)
            {
                volt = componentList[i];
                
            }
            if (neg == temp)
            {
                neg = "0";
            }
            if (pos == temp)
            {
                pos = "0";
            }
            //print("printing evrything " + pos + " " + neg+" "+temp.GetType());
            componentList[i].GetComponent<ComponentInitialization>().pos = pos;
            componentList[i].GetComponent<ComponentInitialization>().neg = neg;
            componentList[i].GetComponent<ComponentInitialization>().Initialize(i,pos,neg);

            int placed = 0;
            int placedindex = -1;

            for (int j = 0; j < circuits.Count; j++)
            {
                
                
                
                    if (circuits[j].Contains(pos) || circuits[j].Contains(neg) )
                    {
                        if (placed == 0)
                        {
                            circuits[j] = circuits[j].Union(new List<string> { pos, neg }).ToList();
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
                circuits.Add(new List<string>() { pos, neg });
            }


        }
        Groundit();

      

        var dc = new DC("dc", volt.GetComponent<ComponentInitialization>().nameInCircuit, 0.0,double.Parse(volt.GetComponent<ComponentInitialization>().value), 0.001);
        var currentExport = new RealPropertyExport(dc, selected.GetComponent<ComponentInitialization>().nameInCircuit, "i");
        dc.ExportSimulationData += (sender, exportDataEventArgs) =>
        {
            voltageText.text= ("Voltage: "+ exportDataEventArgs.GetVoltage(selected.GetComponent<ComponentInitialization>().pos ,selected.GetComponent<ComponentInitialization>().neg));
            currentText.text= ("Current: " + currentExport.Value);
            //print(selected.name);
            //Debug.Log("Kinda Working");
        };


        // Run the simulation
        dc.Run(CircuitManager.ckt);

    }

 

    public static void ChangeSelected(GameObject gameObject)
    {
        if (selected)
        {
            selected.GetComponent<Renderer>().material = AssetManager.GetInstance().defaultMaterial;
        }
        selected = gameObject;
        selected.GetComponent<Renderer>().material = AssetManager.GetInstance().outlineMaterial;
    }


 

    private void Merge(int i, int j)
    {
        //Debug.Log("Before merge" + circuits.Count);
        circuits[i] = circuits[i].Union(circuits[j]).ToList();
        circuits.RemoveAt(j);
        //Debug.Log("After merge" + circuits.Count);
    }

    private void Groundit()
    {
        Debug.Log("no of circuits =" + circuits.Count);

        for (int i = 1; i < circuits.Count; i++)
        {
            UnifiedScript.WireInitialize("GroundingWire" + i, circuits[i][0], "0", "0");

        }
        //Debug.Log("count =" + circuits.Count);
        circuits.Clear();
    }

}
