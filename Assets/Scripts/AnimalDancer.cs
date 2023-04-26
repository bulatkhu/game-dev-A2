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

    private DiscoLightController discolightcontroller => FindObjectOfType<DiscoLightController>();

    private InputTracker inputtracker => FindObjectOfType<InputTracker>();


    private void Start()
    {
        startPosition = transform.position;
        StartCoroutine(PerformRandomDanceMoves());
    }
  
    private void Update()
    {
        Renderer objectRenderer = GetComponent<Renderer>();


        if (inputtracker.isSquidGameMode == false)
        {
            objectRenderer.material.color = new Color(1f, 1f, 1f, 1f);
            objectRenderer.enabled = true;
        }
        else if (discolightcontroller.isRed == true && isDancing)
        {
            StartCoroutine(VanishObjectSlowly(1.5f));
        }

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


    private IEnumerator VanishObjectSlowly(float duration)
    {
        Renderer objectRenderer = GetComponent<Renderer>();
        Color startColor = objectRenderer.material.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        float time = 0f;
        while (time < duration)
        {

            if (inputtracker.isSquidGameMode == false)
            {
                objectRenderer.material.color = new Color(1f, 1f, 1f, 1f);
                objectRenderer.enabled = true;
                break;
            }
            time += Time.deltaTime;
            objectRenderer.material.color = Color.Lerp(startColor, endColor, time / duration);
            yield return null;
        }

        objectRenderer.material.color = endColor;
        objectRenderer.enabled = false;
    }


}
