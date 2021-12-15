using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WireManager : MonoBehaviour
{
    [SerializeField] GameObject wire;
    [SerializeField] GameObject newWireManager;
    public static bool isDrawingWire=false;
    [SerializeField] GameObject wireNode;
    RaycastHit2D hit;
    Vector2 worldPoint;


    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Shop");
        }


        worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(1))
        {
            hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null && hit.collider.tag == "Wire")
            {
                hit.collider.GetComponentInParent<NewWireManager>().DestroyWire();
            }
        }

    }

    public void DrawWire(Transform node)
    {
        isDrawingWire = true;
        GameObject newWire = Instantiate<GameObject>(wire);
        newWire.GetComponent<Wire>().node1 = node;
        GameObject newNewWireManager = Instantiate<GameObject>(newWireManager);
        newNewWireManager.transform.SetParent(gameObject.transform);
        newNewWireManager.GetComponent<NewWireManager>().nodes.Add(node);
        newWire.transform.SetParent(newNewWireManager.transform);
        node.GetComponent<NodeTinker>().wires.Add(newWire);
        
        

    }
}
