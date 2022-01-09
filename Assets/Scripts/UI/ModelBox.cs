using UnityEngine;

public class ModelBox : MonoBehaviour
{
    public Transform box;
    public CanvasGroup background;


    private void OnEnable()
    {
        background.alpha = 0;
        background.LeanAlpha(1, 0.5f);
        box.localPosition = new Vector2(0, -Screen.height);
        box.LeanMoveLocalY(0, 0.5f).setEaseOutExpo();

        //box.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;
    }

    public void CloseDialogue()
    {
        background.LeanAlpha(0, 0.5f);
        box.LeanMoveLocalY(-2160, 1.0f).setEaseOutExpo().setOnComplete(OnComplete);
    }

    void OnComplete()
    {
        gameObject.SetActive(false);
    }

    public void StartQuest()
    {
        MoneyAndXPData.InitiateMoney(500);
        LoadingManager.instance.LoadGame(SceneIndexes.Dialogue, SceneIndexes.Falstad);
    }

}
