using UnityEngine;

public class Beads : MonoBehaviour
{
    public bool isout = false;

    private void OnEnable()
    {
        isout = false;
        GameManager.CheckBead += CheckIsOut;
    }

    public void CheckIsOut()
    {
        if(isout)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        GameManager.CheckBead -= CheckIsOut;
    }
}
