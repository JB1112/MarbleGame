using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    private Rigidbody rb;
    private BallsMovement move;
    public AudioSource audio;

    private string beads = "Beads";
    private string EndLine = "EndLine";

    private void Awake()
    {
        GameManager.Instance.turnStart += CheckMyTurn;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        move = GetComponent<BallsMovement>();
    }

    private void CheckMyTurn()
    {
        Debug.Log(GameManager.Instance.mainGameTurn[GameManager.Instance.turn - 1]);
        Debug.Log(GameManager.Instance.PlayerNumber);
        if (GameManager.Instance.mainGameTurn[GameManager.Instance.turn - 1] >= GameManager.Instance.PlayerNumber)
        {
            GameManager.Instance.isMoving = false;
            StartCoroutine(ShootBall());
        }
    }

    private IEnumerator ShootBall()
    {
        while (GameManager.Instance.isWaiting)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        Vector3 targetPosition = FindBestTarget();

        Vector3 direction = (targetPosition - transform.position).normalized;

        float force = CalculateForce(targetPosition);

        rb.AddForce(direction * force, ForceMode.Impulse);

        audio.Play();
        GameManager.Instance.isMoving = true;
        move.CheckStop();
    }

    private float CalculateForce(Vector3 targetPosition)
    {
        float acceleration = 0;
        Vector3 curPosition = transform.position;

        float distance = Vector3.Distance(curPosition, targetPosition);

        float mass = rb.mass;

        if(GameManager.Instance.isSetTurn)
        {
            float randomMultiplier = Random.Range(1.0f, 1.5f);

            acceleration = distance * randomMultiplier;
        }
        else
        {
            float randomMultiplier = Random.Range(2.5f, 3.0f);

            acceleration = distance * randomMultiplier;
        }

        float force = mass * acceleration;

        return force;
    }

    private Vector3 FindBestTarget()
    {
        Vector3 targetpos;

        if(GameManager.Instance.isSetTurn)
        {
            GameObject Line = GameObject.FindGameObjectWithTag(EndLine);

            targetpos = Line.transform.position;
        }
        else
        {
            GameObject[] opponentBeads = GameObject.FindGameObjectsWithTag(beads);

            List<GameObject> closestBeads = opponentBeads
    .OrderBy(b => Vector3.Distance(b.transform.position, transform.position))
    .Take(3)
    .ToList();

            GameObject chosenBead = closestBeads[Random.Range(0, closestBeads.Count)];

            targetpos = chosenBead.transform.position;
        }

        float randomOffsetX = Random.Range(-0.2f, 0.2f);

        targetpos.x += randomOffsetX;

        return targetpos;
    }

    private void OnDisable()
    {
        GameManager.Instance.turnStart -= CheckMyTurn;
    }
}
