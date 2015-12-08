using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class SupportShipCollision : NetworkBehaviour
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
            CmdDestroyObject(coll.gameObject);
            mainShip.RemoveShip(transform);
            CmdDestroyObject(gameObject);
        }
    }

    [Command]
    void CmdDestroyObject(GameObject coll)
    {
        Destroy(coll);

        NetworkServer.Destroy(coll);
    }
}
