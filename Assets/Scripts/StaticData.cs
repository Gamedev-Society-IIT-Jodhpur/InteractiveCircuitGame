using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData : MonoBehaviour
{
    public static List<GameObject> componentList;

    public struct Component
    {
        public string name;
        public string value;
        public string unit;
        public int quantity;
    }
    public static List<Component> Inventory;

    private void Start()
    {
        Inventory = new List<Component>();
        Component res1 = new Component();
        res1.name = "resistor";
        res1.value = "1.5";
        res1.unit = "Ohm";
        res1.quantity = 5;

        Component battery9v = new Component();
        battery9v.name = "voltage9";
        battery9v.value = "9";
        battery9v.unit = "V";
        battery9v.quantity = 3;

        Component breadboard = new Component();
        breadboard.name = "breadboard";
        breadboard.value = "";
        breadboard.unit = "";
        breadboard.quantity = 1;

        Inventory.Add(res1);
        Inventory.Add(battery9v);
        Inventory.Add(breadboard);
    }

    public static void UpdateComponentList()
    {
        componentList = CircuitManager.componentList;
    }

}
