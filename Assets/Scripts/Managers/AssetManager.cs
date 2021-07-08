using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    private static AssetManager instace;

    public static AssetManager GetInstance()
    {
        return instace;
    }
    private void Awake()
    {
        instace = this;
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