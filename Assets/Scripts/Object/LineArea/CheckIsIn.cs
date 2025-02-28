using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIsIn : MonoBehaviour
{
    private string player = "Player";
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(player))
        {
            GameManager.isIn = true;
            Debug.Log($"µé¾î°¨{GameManager.isIn}");
        }
    }
}
