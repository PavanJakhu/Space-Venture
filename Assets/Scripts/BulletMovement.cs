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
    private Transform playerShip;
    private Rigidbody2D bulletRigidbody;

    void Start()
    {
        scoreKeeper = GameObject.Find("Score Text").GetComponent<ScoreKeeper>();
        bulletRigidbody = GetComponent<Rigidbody2D>();

        Vector2 startPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 endPosition = CrossPlatformInputManager.mousePosition;
        direction = (endPosition - startPosition).normalized;

        float angle = Mathf.Atan2(endPosition.y - startPosition.y, endPosition.x - startPosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90.0f));
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
            int ranNum = Random.Range(1, 10);
            if (ranNum == 1)
            {
                Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
            }
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(coll.gameObject);
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
