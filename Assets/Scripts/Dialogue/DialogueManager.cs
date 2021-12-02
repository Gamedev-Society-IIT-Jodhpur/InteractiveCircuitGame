using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    public TMP_Text textDisplay;
    [SerializeField]
    public TMP_Text name_avatar;
    [SerializeField]
    public RawImage image;
    [SerializeField]
    public List<Dialogue> sentences_list;
    [SerializeField]
    public float typeIntervel;

    public Image previousButtonImg;
    public Image nextButtonImg;
    public Text nextButtonText;

    private int index = 0;
    private bool isCoroutineRunning;
    private Color defaultColor;
    private Color disabledColor;
    private Color continueColor;

    private void Start()
    {
        textDisplay.text = "";
        ColorUtility.TryParseHtmlString("#0894F7", out defaultColor);
        ColorUtility.TryParseHtmlString("#CAD3C8", out disabledColor);
        ColorUtility.TryParseHtmlString("#F97F51", out continueColor);

        previousButtonImg.color = disabledColor;
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        isCoroutineRunning = true;
        nextButtonImg.color = disabledColor;
        previousButtonImg.color = disabledColor;
        Dialogue tempDialogue = sentences_list[index];
        name_avatar.text = tempDialogue.name;
        image.texture = tempDialogue.image;
        foreach (char letter in tempDialogue.sentences.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typeIntervel);
        }
        isCoroutineRunning = false;
        nextButtonImg.color = defaultColor;
        if (index > 0)
        {
            previousButtonImg.color = defaultColor;
        }
        yield return null;

    }

    public void NextSentence()
    {
        if (!isCoroutineRunning)
        {
            if (index < sentences_list.Count - 1)
            {
                nextButtonImg.color = defaultColor;
                previousButtonImg.color = defaultColor;
                textDisplay.text = "";
                index += 1;
                StartCoroutine(Type());
            }
        }
        if (index == sentences_list.Count - 1)
        {
            nextButtonImg.color = continueColor;
            nextButtonText.text = "Continue";
        }
    }

    public void PreviousSentence()
    {
        if (!isCoroutineRunning)
        {
            if (index > 0)
            {
                previousButtonImg.color = defaultColor;
                nextButtonImg.color = defaultColor;
                nextButtonText.text = "Next";
                textDisplay.text = "";
                index -= 1;
                StartCoroutine(Type());
            }
        }
        if (index == 0)
        {
            previousButtonImg.color = disabledColor;
        }
    }

}
