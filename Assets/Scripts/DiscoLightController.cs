using System.Collections;
using UnityEngine;

public class DiscoLightController : MonoBehaviour
{
    private Dance1 dance1;
    private InputTracker inputtracker => FindObjectOfType<InputTracker>();
    public float minTimeBetweenColorChanges = 1f;
    public float maxTimeBetweenColorChanges = 5f;
    private float timer = 0f;
    public bool isRed = false;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        dance1 = FindObjectOfType<Dance1>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (inputtracker.isSquidGameMode == true && timer >= 2.5f && isRed == false)
        {
            spriteRenderer.color = new Color(1f,0f,0f, 1f);
            timer = 0f;
            isRed = true;
        }

        else if (inputtracker.isSquidGameMode == true && timer >= 1.5f && isRed == true)
        {
            spriteRenderer.color = new Color(0f, 1f, 0f, 1f);
            timer = 0f;
            isRed = false;
        }

        // Only change the color of the lights if dancemove4 is not being performed
        else if (dance1.isPerformingDanceMove4 == false && timer >= 1f && inputtracker.isSquidGameMode == false)
        {
            float waitTime = Random.Range(minTimeBetweenColorChanges, maxTimeBetweenColorChanges);
            spriteRenderer.color = new Color(Random.value, Random.value, Random.value, 1f);
            timer = 0f;
        }      
    }

}