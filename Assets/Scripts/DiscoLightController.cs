using System.Collections;
using UnityEngine;

public class DiscoLightController : MonoBehaviour
{
    public float minTimeBetweenColorChanges = 1f;
    public float maxTimeBetweenColorChanges = 5f;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor()
    {
        while (true)
        {
            float waitTime = Random.Range(minTimeBetweenColorChanges, maxTimeBetweenColorChanges);
            yield return new WaitForSeconds(waitTime);

            spriteRenderer.color = new Color(Random.value, Random.value, Random.value, 1f);
        }
    }
}
