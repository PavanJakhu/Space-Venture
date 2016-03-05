using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class ShipMovement : MonoBehaviour
{
    public float movementSpeed;

    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        transform.position += move * movementSpeed * Time.deltaTime;
    }
}
