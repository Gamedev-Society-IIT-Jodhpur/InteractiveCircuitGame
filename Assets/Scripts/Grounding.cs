using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class Grounding : MonoBehaviour
{
    // Start is called before the first frame update
    public Ground ground;
    public GameObject  CIrcuitSim;
    public CIrcuitSim Sim;
    void Awake()
    {
        CIrcuitSim= GameObject.FindGameObjectWithTag("CIrcuitSim");
        Sim=CIrcuitSim.GetComponent<CIrcuitSim>();

        ground = Sim.sim.Create<Ground>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
