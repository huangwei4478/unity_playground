using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damage = 3;

    public Collider2D swordCollider;
    Vector3 rightOffsetPosition;
    void Start()
    {
        rightOffsetPosition = transform.localPosition;
        swordCollider.enabled = false;
    }

    public void AttackLeft()
    {
        transform.localPosition = new Vector3(-rightOffsetPosition.x, rightOffsetPosition.y);
        swordCollider.enabled = true;
    }

    public void AttackRight()
    {
        transform.localPosition = rightOffsetPosition;
        swordCollider.enabled = true;
    }

    public void HitBoxStopAttack()
    {
        swordCollider.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            // Deal damange to the enemy
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.Health -= damage;
            }
                
        }
    }
}
