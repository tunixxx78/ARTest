using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] int numberToSpawn;
    [SerializeField] List<GameObject> objectsToSpawn;
    [SerializeField] GameObject quad;
    [SerializeField] float obstacleLifetime;

    private void Start()
    {
        SpawnObstacleObjects();
    }

    public void SpawnObstacleObjects()
    {
        int randomItem = 0;
        GameObject toSpawn;
        MeshCollider c = quad.GetComponent<MeshCollider>();

        float screenX, screenY;
        Vector2 pos;

        for(int i = 0; i < numberToSpawn; i++)
        {
            randomItem = Random.Range(0, objectsToSpawn.Count);
            toSpawn = objectsToSpawn[randomItem];

            screenX = Random.Range(c.bounds.min.x, c.bounds.max.x);
            screenY = Random.Range(c.bounds.min.y, c.bounds.max.y);
            pos = new Vector2(screenX, screenY);

            var spawnInstance = Instantiate(toSpawn, pos, Quaternion.identity);
            Destroy(spawnInstance, obstacleLifetime);
        }

        StartCoroutine(ObstacleSpawnLoop());
    }

    IEnumerator ObstacleSpawnLoop()
    {
        yield return new WaitForSeconds(obstacleLifetime);

        SpawnObstacleObjects();
    }
}
