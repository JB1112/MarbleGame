using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCamera : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.SubCamera = gameObject;

        gameObject.SetActive(false);
    }
}
