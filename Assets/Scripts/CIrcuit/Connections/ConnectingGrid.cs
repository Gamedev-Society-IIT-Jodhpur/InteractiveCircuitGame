using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpCircuit;

public class ConnectingGrid : MonoBehaviour
{
    enum Connection
    {
        Horizontal,
        Vertical,
    }
    [SerializeField] Connection connection;
    List<List<GameObject>> nodes;
   // public List<GameObject> node;
    GameObject CircuitSim;
    CircuitSim Sim;
    int height;
    int width;
    [SerializeField] DCBattery dcBattery;
    [SerializeField] ResistorComponent resistor;
    [SerializeField] WireObject Wire;
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        CircuitSim = GameObject.FindGameObjectWithTag("CircuitSim");
        Sim = CircuitSim.GetComponent<CircuitSim>();
        nodes = GetComponent<grid>().nodes;
        //node.Add(nodes[0][0]);
        //node.Add(nodes[1][0]);
        height = GetComponent<grid>().height;
        width = GetComponent<grid>().width;
        //ConnectGrid();


        /*Sim.sim.Connect(nodes[0][0].GetComponent<WireObject>().wire.leadIn, dcBattery.DCVolt.leadPos);
        Sim.sim.Connect(nodes[0][0].GetComponent<WireObject>().wire.leadOut, resistor.resistor.leadIn);
        Sim.sim.Connect(dcBattery.DCVolt.leadNeg, resistor.resistor.leadOut);*/
        //Sim.sim.Connect(Wire.wire.leadIn, dcBattery.DCVolt.leadPos);
        Sim.sim.Connect(dcBattery.DCVolt.leadPos, Wire.wire.leadIn);
        Sim.sim.Connect(Wire.wire.leadOut, resistor.resistor.leadIn);
        Sim.sim.Connect(dcBattery.DCVolt.leadNeg, resistor.resistor.leadOut);

        //Sim.sim.Connect(volt0.DCVolt.leadPos, wire.wire.leadIn);
        //Sim.sim.Connect(wire.wire.leadOut, res1.resistor.leadIn);
        //Sim.sim.Connect(volt0.DCVolt.leadNeg, res1.resistor.leadOut);
        
        //Sim.sim.Connect(nodes[1][0].GetComponent<WireObject>().wire.leadOut, resistor.resistor.leadOut);
        //Sim.sim.Connect(nodes[1][0].GetComponent<WireObject>().wire.leadIn, dcBattery.DCVolt.leadNeg);
        //Sim.sim.Connect(nodes[0][0].GetComponent<Node>().wire.leadOut, nodes[0][1].GetComponent<Node>().wire.leadIn);
        //Sim.sim.Connect(nodes[1][0].GetComponent<Node>().wire.leadOut, nodes[1][1].GetComponent<Node>().wire.leadIn);

    }

    // Update is called once per frame
    void Update()
    {
        //Sim.sim.doTick();
        //Debug.Log(resistor.resistor.getCurrent());
    }

    public void ConnectGrid()
    {
        if (connection == Connection.Vertical)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height - 1; j++)
                {
                    //print(nodes[i][j].name);
                    Sim.sim.Connect(nodes[i][j].GetComponent<WireObject>().wire.leadOut, nodes[i][j + 1].GetComponent<WireObject>().wire.leadIn);
                }
            }
        }
        else
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width - 1; j++)
                {
                    Sim.sim.Connect(nodes[j][i].GetComponent<WireObject>().wire.leadOut, nodes[j + 1][i].GetComponent<WireObject>().wire.leadIn);
                }
            }
        }
        
    }
}
