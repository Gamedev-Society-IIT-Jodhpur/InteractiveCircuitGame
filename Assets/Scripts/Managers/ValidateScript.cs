using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Simulations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class ValidateScript : MonoBehaviour
{
    [SerializeField]
    GameObject circuitManager;
    [SerializeField]
    public static GameObject gizmo;

    string addResult = AvailableRoutes.addResult;
    string addErrors = AvailableRoutes.addError;


    // Components in series
    //---- Components in series falstad
    List<StaticData.series> serieslistFalstad = new List<StaticData.series>();
    //---- Components in series tinker
    List<StaticData.series> serieslistTinker = new List<StaticData.series>();

    // Components in seriesd
    Dictionary<string, StaticData.node> NodedataFalstad = new Dictionary<string, StaticData.node>();
    Dictionary<string, StaticData.ComponentValidate> ComponentdataFalstad = new Dictionary<string, StaticData.ComponentValidate>();

    Dictionary<string, StaticData.node> NodedataTinker = new Dictionary<string, StaticData.node>();
    Dictionary<string, StaticData.ComponentValidate> ComponentdataTinker = new Dictionary<string, StaticData.ComponentValidate>();

    // Components not in series
    Dictionary<string, List<string>> NotSeriesFalstad = new Dictionary<string, List<string>>();
    Dictionary<string, List<string>> NotSeriesTinker = new Dictionary<string, List<string>>();

    // GameObject data in string form
    static List<string> serieslistFalstadModified = new List<string>();
    static List<string> serieslistTinkerModified = new List<string>();
    static Dictionary<string, List<string>> NotSeriesFalstadModified = new Dictionary<string, List<string>>();
    static Dictionary<string, List<string>> NotSeriesTinkerModified = new Dictionary<string, List<string>>();


    public List<string> ModifySeries(List<StaticData.series> series, Dictionary<string, StaticData.ComponentValidate> Componentdata)
    {
        var ans = new List<string>();
        foreach (var i in series)
        {
            var temp = new List<string>();
            foreach (var j in i.components)
            {
                //print("loop works");
                string formattedstring = Componentdata[j].ctype;
                formattedstring += (" " + Componentdata[j].value);
                for (int k = 0; k < Componentdata[j].I.Count; k++)
                {
                    formattedstring += (" " + Math.Round(Componentdata[j].I[k], 5));
                }
                for (int k = 0; k < Componentdata[j].V.Count; k++)
                {
                    formattedstring += (" " + Math.Round(Componentdata[j].V[k], 5));
                }
                formattedstring += (" " + Componentdata[j].beta.ToString());
                temp.Add(formattedstring);
            }
            temp.Sort();
            var output = "";
            foreach (var k in temp)
            {
                output += k + "$";
            }
            ans.Add(output);
        }
        ans.Sort();
        return ans;
    }
    public Dictionary<string, List<string>> ModifyDict(Dictionary<string, List<string>> Notseries, Dictionary<string, StaticData.ComponentValidate> Componentdata)
    {
        var a = new Dictionary<string, List<string>>();
        foreach (var i in Notseries)
        {
            var templist = new List<string>();
            foreach (var j in i.Value)
            {
                string formattedstring = Componentdata[j].value;
                for (int k = 0; k < Componentdata[j].I.Count; k++)
                {
                    formattedstring += (" " + Math.Round(Componentdata[j].I[k], 5));
                }
                for (int k = 0; k < Componentdata[j].V.Count; k++)
                {
                    formattedstring += (" " + Math.Round(Componentdata[j].V[k], 5));
                }
                formattedstring += (" " + Componentdata[j].beta.ToString());
                templist.Add(formattedstring);
            }

            templist.Sort();
            a[i.Key] = templist;
        }
        return a;
    }

    public bool CheckSpecs()
    {
        if (gizmo == null)
        {
            CustomNotificationManager.Instance.AddNotification(1, "Gizmo Not Present. Please add gizmo to your design.");
            return false;
        }
        else
        {
            bool passed = false;
            var n_zener = 0;
            string zenername = "";
            double zenervzk =-2;
             List<string> zenernodes = new List<string>();
            foreach (var j in CircuitManager.componentList)
            {
                if (j.GetComponent<ComponentInitialization>().a == CircuitManager.component.zenerDiode)
                {
                    n_zener += 1;
                    zenername = j.GetComponent<ComponentInitialization>().nameInCircuit;
                    zenernodes = j.GetComponent<ComponentInitialization>().nodes;
                    zenervzk = j.GetComponent<ComponentInitialization>().beta;
                }
            }
            if (n_zener == 1)
            {
                passed = true;
            }
            else
            {
                CustomNotificationManager.Instance.AddNotification(2, "Exactly one Zener must be present in the circuit");
                passed = false;

                return passed;
            }
            if (passed) {
                passed = false;

                //Thevnin
                Circuit ckt3 = new Circuit();
                Circuit ckt4 = new Circuit();
                foreach (var i in CircuitManager.ckt)
                {
                    ckt3.Add(i);
                }
                foreach (var i in CircuitManager.ckt)
                {
                    ckt4.Add(i);
                }

                ckt3.Remove(zenername);
                ckt4.Remove(zenername);
                ckt4.Add(new Resistor("AWire", zenernodes[0], zenernodes[1], 0));
                var dc3 = new DC("dc", CircuitManager.volt.GetComponent<ComponentInitialization>().nameInCircuit, double.Parse(CircuitManager.volt.GetComponent<ComponentInitialization>().value), double.Parse(CircuitManager.volt.GetComponent<ComponentInitialization>().value), 0.001);
                var dc4 = new DC("dc", CircuitManager.volt.GetComponent<ComponentInitialization>().nameInCircuit, double.Parse(CircuitManager.volt.GetComponent<ComponentInitialization>().value), double.Parse(CircuitManager.volt.GetComponent<ComponentInitialization>().value), 0.001);
                var currentExport4 = new RealPropertyExport(dc4, "AWire", "i");
                var ThevninV = -1.0;
                var ThevninI = -1.0;
                dc3.ExportSimulationData += (sender, exportDataEventArgs) =>
                {
                    
                        ThevninV = exportDataEventArgs.GetVoltage(zenernodes[1], zenernodes[0]);
                        
                    
                };
                dc4.ExportSimulationData += (sender, exportDataEventArgs) =>
                {
                    
                        ThevninI = Math.Abs(currentExport4.Value);
                       
                        if (Math.Abs(ThevninV / ThevninI - (ThevninV - zenervzk) / 0.2) <= 0.0001 && ThevninV >= 9 && ThevninV <= 10)
                        {
                            passed = true;
                        }
                        else
                        {
                            passed = false;
                        }
                    
                };


                //Gizmo Conditions
                
                string node1 = (Mathf.RoundToInt(gizmo.GetComponentsInChildren<Transform>()[1].position.x)).ToString() + " " + (Mathf.RoundToInt(gizmo.GetComponentsInChildren<Transform>()[1].position.y)).ToString();
            string node2 = (Mathf.RoundToInt(gizmo.GetComponentsInChildren<Transform>()[2].position.x)).ToString() + " " + (Mathf.RoundToInt(gizmo.GetComponentsInChildren<Transform>()[2].position.y)).ToString();
            Circuit ckt1 = new Circuit();
            foreach (var i in CircuitManager.ckt)
            {
                ckt1.Add(i);
            }
            ckt1.Add(new Resistor("CheckResistor1", node1, node2, 2.0e15));
            Circuit ckt2 = new Circuit();
            foreach (var i in CircuitManager.ckt)
            {
                ckt2.Add(i);
            }
            ckt2.Add(new Resistor("CheckResistor1", node1, node2, 30));
            var dc1 = new DC("dc", CircuitManager.volt.GetComponent<ComponentInitialization>().nameInCircuit, double.Parse(CircuitManager.volt.GetComponent<ComponentInitialization>().value), double.Parse(CircuitManager.volt.GetComponent<ComponentInitialization>().value), 0.001);
            var dc2 = new DC("dc", CircuitManager.volt.GetComponent<ComponentInitialization>().nameInCircuit, double.Parse(CircuitManager.volt.GetComponent<ComponentInitialization>().value), double.Parse(CircuitManager.volt.GetComponent<ComponentInitialization>().value), 0.001);
            var currentExport1 = new RealPropertyExport(dc1, "CheckResistor1", "i");
            var currentExport2 = new RealPropertyExport(dc2, "CheckResistor1", "i");
            List<RealPropertyExport> currentexportsources = new List<RealPropertyExport>();
            List<RealPropertyExport> currentexportsources1 = new List<RealPropertyExport>();
            foreach (var i in ckt1.ByType<VoltageSource>())
            {
                currentexportsources.Add(new RealPropertyExport(dc1, i.Name, "i"));
            }
            foreach (var i in ckt1.ByType<VoltageSource>())
            {
                currentexportsources1.Add(new RealPropertyExport(dc2, i.Name, "i"));
            }
            dc1.ExportSimulationData += (sender, exportDataEventArgs) =>
            {
                if (passed)
                {
                   
                    if (Math.Round(Math.Abs(exportDataEventArgs.GetVoltage(node1, node2)) / ThevninV, 1) >= 0.6
                    && Math.Abs(exportDataEventArgs.GetVoltage(node1, node2)) * Math.Abs(currentExport1.Value)  <= 1.2
                    && Math.Abs(exportDataEventArgs.GetVoltage(zenernodes[0], zenernodes[1]) + zenervzk) <= 0.2
                    && Math.Abs(Math.Abs(exportDataEventArgs.GetVoltage(node1, node2)) - 6) <= 0.2
                    )
                    {
                        passed = true;
                        
                    }
                    
                } };
            
            dc2.ExportSimulationData += (sender, exportDataEventArgs) =>
            {
                
               
                if (passed == true)
                {
                    

                    if (Math.Abs(Math.Abs(exportDataEventArgs.GetVoltage(node1, node2)) - 6) <= 0.2
                    && Math.Round(Math.Abs(exportDataEventArgs.GetVoltage(node1, node2))  / ThevninV ,1) >= 0.6
                    && Math.Abs(exportDataEventArgs.GetVoltage(node1, node2)) * Math.Abs(currentExport2.Value)  <= 1.2
                    &&  Math.Abs(exportDataEventArgs.GetVoltage(zenernodes[0], zenernodes[1]) + zenervzk) <= 0.2)
                    {
                        Debug.LogError((Math.Abs(exportDataEventArgs.GetVoltage(node1, node2))  / ThevninV).ToString());
                        passed = true;
                        
                    }
                    else
                    {
                        
                        passed = false;
                    }
                }

            };
                
                

                try
            {
                
                dc3.Run(ckt3);
                dc4.Run(ckt4);
                dc1.Run(ckt1);
                dc2.Run(ckt2);

                }
            catch (Exception e)
            {
                CustomNotificationManager.Instance.AddNotification(2, "Invalid Circuit");

                //print(e.Message);
                passed = false;
            }

            return passed;
        }
            else
            {
                passed = false;
                return passed;
            }
    } 
    }

    public void SaveDataFalstad()
    {
        circuitManager.GetComponent<CircuitManager>().Play();
        serieslistFalstad.Clear();
        NodedataFalstad.Clear();
        ComponentdataFalstad.Clear();
        NotSeriesFalstad.Clear();
        NotSeriesFalstadModified.Clear();
        serieslistFalstadModified.Clear();

        if (CheckSpecs())
        {
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
            try
            {
                string node1 = (Mathf.RoundToInt(gizmo.GetComponentsInChildren<Transform>()[1].position.x)).ToString() + " " + (Mathf.RoundToInt(gizmo.GetComponentsInChildren<Transform>()[1].position.y)).ToString();
                string node2 = (Mathf.RoundToInt(gizmo.GetComponentsInChildren<Transform>()[2].position.x)).ToString() + " " + (Mathf.RoundToInt(gizmo.GetComponentsInChildren<Transform>()[2].position.y)).ToString();
                var dc = new DC("dc", CircuitManager.volt.GetComponent<ComponentInitialization>().nameInCircuit, double.Parse(CircuitManager.volt.GetComponent<ComponentInitialization>().value), double.Parse(CircuitManager.volt.GetComponent<ComponentInitialization>().value), 0.001);
                CircuitManager.ckt.Add(new Resistor("CheckResistor1", node1, node2, 999999999));
                var currentExport1 = new RealPropertyExport(dc, "CheckResistor1", "i");
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
                        newcomp.beta = CircuitManager.componentList[i].GetComponent<ComponentInitialization>().beta;
                        newcomp.value = CircuitManager.componentList[i].GetComponent<ComponentInitialization>().value;

                        ComponentdataFalstad[newcomp.componentID] = newcomp;
                        foreach (var k in CircuitManager.componentList[i].GetComponent<ComponentInitialization>().nodes)
                        {
                            NodedataFalstad[k].attached.Add(newcomp.componentID);
                        }
                    }
                    var newcomp1 = new StaticData.ComponentValidate();
                    newcomp1.V = new List<double>();
                    newcomp1.I = new List<double>();
                    newcomp1.V.Add(Math.Abs(exportDataEventArgs.GetVoltage(node1, node2)));
                    newcomp1.I.Add(Math.Abs(currentExport1.Value));
                    newcomp1.ctype = "resistor";
                    newcomp1.nodes = new List<string>() { node1,node2};
                    newcomp1.componentID = "CheckResistor1";
                    newcomp1.isSeries = -1;
                    newcomp1.beta = 6;
                    newcomp1.value = "999999999";
                    ComponentdataFalstad[newcomp1.componentID] = newcomp1;
                    foreach (var k in new List<string>() { node1, node2 })
                    {
                        NodedataFalstad[k].attached.Add(newcomp1.componentID);
                    }

                };

                try
                {
                    dc.Run(CircuitManager.ckt);
                }
                catch (Exception e)
                {
                    CustomNotificationManager.Instance.AddNotification(2, "Invalid circuit");

                    //print(e.Message);
                }
            }
            catch (Exception e)
            {
                CustomNotificationManager.Instance.AddNotification(2, "No voltage/ no component selected");

                //print(e.Message);
            }
            // Creating Series list

            foreach (var i in NodedataFalstad)
            {
                if (i.Value.attached.Count == 2)
                {
                    if (ComponentdataFalstad[i.Value.attached[0]].isSeries == -1 && ComponentdataFalstad[i.Value.attached[1]].isSeries == -1)
                    {
                        //print(ComponentdataFalstad[i.Value.attached[0]].isSeries + "Before update");
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
                        //print(ComponentdataFalstad[i.Value.attached[0]].isSeries + "After update");
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
                        NotSeriesFalstad[ComponentdataFalstad[serieslistFalstad[i].components[0]].ctype].Add(serieslistFalstad[i].components[0]);
                    }

                    serieslistFalstad.RemoveAt(i);
                }
            }

            NotSeriesFalstadModified = ModifyDict(NotSeriesFalstad, ComponentdataFalstad);
            serieslistFalstadModified = ModifySeries(serieslistFalstad, ComponentdataFalstad);
            //foreach (var i in serieslistFalstadModified)
            //{
            //    print(i);

            //}
            /* foreach (var i in ComponentdataFalstad)
             {
                 print(i.Key);
                 print(i.Value.ctype);
                 print(i.Value.value);
             }*/

            ValidationModel.isSuccess = true;
            ValidationModel.Instance.Open();
        }
        else
        {
            ValidationModel.isSuccess = false;
            ValidationModel.Instance.Open();
            CustomNotificationManager.Instance.AddNotification(2, "Circuit doesn't meet specifications");
            //print("Circuit doesn't meet specifications");
            ScoringScript.UpdateError(3);
        }
    }

    public void SaveDataTinker()
    {

        circuitManager.GetComponent<CircuitManagerTinker>().Play();
        serieslistTinker.Clear();
        NodedataTinker.Clear();
        ComponentdataTinker.Clear();
        NotSeriesTinker.Clear();
        NotSeriesTinkerModified.Clear();
        serieslistTinkerModified.Clear();
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
        try
        {
           /* string node1 = "";
            string node2 = "";
            var childs = gizmo.GetComponentsInChildren<NodeTinker>();
            var a = "";
            var b="";
            var c = "";
            var d = "";
            if (childs[0].transform.position.x < 0) a = (childs[0].transform.position.x).ToString().Substring(0, 4);
            else a = (childs[0].transform.position.x).ToString().Substring(0, 3);
            if (childs[0].transform.position.y < 0) b = (childs[0].transform.position.y).ToString().Substring(0, 4);
            else b = (childs[0].transform.position.y).ToString().Substring(0, 3);
            if (childs[1].transform.position.x < 0) c = (childs[1].transform.position.x).ToString().Substring(0, 4);
            else c = (childs[1].transform.position.x).ToString().Substring(0, 3);
            if (childs[1].transform.position.y < 0) d = (childs[1].transform.position.y).ToString().Substring(0, 4);
            else d = (childs[1].transform.position.y).ToString().Substring(0, 3);

            node1 = a+ " " + b;
            node2 = c + " " + d;*/
            /*print("Node1"+node1);*/
            var dc = new DC("dc", CircuitManagerTinker.volt.GetComponent<ComponentTinker>().nameInCircuit,
                                    double.Parse(CircuitManagerTinker.volt.GetComponent<ComponentTinker>().value),
                                    double.Parse(CircuitManagerTinker.volt.GetComponent<ComponentTinker>().value), 0.001);
            var currentExport = new List<RealPropertyExport>();
            /*CircuitManagerTinker.ckt.Add(new Resistor("CheckResistor1", node1, node2, 2.0e15));
            var currentExport1 = new RealPropertyExport(dc, "CheckResistor1", "i");*/
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
                    newcomp.beta = CircuitManagerTinker.componentList[i].GetComponent<ComponentTinker>().beta;
                    newcomp.value = CircuitManagerTinker.componentList[i].GetComponent<ComponentTinker>().value;

                    ComponentdataTinker[newcomp.componentID] = newcomp;
                    foreach (var k in CircuitManagerTinker.componentList[i].GetComponent<ComponentTinker>().nodes)
                    {
                        NodedataTinker[k].attached.Add(newcomp.componentID);
                    }
                }
                /*var newcomp1 = new StaticData.ComponentValidate();
                newcomp1.V = new List<double>();
                newcomp1.I = new List<double>();
                newcomp1.V.Add(Math.Abs(exportDataEventArgs.GetVoltage(node1, node2)));
                newcomp1.I.Add(Math.Abs(currentExport1.Value));
                newcomp1.ctype = "resistor";
                newcomp1.nodes = new List<string>() { node1, node2 };
                newcomp1.componentID = "CheckResistor1";
                newcomp1.isSeries = -1;
                newcomp1.beta = 6;
                newcomp1.value = "2.0e15";
                ComponentdataTinker[newcomp1.componentID] = newcomp1;*/
               /* foreach (var k in new List<string>() { node1, node2 })
                {
                    NodedataTinker[k].attached.Add(newcomp1.componentID);
                }*/
            };
            try
            {
                dc.Run(CircuitManagerTinker.ckt);
            }
            catch (SpiceSharp.Simulations.ValidationFailedException e)
            {

                CustomNotificationManager.Instance.AddNotification(2, "Invalid circuit");

                //print(e.Message);
            }
        }
        catch (Exception e)
        {
            CustomNotificationManager.Instance.AddNotification(2, "No voltage/ no component selected");
            print(e.Message);
        }
        // Creating Series list

        foreach (var i in NodedataTinker)
        {
            if (i.Value.attached.Count == 2)
            {
                if (ComponentdataTinker[i.Value.attached[0]].isSeries == -1 && ComponentdataTinker[i.Value.attached[1]].isSeries == -1)
                {
                    //print(ComponentdataTinker[i.Value.attached[0]].isSeries + "Before update");
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
                    //print(ComponentdataTinker[i.Value.attached[0]].isSeries + "After update");
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

                        for (int k = max + 1; k < serieslistTinker.Count; k++)
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


        NotSeriesTinkerModified = ModifyDict(NotSeriesTinker, ComponentdataTinker);
        serieslistTinkerModified = ModifySeries(serieslistTinker, ComponentdataTinker);
        foreach (var i in NotSeriesTinkerModified)
        {
            print(i.Key);
            foreach (var j in i.Value)
            {
                print(j);
            }
        }
        foreach (var i in NotSeriesFalstadModified)
        {
            print(i.Key);
            foreach (var j in i.Value)
            {
                print(j);
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
        foreach (var i in serieslistTinkerModified)
        {
            print(i);
        }
        foreach (var i in serieslistFalstadModified)
        {
            print(i);
        }
        Evaluate();
    }

    public void Evaluate()
    {
        bool dictsame = true;
        if (NotSeriesFalstadModified.Count == NotSeriesTinkerModified.Count)
        {
            foreach (var i in NotSeriesFalstadModified)
            {
                if (NotSeriesTinkerModified.ContainsKey(i.Key))
                {
                    bool dictequal = NotSeriesFalstadModified[i.Key].SequenceEqual(NotSeriesTinkerModified[i.Key]);
                    if (!dictequal)
                    {
                        dictsame = false;
                        break;
                    }
                }
                else
                {
                    dictsame = false;
                    break;
                }
            }
        }
        else
        {
            dictsame = false;
        }

        bool seriesequal = serieslistFalstadModified.SequenceEqual(serieslistTinkerModified);

        if (seriesequal && dictsame == true)
        {
            List<int>errors = ScoringScript.GetError();
            //print("Hurray You have Passed");
            Timer.StopTimer();

            FirstSolderBreakPopUp.Instance.Open(GoToResult, "Your design is correct.\nTotal Time: "+(int)Timer.currentTime/60+"min and "+ (int)Timer.currentTime % 60+"sec"+
                "\nXP: " +MoneyAndXPData.xp+
                "\n <u>ERRORS</u>" +
                "\nWrong circuit in Falstad : " + errors[3] +
                "\nUsed Non-stand. components : " + errors[1] +
                "\nForgot to buy component : " + errors[2] +
                "\nBroke Solder " + errors[0] +
                "\nWrong circuit in Tinker : " + errors[4] , 
                "Congratulations!!", "Submit Circuit & Check Score");
        }                                                                                                                                                                                                                                                             
        else                                                                                                                                                                                                                   
        {                                                                                                                                                                                         
            serieslistTinker.Clear();                                                                                                                                                             
            NodedataTinker.Clear();                                                                                                                                    
            ComponentdataTinker.Clear();                                                                                                            
            NotSeriesTinker.Clear();
            NotSeriesTinkerModified.Clear();
            serieslistTinkerModified.Clear();
            //print("Failed");
            //print("Dict Comparison:" + dictsame);
            //print("List Comparison:" + seriesequal);
            FirstSolderBreakPopUp.Instance.Open(FirstSolderBreakPopUp.Instance.Close, "Your circuit doesn't match the design you made in the circuit simulator.", "Sorry!", "Try Again");
            ScoringScript.UpdateError(4);
        }
    }


    void GoToResult()
    {
        StaticData.Inventory.Clear();
        string email = PlayerPrefs.GetString("player_email", "");
        StartCoroutine(AddErrorsCoroutine(email));
    }

    IEnumerator AddErrorsCoroutine(string email)
    {
        int time = (int)Timer.currentTime;
        int money = (int)MoneyAndXPData.money;
        int score = (int)ScoringScript.CalcScore();
        int xp = (int)MoneyAndXPData.xp;
        WWWForm postError = new WWWForm();

        List<int> errors = ScoringScript.GetError();

        postError.AddField("email", email);
        postError.AddField("Solder_Break", errors[0]);
        postError.AddField("Non_Standard", errors[1]);
        postError.AddField("Forgets_To_Buy", errors[2]);
        postError.AddField("Doesnt_Meet_Specs", errors[3]);
        postError.AddField("Circuit_Doesnt_Match", errors[4]);
        postError.AddField("Component_Damage", errors[5]);

        UnityWebRequest uwr = UnityWebRequest.Post(addErrors, postError);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            CustomNotificationManager.Instance.AddNotification(2, "Can't submit errors");
        }
        else
        {
            StartCoroutine(AddResultCoroutine(email, time, money, score, xp));
        }
    }

    IEnumerator AddResultCoroutine(string email, int time, int money, int score, int xp)
    {
        WWWForm postResult = new WWWForm();

        postResult.AddField("email", email);
        postResult.AddField("time", time);
        postResult.AddField("money", money);
        postResult.AddField("score", score);
        postResult.AddField("xp", xp);

        UnityWebRequest uwr = UnityWebRequest.Post(addResult, postResult);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            CustomNotificationManager.Instance.AddNotification(2, "Can't submit the result");
        }
        else
        {
            LoadingManager.instance.LoadGame(SceneIndexes.Tinker, SceneIndexes.Result);
        }
    }
}
