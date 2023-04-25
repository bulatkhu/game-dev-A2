using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class Dance1 : MonoBehaviour
{
    public bool isPerformingDanceMove4;
    public KeyCode dance1Key = KeyCode.Alpha1;
    public KeyCode dance2Key = KeyCode.Alpha2;
    public KeyCode dance3Key = KeyCode.Alpha3;
    public KeyCode dance4Key = KeyCode.Alpha4;
    public KeyCode dance5Key = KeyCode.Alpha5;
    public KeyCode dance6Key = KeyCode.Alpha6;
    public float moveDistance = 1f;
    public float moveDuration = 1f;
    public float rotationDuration = 1f;

    public GameObject background;
    public Transform discoLightsParent;
    public float circleGrowthRate = 0.5f;

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
        else if (Input.GetKeyDown(dance4Key))
        {
            isPerformingDanceMove4 = true;
            StartCoroutine(PerformDanceMove4());
        }
        else if (Input.GetKeyDown(dance5Key))
        {
            isPerformingDanceMove4 = true;
            StartCoroutine(PerformDanceMove5());
        }
        else if (Input.GetKeyDown(dance6Key))
        {
            isPerformingDanceMove4 = true;
            StartCoroutine(PerformDanceMove6());
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
    public IEnumerator PerformDanceMove4()
    {
        IsDancing = true;
        float time = 0f;
        float jumpDuration = 1f;
        float jumpHeight = 2f;
        Vector3 startPosition = transform.position;
        Vector3 jumpPosition = startPosition + Vector3.up * jumpHeight;
        Vector3 endPosition = startPosition;

        // Disable the background object
        if (background != null)
        {
            // Set the background color to black with 0 HSV and 0 V
            background.GetComponent<Renderer>().material.color = Color.HSVToRGB(0, 0, 0);
        }

        // Shrink to small size
        while (time < 1.25f)
        {
            float scaleFactor = Mathf.Lerp(1f, 0.5f, time / 1.25f);
            transform.localScale = Vector3.one * scaleFactor;
            transform.Rotate(Vector3.forward, 360f * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }

        // Grow in size and jump up
        time = 0f;
        while (time < jumpDuration / 2f)
        {
            time += Time.deltaTime;
            float height = Mathf.Lerp(0f, jumpHeight, time / (jumpDuration / 2f));
            float scaleFactor = Mathf.Lerp(0.5f, 2f, time / (jumpDuration / 2f));
            transform.localScale = Vector3.one * scaleFactor;
            transform.position = Vector3.Lerp(startPosition, jumpPosition, time / (jumpDuration / 2f)) + Vector3.up * height;
            transform.Rotate(Vector3.forward, 360f * Time.deltaTime);
            yield return null;
        }

        // Jump down and rotate
        time = 0f;
        while (time < jumpDuration / 2f)
        {
            time += Time.deltaTime;
            float height = Mathf.Lerp(jumpHeight, 0f, time / (jumpDuration / 2f));
            float scaleFactor = Mathf.Lerp(2f, 1f, time / (jumpDuration / 2f));
            transform.localScale = Vector3.one * scaleFactor;
            transform.position = Vector3.Lerp(jumpPosition, endPosition, time / (jumpDuration / 2f)) + Vector3.up * height;
            transform.Rotate(Vector3.forward, 360f * Time.deltaTime * 3f);
            yield return null;
        }

        // Grow to normal size and stop rotating
        time = 0f;
        while (time < 1.25f)
        {
            float scaleFactor = Mathf.Lerp(1f, 1f, time / 1.25f);
            transform.localScale = Vector3.one * scaleFactor;
            transform.rotation = Quaternion.identity;
            time += Time.deltaTime;
            yield return null;
        }

        // Enable the background object and set its color back to white
        if (background != null)
        {
            background.GetComponent<Renderer>().material.color = Color.white;
        }
        isPerformingDanceMove4 = false;
        IsDancing = false;
    }



    public IEnumerator PerformDanceMove5()
    {
        IsDancing = true;
        float time = 0f;
        float jumpDuration = 1f;
        float jumpHeight = 2f;
        float rotateSpeed = 720f / 5f; // Two full rotations in 5 seconds
        Vector3 startPosition = transform.position;
        Vector3 jumpPosition = startPosition + Vector3.up * jumpHeight;
        Vector3 endPosition = startPosition - Vector3.right * moveDistance;

        // Disable the background object
        if (background != null)
        {
            // Set the background color to black with 0 HSV and 0 V
            background.GetComponent<Renderer>().material.color = Color.HSVToRGB(0, 0, 0);
        }

        // Shrink to small size and rotate
        while (time < 1f)
        {
            float scaleFactor = Mathf.Lerp(1f, 0.5f, time / 1f);
            transform.localScale = Vector3.one * scaleFactor;
            transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }

        // Move right and grow
        time = 0f;
        while (time < 1f)
        {
            float scaleFactor = Mathf.Lerp(0.5f, 2f, time / 1f);
            transform.localScale = Vector3.one * scaleFactor;
            transform.position = Vector3.Lerp(startPosition, endPosition, time / 1f);
            time += Time.deltaTime;
            yield return null;
        }

        // Jump up and rotate
        time = 0f;
        while (time < jumpDuration)
        {
            float height = Mathf.Lerp(0f, jumpHeight, time / jumpDuration);
            float scaleFactor = Mathf.Lerp(2f, 1f, time / jumpDuration);
            transform.localScale = Vector3.one * scaleFactor;
            transform.position = Vector3.Lerp(endPosition, jumpPosition, time / jumpDuration) + Vector3.up * height;
            transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }

        // Jump down and rotate
        time = 0f;
        while (time < jumpDuration)
        {
            float height = Mathf.Lerp(jumpHeight, 0f, time / jumpDuration);
            float scaleFactor = Mathf.Lerp(1f, 2f, time / jumpDuration);
            transform.localScale = Vector3.one * scaleFactor;
            transform.position = Vector3.Lerp(jumpPosition, endPosition, time / jumpDuration) + Vector3.up * height;
            transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }

        // Move back to center and shrink to normal size
        time = 0f;
        while (time < 1f)
        {
            float scaleFactor = Mathf.Lerp(2f, 1f, time / 1f);
            transform.localScale = Vector3.one * scaleFactor;
            transform.position = Vector3.Lerp(endPosition, startPosition, time / 1f);
            time += Time.deltaTime;
            yield return null;
        }

        // Vanish for 1 second
        time = 0f;
        while (time < 1f)
        {
            float alpha = Mathf.Lerp(1f, 0f, time / 1f);
            var spriteRenderer = GetComponent<SpriteRenderer>();
            var color = spriteRenderer;
            // Stop rotating and reset scale and position
            transform.localScale = Vector3.one;
            transform.position = startPosition;
            // Vanish for 1 second
            time = 0f;
            while (time < 1f)
            {
                float scaleFactor = Mathf.Lerp(1f, 0f, time / 1f);
                transform.localScale = Vector3.one * scaleFactor;
                time += Time.deltaTime;

                yield return null;
            }

            transform.rotation = Quaternion.identity;
            // Enable the background object and set its color back to white
            if (background != null)
            {
                background.GetComponent<Renderer>().material.color = Color.white;
            }
            // Reset scale and position to original values
            transform.localScale = Vector3.one;
            transform.position = startPosition;

            IsDancing = false;
        }
    }


        public IEnumerator PerformDanceMove6()
    {
        IsDancing = true;
        float time = 0f;
        float moveDuration = 3f;
        float initialSize = transform.localScale.x;
        Vector3 initialPosition = transform.position;
        Vector3 topPosition = initialPosition + Vector3.up * 2f * initialSize;
        Vector3 leftPosition = initialPosition + Vector3.left * 2f * initialSize;
        Vector3 bottomPosition = initialPosition + Vector3.down * 2f * initialSize;

        // Disable the background object
        if (background != null)
        {
            // Set the background color to black with 0 HSV and 0 V
            background.GetComponent<Renderer>().material.color = Color.HSVToRGB(0, 0, 0);
        }

        // Move to top and increase size
        while (time < 0.5f)
        {
            time += Time.deltaTime / moveDuration;
            transform.position = Vector3.Lerp(initialPosition, topPosition, time * 2f);
            transform.localScale = Vector3.Lerp(Vector3.one * initialSize, Vector3.one * initialSize * 2f, time * 2f);
            yield return null;
        }

        // Shrink back to normal size
        while (time < 1f)
        {
            time += Time.deltaTime / moveDuration;
            transform.localScale = Vector3.Lerp(Vector3.one * initialSize * 2f, Vector3.one * initialSize, (time - 0.5f) * 2f);
            yield return null;
        }

        // Move to left and rotate
        while (time < 1.5f)
        {
            time += Time.deltaTime / moveDuration;
            transform.position = Vector3.Lerp(topPosition, leftPosition, (time - 1f) * 2f);
            transform.Rotate(Vector3.forward, 360f * Time.deltaTime * 2f);
            yield return null;
        }

        // Move to bottom and flip
        while (time < 2f)
        {
            time += Time.deltaTime / moveDuration;
            transform.position = Vector3.Lerp(leftPosition, bottomPosition, (time - 1.5f) * 2f);
            transform.Rotate(Vector3.forward, 360f * Time.deltaTime * 2f);
            transform.localScale = Vector3.Lerp(Vector3.one * initialSize, Vector3.one * -initialSize, (time - 1.5f) * 2f);
            yield return null;
        }

        // Grow 10 times in size and perform special move
        while (time < 2.5f)
        {
            time += Time.deltaTime / moveDuration;
            transform.localScale = Vector3.Lerp(Vector3.one * -initialSize, Vector3.one * initialSize * 10f, (time - 2f) * 2f);
            // Perform special move
            // ...
            yield return null;
        }

        // Vanish and return to original position
        while (time < 3f)
        {
            time += Time.deltaTime / moveDuration;
            transform.localScale = Vector3.Lerp(Vector3.one * initialSize * 10f, Vector3.zero, (time - 2.5f) * 2f);
            transform.position = Vector3.Lerp(bottomPosition, initialPosition, (time - 2.5f) * 2f);
            yield return null;
        }

        // Reset the transform and size to original values
        transform.position = initialPosition;
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;

        // Enable the background object and set its color back to white
        if (background != null)
        {
            background.GetComponent<Renderer>().material.color = Color.white;
        }
        isPerformingDanceMove4 = false;
        IsDancing = false;
    }


}
