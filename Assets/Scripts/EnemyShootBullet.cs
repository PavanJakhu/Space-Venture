using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class EnemyShootBullet : NetworkBehaviour
{
    public BulletMovement bulletPrefab;
    public float rateOfFire;

    private float elapsedTime;
    private GameObject playerShip;

    // Use this for initialization
    void Start()
    {
        elapsedTime = 0.0f;

        playerShip = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            return;
        }

        elapsedTime += Time.deltaTime;

        if (playerShip && elapsedTime >= rateOfFire)
        {
            CmdSpawnBullet();

            elapsedTime = 0.0f;
        }
    }

    [Command]
    void CmdSpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab.gameObject, transform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<BulletMovement>().SetTarget(Camera.main.WorldToScreenPoint(playerShip.transform.position));

        NetworkServer.Spawn(bullet);
    }
}
