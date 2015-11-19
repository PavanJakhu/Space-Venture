using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class ShootBullet : MonoBehaviour
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
        elapsedTime += Time.deltaTime;

        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            if (elapsedTime >= rateOfFire)
            {
                Instantiate(bulletPrefab.gameObject, transform.position, Quaternion.identity);
                elapsedTime = 0.0f;
            }
        }
    }
}
