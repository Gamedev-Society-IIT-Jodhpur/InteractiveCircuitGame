using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingGround : MonoBehaviour
{
    List<GameObject> cmplist = CircuitManager.componentList;
    List<List<GameObject>> circuits= new List<List<GameObject>>() { };

    // Start is called before the first frame update
    void Start()
    {

    }

    public void CheckGround()
    {

        foreach (var item in cmplist)
        {
            int placed = 0;
            for (int i = 0; i < circuits.Count; i++)
            {
                for (int j = i + 1; j < circuits.Count; j++)
                {
                    if (Itemincircuit(item, i) && Itemincircuit(item, j))
                    {
                        circuits[i].Add(item);
                        placed = 1;
                        Merge(i, j);
                        j = j - 1;


                    }
                }
            }

            if (placed == 0)
            {
                circuits.Add(new List<GameObject>() { item });
            }
        }

        Groundit();
    }

    private bool Itemincircuit(GameObject item, int i)
    {
        bool Value = false;
        foreach (var obj in circuits[i])
        {
            if ((obj.GetComponent<ComponentInitialization>().pos == item.GetComponent<ComponentInitialization>().pos) ||
                    (obj.GetComponent<ComponentInitialization>().pos == item.GetComponent<ComponentInitialization>().neg) ||
                    (obj.GetComponent<ComponentInitialization>().neg == item.GetComponent<ComponentInitialization>().pos) ||
                    (obj.GetComponent<ComponentInitialization>().neg == item.GetComponent<ComponentInitialization>().neg))
            {
                Value = true;
            }
        }
        return Value;
    }

    private void Merge(int i, int j)
    {
        
        circuits[i].AddRange(circuits[j]);
        circuits.RemoveAt(j);
    }

    private void Groundit()
    {
        foreach (var item in circuits)
        {
            bool gnd = false;
            foreach (var obj in item)
            {
                if (obj.GetComponent<ComponentInitialization>().pos == "0" ||
                    obj.GetComponent<ComponentInitialization>().neg == "0")
                {
                    gnd = true;
                    break;
                }
            }
            if (gnd == false)
            {
                var tempNode = item[0].GetComponent<ComponentInitialization>().neg;
                item[0].GetComponent<ComponentInitialization>().neg = "0";
                foreach (var obj in item)
                {
                    if (obj.GetComponent<ComponentInitialization>().pos == tempNode)
                    {
                        obj.GetComponent<ComponentInitialization>().pos = "0";
                    }
                    if (obj.GetComponent<ComponentInitialization>().neg == tempNode)
                    {
                        obj.GetComponent<ComponentInitialization>().neg = "0";
                    }
                }
            }
        }
    } }
