using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData : MonoBehaviour
{
    public static List<GameObject> componentList;


    public struct ComponentValidate 
    {
        public int isSeries;
        public List<double> V, I;
        public string ctype;
        public List<string> nodes;
        public string componentID;
        public  string value;
        public int beta;
    };
    public struct series
    {


        public List<string> components;

    };
    public struct node
    {
        public string nodeID;
        public List<string> attached;
    };
    public struct ComponentData //to save items in inventory
    {
        public string name;
        public string value;
        public string unit;
        public int quantity;
    }
    public static List<ComponentData> Inventory;
    public static bool isSoldering = false;
    public static float dragThreshold = 0.01f;



    private void Start()
    {
        Inventory = new List<ComponentData>();
        ComponentData res1 = new ComponentData();
        res1.name = "resistor";
        res1.value = "1.5";
        res1.unit = "Ohm";
        res1.quantity = 20;

        ComponentData battery9v = new ComponentData();
        battery9v.name = "voltage9";
        battery9v.value = "9";
        battery9v.unit = "V";
        battery9v.quantity = 3;

        ComponentData battery15v = new ComponentData();
        battery15v.name = "voltage1.5";
        battery15v.value = "1.5";
        battery15v.unit = "V";
        battery15v.quantity = 3;

        ComponentData breadboard = new ComponentData();
        breadboard.name = "breadboard";
        breadboard.value = "";
        breadboard.unit = "";
        breadboard.quantity = 2;

        Inventory.Add(res1);
        Inventory.Add(battery9v);
        Inventory.Add(battery15v);
        Inventory.Add(breadboard);
    }

    public static void UpdateComponentList()
    {
        componentList = CircuitManager.componentList;
    }


}
