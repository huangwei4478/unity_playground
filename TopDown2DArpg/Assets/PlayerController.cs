using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;

    public float collisionOffset = 0.05f;

    public ContactFilter2D movementFilter;

    public SwordAttack swordAttack;

    Vector2 movementInput;

    Rigidbody2D rb;

    List<RaycastHit2D> castCollision = new List<RaycastHit2D>();

    Animator animator;

    SpriteRenderer spriteRenderer;

    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (canMove == false)
        {
            return;
        }

        // If movement is not zero, try to move
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);
            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));
            }

            if (!success)
            {
                success = TryMove(new Vector2(0, movementInput.y));
            }

            animator.SetBool("isMoving", success);
        } else
        {
            animator.SetBool("isMoving", false);
        }

        // Set Character facing
        if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        } else if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            return false;
        }

        int count = rb.Cast(
                direction,
                movementFilter,
                castCollision,
                moveSpeed * Time.deltaTime + collisionOffset
            );

        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        } else
        {
            return false;
        }
    }

    void OnMove(InputValue movementInputValue)
    {
        movementInput = movementInputValue.Get<Vector2>();
    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }

    public void StartAttack()
    {
        if (spriteRenderer.flipX == true)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }

        if (swordAttack == null)
        {
            Debug.Log("StartAttack - swordAttack is null");
        }
        else
        {
            Debug.Log("StartAttackAttack - swordAttack is nonnull");
        }


        LockMovement();
    }

    public void StopAttack()
    {
        if (swordAttack == null) 
        {
            Debug.Log("StopAttack - swordAttack is null");
        } else
        {
            Debug.Log("StopAttack - swordAttack is nonnull");
        }
        swordAttack.StopAttack();
        UnlockMovement();
    }

    private void LockMovement()
    {
        canMove = false;
    }

    private void UnlockMovement()
    {
        canMove = true;
    }
}
