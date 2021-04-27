using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : MonoBehaviour
{
    [SerializeField] GameObject wireComponent;
    GameObject newWire;
    SimArea simArea;
    

    void Start()
    {
        simArea = GetComponentInParent<SimArea>();
    }

    public void StartDrawingWire()
    {
        newWire = Instantiate(wireComponent, gameObject.transform.position,Quaternion.identity);
        newWire.transform.SetParent(simArea.transform);
        newWire.transform.localScale = simArea.transform.localScale;
        newWire.transform.SetAsFirstSibling();
        newWire.GetComponent<WireComponent>().Draw();
    }
}
