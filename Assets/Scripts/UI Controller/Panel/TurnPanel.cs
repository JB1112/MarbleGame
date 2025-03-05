using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnPanel : MonoBehaviour
{
    public List<GameObject> turnchecks;
    public List<TextMeshProUGUI> distances;

    private void Awake()
    {
        GameManager.Instance.turnStart += ChangePlayer;
        GameManager.Instance.CheckScore += WriteDistance;
    }

    private void WriteDistance(float distance)
    {
        int i = GameManager.Instance.turn - 1;
        distances[i].text = distance.ToString("0.00");

    }

    private void ChangePlayer()
    {
        int pre = GameManager.Instance.turn - 2;
        if(pre <0)
        {
            pre = 2;
        }
        int post = GameManager.Instance.turn - 1;

        turnchecks[pre].SetActive(false);
        turnchecks[post].SetActive(true);
    }

    private void OnDisable()
    {
        GameManager.Instance.turnStart -= ChangePlayer;
        GameManager.Instance.CheckScore -= WriteDistance;
    }
}
