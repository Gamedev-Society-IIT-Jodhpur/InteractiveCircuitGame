using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] GameObject wire;
    GameObject newWire;
    SimArea simArea;
    // Start is called before the first frame update
    void Start()
    {
        simArea = GetComponentInParent<SimArea>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDrawingWire()
    {
        newWire = Instantiate(wire, gameObject.transform.position,Quaternion.identity);
        newWire.transform.SetParent(simArea.transform);
        newWire.transform.localScale = simArea.transform.localScale;
        newWire.transform.SetAsFirstSibling();
        newWire.GetComponent<WireComponent>().Draw();
    }
}
