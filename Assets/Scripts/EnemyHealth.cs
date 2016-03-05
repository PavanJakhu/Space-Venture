using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class EnemyHealth : NetworkBehaviour
{
    [SyncVar]
    public float maxHeath;

    [SyncVar]
    private float m_health;
    [SyncVar]
    private bool m_alive;

    void Start()
    {
        m_health = maxHeath;
        m_alive = true;
    }

    public void DecreaseHealth(float amt)
    {
        if (!isServer)
        {
            return;
        }

        if (m_health - amt > 0.0f)
        {
            m_health -= amt;
        }
        else
        {
            m_alive = false;
        }
    }

    public void IncreaseHealth(float amt)
    {
        if (!isServer)
        {
            return;
        }

        if (m_health + amt < maxHeath)
        {
            m_health += amt;
        }
        else
        {
            m_health = maxHeath;
        }
    }

    public bool IsAlive()
    {
        return m_alive;
    }

    public float GetHealth()
    {
        return m_health;
    }
}
