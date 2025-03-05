using UnityEngine;

public class PlusScore : MonoBehaviour
{
    string beadTag = "Beads";
    private string player = "Player";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(beadTag))
        {
            Beads beadScript = other.GetComponent<Beads>();

            if (beadScript != null)
            {
                if(beadScript.isout == true)
                {
                    return;
                }
                beadScript.isout = true;

                GameManager.Instance.outBall++;
                GameManager.Instance.CheckScore(GameManager.Instance.outBall);
            }
        }
        else if (other.CompareTag(player))
        {
            GameManager.Instance.isIn = false;
        }
    }
}
