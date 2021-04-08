using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowChild : MonoBehaviour
{
    [SerializeField] Transform follow = null;
    Vector3 originalPosition;

    private void Awake()
    {
        originalPosition = follow.localPosition;
    }

    private void Update()
    {
        transform.position = follow.position;
    }
}
