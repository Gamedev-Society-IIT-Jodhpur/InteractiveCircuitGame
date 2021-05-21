using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class MynkTestConnect : MonoBehaviour
{
    GameObject CircuitSim;
    CircuitSim Sim;
    //DCVoltageSource dcBattery;
    [SerializeField]ResistorComponent resistor;
    /*[SerializeField] GameObject node1;
    [SerializeField] GameObject node2;
    WireObject node1Wire;
    WireObject node2Wire;
    GameObject node1Child;
    GameObject node2Child;*/
    //Wire wire;
    // Start is called before the first frame update
    void Start()
    {
        CircuitSim = GameObject.FindGameObjectWithTag("CircuitSim");
        Sim = CircuitSim.GetComponent<CircuitSim>();
        //dcBattery=Sim.sim.Create<DCVoltageSource>(10);
        /*node1Child = node1.transform.GetChild(0).gameObject;
        node1Child.SetActive(true);
        node2Child = node2.transform.GetChild(0).gameObject;
        node2Child.SetActive(true);
        node1Wire = node1Child.GetComponent<WireObject>();
        node2Wire = node2Child.GetComponent<WireObject>();
        //wire = Sim.sim.Create<Wire>();

        Sim.sim.Connect(dcBattery.leadPos,resistor.resistor.leadIn);
        Sim.sim.Connect(resistor.resistor.leadOut,node2Wire.wire.leadIn);
        Sim.sim.Connect(node1Wire.wire.leadOut, dcBattery.leadNeg);*/

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        /*node1Child = node1.transform.GetChild(0).gameObject;
        node1Child.SetActive(true);
        node2Child = node2.transform.GetChild(0).gameObject;
        node2Child.SetActive(true);
        node1Wire = node1Child.GetComponent<WireObject>();
        node2Wire = node2Child.GetComponent<WireObject>();
        //wire = Sim.sim.Create<Wire>();

        Sim.sim.Connect(dcBattery.leadPos, resistor.resistor.leadIn);
        Sim.sim.Connect(resistor.resistor.leadOut, node2Wire.wire.leadIn);
        Sim.sim.Connect(node1Wire.wire.leadOut, dcBattery.leadNeg);*/

        Sim.sim.doTick();
        print(resistor.resistor.getCurrent());
    }
}
