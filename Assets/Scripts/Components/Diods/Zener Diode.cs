using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class ZenerDiode : MonoBehaviour
{
    // Start is called before the first frame update
    public ZenerElm zener;
    void Awake()
    {
        zener=CIrcuitSim.sim.Create<ZenerElm>();
    }

    // Update is called once per frame
    
}
