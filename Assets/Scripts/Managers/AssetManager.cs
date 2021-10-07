using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public List<Sprite> tinkerIcons;
    public static Dictionary<String, Sprite> tinkerIconsDict;

    private static AssetManager instace;
    public GameObject wireManagerinstance;
    public static GameObject wireManager;

    public static AssetManager GetInstance()
    {
        return instace;
    }
    private void Awake()
    {
        instace = this;
        wireManager = wireManagerinstance;
        

        if (tinkerIcons.Count != 0)
        {
            tinkerIconsDict = new Dictionary<string, Sprite>(){
            { "voltage9",tinkerIcons[0]},
            { "breadboard",tinkerIcons[1]},
            { "led",tinkerIcons[2]},
            { "resistor",tinkerIcons[3]},
            { "voltage1.5",tinkerIcons[4]}
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