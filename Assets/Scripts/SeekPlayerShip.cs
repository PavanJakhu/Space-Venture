using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class SeekPlayerShip : NetworkBehaviour
{
    public float seekMovement;
    public Transform dustParticleSystemPrefab;
    
    private TryAgain tryAgainButton;
    private Transform playerShip; 

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
            transform.position = Vector2.MoveTowards(transform.position, playerShip.position, Time.deltaTime * seekMovement);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            var ships = coll.gameObject.GetComponent<Flocking>().GetSupportShips();
            for (int s = 0; s < ships.Count; s++)
            {
                CmdDestroyObject(ships[s].gameObject);
            }
            CmdSpawnDust();
            CmdDestroyObject(coll.gameObject);

            tryAgainButton.SetTryAgainButtonActive(true);
        }
    }

    [Command]
    void CmdSpawnDust()
    {
        GameObject dust = Instantiate(dustParticleSystemPrefab, transform.position, Quaternion.identity) as GameObject;

        NetworkServer.Spawn(dust);
    }

    [Command]
    void CmdDestroyObject(GameObject coll)
    {
        Destroy(coll);

        NetworkServer.Destroy(coll);
    }
}
