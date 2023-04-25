using UnityEngine;

public class DiscoLightSpawner : MonoBehaviour
{
    public GameObject discoLightPrefab;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnDiscoLight();
        }
    }

    private void SpawnDiscoLight()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(mainCamera.ViewportToWorldPoint(Vector3.zero).x, mainCamera.ViewportToWorldPoint(Vector3.one).x),
                                            Random.Range(mainCamera.ViewportToWorldPoint(Vector3.zero).y, mainCamera.ViewportToWorldPoint(Vector3.one).y),
                                            0);

        Instantiate(discoLightPrefab, spawnPosition, Quaternion.identity);
    }
}
