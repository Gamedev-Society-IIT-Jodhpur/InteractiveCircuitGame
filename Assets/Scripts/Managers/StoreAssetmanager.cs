using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
public class StoreAssetmanager : MonoBehaviour
{

    public static StoreAssetmanager Instance { get; private set; }

    public JSONNode itemsAvailable;

    public Dictionary<string, string> itemsNameMaping = new Dictionary<string, string>();

    [System.Serializable]
    public struct StoreItemIcon
    {
        public string name;
        public Texture image;
    }

    public List<StoreItemIcon> storeItemIcons;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        itemsNameMaping.Add("Resistor", "resistor");
        itemsNameMaping.Add("Battery 9.0", "voltage9");
        itemsNameMaping.Add("Battery 1.5", "voltage1.5");
        itemsNameMaping.Add("BJT PNP", "bjtpnp");
        itemsNameMaping.Add("BJT NPN", "bjtnpn");
        itemsNameMaping.Add("Diode", "diode");
        itemsNameMaping.Add("Zener Diode", "zenerDiode");
        itemsNameMaping.Add("Breadboard", "breadboard");
    }

    public Texture getItemIcon(string name)
    {
        //int index = storeItemIcons.FindIndex(a => a.name.Contains(name));
        int index = storeItemIcons.FindIndex(a => name.Contains(a.name));
        Debug.Log("Index : " + index);
        return storeItemIcons[index].image;
    }



    //public string getitemname(string name)
    //{

    //}
}
