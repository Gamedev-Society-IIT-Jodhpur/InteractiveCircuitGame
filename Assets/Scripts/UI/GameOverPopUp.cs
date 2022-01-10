using TMPro;
using UnityEngine;
public class GameOverPopUp : MonoBehaviour
{
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text messageText;
    [SerializeField] GameObject blocker;
    [SerializeField] GameObject image;

    public void Open()
    {
        //image.transform.localScale = Vector2.zero;
        titleText.text = "Not Enough Money!";
        messageText.text = "You don't have enough money left.\nBuy something else or play again";
        blocker.SetActive(true);
        //image.LeanScale(Vector2.one, 0.5f);

    }


    public void Close()
    {
        //image.transform.LeanScale(Vector2.zero, .5f).setEaseInBack();
        blocker.SetActive(false);
    }


    //todo function to call after play again is pressed on game over popup.
    public void GameOver()
    {

    }
}
