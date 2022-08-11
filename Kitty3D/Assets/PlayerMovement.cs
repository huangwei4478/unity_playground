using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 m_movement;
    private float m_horizontal;
    private float m_vertial;
    private Animator m_animator;
    private Rigidbody m_rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        m_horizontal = Input.GetAxis("Horizontal");
        m_vertial = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        m_movement.Set(m_horizontal, 0.0f, m_vertial);
        m_movement.Normalize();

        bool hasHorizontalMove = !Mathf.Approximately(m_horizontal, 0.0f);
        bool hasVerticalMove = !Mathf.Approximately(m_vertial, 0.0f);

        bool isWalking = hasHorizontalMove || hasVerticalMove;

        m_animator.SetBool("isWalking", isWalking);
    }

    private void OnAnimatorMove()
    {
        m_rigidbody.MovePosition(m_rigidbody.position + m_movement * m_animator.deltaPosition.magnitude);
    }
}
