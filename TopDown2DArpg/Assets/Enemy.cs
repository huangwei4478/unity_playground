using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float health = 1;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                animator.SetTrigger("Defeated");
            }
        }

        get
        {
            return health;
        }
    }

    public void EnemyDefeated()
    {
        Destroy(gameObject);
    }
}
