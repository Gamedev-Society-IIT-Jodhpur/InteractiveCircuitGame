using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        Transform name = item.transform.Find("Name");
        Transform componenetName = name.transform.Find("ComponentName");
        componenetName.GetComponent<TextMeshProUGUI>().text = "NewCompoenent";
        item.transform.SetParent(this.transform);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
