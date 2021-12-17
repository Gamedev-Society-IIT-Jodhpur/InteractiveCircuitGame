using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    //Add in to buttons used for changing screen
    public SceneIndexes from;
    public SceneIndexes to;
    public void ChangScene()
    {
        LoadingManager.instance.LoadGame(from,to);
    }
}
