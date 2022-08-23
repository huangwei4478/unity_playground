using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    Collider2D collider;
    Vector3 rightOffsetPosition;
    void Start()
    {
        collider = GetComponent<Collider2D>();
        rightOffsetPosition = transform.localPosition;
        collider.enabled = false;
    }

    public void AttackLeft()
    {
        transform.localPosition = new Vector3(-rightOffsetPosition.x, rightOffsetPosition.y);
        collider.enabled = true;
    }

    public void AttackRight()
    {
        transform.localPosition = rightOffsetPosition;
        collider.enabled = false;
    }

    public void StopAttack()
    {
        collider.enabled = false;
    }
}
