using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class BulletMovement : NetworkBehaviour
{
    public float movementSpeed;
    public Transform powerUpPrefab;
    public Transform explosionPrefab;
    
    private ScoreKeeper scoreKeeper;
    private Vector2 direction;
    private GameObject playerShip;
    private Rigidbody2D bulletRigidbody;

    void Start()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player");
        scoreKeeper = GameObject.Find("Score Text").GetComponent<ScoreKeeper>();
        bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Stuff
        bulletRigidbody.AddForce(direction * movementSpeed);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            scoreKeeper.AddScore(10);

            EnemyHealth healthScript = coll.gameObject.GetComponent<EnemyHealth>();
            healthScript.DecreaseHealth(1.0f);
            if (!healthScript.IsAlive())
            {
                CmdSpawnPowerUp();

                CmdSpawnExplosion();

                CmdDestroyObject(coll.gameObject);
            }

            CmdDestroyObject(gameObject);
        }
        else if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Support Ship")
        {
            if (coll.gameObject.tag == "Player")
            {
                var ships = coll.gameObject.GetComponent<Flocking>().GetSupportShips();
                for (int s = 0; s < ships.Count; s++)
                {
                    CmdDestroyObject(ships[s].gameObject);
                }
            }
            else if (coll.gameObject.tag == "Support Ship")
            {
                playerShip.GetComponent<Flocking>().RemoveShip(coll.gameObject.transform);
            }
            CmdDestroyObject(coll.gameObject);
            CmdDestroyObject(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        CmdDestroyObject(gameObject);
    }

    public void SetTarget(Vector2 target)
    {
        Vector2 startPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 endPosition = target;
        direction = (endPosition - startPosition).normalized;

        float angle = Mathf.Atan2(endPosition.y - startPosition.y, endPosition.x - startPosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90.0f));
    }

    [Command]
    void CmdSpawnPowerUp()
    {
        int ranNum = Random.Range(1, 10);
        if (ranNum == 1)
        {
            GameObject powerup = Instantiate(powerUpPrefab, transform.position, Quaternion.identity) as GameObject;

            NetworkServer.Spawn(powerup);
        }
    }

    [Command]
    void CmdSpawnExplosion()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;

        NetworkServer.Spawn(explosion);
    }

    [Command]
    void CmdDestroyObject(GameObject coll)
    {
        Destroy(coll);

        NetworkServer.Destroy(coll);
    }
}
