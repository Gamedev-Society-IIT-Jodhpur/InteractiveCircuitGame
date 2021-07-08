using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void DeleteComponent()
    {
        CircuitManager.selected.GetComponent<Renderer>().material = AssetManager.GetInstance().defaultMaterial;
        CircuitManager.componentList.Remove(CircuitManager.selected);
        Destroy(CircuitManager.selected);
    } 
}
