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
        public string value;
        public double beta;
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
        public string price;
        public int quantity;
    }

    public static List<ComponentData> Inventory = new List<ComponentData>();
    public static bool isSoldering = false;
    public static float dragThreshold = 0.01f;
    public static bool isSolderingIron;
    public static bool hasSolderBroken = false;


    private void Awake()
    {
        //UpdateSolderingIron();
    }

    private void Start()
    {
        print("isSolderingIron"+isSolderingIron);

    //    #region Custom Inventory Items
    //    Inventory = new List<ComponentData>();
    //    ComponentData res1 = new ComponentData();
    //    res1.name = "resistor";
    //    res1.value = "15";
    //    res1.unit = "Ohm";
    //    res1.quantity = 20;

    //    ComponentData res2 = new ComponentData();
    //    res2.name = "resistor";
    //    res2.value = "2.5";
    //    res2.unit = "Ohm";
    //    res2.quantity = 20;

    //    ComponentData battery9v = new ComponentData();
    //    battery9v.name = "voltage9";
    //    battery9v.value = "9";
    //    battery9v.unit = "V";
    //    battery9v.quantity = 3;

    //    ComponentData battery15v = new ComponentData();
    //    battery15v.name = "voltage1.5";
    //    battery15v.value = "1.5";
    //    battery15v.unit = "V";
    //    battery15v.quantity = 3;

    //    ComponentData BJTpnp = new ComponentData();
    //    BJTpnp.name = "bjtpnp";
    //    BJTpnp.value = "100";
    //    BJTpnp.unit = "β";
    //    BJTpnp.quantity = 3;

    //    ComponentData BJTnpn = new ComponentData();
    //    BJTnpn.name = "bjtnpn";
    //    BJTnpn.value = "100";
    //    BJTnpn.unit = "β";
    //    BJTnpn.quantity = 3;

    //    ComponentData Diode = new ComponentData();
    //    Diode.name = "diode";
    //    Diode.value = "Default";
    //    Diode.unit = "";
    //    Diode.quantity = 3;

    //    ComponentData ZenerDiode = new ComponentData();
    //    ZenerDiode.name = "zenerDiode";
    //    ZenerDiode.value = "zenerDiode";
    //    ZenerDiode.unit = "";
    //    ZenerDiode.quantity = 3;

    //    ComponentData breadboard = new ComponentData();
    //    breadboard.name = "breadboard";
    //    breadboard.value = "";
    //    breadboard.unit = "";
    //    breadboard.quantity = 1;

    //    Inventory.Add(res1);
    //    Inventory.Add(battery9v);
    //    Inventory.Add(battery15v);
    //    Inventory.Add(breadboard);
    //    Inventory.Add(BJTnpn);
    //    Inventory.Add(BJTpnp);
    //    Inventory.Add(Diode);
    //    Inventory.Add(ZenerDiode);
    //    Inventory.Add(res2);
    //    #endregion
    }


    public static void UpdateComponentList()
    {
        componentList = CircuitManager.componentList;
    }


    //TODO call this function when we need to show if soldering iron is present in tinker or not.
    //also change issolderingiron to true if player buys soldering iron from shop. 
    public static void UpdateSolderingIron()
    {
        if (Mathf.RoundToInt(Random.Range(0, 2)) == 0)
        {
            isSolderingIron = true;
        }
        else
        {
            isSolderingIron = false;
        }
        print(isSolderingIron);
    }
}
