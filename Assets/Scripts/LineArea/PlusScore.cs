using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusScore : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Beads"))
        {
            Debug.Log("ªÁ√‚");
        }
    }
}
