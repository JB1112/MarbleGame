using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour
{
    public GameObject ball;
    public void Onclick()
    {
        ball.transform.position = new Vector3(25f, 0.5f, 5f);
        ball.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
