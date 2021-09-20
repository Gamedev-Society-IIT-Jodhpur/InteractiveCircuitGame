using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] List<GameObject> components;
    public Dictionary<string, GameObject> componentsDict;

    public string component;
    public string value;
    public int quantity;
    Text[] childs;

    private void Start()
    {
        componentsDict = new Dictionary<string, GameObject>(){
            { "9V Battery",components[0]},
            { "Breadboard",components[1]},
            { "Led",components[2]},
            { "Resistor",components[3]}/*,
            { "1.5V Battery",components[4]}*/
        };
        childs = GetComponentsInChildren<Text>();

    }

    public void Instantiate()
    {
        Instantiate(componentsDict[component]);
        if (quantity == 1)
        {
            Destroy(gameObject);
        }
        else
        {
            quantity -= 1;
            childs[0].text = quantity.ToString();
            
        }
    }


}
