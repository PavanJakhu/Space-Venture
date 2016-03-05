using UnityEngine;
using System.Collections;

public class EnemyShootBullet : MonoBehaviour
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
        elapsedTime += Time.deltaTime;

        if (playerShip && elapsedTime >= rateOfFire)
        {
            GameObject bullet = Instantiate(bulletPrefab.gameObject, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<BulletMovement>().SetTarget(Camera.main.WorldToScreenPoint(playerShip.transform.position));
            elapsedTime = 0.0f;
        }
    }
}
