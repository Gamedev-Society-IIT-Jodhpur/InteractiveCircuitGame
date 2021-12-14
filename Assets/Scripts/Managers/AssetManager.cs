using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public List<Sprite> tinkerComponentSprites;
    public static Dictionary<String, Sprite> tinkerComponentSpritesDict;

    private static AssetManager instace;
    public GameObject wireManagerinstance;
    public static GameObject wireManager;
    public static bool isSolderingIron;
    public static SolderingIronIcon solderingIronIcon;
    public static DeleteButton deleteButton;
    public static GameObject soldered;
    public GameObject solderedInstance;

    public static AssetManager GetInstance()
    {
        return instace;
    }
    private void Awake()
    {
        instace = this;
        wireManager = wireManagerinstance;
        soldered = solderedInstance;
        
        if(GameObject.FindWithTag("soldering iron icon"))
        {
            isSolderingIron = true;
            solderingIronIcon = GameObject.FindWithTag("soldering iron icon").GetComponent<SolderingIronIcon>();
        }
        if (GameObject.FindWithTag("delete button"))
        {
            deleteButton = GameObject.FindWithTag("delete button").GetComponent<DeleteButton>();
        }
        

        if (tinkerComponentSprites.Count != 0)
        {
            tinkerComponentSpritesDict = new Dictionary<string, Sprite>(){
            { "voltage9",tinkerComponentSprites[0]},
            { "breadboard",tinkerComponentSprites[1]},
            { "led",tinkerComponentSprites[2]},
            { "resistor",tinkerComponentSprites[3]},
            { "voltage1.5",tinkerComponentSprites[4]},
            { "bjtnpn",tinkerComponentSprites[5]},
            { "bjtpnp",tinkerComponentSprites[6]},
            { "diode",tinkerComponentSprites[7]},
            { "zenerDiode",tinkerComponentSprites[8]},
            { "gizmo",tinkerComponentSprites[9]},
            };
        }
    }

    public Material outlineMaterial;
    public Material defaultMaterial;

    

    




    //public SoundAudioClip[] soundAudioClipArray;
    /*

    [Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }*/
}