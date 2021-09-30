using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    private void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("avatar"));
    }
    public void DeleteComponent()
    {
        if(CircuitManager.selected)
        {
            CircuitManager.selected.GetComponent<Renderer>().material = AssetManager.GetInstance().defaultMaterial;
            CircuitManager.componentList.Remove(CircuitManager.selected);
            Destroy(CircuitManager.selected);
        }
        else
        {
            Debug.Log("No component selected"); 
        }
    } 
}
