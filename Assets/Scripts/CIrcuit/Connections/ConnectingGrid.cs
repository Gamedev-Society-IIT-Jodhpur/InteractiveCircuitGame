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

    // Start is called before the first frame update
    void Start()
    {
        nodes = GetComponent<grid>().nodes;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectGrid()
    {
        
    }
}
