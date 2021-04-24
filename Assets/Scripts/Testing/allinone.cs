using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class allinone : MonoBehaviour

{
    // Start is called before the first frame update

    Circuit sim;
    Resistor res1;
    DCVoltageSource volt0;

    void Start()
    {
        sim = new Circuit();
        res1= sim.Create<Resistor>(100);
        volt0=sim.Create<DCVoltageSource>(50);
        sim.Connect(res1, 0, volt0, 0);
        sim.Connect(volt0, 1, res1, 1);
        

    }

    // Update is called once per frame
    void Update()
    {
        sim.analyze();
        sim.doTick();
                Debug.Log(res1.getCurrent());
    }
}
