using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverPopUp : MonoBehaviour
{
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text messageText;
    [SerializeField] TMP_Text tryAgainButtonText;
    [SerializeField] GameObject blocker;
    [SerializeField] GameObject image;

    public void Open()
    {
        image.transform.localScale = Vector2.zero;
        titleText.text = "Not Enough Money!";
        if (SceneManager.GetSceneAt(1).name == "MAP")
        {
            messageText.text = "You don't have enough money left.\nChoose another mode or play again";
            tryAgainButtonText.text = "Choose Another Mode";
        }
        else
        {
            messageText.text = "You don't have enough money left.\nBuy something else or play again";
            tryAgainButtonText.text = "Buy Something Else";
        }
        blocker.SetActive(true);
        image.LeanScale(Vector2.one, 0.5f);

    }


    public void Close()
    {
        image.transform.LeanScale(Vector2.zero, .5f).setEaseInBack();
        blocker.SetActive(false);
    }


    //todo function to call after play again is pressed on game over popup.
    public void GameOver()
    {
        NetworkSingleton.Instance.SetXp();
        //LoadingManager.instance.LoadGame(SceneIndexes.Tinker, SceneIndexes.MainMenu);

        if (SceneManager.GetSceneAt(1).name == "MAP")
        {
            LoadingManager.instance.LoadGame(SceneIndexes.MAP, SceneIndexes.MainMenu);
        }
        else
        {
            LoadingManager.instance.LoadGame(SceneIndexes.Shop, SceneIndexes.MainMenu);
        }
    }
}
