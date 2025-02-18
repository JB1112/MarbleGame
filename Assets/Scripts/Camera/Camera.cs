using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform ball;
    public Vector3 offset;
    public float smoothSpeed = 5f;

    private void LateUpdate()
    {
        Vector3 targetPosition = ball.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
