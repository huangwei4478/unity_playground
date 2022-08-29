using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float health = 1;

    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                Defeated();
            }
        }

        get
        {
            return health;
        }
    }

    private void Defeated()
    {
        Destroy(gameObject);
    }
}
