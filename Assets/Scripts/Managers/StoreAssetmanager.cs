using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;
public class StoreAssetmanager : MonoBehaviour
{

    public static StoreAssetmanager Instance { get; private set; }

    public JSONNode itemsAvailable;

    public Dictionary<string, string> itemsNameMaping = new Dictionary<string, string>();

    [System.Serializable]
    public struct StoreItemIcon
    {
        public string name;
        public Sprite image;
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
        itemsNameMaping.Add("Battery9", "voltage9");
        itemsNameMaping.Add("Battery1.5", "voltage1.5");
        itemsNameMaping.Add("BJT PNP", "bjtpnp");
        itemsNameMaping.Add("BJT NPN", "bjtnpn");
        itemsNameMaping.Add("Diode", "diode");
        itemsNameMaping.Add("Zener Diode", "zenerDiode");
        itemsNameMaping.Add("Breadboard", "breadboard");
    }
    private void Start()
    {
        ScoringScript.UpdateError(2);
    }
    public Sprite getItemIcon(string name)
    {
        int index = storeItemIcons.FindIndex(a => string.Equals(name, a.name));

        return storeItemIcons[index].image;
    }
}
