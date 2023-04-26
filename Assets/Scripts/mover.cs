using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{
    public float speed = 5f;
    private Dance1 penguinDance;
    private InputTracker inputtracker => FindObjectOfType<InputTracker>();

    void Start()
    {
        penguinDance = GetComponent<Dance1>();
    }

    void Update()
    {
        if (penguinDance == null || !penguinDance.IsDancing)
        {
            Move();
        }
    }

    void Move()
    {
        Vector3 moveVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveVector.y = 1;
            if (inputtracker.isSquidGameMode)
            {
                inputtracker.isSquidGameMode = false;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveVector.x = -1;
            if (inputtracker.isSquidGameMode)
            {
                inputtracker.isSquidGameMode = false;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVector.y = -1;
            if (inputtracker.isSquidGameMode)
            {
                inputtracker.isSquidGameMode = false;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVector.x = 1;
            if (inputtracker.isSquidGameMode)
            {
                inputtracker.isSquidGameMode = false;
            }
        }

        moveVector.Normalize();

        transform.position += Time.deltaTime * speed * moveVector;

        Vector3 characterScale = transform.localScale;
        if (moveVector.x < 0)
            characterScale.x = -1;
        else if (moveVector.x > 0)
            characterScale.x = 1;
        transform.localScale = characterScale;
    }
}
