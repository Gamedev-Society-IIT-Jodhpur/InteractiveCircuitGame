using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    public TMP_Text textDisplay;
    [SerializeField]
    public TMP_Text name;
    [SerializeField]
    public RawImage image;
    [SerializeField]
    public string[] sentences;
    [SerializeField]
    public List<Dialogue> sentences_list;
    [SerializeField]
    public float typingSpeed;
    private int index = 0;
    private bool isCoroutineRunning;

    private void Start()
    {
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        isCoroutineRunning = true;
        Dialogue tempDialogue = sentences_list[index];
        name.text = tempDialogue.name;
        image.texture = tempDialogue.image;
        foreach (char letter in tempDialogue.sentences.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isCoroutineRunning = false;
        yield return null;

    }

    public void NextSentence()
    {
        if (!isCoroutineRunning)
        {

            if (index < sentences_list.Count - 1)
            {
                index += 1;
                textDisplay.text = "";
                StartCoroutine(Type());
            }
        }
    }

    public void PreviousSentence()
    {
        if (!isCoroutineRunning)
        {
            if (index > 0)
            {
                index -= 1;
                textDisplay.text = "";
                StartCoroutine(Type());
            }
        }
    }


}
