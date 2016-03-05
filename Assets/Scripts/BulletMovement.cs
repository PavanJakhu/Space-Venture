using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class BulletMovement : MonoBehaviour
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
                int ranNum = Random.Range(1, 10);
                if (ranNum == 1)
                {
                    Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
                }

                Instantiate(explosionPrefab, transform.position, Quaternion.identity);

                Destroy(coll.gameObject);
            }
            
            Destroy(gameObject);
        }
        else if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Support Ship")
        {
            if (coll.gameObject.tag == "Player")
            {
                var ships = coll.gameObject.GetComponent<Flocking>().GetSupportShips();
                for (int s = 0; s < ships.Count; s++)
                {
                    Destroy(ships[s].gameObject);
                }
            }
            else if (coll.gameObject.tag == "Support Ship")
            {
                playerShip.GetComponent<Flocking>().RemoveShip(coll.gameObject.transform);
            }
            Destroy(coll.gameObject);
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void SetTarget(Vector2 target)
    {
        Vector2 startPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 endPosition = target;
        direction = (endPosition - startPosition).normalized;

        float angle = Mathf.Atan2(endPosition.y - startPosition.y, endPosition.x - startPosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90.0f));
    }
}
