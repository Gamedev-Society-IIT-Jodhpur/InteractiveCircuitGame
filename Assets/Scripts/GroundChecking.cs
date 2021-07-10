using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GroundChecking : MonoBehaviour
{
    // Start is called before the first frame update
    List<GameObject> cmplist = CircuitManager.componentList;
    List<List<string>> circuits = new List<List<string>>() { };
    void Start()
    {
        
    }

    // Update is called once per frame
    public void CheckGround()
    {

        foreach (var item in cmplist)
        {
            int placed = 0;
            for (int i = 0; i < circuits.Count; i++)
            {
                for (int j = i + 1; j < circuits.Count; j++)
                {
                    if (Nodeincircuit(item, i) && Nodeincircuit(item, j))
                    {
                        circuits[i].Add( item.GetComponent<ComponentInitialization>().pos );
                        circuits[i].Add(item.GetComponent<ComponentInitialization>().neg);
                        placed = 1;
                        Merge(i, j);
                        j = j - 1;


                    }
                }
            }

            if (placed == 0)
            {
                circuits.Add(new List<string>() { item.GetComponent<ComponentInitialization>().pos , item.GetComponent<ComponentInitialization>().neg });
            }
        }

        Groundit();
    }

    private bool Nodeincircuit(GameObject item, int i)
    {
        var l = new List<string>() { item.GetComponent<ComponentInitialization>().pos, item.GetComponent<ComponentInitialization>().neg };
        if (l.Intersect(circuits[i]).ToList().Count>0)
        {
            return true;
        }

        return false;
        

    }

    private void Merge(int i, int j)
    {
        circuits[i] = circuits[i].Union(circuits[j]).ToList();
        circuits.RemoveAt(j);

           
    }

    private void Groundit()
    {
        int i = 0;
        foreach(var item in circuits)
        {
            UnifiedScript.WireInitialize("GroundingWire" + i, item[0], "0", "0");
            i++;
        }
    }
}
