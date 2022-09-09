using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private SpriteRenderer spriteRenderer;

    private RaycastHit2D hit;

    private Vector3 moveDelta;

    private int speed = 4;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveDelta = new Vector3(x, y, 0);

        // check collision for Y-Axis
        hit = Physics2D.BoxCast(transform.position, 
	                            boxCollider.size, 
				                0, 
				                new Vector2(0, moveDelta.y), 
				                Mathf.Abs(moveDelta.y * Time.deltaTime * speed), 
				                LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null) 
	    {
            transform.Translate(0, moveDelta.y * Time.deltaTime * speed, 0);
	    }

        // check collision for X-Axis
        hit = Physics2D.BoxCast(transform.position,
                                boxCollider.size,
                                0,
                                new Vector2(moveDelta.x, 0),
                                Mathf.Abs(moveDelta.x * Time.deltaTime * speed),
                                LayerMask.GetMask("Actor", "Blocking")
                                );

        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime * speed, 0, 0); 
	    }
        
    } 
}
