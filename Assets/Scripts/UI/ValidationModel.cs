using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ValidationModel : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool isSuccess = false;

    [SerializeField]
    TMP_Text title;
    [SerializeField]
    TMP_Text data;
    [SerializeField]
    Text buttonText;


    void Start()
    {
        transform.localScale = Vector2.zero;
    }

    public void Open()
    {
        isSuccess = !isSuccess;
        if (isSuccess)
        {
            title.text = "Success";
            data.text = "Your circuit is valid";
            buttonText.text = "Continue";
        }
        else
        {
            title.text = "Error";
            data.text = "Your circuit is invalid";
            buttonText.text = "Close";
        }
        transform.LeanScale(Vector2.one, 0.8f);
    }

    public void Close()
    {
        if (isSuccess)
        {
            transform.LeanScale(Vector2.zero, 1.0f).setEaseInBack().setOnComplete(Success);
        }
        else
        {
            transform.LeanScale(Vector2.zero, 1.0f).setEaseInBack();
        }
    }

    public void Success()
    {
        Debug.Log("Success");
    }
}
