using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class ShipMovement : NetworkBehaviour
{
    public float movementSpeed;

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        transform.position += move * movementSpeed * Time.deltaTime;
    }
}
