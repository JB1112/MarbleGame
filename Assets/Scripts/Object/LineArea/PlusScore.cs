using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusScore : MonoBehaviour
{
    string beadTag = "Beads";

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(beadTag))
        {
            Debug.Log("����");
        }
    }
}
