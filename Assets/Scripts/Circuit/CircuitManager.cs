using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Simulations;

public class CircuitManager : MonoBehaviour
{
    public static Circuit ckt;
    public static Dictionary<string, int> components ;
    public static List<GameObject> componentList;
    
    //private IList<string> componentList = new List<string>(){"Voltage","Resistor"};

    private void Awake(){
        ckt = new Circuit();
        components = new Dictionary<string, int>();
        componentList = new List<GameObject>();
      /*  foreach (var item in componentList)
        {
            item.GetComponent<Try>().Initialize();
        }*/

      
        //  components.Add("Voltage",1);
        //   components["Voltage"]=1;
        //    print(components["Voltage"]);
        // componentList=["Vlotage"];



    }
    void Update()
    {
        foreach (var item in componentList)
        {
            Debug.Log("inside update");
            item.GetComponent<ComponentInitialization>().Initialize();
        }
    }


}
