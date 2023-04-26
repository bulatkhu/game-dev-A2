using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTracker : MonoBehaviour
{
    private string inputString = "";
    private bool isNinja = false;
    private bool isDogeMode = false;

    public List<GameObject> animalGameObjects;
    public Sprite dogeSprite;
    private List<Sprite> originalAnimalSprites;


    public bool isSquidGameMode = false;


    public Sprite starSprite;
    public GameObject starPrefab;

    public Sprite mushroomSprite;
    public GameObject mushroomPrefab;

    public GameObject shyGuyPrefab;
    public GameObject gameOverPrefab;


    void Start()
    {
        originalAnimalSprites = new List<Sprite>();
        foreach (GameObject animal in animalGameObjects)
        {
            originalAnimalSprites.Add(animal.GetComponent<SpriteRenderer>().sprite);
        }
    }

    void Update()
    {

        if (Input.anyKeyDown)
        {
            inputString += Input.inputString.ToLower();

            // NINJA BLOCK
            if (inputString.Contains("ninja") && isNinja == false)
            {
                // Make the player transparent and slower
                GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
                GetComponent<mover>().speed /= 2f;
                isNinja = true;
                // Reset the input string
                inputString = "";
            }
            else if (inputString.Contains("ninja") && isNinja == true)
            {
                // Make the player not transparent and faster again
                GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
                GetComponent<mover>().speed *= 2f;
                isNinja = false;
                // Reset the input string
                inputString = "";
            }



            // DOGE BLOCK
            if (inputString.Contains("doge"))
            {
                isDogeMode = !isDogeMode;
                for (int i = 0; i < animalGameObjects.Count; i++)
                {
                    SpriteRenderer animalSpriteRenderer = animalGameObjects[i].GetComponent<SpriteRenderer>();
                    if (isDogeMode)
                    {
                        animalSpriteRenderer.sprite = dogeSprite;
                    }
                    else
                    {
                        animalSpriteRenderer.sprite = originalAnimalSprites[i];
                    }
                }
                // Reset the input string
                inputString = "";
            }




            // SQUIDGAME BLOCK
            if (inputString.Contains("squidgame"))
            {
                isSquidGameMode = true;
                inputString = "";

            }


            // STARS BLOCK
            if (inputString.Contains("stars"))
            {
                SpawnStars();
                // Reset the input string
                inputString = "";
            }

            // POWERUP BLOCK
            if (inputString.Contains("powerup"))
            {
                SpawnMushrooms();
                inputString = "";
            }

            // GAMEOVER BLOCK
            if (inputString.Contains("gameover"))
            {
                StartCoroutine(SpawnGameOverAnimation());
                inputString = "";
            }

        }
    }

    private void SpawnStars()
    {
        Camera mainCamera = Camera.main;

        for (int i = 0; i < 100; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f), 0f);
            Vector3 spawnPosition = mainCamera.transform.position + randomOffset;
            spawnPosition.z = -1f; // Set the z-axis position to be in front of other objects

            GameObject star = Instantiate(starPrefab, spawnPosition, Quaternion.identity);
            star.GetComponent<SpriteRenderer>().sprite = starSprite;
            star.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground"; // Set the stars' sorting layer to be on top of other objects

            StartCoroutine(AnimateStar(star));
        }
    }

    private IEnumerator AnimateStar(GameObject star)
    {
        float elapsedTime = 0f;
        float duration = 8f;
        Vector3 originalScale = star.transform.localScale;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Jump and rotate
            star.transform.position += Vector3.up * Mathf.Sin(Time.time * 10f) * Time.deltaTime;
            star.transform.Rotate(0f, 0f, 360f * Time.deltaTime);

            // Shrink and grow
            float scaleMultiplier = 1f + 0.5f * Mathf.Sin(Time.time * 5f);
            star.transform.localScale = originalScale * scaleMultiplier;

            yield return null;
        }

        // Destroy the star after the animation is finished
        Destroy(star);
    }

    private void SpawnMushrooms()
    {
        Camera mainCamera = Camera.main;
        float screenHeight = mainCamera.orthographicSize * 2f;
        float screenWidth = screenHeight * mainCamera.aspect;

        for (int i = 0; i < 25; i++)
        {
            float randomX = Random.Range(-screenWidth / 2f, screenWidth / 2f);
            Vector3 spawnPosition = mainCamera.transform.position + new Vector3(randomX, mainCamera.orthographicSize, 0f);
            spawnPosition.z = -1f;

            GameObject mushroom = Instantiate(mushroomPrefab, spawnPosition, Quaternion.identity);
            mushroom.GetComponent<SpriteRenderer>().sprite = mushroomSprite;
            mushroom.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";

            // Random size
            float randomSize = Random.Range(0.5f, 2f);
            mushroom.transform.localScale = new Vector3(randomSize, randomSize, 1f);

            // Different color value for S in HSV
            float hue = Random.Range(0f, 1f);
            float saturation = Random.Range(0.2f, 1f);
            float value = 1f;
            Color mushroomColor = Color.HSVToRGB(hue, saturation, value);
            mushroom.GetComponent<SpriteRenderer>().color = mushroomColor;

            StartCoroutine(AnimateMushroom(mushroom));
        }
    }

    private IEnumerator AnimateMushroom(GameObject mushroom)
    {
        float duration = 6f;
        float elapsedTime = 0f;
        Vector3 startPosition = mushroom.transform.position;
        Vector3 endPosition = startPosition - new Vector3(0f, Camera.main.orthographicSize * 2f, 0f);
        Vector3 middlePosition = (startPosition + endPosition) / 2;

        // First half of the animation (mushrooms slowly drop down)
        while (elapsedTime < duration / 2)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / (duration / 2);

            Vector3 position = Vector3.Lerp(startPosition, middlePosition, t);
            mushroom.transform.position = position;

            yield return null;
        }

        // Reset elapsed time
        elapsedTime = 0f;

        // Second half of the animation (mushrooms jump up to the middle)
        while (elapsedTime < duration / 2)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / (duration / 2);

            Vector3 position = Vector3.Lerp(middlePosition, endPosition, t);
            position.y += 4f * (1 - 4 * Mathf.Pow(t - 0.5f, 2));
            mushroom.transform.position = position;

            yield return null;
        }

        Destroy(mushroom);
    }


    private IEnumerator SpawnGameOverAnimation()
    {
        // Spawn gameover sprite in the middle and grow in size
        Vector3 gameOverPosition = Camera.main.transform.position;
        gameOverPosition.z = -1f;
        GameObject gameOver = Instantiate(gameOverPrefab, gameOverPosition, Quaternion.identity);
        gameOver.transform.localScale = Vector3.zero;

        float elapsedTime = 0f;
        while (elapsedTime < 3f)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / 3f;
            gameOver.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            yield return null;
        }

        // Spawn shy guys from right with random HSV colors
        int spawnedShyGuysCount = 0;
        while (spawnedShyGuysCount < 50)
        {
            Vector3 startPosition = Camera.main.transform.position + new Vector3(Camera.main.aspect * Camera.main.orthographicSize, Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize), -1f);
            GameObject shyGuy = Instantiate(shyGuyPrefab, startPosition, Quaternion.identity);

            // Set z-axis position to -1
            Vector3 shyGuyPosition = shyGuy.transform.position;
            shyGuyPosition.z = -1f;
            shyGuy.transform.position = shyGuyPosition;

            SpriteRenderer spriteRenderer = shyGuy.GetComponent<SpriteRenderer>();
            Color newColor;
            Color.RGBToHSV(spriteRenderer.color, out float H, out float S, out float V);
            H = Random.Range(0f, 1f);
            newColor = Color.HSVToRGB(H, S, V);
            spriteRenderer.color = newColor;

            StartCoroutine(MoveShyGuyToLeft(shyGuy));
            spawnedShyGuysCount++;

            yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
        }

        // Destroy gameover sprite after 8 seconds
        yield return new WaitForSeconds(5f);
        Destroy(gameOver);
    }

    private IEnumerator MoveShyGuyToLeft(GameObject shyGuy)
    {
        float elapsedTime = 0f;
        float moveDuration = (Camera.main.aspect * Camera.main.orthographicSize * 2f / 5);

        Vector3 startPosition = shyGuy.transform.position;
        Vector3 endPosition = Camera.main.transform.position + new Vector3(-Camera.main.aspect * Camera.main.orthographicSize - 3f, startPosition.y - Camera.main.transform.position.y, -1f);


        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;
            shyGuy.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        Destroy(shyGuy);
    }




}
