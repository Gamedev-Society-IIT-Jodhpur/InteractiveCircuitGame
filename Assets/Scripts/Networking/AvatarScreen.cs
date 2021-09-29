using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarScreen : MonoBehaviour
{
    public GameObject[] avatars;
    int oldavatar = 0;

    public void setAvatar(int index)
    {
        avatars[oldavatar].GetComponent<RawImage>().color = new Color32(37, 37, 92, 255);
        avatars[index].GetComponent<RawImage>().color = new Color32(25, 156, 252, 255);
        oldavatar = index;
    }

}
