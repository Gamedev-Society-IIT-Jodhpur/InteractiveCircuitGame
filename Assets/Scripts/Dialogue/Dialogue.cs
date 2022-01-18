using UnityEngine;

[System.Serializable]
public class Dialogue
{

    public string name;
    public Texture image;

    [TextArea(3, 10)]
    public string sentences;

}