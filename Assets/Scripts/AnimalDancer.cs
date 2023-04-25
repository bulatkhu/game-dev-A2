using System.Collections;
using UnityEngine;

public class AnimalDancer : MonoBehaviour
{
    public float moveDistance = 1f;
    public float moveDuration = 1f;
    public float rotationDuration = 1f;
    public float minTimeBetweenMoves = 2f;
    public float maxTimeBetweenMoves = 5f;

    private Vector3 startPosition;
    private bool isDancing;

    private void Start()
    {
        startPosition = transform.position;
        StartCoroutine(PerformRandomDanceMoves());
    }

    private IEnumerator PerformRandomDanceMoves()
    {
        while (true)
        {
            float waitTime = Random.Range(minTimeBetweenMoves, maxTimeBetweenMoves);
            yield return new WaitForSeconds(waitTime);

            if (!isDancing)
            {
                int moveIndex = Random.Range(1, 4); // Randomly select a dance move between 1 and 3

                switch (moveIndex)
                {
                    case 1:
                        StartCoroutine(PerformDanceMove1());
                        break;
                    case 2:
                        StartCoroutine(PerformDanceMove2());
                        break;
                    case 3:
                        StartCoroutine(PerformDanceMove3());
                        break;
                }
            }
        }
    }


    public IEnumerator PerformDanceMove1()
    {
        isDancing = true;
        startPosition = transform.position;
        float time = 0f;

        // Move right
        while (time < moveDuration / 3)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, startPosition + Vector3.right * moveDistance, time / (moveDuration / 3));
            yield return null;
        }

        // Move left
        time = 0f;
        while (time < moveDuration / 3)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition + Vector3.right * moveDistance, startPosition + Vector3.left * moveDistance, time / (moveDuration / 3));
            yield return null;
        }

        // Move back to center
        time = 0f;
        while (time < moveDuration / 3)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition + Vector3.left * moveDistance, startPosition, time / (moveDuration / 3));
            yield return null;
        }

        isDancing = false;
    }

    public IEnumerator PerformDanceMove2()
    {
        isDancing = true;
        startPosition = transform.position;
        float time = 0f;

        Vector3 circleCenter = startPosition - (Vector3.right * moveDistance);
        float angle = 360f;
        while (time < moveDuration)
        {
            time += Time.deltaTime;
            float currentAngle = Mathf.Lerp(0, angle, time / moveDuration);
            transform.position = circleCenter + Quaternion.Euler(0, 0, currentAngle) * (Vector3.right * moveDistance);
            yield return null;
        }

        isDancing = false;
    }
    public IEnumerator PerformDanceMove3()
    {
        isDancing = true;
        float startAngle = 0f;
        float endAngle = 720f; // Two full rotations (2 * 360)
        float time = 0f;

        while (time < rotationDuration)
        {
            time += Time.deltaTime;
            float currentAngle = Mathf.Lerp(startAngle, endAngle, time / rotationDuration);
            transform.rotation = Quaternion.Euler(0, 0, currentAngle);
            yield return null;
        }

        // Reset the rotation to avoid accumulation of floating-point errors
        transform.rotation = Quaternion.identity;
        isDancing = false;
    }
}
