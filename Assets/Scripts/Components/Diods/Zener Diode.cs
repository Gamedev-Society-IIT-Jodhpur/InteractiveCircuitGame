using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class ZenerDiode : MonoBehaviour
{
    // Start is called before the first frame update
    public ZenerElm zener;public GameObject  CIrcuitSim;
    public CIrcuitSim Sim;
    void Awake()
    {
        CIrcuitSim= GameObject.FindGameObjectWithTag("CIrcuitSim");
        Sim=CIrcuitSim.GetComponent<CIrcuitSim>();

        zener=Sim.sim.Create<ZenerElm>();
    }

    // Update is called once per frame
    
}
