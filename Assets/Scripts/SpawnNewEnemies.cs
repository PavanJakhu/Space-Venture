using UnityEngine;
using System.Collections;

public class SpawnNewEnemies : MonoBehaviour
{
    enum SpawnSide { TOP, BOTTOM, RIGHT, LEFT };

    public GameObject enemyShip;
    public float spawnEnemyAt;

    private float enemySpawnElapsedTime;
    private Vector2 minBounds, maxBounds;

    // Use this for initialization
    void Start()
    {
        enemySpawnElapsedTime = 0.0f;
        minBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        maxBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        enemySpawnElapsedTime += Time.deltaTime;
        if (enemySpawnElapsedTime >= spawnEnemyAt)
        {
            Vector2 spawnPos = Vector2.zero;
            switch ((SpawnSide)Random.Range(0, 4))
            {
                case SpawnSide.TOP:
                    spawnPos = new Vector2(Random.Range(minBounds.x, maxBounds.x), minBounds.y - 1.0f);
                    break;
                case SpawnSide.BOTTOM:
                    spawnPos = new Vector2(Random.Range(minBounds.x, maxBounds.x), maxBounds.y + 1.0f);
                    break;
                case SpawnSide.RIGHT:
                    spawnPos = new Vector2(maxBounds.x + 1.0f, Random.Range(minBounds.y, maxBounds.y));
                    break;
                case SpawnSide.LEFT:
                    spawnPos = new Vector2(minBounds.x - 1.0f, Random.Range(minBounds.y, maxBounds.y));
                    break;
                default:
                    break;
            }
            Instantiate(enemyShip, spawnPos, Quaternion.identity);
            enemySpawnElapsedTime = 0.0f;
        }
    }
}
