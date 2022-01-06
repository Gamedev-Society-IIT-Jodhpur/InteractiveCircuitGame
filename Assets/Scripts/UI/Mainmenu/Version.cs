using TMPro;
using UnityEngine;

public class Version : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TMP_Text>().text = PlayerPrefs.GetString("player_email");
    }
}
