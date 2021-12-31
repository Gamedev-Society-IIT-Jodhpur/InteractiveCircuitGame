using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoadManager : MonoBehaviour
{
   
    public void LoadScene(string sceneToLoad)
    {
        
      
        SceneManager.LoadScene(sceneToLoad);
    }
    public void Change(int i)
    {
        PrevCurrScene.prev = PrevCurrScene.curr;
        PrevCurrScene.curr = i;
    }

    public void QuitGame()
    {
        // save any game data here
        #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
