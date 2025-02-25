using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.CheckScore += Write;
    }

    private void Write(float score)
    {
        if (GameManager.isSetTurn == true)
        {
            switch (GameManager.turn)
            {
                case 1:
                    break;

                case 2:
                    break;

                case 3:
                    break;
            }
        }
        else
        {
            switch (GameManager.turn)
            {
                case 1:
                    break;

                case 2:
                    break;

                case 3:
                    break;
            }
        }
    }
}
