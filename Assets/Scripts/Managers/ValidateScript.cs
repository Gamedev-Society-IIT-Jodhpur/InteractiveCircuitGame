using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Simulations;
using System.Linq;

public class ValidateScript : MonoBehaviour
{
    [SerializeField]
    GameObject circuitManager;

    static Dictionary<string, StaticData.node> NodedataFalstad = new Dictionary<string, StaticData.node>();
    static Dictionary<string, StaticData.ComponentValidate> ComponentdataFalstad = new Dictionary<string, StaticData.ComponentValidate>();
    static List<StaticData.series> serieslistFalstad = new List<StaticData.series>();
    static Dictionary<string, List<string>> NotSeriesFalstad = new Dictionary<string, List<string>>();
    static Dictionary<string, StaticData.node> NodedataTinker = new Dictionary<string, StaticData.node>();
    static Dictionary<string, StaticData.ComponentValidate> ComponentdataTinker = new Dictionary<string, StaticData.ComponentValidate>();
    static List<StaticData.series> serieslistTinker = new List<StaticData.series>();
    static Dictionary<string, List<string>> NotSeriesTinker = new Dictionary<string, List<string>>();

    public void Validate()
    {

    }

    public void SaveDataFalstad()
    {
        circuitManager.GetComponent<CircuitManager>().Play();



        //All nodes id are updated
        HashSet<StaticData.node> nodeList = new HashSet<StaticData.node>();
        for (int i = 0; i < CircuitManager.componentList.Count; i++)

        {
            HashSet<StaticData.node> componentnodes = new HashSet<StaticData.node>(); ;
            foreach (var j in CircuitManager.componentList[i].GetComponent<ComponentInitialization>().nodes)
            {
                var newnode = new StaticData.node();
                newnode.nodeID = j;
                newnode.attached = new List<string>();
                componentnodes.Add(newnode);
            }
            nodeList.UnionWith(componentnodes);
        }

        foreach (var i in nodeList)
        {

            NodedataFalstad[i.nodeID] = i;

        }

        //Creating ComponentData as well as updating it in Nodedata 
        var dc = new DC("dc", CircuitManager.volt.GetComponent<ComponentInitialization>().nameInCircuit, double.Parse(CircuitManager.volt.GetComponent<ComponentInitialization>().value), double.Parse(CircuitManager.volt.GetComponent<ComponentInitialization>().value), 0.001);

        var currentExport = new List<RealPropertyExport>();
        for (int i = 0; i < CircuitManager.componentList.Count; i++)
        {
            currentExport.Add(new RealPropertyExport(dc, CircuitManager.componentList[i].GetComponent<ComponentInitialization>().nameInCircuit, "i"));

        }


        dc.ExportSimulationData += (sender, exportDataEventArgs) =>
        {

            for (int i = 0; i < CircuitManager.componentList.Count; i++)
            {
                var newcomp = new StaticData.ComponentValidate();
                newcomp.V = new List<double>();
                newcomp.I = new List<double>();
                if (CircuitManager.componentList[i].GetComponent<ComponentInitialization>().a != CircuitManager.component.bjt && CircuitManager.componentList[i].GetComponent<ComponentInitialization>().a != CircuitManager.component.resistor)
                {

                    newcomp.V.Add(exportDataEventArgs.GetVoltage(CircuitManager.componentList[i].GetComponent<ComponentInitialization>().nodes[0], CircuitManager.componentList[i].GetComponent<ComponentInitialization>().nodes[1]));

                    newcomp.I.Add(currentExport[i].Value);
                }
                else if (CircuitManager.componentList[i].GetComponent<ComponentInitialization>().a == CircuitManager.component.resistor)
                {
                    newcomp.V.Add(Math.Abs(exportDataEventArgs.GetVoltage(CircuitManager.componentList[i].GetComponent<ComponentInitialization>().nodes[0], CircuitManager.componentList[i].GetComponent<ComponentInitialization>().nodes[1])));

                    newcomp.I.Add(Math.Abs(currentExport[i].Value));
                }
                else
                {
                    var vbe = new RealPropertyExport(dc, CircuitManager.componentList[i].GetComponent<ComponentInitialization>().nameInCircuit, "vbe");
                    var vbc = new RealPropertyExport(dc, CircuitManager.componentList[i].GetComponent<ComponentInitialization>().nameInCircuit, "vbc");
                    var ib = new RealPropertyExport(dc, CircuitManager.componentList[i].GetComponent<ComponentInitialization>().nameInCircuit, "ib");
                    var ic = new RealPropertyExport(dc, CircuitManager.componentList[i].GetComponent<ComponentInitialization>().nameInCircuit, "ic");

                    newcomp.V.Add(vbe.Value);
                    newcomp.V.Add(vbc.Value);
                    newcomp.I.Add(ib.Value);
                    newcomp.I.Add(ic.Value);
                }



                newcomp.ctype = CircuitManager.componentList[i].GetComponent<ComponentInitialization>().a.ToString();
                newcomp.nodes = CircuitManager.componentList[i].GetComponent<ComponentInitialization>().nodes;
                newcomp.componentID = CircuitManager.componentList[i].GetComponent<ComponentInitialization>().nameInCircuit;
                newcomp.isSeries = -1;

                ComponentdataFalstad[newcomp.componentID] = newcomp;
                foreach (var k in CircuitManager.componentList[i].GetComponent<ComponentInitialization>().nodes)
                {
                    NodedataFalstad[k].attached.Add(newcomp.componentID);
                }
            }
        };
        dc.Run(CircuitManager.ckt);



        // Creating Series list

        foreach (var i in NodedataFalstad)
        {
            if (i.Value.attached.Count == 2)
            {
                if (ComponentdataFalstad[i.Value.attached[0]].isSeries == -1 && ComponentdataFalstad[i.Value.attached[1]].isSeries == -1)
                {
                    print(ComponentdataFalstad[i.Value.attached[0]].isSeries + "Before update");
                    var newseries = new StaticData.series();
                    newseries.components = new List<string>();
                    newseries.components.Add(i.Value.attached[0]);
                    newseries.components.Add(i.Value.attached[1]);
                    var temp = ComponentdataFalstad[i.Value.attached[0]];

                    temp.isSeries = serieslistFalstad.Count;

                    ComponentdataFalstad[i.Value.attached[0]] = temp;
                    var temp1 = ComponentdataFalstad[i.Value.attached[1]];
                    temp1.isSeries = serieslistFalstad.Count;
                    ComponentdataFalstad[i.Value.attached[1]] = temp1;
                    serieslistFalstad.Add(newseries);
                    print(ComponentdataFalstad[i.Value.attached[0]].isSeries + "After update");
                }
                else if (ComponentdataFalstad[i.Value.attached[0]].isSeries != -1 && ComponentdataFalstad[i.Value.attached[1]].isSeries == -1)
                {
                    StaticData.series t = new StaticData.series();
                    t.components = serieslistFalstad[ComponentdataFalstad[i.Value.attached[0]].isSeries].components;
                    t.components = t.components.Union(new List<string> { i.Value.attached[1] }).ToList();
                    serieslistFalstad[ComponentdataFalstad[i.Value.attached[0]].isSeries] = t;
                    var temp = ComponentdataFalstad[i.Value.attached[1]];
                    temp.isSeries = ComponentdataFalstad[i.Value.attached[0]].isSeries;
                    ComponentdataFalstad[i.Value.attached[1]] = temp;
                }
                else if (ComponentdataFalstad[i.Value.attached[0]].isSeries == -1 && ComponentdataFalstad[i.Value.attached[1]].isSeries != -1)
                {
                    StaticData.series t = new StaticData.series();
                    t.components = serieslistFalstad[ComponentdataFalstad[i.Value.attached[1]].isSeries].components;
                    t.components = t.components.Union(new List<string> { i.Value.attached[0] }).ToList();
                    serieslistFalstad[ComponentdataFalstad[i.Value.attached[1]].isSeries] = t;
                    var temp = ComponentdataFalstad[i.Value.attached[0]];
                    temp.isSeries = ComponentdataFalstad[i.Value.attached[1]].isSeries;
                    ComponentdataFalstad[i.Value.attached[0]] = temp;
                }
                else
                {
                    if (ComponentdataFalstad[i.Value.attached[0]].isSeries == ComponentdataFalstad[i.Value.attached[1]].isSeries)
                    {
                        continue;
                    }
                    else
                    {
                        int max = Math.Max(ComponentdataFalstad[i.Value.attached[0]].isSeries, ComponentdataFalstad[i.Value.attached[1]].isSeries);
                        int min = Math.Min(ComponentdataFalstad[i.Value.attached[0]].isSeries, ComponentdataFalstad[i.Value.attached[1]].isSeries);

                        foreach (var item in serieslistFalstad[max].components)
                        {
                            var temp = ComponentdataFalstad[item];
                            temp.isSeries = min;
                            ComponentdataFalstad[item] = temp;
                            serieslistFalstad[min].components.Add(item);
                        }
                        for (int k = max + 1; k <= serieslistFalstad.Count; k++)
                        {
                            foreach (var item in serieslistFalstad[k].components)
                            {
                                var temp = ComponentdataFalstad[item];
                                temp.isSeries = temp.isSeries - 1;
                                ComponentdataFalstad[item] = temp;
                            }
                        }

                        serieslistFalstad.RemoveAt(max);
                    }
                }
            }
            else
            {
                continue;
            }
        }

        // Creating Non-Series Dictionary
        foreach (var i in ComponentdataFalstad)
        {
            if (i.Value.isSeries == -1)
            {
                if (NotSeriesFalstad.ContainsKey(i.Value.ctype))
                {
                    NotSeriesFalstad[i.Value.ctype].Add(i.Key);
                }
                else
                {
                    NotSeriesFalstad[i.Value.ctype] = new List<string>();
                    NotSeriesFalstad[i.Value.ctype].Add(i.Key);
                }
            }
        }

        for (int i = serieslistFalstad.Count - 1; i >= 0; i--)
        {
            for (int j = serieslistFalstad[i].components.Count - 1; j >= 0; j--)
            {
                if (ComponentdataFalstad[serieslistFalstad[i].components[j]].ctype == "wire")
                {
                    serieslistFalstad[i].components.RemoveAt(j);
                }
            }
            if (serieslistFalstad[i].components.Count == 1)
            {
                if (NotSeriesFalstad.ContainsKey(ComponentdataFalstad[serieslistFalstad[i].components[0]].ctype))
                {
                    NotSeriesFalstad[ComponentdataFalstad[serieslistFalstad[i].components[0]].ctype].Add(serieslistFalstad[i].components[0]);
                }
                else
                {
                    NotSeriesFalstad[ComponentdataFalstad[serieslistFalstad[i].components[0]].ctype] = new List<string>();
                    NotSeriesFalstad[ComponentdataTinker[serieslistFalstad[i].components[0]].ctype].Add(serieslistFalstad[i].components[0]);
                }

                serieslistFalstad.RemoveAt(i);
            }
        }
    }

