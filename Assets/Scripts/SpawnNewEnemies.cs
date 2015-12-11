using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class SpawnNewEnemies : NetworkBehaviour
{
    public enum SpawnSide { TOP, BOTTOM, RIGHT, LEFT };

    public GameObject[] enemyShips;
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
            CmdSpawnEnemyShip();

            enemySpawnElapsedTime = 0.0f;
        }
    }

    [Command]
    void CmdSpawnEnemyShip()
    {
        Vector2 spawnPos = Vector2.zero;
        SpawnSide sideSpawned = (SpawnSide)Random.Range(0, 4);
        switch (sideSpawned)
        {
            case SpawnSide.TOP:
                spawnPos = new Vector2(Random.Range(minBounds.x, maxBounds.x), minBounds.y - 1.0f);
                sideSpawned = SpawnSide.TOP;
                break;
            case SpawnSide.BOTTOM:
                spawnPos = new Vector2(Random.Range(minBounds.x, maxBounds.x), maxBounds.y + 1.0f);
                sideSpawned = SpawnSide.BOTTOM;
                break;
            case SpawnSide.RIGHT:
                spawnPos = new Vector2(maxBounds.x + 1.0f, Random.Range(minBounds.y, maxBounds.y));
                sideSpawned = SpawnSide.RIGHT;
                break;
            case SpawnSide.LEFT:
                spawnPos = new Vector2(minBounds.x - 1.0f, Random.Range(minBounds.y, maxBounds.y));
                sideSpawned = SpawnSide.LEFT;
                break;
            default:
                break;
        }

        int randomShip = Random.Range(0, enemyShips.Length);
        GameObject spawnedShip = Instantiate(enemyShips[randomShip], spawnPos, Quaternion.identity) as GameObject;

        if (spawnedShip.GetComponent<ShootFromAfar>() != null)
        {
            spawnedShip.GetComponent<ShootFromAfar>().SetSpawnSide(sideSpawned);
        }

        NetworkServer.Spawn(spawnedShip);
    }
}
