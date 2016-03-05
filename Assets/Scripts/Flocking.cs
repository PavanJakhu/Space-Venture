using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class Flocking : NetworkBehaviour
{
    public Transform supportShipPrefab;
    [SyncVar]
    public int numberOfSupportShips;
    public int maxNumberOfSupportShips;
    public int distanceFromShip;

    private List<Transform> supportShips = new List<Transform>();
    private ScoreKeeper scoreKeeper;
    private float anglePerShip;

    // Use this for initialization
    void Start()
    {
        scoreKeeper = GameObject.Find("Score Text").GetComponent<ScoreKeeper>();

        if (!isServer)
        {
            return;
        }

        CmdSpawnInitSupportShips();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        Vector2 startPosition = transform.position;
        Vector2 endPosition = Camera.main.ScreenToWorldPoint(CrossPlatformInputManager.mousePosition);
        float angle = 0.0f;

        for (int i = 0; i < supportShips.Count; i++)
        {
            supportShips[i].position = new Vector3(transform.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * distanceFromShip,
                                                   transform.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * distanceFromShip,
                                                   0.0f);

            float facingAngle = Mathf.Atan2(endPosition.y - startPosition.y, endPosition.x - startPosition.x) * Mathf.Rad2Deg;
            supportShips[i].rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, facingAngle - 90.0f));

            angle += anglePerShip;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isServer)
        {
            return;
        }

        if (other.gameObject.tag == "Power Up")
        {
            if (supportShips.Count < maxNumberOfSupportShips)
            {
                CmdSpawnPowerUpSupportShips();
            }
            CmdDestroyObject(other.gameObject);
            scoreKeeper.AddScore(5);
        }
    }

    public List<Transform> GetSupportShips()
    {
        return supportShips;
    }
    
    public void RemoveShip(Transform removedShip)
    {
        if (!isServer)
        {
            return;
        }

        numberOfSupportShips--;
        anglePerShip = 360.0f / numberOfSupportShips;
        supportShips.Remove(removedShip);
    }

    [Command]
    void CmdSpawnInitSupportShips()
    {
        anglePerShip = 360.0f / numberOfSupportShips;
        for (int i = 0; i < numberOfSupportShips; i++)
        {
            Transform ship = Instantiate(supportShipPrefab) as Transform;
            //ship.parent = transform;
            supportShips.Add(ship);

            NetworkServer.Spawn(ship.gameObject);
        }
    }

    [Command]
    void CmdSpawnPowerUpSupportShips()
    {
        Transform ship = Instantiate(supportShipPrefab) as Transform;
        numberOfSupportShips++;
        anglePerShip = 360.0f / numberOfSupportShips;
        supportShips.Add(ship);

        NetworkServer.Spawn(ship.gameObject);
    }

    [Command]
    void CmdDestroyObject(GameObject gameobject)
    {
        Destroy(gameobject);

        NetworkServer.Destroy(gameobject);
    }
}
