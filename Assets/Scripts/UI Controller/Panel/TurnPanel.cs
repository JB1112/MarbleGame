using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnPanel : MonoBehaviour
{
    public List<GameObject> turnchecks;
    public List<TextMeshProUGUI> distances;

    private void Awake()
    {
        GameManager.turnStart += ChangePlayer;
        GameManager.CheckScore += WriteDistance;
    }

    private void WriteDistance(float distance)
    {
        int i = GameManager.turn - 1;
        distances[i].text = distance.ToString("0.00");

    }

    private void ChangePlayer()
    {
        int pre = GameManager.turn - 2;
        if(pre <0)
        {
            pre = 2;
        }
        int post = GameManager.turn - 1;

        turnchecks[pre].SetActive(false);
        turnchecks[post].SetActive(true);
    }
}
