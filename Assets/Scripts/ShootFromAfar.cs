using UnityEngine;
using System.Collections;

public class ShootFromAfar : MonoBehaviour
{
    public float movementSpeed;
    public Transform dustParticleSystemPrefab;

    private TryAgain tryAgainButton;
    private Transform playerShip;
    private Vector2 crossedPoint;

    // Use this for initialization
    void Start()
    {
        tryAgainButton = GameObject.Find("UI").GetComponent<TryAgain>();
        playerShip = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerShip)
        {
            float angle = Mathf.Atan2(playerShip.transform.position.y - transform.position.y, playerShip.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, angle + 90.0f));

            if (Vector2.Distance(crossedPoint, transform.position) <= 2.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0.0f, 0.0f), movementSpeed * Time.deltaTime);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            var ships = coll.gameObject.GetComponent<Flocking>().GetSupportShips();
            for (int s = 0; s < ships.Count; s++)
            {
                Destroy(ships[s].gameObject);
            }
            Instantiate(dustParticleSystemPrefab, transform.position, Quaternion.identity);
            Destroy(coll.gameObject);

            tryAgainButton.SetTryAgainButtonActive(true);
        }
    }

    public void SetSpawnSide(SpawnNewEnemies.SpawnSide side)
    {
        Vector2 minBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        Vector2 maxBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

        switch (side)
        {
            case SpawnNewEnemies.SpawnSide.TOP:
                crossedPoint = new Vector2(transform.position.x, minBounds.y);
                break;
            case SpawnNewEnemies.SpawnSide.BOTTOM:
                crossedPoint = new Vector2(transform.position.x, maxBounds.y);
                break;
            case SpawnNewEnemies.SpawnSide.RIGHT:
                crossedPoint = new Vector2(maxBounds.x, transform.position.y);
                break;
            case SpawnNewEnemies.SpawnSide.LEFT:
                crossedPoint = new Vector2(minBounds.x, transform.position.y);
                break;
            default:
                break;
        }
    }
}
