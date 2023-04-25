using System.Collections;
using UnityEngine;

public class Dance1 : MonoBehaviour
{
    public KeyCode dance1Key = KeyCode.Alpha1;
    public KeyCode dance2Key = KeyCode.Alpha2;
    public KeyCode dance3Key = KeyCode.Alpha3;
    public float moveDistance = 1f;
    public float moveDuration = 1f;
    public float rotationDuration = 1f;

    private Vector3 startPosition;
    public bool IsDancing { get; private set; }

    private void Update()
    {
        if (IsDancing) return;

        if (Input.GetKeyDown(dance1Key))
        {
            StartCoroutine(PerformDanceMove1());
        }
        else if (Input.GetKeyDown(dance2Key))
        {
            StartCoroutine(PerformDanceMove2());
        }
        else if (Input.GetKeyDown(dance3Key))
        {
            StartCoroutine(PerformDanceMove3());
        }
    }
    public IEnumerator PerformDanceMove1()
    {
        IsDancing = true;
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

        IsDancing = false;
    }

    public IEnumerator PerformDanceMove2()
    {
        IsDancing = true;
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

        IsDancing = false;
    }
    public IEnumerator PerformDanceMove3()
    {
        IsDancing = true;
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
        IsDancing = false;
    }
}