    public void SaveDataTinker()
    {
        circuitManager.GetComponent<CircuitManagerTinker>().Play();



        //All nodes id are updated
        HashSet<StaticData.node> nodeList = new HashSet<StaticData.node>();
        for (int i = 0; i < CircuitManagerTinker.componentList.Count; i++)

        {
            HashSet<StaticData.node> componentnodes = new HashSet<StaticData.node>(); ;
            foreach (var j in CircuitManagerTinker.componentList[i].GetComponent<ComponentTinker>().nodes)
            {
                var newnode = new StaticData.node();
                newnode.nodeID = j;
                newnode.attached = new List<string>();
                componentnodes.Add(newnode);
            }
            nodeList.UnionWith(componentnodes);
        }

        foreach (var i in nodeList)
        {

            NodedataTinker[i.nodeID] = i;

        }

        //Creating ComponentData as well as updating it in Nodedata 
        var dc = new DC("dc", CircuitManagerTinker.volt.GetComponent<ComponentTinker>().nameInCircuit, double.Parse(CircuitManagerTinker.volt.GetComponent<ComponentTinker>().value), double.Parse(CircuitManagerTinker.volt.GetComponent<ComponentTinker>().value), 0.001);

        var currentExport = new List<RealPropertyExport>();
        for (int i = 0; i < CircuitManagerTinker.componentList.Count; i++)
        {
            currentExport.Add(new RealPropertyExport(dc, CircuitManagerTinker.componentList[i].GetComponent<ComponentTinker>().nameInCircuit, "i"));

        }


        dc.ExportSimulationData += (sender, exportDataEventArgs) =>
        {

            for (int i = 0; i < CircuitManagerTinker.componentList.Count; i++)
            {
                var newcomp = new StaticData.ComponentValidate();
                newcomp.V = new List<double>();
                newcomp.I = new List<double>();
                if (CircuitManagerTinker.componentList[i].GetComponent<ComponentTinker>().a != CircuitManagerTinker.component.bjt && CircuitManagerTinker.componentList[i].GetComponent<ComponentTinker>().a != CircuitManagerTinker.component.resistor)
                {

                    newcomp.V.Add(exportDataEventArgs.GetVoltage(CircuitManagerTinker.componentList[i].GetComponent<ComponentTinker>().nodes[0], CircuitManagerTinker.componentList[i].GetComponent<ComponentTinker>().nodes[1]));

                    newcomp.I.Add(currentExport[i].Value);
                }
                else if (CircuitManagerTinker.componentList[i].GetComponent<ComponentTinker>().a == CircuitManagerTinker.component.resistor)
                {
                    newcomp.V.Add(Math.Abs(exportDataEventArgs.GetVoltage(CircuitManagerTinker.componentList[i].GetComponent<ComponentTinker>().nodes[0], CircuitManagerTinker.componentList[i].GetComponent<ComponentTinker>().nodes[1])));

                    newcomp.I.Add(Math.Abs(currentExport[i].Value));
                }
                else
                {
                    var vbe = new RealPropertyExport(dc, CircuitManagerTinker.componentList[i].GetComponent<ComponentTinker>().nameInCircuit, "vbe");
                    var vbc = new RealPropertyExport(dc, CircuitManagerTinker.componentList[i].GetComponent<ComponentTinker>().nameInCircuit, "vbc");
                    var ib = new RealPropertyExport(dc, CircuitManagerTinker.componentList[i].GetComponent<ComponentTinker>().nameInCircuit, "ib");
                    var ic = new RealPropertyExport(dc, CircuitManagerTinker.componentList[i].GetComponent<ComponentTinker>().nameInCircuit, "ic");

                    newcomp.V.Add(vbe.Value);
                    newcomp.V.Add(vbc.Value);
                    newcomp.I.Add(ib.Value);
                    newcomp.I.Add(ic.Value);
                }



                newcomp.ctype = CircuitManagerTinker.componentList[i].GetComponent<ComponentTinker>().a.ToString();
                newcomp.nodes = CircuitManagerTinker.componentList[i].GetComponent<ComponentTinker>().nodes;
                newcomp.componentID = CircuitManagerTinker.componentList[i].GetComponent<ComponentTinker>().nameInCircuit;
                newcomp.isSeries = -1;

                ComponentdataTinker[newcomp.componentID] = newcomp;
                foreach (var k in CircuitManagerTinker.componentList[i].GetComponent<ComponentTinker>().nodes)
                {
                    NodedataTinker[k].attached.Add(newcomp.componentID);
                }
            }
        };
        dc.Run(CircuitManagerTinker.ckt);



        // Creating Series list

        foreach (var i in NodedataTinker)
        {
            if (i.Value.attached.Count == 2)
            {
                if (ComponentdataTinker[i.Value.attached[0]].isSeries == -1 && ComponentdataTinker[i.Value.attached[1]].isSeries == -1)
                {
                    print(ComponentdataTinker[i.Value.attached[0]].isSeries + "Before update");
                    var newseries = new StaticData.series();
                    newseries.components = new List<string>();
                    newseries.components.Add(i.Value.attached[0]);
                    newseries.components.Add(i.Value.attached[1]);
                    var temp = ComponentdataTinker[i.Value.attached[0]];

                    temp.isSeries = serieslistTinker.Count;

                    ComponentdataTinker[i.Value.attached[0]] = temp;
                    var temp1 = ComponentdataTinker[i.Value.attached[1]];
                    temp1.isSeries = serieslistTinker.Count;
                    ComponentdataTinker[i.Value.attached[1]] = temp1;
                    serieslistTinker.Add(newseries);
                    print(ComponentdataTinker[i.Value.attached[0]].isSeries + "After update");
                }
                else if (ComponentdataTinker[i.Value.attached[0]].isSeries != -1 && ComponentdataTinker[i.Value.attached[1]].isSeries == -1)
                {
                    StaticData.series t = new StaticData.series();
                    t.components = serieslistTinker[ComponentdataTinker[i.Value.attached[0]].isSeries].components;
                    t.components = t.components.Union(new List<string> { i.Value.attached[1] }).ToList();
                    serieslistTinker[ComponentdataTinker[i.Value.attached[0]].isSeries] = t;
                    var temp = ComponentdataTinker[i.Value.attached[1]];
                    temp.isSeries = ComponentdataTinker[i.Value.attached[0]].isSeries;
                    ComponentdataTinker[i.Value.attached[1]] = temp;
                }
                else if (ComponentdataTinker[i.Value.attached[0]].isSeries == -1 && ComponentdataTinker[i.Value.attached[1]].isSeries != -1)
                {
                    StaticData.series t = new StaticData.series();
                    t.components = serieslistTinker[ComponentdataTinker[i.Value.attached[1]].isSeries].components;
                    t.components = t.components.Union(new List<string> { i.Value.attached[0] }).ToList();
                    serieslistTinker[ComponentdataTinker[i.Value.attached[1]].isSeries] = t;
                    var temp = ComponentdataTinker[i.Value.attached[0]];
                    temp.isSeries = ComponentdataTinker[i.Value.attached[1]].isSeries;
                    ComponentdataTinker[i.Value.attached[0]] = temp;
                }
                else
                {
                    if (ComponentdataTinker[i.Value.attached[0]].isSeries == ComponentdataTinker[i.Value.attached[1]].isSeries)
                    {
                        continue;
                    }
                    else
                    {
                        int max = Math.Max(ComponentdataTinker[i.Value.attached[0]].isSeries, ComponentdataTinker[i.Value.attached[1]].isSeries);
                        int min = Math.Min(ComponentdataTinker[i.Value.attached[0]].isSeries, ComponentdataTinker[i.Value.attached[1]].isSeries);

                        foreach (var item in serieslistTinker[max].components)
                        {
                            var temp = ComponentdataTinker[item];
                            temp.isSeries = min;
                            ComponentdataTinker[item] = temp;
                            serieslistTinker[min].components.Add(item);
                        }
                        for (int k = max + 1; k <= serieslistTinker.Count; k++)
                        {
                            foreach (var item in serieslistTinker[k].components)
                            {
                                var temp = ComponentdataTinker[item];
                                temp.isSeries = temp.isSeries - 1;
                                ComponentdataTinker[item] = temp;
                            }
                        }

                        serieslistTinker.RemoveAt(max);
                    }
                }
            }
            else
            {
                continue;
            }
        }

        // Creating Non-Series Dictionary
        foreach (var i in ComponentdataTinker)
        {
            if (i.Value.isSeries == -1)
            {
                if (NotSeriesTinker.ContainsKey(i.Value.ctype))
                {
                    NotSeriesTinker[i.Value.ctype].Add(i.Key);
                }
                else
                {
                    NotSeriesTinker[i.Value.ctype] = new List<string>();
                    NotSeriesTinker[i.Value.ctype].Add(i.Key);
                }
            }
        }

        for (int i = serieslistTinker.Count - 1; i >= 0; i--)
        {
            for (int j = serieslistTinker[i].components.Count - 1; j >= 0; j--)
            {
                if (ComponentdataTinker[serieslistTinker[i].components[j]].ctype == "wire")
                {
                    serieslistTinker[i].components.RemoveAt(j);
                }
            }
            if (serieslistTinker[i].components.Count == 1)
            {
                if (NotSeriesTinker.ContainsKey(ComponentdataTinker[serieslistTinker[i].components[0]].ctype))
                {
                    NotSeriesTinker[ComponentdataTinker[serieslistTinker[i].components[0]].ctype].Add(serieslistTinker[i].components[0]);
                }
                else
                {
                    NotSeriesTinker[ComponentdataTinker[serieslistTinker[i].components[0]].ctype] = new List<string>();
                    NotSeriesTinker[ComponentdataTinker[serieslistTinker[i].components[0]].ctype].Add(serieslistTinker[i].components[0]);
                }
                serieslistTinker.RemoveAt(i);
            }
        }

        foreach (var i in serieslistTinker)
        {
            print(serieslistTinker.IndexOf(i));
            foreach (var j in i.components)
            {
                print(j);
            }
        }
    }

    public void Evaluate()
    {

    }


}
