using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Simulations;
using UnityEngine.UI;

public class CircuitManager : MonoBehaviour
{
    public static Circuit ckt;
    public static Dictionary<string, int> components ;
    public static List<GameObject> componentList;
    public static GameObject selected;
    string pos;
    string neg;
    Transform[] childs;
    string volt="";
    string temp;
    [SerializeField] Text voltageText;
    [SerializeField] Text currentText;


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

            print(componentList[i].name);
            childs = componentList[i].GetComponentsInChildren<Transform>();
            pos = (Mathf.RoundToInt(childs[1].position.x)).ToString() +" "+ (Mathf.RoundToInt(childs[1].position.y)).ToString();
            neg = (Mathf.RoundToInt(childs[2].position.x)).ToString() +" "+ (Mathf.RoundToInt(childs[2].position.y)).ToString();
            if (i == 0)
            {
                temp = neg;
                neg = "0";
            }
            if (componentList[i].GetComponent<ComponentInitialization>().a == "volt" && volt=="")
            {
                volt = "volt" + i;
                
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
        }

        var dc = new DC("dc", volt, 0.0, 5.0, 0.001);
        var currentExport = new RealPropertyExport(dc, selected.GetComponent<ComponentInitialization>().nameInCircuit, "i");
        dc.ExportSimulationData += (sender, exportDataEventArgs) =>
        {
            voltageText.text="Voltage: "+ exportDataEventArgs.GetVoltage(selected.GetComponent<ComponentInitialization>().pos ,selected.GetComponent<ComponentInitialization>().neg);
            currentText.text = "Current: " + currentExport.Value;
            //print(selected.name);
        };

        // Run the simulation
        dc.Run(CircuitManager.ckt);

    }

    public void DeleteComponent()
    {
        CircuitManager.componentList.Remove(selected);
        Destroy(selected);
    }



}