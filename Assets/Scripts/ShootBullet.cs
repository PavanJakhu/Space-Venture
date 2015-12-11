using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class ShootBullet : NetworkBehaviour
{
    public BulletMovement bulletPrefab;
    public float rateOfFire;

    private float elapsedTime;

    void Start()
    {
        elapsedTime = 0.0f;
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            return;
        }

        elapsedTime += Time.deltaTime;
        
        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            if (elapsedTime >= rateOfFire)
            {
                CmdSpawnBullet();

                elapsedTime = 0.0f;
            }
        }
    }

    [Command]
    void CmdSpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab.gameObject, transform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<BulletMovement>().SetTarget(CrossPlatformInputManager.mousePosition);

        NetworkServer.Spawn(bullet);
    }
}
