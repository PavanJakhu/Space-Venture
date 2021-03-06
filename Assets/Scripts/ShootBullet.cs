﻿using UnityEngine;
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
                GameObject bullet = Instantiate(bulletPrefab.gameObject, transform.position, Quaternion.identity) as GameObject;
                bullet.GetComponent<BulletMovement>().SetTarget(CrossPlatformInputManager.mousePosition);
                elapsedTime = 0.0f;
            }
        }
    }
}
