using UnityEngine;

public class NextScene : MonoBehaviour
{
    //Add in to buttons used for changing screen
    public SceneIndexes from;
    public SceneIndexes to;
    public void ChangScene()
    {
        LoadingManager.instance.LoadGame(from, to);
    }
}
