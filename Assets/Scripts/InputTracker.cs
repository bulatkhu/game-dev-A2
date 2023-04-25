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


        }
    }
}
