using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class Grounding : MonoBehaviour
{
    // Start is called before the first frame update
    public Ground ground;
    void Awake()
    {
        ground = CIrcuitSim.sim.Create<Ground>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
