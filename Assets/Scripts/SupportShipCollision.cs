using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SupportShipCollision : MonoBehaviour
{
    private Flocking mainShip;

    void Start()
    {
        mainShip = GameObject.FindGameObjectWithTag("Player").GetComponent<Flocking>();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Destroy(coll.gameObject);
            mainShip.RemoveShip(transform);
            Destroy(gameObject);
        }
    }
}
