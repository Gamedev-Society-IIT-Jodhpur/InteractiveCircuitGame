using UnityEngine;

public class ButtonFunctionWrapper : MonoBehaviour
{
    public enum modeOfTransportation
    {
        Cab,
        Walk,
    };
    // Start is called before the first frame update
    public int changeindex;
    public string scenename;
    public SceneIndexes toScene;
    public modeOfTransportation mode;
    public Transform end;
    public int to;


}
